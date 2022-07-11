using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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

namespace Cashier
{
   public class ComboboxValue // for binding the combobox and id from the users
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public ComboboxValue(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }


    /// <summary>
    /// Interaction logic for EditOrders.xaml
    /// </summary>
    public partial class EditOrders : Window
    {
        public static int getOrderDetailsID = 0;
        public EditOrders()
        {
            InitializeComponent();
            FillComboBox();
            FillDataGrid();
        }

        private void textbox_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            FillDataGrid();
        }

        //Functions

        //Filling the datagrid

        void FillDataGrid()
        {
            string search = textbox_Search.Text.Trim();
            DateTime searchdate;
            //checks if the data in search is date only
            bool searchdatebl = DateTime.TryParseExact(search, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out searchdate);
            SqlConnection con = new SqlConnection(App.connection);
            string CmdString = @"Select Orders.ID,Users.Username,Orders.User_ID,Orders.Date,Orders.Total,Orders.Payment_Method,Orders.IsOrderCompleted from Orders Inner Join Users on Users.ID = Orders.User_ID where ((Orders.Date >= @searchdate and Orders.Date < @searchdate2) or Orders.ID like @search or Orders.Total like @search or Orders.Payment_Method like @search or Orders.User_ID like @search or Users.Username like @search) ";
            SqlCommand cmd = new SqlCommand(CmdString, con);

            //checks if the data in search is date only
            if (searchdatebl)
            {
                cmd.Parameters.AddWithValue("@search", "");
                cmd.Parameters.AddWithValue("@searchdate", searchdate.Date);
                cmd.Parameters.AddWithValue("@searchdate2", searchdate.AddDays(1).Date);

            }
            else
            {
                cmd.Parameters.AddWithValue("@search", "%"+ search +"%");
                cmd.Parameters.AddWithValue("@searchdate", "");
                cmd.Parameters.AddWithValue("@searchdate2", "");
            }

            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable("t1");

            sda.Fill(dt);

            dg_Orders.ItemsSource = dt.DefaultView;
        }

        void FillComboBox()
        {
            using (SqlConnection sqlConnection = new SqlConnection(App.connection))
            {
                SqlCommand sqlCmd = new SqlCommand("SELECT * FROM Users", sqlConnection);
                try
                {
                    sqlConnection.Open();
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        //combobox_user.Items.Add(sqlReader["Username"].ToString());
                        combobox_user.Items.Add(new ComboboxValue(int.Parse(sqlReader["ID"].ToString()), sqlReader["Username"].ToString()));
                    }

                    sqlReader.Close();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString());
                }
            }
        }
        //          Buttons

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)dg_Orders.SelectedItem;
            if (dataRowView == null)
            {
                MessageBox.Show("Please select the item you want to Delete");
                return;
            }
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you want to delete that order and it's order details", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No)
            {
                return;
            }
            string order_ID = dataRowView.Row[0].ToString();
            SqlConnection conn = new SqlConnection(App.connection);
            
                string query2 = "Delete from Order_Details where Order_ID like @ID Delete From orders where ID like @ID";
                SqlCommand cmd2 = new SqlCommand(query2, conn);
                cmd2.Parameters.AddWithValue("@ID", order_ID);
                try
                {
                    conn.Open();
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Order Deleted Successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error at deleting item\n"+ex.ToString());
                }
                finally
                {
                    conn.Close();
                    FillDataGrid();
                }
        }

        private void btn_OrderDetails_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dr = (DataRowView)dg_Orders.SelectedItem;
            if (dr == null)
            {
                System.Windows.Forms.MessageBox.Show("Please select the order that you want to check it\'s details");
                return;
            }
            
            string orderID = dr.Row[0].ToString();
            getOrderDetailsID = int.Parse(orderID);
            Order_Details_Window win = new Order_Details_Window();
            win.Width = EditOrdersWindow.Width;
            win.Height = EditOrdersWindow.Height;
            win.Left = EditOrdersWindow.Left;
            win.Top = EditOrdersWindow.Top;
            win.WindowState = EditOrdersWindow.WindowState;
            win.ShowDialog();
            FillDataGrid();
        }

        private void btn_PaymentMethod_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)dg_Orders.SelectedItem;
            if (dataRowView == null)
            {
                MessageBox.Show("Please select the item you want to change it's payment method");
                return;
            }
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you?", "Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No)
            {
                return;
            }
            string item_ID = dataRowView.Row[0].ToString();
            SqlConnection conn = new SqlConnection(App.connection);
            string query;
            
                query = "Update Orders set [Payment_Method] = @payment_Method where  ID like @id";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ID", item_ID);
            cmd.Parameters.AddWithValue("@payment_Method", combobox_PaymentMethod.SelectedValue.ToString());
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n"+ex.ToString());
            }
            finally
            {
                conn.Close();
                FillDataGrid();
            }
        }
        

        private void btn_user_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)dg_Orders.SelectedItem;
            if (dataRowView == null)
            {
                MessageBox.Show("Please select the order you want to change it's username");
                return;
            }
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you?", "Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No)
            {
                return;
            }
            string item_ID = dataRowView.Row[0].ToString();
            SqlConnection conn = new SqlConnection(App.connection);
            string query;
            ComboboxValue tmpComboboxValue = (ComboboxValue)combobox_user.SelectedItem;
            query = "Update Orders set [User_ID] = @user where  ID like @id";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ID", item_ID);
            cmd.Parameters.AddWithValue("@user", tmpComboboxValue.Id);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n"+ex.ToString());
            }
            finally
            {
                conn.Close();
                FillDataGrid();
            }
        }
        // error controls 
        private void dg_Orders_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)//better date view
        {
            if (e.PropertyType == typeof(System.DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy hh:MM tt";
        }
    }
}
