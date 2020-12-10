using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TracciamiApp.Data
{
    public class InventoryItem
    {
        //ID,Cognome,Nome,Località,Telefono,Gruppo,Tessera,ScadTessera,ScadVisita,TipoAtleta,Iscrizione,Anno,Cat

        public string Id { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string Localita { get; set; }
        public string Tel { get; set; }

        public string Gruppo { get; set; }
        public string Tessera { get; set; }
        public string ScadTessera { get; set; }
        public string ScadVisita { get; set; }
        public string TipoAtleta { get; set; }
        public string Iscrizione { get; set; }
        public string Anno { get; set; }
        public string Cat { get; set; }

        public string Nominativo { get { return Cognome + " " + Nome; } }



        public InventoryItem()
        {
            Id = "";
            Cognome = "";
            Nome = "";
            Localita = "";
            Tel = "";
            Gruppo = "";
            Tessera = "";
            ScadTessera = "";
            ScadVisita = "";
            TipoAtleta = "";
            Iscrizione = "";
            Anno = "";
            Cat = "";
        }
        public void PulisciDate()
        {
            if (!string.IsNullOrEmpty(ScadTessera))
            {
                if (ScadTessera.Contains(" ")) ScadTessera = ScadTessera.Split(' ')[0];
            }
            if (!string.IsNullOrEmpty(ScadVisita))
            {
                if (ScadVisita.Contains(" ")) ScadVisita = ScadVisita.Split(' ')[0];
            }
            if (!string.IsNullOrEmpty(Iscrizione))
            {
                if (Iscrizione.Contains(" ")) Iscrizione = Iscrizione.Split(' ')[0];
            }
        }
        public override string ToString()
        {
            return Nominativo;
        }
    }

    public class InventoryItems: System.Collections.Generic.List<InventoryItem>
    {
      
    }

   
    public class InventoryInstance 
    {
        public string Nome { get; set; }
        public DateTime TimeStamp { get; set; }
        public InventoryItems Items { get; set; }
        public InventoryInstance()
        {
            Nome = "Sessione " + DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            TimeStamp = DateTime.Now;
            Items = new InventoryItems();
        }
    }
}
