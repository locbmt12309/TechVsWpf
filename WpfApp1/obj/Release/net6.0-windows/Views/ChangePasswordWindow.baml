   M S B A M L   `   `   `  ����   WpfApp1(    "WpfApp1.Views.ChangePasswordWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Đổi Mật Khẩu��$	��700��$
��1300��.��� $��Center=�$��Center=�.+��� $	��300��$	��600��
������� �� CornerRadius$ 6�� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 15Q�   	Direction$	 -90Q�   RenderingBias$ Quality=�   ShadowDepth$ 2Q�   Color$
 Gray��._���  �� Orientation$ Vertical=�$��20q�.+��� $��Đổi Mật Khẩu��$��25(�$��Center=�$	��Top=�$
��Bold"�	 �� Padding$	 10q��� -   
 ��Name$
 ErrorMessage��
���  ��$y�	Collapsed=�$��Center=�$��10,10q��� $��Tên đăng nhập��$
��0,15q�$
��Left=��� -    ��Name$ UserNameTextBox��$	��0,5q�$	��255��$��0,0,0,1q�$
��Left=��� $��Mật khẩu cũ��$
��0,15q�$
��Left=�G� -    G�Name$ OldPasswordBox��$	��0,5q�$	��255��$��0,0,0,1q�$
��Left=��� $��Mật khẩu mới��$
��0,15q�$
��Left=�G� -   $ NewPasswordBox��$	��0,5q�$	��255��$��0,0,0,1q�$
��Left=��� $$��Xác nhận mật khẩu mới��$
��0,15q�$
��Left=�G� -   $ ConfirmNewPasswordBox��$	��0,5q�$	��255��$��0,0,0,1q�$
��Left=��� -    ��Name$ SaveBtn��$
��Lưu��
�������
���   �$
��0,25q�$��35��$	��250������  �� 
TargetType" ��.���  �� Property � �� �� Value ��  �� 
TargetType" ��.R���  ��Name$ 
SaveBorder��#��z��$ 16��._��� $��Center=�$��Center=� �� Triggers P�  P� Property �@J�IsMouseOver P� Value$
 True��. ���  �� 
TargetName$ 
SaveBorder�� � ��
 �����