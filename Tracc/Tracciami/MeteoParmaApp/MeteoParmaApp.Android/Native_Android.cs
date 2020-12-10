using Xamarin.Forms;

using Android.App;
using System.Threading.Tasks;
using System;
using Xamarin.Essentials;
using Android.Content;
using Plugin.CurrentActivity;
using ZXing.Mobile;
using Java.IO;
using Android.Bluetooth;
using Java.Util;
using System.Threading;
using System.Collections.ObjectModel;

[assembly: UsesPermission(Android.Manifest.Permission.Vibrate)]
[assembly: Dependency(typeof(TracciamiApp.Droid.Native_Android))]
namespace TracciamiApp.Droid
{
    public class Native_Android : TracciamiApp.Core.INative
    {
        public Task<string> get2DBarcode(int Tab)
        {
            return Task.Run(async () =>
            {
                return await (CrossCurrentActivity.Current.Activity as MainActivity).getbarcode(Tab);
            });
        }

        public Task<string> getImagePath()
        {
            return Task.Run(async () =>
            {
                return "";
            });
        }

        public void Vibrate(long ms)
        {
            try
            {
                // Use default vibration length
                Vibration.Vibrate();

                // Or use specified time
                var duration = TimeSpan.FromMilliseconds(ms);
                Vibration.Vibrate(duration);
            }
            catch (FeatureNotSupportedException ex)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }

        public void SendLink(string newUrl)
        {
            (CrossCurrentActivity.Current.Activity as MainActivity).sendLink(newUrl);
        }

        public void SendImage(string b64, string plID)
        {
           // (CrossCurrentActivity.Current.Activity as MainActivity).sendImage(b64, plID);
        }

        public void DownloadUpdate(string apkUrl)
        {
           // (CrossCurrentActivity.Current.Activity as MainActivity).DownloadUpdate(apkUrl);
        }

        public string getPath(string fname)
        {
            var javafile = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDownloads);

            //var javafile = (Android.OS.Environment.GetExternalStoragePublicDirectory( Android.OS.Environment.DirectoryPictures));
            string javafile1 = System.IO.Path.Combine(javafile.AbsolutePath, fname);
            return javafile1;
        }
    }
}
