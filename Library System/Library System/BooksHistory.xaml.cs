using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Library_System
{
    /// <summary>
    /// Interaction logic for BooksHistory.xaml
    /// </summary>
    public partial class BooksHistory : Window
    {
        //BooksHistory Initilaization Start
        public BooksHistory()
        {
            try
            {
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                InitializeComponent();
                datagrid_BooksHistory.ItemsSource = PublicMethods.SearchBooksHistory(textbox_Search.Text).DefaultView;
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 71 : " + x.ToString());
            }
            
        }
        //BooksHistory Initilaization End

        private void button_ReturnToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 72 : " + x.ToString());
            }
            
        }

        private void button_Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                datagrid_BooksHistory.ItemsSource = PublicMethods.SearchBooksHistory(textbox_Search.Text).DefaultView;
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 73 : " + x.ToString());
            }
           
        }

        
    }
}
