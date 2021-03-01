using TiTorrent.UWP.ViewModels;

namespace TiTorrent.UWP.Views
{
    public sealed partial class ShellPage
    {
        private ShellViewModel ViewModel => ViewModelLocator.Current.ShellViewModel;

        public ShellPage()
        {
            InitializeComponent();

            DataContext = ViewModel;
            ViewModel.Initialize(ShellFrame, NavigationView, KeyboardAccelerators);
        }
    }
}
