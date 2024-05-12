using Microsoft.ReactNative;
using Microsoft.ReactNative.Managed;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Drawing;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;


namespace rnwmultiwindow
{
    [ReactModule]
    public class Native
    {
        [ReactMethod]
        public async void OpenSecondaryWindow()
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                ((App)Application.Current).OpenSecondaryWindow();
            });
        }
    }
}
