using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracciamiApp.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TracciamiApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagePresenti : Core.MyContentPage
    {
        public PagePresenti()
        {
            InitializeComponent();
            BindingContext = this;
        }
        protected override async void OnAppearing()
        {
            lblstat.Text = "";
            if (MAIN_PAGE.MySession.CurrentInventory!=null) lwadded.ItemsSource = MAIN_PAGE.MySession.CurrentInventory.Items;
            showData();
        }
        private void showData()
        {
            lwadded.ItemsSource = null;
            if (MAIN_PAGE.MySession.CurrentInventory != null)
            {
                lwadded.ItemsSource = MAIN_PAGE.MySession.CurrentInventory.Items;
                lblstat.Text = MAIN_PAGE.MySession.CurrentInventory.Items.Count.ToString();
            } else
            {
                lblstat.Text = "";
            }
           
        }
        private async void lwadded_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            InventoryItem itm = (InventoryItem)e.Item;
            bool answer = await DisplayAlert("Rimuovo ?", itm.Nominativo, "SI", "NO");

            if (answer)
            {
                if (MAIN_PAGE.MySession.CurrentInventory.Items.Contains(itm)) MAIN_PAGE.MySession.CurrentInventory.Items.Remove(itm);
                MAIN_PAGE.MySession.SaveCurrent();
                showData();
            }

        }
    }
}