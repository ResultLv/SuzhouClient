using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using USBHIDControl;

namespace client.View
{
    /// <summary>
    /// mintenance.xaml 的交互逻辑
    /// </summary>
    public partial class mintenance : Window
    {
        public mintenance()
        {
            InitializeComponent();
        }

        public static USBHID usbHID = USBHID.GetInstance();
        public string getReturn(string commond)
        {
            usbHID.sendCommond(commond);
            return usbHID.status;
        }

        public string strToHex(String str)
        {
            string ret = null;
            char[] strarr = { };
            if (str == null) return null;
            strarr = str.ToCharArray();
            for (int i = 0; i < strarr.Length; i++)
            {
                int value = Convert.ToInt32(strarr[i]);
                string hexOutput = String.Format("{0:X}", value);
                ret = ret + hexOutput + " ";

            }
            //MessageBox.Show(ret);
            return ret;
        }

        private void read(object sender, RoutedEventArgs e)
        {
            string result = getReturn("10");
            if (result.Length > 1)
            {
                string[] res = result.Split(';');
                firstMaintenanceBox.Text = res[0].Split(':')[1];
                intervalBox.Text = res[1].Split(':')[1];

            }
            else
            {
                firstMaintenanceBox.Text = null;
                intervalBox.Text = null;
            }


        }
        private void set(object sender, RoutedEventArgs e)
        {
            int len = 8;
            string str1 = null, str2 = null;
            for (int i = 0; i < len - 1; i++)
                str1 += "0"; str2 += "0";
            str1 += firstMaintenanceBox.Text;
            str2 += intervalBox.Text;

            string commond = "0F " + strToHex(str1) + strToHex(str2);
            string result = getReturn(commond.Substring(0, commond.Length - 1));
            MessageBox.Show("设置成功");
        }
    }
}
