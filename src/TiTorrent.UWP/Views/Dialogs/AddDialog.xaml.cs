using GalaSoft.MvvmLight.Command;
using MonoTorrent;
using MonoTorrent.Client;
using System;
using System.IO;
using System.Windows.Input;
using TiTorrent.UWP.Helpers;
using TiTorrent.UWP.Services;
using Windows.Storage;
using Windows.UI.Popups;

namespace TiTorrent.UWP.Views.Dialogs
{
    public sealed partial class AddDialog
    {
        #region PublicProps

        public TorrentManager Manager { get; set; }

        public string SavePath { get; set; } = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Downloads");

        #endregion

        public AddDialog()
        {
            RequestedTheme = ThemeSelectorService.Theme;

            InitializeComponent();
        }

        #region Commands

        public ICommand BOpenTorrentCommand => new RelayCommand(async () =>
        {
            try
            {
                var file = await Utils.OpenFile();
                if (file is null)
                    return;

                Manager = new TorrentManager(await Torrent.LoadAsync(file), SavePath);

                Bindings.Update();
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message, "Error!").ShowAsync();
                Log.Instance.Error(ex, "Ошибка, при добавлении торрента!");
            }
        });

        public ICommand BOpenSaveFolderCommand => new RelayCommand(async () =>
        {
            try
            {
                var folder = await Utils.OpenFolder();
                if (folder is null)
                    return;

                SavePath = folder;

                Bindings.Update();
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message, "Error!").ShowAsync();
                Log.Instance.Error(ex, "Ошибка, при выборе пути сохранения!");
            }
        });

        #endregion
    }
}
