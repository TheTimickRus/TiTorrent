using TiTorrent.UWP.Services;

namespace TiTorrent.UWP.Views.Dialogs
{
    public sealed partial class DeleteDialog
    {
        public DeleteDialog()
        {
            RequestedTheme = ThemeSelectorService.Theme;

            InitializeComponent();
        }
    }
}
