   M S B A M L   `   `   `  ����A  =client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:client.Viewclient.View  5      3clr-namespace:client.ViewModelclient.ViewModel  5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5          client.View.upload   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:client.View     5      -vmclr-namespace:client.ViewModel     5      ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5        N� Title$  upload��5       � DataContext 5      &   client.ViewModel.UploadViewModel  5   
   6    � 	Resources 5       �� 
TargetType �� Property �� Value��%/  '��       '�� w     '�� �     �� 5   
   " ��6   .�5      ��  � ��$	 180��6&   64   �� 5       � ��$ 14(�6)   66   5      �� 5   
   " ��6   .�5      ��  � ��$ 12(�6)   66   5      �� 5   
   " ��6   .�5      ��  � ��$ 14(�6)   66   5      5      .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5    
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5!      6  0System.Windows.Interactivity.InvokeCommandAction  	   Command	 6$   �� *
PageLoaded+5"      .��5%      � 
 � RowDefinitions
 5&   
   �� 5'      $��2* �6   6*   �� 5(      $��5* �6   6*   �� 5)      $��15 �6   6*    � ColumnDefinitions 5+   
   �� 5,      $
��1.5* �6   6.   �� 5-      $��6* �6   6,   .+�5/   
   �� $��Center=�6   $��Center=�6/   $��18(�6L   服务器设置6a   �� 50   
   $��1��6   $��Center=�6!   $��Center=�6<   $��18(�6Y   
WIFI设置6o   ]� 53   
   $��1��6   $��	60,0,60,0q�6$   .+�54      �� $��Center=�6   $��Center=�64   $��0,0,0,0q�6Q   .+�55      �� $��14(�6   
IP地址60   �� 56      ��6   �� *IP+60   �� 57      $��0,10,0,0q�6   $��14(�6.   	端口号6A   �� 58      ��6   �� *Port+62   59      �� 5:      $��Center=�6   $	��140��64   .+�5;      �� $��	0,15,0,27q�6   ��6,   �� *
readServer+.��6K   读取6O   �� 5<      ��6   �� *	setServer+.��67   设定6;   5=      5>      ]� 5A   
   $��1��6   $��2��6!   $��
30,20,30,0q�61   .+�5B      �� $	��top=�6   $��Center=�61   $��0,0,0,0q�6N   .+�5C      �� $��14(�6   WIFI接入点64   �� 5D      -   5         ��Name$ listview��5D      $	��140��6-   ��6:   �� *
datalist+ �� View 5E      �� 5F      .P�5G      �� $��名称��6-    �� Width$	 100��6c    �� DisplayMemberBinding 69   �� *
WifiName+6c   �� 5H      $��信号强度��6-   $ 80��6m    6;   �� *ChannelIntensity+6m   �� 5I      $��	MAC地址��6-   $	 120��6e    6<   �� *	WifiMac+6e   �� 5J      $��	信道号��6-   $ 60��6f    6:   �� *
ChannelNum+6f   �� 5K      $��加密方式��6-   $	 110��6d    6;   �� *	WifiEnc+6d   5L       5O        5P      $ SelectionChanged��6)   . 5Q        	 64   �� *SelectionChanged+   CommandParameter 6Y   ��  �� ElementName$ listview��5R      5T      ]� 5V      .+�5W      �� $��0,10,0,5q�6    $��14(�62   
设置WIFI6H   �� 5X      $	��100��6   $��22��6)   $��Right=�65   ��6Q   �� *refreshList+.��6q   刷新6u   5Y      ]� 5[      $��0,0,0,2q�6   .+�5\      �� $��0,0,10,0q�6    $��14(�62   
WIFI名称6H   �� 5]      $
��Left=�6   ��69   �� *
WifiName+6U   �� 5^      $	��100��6   ��6)   �� *checkConnect+.��6J   查看当前连接6R   5_      ]� 5`      .+�5a      �� $��0,0,10,0q�6    $��14(�63   
WIFI密码6I   �� 5b      $
��Left=�6   ��69   �� *WifiPW+6S   �� 5c      $	��100��6   ��6)   �� *connectWifi+.��6I   设置连接6O   5d      5e      5f      �� 5h   
   $
��Left=�6   $��5��62   $��1��6=   $	��Top=�6J   $	��710��6b   $��2��6n   ���#E6E6E66�   �� 5i   
   $��1��6   $
��Left=�6'   $	��440��6B   $��2��6O   $	��Top=�6`   $��5��6x   ���#E6E6E66�   �� 5k   
   -   5         ��Name$ status��5k      $��2��6$   $��2��61   ���#F0F0F0��6E   �� *Status+6t   5l      5m      