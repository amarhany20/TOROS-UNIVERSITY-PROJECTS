   M S B A M L   `   `   `  ����I  ELibrary System, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Library_SystemLibrary_System  5   	       Library_System.AllBooks   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:Library_System     5   	     � 
ResizeMode$  NoResize=�5   	    � Title$ AllBooks��5	   	   $	��800��6   $
��1300��6'   .��5
      � .+�5   
   �� -   5         ��Name$ textbox_Search��5      $
��Left=�6*   $��30��6E   $��	10,10,0,0q�6Q    �� TextWrapping$
 Wrap=�6d   $	��Top=�6x   $	��230��6�   
�������6�   �� 5   
   -   5         ��Name$ button_Search��5      $��Search��6(   $
��Left=�69   $��
245,10,0,0q�6T   $	��Top=�6h   $	��110��6�   $��30��6�    �� 	IsDefault . 
�������6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ datagrid_AllBooks��5      $
��Left=�6.   $	��455��6I   $��	10,45,0,0q�6V   $	��Top=�6i   $
��1274��6�   
�������   SelectionMode$ Single=�6�   �� 5   
   -   5        $ button_Update��5      $��Update��6(   $
��Left=�69   $��
10,505,0,0q�6T   $	��Top=�6h   $	��110��6�   $��30��6�   
�������6�   �� 5   
   -   5        $ textbox_BookTitle��5      $
��Left=�6-   $��30��6H   $��
10,570,0,0q�6T   $
 Wrap=�6h   $	��Top=�6|   $	��209��6�   
�������6�   �� 5   
   -   5        $ textbox_NumberOfPages��5      $
��Left=�61   $��30��6L   $��223,570,0,0q�6X   $
 Wrap=�6m   $	��Top=�6�   $	��209��6�   
�������6�   �� 5   
   -   5        $ textbox_BooksAuthor��5      $
��Left=�6/   $��30��6J   $��437,570,0,0q�6V   $
 Wrap=�6k   $	��Top=�6   $	��207��6�   �� 5   
   -   5        $ textbox_BooksCategory��5      $
��Left=�61   $��30��6L   $��649,570,0,0q�6X   $
 Wrap=�6m   $	��Top=�6�   $	��208��6�   �� 5   
   -	   5        $ textbox_BooksYear��5   O   $
��Left=�6j   $��30��6�   $��862,570,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��209��6�    �� 	MaxLength$ 10��6�   ��6   �� 	 �� Path$	 DateTimeValue�
 �� StringFormat$
 
yyyy-mm-dd��6�   �� 5   
   -
   5        $ textbox_Amount��5      $
��Left=�6*   $��30��6E   $��1076,570,0,0q�6Q   $
 Wrap=�6g   $	��Top=�6{   $	��209��6�   6�   �� 5   
   $��Book's Title :��6   $
��Left=�6)   $��
10,544,0,0q�6D   $	��Top=�6X   $	��208��6p   
�������6|   �� 5   
   $��Number Of Pages :��6   $
��Left=�6,   $��224,544,0,0q�6G   $	��Top=�6\   $	��208��6t   
�������6�   �� 5   
   $��Book's Author :��6   $
��Left=�6*   $��437,544,0,0q�6E   $	��Top=�6Z   $	��208��6r   
�������6~   �� 5   
   $��Book's Category :��6   $
��Left=�6,   $��650,544,0,0q�6G   $	��Top=�6\   $	��208��6t   
�������6�   �� 5   
   $)��#Book's Publishing Year (yyyy-mm-dd)��6   $
��Left=�6>   $��863,544,0,0q�6Y   $	��Top=�6n   $	��208��6�   
�������6�   �� 5   
   $��Amount :��6   $
��Left=�6#   $��1076,544,0,0q�6>   $	��Top=�6T   $	��208��6l   
�������6x   �� 5   
   -   5        $ button_AddBook��5      $��Add Book��6)   $
��Left=�6<   $	��Top=�6W   $	��110��6o   $��
10,615,0,0q�6{   $��35��6�   
�������6�   �� 5   
   -   5        $ button_ReturnToMenu��5      $��Return to the main menu��6.   $
��Left=�6P   $��1120,730,0,0q�6k   $	��Top=�6�   $	��164��6�   $��31��6�   
�������6�   �� 5   
   -   5        $ button_DeleteBook��5      $��Delete Book��6,   $
��Left=�6B   $��1160,505,0,0q�6]   $	��Top=�6s   $	��124��6�   $��30��6�   
�������6�   ��5   
   �� 5        �� ImageSource$/ )books_shelf_graffiti_125860_1920x1080.jpg��6   5"      5#      