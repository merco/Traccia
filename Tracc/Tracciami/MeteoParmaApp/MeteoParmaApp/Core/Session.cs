using TracciamiApp.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Runtime.InteropServices;
using OfficeOpenXml;
using System.Linq;
using System.Drawing.Imaging;
using Xamarin.Forms.Internals;
using ExcelDataReader;
using System.Data;

namespace TracciamiApp.Core
{
    public class Session
    {
        System.Reflection.Assembly _localAssembly;
        String _assemblyName = "TracciamiApp";
        public static string COMUNE_KEY = "CodiceComune";
        public static string DATASET_KEY = "TracciamiDataset";
        public static string CURRENT_KEY = "TracciamiLista";
        public String BaseUrl
        { get; set; }

        public String CodiceComune
        { get; set; }
        public String UrlPrevision
        { get; set; }
        public String UrlWrf
        { get; set; }
        public String UrlWebcam
        { get; set; }
        public String UrlPC
        { get; set; }
        public bool MainDataLoaded
        { get; set; }
       

        public InventoryItems GlobalItems
        { get; set; }

        public InventoryInstance CurrentInventory
         { get; set; }

    public Session()
        {
            BaseUrl = "https://davidemercanti.altervista.org/MeteoPR/";
            UrlPrevision = BaseUrl + "grabMeteo.php";
            UrlWrf = BaseUrl + "wrf.php";
            UrlWebcam = BaseUrl + "webcams.php";
            UrlPC = BaseUrl + "grabPC.php?C=%CODICECOMUNE%";
           
            MainDataLoaded = false;
            _localAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            CodiceComune = "";
            GlobalItems = new InventoryItems();

            if (Application.Current.Properties.ContainsKey(DATASET_KEY))
            {
                string tmp = Application.Current.Properties[DATASET_KEY] as string;
                GlobalItems = JsonConvert.DeserializeObject<InventoryItems>(tmp);

             
                MainDataLoaded = true;
            }
            if (GlobalItems.Count>0 && Application.Current.Properties.ContainsKey(CURRENT_KEY))
            {
                string tmp = Application.Current.Properties[CURRENT_KEY] as string;
                CurrentInventory = JsonConvert.DeserializeObject<InventoryInstance>(tmp);
               
            }

        }
        public async System.Threading.Tasks.Task<string> take2DBarcode(int Tab)
        {
            String s = await Xamarin.Forms.DependencyService.Get<Core.INative>().get2DBarcode(Tab);
            return s;
        }
        public void Vibrate(long ms)
        {
            Xamarin.Forms.DependencyService.Get<Core.INative>().Vibrate(ms);
        }
        public void SendLink(string newUrl)
        {
            Xamarin.Forms.DependencyService.Get<Core.INative>().SendLink(newUrl);
        }
        public string GetPath(string newUrl)
        {
           return  Xamarin.Forms.DependencyService.Get<Core.INative>().getPath(newUrl);
        }
        public void SendImage(string b64, string plID)
        {
            Xamarin.Forms.DependencyService.Get<Core.INative>().SendImage(b64, plID);
        }
        public void SaveComune()
        {
            Application.Current.Properties[COMUNE_KEY] = CodiceComune;
        }

        public void SaveGlobalItems()
        {
            string output = JsonConvert.SerializeObject(GlobalItems);

            Application.Current.Properties[DATASET_KEY] = output;
            App.Current.SavePropertiesAsync();
        }
        public void SaveCurrent()
        {
            string output = JsonConvert.SerializeObject(CurrentInventory);

            Application.Current.Properties[CURRENT_KEY] = output;
            App.Current.SavePropertiesAsync();
        }

        private string GetInternalData(String fileName)
        {
            System.IO.Stream tStream = _localAssembly.GetManifestResourceStream(_assemblyName + ".InternalData." + fileName);
            string result = "";
            using (StreamReader reader = new StreamReader(tStream))
            {
                result = reader.ReadToEnd();
            }
            tStream.Dispose();
            return result;
        }
        private  async Task<byte[]> GetURLContentsAsync(string url, bool GZip, string PostData)
        {
            // The downloaded resource ends up in the variable named content.
            var content = new MemoryStream();

            // Initialize an HttpWebRequest for the current URL.
            var webReq = (HttpWebRequest)WebRequest.Create(url);
            if (GZip)
                webReq.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");

            if (!string.IsNullOrEmpty(PostData))
            {
                webReq.Method = "POST";
                byte[] lbPostBuffer = Encoding.Default.GetBytes(PostData);

                webReq.ContentLength = lbPostBuffer.Length;

                Stream PostStream = webReq.GetRequestStream();
                PostStream.Write(lbPostBuffer, 0, lbPostBuffer.Length);
                PostStream.Close();
            }

            // Send the request to the Internet resource and wait for
            // the response.
            using (WebResponse response = await webReq.GetResponseAsync())

            // The previous statement abbreviates the following two statements.

            //Task<WebResponse> responseTask = webReq.GetResponseAsync();
            //using (WebResponse response = await responseTask)
            {
                // Get the data stream that is associated with the specified url.
                using (Stream responseStream = response.GetResponseStream())
                {
                    // Read the bytes in responseStream and copy them to content.
                    await responseStream.CopyToAsync(content);

                    // The previous statement abbreviates the following two statements.

                    // CopyToAsync returns a Task, not a Task<T>.
                    //Task copyTask = responseStream.CopyToAsync(content);

                    // When copyTask is completed, content contains a copy of
                    // responseStream.
                    //await copyTask;

                   
                }
            }
            // Return the result as a byte array.
            return content.ToArray();
        }
     
        private async System.Threading.Tasks.Task<string> _getURL(string URL)
        {
            string responseJson = "";
            HttpClient _client = null;
            try
            {

                HttpClientHandler handler = new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                };

                _client = new HttpClient(handler);
               
                _client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip,deflate");
                // String content = await _client.GetStringAsync(URL + "?D=" + long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")));


                String content = "";
                
                if (!URL.Contains("?")) content=await _client.GetStringAsync(URL + "?D=" + long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")));
                else content= await _client.GetStringAsync(URL + "&D=" + long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")));

                responseJson = content;

            }
            catch (Exception err)
            {
                responseJson = "!!! " + err.Message;
            }
            finally
            {
                _client.Dispose();
            }
            return responseJson;
        }
        public async System.Threading.Tasks.Task<string> GetWeatherData()
        {
            var response = await _getURL(UrlPrevision);

           
          
            MainDataLoaded = true;
            return "";
        }
        public async System.Threading.Tasks.Task<string> GetWrfData()
        {
            var response = await _getURL(UrlWrf);

            if (response.StartsWith("!!!")) return response;


           
        
            return "";
        }
        public async System.Threading.Tasks.Task<string> GetWebcamData()
        {
            var response = await _getURL(UrlWebcam);

            if (response.StartsWith("!!!")) return response;


           
            return "";
        }
        public async System.Threading.Tasks.Task<string> GetPCData()
        {

            string tmp = UrlPC.Replace("%CODICECOMUNE%", CodiceComune);
            var response = await _getURL(tmp);

            if (response.StartsWith("!!!")) return response;


          
            return "";
        }
        public void GetComuni()
        {
         

        }

        public async System.Threading.Tasks.Task<string> SaveCSVItems(char sep)
        {
            string fileToExcel = GetPath("ListaSessione" + DateTime.Today.ToString("dd-MM-yyyy") + ".csv");
            if (System.IO.File.Exists(fileToExcel)) System.IO.File.Delete(fileToExcel);

            System.Text.StringBuilder SB = new System.Text.StringBuilder();

           
            SB.AppendLine("Data" + sep + "Ora" + sep + "Id" + sep + "Nome" + sep + "Abitazione" + sep + "Tel");

           

            int recordIndex = 2;

            foreach (InventoryItem itm in CurrentInventory.Items)
            {


                SB.AppendLine(CurrentInventory.TimeStamp.ToString("dd-MM-yyyy") + sep + CurrentInventory.TimeStamp.ToString("HH:mm") + sep + itm.Id + sep + itm.Nominativo + sep + itm.Localita + sep + itm.Tel);

               
                recordIndex++;
            }




            // Write content to excel file  
            File.WriteAllText(fileToExcel, SB.ToString());

           

            return fileToExcel;

        }
        public async System.Threading.Tasks.Task<string> SaveSimpleExcelItems(string dataFine, string oraFine)
        {
            string fileToExcel = GetPath("ListaSessione" + DateTime.Today.ToString("dd-MM-yyyy") + ".xlsx");
            if (System.IO.File.Exists(fileToExcel)) System.IO.File.Delete(fileToExcel);

            var wb = new Simplexcel.Workbook();
            wb.Title = System.IO.Path.GetFileNameWithoutExtension(fileToExcel);
            wb.Author = "MD";
            var sheet = new Simplexcel.Worksheet(wb.Title);

// "ID,Cognome,Nome,Località,Telefono,Gruppo,Tessera,ScadTessera,ScadVisita,TipoAtleta,Iscrizione,Anno,Cat" 


             sheet.Cells[0, 0].Value = "IngressoData";
            sheet.Cells[0, 1].Value = "IngressoOra";
            sheet.Cells[0, 2].Value = "Id";
            sheet.Cells[0, 3].Value = "Cognome";
            sheet.Cells[0, 4].Value = "Nome";
            sheet.Cells[0, 5].Value = "Località";
            sheet.Cells[0, 6].Value = "Telefono";
            sheet.Cells[0, 7].Value = "Gruppo";
            sheet.Cells[0, 8].Value = "Tessera";
            sheet.Cells[0, 9].Value = "ScadTessera";
            sheet.Cells[0, 10].Value = "ScadVisita";
            sheet.Cells[0, 11].Value = "TipoAtleta";
            sheet.Cells[0, 12].Value = "Iscrizione";
            sheet.Cells[0, 13].Value = "Anno";
            sheet.Cells[0, 14].Value = "Cat";
            sheet.Cells[0, 15].Value = "UscitaData";
            sheet.Cells[0, 16].Value = "UscitaOra";

            int recordIndex = 1;

            foreach (InventoryItem itm in CurrentInventory.Items)
            {
                sheet.Cells[recordIndex, 0].Value = CurrentInventory.TimeStamp.ToString("dd-MM-yyyy");
                sheet.Cells[recordIndex, 1].Value = CurrentInventory.TimeStamp.ToString("HH:mm");
                sheet.Cells[recordIndex, 2].Value = itm.Id;
                sheet.Cells[recordIndex, 3].Value = itm.Cognome;
                sheet.Cells[recordIndex, 4].Value = itm.Nome;
                sheet.Cells[recordIndex, 5].Value = itm.Localita;
                sheet.Cells[recordIndex, 6].Value = itm.Tel;
                sheet.Cells[recordIndex, 7].Value = itm.Gruppo;
                sheet.Cells[recordIndex, 8].Value = itm.Tessera;
                sheet.Cells[recordIndex, 9].Value = itm.ScadTessera;
                sheet.Cells[recordIndex, 10].Value = itm.ScadVisita;
                sheet.Cells[recordIndex, 11].Value = itm.TipoAtleta;
                sheet.Cells[recordIndex, 12].Value = itm.Iscrizione;
                sheet.Cells[recordIndex, 13].Value = itm.Anno;
                sheet.Cells[recordIndex, 14].Value = itm.Cat;
                sheet.Cells[recordIndex, 15].Value = dataFine;
                sheet.Cells[recordIndex, 16].Value = oraFine;


                recordIndex++;
            }

            wb.Add(sheet);
            wb.Save(fileToExcel, compress: false);

            
            return fileToExcel;

        }
        public async System.Threading.Tasks.Task<string> SaveExcelItems()
        {
            string fileToExcel = GetPath("ListaSessione" + DateTime.Today.ToString("dd-MM-yyyy") + ".xlsx");
            if (System.IO.File.Exists(fileToExcel)) System.IO.File.Delete(fileToExcel);

            ExcelPackage Ep = new ExcelPackage();
            var workSheet = Ep.Workbook.Worksheets.Add(CurrentInventory.Nome);
            workSheet.Cells[1, 1].Value = "Data";
            workSheet.Cells[1, 2].Value = "Ora";
            workSheet.Cells[1, 3].Value = "Id";
            workSheet.Cells[1, 4].Value = "Nome";
            workSheet.Cells[1, 5].Value = "Abitazione";
            workSheet.Cells[1, 6].Value = "Tel";

            int recordIndex = 2;

            foreach (InventoryItem itm in CurrentInventory.Items)
            {
                workSheet.Cells[recordIndex, 1].Value = CurrentInventory.TimeStamp.ToString("dd-MM-yyyy");
                workSheet.Cells[recordIndex, 2].Value = CurrentInventory.TimeStamp.ToString("HH:mm");
                workSheet.Cells[recordIndex, 3].Value = itm.Id;
                workSheet.Cells[recordIndex, 4].Value = itm.Nominativo;
                workSheet.Cells[recordIndex, 5].Value = itm.Localita;
                workSheet.Cells[recordIndex, 6].Value = itm.Tel;
                recordIndex++;
            }
           

      

            // Write content to excel file  
            File.WriteAllBytes(fileToExcel, Ep.GetAsByteArray());
        
            //Close Excel package 
            Ep.Dispose();

            return fileToExcel;

        }
        public async System.Threading.Tasks.Task<string> LoadExcelItems(string pathToExcel, string txtfields)
        {

            Log.Warning("TRACCIAMI", "LoadExcelItems 1");
            InventoryItems vo = new InventoryItems();
            if (!System.IO.File.Exists(pathToExcel)) return "";
            System.IO.FileInfo FI = new System.IO.FileInfo(pathToExcel);
            Log.Warning("TRACCIAMI", "LoadExcelItems 2");
            ExcelPackage Ep = new ExcelPackage(FI);
            if (Ep.Workbook.Worksheets.Count <= 0) return "";
            Log.Warning("TRACCIAMI", "LoadExcelItems 3");
            ExcelWorksheet wsc = Ep.Workbook.Worksheets[1];
            int maxrows = wsc.Dimension.End.Row;
            int maxcols = wsc.Dimension.End.Column;
            string[] indicilist = txtfields.Split(',');
            var indici = new System.Collections.Generic.List<int>();
            foreach (string stridx in indicilist) indici.Add(Convert.ToInt32(stridx));

            Log.Warning("TRACCIAMI", "LoadExcelItems 4");

            for (int ir = 1; ir <= maxrows; ir++)
            {
                 InventoryItem li = new InventoryItem();
                var obj = wsc.Cells[ir, indici[0]].Value;

                if (obj != null)
                {
                    li.Id = obj.ToString();
                } else
                {
                    goto nextRow;
                }
                obj = wsc.Cells[ir, indici[1]].Value;
                if (obj != null)
                {
                    if (obj.ToString().StartsWith("#")) goto nextRow;
                    li.Cognome = obj.ToString();
                }
                else
                {
                    goto nextRow;
                }
                obj = wsc.Cells[ir, indici[2]].Value;
                if (obj != null) li.Nome = obj.ToString();
               
                obj = wsc.Cells[ir, indici[3]].Value;
                if (obj != null)
                {
                    li.Tel = obj.ToString();
                }
                if (!string.IsNullOrEmpty(li.Id)) vo.Add(li);


                nextRow:
                int a = 1;
            }
            Log.Warning("TRACCIAMI", "LoadExcelItems 5");
            wsc.Dispose();
            Ep.Dispose();
            Log.Warning("TRACCIAMI", "LoadExcelItems 6");
            var ele = vo.OrderBy(x => x.Nominativo).ToList();
            Log.Warning("TRACCIAMI", "LoadExcelItems 7");
            GlobalItems = new InventoryItems();
            GlobalItems.AddRange(ele);
            Log.Warning("TRACCIAMI", "LoadExcelItems 8");
            SaveGlobalItems();

            return "";
        }
        public async System.Threading.Tasks.Task<string> LoadExcelDRItems(string pathToExcel, string txtfields)
        {

           
            InventoryItems vo = new InventoryItems();
            if (!System.IO.File.Exists(pathToExcel)) return "";
            System.IO.FileInfo FI = new System.IO.FileInfo(pathToExcel);

             var stream = new FileStream(pathToExcel, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);



            ExcelDataReader.IExcelDataReader reader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream);


            // reader.IsFirstRowAsColumnNames = firstRowNamesCheckBox.Checked;
            var ds = reader.AsDataSet(new ExcelDataSetConfiguration()
            {
                UseColumnDataType = false,
                ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = false
                }
            });
            reader.Close();
            reader.Dispose();
            stream.Close();
            stream.Dispose();



            if (ds.Tables.Count <= 0) return "";
            var dt = ds.Tables[0];



            string[] indicilist = txtfields.Split(',');
            var indici = new System.Collections.Generic.List<int>();
            foreach (string stridx in indicilist) indici.Add(Convert.ToInt32(stridx));
            int maxrows = dt.Rows.Count;

 

            for (int ir = 1; ir < maxrows; ir++)
            {
                DataRow dr = dt.Rows[ir];
                InventoryItem li = new InventoryItem();
                var obj = dr[indici[0]-1];
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
                {
                    li.Id = obj.ToString();
                }
                else
                {
                    goto nextRow;
                }
                obj = dr[indici[1]-1];
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
                {
                    if (obj.ToString().StartsWith("#")) goto nextRow;
                    li.Cognome = obj.ToString();
                }
                else
                {
                    goto nextRow;
                }
                obj = dr[indici[2]-1];
                if (obj != null) li.Nome = obj.ToString();

                obj = dr[indici[3] - 1];
                if (obj != null) li.Localita = obj.ToString();

                obj = dr[indici[4] - 1];
                if (obj != null) li.Tel = obj.ToString();


                obj = dr[indici[5] - 1];
                if (obj != null) li.Gruppo = obj.ToString();

                obj = dr[indici[6] - 1];
                if (obj != null) li.Tessera = obj.ToString();

                obj = dr[indici[7] - 1];
                if (obj != null) li.ScadTessera = obj.ToString();

                obj = dr[indici[8] - 1];
                if (obj != null) li.ScadVisita = obj.ToString();

                obj = dr[indici[9] - 1];
                if (obj != null) li.TipoAtleta = obj.ToString();

                obj = dr[indici[10] - 1];
                if (obj != null) li.Iscrizione = obj.ToString();

                obj = dr[indici[11] - 1];
                if (obj != null) li.Anno = obj.ToString();
                obj = dr[indici[12] - 1];
                if (obj != null) li.Cat = obj.ToString();
                //ID,Cognome,Nome,Località,Telefono,Gruppo,Tessera,ScadTessera,ScadVisita,TipoAtleta,Iscrizione,Anno,Cat


                li.PulisciDate();

                if (!string.IsNullOrEmpty(li.Id)) vo.Add(li);



                nextRow:
                int a = 1;
            }

            
 
            var ele = vo.OrderBy(x => x.Nominativo).ToList();
     
            GlobalItems = new InventoryItems();
            GlobalItems.AddRange(ele);
           
            SaveGlobalItems();

            return "";
        }
        public async System.Threading.Tasks.Task<string> LoadCSVItems(string pathToExcel, string txtfields, char sep)
        {

    
            InventoryItems vo = new InventoryItems();
            if (!System.IO.File.Exists(pathToExcel)) return "";
            System.IO.FileInfo FI = new System.IO.FileInfo(pathToExcel);

            string[] rows = System.IO.File.ReadAllLines(pathToExcel);
           
            if (rows.Length <= 0) return "";


            int maxrows = rows.Length - 1;
       
            string[] indicilist = txtfields.Split(',');
            var indici = new System.Collections.Generic.List<int>();
            foreach (string stridx in indicilist) indici.Add(Convert.ToInt32(stridx));



            for (int ir = 0; ir <= maxrows; ir++)
            {
                string rigaCorrente = rows[ir];
                InventoryItem li = new InventoryItem();
                string[] campilocali = rigaCorrente.Split(sep);
                var obj = campilocali[indici[0]-1];

                if (obj != null && !string.IsNullOrEmpty(obj))
                {
                    li.Id = obj.ToString();
                }
                else
                {
                    goto nextRow;
                }
                obj =campilocali[indici[1] - 1];
                if (obj != null && !string.IsNullOrEmpty(obj))
                {
                    if (obj.ToString().StartsWith("#")) goto nextRow;
                    li.Cognome = obj.ToString();
                }
                else
                {
                    goto nextRow;
                }
                obj =  campilocali[indici[2] - 1];
                if (obj != null && !string.IsNullOrEmpty(obj))
                {
                    li.Localita = obj.ToString();
                }
                obj = campilocali[indici[3] - 1];
                if (obj != null && !string.IsNullOrEmpty(obj))
                {
                    li.Tel = obj.ToString();
                }
                if (!string.IsNullOrEmpty(li.Id)) vo.Add(li);


                nextRow:
                int a = 1;
            }

            var ele = vo.OrderBy(x => x.Nominativo).ToList();

            GlobalItems = new InventoryItems();
            GlobalItems.AddRange(ele);

            SaveGlobalItems();

            return "";
        }
    }
}
