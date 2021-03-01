using Microsoft.Toolkit.Uwp.Notifications;
using Windows.UI.Notifications;

namespace TiTorrent.UWP.Services
{
    internal partial class ToastNotificationsService
    {
        public void ShowToastNotificationSample()
        {
            var content = new ToastContent
            {
                Launch = "ToastContentActivationParams",

                Visual = new ToastVisual
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText
                            {
                                Text = "Sample Toast Notification"
                            },

                            new AdaptiveText
                            {
                                 Text = @"Click OK to see how activation from a toast notification can be handled in the ToastNotificationService."
                            }
                        }
                    }
                },

                Actions = new ToastActionsCustom
                {
                    Buttons =
                    {
                        new ToastButton("OK", "ToastButtonActivationArguments")
                        {
                            ActivationType = ToastActivationType.Foreground
                        },

                        new ToastButtonDismiss("Cancel")
                    }
                }
            };

            var toast = new ToastNotification(content.GetXml())
            {
                Tag = "ToastTag"
            };

            ShowToastNotification(toast);
        }
    }
}
