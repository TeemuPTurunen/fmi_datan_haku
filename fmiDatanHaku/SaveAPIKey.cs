using System;
using System.IO;

namespace fmiDatanHaku
{
    public class SaveAPIKey
    {
       static public void SaveKey(string key)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            path = Path.Combine(path, Constants.FolderName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path, Constants.FileName);

            File.WriteAllText(@path, key);

        }

    }
}
