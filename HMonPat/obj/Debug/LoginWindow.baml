   M S B A M L   `   `   `  ����B  >HMonPat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:HMonPat.ConvertersHMonPat.Converters  5   	   !clr-namespace:HMonPatHMonPat  5   	       HMonPat.LoginWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1conv clr-namespace:HMonPat.Converters     5   	   'localclr-namespace:HMonPat     5   	     � Title$  Login��5	   	   $	��350��6   $	��525��6$    � 	Resources 5
      5   /HMonPat.Converters.BooleanToVisibilityConverter    BooleanToVisibilityConverter��%   &	          5   
   6,   5      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $	��125 �6   �� 5      $��50 �6   �� 5      $��* �6   .+�5   
   � $��1��6   y�6   �� *
IsLoggedIn+ �� 	Converter# [   5      �� 5      $��40 �6    �� 5      $��40 �6    �� 5      $��40 �6    �� 5      $��40 �6     � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $	��100 �6#   �� 5      $	��150 �6#   �� 5      $��* �6#   .+�5"      �� $��
Host name:��6   $
��Left=�6*   $��Center=�6E   $��1��6`   $��1��6m   �� 5#      -   5        $��Hostname��5#      $	��150��6&   $��25��62   $��Center=�6>   $
��Left=�6Y   $��1��6t   $��2��6�   �� 5%      $��Port number:��6   $
��Left=�6,   $��Center=�6G   $��2��6b   $��1��6o   �� 5&      -   5        $��
Portnumber��5&      $	��150��6(   $��25��64   $��Center=�6@   $
��Left=�6[   $��2��6v   $��2��6�   5(      �� 5)   
   -   5        $��LoginButton��5)      $	��150��6$   $��50��60   $��Center=�6<   $��20(�6Y   $��Login��6g   $��2��6�   �� 5*   
   -   5        $��ErrorMessage��5*      $��Center=�6(   $��3��6E   $��12(�6R   
���  ��$��5,5,5,5q�6q   5+      5,      