   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WpfApp1.ViewsWpfApp1.Views  5   	       WpfApp1.Views.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:WpfApp1.Views     5   	     � Title$  Đăng nhập��5   	   $	��750��6   $
��1300��6(   .��5	      � $��Center=�6   $��Center=�6&   .+�5
   	   �� $	��300��6   $	��400��5      
������� �� CornerRadius$ 6��5       J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 15Q�6#      	Direction$	 -90Q�5   #      RenderingBias$ Quality=�5   #      ShadowDepth$ 2Q�5   #      Color$
 Gray��5   #   ._�5      ��  �� Orientation$ Vertical=�6   $��20q�5      .+�5      �� $��Đăng nhập��6   $��25(�5      $��Center=�5      $	��Top=�5      $
��Bold"�5      	 �� Padding$	 10q�5      �� 5      -   5        
 ��Name$
 ErrorMessage��5      
���  ��$y�	Collapsed=�5!      $��Center=�5"      $��10,10q�5#      �� 5%      $��Tên Đăng nhập��6   $
��0,15q�5&      $
��Left=�5'      �� 5)      -   5         ��Name$ UserNameTextBox��5)      $	��0,5q�5*      $	��255��5+      $��0,0,0,1q�5,      $
��Left=�5-      �� 5/      $��Mật khẩu��6   $
��0,15q�50      $
��Left=�51      G� 53      -   5         G�Name$ PasswordTextBox��53      $	��0,5q�54      $	��255��55      $��0,0,0,1q�56      $
��Left=�57      �� 59      -   5         ��Name$ LoginBtn��59      $��Đăng nhập��5:      
�������
���   �$
��0,25q�5=      $��35��5>      $	��250��5?      ��5A      �� 5B       �� 
TargetType" ��6    .�5C      ��  �� Property � �� �� Value 5D   "   �� 5E   &    �� 
TargetType" ��66   .R�5F   *   ��  ��Name$ LoginBorder��61   #��z��5G   1   $ 16��5H   1   ._�5I   .   �� $��Center=�6?   $��Center=�5J   ?   5K   +    �� Triggers 5L   *   P� 5M   .    P� Property �@J�IsMouseOver P� Value$
 True��6M   . �5N   2   ��  �� 
TargetName$ LoginBorder��5P   9    � ��
 �����5Q   /   5S   '   5U      5V      5X      �� 5Y      $��Right=�6   �� 5Z      -   5         ��Name$ 	hyperlink��5Z       $��http://www.example.com?�5[       .�5\   L   Đổi mật khẩu5^      5_      5`      5a      5b      5c      