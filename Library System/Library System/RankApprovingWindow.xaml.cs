using System.Windows;
using System.Windows.Controls;
namespace Library_System
{
    /// <summary>
    /// Interaction logic for RankApprovingWindow.xaml
    /// </summary>
    public partial class RankApprovingWindow : Window
    {
        //RankApprovingWindow Initialization
        public RankApprovingWindow()
        {
            try
            {
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                InitializeComponent();
                datagrid_RankApproval.ItemsSource = PublicMethods.RankRequestedShowing(textbox_Search.Text).DefaultView;
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 47 : " + x.ToString());
            }

        }
        ////RankApprovingWindow Initialization END
        //Non Static Methods
        int SelectedID;
        public bool selection()
        {
            try
            {
                object item = datagrid_RankApproval.SelectedItem;
                try
                {
                    SelectedID = int.Parse((datagrid_RankApproval.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
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

                MessageBox.Show("Error 48 : " + x.ToString());
                return false;
            }

        }
        //Non Static Methods End

        //Set Admin / Teacher and search button
        private void button_SetTeacher_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure? The use will get the ability to rent unlimited amount of books and no return time. ", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    return;
                }

                selection();
                PublicMethods.RankApproval(SelectedID, false);
                datagrid_RankApproval.ItemsSource = PublicMethods.RankRequestedShowing(textbox_Search.Text).DefaultView;
            }
            catch (System.Exception x)
            {

                MessageBox.Show("Error 50 : " + x.ToString());
            }

        }
        private void button_SetAdmin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure? The user will get access to everything in the program.", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    return;
                }
                else
                {
                    if (selection())
                    {
                        PublicMethods.RankApproval(SelectedID, true);
                        datagrid_RankApproval.ItemsSource = PublicMethods.RankRequestedShowing(textbox_Search.Text).DefaultView;
                    }
                    else
                    {
                        MessageBox.Show("Canceled");
                    }
                }
            }
            catch (System.Exception x)
            {

                MessageBox.Show("Error 51 : " + x.ToString());
            }

        }
        private void button_Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (selection())
                {
                    datagrid_RankApproval.ItemsSource = PublicMethods.RankRequestedShowing(textbox_Search.Text).DefaultView;
                }
                else
                {
                    MessageBox.Show("Canceled");
                }

            }
            catch (System.Exception x)
            {

                MessageBox.Show("Error 52 : " + x.ToString());
            }

        }
        //Set Admin / Teacher and search button end 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Leave Button Start
            try
            {

                this.Close();
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Error 77 : " + x.ToString());
            }
            //Leave Button End
        }
    }
}
