using Windows.ApplicationModel.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using TiTorrent.UWP.Views;

namespace TiTorrent.UWP
{
    public sealed partial class MainPage 
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void NavigationView_OnLoaded(object sender, RoutedEventArgs e)
        {
            var navView = (NavigationView)sender;

            foreach (NavigationViewItemBase item in navView.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "Main")
                {
                    navView.SelectedItem = item;
                    break;
                }
            }
        }

        private void NavigationView_OnSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem is NavigationViewItem selectedItem && sender.Content is Frame content)
            {
                if (args.IsSettingsSelected)
                {
                    sender.Header = "Параметры";
                    content.Navigate(typeof(SettingsNavView));
                    return;
                }

                switch (selectedItem.Tag.ToString())
                {
                    case "Main":
                        sender.Header = "Главное";
                        content.Navigate(typeof(MainNavView));
                        break;
                }
            }
        }


    }
}
