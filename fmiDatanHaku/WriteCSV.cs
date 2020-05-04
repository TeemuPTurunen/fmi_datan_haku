using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmiDatanHaku
{
    static class WriteCSV
    {

        /// <summary>
        /// creates csv file and writes data into it.
        /// </summary>
        /// <param name="measurements"></param>
        static public void Write(List<Temperature> measurements, string path)
        { 

            var csv = new StringBuilder();

            //var columnNames = string.Format("Time; Value; Location");
            //csv.AppendLine(columnNames);

                for (int i = 0; i<measurements.Count(); i++)
                {
                    string time = measurements[i].Timestamp.ToString();
                    string value = measurements[i].Values.ToString();
                    string location = measurements[i].Location.ToString().Replace(",", ".");

                    var newLine = string.Format("{0}; {1}; {2}", time, value, location);
                    csv.AppendLine(newLine);

               }


  
             File.WriteAllText(@path, csv.ToString());

    }
}
}
