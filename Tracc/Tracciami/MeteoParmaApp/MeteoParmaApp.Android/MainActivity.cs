using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Timers;
using Android;
using ZXing.Mobile;
using System.Collections.Generic;
using Plugin.CurrentActivity;
using Android.Content;

namespace TracciamiApp.Droid
{
    [Activity(Label = "TracciamiApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        public MobileBarcodeScanner scanner;
        public List<String> ONE_D_CODE_TYPES = new List<String> { "UPC_A", "UPC_E", "EAN_8", "EAN_13", "CODE_39", "CODE_93", "CODE_128", "ITF", "RSS_14", "RSS_EXPANDED" };
        public List<String> QR_CODE_TYPES = new List<String> { "QR_CODE" };
        public List<String> MIXED_CODE_TYPES = new List<String> { "QR_CODE", "CODE_39","CODE_128" };
        public Timer scanTimer = new Timer(10000);
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);


            LoadApplication(new App());

            //var javafile = Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDownloads);

            ////var javafile = (Android.OS.Environment.GetExternalStoragePublicDirectory( Android.OS.Environment.DirectoryPictures));
            //string javafile1 = System.IO.Path.Combine(javafile.AbsolutePath, "a.txt");
            //System.IO.File.WriteAllText(javafile1, "prova");

        }
        public void sendLink(string newUrl)
        {
            Intent shareIntent = new Intent();
            StrictMode.VmPolicy.Builder builder = new StrictMode.VmPolicy.Builder();
            StrictMode.SetVmPolicy(builder.Build());
            Android.Net.Uri uriToImage = Android.Net.Uri.Parse($"file://{newUrl}");

            shareIntent.SetAction(Intent.ActionSend);
            shareIntent.SetType("image/*");
            shareIntent.PutExtra(Intent.ExtraStream, uriToImage);
            shareIntent.AddFlags(ActivityFlags.GrantReadUriPermission);

            StartActivity(Intent.CreateChooser(shareIntent, "Share Image"));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            

        }
        //Cancello la scansione dopo tot tempo
        public void scanTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            scanTimer.Stop();
            scanner.Cancel();
        }
        public async System.Threading.Tasks.Task<String> getbarcode(int Tab)
        {
            MobileBarcodeScanner.Initialize(Application);
            var options = new ZXing.Mobile.MobileBarcodeScanningOptions();

            int thisTab = Tab;

            if ((thisTab >= 0) && (thisTab <= 3))
            {
                bool qr = (thisTab == 0);

                options.PossibleFormats = qr ? Utils.setDesiredBarcodeFormats(QR_CODE_TYPES) : Utils.setDesiredBarcodeFormats(MIXED_CODE_TYPES);


                scanner = new MobileBarcodeScanner();

                scanTimer.Elapsed += scanTimer_Elapsed; //Evento contatore, chiude la scansione del barcode dopo tot secondi
                scanTimer.Start();


                var result = await scanner.Scan(options);
                scanner.Cancel();
                scanTimer.Stop();


                if (result == null)
                {
                    //Per far comparire il Toast devo aggiornare l'UI thread 
                    //https://docs.microsoft.com/it-it/xamarin/android/app-fundamentals/writing-responsive-apps
                    RunOnUiThread(() => Toast.MakeText(this.ApplicationContext, "Barcode scan cancelled", ToastLength.Long).Show());

                    return String.Empty;
                }

                return result.Text;
            }
            else
            {
                return String.Empty;
            }

        }
    }
}