   M S B A M L   `   `   `  ����   WpfApp1(    "WpfApp1.Views.ChangeDateTimeWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     V RMaterialDesignThemes.Wpf, Version=3.1.2.1465, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    � Title$  Change Date/Time Settings��$	��700��$
��1300��   MaterialDesignBody#���   $��Regular"�$��13(�&   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�   TextRenderingMode$
 Auto=�  MaterialDesignPaper#���    MaterialDesignFont#���  .��� $	��Top=�$��Stretch=� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��
������$��50��$��0,0,0,10q�._�� $��Stretch=�$��Center=� � ColumnDefinitions �� $��* ��� $
��Auto ��� $
��Auto �.+��� $��0��$��Menu Manage��$��Center=�
���   �$��18(�$
��10,0q��� $��2�� �� Orientation$ 
Horizontal=�$��Right=�$
��10,0q�$��Center=�.+��� -   $��UserNameTextBlock��$��UserName��$��Center=�
���   �$��0,0,10,0q� � Cursor$
 Hand���� -   $��LogoutButton��$��Logout��$��80��$��30��$y�	Collapsed=�� $��1�����#f0f0f0$��40��.+��� $��Service List��$��18(�$
��Bold"�$��Center=�$��Center=�$��10q�&   System.Windows.Controls.DataGrid  -   $��FunctionDataGrid��$��2��   AutoGenerateColumns$ False��   HeadersVisibility$ Column=�	   CanUserSortColumns$
	 True��
   CanUserAddRows$
 False��   
IsReadOnly$
 True��$��10q�   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ *    Columns 1  +MaterialDesignThemes.Wpf.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$	 STT��   Width$ 50 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *STT+   ElementStyle ��  �� 
TargetType" ��.���  �� Property � �� �� Value$ Center=���  � ��$ Center=���  �@��TextAlignment$ Center=�  $ Tên Service��$ *  �� *ServiceName+ �� " ��.���  � ��$ Center=���  � ��$ Center=���  �@��TextAlignment$ Center=�4  .System.Windows.Controls.DataGridTemplateColumn  $ Trạng thái��$
 Auto    CellTemplate �� .R�d� $��80��$��30�� $  MaterialDesignSwitchToggleButton#��[  � ToolTip$* $Click to toggle the date/time change�� d� 	IsChecked �� *	IsChecked+ �� Mode$ TwoWay=����� *ButtonColor+���� *ToggleStatusCommand+�� -   $��
SaveButton��$
��Save��$	��100��$��40��
�������
�������$
��Bold"�$��3��$��Right=�$��10q�