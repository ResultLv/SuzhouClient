using client.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;

using USBHIDControl;

namespace client
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //bool loginSuccess = false;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ClientViewModel();
        }

        private void client_Loaded(object sender, RoutedEventArgs e)
        {
            //mainFrame.Navigate(new Uri("/View/display.xaml", UriKind.Relative));
        }

    }
}
