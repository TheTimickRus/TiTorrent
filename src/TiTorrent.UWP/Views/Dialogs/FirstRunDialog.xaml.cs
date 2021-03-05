using TiTorrent.UWP.Services;

namespace TiTorrent.UWP.Views.Dialogs
{
    public sealed partial class FirstRunDialog
    {
        public FirstRunDialog()
        {
            RequestedTheme = ThemeSelectorService.Theme;
            InitializeComponent();
        }
    }
}
