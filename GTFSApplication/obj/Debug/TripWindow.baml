   M S B A M L   `   `   `  ����J  FGTFSApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:GTFSApplicationGTFSApplication  5   	        GTFSApplication.TripWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:GTFSApplication     5   	     � Title$  Detaily spoja��5   	    � SizeToContent$ WidthAndHeight=�6    � 
ResizeMode$ NoResize=�6>   .��5	      � .+�5
   
   �� -   5        $
��Trip��5
      $��Dráha spoja ��6    $��30(�64   $��Segoe UI Light)�6B   $��
25,0,0,463q�6^   $
��Left=�6r   $	��422��6�   6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGridResult��5      $
��Left=�6+   $��	25,60,0,0q�6F    J� IsManipulationEnabled .     AutoGenerateColumns .  $��20(�5   0   $y�Visible=�6>      	RowHeight$ 40Q�6T      SelectionUnit$ FullRow=�6d      CanUserSortColumns .  	   CanUserAddRows	 .  
   RowHeaderWidth$
 0Q�5   I      CanUserResizeRows .     GridLinesVisibility$ 
Horizontal=�5   /   $	��310��6P   $	��400��6`      CanUserResizeColumns . $	��Top=�5   +      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   
IsReadOnly .    MaxWidth$ 30Q�6X   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *resultNumber+6X     5       .    Header$ 	Zástavka��6V      MinWidth$	 300Q�6h    6%   �� *
resultStop+6h     5       . $	 100Q�6^   $ 	Príchod ��6m    6%   �� *resultArrivalTime+6m   5      5      5      