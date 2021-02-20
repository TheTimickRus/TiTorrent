using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace TiTorrent.Core.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class ViewModelLocator
    {
        private static ViewModelLocator _instance;
        public static ViewModelLocator Instance => _instance ??= new ViewModelLocator();

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel MainVm => SimpleIoc.Default.GetInstance<MainViewModel>();
    }
}
