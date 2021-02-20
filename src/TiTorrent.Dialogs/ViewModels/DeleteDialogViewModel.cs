using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MonoTorrent.Client;
using System.Windows.Input;

namespace TiTorrent.Dialogs.ViewModels
{
    public class DeleteDialogViewModel : ViewModelBase
    {
        public TorrentManager Manager { get; set; }

        public bool IsDeleteFiles { get; set; } = true;

        public DeleteDialogViewModel()
        {
            MessengerInstance.Register<TorrentManager>(this, manager => { Manager = manager; });
        }

        public ICommand BPrimaryCommand => new RelayCommand(() =>
        {
            MessengerInstance.Send(IsDeleteFiles);
        });
    }
}
