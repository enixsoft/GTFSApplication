   M S B A M L   `   `   `  ����Q  MWPFTextBoxAutoComplete, Version=1.0.0.2, Culture=neutral, PublicKeyToken=null_Dclr-namespace:WPFTextBoxAutoComplete;assembly=WPFTextBoxAutoCompleteWPFTextBoxAutoComplete  5   	   p lSystem.Windows.Controls.Input.Toolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35pTclr-namespace:System.Windows.Controls;assembly=System.Windows.Controls.Input.ToolkitSystem.Windows.Controls 5   	   J FGTFSApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:GTFSApplicationGTFSApplication 5   	       GTFSApplication.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Z	behaviorsDclr-namespace:WPFTextBoxAutoComplete;assembly=WPFTextBoxAutoComplete     5   	   htoolkitTclr-namespace:System.Windows.Controls;assembly=System.Windows.Controls.Input.Toolkit     5   	   /localclr-namespace:GTFSApplication     5   	     �Language$  sk-SK�5   *    � Title$ Hlavné okno��6	   $	��774��6   $
��1100��6*   .��5      � .+�5   
   ��  �� Orientation$ Vertical=�6   $��10,0,0,0q�6,   $
��Left=�6>   $	��170��6Y   .+�5      �� $��Editor súborov��6   $��21,20,19,20q�6.   $��20(�6C   $��Segoe UI Light)�6S   �� 5      -   5         ��Name$ buttonAgency��5      $��Agency��6+   $��40��6L   $��0,0,0,0q�6X   $��16(�6i   ��6w   Z� �� 5      -   5        $ buttonCalendar��5      $��Calendar��6-   $��40��6P   $��0,12,0,0q�6\   $��16(�6n   ��6|   Z� �� 5      -   5        $ buttonCalendarDates��5      $��CalendarDates��62   $��40��6Z   $��0,12,0,0q�6f   $��16(�6x   ��6�   Z� �� 5      -   5        $ buttonRoutes��5      $��Routes��6,   $��40��6M   $��0,12,0,0q�6Y   $��16(�6k   ��6y   Z� �� 5      -   5        $ buttonTrips��5      $��Trips��6*   $��40��6J   $��0,12,0,0q�6V   $��16(�6h   ��6v   Z� �� 5      -   5        $ buttonStops��5      $��Stops��6*   $��40��6K   $��0,12,0,0q�6W   $��16(�6i   ��6w   Z� �� 5      -   5        $ buttonStopTimes��5      $��	StopTimes��6.   $��40��6S   $��0,12,0,0q�6_   $��16(�6q   ��6   Z� 5      �� 5"   
   $��	200,0,0,0q�6   $ Vertical=�6(   $��Stretch=�6?   $	��392��6]   $	��Top=�6j   .+�5#      �� $��Vyhľadávanie��6   $��45��6,   $��
369,20,0,0q�68   $��30(�6L   $
��Left=�6Z   $	��185��6u   $��Segoe UI Light)�6�   �� 5$      $ 
Horizontal=�6   $��40��62   .+�5%      �� $��
Zdroj dát��6   $��24(�6-   $
��Left=�6;   $��20,0,0,0q�6V   $	��195��6h   $��Segoe UI Light)�6t   �� 5&      -   5         ��Name$ comboBox��5&   5   $	��266��6   $��24(�6'   $��40��6G   6S   �� 5'      -	   5        $ buttonDatabaseManager��5'      $��Manažér databáz��68   $��00,0,0,0q�6b   $��16(�6t   $��40��6�   ��6�   Z� 6�   5(      �� 5)      $ 
Horizontal=�6   $��0,10,0,0q�62   $��48��6D   .+�5*      �� $��Odkiaľ��6   $��20,0,0,0q�6*   $��24(�6<   $
��Left=�6J   $	��195��6e   $��Segoe UI Light)�6q   -  'System.Windows.Controls.AutoCompleteBox  5+      -
   5          Name$ 
searchFrom��5+   +   $	��266��6A   $��24(�6M   $��48��6[      ItemsSource 6g   �� *Stops+   SelectedItem 6�   �� *SelectedStop+ �� Mode$ TwoWay=��� 5,      -   5        	 ��Name$	 searchFromError��5,   <   $!��Zástavka nebola nájdená.��6   $��20,0,0,0q�6U   
���  ��$��20(�6x   $y�Hidden=�6�   $	��390��6�   $��Segoe UI Light)�6�   5.      �� 5/      $ 
Horizontal=�6   $��0,10,0,0q�62   $��50��6D   .+�50      �� $	��Kam��6   $��20,1,0,0q�6'   $��24(�69   $
��Left=�6G   $	��195��6b   $��Segoe UI Light)�6n     51      -   5        $ searchTo��51   +   $	��266��6?   $��24(�6K   $��48��6Y    6e   �� *Stops+ 6�   �� *SelectedStop+$ TwoWay=��� 52      -   5        $��1,0,0,0q�52         SwapImageStyle#��[  6*   $��70��6R   $��1q�6p   
 � Cursor$

 Hand��6�   �� 53      -   5        $	 searchToError��53   <   $!��Zástavka nebola nájdená.��6   $��20,0,0,0q�6S   
���  ��$��20(�6v   $y�Hidden=�6�   $	��273��6�   $��Segoe UI Light)�6�   54      �� 55      $ 
Horizontal=�6   $��59��62   $��0,10,0,0q�6>   .+�57      �� $��Dátum a čas��6   $��	20,0,0,10q�6/   $��24(�6B   $
��Left=�6P   $	��195��6k   $��Segoe UI Light)�6w   (  "System.Windows.Controls.DatePicker  59      -   5          Name$ searchDatePicker��59   7   $	��266��6   $��24(�6)   $��0,0,0,10q�6Q   $��48��6c     DatePickerStyle1#���  6o      FirstDayOfWeek$ Monday=�6�   5J      �� 5K      -   5         ��Name$ searchTimeTextBox��5K      $��32(�65   $��90��6C   $��48��6N   $��Segoe UI Light)�6Z   $��0,0,0,11q�6v     TextBoxStyleWatermark#��[ 6�   
 � Tag$ Teraz��6�    �� 	MaxLength$ 5��5L   E   $
 IBeam��6�   �� 5M      $ Vertical=�6   $��45��64   �� 5N      -   5        $	 searchTimeError��5N   9   $��Nesprávne zadaný čas.��6   $��	10,0,0,11q�6R   
���  ��$��16(�6v   $y�Hidden=�6�   $	��246��6�   $��Segoe UI Light)�6�   $��38��6�   $��Bottom=�6�   5Q      �� 5R      -   5        $	 searchError��5R   E   $0��*Vyhľadávanie nenašlo žiadne výsledky.��6   $��	270,0,0,0q�6Z   
���  ��$��22(�6~   $y�Hidden=�6�   $��Segoe UI Light)�6�   $��30��6�   $��Bottom=�6�   �� 5T      $ 
Horizontal=�6   $��54��62   $��0,10,0,0q�6>   .+�5U      �� -   5        $ previousButton��5U   @   $��	160,0,0,0q�6   $y�Hidden=�6,   $��Predchádzajúce��6X   ���	#FF9ECAF5$��24(�6�   $
��Bold"�6�   $	��185��6�   $��44��6�   $��Bottom=�6�   �� 5V      -   5        $��searchButton��5V   +   $��16,0,0,0q�6   $��
VYHĽADAŤ��6?   ���	#FF9ECAF5$��32(�6i   $
��Bold"�6w   $	��171��6�   $

 Hand��6�     SearchButtonStyle1#���  6�   �� 5W      -   5        $ 
nextButton��5W   +   $��16,0,0,0q�6   $y�Hidden=�6?   $��Nasledujúce��6S   ���	#FF9ECAF5$��24(�6�   $
��Bold"�6�   $	��161��6�   $��44��6�   $��Bottom=�6�   6�   5X      �� 5Y      $��0,5,0,0q�6   
�������
�������$��1q�5\      5^      �� 5_   
   $��200,421,0,0q�6   .+�5`      �� $ Vertical=�6   .+�5b      &   System.Windows.Controls.DataGrid  -   5          Name$ dataGridResult��5b      $
��Left=�63   $��0,0,0,0q�6N    J� IsManipulationEnabled .  $	��Top=�6~      AutoGenerateColumns .  $��20(�5c   0   $y�Hidden=�6>      	RowHeight$ 40Q�6S   $	��800��6b      SelectionUnit$
 Cell=�6q      CanUserSortColumns .     CanUserAddRows .     RowHeaderWidth$ 0Q�5d   I      CanUserResizeRows .     GridLinesVisibility$ 
Horizontal=�5e   /      CanUserResizeColumns .    Columns 5h      0  *System.Windows.Controls.DataGridTextColumn  5i      ,  &System.Windows.Controls.DataGridColumn   
IsReadOnly .    Header$
 Čas��6g      
FontWeight$
 Bold"�6t   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6-   �� *resultDepartureTime+6t     5j       . $ Dátum��6^    6-   �� *
resultDate+6^     5k       . $ Odkiaľ��6^    6-   �� *
resultStop+6^     5l       . $
 Kam ��6b    6-   �� *resultNextStop+6b     5m       . $ Odchod��6g    6-   �� *resultDepartureTime+6g     5n       . $ 	Príchod ��6e    6-   �� *resultArrivalTime+6e   4  .System.Windows.Controls.DataGridTemplateColumn  5o       . $ Spoj ��6D       CellTemplate  5p      �� 5q   "   .R�5r   &   �� -   5        $��0q�5r   S   ��6-   �� *resultTripNumber+ 6g   �� *resultTripNumber+6�   5s   #   5u      !   	CellStyle! 5x      �� 5y      " �� 
TargetType*  $System.Windows.Controls.DataGridCell""  6    # �� Triggers# 5z      P� 5{   "   $ P� Property$ �@ 
IsSelected% P� Value% . . �5|   &   �� & �� Property& � ��' �� Value' 5}   *   �� 5~   .   ( �� Color$( Transparent��6>   5�   '   �� 5�   &   & � ��#' � ��5�      �� 5�   &   & � ��' 5�   *   �� 5�   .   $( Transparent��6>   5�   '   5�   #   5�      5�      5�      5�      �� 5�   
   -   5        $��loadingImage��5�      $8��2/GTFSApplication;component/Resources/sandClock.png��6$   $��613,465,0,162q�6`   $
��Left=�6w   $��70��6�   $y�Hidden=�6�   �� 5�   
   -   5        $
��page��5�   ?   $y�Hidden=�6   $��PageNumberDebug��6(   $��24(�6K   $��47,465,959,243q�6Y   6s   5�      ) � 	Resources) 5�      ��%m   '��       �� 5�   
   "" ��6   .�5�      �� & �@��BitmapScalingMode$' HighQuality=�6@   5�      5�      5�      