using client.Model;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using USBHIDControl;

namespace client.ViewModel
{
    public class ViewModel : INotifyPropertyChanged // 实现INotifyPropertyChanged接口，通知界面更新
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if(propertyName != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    class DeviceViewModel : ViewModel
    {
        // 声明USB实例
        public static USBHID usbHID = USBHID.GetInstance();
        public string getReturn(string commond)
        {
            usbHID.sendCommond(commond);
            Status = usbHID.status;
            return Status;
            //return usbHID.status;
        }


        // 声明Model实例
        private DeviceInfo devInfo;
        private DeviceStatus devStatus;
        private Recived recived;

        public string Status
        {
            set { recived.Status = value; RaisePropertyChanged("Status"); }
            get { return recived.Status; }
        }

        // 设备信息
        public string ID
        {
            set { devInfo.ID = value; RaisePropertyChanged("ID"); }
            get { return devInfo.ID; }
        }
        public string sRev
        {
            set { devInfo.sRev = value; RaisePropertyChanged("sRev"); }
            get { return devInfo.sRev; }
        }
        public string hRev
        {
            set { devInfo.hRev = value; RaisePropertyChanged("hRev"); }
            get { return devInfo.hRev; }
        }
        public string cmp
        {
            set { devInfo.cmp = value; RaisePropertyChanged("cmp"); }
            get { return devInfo.cmp; }
        }

        // 设备状态
        public string AP
        {
            set { devStatus.AP = value; RaisePropertyChanged("AP"); }
            get
            {
                if (devStatus.AP == null) return null;
                if (devStatus.AP.Equals("0")) return "未连接";
                if (devStatus.AP.Equals("1")) return "已连接";
                if (devStatus.AP.Equals("2")) return "故障";
                return null;
            }
        }
        public string Svr
        {
            set { devStatus.Svr = value; RaisePropertyChanged("Svr"); }
            get
            {
                if (devStatus.Svr == null) return null;
                if (devStatus.Svr.Equals("0")) return "从未连接成功";
                if (devStatus.Svr.Equals("1")) return "曾经连接成功";
                return null;
            }
        }
        public string Batt
        {
            set { devStatus.Batt = value; RaisePropertyChanged("Batt"); }
            get
            {
                if (devStatus.Batt == null) return null;
                int temp = int.Parse(devStatus.Batt);
                decimal res = System.Math.Round((decimal)temp / 100, 2);
                return res.ToString() + " V";

            }
        }
        public string Adh
        {
            set { devStatus.Adh = value; RaisePropertyChanged("Adh"); }
            get
            {
                if (devStatus.Adh == null) return null;
                if (devStatus.Adh.Equals("0")) return "附着状态";
                if (devStatus.Adh.Equals("1")) return "已拆除";
                return null;
            }
        }
        public string Disarm
        {
            set { devStatus.Disarm = value; RaisePropertyChanged("Disarm"); }
            get
            {
                if (devStatus.Disarm == null) return null;
                int temp = int.Parse(devStatus.Disarm);
                return temp.ToString() + " 次";
            }
        }
        public string DtTm
        {
            set { devStatus.DtTm = value; RaisePropertyChanged("DtTm"); }
            get { return devStatus.DtTm; }
        }
        public string CardCap
        {
            set { devStatus.CardCap = value; RaisePropertyChanged("CardCap"); }
            get
            {
                if (devStatus.CardCap == null) return null;
                int temp = int.Parse(devStatus.CardCap) / 1024 / 1024;
                return temp.ToString() + " MB";
            }
        }


        // 构造函数
        public DeviceViewModel()
        {
            devInfo = new DeviceInfo();
            devStatus = new DeviceStatus();
            recived = new Recived();
        }

        public ICommand PageLoaded
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    string recived01 = getReturn("01");
                    string recived02 = getReturn("02");
                    string recived05 = getReturn("05");
                    string recived0E = getReturn("0E");
                    if (recived01 != "" && recived01 != null)
                    {
                        string[] str01 = recived01.Split(';');
                        int len = str01.Length;
                        for (int i = 0; i < len; i++)
                        {
                            str01[i] = str01[i].Split(':')[1];
                        }
                        if (len >= 1) ID = str01[0];
                        if (len >= 2) sRev = str01[1];
                        if (len >= 3) hRev = str01[2];
                        if (len >= 4) cmp = str01[3].Substring(0, str01[3].Length - 1);
                    }
                    if (recived02 != "" && recived02 != null)
                    {
                        string[] str02 = recived02.Split(';');
                        int len = str02.Length;
                        if (len != 4)
                            if (len >= 1) DtTm = str02[0].Substring(5);
                            if (len == 2) CardCap = str02[1].Split(':')[1].Substring(0, 9);
                    }
                    if (recived05 != "" && recived05 != null)
                    {
                        string[] str05 = recived05.Split(';');
                        int len = str05.Length;
                        for (int i = 0; i < len; i++)
                        {
                            str05[i] = str05[i].Split(':')[1];
                        }
                        if (len >= 1) Disarm = "0";
                    }
                    if (recived0E != "" && recived0E != null)
                    {
                        string[] str0E = recived0E.Split(';');
                        int len = str0E.Length;
                        for (int i = 0; i < len; i++)
                        {
                            str0E[i] = str0E[i].Split(':')[1];
                        }
                        if (len >= 1) AP = str0E[0];
                        if (len >= 2) Svr = str0E[1];
                        if (len >= 3) Batt = str0E[2];
                        if (len >= 4) Adh = str0E[3].Substring(0, str0E[3].Length - 1);
                    }
                });
            }
        }

        public ICommand timeCorrection  // 时间校正
        {
            get
            {
                return new DelegateCommand(() =>    //使用Prism框架实现的DelegateCommand类
                {
                    if (USBHID.isOpened)
                    {
                        string now = DateTime.Now.ToString("yy MM dd hh mm ss");
                        string hour = DateTime.Now.Hour.ToString();
                        string result = getReturn("03 " + now.Substring(0, 9) + hour + now.Substring(11));
                        DtTm = result.Substring(5, result.Length - 6);
                    }
                    else MessageBox.Show("USB未插入或已变为外存");
                });
            }
        }
    }
}
