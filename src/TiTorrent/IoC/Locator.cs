using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using MonoTorrent.Client;
using TiTorrent.Helpers.AddTorrentHelper.ViewModels;

namespace TiTorrent
{
    public class Locator
    {
        public Locator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<AddTorrentViewModel>();
        }

        public MainViewModel MainVm => SimpleIoc.Default.GetInstance<MainViewModel>();
        public AddTorrentViewModel AddTorrentVm => SimpleIoc.Default.GetInstance<AddTorrentViewModel>();


    }
}