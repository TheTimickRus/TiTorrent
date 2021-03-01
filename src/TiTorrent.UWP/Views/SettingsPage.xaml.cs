using TiTorrent.UWP.ViewModels;
using Windows.UI.Xaml.Navigation;

namespace TiTorrent.UWP.Views
{
    public sealed partial class SettingsPage
    {
        private SettingsViewModel ViewModel => ViewModelLocator.Current.SettingsViewModel;

        public SettingsPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.InitializeAsync();
        }
    }
}
