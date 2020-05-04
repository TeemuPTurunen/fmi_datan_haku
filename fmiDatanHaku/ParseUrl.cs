using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmiDatanHaku
{


    static class ParseUrl
    {
        /// <summary>
        /// More information http://ilmatieteenlaitos.fi/tallennetut-kyselyt 
        /// Example url:
        /// http://data.fmi.fi/fmi-apikey/laita-fmi-avain-tähän/wfs?request=getFeature&storedquery_id=fmi::observations::weather::simple&starttime=2012-10-01T00:00:00Z&endtime=2012-10-05T00:00:00Z&place=Kuopio&parameters=td
        /// 
        /// Creates urls for reading data from fmi based off used input.

        /// 
        /// </summary>        /// 
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <param name="place"></param>
        /// <param name="timestep"></param>
        /// <returns></returns>
        static public string[] GetUrl(string apikey, DateTime dtStart, DateTime dtEnd, string[] places, string timestep)
        {
            int numberOfSearches = Convert.ToInt32(Math.Ceiling(((dtEnd - dtStart).TotalDays / Constants.MaxDays)));

            string[] url = new string[numberOfSearches];
            int searchesdone = 0;

            DateTime searchStart = dtStart;
            DateTime searchEnd = dtStart;
            string place = "";

            for(int i = 0; i < places.Count(); i++)
            {
                place += "&place=" + places[i].ToString();
            }

            for (int i = 0; i < url.Length; i++)
            {
                if (numberOfSearches > 1)
                {
                    if(searchesdone < numberOfSearches - 1)
                    {
                        searchEnd = searchStart.AddDays(Constants.MaxDays);

                        url[i] = string.Format("http://data.fmi.fi/fmi-apikey/{0}/wfs?request=getFeature&storedquery_id=fmi::observations::weather::simple&starttime={1}&endtime={2}{3}&parameters=td&timestep={4}",
    apikey, searchStart.ToString(Constants.DateFormat), searchEnd.ToString(Constants.DateFormat), place, timestep);

                        searchStart = searchEnd;
                        searchesdone++;
                    }
                    else
                    {
                        url[i] = string.Format("http://data.fmi.fi/fmi-apikey/{0}/wfs?request=getFeature&storedquery_id=fmi::observations::weather::simple&starttime={1}&endtime={2}{3}&parameters=td&timestep={4}",
apikey, searchStart.ToString(Constants.DateFormat), dtEnd.ToString(Constants.DateFormat), place, timestep);
                    }
                }
                else
                {
                    url[i] = string.Format("http://data.fmi.fi/fmi-apikey/{0}/wfs?request=getFeature&storedquery_id=fmi::observations::weather::simple&starttime={1}&endtime={2}{3}&parameters=td&timestep={4}",
                        apikey, dtStart.ToString(Constants.DateFormat), dtEnd.ToString(Constants.DateFormat), place, timestep);
                }
            }
            return url;
        }

        
    }
}
