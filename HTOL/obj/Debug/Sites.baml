   M S B A M L   `   `   `  ����?  ;HTOL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:HTOL.ConvertersHTOL.Converters  5      clr-namespace:HTOLHTOL  5          
HTOL.Sites   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      0converclr-namespace:HTOL.Converters     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      $localclr-namespace:HTOL     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5        � 	Resources  5       �� 
TargetType �� Property �� Value)   #HTOL.Converters.BackgroundConverter   bgConverter��%q   '��       &	  J     �� 5   
   " ��6   .�5      ��  � ��
 �����6+   5        5   
   6%   5      .��5      �� $��1��6   $��0��6   ._�5   
   �� ��6   �� *SitesVM.Sites+��5      �� 5      .R�5      A�  A� Columns$ 6��6"   5      ��5      �� 5      .R�5      �� $��2q�5       �� CornerRadius$ 5��5      ��5      �� *Status+ �� 	Converter# [  ._�5      �� $��Center=�5       $��Center=�5!       �� Orientation$ Vertical=�5"      .+�5#      �� 
���������6;   �� *	TcaAddr+ �� StringFormat$ TCA Addr : {0}���� 5$      
���������6;   �� *	Channel+�� 5%      
���������6;   �� *
ChipAddr+$ Chip Addr : {0}��5&      	 � ToolTip	 5'      �� 5(      ��6+   �� *
Register+��5)   "   �� 5*   &   .R�5+   *   �� $ 
Horizontal=�65   .+�5,   .   �� ��68   �� *Addr+$ 	0x{0:X4}:���� 5-   .   ��68   �� *Val+$ 0x{0:X8}��5.   +   5/   '   51      53      54      56      57      58      