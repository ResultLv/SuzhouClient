using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32.SafeHandles;
using SP_DEVICE_INTERFACE_DATA = USBHIDControl.WindowsAPI.SP_DEVICE_INTERFACE_DATA;
using SP_DEVICE_INTERFACE_DETAIL_DATA = USBHIDControl.WindowsAPI.SP_DEVICE_INTERFACE_DETAIL_DATA;

namespace USBHIDControl
{
    public class USBHID
    {
        private string vidpid = "vid_" + 3333 + "&pid_" + 6666;
        public static bool isOpened = false;
        public string status = null;

        private int outputReportLength;
        private int inputReportLength;

        private FileStream hidDevice; 
        private const int MAX_USB_DEVICES = 64;
        WindowsAPI windowsApi = new WindowsAPI();

        // 定义一个静态变量来保存类的实例
        private static USBHID uniqueInstance;

        // 定义一个标识确保线程同步
        private static readonly object locker = new object();

        public static USBHID GetInstance()
        {
            // 当第一个线程运行到这里时，此时会对locker对象 "加锁"，
            // 当第二个线程运行该方法时，首先检测到locker对象为"加锁"状态，该线程就会挂起等待第一个线程解锁
            // lock语句运行完之后（即线程运行完之后）会对该对象"解锁"
            // 双重锁定只需要一句判断就可以了
            if (uniqueInstance == null)
            {
                lock (locker)
                {
                    // 如果类的实例不存在则创建，否则直接返回
                    if (uniqueInstance == null)
                    {
                        uniqueInstance = new USBHID();
                    }
                }
            }
            return uniqueInstance;
        }


        public bool openUSB()
        {
            List<String> deviceList = GetDeviceList();
            string vidpid = "vid_" + 3333 + "&pid_" + 6666;
            for (int i = 0; i < deviceList.Count; i++)
            {
                if (deviceList[i].IndexOf(vidpid) >= 0)
                {
                    isOpened = OpenUSBHid(deviceList[i].ToString());
                    if (isOpened != true)
                    {
                        Console.WriteLine("打开成功");
                        isOpened = true; 
                    }
                }
            }
            if(DataReceived == null) DataReceived += usbHID_DataReceived;
            return isOpened;
        }
        public string sendCommond(string commond)
        {
            //DataReceived += usbHID_DataReceived;
            WriteUSBHID(commond);
            Thread.Sleep(100);
            return status;
        }
        void usbHID_DataReceived(object sender, EventArgs e)
        {
            report myRP = (report)e;
            int i = 0;
            string str = null;
            while (i < myRP.reportBuff.Length)
            {
                if ((char)myRP.reportBuff[i] != '\0')
                    str += (char)myRP.reportBuff[i];
                i++;
            }
            status = str;
            Console.WriteLine(str);
        }

        private  List<String> deviceList = new List<string>();
        public List<String> GetDeviceList() {
            return deviceList;
        }

        private USBHID() {
            GetDeviceList(ref deviceList);
        }

        public bool OpenUSBHid(string deviceStr)
        {
            //创建，打开设备文件
            IntPtr device = windowsApi.CreateDeviceFile(deviceStr);
            if (device == new IntPtr(-1))
                return false;

            HIDD_ATTRIBUTES attributes;
            windowsApi.GETDeviceAttribute(device, out attributes);

            //找到相对应的HID设备信息
            IntPtr preparseData;
            HIDP_CAPS caps;
            windowsApi.GetPreparseData(device, out preparseData);
            windowsApi.GetCaps(preparseData, out caps);
            windowsApi.FreePreparseData(preparseData);
            outputReportLength = caps.OutputReportByteLength;
            inputReportLength = caps.InputReportByteLength;

            hidDevice = new FileStream(new SafeFileHandle(device, false), FileAccess.ReadWrite, inputReportLength, true);

            BeginAsyncRead();
            return true;
        }

        private void BeginAsyncRead()
        {
            byte[] inputBuff = new byte[inputReportLength];
            hidDevice.BeginRead(inputBuff, 0, inputReportLength, new AsyncCallback(ReadCompleted), inputBuff);
        }


        /// <summary>
        /// 异步读取结束,发出有数据到达事件
        /// </summary>
        /// <param name="iResult">这里是输入报告的数组</param>
        private void ReadCompleted(IAsyncResult iResult)
        {
            byte[] readBuff = (byte[])(iResult.AsyncState);
            //MessageBox.Show(iResult.AsyncState.ToString());
            try
            {
                hidDevice.EndRead(iResult);//读取结束,如果读取错误就会产生一个异常
                byte[] reportData = new byte[readBuff.Length - 1];
                for (int i = 1; i < readBuff.Length; i++)
                    reportData[i - 1] = readBuff[i];
                report e = new report(readBuff[0], reportData);
                OnDataReceived(e); //发出数据到达消息
                BeginAsyncRead();//启动下一次读操作
            }
            catch (IOException)//读写错误,设备已经被移除
            {
                EventArgs ex = new EventArgs();
                OnDeviceRemoved(ex);//发出设备移除消息
                CloseDevice();
            }
        }

        /// <summary>
        /// 事件:数据到达,处理此事件以接收输入数据
        /// </summary>
        public event EventHandler DataReceived;
        protected virtual void OnDataReceived(EventArgs e)
        {
            if (DataReceived != null) DataReceived(this, e);
        }

        /// <summary>
        /// 事件:设备断开
        /// </summary>
        public event EventHandler DeviceRemoved;
        protected virtual void OnDeviceRemoved(EventArgs e)
        {
            if (DeviceRemoved != null) DeviceRemoved(this, e);
        }

        /// <summary>
        /// 关闭打开的设备
        /// </summary>
        public void CloseDevice()
        {
            hidDevice.Close();
        }

        /// <summary>
        /// 获取所有连接HID的设备
        /// </summary>
        /// <param name="deviceList">返回所有连接HID的设备</param>
        private void GetDeviceList(ref List<string> deviceList)
        {
            Guid HIDGuid = Guid.Empty;
            windowsApi.GetDeviceGuid(ref HIDGuid);//获取HID的全局GUID
            IntPtr HIDInfoSet = windowsApi.GetClassDevOfHandle(HIDGuid);//获取包含所有HID接口信息集合的句柄

            if (HIDInfoSet != IntPtr.Zero)
            {
                SP_DEVICE_INTERFACE_DATA interfaceInfo = new SP_DEVICE_INTERFACE_DATA();
                interfaceInfo.cbSize = Marshal.SizeOf(interfaceInfo);

                //检测集合的每个接口
                for (uint index = 0; index < MAX_USB_DEVICES; index++) {
                    //获取接口信息
                    if (!windowsApi.GetEnumDeviceInterfaces(HIDInfoSet, ref HIDGuid, index, ref interfaceInfo))
                        continue;
                    
                    int buffsize=0;
                    //获取接口详细信息；第一次读取错误，但可取得信息缓冲区的大小
                    windowsApi.GetDeviceInterfaceDetail(HIDInfoSet,ref interfaceInfo,IntPtr.Zero, ref buffsize);
                    
                    //接受缓冲
                    IntPtr pDetail = Marshal.AllocHGlobal(buffsize);
                    SP_DEVICE_INTERFACE_DETAIL_DATA detail = new WindowsAPI.SP_DEVICE_INTERFACE_DETAIL_DATA();
                    detail.cbSize = Marshal.SizeOf(typeof(USBHIDControl.WindowsAPI.SP_DEVICE_INTERFACE_DETAIL_DATA));
                    Marshal.StructureToPtr(detail, pDetail, false);
                    if (windowsApi.GetDeviceInterfaceDetail(HIDInfoSet, ref interfaceInfo, pDetail, ref buffsize))//第二次读取接口详细信息
                        deviceList.Add(Marshal.PtrToStringAuto((IntPtr)((int)pDetail + 4)));
                    Marshal.FreeHGlobal(pDetail);
                }
            }

            //删除设备信息并释放内存
            windowsApi.DestroyDeviceInfoList(HIDInfoSet);
        }

        /// <summary>
        /// 读取信息
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        internal static string ByteToHexString(byte[] p)
        {
            string str = string.Empty;
            if (p != null)
            {
                for (int i = 0; i < p.Length; i++)
                {
                    str += p[i].ToString("X2");
                }
            }
            return str;
        }

        /// <summary>
        /// 写入设备
        /// </summary>
        /// <param name="sendValue"></param>
        /// <returns></returns>
        internal string WriteUSBHID(string sendValue)
        {
            try
            {
                string[] strList = Regex.Split(sendValue, " ", RegexOptions.IgnoreCase);
                byte[] arr = new byte[outputReportLength];
                for (int i = 0; i < strList.Length; i++)
                {
                    arr[i + 1] = (byte)Int32.Parse(strList[i], System.Globalization.NumberStyles.HexNumber);
                }
                hidDevice.Write(arr, 0, arr.Length);
                //for(int i  = 0; i < arr.Length; i++)
                //{
                //    Console.Write(arr[i]);
                //}
                
                return sendValue;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
    /// <summary>
    /// The HIDD_ATTRIBUTES structure contains vendor information about a HIDClass device
    /// </summary>
    public struct HIDD_ATTRIBUTES
    {
        public int Size;
        public ushort VendorID;
        public ushort ProductID;
        public ushort VersionNumber;
    }

    public struct HIDP_CAPS
    {
        public ushort Usage;
        public ushort UsagePage;
        public ushort InputReportByteLength;
        public ushort OutputReportByteLength;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public ushort[] Reserved;
        public ushort NumberLinkCollectionNodes;
        public ushort NumberInputButtonCaps;
        public ushort NumberInputValueCaps;
        public ushort NumberInputDataIndices;
        public ushort NumberOutputButtonCaps;
        public ushort NumberOutputValueCaps;
        public ushort NumberOutputDataIndices;
        public ushort NumberFeatureButtonCaps;
        public ushort NumberFeatureValueCaps;
        public ushort NumberFeatureDataIndices;
    }
    public class report : EventArgs
    {
        public readonly byte reportID;
        public readonly byte[] reportBuff;
        public report(byte id, byte[] arrayBuff)
        {
            reportID = id;
            reportBuff = arrayBuff;
        }
    }
}
