using MonoTorrent.Client;
using System;
using System.Threading.Tasks;
using TiTorrent.UWP.Helpers.Extensions;
using TiTorrent.UWP.Views.Dialogs;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml.Controls;

namespace TiTorrent.UWP.Services.Dialogs
{
    internal class DeleteDisplayService
    {
        internal async Task<bool?> ShowAsync(TorrentManager manager)
        {
            bool? isDeleteFiles = null;
            
            await CoreApplication.GetCurrentView().Dispatcher.RunTaskAsync(
                async () =>
                {
                    var deleteDialog = new DeleteDialog(manager);

                    if (await deleteDialog.ShowAsync() == ContentDialogResult.Primary)
                        isDeleteFiles = deleteDialog.IsDeleteFiles;
                });

            return isDeleteFiles;
        }
    }
}
