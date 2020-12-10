using System;
using System.Collections.Generic;
using Android.Content;
using Java.IO;
using Java.Lang;
using Java.Net;

namespace TracciamiApp.Droid
{
    public class Utils
    {

        public static long currentTimeMillis()
        {
            DateTime Jan1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan javaSpan = DateTime.UtcNow - Jan1970;
            return Convert.ToInt64(javaSpan.TotalMilliseconds);
        }

        public static List<ZXing.BarcodeFormat> setDesiredBarcodeFormats(List<System.String> lsBarcodeFormat)
        {
            List<ZXing.BarcodeFormat> retval = new List<ZXing.BarcodeFormat>();

            foreach (var barcodeformat in lsBarcodeFormat)
            {
                try
                {
                    ZXing.BarcodeFormat find = (ZXing.BarcodeFormat)System.Enum.Parse(typeof(ZXing.BarcodeFormat), barcodeformat);

                    retval.Add(find);
                }
                catch
                { 
                    //Formato non trovato nell'Enum
                }
            }

            return retval;
        }

    }
}