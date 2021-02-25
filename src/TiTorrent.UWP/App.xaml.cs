using GalaSoft.MvvmLight.Ioc;
using System;
using TiTorrent.Core.ViewModels;
using TiTorrent.Shared;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace TiTorrent.UWP
{
    sealed partial class App
    {
        public App()
        {
            AppState.Init();

            InitializeComponent();
            Suspending += OnSuspending;

            Log.Instance.Information("Программа запущена!");
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            if (!(Window.Current.Content is Frame rootFrame))
            {
                rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;

                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }

                Window.Current.Activate();
            }

            // Содержимое поверх TitleBar
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
        }

        private static void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            Log.Instance.Error(new Exception("Failed to load Page " + e.SourcePageType.FullName), "");
        }

        private static async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            // Сохраняемся
            try
            {
                await SimpleIoc.Default.GetInstance<MainViewModel>().SaveState();
                Log.Instance.Information("Данные сохранены!");
            }
            catch (Exception ex)
            {
                Log.Instance.Error(ex, "Ошибка при сохранении данных!", SimpleIoc.Default.GetInstance<MainViewModel>());
            }

            // Убиваем логгер
            Log.Instance.Dispose();

            deferral.Complete();
        }
    }
}
