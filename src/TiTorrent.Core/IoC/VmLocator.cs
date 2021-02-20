using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using TiTorrent.Core.ViewModels;

namespace TiTorrent.Core.IoC
{
    public class VmLocator
    {
        public VmLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel MainVm => SimpleIoc.Default.GetInstance<MainViewModel>();
    }
}
