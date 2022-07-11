using System.Windows;
using System.Windows.Controls;
namespace Library_System
{
    /// <summary>
    /// Interaction logic for ReturnedBooksApproval.xaml
    /// </summary>
    public partial class ReturnedBooksApproval : Window
    {
        //ReturnedBooksApproval Initialization
        public ReturnedBooksApproval()
        {
            try
            {
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                InitializeComponent();
                datagrid_ReturnedBooksApproval.ItemsSource = PublicMethods.ReturnedBookApprovalSearchAndRefresh(textbox_Search.Text).DefaultView;
            }
            catch (System.Exception x)
            {

                MessageBox.Show("Error 53 : " + x.ToString());
            }

        }
        int SelectedID, TakenBookID, TakenByID;
        //ReturnedBooksApproval Initialization end
        //non static methods
        public bool selection()
        {
            try
            {
                object item = datagrid_ReturnedBooksApproval.SelectedItem;
                try
                {
                    SelectedID = int.Parse((datagrid_ReturnedBooksApproval.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                    TakenBookID = int.Parse((datagrid_ReturnedBooksApproval.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text);
                    TakenByID = int.Parse((datagrid_ReturnedBooksApproval.SelectedCells[5].Column.GetCellContent(item) as TextBlock).Text);
                    return true;

                }
                catch (System.ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Please select user");
                    return false;
                }
            }
            catch (System.Exception x)
            {

                MessageBox.Show("Error 54 : " + x.ToString());
                return false;
            }

        }
        //non static methods end
        //Search and Return Approving Buttons.

        private void button_Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                datagrid_ReturnedBooksApproval.ItemsSource = PublicMethods.ReturnedBookApprovalSearchAndRefresh(textbox_Search.Text).DefaultView;
            }
            catch (System.Exception x)
            {

                MessageBox.Show("Error 55 : " + x.ToString());
            }

        }
        private void button_BookReturnApproval_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selection())
                {
                    if (MessageBox.Show("Are you sure this book is returned?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    {
                        return;
                    }
                    else
                    {
                        PublicMethods.ApprovingReturnedBook(SelectedID, TakenBookID, TakenByID);
                        datagrid_ReturnedBooksApproval.ItemsSource = PublicMethods.ReturnedBookApprovalSearchAndRefresh(textbox_Search.Text).DefaultView;
                    }
                }
                else
                {
                    MessageBox.Show("Canceled");
                }
            }
            catch (System.Exception x)
            {

                MessageBox.Show("Error 56 : " + x.ToString());
            }

        }
        //Search and Return Approving Buttons End.
        //Leave Button Start .
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 76 : " + x.ToString());
            }
            
        }
        //Leave Button End .
    }
}
