   M S B A M L   `   `   `  ���� D  @Sales_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Sales_WPF	Sales_WPF  U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf     Sales_WPF.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:Sales_WPF     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  >lvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf    � Title$  
MainWindow��$	��800��$
��1200�� � DataContext   Sales_WPF.MainViewModel  .��� .+��� -   $��MainTab��.@��� -   $��TabProducts��$��PRODUKTY��$��17(�
�����F� �� *Children[0]+.��� 
������� � ColumnDefinitions �� $
��125* ��� $��1061* � � RowDefinitions �� $��60 ��� $��* �.+��� -    ��Name$ 
stkProduct�� �� Orientation$ 
Horizontal=�$��Stretch=�
����İ�$��0��$��2��.+��� -    ��Name$ cmbProducer��$��30��$
��Auto��$	��200��$��17(�$	��Top=�$��
10,15,10,0q� �� DisplayMemberPath$ ProducerName�� �� SelectedItem �� *selectedProducer+	 �� Mode$	 TwoWay=����� *ListProducers+�� -   
 ��Name$
 txtProductName��$
��Auto��$��17(�$��30��$��Center=�$��0,0,10,0q�$	��200�� � ToolTip$ Nazwa������ *newProduct.ProductName+$	 TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� -   $
 txtProductCode��$
��Auto��$��17(�$��30��$��Center=�$��0,0,10,0q�$	��120��$	 Kod������ *newProduct.ProductCode+$	 TwoWay=�$ PropertyChanged=��� -   $��txtProductPrice��
����  � � 
InputScope$ Digits��$��17(�$��
ExtraLight"�$��30��$��50��$��0,0,10,0q�$
 Cena������ *newProduct.Price+$	 TwoWay=�$ PropertyChanged=��� -   $��txtProductCost��
���<��$ Digits��$��17(�$��0,0,10,0q�$��
ExtraLight"�$��30��$��50��$ Koszt������ *newProduct.Prod+$	 TwoWay=�$ PropertyChanged=� �� StringFormat$ N1���� -	   $��txtProductTax��
���<��$ Digits��$��17(�$��0,0,10,0q�$��
ExtraLight"�$��30��$��50��$ Podatek������ *newProduct.Tax+$	 TwoWay=�$ PropertyChanged=��� -
    ��Name$ btnAddProduct��$��30��$��Dodaj��$	��100������ *AddProductCommand+&   System.Windows.Controls.DataGrid  -     Name$ dtgProducts��   SelectionMode$ Single=�$��Light"�$��1��$��
10,5,10,10q�$
��Auto��$��20(�   AutoGenerateColumns .     CanUserAddRows .     HorizontalGridLinesBrush
 �����   VerticalGridLinesBrush
 �����$��2�� �� *selectedProduct+���� *ListProducts+$	 TwoWay=�$ PropertyChanged=� J� InputBindings �� 
 �� Key$ Delete�� �� Command �� *DelProductCommand+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 	Producent��   
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Producer.ProducerName+$	 TwoWay=�$ PropertyChanged=�   HeaderStyle ��  �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  �� Property � ��  �� Value
  ���F���  � ��$  	10,0,10,0q���  � ��$  1q���  � ��
  �����!   	CellStyle! �� *  $System.Windows.Controls.DataGridCell"    $ Nazwa��"   
Visibility$" Visible=�#   Width5  /System.Windows.Controls.DataGridLengthConverter$# SizeToCells  �� *ProductName+$	 TwoWay=�$ PropertyChanged=� �� "  .���  � ��
  ���F���  � ��$  	10,0,10,0q���  � ��$  1q���  � ��
  �����  $	 Kod��$" Visible=�$# 70  �� *ProductCode+$	 TwoWay=�$ PropertyChanged=� �� "  .���  � ��
  �� ���  � ��$  	10,0,10,0q���  � ��$  1q���  � ��
  �����  $
 Cena��$" Visible=�$# 70  �� *Price+$	 TwoWay=�$ PropertyChanged=� �� "  .���  � ��
  �\\����  � ��$  	10,0,10,0q���  � ��$  1q���  � ��
  �����  $ Koszt��$" Visible=�$# 70  �� *Prod+$	 TwoWay=�$ PropertyChanged=� �� "  .���  � ��
  �� ���  � ��$  	10,0,10,0q���  � ��$  1q���  � ��
  �����  $ Podatek��$" Visible=�$# 90  �� *Tax+$	 TwoWay=�$ PropertyChanged=� �� "  .���  � ��
  �� ���  � ��$  	10,0,10,0q���  � ��$  1q���  � ��
  �����  $
 Stan��$" Visible=�$# 90  �� *ProductStock+$	 TwoWay=�$ PropertyChanged=� �� "  .���  � ��
  �� ���  � ��$  	10,0,10,0q���  � ��$  1q���  � ��
  ������� $��
PRODUCENCI��$��17(�
�����F� �� *Children[1]+.��� 
������� �� $
��125* ��� $��1061* � �� $��60 ��� $��* �.+��� -   $ stkProducer��$ 
Horizontal=�$��Stretch=�
����İ�$��0��$��2��.+��� -   $
 txtProducerName��$
��Auto��$��17(�$��30��$��Center=�$��	10,0,10,0q�$	��200������ *newProducer.ProducerName+$	 TwoWay=�$ PropertyChanged=��� -   $ btnAddProducer��$��30��$��Dodaj��$	��100������ *AddProducerCommand+� -   $ �Name$$ dtgProducers��$��1��$��
10,5,10,10q�$��2��.+��� ���� *ListProducers+$	 TwoWay=�$ PropertyChanged=����� .R��� -   $��5,5,5,5q�$��25(�$��60��
�����F�
����������� *ProducerName+$	 TwoWay=�$ PropertyChanged=����� .R�� �� $��SKLEP��$��17(�
���\\�� �� *Children[2]+.��� 
����İ� �� $��80 ��� $��* ��� $��30 � �� $	��400 ��� $��* �.+��� -   $ 	stkEvents��$ 
Horizontal=�$��Center=�
����İ�$��0��$��0��.+�(	  "System.Windows.Controls.DatePicker	  -   % 	 Name$% 
datePicker��$��30��$��0,0,20,0q�& 	  SelectedDate& �� *
EventDay+$	 OneWayToSource=�$ PropertyChanged=��� -   $ btnAddEvent��$��30��$��Dodaj��$	��100������ *AddEventCommand+�� -   $ stkSale��$ 
Horizontal=�$��Center=�
����İ�$��0��$��1��.+��� -   $ cmbProducts��$��30��$
��Auto��$	��200��$��17(�$��Center=�$��	10,0,10,0q�$ FullName�� �� *selectedProduct+$	 TwoWay=����� *ListProducts+�� -   $��txtSalePrice��
����  �$ Digits��$��17(�$��
ExtraLight"�$��30��$��50��$��0,0,10,0q����� *	SalePrice+$ PropertyChanged=��� -   $��
txtSaleQty��
���<��$ Digits��$��0,0,10,0q�$��
ExtraLight"�$��30��$��50������ *	SaleQty+$ PropertyChanged=��� -   $��txtProdStockInfo��
���2͚�$��2q�
��� ���
�����F�$��Center=�' �� 
IsReadOnly' . $ Digits��$��0,0,10,0q�$��
ExtraLight"�$��30��$��50������ *	ProdStock+$ PropertyChanged=�$	 TwoWay=��� -   $��chkRegister��
���<��$��Center=�$��0,0,10,0q�$��
ExtraLight"�$��30��$��30��( d� 	IsChecked( �� *SaleRegister+$ PropertyChanged=��� -   $ 
btnAddSale��$��30��$��Dodaj��$	��100������ *AddSaleCommand+�� -   ) ��Name$) lstShopDays��$��5,5,0,10q�$
��Auto��
����� �$��Light"�$��20(�$ 	EventName��$��0��$��1�� �� *selectedEvent+$	 TwoWay=����� *ListShopDays+ �� $ Delete�� �� *DeleteEventCommand+  -   $ dtgShopSales��$ Single=�$��Light"�$��1��$��1��$��
10,5,10,10q�$
��Auto��$��20(� .   .  
 �����
 ����� �� *selectedSale+���� * �� ElementName$* lstShopDays��+ �� Path$+ SelectedItem.ListSales�.
  (System.Windows.Interactivity.Interaction, 
  Triggers, /  )System.Windows.Interactivity.EventTrigger  -   	EventName$- CellEditEnding��.  (System.Windows.Interactivity.TriggerBase.   Actions.. 6  0System.Windows.Interactivity.InvokeCommandAction  /   Command/ �� *EditCommand+ �� $ Delete�� �� *DeleteSaleCommand+   $ Produkt�� .  �� *	Product+ �� "  .���  � ��
  ���F���  � ��$  	10,0,10,0q���  � ��$  1q���  � ��
  �����! �� "    $
 Cena��$" Visible=�$# 70  �� *Price+$	 TwoWay=�$ PropertyChanged=� �� "  .���  � ��
  ���F���  � ��$  	10,0,10,0q���  � ��$  1q���  � ��
  �����  $ Ilość��$" Visible=�$# 70  �� *Qty+$	 TwoWay=�$ PropertyChanged=� �� "  .���  � ��
  �\\����  � ��$  	10,0,10,0q���  � ��$  1q���  � ��
  �����  $
 Kasa��$" Visible=�$# 70  �� *
IsRegister+$	 TwoWay=�$ PropertyChanged=� �� "  .���  � ��
  �\\����  � ��$  	10,0,10,0q���  � ��$  1q���  � ��
  ������� -   0 ��Name$0 txbSalesSum��$��2��$��1��$��Right=�$��0,0,20,0q����� *	SaleSum+$	 TwoWay=�$ PropertyChanged=��� $��RAPORTY��$��17(�
���2͚� �� *Children[3]+.��� 
������� �� $
��Auto ��� $��30 ��� $��* � �� $
��Auto ��� $
��Auto ��� $��* �.+��� -   $ 	stkParams��$��0��$��1��$��20,10,20,20q��� -   $ btnGenerateReport��$y�	Collapsed=�$��Generuj��$��1��$��1��$	��100��$��20,0,0,0q��� -    $) lstCustomReports��$y�Visible=�$��	10,0,10,0q�$��0��$��0��$��20,10,20,20q�$��3��
�������
���   �$��0q�$��20(�$��
UltraLight"�$
 Name������ *ListReports+1 � 	Resources1 2 �� 
TargetType3 ��Name4 �� Padding5 J� SnapsToDevicePixels6 �� Triggers7 ]� 
Conditions8 �� Property9 �� Value: �� 
TargetName��%�   '��       �� " ��.���  � ��  �� "2 ��.R��� $3 Bd��#��z��#��z��#��z��#4 z��5 . ._��� #��z��#5 z5 #��z��6 ]� 7 �� 8 �@��IsSelectionActive9 . �� 8 �@��
IsSelected9 . ./��� $: Bd�� � ��
  � E��  -!   $ dtgCustomReport��$y�	Collapsed=�$��10,10,40,40q�;   EnableRowVirtualization; .  <   CanUserResizeColumns< . $��1��$��2��$��17(�=   
IsReadOnly= .  . >   ColumnWidth$
> Auto ���� #  LiveCharts.Wpf.CartesianChart  -"   ?  Name$? ReportChart��&   LiveCharts.Wpf.Charts.Base.Chart@   LegendLocation$
@ None=�$��2��$��2��$y�	Collapsed=�A   SeriesA �� B   AxisYB   LiveCharts.Wpf.Axis  -#   C  Name$C ReportChartValue��D   
ForegroundD �#2e3033E   FontSize$E 15Q�F   AxisXF   -$   $C ReportChartLabels��G   LabelsRotation$	G 270Q�$E 15Q�
D �\\��H   LabelsH �� 