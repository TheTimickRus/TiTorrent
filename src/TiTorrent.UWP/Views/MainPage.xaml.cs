using Windows.UI.Xaml.Navigation;
using TiTorrent.UWP.ViewModels;

namespace TiTorrent.UWP.Views
{
    public sealed partial class MainPage
    {
        // ReSharper disable once MemberCanBeMadeStatic.Local
        private MainViewModel ViewModel => ViewModelLocator.Current.MainViewModel;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.InitializeAsync();
        }
    }
}
