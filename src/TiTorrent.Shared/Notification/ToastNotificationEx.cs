using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications;

namespace TiTorrent.Shared.Notification
{
    public static class ToastNotificationEx
    {
        public static void ShowInfo(string title, string msg)
        {
            var builder = new ToastContentBuilder().SetToastScenario(ToastScenario.Reminder)
                .AddText(title)
                .AddText(msg);

            ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(builder.GetXml()));
        }
    }
}
