using Microsoft.ReactNative;
using Microsoft.ReactNative.Managed;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace rnwmultiwindow
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SecondaryWindow : Page
    {
        public SecondaryWindow()
        {
            this.InitializeComponent();

            var app = Application.Current as App;
            secondaryWindowReactRootView.ReactNativeHost = app.Host;
        }

        public void OnClose(IReactContext reactContext)
        {
            if (secondaryWindowReactRootView.Parent is Panel parentPanel)
            {
                parentPanel.Children.Remove(secondaryWindowReactRootView);
            }

            var jsDispatcher = reactContext.UIDispatcher;

            jsDispatcher.Post(() =>
            {
                secondaryWindowReactRootView.ReactNativeHost = null;
            });
        }
    }
}
