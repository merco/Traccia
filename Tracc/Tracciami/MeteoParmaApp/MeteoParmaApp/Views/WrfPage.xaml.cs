using TracciamiApp.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TracciamiApp.Data;
using System.Security;

namespace TracciamiApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WrfPage : MyContentPage
    {

       
        public WrfPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
      
       
        protected override async void OnAppearing()
        {

            mainLW.ItemsSource = null;
               mainLW.ItemsSource = MAIN_PAGE.MySession.GlobalItems;
            dett.IsVisible = false;
            mainLW.IsVisible = true;
            btnAdd.IsVisible = true;
            btnSave.IsVisible = false;

        }

        private void mainLW_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            InventoryItem itm = (InventoryItem)e.Item;
            gr.BindingContext = itm;

            //DisplayAlert(itm.Id, itm.Nominativo + "\r\n" + itm.Localita + "\r\n" + itm.Tel, "OK");
            mainLW.IsVisible = false;
            dett.IsVisible = true;
            //btnAdd.IsVisible = true;
            btnSave.IsVisible = false;
            btnEli.IsEnabled = string.IsNullOrEmpty(itm.Id);

        }

        private void btnAdd_Clicked(object sender, EventArgs e)
        {

            mainLW.IsVisible = false;
            dett.IsVisible = true;

            btnAdd.IsVisible = false;

            InventoryItem itm = new InventoryItem();
            gr.BindingContext = itm;

            btnSave.IsVisible = true;
            
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            InventoryItem itm = (InventoryItem)gr.BindingContext;
            if (string.IsNullOrEmpty(itm.Nominativo) || string.IsNullOrEmpty(itm.Localita) || string.IsNullOrEmpty(itm.Tel))
            {
                bool answer = await DisplayAlert("ERRORE", "Devi compilare tutto ! \r\n Vuoi annullare", "SI", "NO");
                if (!answer) return;

            } else
            {
                if (!MAIN_PAGE.MySession.GlobalItems.Contains(itm)) MAIN_PAGE.MySession.GlobalItems.Add(itm);
                MAIN_PAGE.MySession.SaveGlobalItems();
            }


            resetView();

        }
        private void resetView()
        {
            mainLW.ItemsSource = null;
            mainLW.ItemsSource = MAIN_PAGE.MySession.GlobalItems;
            dett.IsVisible = false;
            mainLW.IsVisible = true;
            btnAdd.IsVisible = true;
            btnSave.IsVisible = false;
            btnEli.IsEnabled = false;
        }
        private async void btnEli_Clicked(object sender, EventArgs e)
        {
            InventoryItem itm = (InventoryItem)gr.BindingContext;

            bool answer = await DisplayAlert("ELIMINO ?", itm.Nominativo, "SI", "NO");
            if (!answer) return;


            if (MAIN_PAGE.MySession.GlobalItems.Contains(itm)) MAIN_PAGE.MySession.GlobalItems.Remove(itm);
            MAIN_PAGE.MySession.SaveGlobalItems();

            resetView();
        }
    }
}