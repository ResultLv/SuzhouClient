using client.Model;
using client.View;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using USBHIDControl;

namespace client.ViewModel
{
    class ClientViewModel:ViewModel
    {
        static USBHID usbHID = USBHID.GetInstance();
        static bool loginSuccess = false;
        public ClientViewModel()
        {

            if (usbHID.openUSB() == true) USBHID.isOpened = true;
            else USBHID.isOpened = false;
            //MessageBox.Show(usbHID.openUSB().ToString());

        }

        public string getReturn(string commond)
        {
            usbHID.sendCommond(commond);
            //Status = usbHID.status;
            //return Status;
            return usbHID.status;
        }

        public void hasSet(Grid grid)
        {
            DockPanel loginPanel = null;
            Frame mainFrame = null;
            for (int i = 0; i < grid.Children.Count; i++)
            {
                if (grid.Children[i] is DockPanel)
                {
                    loginPanel = (DockPanel)grid.Children[i];
                }
                if(grid.Children[i] is Frame)
                {
                    mainFrame = (Frame)grid.Children[i];
                }
            }

            login log = new login();
            log.ShowDialog();
            if (log.hasLogin == true)    //如果登录成功，则在主界面修改View
            {
                loginSuccess = true;
                string UserName = log.userName;

                loginPanel.Children.Clear();
                TextBlock txBlock = new TextBlock();
                txBlock.Text = UserName + "，欢迎您！";
                txBlock.FontSize = 14;
                txBlock.Margin = new Thickness(0, 0, 10, 0);
                txBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                txBlock.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                Button btn = new Button();
                btn.Content = "退出登录";
                btn.FontSize = 14;
                btn.Width = 70;
                btn.Height = 20;
                btn.Margin = new Thickness(0, 0, 10, 0);
                DockPanel.SetDock(btn, Dock.Right);
                btn.Command = exitLogin;
                btn.CommandParameter = grid;

                loginPanel.Children.Add(btn);
                loginPanel.Children.Add(txBlock);
                try
                {
                    Thread.Sleep(1400);
                    mainFrame.Navigate(new Uri("/View/display.xaml", UriKind.Relative));
                }
                catch (System.NullReferenceException)
                {
                    MessageBox.Show(mainFrame.ToString());
                }

            }
        }

        /*------------ 绑定命令--------------------*/
        public ICommand device  // 跳转设备页面
        {
            get
            {
                return new DelegateCommand<Frame>((mainFrame) =>    //使用Prism框架实现的DelegateCommand类
                {
                    mainFrame.Navigate(new Uri("/View/device.xaml", UriKind.Relative));

                });
            }
        }

        public ICommand upload  // 跳转上传页面
        {
            get
            {
                return new DelegateCommand<Frame>((mainFrame) =>    //使用Prism框架实现的DelegateCommand类
                {
                    mainFrame.Navigate(new Uri("/View/upload.xaml", UriKind.Relative));
                });
            }
        }

        public ICommand display  // 跳转数据展示页面
        {
            get
            {
                return new DelegateCommand<Frame>((mainFrame) =>    //使用Prism框架实现的DelegateCommand类
                {
                    if (loginSuccess) mainFrame.Navigate(new Uri("/View/display.xaml", UriKind.Relative));
                    else MessageBox.Show("请先登录");
                });
            }
        }

        public ICommand signin  // 打开登录页面
        {
            
            get
            {
                return new DelegateCommand<Grid>((grid) =>    //使用Prism框架实现的DelegateCommand类
                {
                    if (USBHID.isOpened)
                    {
                        // 登录前先判断用户名是否已设置
                        int isSet = int.Parse(getReturn("04 00").Substring(5, 1));
                        // 已设置则跳转至登录界面
                        if (isSet == 0)
                        {
                            hasSet(grid);
                        } 
                        else
                        {
                            MessageBox.Show("该设备还未设置过账号，请设置用户名和密码");

                            user setUser = new user();
                            setUser.ShowDialog();
                        }
                    }
                    else MessageBox.Show("USB未插入或已变为外存");
                });
            }
        }

        public ICommand exitLogin  // 退出登录
        {
            get
            {
                return new DelegateCommand<Grid>((grid) =>
                {
                    DockPanel loginPanel = null;
                    for(int i = 0; i < grid.Children.Count; i++)
                    {
                        if(grid.Children[i] is DockPanel)
                        {
                            loginPanel = (DockPanel)grid.Children[i];
                        }
                    }
                    loginPanel.Children.Clear();
                    TextBlock txBlock = new TextBlock();
                    txBlock.Text = "未登录";
                    txBlock.FontSize = 14;
                    txBlock.Margin = new Thickness(0, 0, 10, 0);
                    txBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    txBlock.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                    Button btn = new Button();
                    btn.Content = "登录";
                    btn.FontSize = 14;
                    btn.Width = 60;
                    btn.Height = 20;
                    btn.Margin = new Thickness(0, 0, 10, 0);
                    DockPanel.SetDock(btn, Dock.Right);
                    btn.Command = signin;
                    btn.CommandParameter = loginPanel;

                    loginPanel.Children.Add(btn);
                    loginPanel.Children.Add(txBlock);
                });
            }
        }

        public ICommand checkUserInfo  // 检查是否设置用户名
        {
            get
            {
                return new DelegateCommand<Grid>((grid) =>    //使用Prism框架实现的DelegateCommand类
                {
                    if (USBHID.isOpened)
                    {
                        int isSet = int.Parse(getReturn("04 00").Substring(5, 1));
                        if (isSet == 0)
                        {

                            MessageBox.Show("该设备已设置过账号，请直接登录");
                            hasSet(grid);

                        }
                        else
                        {
                            MessageBox.Show("该设备还未设置过账号，请设置用户名和密码");
                            user setUser = new user();
                            setUser.ShowDialog();
                        }
                    }
                    else MessageBox.Show("USB未插入或已变为外存");

                });
            }
        }

        public ICommand reviseUserInfo  // 修改账户
        {
            get
            {
                return new DelegateCommand(() =>    //使用Prism框架实现的DelegateCommand类
                {
                    if (USBHID.isOpened)
                    {
                        user setUser = new user();
                        setUser.ShowDialog();
                    }

                });
            }
        }

        public ICommand setMaintenance  // 设置预防性维护
        {
            get
            {
                return new DelegateCommand(() =>    //使用Prism框架实现的DelegateCommand类
                {
                    if (USBHID.isOpened)
                    {
                        mintenance minten = new mintenance();
                        minten.ShowDialog();
                    }

                });
            }
        }

        public ICommand modiTimes  // 读取模具检修次数
        {
            get
            {
                return new DelegateCommand(() =>    //使用Prism框架实现的DelegateCommand类
                {
                    if (USBHID.isOpened)
                    {
                        string times = getReturn("05").Split(':')[1];
                        MessageBox.Show("检修次数:" + times.Substring(0, times.Length - 1));
                    }
                });
            }
        }

        public ICommand resetModiTimes  // 重置检修次数
        {
            get
            {
                return new DelegateCommand(() =>    //使用Prism框架实现的DelegateCommand类
                {
                    if (USBHID.isOpened)
                    {
                        if (MessageBox.Show("您确定要重置检修次数吗？", "提示：", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                        {
                            string result = getReturn("OC").Substring(5, 1);
                            MessageBox.Show(result);
                            if (result == "0") MessageBox.Show("重置成功");
                            else MessageBox.Show("重置失败");
                        }
                    }

                });
            }
        }

        public ICommand initialSystemData  // 初始化系统数据
        {
            get
            {
                return new DelegateCommand(() =>    //使用Prism框架实现的DelegateCommand类
                {
                    if (USBHID.isOpened)
                    {
                        if (MessageBox.Show("您确定要初始化系统吗？(除TF卡外所有系统数据将会被清零)", "提示：", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                        {
                            string result = getReturn("FE");
                            MessageBox.Show(result);
                        }
                    }
                });
            }
        }

    }
}
