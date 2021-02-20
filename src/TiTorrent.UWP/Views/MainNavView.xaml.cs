using TiTorrent.Core.ViewModels;

namespace TiTorrent.UWP.Views
{
    public sealed partial class MainNavView
    {
        private readonly MainViewModel _viewModel = ViewModelLocator.Instance.MainVm;

        public MainNavView()
        {
            InitializeComponent();
        }
    }
}
