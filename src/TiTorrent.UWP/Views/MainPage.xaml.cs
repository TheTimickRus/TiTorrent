using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
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


        private void GridSplitter_OnDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            MainGrid.RowDefinitions[3].Height = new GridLength(345);
        }
    }
}
