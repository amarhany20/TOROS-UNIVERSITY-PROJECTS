   M S B A M L   `   `   `  ����I  ELibrary System, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Library_SystemLibrary_System  5   	       Library_System.MyBooks   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:Library_System     5   	     � 
ResizeMode$  NoResize=�5   	    � Title$ MyBooks��5	   	   $	��800��6   $
��1300��6&   .��5
      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ datagrid_MyBooksDisplay��5      $
��Left=�64   $	��424��6O   $��	10,51,0,0q�6\   $	��Top=�6o   $
��1274��6�      
IsReadOnly .    SelectionMode$ Single=�6�   
�������6�   �� 5   
   -   5         ��Name$ button_ReturnSelectedBook��5      $��Return Selected Book��64   $
��Left=�6S   $��
10,480,0,0q�6n   $	��Top=�6�   $	��131��6�   $��37��6�   
�������6�   �� 5   
   -   5        $! button_ReturnBackToMainMenu��5      $��Return Back To Main Menu��66   $
��Left=�6Y   $��1125,724,0,0q�6t   $	��Top=�6�   $	��159��6�   $��37��6�   
�������6�   �� 5   
   -   5         ��Name$ textbok_SearchMyBooks��5      $
��Left=�61   $��34��6L   $��	10,10,0,0q�6X    �� TextWrapping$
 Wrap=�6k   $	��Top=�6   $	��210��6�   �� 5   
   -   5        $ button_SearchMyBooks��5      $��Search��6/   $
��Left=�6@   $��
225,10,0,0q�6[   $	��Top=�6o   $	��141��6�   $��36��6�    �� 	IsDefault . 
�������6�   �� 5   
   -   5        	 ��Name$"	 checkbox_SearchByFirstLetter��5      $"��Search title By First Letter��69   $
��Left=�6`   $��
371,21,0,0q�6{   $	��Top=�6�   $	��186��6�   
�������6  �� 5   
   -   5        
 ��Name$
 label_CurrentLimit��5      $
��Left=�6-   $��
562,10,0,0q�6H   $	��Top=�6\   $	��213��6t   $��34��6�   $�� ��6�   
�������6�   ��5   
   �� 5       �� ImageSource$/ )books_shelf_graffiti_125860_1920x1080.jpg��6   5      5      