using System;
using System.Threading.Tasks;
using TiTorrent.UWP.Activation;
using TiTorrent.UWP.Services.Dialogs;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TiTorrent.UWP.Services
{
    internal class ActivationService
    {
        private readonly App _app;
        private readonly Type _defaultNavItem;
        private readonly Lazy<UIElement> _shell;

        private object _lastActivationArgs;

        public ActivationService(App app, Type defaultNavItem, Lazy<UIElement> shell = null)
        {
            _app = app;
            _shell = shell;
            _defaultNavItem = defaultNavItem;
        }

        public async Task ActivateAsync(object activationArgs)
        {
            if (IsInteractive(activationArgs))
            {
                await InitializeAsync();
                Window.Current.Content ??= _shell?.Value ?? new Frame();
            }

            await HandleActivationAsync(activationArgs);
            _lastActivationArgs = activationArgs;

            if (IsInteractive(activationArgs))
            {
                var activation = activationArgs as IActivatedEventArgs;

                Window.Current.Activate();

                await StartupAsync();
            }
        }

        private static async Task InitializeAsync()
        {
            await ThemeSelectorService.InitializeAsync().ConfigureAwait(false);
        }

        private async Task HandleActivationAsync(object activationArgs)
        {
            //var activationHandler = GetActivationHandlers().FirstOrDefault(h => h.CanHandle(activationArgs));
            //if (activationHandler != null)
            //{
            //    await activationHandler.HandleAsync(activationArgs);
            //}

            if (IsInteractive(activationArgs))
            {
                var defaultHandler = new DefaultActivationHandler(_defaultNavItem);
                if (defaultHandler.CanHandle(activationArgs)) await defaultHandler.HandleAsync(activationArgs);
            }
        }

        private static async Task StartupAsync()
        {
            await ThemeSelectorService.SetRequestedThemeAsync();
            await FirstRunDisplayService.ShowIfAppropriateAsync();
        }

        //private IEnumerable<ActivationHandler> GetActivationHandlers()
        //{
        //    yield return Singleton<CommandLineActivationHandler>.Instance;
        //}

        public bool IsInteractive(object args)
        {
            return args is IActivatedEventArgs;
        }
    }
}
