using System;
using System.Collections.Generic;
using System.Text;

namespace TracciamiApp.Core
{
    public interface INative
    {
        void Vibrate(long ms);
        System.Threading.Tasks.Task<String> getImagePath();
        String getPath(string fname);
        System.Threading.Tasks.Task<String> get2DBarcode(int tab);
        void SendLink(string newUrl);
        void SendImage(string b64, string plID);
        void DownloadUpdate(string apkUrl);
    }
}
