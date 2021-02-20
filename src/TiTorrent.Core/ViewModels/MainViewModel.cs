using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MonoTorrent;
using MonoTorrent.BEncoding;
using MonoTorrent.Client;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using TiTorrent.Core.Models;
using TiTorrent.Dialogs.Models;
using TiTorrent.Dialogs.Views;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TiTorrent.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region PublicProps

        public ObservableCollection<DataGridModel> TorrentsCollection { get; set; } = new();
        public DataGridModel SelectedTorrent { get; set; }

        public PivotModel MainPivotModel { get; set; }

        #endregion

        #region PrivateProps

        private static readonly string TorrentsFolderPath = Path.Combine($"{ApplicationData.Current.LocalFolder.Path}", "State", "Torrents");
        private static readonly string DhtNodesPath = $"{ApplicationData.Current.LocalFolder.Path}\\State\\DhtNodes.dat";
        private static readonly string FastResumePath = $"{ApplicationData.Current.LocalFolder.Path}\\State\\FastResume.dat";

        private readonly DispatcherTimer _timer = new();
        private ClientEngine _clientEngine = new();

        #endregion

        #region Constructors

        public MainViewModel()
        {
            // Загрузка сохранений
            Task.Run(async () =>
            {
                _clientEngine = await LoadState();

                Thread.Sleep(2000);

                await CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    _clientEngine?.Torrents?
                        .ToList()
                        .ForEach(manager =>
                        {
                            manager.StartAsync();
                            TorrentsCollection.Add(new DataGridModel(manager));
                        });
                });
            });

            // Запускаем таймер обновления
            MainTimer();
        }
        
        #endregion

        #region Commands

        public ICommand BAddCommand => new RelayCommand(async () =>
        {
            try
            {
                // Создаем форму добавления
                var addDialog = new AddDialog();
                if (await addDialog.ShowAsync() != ContentDialogResult.Primary)
                {
                    return;
                }

                // Проверяем, на повтор
                if (_clientEngine.Torrents.FirstOrDefault(m => m.Equals(AddDialogModel.Manager)) != null)
                {
                    throw new Exception("Торрент уже существует!");
                }

                var manager = AddDialogModel.Manager;

                // Добавляем в коллекцию и регистрируем в движке
                TorrentsCollection.Add(new DataGridModel(manager));
                await _clientEngine.Register(manager);

                // Начинаем загрузку
                await AddDialogModel.Manager.StartAsync();
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message, "Error!").ShowAsync();
            }
        });

        public ICommand BDeleteCommand => new RelayCommand(async () =>
        {
            try
            {
                // Менеджер
                var manager = _clientEngine.Torrents.First(tm => tm.InfoHash.ToHex().Equals(SelectedTorrent.Hash));
                
                // Удаление загруженных файлов
                var isFilesDelete = false;

                // Создаем форму вопроса, передаем ей менеджер и ждем ответа
                var deleteDialog = new DeleteDialog();
                MessengerInstance.Send(manager);
                MessengerInstance.Register<bool>(this, value => { isFilesDelete = value; });
                if (await deleteDialog.ShowAsync() != ContentDialogResult.Primary)
                {
                    return;
                }
                MessengerInstance.Unregister<bool>(isFilesDelete);

                // Удаляем торрент
                await manager.StopAsync();
                await _clientEngine.Unregister(manager);
                TorrentsCollection.Remove(SelectedTorrent);

                // Удаляем загруженные файлы
                if (isFilesDelete)
                {
                    var path = $"{manager.SavePath}\\{manager.Torrent.Name}";

                    if (Directory.Exists(path))
                    {
                        Directory.Delete(path);
                    }
                    else if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }

            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message, "Error!").ShowAsync();
            }
        }, () => SelectedTorrent != null);

        public ICommand BStartCommand => new RelayCommand(async () =>
        {
            try
            {
                var manager = _clientEngine.Torrents.First(tm => tm.InfoHash.ToHex().Equals(SelectedTorrent.Hash));
                await manager.StartAsync();
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message, "Error!").ShowAsync();
            }
        }, () => SelectedTorrent != null);

        public ICommand BPauseCommand => new RelayCommand(async () =>
        {
            try
            {
                var manager = _clientEngine.Torrents.First(tm => tm.InfoHash.ToHex().Equals(SelectedTorrent.Hash));
                await manager.PauseAsync();
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message, "Error!").ShowAsync();
            }
        }, () => SelectedTorrent != null);

        public ICommand BStopCommand => new RelayCommand(async () =>
        {
            try
            {
                var manager = _clientEngine.Torrents.First(tm => tm.InfoHash.ToHex().Equals(SelectedTorrent.Hash));
                await manager.StopAsync();
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message, "Error!").ShowAsync();
            }
        }, () => SelectedTorrent != null);

        public ICommand FOpenFolderCommand => new RelayCommand(async () =>
        {
            try
            {
                var manager = _clientEngine.Torrents.First(tm => tm.InfoHash.ToHex().Equals(SelectedTorrent.Hash));

                throw new Exception("Не реализовано :(");
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message, "Error!").ShowAsync();
            }
        }, () => SelectedTorrent != null);

        #endregion

        #region Methods

        private void MainTimer(bool startTimer = true)
        {
            if (startTimer)
            {
                _timer.Interval = TimeSpan.FromSeconds(1);
                _timer.Tick += (_, _) =>
                {
                    if (_clientEngine == null || _clientEngine.Torrents.Count == 0)
                    {
                        SelectedTorrent = null;
                        MainPivotModel = null;
                        
                        return;
                    }

                    // Обновляем данные в ListView
                    _clientEngine.Torrents
                        .ToList()
                        .ForEach(manager =>
                        {
                            TorrentsCollection
                                .FirstOrDefault(model => model.Hash.Equals(manager.InfoHash.ToHex()))?
                                .UpdateProp(manager);

                        });

                    // Обновляем данные в Pivot
                    if (SelectedTorrent != null)
                    {
                        var manager = _clientEngine.Torrents.First(m => m.InfoHash.ToHex().Equals(SelectedTorrent.Hash));

                        if (MainPivotModel != null && MainPivotModel.Hash == SelectedTorrent.Hash)
                        {
                            MainPivotModel.UpdateProp(manager);
                        }
                        else
                        {
                            MainPivotModel = new PivotModel(manager);
                        }
                    }
                    else
                    {
                        MainPivotModel = null;
                    }
                };

                _timer.Start();
            }
            else
            {
                _timer?.Stop();
            }
        }

        private static async Task<ClientEngine> LoadState()
        {
            var engine = new ClientEngine();

            //var nodes = Array.Empty<byte>();
            //if (File.Exists(DhtNodesPath))
            //{
            //    nodes = await File.ReadAllBytesAsync(DhtNodesPath);
            //}

            //await engine.DhtEngine.StartAsync(nodes);

            if (File.Exists(FastResumePath))
            {
                var fastResume = BEncodedValue.Decode<BEncodedDictionary>(await File.ReadAllBytesAsync(FastResumePath));

                foreach (var file in Directory.GetFiles(TorrentsFolderPath))
                {
                    if (file.EndsWith(".torrent", StringComparison.OrdinalIgnoreCase))
                    {
                        Torrent torrent;

                        try
                        {
                            torrent = await Torrent.LoadAsync(file);
                        }
                        catch (Exception ex)
                        {
                            Debug.Fail($"\n{ex.Message}\n{ex.StackTrace}\n");
                            continue;
                        }

                        if (fastResume.ContainsKey(torrent.InfoHash.ToHex()))
                        {
                            var manager = new TorrentManager(torrent, $"{ApplicationData.Current.LocalFolder.Path}\\Downloads");

                            manager.LoadFastResume(new FastResume((BEncodedDictionary)fastResume[torrent.InfoHash.ToHex()]));
                            await engine.Register(manager);
                        }
                    }
                }
            }

            return engine;
        }

        public async Task SaveState()
        {
            try
            {
                var fastResume = new BEncodedDictionary();
                var torrents = _clientEngine.Torrents;

                await _clientEngine.StopAllAsync();

                torrents.ToList().ForEach(manager =>
                {
                    if (manager.HashChecked)
                    {
                        fastResume.Add(manager.Torrent.InfoHash.ToHex(), manager.SaveFastResume().Encode());
                    }
                });

                await File.WriteAllBytesAsync(DhtNodesPath, await _clientEngine.DhtEngine.SaveNodesAsync());
                await File.WriteAllBytesAsync(FastResumePath, fastResume.Encode());
            }
            catch (Exception ex)
            {
                Debug.Fail($"\n{ex.Message}\n{ex.StackTrace}\n");
            }
            finally
            {
                _clientEngine.Dispose();
                Thread.Sleep(2000);
            }
        }

        #endregion
    }
}
