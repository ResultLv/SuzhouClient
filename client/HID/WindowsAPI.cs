using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace USBHIDControl
{
    public class WindowsAPI
    {
        /// <summary>
        /// The HidD_GetHidGuid routine returns the device interface GUID for HIDClass devices.
        /// </summary>
        /// <param name="HidGuid">a caller-allocated GUID buffer that the routine uses to return the device interface GUID for HIDClass devices.</param>
        [DllImport("hid.dll")]
        private static extern void HidD_GetHidGuid(ref Guid HidGuid);

        /// <summary>
        /// The SetupDiGetClassDevs function returns a handle to a device information set that contains requested device information elements for a local machine. 
        /// </summary>
        /// <param name="ClassGuid">GUID for a device setup class or a device interface class. </param>
        /// <param name="Enumerator">A pointer to a NULL-terminated string that supplies the name of a PnP enumerator or a PnP device instance identifier. </param>
        /// <param name="HwndParent">A handle of the top-level window to be used for a user interface</param>
        /// <param name="Flags">A variable  that specifies control options that filter the device information elements that are added to the device information set.
        /// </param>
        /// <returns>a handle to a device information set </returns>
        [DllImport("setupapi.dll", SetLastError = true)]
        private static extern IntPtr SetupDiGetClassDevs(ref Guid ClassGuid, uint Enumerator, IntPtr HwndParent, USBHIDEnum.DIGCF Flags);
        
        /// <summary>
        /// The SetupDiEnumDeviceInterfaces function enumerates the device interfaces that are contained in a device information set. 
        /// </summary>
        /// <param name="deviceInfoSet">A pointer to a device information set that contains the device interfaces for which to return information</param>
        /// <param name="deviceInfoData">A pointer to an SP_DEVINFO_DATA structure that specifies a device information element in DeviceInfoSet</param>
        /// <param name="interfaceClassGuid">a GUID that specifies the device interface class for the requested interface</param>
        /// <param name="memberIndex">A zero-based index into the list of interfaces in the device information set</param>
        /// <param name="deviceInterfaceData">a caller-allocated buffer that contains a completed SP_DEVICE_INTERFACE_DATA structure that identifies an interface that meets the search parameters</param>
        /// <returns></returns>
        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern Boolean SetupDiEnumDeviceInterfaces(IntPtr deviceInfoSet, IntPtr deviceInfoData, ref Guid interfaceClassGuid, UInt32 memberIndex, ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData);
        /// <summary>
        /// SP_DEVICE_INTERFACE_DATA structure defines a device interface in a device information set.
        /// </summary>
        public struct SP_DEVICE_INTERFACE_DATA
        {
            public int cbSize;
            public Guid interfaceClassGuid;
            public int flags;
            public int reserved;
        }

        /// <summary>
        /// The SetupDiGetDeviceInterfaceDetail function returns details about a device interface.
        /// </summary>
        /// <param name="deviceInfoSet">A pointer to the device information set that contains the interface for which to retrieve details</param>
        /// <param name="deviceInterfaceData">A pointer to an SP_DEVICE_INTERFACE_DATA structure that specifies the interface in DeviceInfoSet for which to retrieve details</param>
        /// <param name="deviceInterfaceDetailData">A pointer to an SP_DEVICE_INTERFACE_DETAIL_DATA structure to receive information about the specified interface</param>
        /// <param name="deviceInterfaceDetailDataSize">The size of the DeviceInterfaceDetailData buffer</param>
        /// <param name="requiredSize">A pointer to a variable that receives the required size of the DeviceInterfaceDetailData buffer</param>
        /// <param name="deviceInfoData">A pointer buffer to receive information about the device that supports the requested interface</param>
        /// <returns></returns>
        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool SetupDiGetDeviceInterfaceDetail(IntPtr deviceInfoSet, ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData, IntPtr deviceInterfaceDetailData, int deviceInterfaceDetailDataSize, ref int requiredSize, SP_DEVINFO_DATA deviceInfoData);
        
        /// <summary>
        /// SP_DEVINFO_DATA structure defines a device instance that is a member of a device information set.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class SP_DEVINFO_DATA
        {
            public int cbSize = Marshal.SizeOf(typeof(SP_DEVINFO_DATA));
            public Guid classGuid = Guid.Empty; // temp
            public int devInst = 0; // dumy
            public int reserved = 0;
        }

        /// <summary>
        /// SP_DEVICE_INTERFACE_DETAIL_DATA structure contains the path for a device interface.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        internal struct SP_DEVICE_INTERFACE_DETAIL_DATA
        {
            internal int cbSize;
            internal short devicePath;
        }

        /// <summary>
        /// The SetupDiDestroyDeviceInfoList function deletes a device information set and frees all associated memory.
        /// </summary>
        /// <param name="DeviceInfoSet">A handle to the device information set to delete.</param>
        /// <returns>returns TRUE if it is successful. Otherwise, it returns FALSE </returns>
        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern Boolean SetupDiDestroyDeviceInfoList(IntPtr deviceInfoSet);

        /// <summary>
        /// This function creates, opens, or truncates a file, COM port, device, service, or console. 
        /// </summary>
        /// <param name="fileName">a null-terminated string that specifies the name of the object</param>
        /// <param name="desiredAccess">Type of access to the object</param>
        /// <param name="shareMode">Share mode for object</param>
        /// <param name="securityAttributes">Ignored; set to NULL</param>
        /// <param name="creationDisposition">Action to take on files that exist, and which action to take when files do not exist</param>
        /// <param name="flagsAndAttributes">File attributes and flags for the file</param>
        /// <param name="templateFile">Ignored</param>
        /// <returns>An open handle to the specified file indicates success</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        //private static extern IntPtr CreateFile(string fileName, uint desiredAccess, uint shareMode, uint securityAttributes, uint creationDisposition, uint flagsAndAttributes, uint templateFile);
        static extern IntPtr CreateFile(
       string FileName,                // 文件名
       uint DesiredAccess,             // 访问模式
       uint ShareMode,                 // 共享模式
       uint SecurityAttributes,        // 安全属性
       uint CreationDisposition,       // 如何创建
       uint FlagsAndAttributes,        // 文件属性
       int hTemplateFile               // 模板文件的句柄
       );

        /// <summary>
        /// The HidD_GetAttributes routine returns the attributes of a specified top-level collection.
        /// </summary>
        /// <param name="HidDeviceObject">Specifies an open handle to a top-level collection</param>
        /// <param name="Attributes">a caller-allocated HIDD_ATTRIBUTES structure that returns the attributes of the collection specified by HidDeviceObject</param>
        /// <returns></returns>
        [DllImport("hid.dll")]
        private static extern Boolean HidD_GetAttributes(IntPtr hidDeviceObject, out HIDD_ATTRIBUTES attributes);

        /// <summary>
        /// The HidD_GetPreparsedData routine returns a top-level collection's preparsed data.
        /// </summary>
        /// <param name="hidDeviceObject">Specifies an open handle to a top-level collection. </param>
        /// <param name="PreparsedData">Pointer to the address of a routine-allocated buffer that contains a collection's preparsed data in a _HIDP_PREPARSED_DATA structure.</param>
        /// <returns>HidD_GetPreparsedData returns TRUE if it succeeds; otherwise, it returns FALSE.</returns>
        [DllImport("hid.dll")]
        private static extern Boolean HidD_GetPreparsedData(IntPtr hidDeviceObject, out IntPtr PreparsedData);

        [DllImport("hid.dll")]
        private static extern uint HidP_GetCaps(IntPtr PreparsedData, out HIDP_CAPS Capabilities);

        [DllImport("hid.dll")]
        private static extern Boolean HidD_FreePreparsedData(IntPtr PreparsedData);

        /// <summary>
        /// get device guid
        /// </summary>
        /// <param name="HIDGuid"></param>
        internal void GetDeviceGuid(ref Guid HIDGuid)
        {
            HidD_GetHidGuid(ref HIDGuid);
        }

        /// <summary>
        /// get IntPtr about all of Devs
        /// </summary>
        /// <param name="HIDGuid"></param>
        /// <returns></returns>
        internal IntPtr GetClassDevOfHandle(Guid HIDGuid)
        {
            return SetupDiGetClassDevs(ref HIDGuid,0,IntPtr.Zero,USBHIDEnum.DIGCF.DIGCF_PRESENT|USBHIDEnum.DIGCF.DIGCF_DEVICEINTERFACE);
        }

        /// <summary>
        /// get interfaces information
        /// </summary>
        /// <param name="HIDInfoSet"></param>
        /// <param name="HIDGuid"></param>
        /// <param name="index"></param>
        /// <param name="interfaceInfo"></param>
        /// <returns></returns>
        internal bool GetEnumDeviceInterfaces(IntPtr HIDInfoSet, ref Guid HIDGuid, uint index, ref SP_DEVICE_INTERFACE_DATA interfaceInfo)
        {
            return SetupDiEnumDeviceInterfaces(HIDInfoSet, IntPtr.Zero, ref HIDGuid, index, ref interfaceInfo);
        }

        /// <summary>
        /// get interface detail information
        /// </summary>
        /// <param name="HIDInfoSet"></param>
        /// <param name="interfaceInfo"></param>
        /// <param name="buffsize"></param>
        /// <param name="buffsize_2"></param>
        internal bool GetDeviceInterfaceDetail(IntPtr HIDInfoSet, ref SP_DEVICE_INTERFACE_DATA interfaceInfo, IntPtr pDetail, ref int buffsize)
        {
            return SetupDiGetDeviceInterfaceDetail(HIDInfoSet, ref interfaceInfo, pDetail, buffsize, ref buffsize, null);
        }

        /// <summary>
        /// Destroy Device information list
        /// </summary>
        /// <param name="HIDInfoSet"></param>
        internal void DestroyDeviceInfoList(IntPtr HIDInfoSet)
        {
            SetupDiDestroyDeviceInfoList(HIDInfoSet);
        }

        internal IntPtr CreateDeviceFile(string device)
        {
            //return new IntPtr ();
            return CreateFile(device, DESIREDACCESS.GENERIC_READ | DESIREDACCESS.GENERIC_WRITE, 0, 0, CREATIONDISPOSITION.OPEN_EXISTING, FLAGSANDATTRIBUTES.FILE_FLAG_OVERLAPPED, 0);
        }

        /// <summary>
        /// File attributes and flags for the file. 
        /// </summary>
        static class FLAGSANDATTRIBUTES
        {
            public const uint FILE_FLAG_WRITE_THROUGH = 0x80000000;
            public const uint FILE_FLAG_OVERLAPPED = 0x40000000;
            public const uint FILE_FLAG_NO_BUFFERING = 0x20000000;
            public const uint FILE_FLAG_RANDOM_ACCESS = 0x10000000;
            public const uint FILE_FLAG_SEQUENTIAL_SCAN = 0x08000000;
            public const uint FILE_FLAG_DELETE_ON_CLOSE = 0x04000000;
            public const uint FILE_FLAG_BACKUP_SEMANTICS = 0x02000000;
            public const uint FILE_FLAG_POSIX_SEMANTICS = 0x01000000;
            public const uint FILE_FLAG_OPEN_REPARSE_POINT = 0x00200000;
            public const uint FILE_FLAG_OPEN_NO_RECALL = 0x00100000;
            public const uint FILE_FLAG_FIRST_PIPE_INSTANCE = 0x00080000;
        }

        /// <summary>
        /// Action to take on files that exist, and which action to take when files do not exist. 
        /// </summary>
        static class CREATIONDISPOSITION
        {
            public const uint CREATE_NEW = 1;
            public const uint CREATE_ALWAYS = 2;
            public const uint OPEN_EXISTING = 3;
            public const uint OPEN_ALWAYS = 4;
            public const uint TRUNCATE_EXISTING = 5;
        }
        
        /// <summary>
        /// Type of access to the object. 
        ///</summary>
        static class DESIREDACCESS
        {
            public const uint GENERIC_READ = 0x80000000;
            public const uint GENERIC_WRITE = 0x40000000;
            public const uint GENERIC_EXECUTE = 0x20000000;
            public const uint GENERIC_ALL = 0x10000000;
        }

        /// <summary>
        /// Get Device Attribute
        /// </summary>
        /// <param name="device"></param>
        /// <param name="attributes"></param>
        internal void GETDeviceAttribute(IntPtr device, out HIDD_ATTRIBUTES attributes)
        {
            HidD_GetAttributes(device, out attributes);
        }

        /// <summary>
        /// Get Preparse Data
        /// </summary>
        /// <param name="device"></param>
        /// <param name="preparseData"></param>
        internal void GetPreparseData(IntPtr device, out IntPtr preparseData)
        {
            HidD_GetPreparsedData(device,out preparseData);
        }

        /// <summary>
        /// Get Caps
        /// </summary>
        /// <param name="preparseData"></param>
        /// <param name="caps"></param>
        internal void GetCaps(IntPtr preparseData, out HIDP_CAPS caps)
        {
            HidP_GetCaps(preparseData,out caps);
        }

        /// <summary>
        /// free preparse data
        /// </summary>
        /// <param name="preparseData"></param>
        internal void FreePreparseData(IntPtr preparseData)
        {
            HidD_FreePreparsedData(preparseData);
        }
    }
}
