using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Microsoft.Toolkit.Uwp.Helpers;
using TiTorrent.UWP.Views.Dialogs;

namespace TiTorrent.UWP.Services.Dialogs
{
    public static class FirstRunDisplayService
    {
        private static bool _shown;

        internal static async Task ShowIfAppropriateAsync()
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                if (SystemInformation.Instance.IsFirstRun && !_shown)
                {
                    _shown = true;
                    var dialog = new FirstRunDialog();
                    await dialog.ShowAsync();
                }
            });
        }
    }
}
