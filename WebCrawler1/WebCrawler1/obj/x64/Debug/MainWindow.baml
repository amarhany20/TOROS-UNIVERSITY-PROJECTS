   M S B A M L   `   `   `  ����F  BWebCrawler1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WebCrawler1WebCrawler1  5   	       WebCrawler1.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:WebCrawler1     5   	   -   5          � Title$  Web Crawler��5   	   $	��700��6   $
��1400��6*   .��5	      � .+�5
   
   �� -   5         ��Name$ 	label_Url��5
      $��URL :��6#   $
��Left=�63   $��	10,10,0,0q�6N   $	��Top=�6a   �� 5   
   -   5         ��Name$ textbox_Url��5      $
��Left=�6'   $��23��6B   $��	60,14,0,0q�6N    �� TextWrapping$
 Wrap=�6a   $	��Top=�6u   $	��645��6�   �� 5   
   -   5         ��Name$ 	btn_Crawl��5      $��Start New Crawling��6$   $
��Left=�6A   $��
710,14,0,0q�6\   $	��Top=�6p   $	��117��6�   $��23��6�   6�   �� 5   
   -   5        $ btn_ContinueCrawl��5      $��Continue Last Crawling��6,   $
��Left=�6M   $��
832,14,0,0q�6h   $	��Top=�6|   $	��128��6�   $��23��6�   6�   �� 5   
   -   5        $ btn_PauseCrawl��5      $��Pause Crawling��6)   $
��Left=�6B   $��
965,14,0,0q�6]   $	��Top=�6q   $	��128��6�   $��23��6�   6�   �� 5   
   -   5        $ btn_StopCrawl��5      $��Stop Crawling��6(   $
��Left=�6@   $��1098,14,0,0q�6[   $	��Top=�6p   $	��128��6�   $��23��6�   6�   �� 5   
   -   5        $ button_DeleteDB��5      $��Delete Database��6*   $
��Left=�6D   $��1231,14,0,0q�6_   $	��Top=�6t   $	��133��6�   $��23��6�   6�   �� 5   
   -	   5         ��Name$  checkbox_CrawlExternalUrls��5      $��Crawl External Urls��67   $
��Left=�6U   $��	60,42,0,0q�6p   $	��Top=�6�   $	��120��6�    d� 	IsChecked$
 TrueY�6�   �� 5   
   -
   5        $ label_RunTime��5      $��	Run Time:��6'   $
��Left=�6;   $��	10,65,0,0q�6V   $	��Top=�6i   $	��190��6�   �� 5   
   -   5        $ label_NumbersOfUrlFound��5      $��Number of Urls Found:��61   $
��Left=�6Q   $��	10,91,0,0q�6l   $	��Top=�6   $	��190��6�   �� 5   
   -   5        $  label_NumbersOfUrlsCrawled��5      $��Number of Urls Crawled:��64   $
��Left=�6V   $��
10,122,0,0q�6q   $	��Top=�6�   $	��190��6�   �� 5   
   -   5        $ label_UrlsFoundPerMinute��5      $��Urls Found Per Minute:��62   $
��Left=�6S   $��
10,153,0,0q�6n   $	��Top=�6�   $	��190��6�   �� 5   
   -   5        $  label_UrlsCrawledPerMinute��5      $��Urls Crawled Per Minute:��64   $
��Left=�6W   $��
10,184,0,0q�6r   $	��Top=�6�   $	��190��6�   �� 5   
   -   5        $ label_FailedUrlsCount��5      $!��Failed Crawling Urls Count:��6/   $
��Left=�6U   $��
205,96,0,0q�6p   $	��Top=�6�   $	��190��6�   �� 5   
   -   5         ��Name$ listbox_Results��5      $
��Left=�6+   $	��388��6F   $��
10,246,0,0q�6S   $	��Top=�6g   $	��695��6   &   System.Windows.Controls.DataGrid  5   
   $
��Left=�6   $	��585��6.   $��
710,60,0,0q�6;   $	��Top=�6O   $	��672��6g   �� 5   
   -   5        $ label_UrlCrawlBlockCount��5      $��Url Crawling Block Count:��62   $
��Left=�6V   $��
205,65,0,0q�6q   $	��Top=�6�   $	��190��6�   �� 5   
   -   5        $ label_Status��5      $��Current Status: ��6&   $
��Left=�6A   $��
400,65,0,0q�6\   $	��Top=�6p   $	��305��6�   �� 5   
   -   5        $ btn_ContinueOld��5      $4��.Continue Crawling the last site before closing��6*   $
��Left=�6c   $��
10,639,0,0q�6~   $	��Top=�6�   $	��290��6�   6�   5       5!      