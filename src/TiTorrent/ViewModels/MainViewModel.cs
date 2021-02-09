using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MonoTorrent.Client;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using System.Windows.Input;
using TiTorrent.Helpers.AddTorrentHelper;
using TiTorrent.Models;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace TiTorrent.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ClientEngine _clientEngine = new();

        public ObservableCollection<TorrentModel> TorrentModels { get; set; } = new();
        public TorrentModel SelectedItem { get; set; } = null;

        public MainViewModel()
        {
            // Обновляем данные по таймеру
            var updater = new Timer(1000);
            updater.Elapsed += ProgressTimerOnElapsed;
            updater.Start();
        }

        private void ProgressTimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            _clientEngine.Torrents
                .ToList()
                .ForEach(async manager =>
                {
                    await CoreApplication.MainView.Dispatcher
                        .RunAsync(CoreDispatcherPriority.Normal, 
                            () => TorrentModels.First(model => model.Manager.Equals(manager)).Update(manager));
                });
        }

        public ICommand BAddCommand => new RelayCommand(async () =>
        {
            TorrentManager torrentManager = null;
            Messenger.Default.Register(this, new Action<TorrentManager>(manager => torrentManager = manager));

            await new AddTorrent().ShowAsync();

            // Если не выбрали торрент - выходим
            if (torrentManager == null)
            {
                return;
            }

            // Проверка на то, если ли данный торрент в списке загрузок
            if (_clientEngine.Torrents.ToList().Find(manager => manager.Equals(torrentManager)) != null)
            {
                return;
            }

            // Добавление торрента в список
            TorrentModels.Add(new TorrentModel(torrentManager, TorrentModels.Count + 1));

            // Регистрация нового торрента
            await _clientEngine.Register(torrentManager);
        });

        public ICommand BStartCommand => new RelayCommand(async () => await _clientEngine.StartAllAsync());

        public ICommand BStopCommand => new RelayCommand(async () => await _clientEngine.StopAllAsync());
    }
}