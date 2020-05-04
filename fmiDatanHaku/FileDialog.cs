using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmiDatanHaku
{
    public class FileDialog
    {
        /// <summary>
        /// Creates dialog to ask file location
        /// </summary>
        /// <returns></returns>
        static public string AskFile(string[] places, DateTime dtStart, DateTime dtEnd)
        {

            string filename = "";
            foreach (string place in places)
            {
                filename += place + "-";
            }
            filename += dtStart.ToString("yyyy-MM-dd");
            filename += "-";
            filename += dtEnd.ToString("yyyy-MM-dd");

            // Configure save file dialog box
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = filename; // Default file name
            dlg.DefaultExt = ".csv"; // Default file extension
            dlg.Filter = "Text documents (.csv)|*.csv"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                filename = dlg.FileName;
            }

            return filename;
        }
    }
}
