   M S B A M L   `   `   `  ����A  =client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:client.Viewclient.View  5      3clr-namespace:client.ViewModelclient.ViewModel  5	      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5
          client.View.display   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:client.View     5      -vmclr-namespace:client.ViewModel     5	      ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5
        N� Title$  dsiplay��5       � DataContext 5      $  client.ViewModel.DataViewModel  5   
   6    � 	Resources 5       �� 
TargetType �� Property �� Value��%/  '��       '�� w     '�� �     �� 5   
   " ��6   .�5      ��  � ��$	 180��6&   64   �� 5       � ��$ 14(�6)   66   5      �� 5   
   " ��6   .�5      ��  � ��$ 12(�6)   66   5      �� 5   
   " ��6   .�5      ��  � ��$ 14(�6)   66   5      5      .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5    
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5!      6  0System.Windows.Interactivity.InvokeCommandAction  	   Command	 6$   �� *
PageLoaded+5"      .��5%      � 
 � RowDefinitions
 5&   
   �� 5'      $��* �6   6)   �� 5(      $��5* �6   6*   �� 5)      $��15 �6   6*    � ColumnDefinitions 5+   
   �� 5,      $��* �6   6+   .+�5/   
   �� $��Center=�6   $��Center=�6/   $��28(�6L   内存卡数据读取6c   ]� 50   
   $��1��6   $��0,5,0,0q�6!   .+�51      &   System.Windows.Controls.DataGrid  $��1��6   $	��Top=�6%   $	��700��6:   $	��280��6F      AutoGenerateColumns .  ��6o   �� *
datalist+   Columns 52      0  *System.Windows.Controls.DataGridTextColumn  53      ,  &System.Windows.Controls.DataGridColumn   Header$ 	文件名��6A   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 6)   �� *Fil+6P     54      $ 日期时间��6B    6)   �� *DtTm+6R     55      $ 成型总数��6B    6)   �� *Totl+6R     56      $ 成型平均时间��6B    6)   �� *MdAv+6T     57      $! 上批次平均成型时间��6C    6)   �� *LstAv+6X     58      $ 效率百分比��6B    6)   �� *MdEf+6S     59      $ 上批次效率百分比��6C    6)   �� *LstEf+6W     5:      $ 当前周期数��6D    6)   �� *CurPer+6U     5;      $ 模具检修次数��6B    6)   �� *MdTm+6T     5<      $ 拆除状态��6A    6)   �� *Dis+6Q   5>      �� 5?      $��80��6   $��24��6    $��Right=�6,   $��0,0,10,0q�6C   ��6U   �� *	readAll+.��6q   读取所有6w   �� 5@      $	��100��6   $��24��6!   $��	460,0,0,0q�6-   ��6@   �� *read10+.��6[   读取近10条6c   5B      �� 5D   
   $
��Left=�6   $��5��62   $��1��6=   $	��Top=�6J   $	��710��6b   $��2��6n   ���#E6E6E66�   �� 5E   
   $��1��6   $
��Left=�6'   $	��440��6B   $��2��6O   $	��Top=�6`   $��5��6x   ���#E6E6E66�   �� 5G   
   -   5         ��Name$ status��5G      $��2��6$   $��2��61   ���#F0F0F0��6E   �� *Status+6t   5H      5I      