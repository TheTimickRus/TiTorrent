using MonoTorrent.Client;
using TiTorrent.UWP.Services;

namespace TiTorrent.UWP.Views.Dialogs
{
    public sealed partial class DeleteDialog
    {
        public bool IsDeleteFiles { get; private set; } = true;
        public TorrentManager Manager { get; }
        
        public DeleteDialog(TorrentManager manager)
        {
            RequestedTheme = ThemeSelectorService.Theme;
            Manager = manager;

            InitializeComponent();
        }
    }
}
