   M S B A M L   `   `   `  ����A  =client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:client.ViewModelclient.ViewModel  5   	   clr-namespace:clientclient  5   	       client.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -vmclr-namespace:client.ViewModel     5   	   &localclr-namespace:client     5   	   -   5          �Name$
  load��5   	    � Title$ 
MainWindow��5	   	   $	��480��6   $	��720��6)    � WindowStartupLocation$ CenterScreen=�5
   	    � DataContext 5      &   client.ViewModel.ClientViewModel  5   
   6    � 	Resources 5      ��%    5      .��5      � -   5         �Name$
 grid��5       � RowDefinitions 5   
   �� 5      $��40 �6   6*   �� 5      6    � ColumnDefinitions 5   
   �� 5      $��3* �6   6,   �� 5      $��2* �6   6,   .+�5   
   o� $��16(�6   .@�5      m� $��设备��6   $��40��6q   $��	10,0,10,0q�6}    m� Command 6#   �� *device+	 m� CommandParameter	 6>   �� 
 �� ElementName$
 	mainFrame��6}   m� 5      $��数据上传��6   $��	10,0,10,0q�6t    6%   �� *upload+	 6A   �� $
 	mainFrame��6t   m� 5      $��生产状况��6   $��	10,0,10,0q�6t    6%   �� *	display+	 6A   �� $
 	mainFrame��6t   m� 5      $��设置��6   $��	10,0,10,0q�6#   .@�5      m� $��检查用户信息��6    6+   �� *checkUserInfo+	 6M   �� $

 grid��6}   m� 5       $��修改用户名和密码��6    6-   �� *reviseUserInfo+6R   m� 5!      $��设置预防性维护点��6    6-   �� *setMaintenance+6R   m� 5"      $��模具检修次数��6    6+   �� *	modiTimes+6K   m� 5#      $��重置检修次数��6    6+   �� *resetModiTimes+6P   m� 5$      $��初始化系统数据��6    6,   �� *initialSystemData+6T   5%      m� 5&      $��关于��6   $��	10,0,10,0q�6#   5'      ]� 5)   
   -   5         ]�Name$ 
loginPanel��5)      $��2��6(   ���#F0F0F0.+�5*      �� $��Right=�6   $��0,0,10,0q�6,   $��60��6>   $��20��6I   $��14(�6U   ��6d   �� *signin+��6   �� $

 grid��.��6�   登录6�   �� 5+      $��0,0,10,0q�6   $��14(�6+   $��Right=�69   $��Center=�6U   	未登录6v   5,       � 5/   
   -   5          �Name$ 	mainFrame��5/        � Source$ /View/device.xaml?�6#   $��1��6>   $��2��6K     � NavigationUIVisibility$ Hidden=�6_   52      53      