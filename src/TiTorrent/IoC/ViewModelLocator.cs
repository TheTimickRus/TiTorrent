using MvvmCross.IoC;

namespace TiTorrent
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            MvxIoCProvider.Initialize();
            MvxIoCProvider.Instance.RegisterSingleton(new MainViewModel());
        }

        public MainViewModel MainVm => MvxIoCProvider.Instance.GetSingleton<MainViewModel>();
    }
}
