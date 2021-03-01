using System;
using System.Threading.Tasks;
using TiTorrent.UWP.Services;
using TiTorrent.UWP.ViewModels;
using Windows.ApplicationModel.Activation;

namespace TiTorrent.UWP.Activation
{
    internal class DefaultActivationHandler : ActivationHandler<IActivatedEventArgs>
    {
        private readonly string _navElement;

        public NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        public DefaultActivationHandler(Type navElement)
        {
            _navElement = navElement.FullName;
        }

        protected override async Task HandleInternalAsync(IActivatedEventArgs args)
        {
            object arguments = null;
            if (args is LaunchActivatedEventArgs launchArgs)
            {
                arguments = launchArgs.Arguments;
            }

            NavigationService.Navigate(_navElement, arguments);

            //Singleton<ToastNotificationsService>.Instance.ShowToastNotificationSample();

            await Task.CompletedTask;
        }

        protected override bool CanHandleInternal(IActivatedEventArgs args)
        {
            return NavigationService.Frame.Content == null && _navElement != null;
        }
    }
}
