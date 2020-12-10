using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using TracciamiApp.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TracciamiApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageList : Core.MyContentPage
    {
        public PageList()
        {
            InitializeComponent();
            BindingContext = this;
        }
        protected override async void OnAppearing()
        {

           
            ShowData();
            if (MAIN_PAGE.MySession.CurrentInventory == null) { txtfilter.IsVisible = false; btnBarcode.IsVisible = false; }
            else { txtfilter.IsVisible = true; btnBarcode.IsVisible = true; }



            }
        private void ShowData()
        {
            lblStat.Text = "Nessuna registrazione";
            
            if (MAIN_PAGE.MySession.CurrentInventory == null) return;
       

            lblStat.Text = MAIN_PAGE.MySession.CurrentInventory.Nome + "  (" + MAIN_PAGE.MySession.CurrentInventory.Items.Count +")";
            
        }
        private async void btnNew_Clicked(object sender, EventArgs e)
        {

            if (MAIN_PAGE.MySession.CurrentInventory!=null)
            {

                bool answer = await DisplayAlert("Azzero ","La lista corrente ?", "SI", "NO");
                if (!answer) return;
            }

            MAIN_PAGE.MySession.CurrentInventory = new InventoryInstance();
            txtfilter.IsVisible = true;
            btnBarcode.IsVisible = true;
            ShowData();
            MAIN_PAGE.MySession.SaveCurrent();


        }

        private void txtfilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            string thevalue = txtfilter.Text.ToUpper();
            if (string.IsNullOrEmpty(thevalue))
            {
                lwric.ItemsSource = null;
                return;
            }

          var list = MAIN_PAGE.MySession.GlobalItems.FindAll(X => X.Nominativo.ToUpper().Contains(thevalue));
             lwric.ItemsSource = list;
        }
        private void autoAdd(InventoryItem itm)
        {
            var presente = MAIN_PAGE.MySession.CurrentInventory.Items.FirstOrDefault(X => X.Nominativo.ToLower() == itm.Nominativo.ToLower());


            if (presente==null)
            {
                MAIN_PAGE.MySession.CurrentInventory.Items.Add(itm);
                MAIN_PAGE.MySession.SaveCurrent();
            }
        }
        private async void lwric_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            InventoryItem itm=(InventoryItem) e.Item;
            autoAdd(itm);
            await DisplayAlert(itm.Nominativo, "Aggiunto", "OK");
            ShowData();

            clearSearch();
        }
        private void clearSearch()
        {
            
            lwric.ItemsSource = null;
            txtfilter.Text = "";
        }
        private async void btnBarcode_Clicked(object sender, EventArgs e)
        {

           

            var ss=await MAIN_PAGE.MySession.take2DBarcode(1);
            if (string.IsNullOrEmpty(ss)) return;
            ss = ss.ToUpper();
           var theitm= MAIN_PAGE.MySession.GlobalItems.FirstOrDefault(X => X.Id.ToUpper() == ss);

            if (theitm!=null)
            {
                autoAdd(theitm);
                MAIN_PAGE.MySession.Vibrate(400);
            } else
            {
                MAIN_PAGE.MySession.Vibrate(3000);
                await DisplayAlert(ss, "NON TROVATO!", "OK");

            }
            ShowData();
            clearSearch();
        }

       
    }
}