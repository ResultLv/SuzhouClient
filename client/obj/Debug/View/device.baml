   M S B A M L   `   `   `  ����A  =client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:client.Viewclient.View  5      3clr-namespace:client.ViewModelclient.ViewModel  5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5          client.View.device   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:client.View     5      -vmclr-namespace:client.ViewModel     5      ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5        N� Title$  device��5       � DataContext 5      &   client.ViewModel.DeviceViewModel  5   
   6    � 	Resources 5       �� 
TargetType �� Property �� Value��%/  '��       '�� w     '�� �     �� 5   
   " ��6   .�5      ��  � ��$	 140��6&   64   �� 5       � ��$ 14(�6)   66   5      �� 5   
   " ��6   .�5      ��  � ��$ 14(�6)   66   5      �� 5   
   " ��6   .�5      ��  � ��$ 14(�6)   66   5      5      .  (System.Windows.Interactivity.Interaction   Triggers 5!      /  )System.Windows.Interactivity.EventTrigger  5"   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5#      6  0System.Windows.Interactivity.InvokeCommandAction  	   Command	 6$   �� *
PageLoaded+5$      .��5'      � 
 � RowDefinitions
 5(   
   �� 5)      $��* �6   6)   �� 5*      $��2* �6   6*   �� 5+      $��15 �6   6*    � ColumnDefinitions 5-   
   �� 5.      $
��1.5* �6   6.   �� 5/      $��6* �6   6,   .+�51   
   �� $��Center=�6   $��Center=�6/   $��18(�6L   设备信息6`   �� 52   
   $��1��6   $��Center=�6!   $��Center=�6<   $��18(�6Y   设备状态6m   ]� 55   
   $��1��6   $��	80,0,80,0q�6$   .+�56      �� $��Center=�6   $��Center=�64   $��0,0,0,0q�6Q   .+�57      �� 	序列号6!   �� 58       �� 
IsReadOnly . ��6,   �� *ID+6B   �� 59      $��0,10,0,0q�6   软件版本号65   �� 5:       . ��6,   �� *sRev+6D   5;      �� 5<      $��2��6   $��Center=�6*   $��Center=�6E   .+�5=      �� 硬件版本号6#   �� 5>       . ��6,   �� *hRev+6D   �� 5?      $��0,10,0,0q�6   发行日期64   �� 5@       . ��6,   �� *cmp+6C   5A      5B      ]� 5I   
   $��1��6   $��2��6!   $��
80,30,80,0q�61   .+�5J      �� $	��top=�6   $��Center=�61   $��0,0,0,0q�6N   .+�5K      �� WIFI连接状态6&   �� 5L       . ��6,   �� *AP+6B   �� 5M      $��0,10,0,0q�6   服务器连接状态67   �� 5N       . ��6,   �� *Svr+6C   �� 5O      $��0,10,0,0q�6   设备时间64   �� 5P       . ��6,   �� *DtTm+6D   �� 5Q      $��0,10,0,0q�6   时间校正64   �� 5R      ��6   �� *DtTm+62   �� 5S      ��6   �� *timeCorrection+.��6<   确定6@   5T      �� 5U      $	��top=�6   $��Center=�61   $��0,0,0,0q�6N   .+�5V      �� 锂电池电压6#   �� 5W       . ��6,   �� *Batt+6D   �� 5X      $��0,10,0,0q�6   存储容量64   �� 5Y       . ��6,   �� *	CardCap+6G   �� 5Z      $��0,10,0,0q�6   拆卸状态64   �� 5[       . ��6,   �� *Adh+6C   �� 5\      $��0,10,0,0q�6   当前拆卸次数66   �� 5]       . ��6,   �� *Disarm+6F   5^      5_      �� 5c   
   $
��Left=�6   $��5��62   $��1��6=   $	��Top=�6J   $	��710��6b   $��2��6n   ���#E6E6E66�   �� 5d   
   $��1��6   $
��Left=�6'   $	��440��6B   $��2��6O   $	��Top=�6`   $��5��6x   ���#E6E6E66�   �� 5f   
   $��2��6   $��2��6!   $��12(�6M   ���#F0F0F0��65   �� *Status+6r   5g      5h      