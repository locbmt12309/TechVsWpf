   M S B A M L   `   `   `  ����  WpfApp1(    "WpfApp1.Views.ChangePasswordWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Đổi Mật Khẩu��5   	   $	��700��6   $
��1300��6+   .��5      � $��Center=�6   $��Center=�6&   .+�5   
   �� $	��300��6   $	��600��5      
������� �� CornerRadius$ 6��5	       J� Effect 5
      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 15Q�6#      	Direction$	 -90Q�5   #      RenderingBias$ Quality=�5   #      ShadowDepth$ 2Q�5   #      Color$
 Gray��5   #   ._�5      ��  �� Orientation$ Vertical=�6   $��20q�5      .+�5      �� $��Đổi Mật Khẩu��6   $��25(�5      $��Center=�5      $	��Top=�5      $
��Bold"�5      	 �� Padding$	 10q�5      �� 5      -   5        
 ��Name$
 ErrorMessage��5      
���  ��$y�	Collapsed=�5      $��Center=�5      $��10,10q�5      �� 5!      $��Tên đăng nhập��6   $
��0,15q�5"      $
��Left=�5#      �� 5$      -   5         ��Name$ UserNameTextBox��5$      $	��0,5q�5%      $	��255��5&      $��0,0,0,1q�5'      $
��Left=�5(      �� 5*      $��Mật khẩu cũ��6   $
��0,15q�5+      $
��Left=�5,      G� 5-      -   5         G�Name$ OldPasswordBox��5-      $	��0,5q�5.      $	��255��5/      $��0,0,0,1q�50      $
��Left=�51      �� 53      $��Mật khẩu mới��6   $
��0,15q�54      $
��Left=�55      G� 56      -   5        $ NewPasswordBox��56      $	��0,5q�57      $	��255��58      $��0,0,0,1q�59      $
��Left=�5:      �� 5<      $$��Xác nhận mật khẩu mới��6   $
��0,15q�5=      $
��Left=�5>      G� 5?      -   5        $ ConfirmNewPasswordBox��5?      $	��0,5q�5@      $	��255��5A      $��0,0,0,1q�5B      $
��Left=�5C      �� 5F      -   5         ��Name$ SaveBtn��5F      $
��Lưu��5G      
�������
���   �$
��0,25q�5J      $��35��5K      $	��250��5L      ��5N      �� 5O       �� 
TargetType" ��6    .�5P      ��  �� Property � �� �� Value 5Q   "   �� 5R   &    �� 
TargetType" ��66   .R�5S   *   ��  ��Name$ 
SaveBorder��61   #��z��5T   1   $ 16��5U   1   ._�5V   .   �� $��Center=�6?   $��Center=�5W   ?   5X   +    �� Triggers 5Y   *   P� 5Z   .    P� Property �@J�IsMouseOver P� Value$
 True��6M   . �5[   2   ��  �� 
TargetName$ 
SaveBorder��5]   9    � ��
 �����5^   /   5`   '   5b      5c      5e      5g      5h      5i      5j      