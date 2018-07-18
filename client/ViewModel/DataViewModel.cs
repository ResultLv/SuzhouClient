using client.Model;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace client.ViewModel
{
    class DataViewModel : ViewModel
    {
        ObservableCollection<Data> _datalist = new ObservableCollection<Data>();
        public ObservableCollection<Data> datalist
        {
            set { _datalist = value; RaisePropertyChanged("datalist"); }
            get { return datalistHandle(_datalist); }
        }
        public ObservableCollection<Data> datalistHandle(ObservableCollection<Data> _datalist)
        {
            int len = _datalist.Count;
            //MessageBox.Show(len.ToString());
            for (int i = 0; i < len; i++)
            {
                if (_datalist[i].Dis.Equals("1")) _datalist[i].Dis = "未拆除";
                if (_datalist[i].Dis.Equals("0")) _datalist[i].Dis = "已拆除";
            }
            return _datalist;

        }
        // 得到盘符
        static public string getDrive()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            for (int i = 0; i < allDrives.Length; i++)
            {
                Console.WriteLine(allDrives[i].Name);
            }
            return allDrives[allDrives.Length - 1].Name;
        }

        // 得到文件名
        public string[] getFileName()
        {
            string[] fileNames = Directory.GetFiles(getDrive());
            Console.WriteLine(fileNames[0]);
            return fileNames;
        }

        //  读取所有文件
        public List<List<string>> read()
        {
            string[] fileNames = getFileName();
            List<List<string>> allLine = new List<List<string>>();
            for (int j = 0; j < fileNames.Length; j++)
            {
                string filepath = fileNames[j];
                FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                StreamReader read = new StreamReader(fs, Encoding.Default);
                string strReadline;

                int ix = 1;
                while ((strReadline = read.ReadLine()) != null)
                {
                    List<string> eachvalue = new List<String>();
                    string[] tmp = strReadline.Split(';');
                    eachvalue.Add("" + (ix + j * 4));
                    ix = ix + 1;
                    for (int i = 1; i < tmp.Length; i++)
                    {
                        if (i == 1 || i == 3 || i == 4)
                        {
                            if (i == 1 || i == 3) eachvalue.Add(tmp[i].Substring(5));
                            if (i == 4) eachvalue.Add(tmp[i].Substring(6));
                        }
                        else
                        {
                            string[] tmp1 = tmp[i].Split(':');
                            if (i < tmp.Length - 1) eachvalue.Add(tmp1[1]);
                            if (i == tmp.Length - 1)
                            {
                                eachvalue.Add(tmp1[1].Substring(0,1));
                            }
                        }

                    }
                    allLine.Add(eachvalue);
                    // strReadline即为按照行读取的字符串
                    Console.WriteLine(strReadline);
                }
                fs.Close();
                read.Close();
            }
            return allLine;
        }

        // 读取所有数据
        public ICommand readAll
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    datalist.Clear();
                    List<List<string>> dataRead = read();
                    for (int i = 0; i < dataRead.Count; i++)
                    {
                        datalist.Add(new Data(int.Parse(dataRead[i][0]), dataRead[i][1], int.Parse(dataRead[i][2]), dataRead[i][3], dataRead[i][4], dataRead[i][5], dataRead[i][6], int.Parse(dataRead[i][7]), int.Parse(dataRead[i][8]), dataRead[i][9]));
                    }
                });
            }

        }

        // 读取近10条数据
        public ICommand read10
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    datalist.Clear();
                    List<List<string>> dataRead = read();
                    for (int i = dataRead.Count-10; i < dataRead.Count; i++)
                    {
                        datalist.Add(new Data(int.Parse(dataRead[i][0]), dataRead[i][1], int.Parse(dataRead[i][2]), dataRead[i][3], dataRead[i][4], dataRead[i][5], dataRead[i][6], int.Parse(dataRead[i][7]), int.Parse(dataRead[i][8]), dataRead[i][9]));
                    }
                });
            }

        }

        public DataViewModel()
        {
            List<List<string>> dataRead = read();
            for (int i = 0; i < dataRead.Count; i++)
            {
                datalist.Add(new Data(int.Parse(dataRead[i][0]), dataRead[i][1], int.Parse(dataRead[i][2]), dataRead[i][3], dataRead[i][4], dataRead[i][5], dataRead[i][6], int.Parse(dataRead[i][7]), int.Parse(dataRead[i][8]), dataRead[i][9]));
            }

            //datalist.Add(new Data("0000010", "2018 - 07 - 03 12:11:51", "0000021", "05:14", "00:00", "006 %", "000 %", "0000001", "00000", "1"));
            //datalist.Add(new Data("0000011", "2018 - 07 - 03 12:11:51", "0000021", "05:14", "00:00", "006 %", "000 %", "0000001", "00000", "1"));
            //datalist.Add(new Data("0000012", "2018 - 07 - 03 12:11:51", "0000021", "05:14", "00:00", "006 %", "000 %", "0000001", "00000", "1"));
            //datalist.Add(new Data("0000013", "2018 - 07 - 03 12:11:51", "0000021", "05:14", "00:00", "006 %", "000 %", "0000001", "00000", "1"));
        }
    }
}
