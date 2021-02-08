using MonoTorrent;
using MonoTorrent.Client;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.Core;


namespace TiTorrent
{
    public class MainViewModel : MvxViewModel
    {
        public ObservableCollection<TorrentModel> TorrentModels { get; set; } = new ObservableCollection<TorrentModel>();
        public TorrentModel SelectedItem { get; set; }

        private readonly ClientEngine _clientEngine = new ClientEngine();
        private readonly Timer _progressTimer = new Timer(1000);


        public MainViewModel()
        {
            // Запускаем таймер для обновления информации
            _progressTimer.Elapsed += ProgressTimerOnElapsed;
            _progressTimer.Start();
        }


        public ICommand BAddCommand => new MvxAsyncCommand(async () =>
        {
            var file = await Utils.OpenFile();
            if (file == null)
            {
                return;
            }

            var torrent = await Torrent.LoadAsync(file);
            var tm = new TorrentManager(torrent, $"{ApplicationData.Current.LocalFolder.Path}\\{torrent.Name}");
            if (_clientEngine.Torrents.ToList().Find(manager => manager.Equals(tm)) != null)
            {
                return;
            }

            // Добавление торента в список
            TorrentModels.Add(new TorrentModel(tm, TorrentModels.Count + 1));

            // Регистрация нового торента
            await _clientEngine.Register(tm);
        });

        public ICommand BStartCommand => new MvxAsyncCommand(async () =>
        {
            await _clientEngine.StartAllAsync();
        });

        public ICommand BStopCommand => new MvxAsyncCommand(async () =>
        {
            await _clientEngine.StopAllAsync();
        });


        private async void ProgressTimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            foreach (var torrentModel in _clientEngine.Torrents)
            {
                await CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    TorrentModels.First(model => model.Name.Equals(torrentModel.Torrent.Name)).Update(torrentModel);
                });
            }
        }
    }
}
