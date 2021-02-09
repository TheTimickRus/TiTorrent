using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TiTorrent.Views
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void NavView_OnLoaded(object sender, RoutedEventArgs e)
        {
            foreach (NavigationViewItemBase item in NavView.MenuItems)
                if (item is NavigationViewItem && item.Tag.ToString() == "MainNavPage")
                {
                    NavView.SelectedItem = item;
                    break;
                }
        }

        private void NavView_OnSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                sender.Header = "Параметры";
                ContentFrame.Navigate(typeof(SettingsNavPage));
            }
            else
            {
                var selectedItem = (NavigationViewItem) args.SelectedItem;
                var selectedItemTag = (string) selectedItem.Tag;

                switch (selectedItemTag)
                {
                    case "MainNavPage":
                        sender.Header = "Главное";
                        ContentFrame.Navigate(typeof(MainNavPage));
                        break;
                }
            }
        }
    }
}