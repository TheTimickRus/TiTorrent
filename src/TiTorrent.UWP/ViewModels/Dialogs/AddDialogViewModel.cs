using System;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI.Popups;
using ByteSizeLib;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MonoTorrent;
using MonoTorrent.Client;
using TiTorrent.UWP.Helpers;

namespace TiTorrent.UWP.ViewModels.Dialogs
{
    public class AddDialogViewModel : ViewModelBase
    {
        #region PublicProps

        public string SavePath { get; set; }
        public string Name { get; set; }
        public ByteSize? Size { get; set; }
        public DateTime? Date { get; set; }
        public string Hash { get; set; }
        public string Comment { get; set; }

        #endregion

        #region PrivateProps

        private TorrentManager _manager;

        #endregion

        #region Constructors

        public AddDialogViewModel()
        {
            SavePath = $"{ApplicationData.Current.LocalFolder.Path}\\Downloads";
        }

        #endregion

        #region Commands

        public ICommand BOpenTorrentCommand => new RelayCommand(async () =>
        {
            try
            {
                var file = await Utils.OpenFile();
                if (file is null)
                {
                    return;
                }
                
                _manager = new TorrentManager(await Torrent.LoadAsync(file), SavePath);

                Update();
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
                if (folder is null)
                {
                    return;
                }

                SavePath = folder;
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message, "Error!").ShowAsync();
            }
        });

        public ICommand BPrimaryCommand => new RelayCommand(() =>
        {
            MessengerInstance.Send(_manager);
        });

        #endregion

        #region Methods

        public void Update()
        {
            Name = _manager.Torrent.Name;
            Size = ByteSize.FromBytes(_manager.Torrent.Size);
            Date = _manager.Torrent.CreationDate;
            Hash = _manager.InfoHash.ToHex();
            Comment = _manager.Torrent.Comment;
        }

        #endregion
    }
}
