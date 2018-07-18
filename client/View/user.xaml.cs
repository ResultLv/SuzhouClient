using client.ViewModel;
using System;
using System.Windows;
using USBHIDControl;

namespace client.View
{
    /// <summary>
    /// user.xaml 的交互逻辑
    /// </summary>
    public partial class user : Window
    {
        public user()
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

        private void reset(object sender, RoutedEventArgs e)
        {
            this.pw1.Password = null;
            this.pw2.Password = null;
        }
        private void set(object sender, RoutedEventArgs e)
        {
            string uName = userName.Text;
            string pWord1 = pw1.Password;
            string pWord2 = pw2.Password;
            if (uName == "" || pWord1 == "" || pWord2 == "") MessageBox.Show("用户名或密码不能为空");
            else if (this.pw1.Password.Equals(this.pw2.Password))
            {
                string res = "1";
                string commond = "04 01 " + uName.Length.ToString("X2") + " " + strToHex(uName) + pWord1.Length.ToString("X2") + " " + strToHex(pWord1);
                string[] result = getReturn(commond.Substring(0, commond.Length - 1)).Split(';');
                result = getReturn(commond.Substring(0, commond.Length - 1)).Split(';');
                if (result.Length > 1) res = result[2].Substring(5, 1);
                else res = result[0].Substring(5, 1);
                if (res == "0") MessageBox.Show("设置成功");
                else if (res == "2") MessageBox.Show("执行失败，用户名含非法字符");
                else if (res == "3") MessageBox.Show("执行失败，密码含非法字符");
                else MessageBox.Show("执行失败");
                this.Close();
            }
            else MessageBox.Show("两次输入密码不一致");
        }
    }
}
