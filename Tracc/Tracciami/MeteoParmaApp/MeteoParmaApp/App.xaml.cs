using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TracciamiApp.Views;

namespace TracciamiApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var enc1252 = System.Text.Encoding.GetEncoding(1252);
            MainPage = new MainPage();
           // MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            MainPage mp = (MainPage)MainPage;
            mp.MySession.MainDataLoaded = false;
        }

        protected override void OnResume()
        {
        }
    }
}
