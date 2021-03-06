using TiTorrent.UWP.ViewModels;

namespace TiTorrent.UWP.Views
{
    public sealed partial class DownloadPage
    {
        // ReSharper disable once MemberCanBeMadeStatic.Local
        private DownloadViewModel ViewModel => ViewModelLocator.Current.DownloadViewModel;

        public DownloadPage()
        {
            InitializeComponent();
        }
    }
}
