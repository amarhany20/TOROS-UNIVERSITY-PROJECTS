using System.Windows;
using System.Windows.Controls;
namespace Library_System
{
    /// <summary>
    /// Interaction logic for MyBooks.xaml
    /// </summary>
    public partial class MyBooks : Window
    {
        public MyBooks()
        {
            try
            {
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                InitializeComponent();
                datagrid_MyBooksDisplay.ItemsSource = PublicMethods.SearchMyBooks(textbok_SearchMyBooks.Text, checkbox_SearchByFirstLetter.IsChecked ?? true, PublicVariables.currentlySignedInID, PublicVariables.currentlySignedInUsername).DefaultView;
                ShowingRemainingLimit();
            }
            catch (System.Exception x)
            {

                MessageBox.Show("Error 47 : " + x.ToString());
            }

        }
        //non static Methods
        public void ShowingRemainingLimit()
        {
            try
            {
                if (int.Parse(PublicMethods.GettingInfoFromSql(PublicVariables.currentlySignedInUsername)[3]) >= 0)
                {
                    label_CurrentLimit.Content = "Remaining Books Limit = " + PublicMethods.GettingInfoFromSql(PublicVariables.currentlySignedInUsername)[3];
                }
                else
                {
                    label_CurrentLimit.Content = "Remaining Books Limit = Unlimited";
                }

            }
            catch (System.Exception x)
            {

                MessageBox.Show("Error 42 : " + x.ToString());
            }

        }
        //non static Methods end
        //Search Button
        private void button_SearchMyBooks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                datagrid_MyBooksDisplay.ItemsSource = PublicMethods.SearchMyBooks(textbok_SearchMyBooks.Text, checkbox_SearchByFirstLetter.IsChecked ?? true, PublicVariables.currentlySignedInID, PublicVariables.currentlySignedInUsername).DefaultView;
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 43 : " + x.ToString());
            }

        }
        //Search Button end

        //Automatic Refresh with Chechbox checking

        private void checkbox_SearchByFirstLetter_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                datagrid_MyBooksDisplay.ItemsSource = PublicMethods.SearchMyBooks(textbok_SearchMyBooks.Text, checkbox_SearchByFirstLetter.IsChecked ?? true, PublicVariables.currentlySignedInID, PublicVariables.currentlySignedInUsername).DefaultView;
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 44 : " + x.ToString());
            }

        }
        private void checkbox_SearchByFirstLetter_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                datagrid_MyBooksDisplay.ItemsSource = PublicMethods.SearchMyBooks(textbok_SearchMyBooks.Text, checkbox_SearchByFirstLetter.IsChecked ?? true, PublicVariables.currentlySignedInID, PublicVariables.currentlySignedInUsername).DefaultView;
            }
            catch (System.Exception x)
            {

                MessageBox.Show("Error 45 : " + x.ToString());
            }

        }
        //Automatic Refresh with Chechbox checking end

        //ReturnBook Button
        private void button_ReturnSelectedBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object item = datagrid_MyBooksDisplay.SelectedItem;
                string ReturnedbookID = "";
                string ReturnededbookID = "";
                string ReturnedBookTitle = "";
                try
                {
                    ReturnedbookID = (datagrid_MyBooksDisplay.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text.Trim();
                    ReturnededbookID = (datagrid_MyBooksDisplay.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                    ReturnedBookTitle = (datagrid_MyBooksDisplay.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text; 
                    if (MessageBox.Show("Do you want to confirm Returning \" " + ReturnedBookTitle.Trim() + " \" book. with ID number " + ReturnededbookID.Trim(), "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        PublicMethods.ReturnBooksProcess(int.Parse(ReturnedbookID), int.Parse(ReturnededbookID));
                        datagrid_MyBooksDisplay.ItemsSource = PublicMethods.SearchMyBooks(textbok_SearchMyBooks.Text, checkbox_SearchByFirstLetter.IsChecked ?? true, PublicVariables.currentlySignedInID, PublicVariables.currentlySignedInUsername).DefaultView;
                    }
                    else
                    {
                        MessageBox.Show("Returning Canceled");
                    }
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    MessageBox.Show("No book selected to be returned. Please select a book.");
                    return;
                }
               
            }
            catch (System.Exception x)
            {

                MessageBox.Show("Error 46 : " + x.ToString());
            }

        }
        //ReturnBook Button end

        //Leave Button Start
        private void button_ReturnBackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 78 : " + x.ToString());
            }
           
        }
        //Leave Button End 
    }
}
