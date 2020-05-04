using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmiDatanHaku
{
    public class Constants
    {
        public const string FileName = "fmiDataSetting";

        public const string FolderName = "fmiData";

        public const int MaxDays = 7;

        public const string DateFormat = "yyyy-MM-ddTHH:mm:ssZ";

        public const string XMLReadToLocation = "gml:pos";

        public const string XMLReadToTime = "BsWfs:Time";

        public const string XMLReadToValue = "BsWfs:ParameterValue";
    }
}
