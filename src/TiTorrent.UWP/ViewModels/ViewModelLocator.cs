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
            SimpleIoc.Default.Register(() => new NavigationService());
            SimpleIoc.Default.Register<ShellViewModel>();

            Register<MainViewModel, MainPage>();
            Register<DownloadViewModel, DownloadPage>();
            Register<UploadViewModel, UploadPage>();
            Register<SettingsViewModel, SettingsPage>();
        }

        public NavigationService NavigationService => SimpleIoc.Default.GetInstance<NavigationService>();
        public ShellViewModel ShellViewModel => SimpleIoc.Default.GetInstance<ShellViewModel>();

        public MainViewModel MainViewModel => SimpleIoc.Default.GetInstance<MainViewModel>();
        public DownloadViewModel DownloadViewModel => SimpleIoc.Default.GetInstance<DownloadViewModel>();
        public UploadViewModel UploadViewModel => SimpleIoc.Default.GetInstance<UploadViewModel>();
        public SettingsViewModel SettingsViewModel => SimpleIoc.Default.GetInstance<SettingsViewModel>();

        public void Register<TVm, TV>() where TVm : class
        {
            SimpleIoc.Default.Register<TVm>();
            NavigationService.Configure(typeof(TVm).FullName, typeof(TV));
        }
    }
}
