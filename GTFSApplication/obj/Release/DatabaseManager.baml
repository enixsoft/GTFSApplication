   M S B A M L   `   `   `  ���� J  FGTFSApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:GTFSApplicationGTFSApplication  %    GTFSApplication.DatabaseManager   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:GTFSApplication     -     � Title$  Manažér databáz��$	��800��$
��1079�� � 	Resources  �� 
TargetType �� Property �� Value��%H   '��       �� " ��.���  �@��BitmapScalingMode$ HighQuality=�.��� .+�]� $��Stretch=�.+�`� $	��Top=� `� 
Background# � �� `� IsLocked . .�c�  c� OverflowMode$ Never=�   ToolBarStyle1#���   $��185,0,-185,0q���Z� .@��� -    � ToolTip$ Vytvorí novú databázu.��$��Stretch=�$��
10,00,10,0q�.���� 	 �� Orientation$	 
Horizontal=�.+��� $<��6/GTFSApplication;component/Resources/document_text.png��$��50��$��50���� $��10,0,0,0q�$��Center=�$��Center=�$��18(�$��Segoe UI Light)�Nová databáza�� -   $��Previous��$) #Cut selection to Windows Clipboard.��$��Stretch=�.���� $	 
Horizontal=�.+��� $3��-/GTFSApplication;component/Resources/Left.png��$��50��$��50���� -   
 ��Name$
 comboBox��$��Stretch=�$	��170��$��5,0,5,0q�$��16(�$��Segoe UI Light)�$��50��$
��Bold"����� *DatabaseList+�� -   $
��Next��$) #Cut selection to Windows Clipboard.��$��Stretch=�.���� $	 
Horizontal=�.+��� $4��./GTFSApplication;component/Resources/Right.png��$��50��$��50���� -   $��
taskbutton��$) #Cut selection to Windows Clipboard.��$��Stretch=�$��
10,00,10,0q�.���� $	 
Horizontal=�.+��� -   $��taskbuttonImage��$6��0/GTFSApplication;component/Resources/refresh.png��$��50��$��50���� -   $��taskbuttonText��$��10,0,0,0q�$��Center=�$��Center=�$��18(�$��Segoe UI Light)�Aktualizovať databázu�� $	 Vertical=�.+��� $	 
Horizontal=�.+��� $��	39,72,0,0q�$��Obsah textových súborov��$��48��$��Segoe UI Light)�$��20(�$
��Left=�$	��500���� $��	59,72,0,0q�$��Obsah databázy��$��48��$��Segoe UI Light)�$��20(�$
��Left=�$	��475���� $	 
Horizontal=�.+�&   System.Windows.Controls.DataGrid  -	     Name$ dataGridFiles��$
��Left=�$��37,0,0,0q� J� IsManipulationEnabled .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ *    SelectionUnit$
 Cell=�$	��Top=�   AutoGenerateColumns .  $	��500��$��14(�$y�Visible=�   	RowHeight$ 30Q�   CanUserSortColumns .     CanUserAddRows .     RowHeaderWidth$ 0Q�   CanUserResizeRows .     CanUserResizeColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   
IsReadOnly .    Header$ Súbor��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *DBFilesEntityNames+   . $ Počet riadkov�� �� *DBFilesEntityRows+�� $	 Vertical=�$��	20,0,0,0 q�.+�  -
   $ dataGridDatabase��$
��Left=�$��37,0,0,0q� . $ * $
 Cell=�$	��Top=� .  $	��460��$��14(�$y�Visible=�$ 30Q� .   .  $ 0Q� .   .  ����  �� Path$ .� �� Mode$ OneWay=� �� NotifyOnSourceUpdated .  �� UpdateSourceTrigger$ PropertyChanged=�    . $ Tabuľka�� �� *DBTablesNames+$ TwoWay=�$ PropertyChanged=�   . $ Počet riadkov�� �� *DBTablesRows+$ TwoWay=�$ PropertyChanged=�4  .System.Windows.Controls.DataGridTemplateColumn   . $
 Stav��   CellTemplate �� .R�� .+�� $��ProgressBar�� � Minimum$ 0Q�  � Maximum$	  100Q�! � Value! �� $ ProgressValue�$ TwoWay=��� $��Center=�$��Center=����� " �� ElementName$" ProgressBar��$ Value�# �� StringFormat$# {0:0}%���� -   $��lastModified��$��Naposledy aktualizované:��$��16(�$��Segoe UI Light)�$��35,0,0,0q�$	��457��