   M S B A M L   `   `   `  ���� ?  ;HTOL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:HTOLHTOL      HTOL.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     $localclr-namespace:HTOL     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$
  HTOL��$
��1200��$	��800��
���   � � 	Resources  �� 
TargetType �� Property �� Value   FocusVisual J� SnapsToDevicePixels �� StrokeDashArray ! RadioButton.Static.Background �� Color  RadioButton.Static.Border  OptionMarkFocusVisual $  RadioButton.MouseOver.Background   RadioButton.MouseOver.Border  RadioButton.MouseOver.Glyph # RadioButton.Disabled.Background  RadioButton.Disabled.Border 	 RadioButton.Disabled.Glyph "
 RadioButton.Pressed.Background  RadioButton.Pressed.Border  RadioButton.Pressed.Glyph  RadioButton.Static.Glyph  RadioButtonStyle �� 
TargetType	 �Name
 ��Name �� CornerRadius ��Name �� Triggers P� Property P� Value �� 
TargetName��%�  '��       &	  %     &	 s     &	 �     &	 �     &	 �     &	     &	     &	 1    &	 F    &		 [    &	
 p    &	 �    &	 �    &	 �    &	 �    7   7  7  7  7  7  7  7  7 
 7  �� " ��.���  � ��
 ������� .���  � �� �� .R��� $��2q� . #��� ��$	 1 2R�$��1���� $ 	#FFFFFFFF���� $ 	#FF707070���� .���  � �� �� .R��� $��14,0,0,0q� . #��� ��$	 1 2R�$��1���� $ 	#FFF3F9FF���� $ 	#FF5593FF���� $ 	#FF212121���� $ 	#FFE6E6E6���� $ 	#FFBCBCBC���� $ 	#FF707070���� $ 	#FFD9ECFF���� $ 	#FF3C77DD���� $ 	#FF212121���� $ 	#FF212121���� " 	�.���  �@�FocusVisualStyle8   ��  � ��8  ��  � ��8  ��  � ��
 �������  � ��$ 1q���  � ��$ Center=���  � ��$ Center=���  � �� �� " 	�.R�� $	 templateRoot��
������  . .+��� $
 radioButtonBorder��$��1,1,2,1q�#��z��#��z��#��z��#��z��#��z��$ 5��._��� $ contentPresenter��#��z��#��z��#��z��|�.  ��. # z  P�  � �� . . ��� 8   �@�FocusVisualStyle��  � ��$ 4,-1,0,0q�P�  �@J�IsMouseOver . . ��� $ radioButtonBorder��8   � ���� $ radioButtonBorder��8   � ��P�  � {� .  . ��� $ radioButtonBorder��8   � ���� $ radioButtonBorder��8   � ��P�  � �� . . ��� $ radioButtonBorder��8   � ���� $ radioButtonBorder��8 	  � ��P�  �@d�	IsChecked$
 trueY�. ��� $ radioButtonBorder�� � ��
 � � �P�  �@d�	IsChecked$ FalseY�. ��� $ radioButtonBorder�� � �� �	#FF1E1E1E.���  � ColumnDefinitions �� $��* � � RowDefinitions �� $
��auto ��� $��* ��� $��* �.+��� $��0��$��0��$��0,5,0,5q�._�]� ��.  .+���  �� Orientation$ 
Horizontal=�.+�	� $
��auto��$��5q�$��Connect��#���  ���� *
ConnectCmd+	� $
��auto��$��5q�$��
Disconnect�� d� 	IsChecked$
 TrueY�#���  ���� *DisconnectCmd+�� $��Right=�$ 
Horizontal=�.+��� $��30��$��30�����	#FF1E1E1E$ 5��._��� $��Center=�$��Center=�$
��Stop���� $��30��$��30��$��5,0,0,0q�
��� � �$ 5��._��� $��Center=�$��Center=�$	��Run���� $��30��$��30��$��5,0,0,0q�
���  ��$ 5��._��� $��Center=�$��Center=�$��Error���� $��30��$��30��$��5,0,0,0q�
���  ��$ 5��._��� $��Center=�$��Center=�$
��Lost��   
HTOL.Sites  $��1��$��0��   
HTOL.Graph  $��2��$��0��