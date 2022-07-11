using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Cashier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int newOrderID;
        public static int userID;
        public static string username;
        public static bool signInSuccessful = false;
        public static bool isAdmin = false ;
        public MainWindow()
        {
            InitializeComponent();
            SignIn win = new SignIn();//starting with sign in
            win.ShowDialog();
            if (!signInSuccessful)
            {
                System.Windows.Application.Current.Shutdown();
            }
            if (isAdmin)//for showing the edit buttons for admin
            {
                btn_EditItems.Visibility = Visibility.Visible;
                btn_EditOrders.Visibility = Visibility.Visible;
                btn_EditUsers.Visibility = Visibility.Visible;
            }
            userInfo.Text = "ID: "+userID +"\n"+username;
            FillOrdersDataGrid();
            FillOrder_DetailsDataGrid();
            System.Windows.Forms.MessageBox.Show("Welcome");
        }
        //                                  Buttons
        //For Opening Orders window
        private void btn_NewOrder_Click(object sender, RoutedEventArgs e)
        {
            string ConString = App.connection;
            string query = "INSERT INTO Orders (IsOrderCompleted,User_ID) VALUES(@false,@userID)  SELECT SCOPE_IDENTITY()";
            SqlConnection connection = new SqlConnection(ConString);
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                command.Parameters.AddWithValue("@false", "0");
                command.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                object returnObj = command.ExecuteScalar();

                if (returnObj != null)
                {
                    int.TryParse(returnObj.ToString(), out newOrderID);
                }
                else
                {
                    MessageBox.Show("Error at getting the ID of the new order");
                    return;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error Generated. Maybe it's a connection to database error. Details: " + ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            New_Order win = new New_Order();
            win.Width = mainWindow.Width;
            win.Height = mainWindow.Height;
            win.Left = mainWindow.Left;
            win.Top = mainWindow.Top;
            win.WindowState = mainWindow.WindowState;
            win.ShowDialog();
            FillOrdersDataGrid();
            FillOrder_DetailsDataGrid();
            if (dg_Orders.Items.Count!=0)
            {
                dg_Orders.SelectedIndex=0;
            }
        }


        //For Opening Edit Items window
        private void btn_EditItems_Click(object sender, RoutedEventArgs e)
        {
            Edit_Items win = new Edit_Items();
            win.Width = mainWindow.Width;
            win.Height = mainWindow.Height;
            win.Left = mainWindow.Left;
            win.Top = mainWindow.Top;
            win.WindowState = mainWindow.WindowState;
            win.ShowDialog();
            FillOrdersDataGrid();
            FillOrder_DetailsDataGrid();
            if (dg_Orders.Items.Count!=0)
            {
                dg_Orders.SelectedIndex=0;
            }
        }
        //edit Users Window Button
        private void btn_EditUsers_Click(object sender, RoutedEventArgs e)
        {
            EditUsers win = new EditUsers();
            win.Width = mainWindow.Width;
            win.Height = mainWindow.Height;
            win.Left = mainWindow.Left;
            win.Top = mainWindow.Top;
            win.WindowState = mainWindow.WindowState;
            win.ShowDialog();
            FillOrdersDataGrid();
            FillOrder_DetailsDataGrid();
        }
        //Edit Orders Window
        private void btn_EditOrders_Click(object sender, RoutedEventArgs e)
        {
            EditOrders win = new EditOrders();
            win.Width = mainWindow.Width;
            win.Height = mainWindow.Height;
            win.Left = mainWindow.Left;
            win.Top = mainWindow.Top;
            win.WindowState = mainWindow.WindowState;
            win.ShowDialog();
            FillOrdersDataGrid();
            FillOrder_DetailsDataGrid();
            if (dg_Orders.Items.Count!=0)
            {
                dg_Orders.SelectedIndex=0;
            }
        }

        //Seach Button
        private void btn_OrdersSearch_Click(object sender, RoutedEventArgs e)
        {
            FillOrdersDataGrid();
            FillOrder_DetailsDataGrid();
        }
        private void button_OrderDetailsSearch_Click(object sender, RoutedEventArgs e)
        {
            FillOrder_DetailsDataGrid();
        }


        // Order Selection For Details
        private void dg_Orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillOrder_DetailsDataGrid();
        }
       
        //Auto Change the format of date time
        private void dg_Orders_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy hh:MM tt";
        }
       
        //Functions
        private void FillOrdersDataGrid()

        {
            string ConString = App.connection;
            string CmdString = string.Empty;
            string search = textbox_Search.Text.Trim();
            DateTime searchdate;
            //checks if the data in search is date only
            bool searchdatebl = DateTime.TryParseExact(search, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out searchdate);
            try
            {
                using (SqlConnection con = new SqlConnection(ConString))

                {

                    CmdString = "SELECT Orders.ID,Orders.Date,Orders.Total,Users.Username,Orders.Payment_Method FROM Orders INNER JOIN Users ON Users.ID = Orders.User_ID where ((Orders.Date >= @searchdate and Orders.Date < @searchdate2) or Orders.Payment_Method like @search or Orders.ID like @search or Orders.Total like @search) And Orders.IsOrderCompleted = '1'";

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
                    if (dg_Orders.Items.Count > 0)
                    {
                        dg_Orders.SelectedIndex=0;
                    }
                    else
                    {
                        dg_Orders.SelectedItem = -1;
                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Server Error\n"+e.ToString());
                return;
            }


        }
        private void FillOrder_DetailsDataGrid()

        {
            string ConString = App.connection;
            string CmdString = string.Empty;
            DataRowView dataRowView = (DataRowView)dg_Orders.SelectedItem;
            if (dataRowView == null)
            {
                DataTable dt = new DataTable("t1");
                dg_OrderDetails.ItemsSource = dt.DefaultView;
                return;
            }
            string ID = dataRowView.Row[0].ToString();
            string search = textbox_OrderDetailsSearch.Text.Trim();
            try
            {
                using (SqlConnection con = new SqlConnection(ConString))

                {

                    CmdString = "SELECT Order_Details.ID,Order_Details.Order_ID, Order_Details.Item_ID,Items.Name,Items.Category,Items.Price as \'Current Price\',Order_Details.Price,Order_Details.Quantity,Order_Details.Total_Item_Price as 'Total Price',Items.Barcode,Items.Description FROM Order_Details INNER JOIN Items ON Order_Details.Item_ID = Items.ID where Order_Details.Order_ID like @ID AND ( Order_Details.ID like @search or Items.Name like @search or Items.Category like @search or Order_Details.Price like @search or Items.Barcode like @search or Items.Description like @search or Order_Details.Total_Item_Price like @search ) ";

                    SqlCommand cmd = new SqlCommand(CmdString, con);

                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@search", "%"+ search + "%");

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable("t1");

                    sda.Fill(dt);

                    dg_OrderDetails.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Server Error\n"+e.ToString());
                return;
            }
        }
        //Auto Update Table on text changed
        private void textbox_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            FillOrdersDataGrid();
            FillOrder_DetailsDataGrid();
        }

        private void textbox_OrderDetailsSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            FillOrder_DetailsDataGrid();
        }

    }


}
