using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace fmiDatanHaku
{
    static class ReadData
    { 
        /// <summary>
        /// Reads temperature(td), location and time data from query;
        /// </summary>
        /// <param name="measurements"></param>
        /// <param name="url"></param>
        /// <param name="place"></param>
        /// <returns></returns>

    static public void ReadTemperature(List<Temperature> measurements, string[] url)
        {
            XmlTextReader reader;
            

            for (int i = 0; i < url.Count(); i++)
            {
                reader = new XmlTextReader(url[i]);

                while (reader.Read())
                {



                    reader.ReadToFollowing(Constants.XMLReadToLocation);

                    if (reader.EOF == false)
                    {
                        Temperature m = new Temperature();

                        m.Location = Location.ParseWGS84(reader.ReadElementContentAsString());
                        reader.ReadToFollowing(Constants.XMLReadToTime);
                        m.Timestamp = Convert.ToDateTime(reader.ReadElementContentAsString());
                        reader.ReadToFollowing(Constants.XMLReadToValue);
                        m.Values = Convert.ToDouble(reader.ReadElementContentAsString().Replace(".", ","));


                        measurements.Add(m);
                    }
                    
                }

            }
                
            
            
        }
    }
}
