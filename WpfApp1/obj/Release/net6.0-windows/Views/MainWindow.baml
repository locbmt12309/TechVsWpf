   M S B A M L   `   `   `  ���� B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WpfApp1.ViewsWpfApp1.Views      WpfApp1.Views.MainWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:WpfApp1.Views       � Title$  Đăng nhập��$	��750��$
��1300��.��� $��Center=�$��Center=�.+��� $	��300��$	��400��
������� �� CornerRadius$ 6�� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 15Q�   	Direction$	 -90Q�   RenderingBias$ Quality=�   ShadowDepth$ 2Q�   Color$
 Gray��._���  �� Orientation$ Vertical=�$��20q�.+��� $��Login��$��25(�$��Center=�$	��Top=�$
��Bold"�	 �� Padding$	 10q��� -   
 ��Name$
 ErrorMessage��
���  ��$y�	Collapsed=�$��Center=�$��10,10q��� $��	User Name��$
��0,15q�$
��Left=��� -    ��Name$ UserNameTextBox��$	��0,5q�$	��255��$��0,0,0,1q�$
��Left=��� $��Password��$
��0,15q�$
��Left=�G� -    G�Name$ PasswordTextBox��$	��0,5q�$	��255��$��0,0,0,1q�$
��Left=��� -    ��Name$ LoginBtn��$��Login��
�������
���   �$
��0,25q�$��35��$	��250������  �� 
TargetType" ��.���  �� Property � �� �� Value ��  �� 
TargetType" ��.R���  ��Name$ LoginBorder��#��z��$ 16��._��� $��Center=�$��Center=� �� Triggers P�  P� Property �@J�IsMouseOver P� Value$
 True��. ���  �� 
TargetName$ LoginBorder�� � ��
 ������� $��Right=��� -    ��Name$ 	hyperlink��$��http://www.example.com?�.�Đổi mật khẩu