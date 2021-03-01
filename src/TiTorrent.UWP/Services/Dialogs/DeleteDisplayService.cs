using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Messaging;
using MonoTorrent.Client;
using TiTorrent.UWP.Views.Dialogs;

namespace TiTorrent.UWP.Services.Dialogs
{
    internal class DeleteDisplayService
    {
        internal async Task<bool?> ShowAsync(TorrentManager manager)
        {
            // Удаление загруженных файлов
            var isFilesDelete = false;

            // Создаем форму вопроса, передаем ей менеджер и ждем ответа
            var deleteDialog = new DeleteDialog();
            Messenger.Default.Send(manager);
            Messenger.Default.Register<bool>(this, value => isFilesDelete = value);
            if (await deleteDialog.ShowAsync() != ContentDialogResult.Primary)
            {
                return null;
            }
            Messenger.Default.Unregister<bool>(this);

            return isFilesDelete;
        }
    }
}
