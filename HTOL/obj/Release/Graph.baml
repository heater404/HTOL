   M S B A M L   `   `   `  ���� ?  ;HTOL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:HTOLHTOL  U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf     
HTOL.Graph   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     $localclr-namespace:HTOL     Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � 	Resources     CleanSeparator �� 
TargetType  LiveCharts.Wpf.Separator �� Property �� Value��%+   &	        �� "  .���  � {� .  .���  � ColumnDefinitions �� .+��� $��0��$��2q����	#FF1E1E1E �� CornerRadius$ 5��._�#  LiveCharts.Wpf.CartesianChart  
�������&   LiveCharts.Wpf.Charts.Base.Chart   	Hoverable .     LegendLocation$	 Top=�   AxisY   LiveCharts.Wpf.Axis  	   Title$	 
Voltage(V)��
   
Foreground

 � � �   LabelFormatter �� *GraphVM.ValueFormatter+  $	 
Current(A)��

 �  ��   Position$ RightTop=� �� *GraphVM.ValueFormatter+   	Separator   #��[     AxisX   $	 Time(HH:mm:ss)��

 ����� �� *GraphVM.DateTimeFormatter+   MaxValue �� *GraphVM.AxisXMax+   MinValue �� *GraphVM.AxisXMin+      Step �� *GraphVM.AxisXStep+   Series   LiveCharts.Wpf.LineSeries    LiveCharts.Wpf.Series   Title$ Voltage��   Fill
 ����    	ScalesYAt$ 0��   Stroke
 � � �   Values �� *GraphVM.Voltages+  $ Current��
 ���� $ 1��
 �  �� �� *GraphVM.Currents+