using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleDriveManager
{
    class GoogleUser
    {
        public string userName { get; set; }
        public string clientSecretPath { get; set; }

        public GoogleUser(string name, string path)
        {
            userName = name;
            clientSecretPath = path;
        }
    }
}
