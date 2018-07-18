using client.Model;
using client.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using USBHIDControl;

namespace client.View
{
    /// <summary>
    /// upload.xaml 的交互逻辑
    /// </summary>
    public partial class upload : Page
    {
        public upload()
        {
            InitializeComponent();
            this.DataContext = new UploadViewModel();
        }
    }
}
