using TracciamiApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace TracciamiApp.Views
{
	public partial class SettingsPage : MyContentPage
	{
		public SettingsPage ()
		{
			InitializeComponent ();
           
		}
        protected override void OnDisappearing()
        {
            base.OnDisappearing();


        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (this.MAIN_PAGE == null) return;
            if (this.MAIN_PAGE.MySession == null) return;


        }
       

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Xamarin.Essentials.Launcher.OpenAsync("mailto:d.mercanti@gmail.com");
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            Xamarin.Essentials.Launcher.OpenAsync("https://play.google.com/store/apps/developer?id=Luigi-Davide");
        }
    }
}

