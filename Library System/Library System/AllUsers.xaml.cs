using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Library_System
{
    /// <summary>
    /// Interaction logic for AllUsers.xaml
    /// </summary>
    public partial class AllUsers : Window
    {
        //AllUsers Initialization
        public AllUsers()

        {
            try
            {
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                InitializeComponent();
                ShowingBooksAndFillingDataSet();
            }
            catch (Exception x)
            {

                MessageBox.Show("Error 57 : " + x.ToString());
            }

        }
        //AllUsers Initialization End
        //Local Methods Start
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();
        public void ShowingBooksAndFillingDataSet()
        {
            try
            {
                da = PublicMethods.SearchAllUsersAdmin(textbox_search.Text);
                ds.Tables.Clear();
                da.Fill(ds, "AllUsers");
                datagrid_AllUsers.ItemsSource = ds.Tables[0].DefaultView;
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 86 : " + x.ToString());
            }

        }
        //Local Methods End
        //Update and search buttons
        private void button_Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowingBooksAndFillingDataSet();
            }
            catch (Exception x)
            {

                MessageBox.Show("Error 59 : " + x.ToString());
            }

        }
        private void button_UpdateChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to apply changes?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    da = PublicMethods.SearchAllUsersAdmin(textbox_search.Text);
                    SqlCommandBuilder sqlBuilder = new SqlCommandBuilder(da);
                    da.Fill(ds, "AllBooks");


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
                    da.Update(ds, "AllUsers");
                    ds.AcceptChanges();
                    ShowingBooksAndFillingDataSet();

                }
                else
                {
                    ShowingBooksAndFillingDataSet();
                    MessageBox.Show("Canceled");
                }
            }
            catch (Exception x)
            {

                MessageBox.Show("Error 58 : " + x.ToString());
            }

        }
        //Update and search buttons end
        //Delete Button Start
        private void button_DeleteUser_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to delete this user?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                object item = datagrid_AllUsers.SelectedItem;
                try
                {

                    int userID = int.Parse((datagrid_AllUsers.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                    PublicMethods.DeletingUser(userID);
                    ShowingBooksAndFillingDataSet();
                    MessageBox.Show("User has been deleted successful");
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    MessageBox.Show("No user selected to be deleted. Please select a user.");
                    return;
                }
                catch (Exception x)
                {
                    MessageBox.Show("Error 87 : " + x.ToString());
                }
            }
            else
            {
                MessageBox.Show("Canceled");
                ShowingBooksAndFillingDataSet();
            }
        }
        //Delete Button End
        //Leave Button Start
        private void Button_ReturnToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 75 : " + x.ToString());
            }
        }
        
        //Leave Button End
    }
}
