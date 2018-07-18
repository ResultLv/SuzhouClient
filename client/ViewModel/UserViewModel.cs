using client.Model;
using client.View;
using Prism.Commands;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using USBHIDControl;

namespace client.ViewModel
{
    class UserViewModel : ViewModel
    {
        // 声明USB实例
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

        // 声明Model实例
        private UserInfo userInfo;
        private DeviceInfo devInfo;
        // 判断登录是否失败的属性
        private bool isLoginFailed = true;

        // 登录成功通知窗口关闭
        public bool IsLoginFailed
        {
            get { return this.isLoginFailed; }
            set
            {
                if (this.isLoginFailed != value)
                {
                    this.isLoginFailed = value;
                    this.RaisePropertyChanged("IsLoginFailed");
                }
            }
        }

        public string ID
        {
            set { devInfo.ID = value; RaisePropertyChanged("ID"); }
            get { return devInfo.ID; }
        }

        public string UserName
        {
            set { userInfo.UserName = value; RaisePropertyChanged("UserName"); }
            get { return userInfo.UserName; }
        }
        public string PassWord
        {
            set { userInfo.PassWord = value; RaisePropertyChanged("PassWord"); }
            get { return userInfo.PassWord; }
        }
        // 构造函数
        public UserViewModel()
        {
            userInfo = new UserInfo();
            devInfo = new DeviceInfo();
        }
        /* -----绑定命令-------*/
        // Wifi列表选中事件
        public ICommand reset
        {
            get
            {
                return new DelegateCommand<PasswordBox>((pwBox) =>
                {
                    pwBox.Password = "";
                });
            }
        }

        public ICommand login
        {
            get
            {
                return new DelegateCommand<PasswordBox>((pwBox) =>
                {
                    string uName = UserName;
                    string pWord = pwBox.Password;
                    string commond = "04 02 " + pWord.Length.ToString("X2") + " " + strToHex(uName) + uName.Length.ToString("X2") + " " + strToHex(pWord);
                    string result = getReturn(commond.Substring(0, commond.Length-1)).Split(':')[1];
                    if (result == "0")
                    {
                        MessageBox.Show("登录成功");
                        IsLoginFailed = false;
                        USBHID.isOpened = false;
                        // 将USB-HID设备枚举为MASS存储器
                    }
                    else MessageBox.Show("登录失败，用户名或密码不正确");
                });
            }
        }

    }
}