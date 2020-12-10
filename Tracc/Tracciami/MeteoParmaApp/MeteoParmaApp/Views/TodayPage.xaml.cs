using Xamarin.Forms;
using TracciamiApp.Core;
using System.IO;
using OfficeOpenXml.Style;
using System.Threading.Tasks;

namespace TracciamiApp.Views
{
	public partial class TodayPage : MyContentPage
	{
		public TodayPage ()
		{
			BindingContext = this;
			InitializeComponent ();

			


		}

	
        protected override async void OnAppearing()
        {
            base.OnAppearing();



			updateUI();
		}

		public void updateUI()
        {
			msgLabel.Text = MAIN_PAGE.MySession.GlobalItems.Count.ToString() + " Voci in anagrafica.";

			btnExp.IsVisible = false;

			if (MAIN_PAGE.MySession.CurrentInventory != null)
            {
				btnExp.IsVisible = (MAIN_PAGE.MySession.CurrentInventory.Items.Count > 0);

				msgLabel.Text = msgLabel.Text + MAIN_PAGE.MySession.CurrentInventory.Items.Count + " in lista.";
			}



		}
		private async void Button_Clicked(object sender, System.EventArgs e)

        {
			var f = await Plugin.FilePicker.CrossFilePicker.Current.PickFile();
			if (f == null) return;
			string ext = System.IO.Path.GetExtension(f.FileName).ToUpper();
			if (ext != ".XLSX") {
				await DisplayAlert("ERRORE", "Devi prelevare un file XLSX", "OK");
				return;
			};

			bool answer = await DisplayAlert("Carico ?", f.FilePath, "SI", "NO");
			if (!answer) return;
			

			try
            {
				//var s = await MAIN_PAGE.MySession.LoadCSVItems(f.FilePath, txtcampi.Text, txtsep.Text.ToCharArray()[0]);
				var s= await MAIN_PAGE.MySession.LoadExcelDRItems(f.FilePath, txtcampi.Text);
				updateUI();
			}
			catch (System.Exception ee )
            {
				await DisplayAlert("ERRORE", ee.Message + " " + ee.StackTrace, "OK");
			}

				

	

			
			
		}

        private async void btnExp_Clicked(object sender, System.EventArgs e)
        {
			//var s=await MAIN_PAGE.MySession.SaveExcelItems();

			//var s = await MAIN_PAGE.MySession.SaveCSVItems(txtsep.Text.ToCharArray()[0]);

			bool answer = await DisplayAlert("ESPORTAZIONE", "Vuoi esportare il file ?", "SI", "NO");
			if (!answer) return;


			string dtFine = await DisplayPromptAsync("DATA FINE", "Indica data di fine","OK","Annulla", System.DateTime.Now.Date.ToShortDateString(), 8);
			string oraFine = await DisplayPromptAsync("ORA FINE", "Indica ora di fine", "OK", "Annulla", System.DateTime.Now.ToShortTimeString(), 8);

			if (string.IsNullOrEmpty(dtFine)) dtFine = System.DateTime.Now.Date.ToShortDateString();
			if (string.IsNullOrEmpty(oraFine)) oraFine = System.DateTime.Now.ToShortTimeString();

			var s = await MAIN_PAGE.MySession.SaveSimpleExcelItems(dtFine, oraFine);
			MAIN_PAGE.MySession.SendLink(s);
			//MAIN_PAGE.MySession.SendLink("ppippo");
		}
    }
}

