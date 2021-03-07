using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Threading.Tasks;
using System.Windows.Input;
using TiTorrent.UWP.Helpers.Extensions;
using TiTorrent.UWP.Services;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Microsoft.Toolkit.Uwp;

namespace TiTorrent.UWP.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private ElementTheme _elementTheme = ThemeSelectorService.Theme;
        public ElementTheme ElementTheme
        {
            get => _elementTheme;
            set => Set(ref _elementTheme, value);
        }

        private string _versionDescription;
        public string VersionDescription
        {
            get => _versionDescription;
            set => Set(ref _versionDescription, value);
        }

        private ICommand _switchThemeCommand;
        public ICommand SwitchThemeCommand
        {
            get
            {
                return _switchThemeCommand ??= new RelayCommand<ElementTheme>(async param =>
                {
                    ElementTheme = param;
                    await ThemeSelectorService.SetThemeAsync(param);
                });
            }
        }

        public async Task InitializeAsync()
        {
            VersionDescription = GetVersionDescription();
            await Task.CompletedTask;
        }

        private static string GetVersionDescription()
        {
            var appName = "AppDisplayName".GetLocalized();
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;

            return $"{appName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }
    }
}
