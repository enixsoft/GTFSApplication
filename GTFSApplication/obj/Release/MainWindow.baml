   M S B A M L   `   `   `  ���� Q  MWPFTextBoxAutoComplete, Version=1.0.0.2, Culture=neutral, PublicKeyToken=null_Dclr-namespace:WPFTextBoxAutoComplete;assembly=WPFTextBoxAutoCompleteWPFTextBoxAutoComplete  p lSystem.Windows.Controls.Input.Toolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35pTclr-namespace:System.Windows.Controls;assembly=System.Windows.Controls.Input.ToolkitSystem.Windows.Controls J FGTFSApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:GTFSApplicationGTFSApplication     GTFSApplication.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Z	behaviorsDclr-namespace:WPFTextBoxAutoComplete;assembly=WPFTextBoxAutoComplete     htoolkitTclr-namespace:System.Windows.Controls;assembly=System.Windows.Controls.Input.Toolkit     /localclr-namespace:GTFSApplication       �Language$  sk-SK� � Title$ Hlavné okno��$	��774��$
��1100��.��� .+���  �� Orientation$ Vertical=�$��10,0,0,0q�$
��Left=�$	��170��.+��� $��Editor súborov��$��21,20,19,20q�$��20(�$��Segoe UI Light)��� -    ��Name$ buttonAgency��$��Agency��$��40��$��0,0,0,0q�$��16(���Z� �� -   $ buttonCalendar��$��Calendar��$��40��$��0,12,0,0q�$��16(���Z� �� -   $ buttonCalendarDates��$��CalendarDates��$��40��$��0,12,0,0q�$��16(���Z� �� -   $ buttonRoutes��$��Routes��$��40��$��0,12,0,0q�$��16(���Z� �� -   $ buttonTrips��$��Trips��$��40��$��0,12,0,0q�$��16(���Z� �� -   $ buttonStops��$��Stops��$��40��$��0,12,0,0q�$��16(���Z� �� -   $ buttonStopTimes��$��	StopTimes��$��40��$��0,12,0,0q�$��16(���Z� �� $��	200,0,0,0q�$ Vertical=�$��Stretch=�$	��392��$	��Top=�.+��� $��Vyhľadávanie��$��45��$��
369,20,0,0q�$��30(�$
��Left=�$	��185��$��Segoe UI Light)��� $ 
Horizontal=�$��40��.+��� $��
Zdroj dát��$��24(�$
��Left=�$��20,0,0,0q�$	��195��$��Segoe UI Light)��� -    ��Name$ comboBox��$	��266��$��24(�$��40���� -	   $ buttonDatabaseManager��$��Manažér databáz��$��00,0,0,0q�$��16(�$��40����Z� �� $ 
Horizontal=�$��0,10,0,0q�$��48��.+��� $��Odkiaľ��$��20,0,0,0q�$��24(�$
��Left=�$	��195��$��Segoe UI Light)�-  'System.Windows.Controls.AutoCompleteBox  -
     Name$ 
searchFrom��$	��266��$��24(�$��48��   ItemsSource �� *Stops+   SelectedItem �� *SelectedStop+ �� Mode$ TwoWay=��� -   	 ��Name$	 searchFromError��$!��Zástavka nebola nájdená.��$��20,0,0,0q�
���  ��$��20(�$y�Hidden=�$	��390��$��Segoe UI Light)��� $ 
Horizontal=�$��0,10,0,0q�$��50��.+��� $	��Kam��$��20,1,0,0q�$��24(�$
��Left=�$	��195��$��Segoe UI Light)�  -   $ searchTo��$	��266��$��24(�$��48�� �� *Stops+ �� *SelectedStop+$ TwoWay=��� -   $��1,0,0,0q�   SwapImageStyle#��[  $��70��$��1q�
 � Cursor$

 Hand���� -   $	 searchToError��$!��Zástavka nebola nájdená.��$��20,0,0,0q�
���  ��$��20(�$y�Hidden=�$	��273��$��Segoe UI Light)��� $ 
Horizontal=�$��59��$��0,10,0,0q�.+��� $��Dátum a čas��$��	20,0,0,10q�$��24(�$
��Left=�$	��195��$��Segoe UI Light)�(  "System.Windows.Controls.DatePicker  -     Name$ searchDatePicker��$	��266��$��24(�$��0,0,0,10q�$��48��  DatePickerStyle1#���     FirstDayOfWeek$ Monday=��� -    ��Name$ searchTimeTextBox��$��32(�$��90��$��48��$��Segoe UI Light)�$��0,0,0,11q�  TextBoxStyleWatermark#��[ 
 � Tag$ Teraz�� �� 	MaxLength$ 5��$
 IBeam���� $ Vertical=�$��45���� -   $	 searchTimeError��$��Nesprávne zadaný čas.��$��	10,0,0,11q�
���  ��$��16(�$y�Hidden=�$	��246��$��Segoe UI Light)�$��38��$��Bottom=��� -   $	 searchError��$0��*Vyhľadávanie nenašlo žiadne výsledky.��$��	270,0,0,0q�
���  ��$��22(�$y�Hidden=�$��Segoe UI Light)�$��30��$��Bottom=��� $ 
Horizontal=�$��54��$��0,10,0,0q�.+��� -   $ previousButton��$��	160,0,0,0q�$y�Hidden=�$��Predchádzajúce�����	#FF9ECAF5$��24(�$
��Bold"�$	��185��$��44��$��Bottom=��� -   $��searchButton��$��16,0,0,0q�$��
VYHĽADAŤ�����	#FF9ECAF5$��32(�$
��Bold"�$	��171��$

 Hand��  SearchButtonStyle1#���  �� -   $ 
nextButton��$��16,0,0,0q�$y�Hidden=�$��Nasledujúce�����	#FF9ECAF5$��24(�$
��Bold"�$	��161��$��44��$��Bottom=��� $��0,5,0,0q�
�������
�������$��1q��� $��200,421,0,0q�.+��� $ Vertical=�.+�&   System.Windows.Controls.DataGrid  -     Name$ dataGridResult��$
��Left=�$��0,0,0,0q� J� IsManipulationEnabled .  $	��Top=�   AutoGenerateColumns .  $��20(�$y�Hidden=�   	RowHeight$ 40Q�$	��800��   SelectionUnit$
 Cell=�   CanUserSortColumns .     CanUserAddRows .     RowHeaderWidth$ 0Q�   CanUserResizeRows .     GridLinesVisibility$ 
Horizontal=�   CanUserResizeColumns .    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   
IsReadOnly .    Header$
 Čas��   
FontWeight$
 Bold"�1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *resultDepartureTime+   . $ Dátum�� �� *
resultDate+   . $ Odkiaľ�� �� *
resultStop+   . $
 Kam �� �� *resultNextStop+   . $ Odchod�� �� *resultDepartureTime+   . $ 	Príchod �� �� *resultArrivalTime+4  .System.Windows.Controls.DataGridTemplateColumn   . $ Spoj ��    CellTemplate  �� .R��� -   $��0q����� *resultTripNumber+ �� *resultTripNumber+!   	CellStyle! �� " �� 
TargetType*  $System.Windows.Controls.DataGridCell""  # �� Triggers# P� $ P� Property$ �@ 
IsSelected% P� Value% . . ��� & �� Property& � ��' �� Value' �� ( �� Color$( Transparent���� & � ��#' � ���� & � ��' �� $( Transparent���� -   $��loadingImage��$8��2/GTFSApplication;component/Resources/sandClock.png��$��613,465,0,162q�$
��Left=�$��70��$y�Hidden=��� -   $
��page��$y�Hidden=�$��PageNumberDebug��$��24(�$��47,465,959,243q�) � 	Resources) ��%H   '��       �� "" ��.��� & �@��BitmapScalingMode$' HighQuality=�