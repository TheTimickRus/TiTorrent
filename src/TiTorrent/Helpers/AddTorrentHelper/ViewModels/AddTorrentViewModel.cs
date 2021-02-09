using System;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI.Popups;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MonoTorrent;
using MonoTorrent.Client;
using TiTorrent.Helpers.AddTorrentHelper.Models;

namespace TiTorrent.Helpers.AddTorrentHelper.ViewModels
{
    public class AddTorrentViewModel : ViewModelBase
    {
        #region Public Props

        public AddTorrentModel InAddTorrentModel { get; set; } = new();

        #endregion

        #region Constructors

        public AddTorrentViewModel()
        {
            InAddTorrentModel.SavePath = $"{ApplicationData.Current.LocalFolder.Path}\\Downloads";
        }

        #endregion

        #region Commands

        public ICommand BOpenTorrentCommand => new RelayCommand(async () =>
        {
            try
            {
                var file = await Utils.OpenFile();
                if (file == null) 
                    return;

                var torrent = await Torrent.LoadAsync(file);

                InAddTorrentModel.Update(torrent);
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
                    return;

                InAddTorrentModel.SavePath = folder;
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message, "Error!").ShowAsync();
            }
        });

        public ICommand BPrimaryCommand => new RelayCommand(async () =>
        {
            try
            {
                if (InAddTorrentModel.InTorrent == null || InAddTorrentModel.SavePath.Equals(string.Empty))
                {
                    throw new Exception("Поля заполнены неверно!");
                }

                var torrentManager = new TorrentManager(InAddTorrentModel.InTorrent, InAddTorrentModel.SavePath);
                Messenger.Default.Send(torrentManager);
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message, "Error!").ShowAsync();
            }
        });

        public ICommand BSecondaryCommand => new RelayCommand(() =>
        {
            Messenger.Default.Send<TorrentManager>(null);
        });

        #endregion
    }
}
