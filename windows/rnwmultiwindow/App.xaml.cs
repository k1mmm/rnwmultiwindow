using Microsoft.ReactNative;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml.Hosting;
using System;

namespace rnwmultiwindow
{
    sealed partial class App : ReactApplication
    {
        public IReactContext ReactContext { get; set; }

        private AppWindow secondaryAppWindow;
        private SecondaryWindow secondaryWindowPage;

        public App()
        {
#if BUNDLE
            JavaScriptBundleFile = "index.windows";
            InstanceSettings.UseWebDebugger = false;
            InstanceSettings.UseFastRefresh = false;
#else
            JavaScriptBundleFile = "index";
            InstanceSettings.UseWebDebugger = true;
            InstanceSettings.UseFastRefresh = true;
#endif

#if DEBUG
            InstanceSettings.UseDeveloperSupport = true;
#else
            InstanceSettings.UseDeveloperSupport = false;
#endif

            Microsoft.ReactNative.Managed.AutolinkedNativeModules.RegisterAutolinkedNativeModulePackages(PackageProviders); // Includes any autolinked modules

            PackageProviders.Add(new ReactPackageProvider());

            InitializeComponent();

            Host.InstanceSettings.InstanceCreated += OnReactInstanceCreated;
        }

        private void OnReactInstanceCreated(object sender, InstanceCreatedEventArgs args)
        {
            // This event is triggered when a React Native instance is created.
            // At this point, the ReactContext becomes valid.
            // However, it's better to emit events only after the JS code that can handle them is loaded.

            // If you need to perform any initialization based on ReactContext creation, do it here.
            // capsterNativeEmitter._reactContext = args.Context;
            this.ReactContext = args.Context;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            base.OnLaunched(e);
            var frame = (Frame)Window.Current.Content;
            frame.Navigate(typeof(MainPage), e.Arguments);
        }

        /// <summary>
        /// Invoked when the application is activated by some means other than normal launching.
        /// </summary>
        protected override void OnActivated(Windows.ApplicationModel.Activation.IActivatedEventArgs e)
        {
            var preActivationContent = Window.Current.Content;
            base.OnActivated(e);
            if (preActivationContent == null && Window.Current != null)
            {
                // Display the initial content
                var frame = (Frame)Window.Current.Content;
                frame.Navigate(typeof(MainPage), null);
            }
        }

        public async void OpenSecondaryWindow()
        {
            if (secondaryAppWindow != null && secondaryAppWindow.IsVisible)
            {
                await secondaryAppWindow.TryShowAsync();
                return;
            }

            // Create a new window if one does not already exist
            secondaryAppWindow = await AppWindow.TryCreateAsync();

            secondaryAppWindow.Closed += (sender, args) =>
            {
                secondaryWindowPage.OnClose(this.ReactContext);
                Debug.WriteLine("Window closed and resources released.");
                secondaryAppWindow = null;
            };

            Frame appWindowContentFrame = new Frame();
            appWindowContentFrame.Navigate(typeof(SecondaryWindow), null);

            secondaryWindowPage = appWindowContentFrame.Content as SecondaryWindow;

            ElementCompositionPreview.SetAppWindowContent(secondaryAppWindow, appWindowContentFrame);

            await secondaryAppWindow.TryShowAsync();
            appWindowContentFrame.Focus(FocusState.Programmatic);
        }
    }
}
