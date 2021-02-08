using MvvmCross.IoC;

namespace TiTorrent
{
    public class Locator
    {
        public Locator()
        {
            MvxIoCProvider.Initialize();
            MvxIoCProvider.Instance.RegisterSingleton(new MainViewModel());
        }

        public MainViewModel MainVm => MvxIoCProvider.Instance.GetSingleton<MainViewModel>();
    }
}