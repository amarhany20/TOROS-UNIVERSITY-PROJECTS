   M S B A M L   `   `   `  ����I  ELibrary System, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Library_SystemLibrary_System  5   	       Library_System.Books   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:Library_System     5   	     � 
ResizeMode$  NoResize=�5   	    � Title$ Books��5	   	   $	��800��6   $
��1300��6$   .��5
      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ datagrid_BooksDisplay��5      $
��Left=�62   $	��424��6M   $��	10,51,0,0q�6Z   $	��Top=�6m   $
��1274��6�      
IsReadOnly .    SelectionMode$ Single=�6�   
�������6�   �� 5   
   -   5         ��Name$ button_TakeSelectedBook��5      $��Take Selected Book��62   $
��Left=�6O   $��
10,480,0,0q�6j   $	��Top=�6~   $	��131��6�   $��37��6�   
�������6�   �� 5   
   -   5        $! button_ReturnBackToMainMenu��5      $��Return Back To Main Menu��66   $
��Left=�6Y   $��1125,723,0,0q�6t   $	��Top=�6�   $	��159��6�   $��37��6�   
�������6�   �� 5   
   -   5         ��Name$ textbok_SearchBooks��5      $
��Left=�6/   $��34��6J   $��	10,10,0,0q�6V    �� TextWrapping$
 Wrap=�6i   $	��Top=�6}   $	��210��6�   
�������6�   �� 5   
   -   5        $ button_SearchBooks��5      $��Search��6-   $
��Left=�6>   $��
225,10,0,0q�6Y   $	��Top=�6m   $	��141��6�   $��36��6�    �� 	IsDefault . 
�������6�   �� 5   
   -   5        	 ��Name$	 checkbox_InStockOnly��5      $��Show in stock only��61   $
��Left=�6N   $��
371,21,0,0q�6i   $	��Top=�6}   $	��178��6�   
�������
�������6  �� 5   
   -   5        $"	 checkbox_SearchByFirstLetter��5      $"��Search title By First Letter��69   $
��Left=�6`   $��
554,21,0,0q�6{   $	��Top=�6�   $	��186��6�   
�������6  ��5   
   �� 5      
 �� ImageSource$/
 )books_shelf_graffiti_125860_1920x1080.jpg��6   5      5      