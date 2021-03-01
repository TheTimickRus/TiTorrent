using System;
using TiTorrent.UWP.Helpers;
using TiTorrent.UWP.Services;
using TiTorrent.UWP.ViewModels;
using TiTorrent.UWP.Views;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;

namespace TiTorrent.UWP
{
    public sealed partial class App
    {
        private readonly Lazy<ActivationService> _activationService;
        private ActivationService ActivationService => _activationService.Value;

        public App()
        {
            InitializeComponent();

            EnteredBackground += App_EnteredBackground;
            Resuming += App_Resuming;
            Suspending += App_OnSuspending;

            UnhandledException += OnAppUnhandledException;
            
            _activationService = new Lazy<ActivationService>(CreateService);
        }

        private ActivationService CreateService()
        {
            return new(this, typeof(MainViewModel), new Lazy<UIElement>(CreateShell));
        }

        private static UIElement CreateShell()
        {
            return new ShellPage();
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (args.PrelaunchActivated is false)
            {
                await ActivationService.ActivateAsync(args);
            }

            // Контент поверх TitleBar
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;

            // Инициализация
            AppState.Init();
            Log.Instance.Information("Программа запущена!");
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }

        private async void App_EnteredBackground(object sender, EnteredBackgroundEventArgs e)
        {
            var deferral = e.GetDeferral();

            await Singleton<SuspendAndResumeService>.Instance.SaveStateAsync();

            deferral.Complete();
        }

        private void App_Resuming(object sender, object e)
        {
            Singleton<SuspendAndResumeService>.Instance.ResumeApp();
        }

        private static async void App_OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            try
            {
                await ViewModelLocator.Current.MainViewModel.SaveState();
            }
            catch (Exception ex)
            {
                Log.Instance.Error(ex, "Ошибка при сохранении!");
            }

            Log.Instance.Information("Программа завершена!");

            deferral.Complete();
        }

        private static void OnAppUnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            Log.Instance.Fatal(e.Exception, e.Message);
        }
    }
}
