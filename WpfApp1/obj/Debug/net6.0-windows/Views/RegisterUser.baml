   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WpfApp1.ViewsWpfApp1.Views  5   	        WpfApp1.Views.RegisterUser   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:WpfApp1.Views     5   	     � Title$  
Đăng ký��5   	   $	��750��6   $
��1300��6&   .��5	      � $��Center=�6   $��Center=�6&   .+�5
   
   �� $	��300��6   $	��600��5      
������� �� CornerRadius$ 6��5       J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 15Q�6#      	Direction$	 -90Q�5   #      RenderingBias$ Quality=�5   #      ShadowDepth$ 2Q�5   #      Color$
 Gray��5   #   ._�5      ��  �� Orientation$ Vertical=�6   $��20q�60   .+�5      �� $��
Đăng ký��6   $��25(�5      $��Center=�5      $	��Top=�5      $
��Bold"�5      	 �� Padding$	 10q�5      �� 5      -   5        
 ��Name$
 ErrorMessage��5      
���  ��$y�	Collapsed=�5       $��Center=�5!      $��10,10q�5"      �� 5$      $��Họ và tên��6   $
��0,15q�6-   $
��Left=�6;   �� 5%      -   5         ��Name$ FullNameTextBox��5%      $	��0,5q�63   $	��255��6A   $��0,0,0,1q�6M   $
��Left=�6g   �� 5'      $��Tên đăng nhập��6   $
��0,15q�61   $
��Left=�6?   �� 5(      -   5        $ UserNameTextBox��5(      $	��0,5q�63   $	��255��6A   $��0,0,0,1q�6M   $
��Left=�6g   �� 5*      $��Mật khẩu��6   $
��0,15q�6,   $
��Left=�6:   G� 5+      -   5         G�Name$ PasswordTextBox��5+      $	��0,5q�67   $	��255��6E   $��0,0,0,1q�6Q   $
��Left=�6k   �� 5-      $��Xác nhận mật khẩu��6   $
��0,15q�65   $
��Left=�6C   G� 5.      -   5        $ ConfirmPasswordTextBox��5.      $	��0,5q�6>   $	��255��6L   $��0,0,0,1q�6X   $
��Left=�6r   �� 50      -   5         ��Name$ RegisterBtn��50      $��
Đăng ký��51      
�������
���   �$
��0,25q�54      $��35��55      $	��250��56      ��58      �� 59       �� 
TargetType" ��6    .�5:      ��  �� Property � �� �� Value 5;   "   �� 5<   &    �� 
TargetType" ��66   .R�5=   *   ��  ��Name$ RegisterBorder��61   #��z��5>   1   $ 16��5?   1   ._�5@   .   �� $��Center=�6?   $��Center=�5A   ?   5B   +    �� Triggers 5C   *   P� 5D   .    P� Property �@J�IsMouseOver P� Value$
 True��6M   . �5E   2   ��  �� 
TargetName$ RegisterBorder��5G   9    � ��
 �����5H   /   5J   '   5L      5M      5O      �� 5Q      $��Right=�6   �� 5R      -   5         ��Name$ loginHyperlink��5R       .�5S   L   &$Đã có tài khoản? Đăng nhập5U      5V      5W      5X      5Y      5Z      