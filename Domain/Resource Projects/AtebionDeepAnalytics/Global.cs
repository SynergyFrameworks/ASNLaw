using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace test
{
    static class Global
    {
        public static bool IsNumeric(string value) // Added 01.15.2019
        {
            int result;
            if (int.TryParse(value, out result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool WriteStringToFile(string lines, string pathFile, bool DeleteIfAlreadyExists)
        {
            if (File.Exists(pathFile))
                if (DeleteIfAlreadyExists)
                    File.Delete(pathFile);
                else
                    return true;

            System.IO.StreamWriter file = new System.IO.StreamWriter(pathFile);
            file.WriteLine(lines);

            file.Close();

            return true;
        }

        public static bool isInternetConnected()
        {
            bool isAvailable = false;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.google.com");
            request.Timeout = 5000;
            request.Credentials = CredentialCache.DefaultNetworkCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("IsSIPServerAvailable: " + response.StatusCode);
                isAvailable = true;
            }

            return isAvailable;
        }

        public static bool DriveExists(string path, out string drive)
        {

            int x = path.IndexOf(":");
            if (x > 0 && path.Length > 2)
            {
                drive = path.Substring(0, 2);
                return Directory.Exists(@drive);
            }
            else
            {
                drive = "Error: No Drive Given";
                return false;
            }
        }




    }
}
