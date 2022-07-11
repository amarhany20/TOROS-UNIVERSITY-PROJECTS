using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
namespace Library_System
{
    /// <summary>
    /// Interaction logic for Books_Students.xaml
    /// </summary>
    public partial class Books : Window
    {
        public Books()
        {
            try
            {
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                InitializeComponent();
                datagrid_BooksDisplay.ItemsSource = PublicMethods.SearchBooks(textbok_SearchBooks.Text, checkbox_InStockOnly.IsChecked ?? true, checkbox_SearchByFirstLetter.IsChecked ?? true).DefaultView;
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 35 : " + x.ToString());
            }
        }
        //All Buttons
        string rentedbookTitle = "";
        string rentedbookID = "";
        string rentedbookAuthor = "";
        string rentedbookCategory = "";
        private void button_TakeSelectedBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object item = datagrid_BooksDisplay.SelectedItem;
                try
                {
                    rentedbookTitle = (datagrid_BooksDisplay.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                    rentedbookID = (datagrid_BooksDisplay.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                    rentedbookAuthor = (datagrid_BooksDisplay.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text;
                    rentedbookCategory = (datagrid_BooksDisplay.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    MessageBox.Show("No book selected to be rented. Please select a book.");
                    return;
                }
                if (MessageBox.Show("Do you want to confirm Renting \" " + rentedbookTitle + " \" book. with ID number " + rentedbookID.Trim(), "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (PublicMethods.RentingProcess(PublicVariables.currentlySignedInUsername, PublicVariables.currentlySignedInID, int.Parse(rentedbookID), rentedbookTitle, rentedbookAuthor, rentedbookCategory, int.Parse(PublicMethods.GettingInfoFromSql(PublicVariables.currentlySignedInUsername)[0])) )
                    {
                        datagrid_BooksDisplay.ItemsSource = PublicMethods.SearchBooks(textbok_SearchBooks.Text, checkbox_InStockOnly.IsChecked ?? true, checkbox_SearchByFirstLetter.IsChecked ?? true).DefaultView;//This is for refresh only.
                    }
                    else
                    {
                    }
                }
                else
                {
                    MessageBox.Show("Renting Canceled");
                }
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 36 : " + x.ToString());
            }
        }
        private void button_SearchBooks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                datagrid_BooksDisplay.ItemsSource = PublicMethods.SearchBooks(textbok_SearchBooks.Text, checkbox_InStockOnly.IsChecked ?? true, checkbox_SearchByFirstLetter.IsChecked ?? true).DefaultView;
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 37 : " + x.ToString());
            }
        }
        //Search,refresh and takingBooks buttons
        //Checkbox Automaticly Refreshing
        private void checkbox_SearchByFirstLetter_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                datagrid_BooksDisplay.ItemsSource = PublicMethods.SearchBooks(textbok_SearchBooks.Text, checkbox_InStockOnly.IsChecked ?? true, checkbox_SearchByFirstLetter.IsChecked ?? true).DefaultView;
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 38 : " + x.ToString());
            }
        }
        private void checkbox_SearchByFirstLetter_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                datagrid_BooksDisplay.ItemsSource = PublicMethods.SearchBooks(textbok_SearchBooks.Text, checkbox_InStockOnly.IsChecked ?? true, checkbox_SearchByFirstLetter.IsChecked ?? true).DefaultView;
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 39 : " + x.ToString());
            }
        }
        private void checkbox_InStockOnly_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                datagrid_BooksDisplay.ItemsSource = PublicMethods.SearchBooks(textbok_SearchBooks.Text, checkbox_InStockOnly.IsChecked ?? true, checkbox_SearchByFirstLetter.IsChecked ?? true).DefaultView;
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 40 : " + x.ToString());
            }
        }
        private void checkbox_InStockOnly_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                datagrid_BooksDisplay.ItemsSource = PublicMethods.SearchBooks(textbok_SearchBooks.Text, checkbox_InStockOnly.IsChecked ?? true, checkbox_SearchByFirstLetter.IsChecked ?? true).DefaultView;
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 41 : " + x.ToString());
            }
        }
        //Checkbox Automaticly Refreshing end
        //Leave window button
        private void button_ReturnBackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 79 : "+x.ToString());//ihtiyat icin yazdim
            }
        }
        //Leave window button end
        //for later
    }
}
