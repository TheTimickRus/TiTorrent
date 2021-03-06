using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using TiTorrent.UWP.Models;

namespace TiTorrent.UWP.ViewModels
{
    public class DownloadViewModel : ViewModelBase
    {
        public ObservableCollection<ListViewItemModel> TorrentsCollection { get; set; } = new();
    }
}
