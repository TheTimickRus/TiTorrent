using TiTorrent.UWP.Services;

namespace TiTorrent.UWP.Views.Dialogs
{
    public sealed partial class AddDialog
    {
        public AddDialog()
        {
            RequestedTheme = ThemeSelectorService.Theme;
            InitializeComponent();
        }
    }
}
