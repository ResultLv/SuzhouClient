using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.Model
{
    class WifiSet
    {
        string _WifiName;
        string _WifiPW;

        public WifiSet()
        {

        }

        public WifiSet(string WifiName, string WifiPW)
        {
            this.WifiName = WifiName;
            this.WifiPW = WifiPW;
        }

        public string WifiName { get => _WifiName; set => _WifiName = value; }
        public string WifiPW { get => _WifiPW; set => _WifiPW = value; }
    }
}
