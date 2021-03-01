using GalaSoft.MvvmLight.Ioc;
using TiTorrent.UWP.Services;
using TiTorrent.UWP.Views;

namespace TiTorrent.UWP.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class ViewModelLocator
    {
        private static ViewModelLocator _current;
        public static ViewModelLocator Current => _current ??= new ViewModelLocator();

        private ViewModelLocator()
        {
            SimpleIoc.Default.Register(() => new NavigationServiceEx());
            SimpleIoc.Default.Register<ShellViewModel>();

            Register<MainViewModel, MainPage>();
            Register<SettingsViewModel, SettingsPage>();
        }

        public NavigationServiceEx NavigationService => SimpleIoc.Default.GetInstance<NavigationServiceEx>();
        public ShellViewModel ShellViewModel => SimpleIoc.Default.GetInstance<ShellViewModel>();

        public MainViewModel MainViewModel => SimpleIoc.Default.GetInstance<MainViewModel>();
        public SettingsViewModel SettingsViewModel => SimpleIoc.Default.GetInstance<SettingsViewModel>();

        public void Register<TVm, TV>() where TVm : class
        {
            SimpleIoc.Default.Register<TVm>();
            NavigationService.Configure(typeof(TVm).FullName, typeof(TV));
        }
    }
}
