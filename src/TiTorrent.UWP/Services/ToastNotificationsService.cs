using System;
using System.Threading.Tasks;
using TiTorrent.UWP.Activation;
using Windows.ApplicationModel.Activation;
using Windows.UI.Notifications;

namespace TiTorrent.UWP.Services
{
    internal partial class ToastNotificationsService : ActivationHandler<ToastNotificationActivatedEventArgs>
    {
        public void ShowToastNotification(ToastNotification toastNotification)
        {
            try
            {
                ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
            }
            catch (Exception)
            {
                /*  */
            }
        }

        protected override async Task HandleInternalAsync(ToastNotificationActivatedEventArgs args)
        {
            await Task.CompletedTask;
        }
    }
}
