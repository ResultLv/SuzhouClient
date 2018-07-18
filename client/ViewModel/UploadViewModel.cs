using client.Model;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using USBHIDControl;

namespace client.ViewModel
{
    class UploadViewModel : ViewModel
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
        private ServerInfo serInfo;
        private WifiSet wifiSet;
        private Recived recived;

        public string Status
        {
            set { recived.Status = value; RaisePropertyChanged("Status"); }
            get { return recived.Status; }
        }

        // 服务器信息
        public string IP
        {
            set { serInfo.IP = value; RaisePropertyChanged("IP"); }
            get { return serInfo.IP; }
        }
        public string Port
        {
            set { serInfo.Port = value; RaisePropertyChanged("Port"); }
            get { return serInfo.Port; }
        }

        public string WifiName
        {
            set { wifiSet.WifiName = value; RaisePropertyChanged("WifiName"); }
            get { return wifiSet.WifiName; }
        }
        public string WifiPW
        {
            set { wifiSet.WifiPW = value; RaisePropertyChanged("WifiPW"); }
            get { return wifiSet.WifiPW; }
        }

        // WIFI列表数据源
        ObservableCollection<WifiInfo> _datalsit = new ObservableCollection<WifiInfo>();
        public ObservableCollection<WifiInfo> datalist
        {
            get { return datalistHandle(_datalsit); }
            set { _datalsit = value; RaisePropertyChanged("datalist"); }
        }
        public ObservableCollection<WifiInfo> datalistHandle(ObservableCollection<WifiInfo> _datalist)
        {
            int len = _datalist.Count;
            for(int i = 0; i < len; i++)
            {
                if (_datalsit[i].WifiEnc.Equals("0")) _datalsit[i].WifiEnc = "开放";
                if (_datalsit[i].WifiEnc.Equals("1")) _datalsit[i].WifiEnc = "WEP";
                if (_datalsit[i].WifiEnc.Equals("2")) _datalsit[i].WifiEnc = "WPA_PSK";
                if (_datalsit[i].WifiEnc.Equals("3")) _datalsit[i].WifiEnc = "WPA_2PSK";
                if (_datalsit[i].WifiEnc.Equals("4")) _datalsit[i].WifiEnc = "WPA_WPA2_PSK";
                string temp = " dBm";
                if (!_datalsit[i].ChannelIntensity.Contains(temp))
                {
                    _datalsit[i].ChannelIntensity += temp;
                }
            }
            return _datalist;
        }

        // 构造函数
        public UploadViewModel()
        {
            serInfo = new ServerInfo();
            wifiSet = new WifiSet();
            recived = new Recived();

            datalist.Add(new WifiInfo("WHU", "2", "78", "0c:82:68:bf:01:16", 11));
            datalist.Add(new WifiInfo("CS-Graduate", "3", "90", "0b:1a:68:cf:41:32", 7));
            datalist.Add(new WifiInfo("ABC", "4", "72", "44:45:53:54:00:00", 9));
            datalist.Add(new WifiInfo("CMCC", "1", "84", "08:00:20:0A:8C:6D", 1));
            datalist.Add(new WifiInfo("JP", "0", "89", " 00:22:15:4c:5d:42", 3));
            datalist.Add(new WifiInfo("CS-Faculty", "2", "86", " 14:0a:f3:4f:21:5c", 6));

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
        public string intToHex(string num)
        {
            string str = null;
            string ret = null;

            str = int.Parse(num).ToString("X2");
            if (str.Length < 4) str = "0" + str;
            for (int i = str.Length; i >= 2; i--)
            {
                if(i%2 == 0)
                {
                    ret += str.Substring(i-2, 2) + " ";
                }
            }
            //MessageBox.Show(ret);
            return ret;
        }

        //-------------------以下为绑定命令--------------------------
        // Wifi列表选中事件
        public ICommand SelectionChanged
        {
            get
            {
                return new DelegateCommand<ListView>((listview) =>
                {
                    WifiInfo wifi = listview.SelectedItem as WifiInfo;
                    if (wifi == null) return;
                    WifiName = wifi.WifiName;
                });
            }
        }
        // 刷新Wifi列表,重新加载datalist;
        public ICommand refreshList
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (USBHID.isOpened)
                    {
                        datalist.Clear();
                        datalist.Add(new WifiInfo("WHU", "2", "78", "0c:82:68:bf:01:16", 11));
                        datalist.Add(new WifiInfo("CS-Graduate", "3", "90", "0b:1a:68:cf:41:32", 7));
                        datalist.Add(new WifiInfo("JP", "0", "89", " 00:22:15:4c:5d:42", 3));
                        datalist.Add(new WifiInfo("CS-Faculty", "2", "86", " 14:0a:f3:4f:21:5c", 6));
                        //int apCount = 0;
                        //string[] result = getReturn("08 00").Split(':');
                        //if (result[0].Equals("QtyAP")) apCount = int.Parse(result[1]);
                        //else
                        //{
                        //    MessageBox.Show("WIFI故障");
                        //    return;
                        //}
                        //for (int i = 1; i <= apCount; i++)
                        //{
                        //    string[] paras = getReturn("08 " + i.ToString("X2")).Split(',');
                        //    string encrypt = paras[0].Split(':')[1];
                        //    string name = paras[1].Substring(1, paras[1].Length - 1);
                        //    string strength = paras[2];
                        //    string mac = paras[3].Substring(1, paras[3].Length - 1);
                        //    int channel = int.Parse(paras[4]);
                        //    datalist.Add(new WifiInfo(name, encrypt, strength, mac, channel));
                        //}

                        //string[] paras = "Numb01:4,\"ABC\",-87,\"0c: 82:68:bf: 01:16\",1".Split(',');
                        //string encrypt = paras[0].Split(':')[1];
                        //string name = paras[1].Substring(1, paras[1].Length - 2);
                        //string strength = paras[2];
                        //string mac = paras[3].Substring(1, paras[3].Length - 2);
                        //int channel = int.Parse(paras[4]);
                        //datalist.Add(new WifiInfo(name, encrypt, strength, mac, channel));

                        datalist.Add(new WifiInfo("CS-Faculty", "2", "86", " 14:0a:f3:4f:21:5c", 6));
                        datalist.Remove(datalist[datalist.Count - 1]);
                    }
                    else MessageBox.Show("USB未插入或已变为外存");
                });
            }
        }
        // 查看当前Wifi连接
        public ICommand checkConnect
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (USBHID.isOpened)
                    {
                        string[] res = getReturn("0A").Split(';');
                        int len = res.Length;
                        WifiName = res[0].Split(':')[1];
                        string pw = res[1].Split(':')[1];
                        if (pw != null)
                            WifiPW = pw.Substring(0, pw.Length - 1);
                        else WifiPW = null;
                    }
                    else MessageBox.Show("USB未插入或已变为外存");
                });
            }
        }
        // 设置Wifi
        public ICommand connectWifi
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (USBHID.isOpened)
                    {
                        // 向设备写入WIFI名和密码
                        if (WifiName == null || WifiName == "") MessageBox.Show("请选择WIFI名");
                        else
                        {
                            string commond = null;
                            if (WifiPW == null)
                            {
                                WifiPW = "";
                                commond = "09 " + WifiName.Length.ToString("X2") + " " + WifiPW.Length.ToString("X2") + " " + strToHex(WifiName);
                            }
                            else commond = "09 " + WifiName.Length.ToString("X2") + " " + WifiPW.Length.ToString("X2") + " " + strToHex(WifiName) + strToHex(WifiPW);
                            //MessageBox.Show(commond);
                            string result = getReturn(commond.Substring(0, commond.Length - 1));
                            if (result != null)
                                MessageBox.Show("已成功设置WIFI密码");
                        }
                    }else MessageBox.Show("USB未插入或已变为外存");

                });
            }
        }

        // 读取服务器信息
        public ICommand readServer
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (USBHID.isOpened)
                    {
                        string recived07 = getReturn("07");
                        if (recived07 != "" && recived07 != null)
                        {
                            string[] str07 = recived07.Split(';');
                            int len = str07.Length;
                            for (int i = 0; i < len; i++)
                            {
                                str07[i] = str07[i].Split(':')[1];
                            }
                            if (len >= 1) IP = str07[0];
                            if (len >= 2) Port = str07[1].Substring(0, str07[1].Length - 1);
                        }
                    }else MessageBox.Show("USB未插入或已变为外存");
                });
            }
        }
        // 设置服务器信息
        public ICommand setServer
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (USBHID.isOpened)
                    {
                        if (IP == null || Port == null) MessageBox.Show("IP地址或端口号不能为空");
                        else
                        {
                            try
                            {
                                int temp = int.Parse(Port);
                                if (temp < 1024 || temp > 60000)
                                {
                                    MessageBox.Show("端口号超出界限");
                                    return;
                                }
                                string setInfo = strToHex(IP) + intToHex(Port);
                                string ipLen = IP.Length.ToString("X2") + " ";
                                string result = getReturn("06 " + ipLen + setInfo.Substring(0, setInfo.Length - 1));
                                int Rslt = 0;
                                string message = null;
                                try
                                {
                                    Rslt = int.Parse(result.Substring(5, 1));
                                    if (Rslt == 4) message = "域名或IP地址长度超过32个字符";
                                    if (Rslt == 5) message = "端口号超出界限";
                                    if (Rslt == 6) message = "域名中包含非法字符";
                                }
                                catch { }
                                if (Rslt == 0) message = "已设置成功";
                                MessageBox.Show(message);
                            }
                            catch (System.OverflowException)
                            {
                                MessageBox.Show("端口号超出界限");
                            }
                            catch (System.FormatException)
                            {
                                MessageBox.Show("端口号只能包含数字");
                            }
                        }
                    }else MessageBox.Show("USB未插入或已变为外存");
                });
            }
        }

        // 页面加载事件
        public ICommand PageLoaded
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    //int apCount = 0;
                    //string[] result = getReturn("08 00").Split(':');
                    //if (result[0].Equals("QtyAP")) apCount = int.Parse(result[1]);
                    //else
                    //{
                    //    MessageBox.Show("WIFI故障");
                    //    return;
                    //}
                    //for (int i = 1; i <= apCount; i++)
                    //{
                    //    string[] paras = getReturn("08 " + i.ToString("X2")).Split(',');
                    //    string encrypt = paras[0].Split(':')[1];
                    //    string name = paras[1].Substring(1, paras[1].Length - 1);
                    //    string strength = paras[2];
                    //    string mac = paras[3].Substring(1, paras[3].Length - 1);
                    //    int channel = int.Parse(paras[4]);
                    //    datalist.Add(new WifiInfo(name, encrypt, strength, mac, channel));
                    //}

                    //datalist.Add(new WifiInfo("CS-Faculty", "2", "86", " 14:0a:f3:4f:21:5c", 6));
                    //datalist.Remove(datalist[datalist.Count - 1]);
                });
            }

        }

    }
}
