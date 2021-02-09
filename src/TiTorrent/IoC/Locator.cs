using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using TiTorrent.Helpers.AddTorrentHelper.ViewModels;
using TiTorrent.ViewModels;

namespace TiTorrent.IoC
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
    }
}