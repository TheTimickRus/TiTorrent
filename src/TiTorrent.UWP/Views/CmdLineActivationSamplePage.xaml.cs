using Windows.UI.Xaml.Navigation;

namespace TiTorrent.UWP.Views
{
    public sealed partial class CmdLineActivationSamplePage
    {
        public CmdLineActivationSamplePage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            PassedArguments.Text = e.Parameter?.ToString() ?? string.Empty;
        }
    }
}
