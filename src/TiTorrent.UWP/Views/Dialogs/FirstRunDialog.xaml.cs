using Windows.UI.Xaml;

namespace TiTorrent.UWP.Views.Dialogs
{
    public sealed partial class FirstRunDialog
    {
        public FirstRunDialog()
        {
            RequestedTheme = ((FrameworkElement) Window.Current.Content).RequestedTheme;
            InitializeComponent();
        }
    }
}
