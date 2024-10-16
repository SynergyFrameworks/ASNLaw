using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace ProfessionalDocAnalyzer
{
    public class LastestRelease
    {
        private string _LatestRelease = string.Empty;
        private string _CurrentRelease = string.Empty;

        public string LatestRelease
        {
            get { return _LatestRelease; }
        }

        public string CurrentRelease
        {
            get { return _CurrentRelease; }
        }

        public bool IsLatestReleaseNewer()
        {
            bool results = false;

            try
            {
                _LatestRelease = GetLatestRelease();

                if (_LatestRelease.Length == 0) // This will occur if there isn't any internet connection or if the web server is down
                    return results;

                _CurrentRelease = Application.ProductVersion;

                Version verLatestRelease = new Version(_LatestRelease);
                Version verCurrentRelease = new Version(_CurrentRelease);

                var result = verLatestRelease.CompareTo(verCurrentRelease);
                if (result > 0)
                    results = true;

            }
            catch
            {
                return results;
            }

            return results;
        }

        public string GetLatestRelease()
        {
            String content = string.Empty;

            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(@"http://www.atebionllc.com/Downloads/ProDANELatestRelease.txt");
                StreamReader reader = new StreamReader(stream);
                content = reader.ReadToEnd();
            }
            catch
            {
                return content;
            }

            return content.Trim();
        }

        public string ReleasesLog()
        {
            String content = string.Empty;

            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(@"http://www.atebionllc.com/Downloads/ProDANEReleasesLog.txt");
                StreamReader reader = new StreamReader(stream);
                content = reader.ReadToEnd();

                return content.Trim();
            }
            catch
            {
                return content;
            }
        }
    }
}
