   M S B A M L   `   `   `  ����?  ;HTOL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:HTOLHTOL  5      U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5          
HTOL.Graph   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      $localclr-namespace:HTOL     5      Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5        � 	Resources  5         CleanSeparator �� 
TargetType  LiveCharts.Wpf.Separator �� Property �� Value��%P   &	        �� 5   
   "  6'   .�5      ��  � {� .  6*   5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      .+�5   
   �� $��0��5      $��2q�5      ���	#FF1E1E1E �� CornerRadius$ 5��5      ._�5      #  LiveCharts.Wpf.CartesianChart  
�������&   LiveCharts.Wpf.Charts.Base.Chart   	Hoverable .     LegendLocation$	 Top=�5         AxisY 5         LiveCharts.Wpf.Axis  	   Title$	 
Voltage(V)��5!      
   
Foreground

 � � �   LabelFormatter 5#      �� *GraphVM.ValueFormatter+  5$      $	 
Current(A)��5%      

 �  ��   Position$ RightTop=�5(       5'      �� *GraphVM.ValueFormatter+   	Separator 5)        5*      #��[  6,   5,         AxisX 5/        $	 Time(HH:mm:ss)��50      

 ����� 52      �� *GraphVM.DateTimeFormatter+   MaxValue 53      �� *GraphVM.AxisXMax+   MinValue 54      �� *GraphVM.AxisXMin+ 55        56         Step 6,   �� *GraphVM.AxisXStep+58         Series 5;        LiveCharts.Wpf.LineSeries    LiveCharts.Wpf.Series   Title$ Voltage��5<         Fill
 ����    	ScalesYAt$ 0��5>         Stroke
 � � �   Values 5@      �� *GraphVM.Voltages+  5A      $ Current��5B      
 ���� $ 1��5D      
 �  �� 5F      �� *GraphVM.Currents+5H      5I      5p      5q      