using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MonoTorrent;
using MonoTorrent.Client;
using System;
using System.IO;
using System.Windows.Input;
using TiTorrent.Dialogs.Models;
using TiTorrent.Shared;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.UI.Popups;

namespace TiTorrent.Dialogs.ViewModels
{
    public class AddDialogViewModel : ObservableObject
    {
        public AddDialogModel Model { get; set; } = new AddDialogModel();

        public AddDialogViewModel()
        {
            Model.SavePath = $"{ApplicationData.Current.LocalFolder.Path}\\Downloads";
        }

        public ICommand BOpenTorrentCommand => new RelayCommand(async () =>
        {
            try
            {
                var file = await Utils.OpenFile();
                if (file == null)
                {
                    return;
                }
                
                var torrent = await Torrent.LoadAsync(file);
                Model.Update(torrent);
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message, "Error!").ShowAsync();
            }
        });

        public ICommand BOpenSaveFolderCommand => new RelayCommand(async () =>
        {
            try
            {
                var folder = await Utils.OpenFolder();
                if (folder == null)
                {
                    return;
                }

                Model.SavePath = folder;
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message, "Error!").ShowAsync();
            }
        });

        public ICommand BOkCommand => new RelayCommand(async () =>
        {
            try
            {
                if (Model.Torrent != null)
                {
                    AddDialogModel.Manager = new TorrentManager(Model.Torrent, Model.SavePath);
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message, "Error!").ShowAsync();
            }
        });
    }
}
