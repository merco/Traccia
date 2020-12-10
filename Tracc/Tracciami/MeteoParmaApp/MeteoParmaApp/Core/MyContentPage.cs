using TracciamiApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TracciamiApp.Core
{
    public class MyContentPage : ContentPage
    {
        public MainPage MAIN_PAGE
        { get
            {

                return (MainPage)Application.Current.MainPage;
            } 
        
        }
    }
}
