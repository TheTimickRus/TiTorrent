using TiTorrent.UWP.ViewModels;

namespace TiTorrent.UWP.Views
{
    public sealed partial class UploadPage
    {
        private UploadViewModel ViewModel => ViewModelLocator.Current.UploadViewModel;

        public UploadPage()
        {
            InitializeComponent();
        }
    }
}
