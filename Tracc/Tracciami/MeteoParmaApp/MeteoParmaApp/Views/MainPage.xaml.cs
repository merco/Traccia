using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TracciamiApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        public Core.Session MySession
        { get; set; }
        
        public MainPage()
        {
            InitializeComponent();
            MySession = new Core.Session();
         
        }
    }
}