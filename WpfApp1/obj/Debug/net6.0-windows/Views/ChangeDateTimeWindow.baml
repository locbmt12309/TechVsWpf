   M S B A M L   `   `   `  ����  WpfApp1(    "WpfApp1.Views.ChangeDateTimeWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   V RMaterialDesignThemes.Wpf, Version=3.1.2.1465, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  Change Date/Time Settings��5   	   $	��700��6+   $
��1300��68      MaterialDesignBody#���   5   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5	   	      TextRenderingMode$
 Auto=�5
   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	   .��5      � $	��Top=�6   $��Stretch=�6#    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   
������$��50��66   $��0,0,0,10q�6B   ._�5      � $��Stretch=�6   $��Center=�61    � ColumnDefinitions 5      �� 5      $��* �6'   �� 5      $
��Auto �6'   �� 5      $
��Auto �6'   .+�5!      �� $��0��6   $��Menu Manage��6,   $��Center=�6?   
���   �$��18(�6m   $
��10,0q�6{   �� 5"      $��2��6    �� Orientation$ 
Horizontal=�6-   $��Right=�6F   $
��10,0q�6b   $��Center=�6p   .+�5#      �� -   5        $��UserNameTextBlock��5#       $��UserName��69   $��Center=�6I   
���   �$��0,0,10,0q�6w    � Cursor$
 Hand��6�   6�   �� 5$      -   5        $��LogoutButton��5$      $��Logout��61   $��80��6B   $��30��6M   $y�	Collapsed=�6t   5%      5&      5'      � 5)   
   $��1��6   ���#f0f0f0$��40��61   .+�5*      �� $��Service List��6   $��18(�6,   $
��Bold"�6:   $��Center=�6L   $��Center=�6g   $��10q�6�   5+      &   System.Windows.Controls.DataGrid  5-   
   -   5        $��FunctionDataGrid��5-       $��2��6      AutoGenerateColumns$ False��68      HeadersVisibility$ Column=�6T   	   CanUserSortColumns$
	 True��5.      
   CanUserAddRows$
 False��6-      
IsReadOnly$
 True��6D   $��10q�6V      ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6b      Columns 5/      1  +MaterialDesignThemes.Wpf.DataGridTextColumn  51      ,  &System.Windows.Controls.DataGridColumn   Header$	 STT��64      Width$	 100 6Y   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6A   �� *STT+   ElementStyle 52      �� 53       �� 
TargetType" ��6    .�54      ��  �� Property � �� �� Value$ Center=�6B   �� 55       � ��$ Center=�6D   �� 56       �@��TextAlignment$ Center=�6>   57      59        5;      $ Tên Service��64   $ * 6i    6I   �� *ServiceName+ 5<      �� 5=      " ��6    .�5>      ��  � ��$ Center=�6B   �� 5?       � ��$ Center=�6D   �� 5@       �@��TextAlignment$ Center=�6>   5A      5C      4  .System.Windows.Controls.DataGridTemplateColumn  5E      $ Trạng thái��6)   $
 Auto 6=      CellTemplate 5F      �� 5G      .R�5H      d� $��80��6+   $��30��66    $  MaterialDesignSwitchToggleButton#��[ 5I   +    � ToolTip$' !Click to toggle CMD functionality��5J   +    d� 	IsChecked 5K   +   �� *	IsChecked+ �� Mode$ TwoWay=���5L   +   �� *ButtonColor+��5M   +   �� *ToggleStatusCommand+5N      5O      5Q      5T      5X      5Z      