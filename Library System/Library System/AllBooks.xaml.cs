using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Library_System
{
    /// <summary>
    /// Interaction logic for AllBooks.xaml
    /// </summary>
    public partial class AllBooks : Window
    {
        //AllBooks Initialazation Start

        public AllBooks()
        {
            try
            {
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                InitializeComponent();
                ShowingBooksAndFillingDataSet();



            }
            catch (Exception x)
            {
                MessageBox.Show("Error 63 : " + x.ToString());
            }

        }

        //AllBooks Initialazation End
        //Local Methods Start
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();
        public void ShowingBooksAndFillingDataSet()
        {
            da = PublicMethods.SearchAllBooksAdmin(textbox_Search.Text);
            ds.Tables.Clear();
            da.Fill(ds, "AllBooks");
            datagrid_AllBooks.ItemsSource = ds.Tables[0].DefaultView;
        }
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            try
            {
                return !_regex.IsMatch(text);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 64 : " + x.ToString());
                return false;
            }

        }
        private void textbox_NumberOfPages_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            try
            {
                e.Handled = !IsTextAllowed(e.Text);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 67 : " + x.ToString());
            }

        }

        private void textbox_Amount_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            try
            {
                e.Handled = !IsTextAllowed(e.Text);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 66 : " + x.ToString());
            }

        }



        //Local Methods End

        //AllBooks Buttons Start
        private void button_Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowingBooksAndFillingDataSet();
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 65 : " + x.ToString());
            }

        }
        private void button_Update_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to apply changes?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                da = PublicMethods.SearchAllBooksAdmin(textbox_Search.Text);
                SqlCommandBuilder sqlBuilder = new SqlCommandBuilder(da);
                da.Fill(ds, "AllBooks");

                try
                {
                    foreach (DataTable dt1 in ds.Tables)
                    {
                        foreach (DataRow row in dt1.Rows)
                        {
                            for (int column = 0; column < dt1.Columns.Count; column++)
                            {
                                var vrCurrent = row[column, DataRowVersion.Current];
                                var vrOrg = row[column, DataRowVersion.Original];
                            }
                        }
                    }

                    da.UpdateCommand = sqlBuilder.GetUpdateCommand();
                    da.Update(ds, "AllBooks");
                    ds.AcceptChanges();
                    ShowingBooksAndFillingDataSet();
                    MessageBox.Show("Changes has been saved.");
                }
                catch (Exception x)
                {
                    MessageBox.Show("Error 70 : " + x.ToString());
                }

            }
            else
            {
                ShowingBooksAndFillingDataSet();
                MessageBox.Show("Canceled");
            }

        }
        private void button_AddBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string title = textbox_BookTitle.Text;
                int numberOfPages, amount;
                if (string.IsNullOrEmpty(title))
                {
                    MessageBox.Show("Title is empty.");
                    return;
                }
                if (string.IsNullOrEmpty(textbox_NumberOfPages.Text))
                {
                    MessageBox.Show("Number Of Pages is empty.");
                    return;
                }
                if (string.IsNullOrEmpty(textbox_Amount.Text))
                {
                    MessageBox.Show("Amount is empty.");
                    return;
                }
                if (string.IsNullOrEmpty(textbox_BooksAuthor.Text))
                {
                    MessageBox.Show("Author is empty.");
                    return;
                }
                if (string.IsNullOrEmpty(textbox_BooksCategory.Text))
                {
                    MessageBox.Show("Category is empty.");
                    return;
                }
                if (string.IsNullOrEmpty(textbox_BooksYear.Text))
                {
                    MessageBox.Show("Publishing Year is empty.");
                }
                try
                {
                    numberOfPages = int.Parse(textbox_NumberOfPages.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Make sure you wrote a number in (Number Of Pages)");
                    return;
                }
                try
                {
                    amount = int.Parse(textbox_Amount.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Make sure you wrote a number in (Amount)");
                    return;
                }
                string author = textbox_BooksAuthor.Text;
                string category = textbox_BooksCategory.Text;
                DateTime year;
                try
                {
                    year = DateTime.Parse(textbox_BooksYear.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Make sure you wrote the date correctly (Year-Month-Day)");
                    return;
                }
                if ((MessageBox.Show($"Are you sure You want to add a new book with these information \n Title : {title} \n Number Of Pages : {numberOfPages} \n Author : {author} \n Category : {category} \n Publishing Year : {year} \n Amount : {amount} ", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes))
                {
                    if (PublicMethods.AddingNewBook(title, numberOfPages, author, category, year, amount))
                    {
                        MessageBox.Show("Books has been added to the database succesfully");
                        ShowingBooksAndFillingDataSet();
                    }
                    else
                    {
                        MessageBox.Show("Please make sure you entered all the data correctly ");
                    }
                }
                else
                {
                    MessageBox.Show("Canceled");
                }
            }
            catch (Exception x)
            {
                MessageBox.Show("Erro 68 : " + x.ToString());
            }

        }

        private void button_ReturnToMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 69 : " + x.ToString());
            }

        }

        private void button_DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            
            if (MessageBox.Show("Are you sure you want to delete this book?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    object item = datagrid_AllBooks.SelectedItem;

                    int userID = int.Parse((datagrid_AllBooks.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                    PublicMethods.DeletingBook(userID);
                    ShowingBooksAndFillingDataSet();
                    MessageBox.Show("Book deleted successful");
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    MessageBox.Show("No book selected to be delete. Please select a book.");
                    return;
                }
                catch (Exception x)
                {
                    MessageBox.Show("Error ** : " + x.ToString());
                }
            }
            else
            {
                MessageBox.Show("Canceled.");
            }
        }
        //AllBooks Buttons End


    }
}
