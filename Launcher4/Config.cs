using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Launcher4
{
    class Config
    {
        public static string siteAddress    = "80.235.132.198";      // site ip or domain NO "http://"
        public static int sitePort          = 80;                    // site port

        public static string launcher_path = "http://" + siteAddress + "/launcher4";

        public static string realmlist      = "localhost";
        public static int worldPort         = 8085;
    }
}
