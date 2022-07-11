using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Cashier
{
    /// <summary>
    /// Interaction logic for Order_Details_Window.xaml
    /// </summary>
    public partial class Order_Details_Window : Window
    {
        public Order_Details_Window()
        {
            InitializeComponent();
            textblock_OrderID.Text = "ID: "+EditOrders.getOrderDetailsID;
            FillDataGrid();
        }


        //          Buttons
        private void button_Update_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)dg_OrderDetails.SelectedItem;
            if (dataRowView == null)
            {
                MessageBox.Show("Please select the item you want to Update");
                return;
            }
            double price = 0;
            if (!(double.TryParse(textbox_Price.Text.Trim(), out price)))
            {
                MessageBox.Show("Please enter the correct price");
                return;
            }
            int quantity = 0;
            if (!(int.TryParse(textbox_Quantity.Text.Trim(), out quantity)))
            {
                MessageBox.Show("Please enter the correct Quantity");
                return;

            }
            string item_ID = dataRowView.Row[0].ToString();
            string query2 = "update Order_Details set Price = @price , Quantity = @quantity, Total_Item_Price = @total where ID like @ID";
            SqlConnection conn = new SqlConnection(App.connection);
            SqlCommand cmd2 = new SqlCommand(query2, conn);
            cmd2.Parameters.AddWithValue("@ID", item_ID);
            cmd2.Parameters.AddWithValue("@price", price);
            cmd2.Parameters.AddWithValue("@quantity", quantity);
            cmd2.Parameters.AddWithValue("@total", (price * quantity));
            try
            {
                conn.Open();
                cmd2.ExecuteNonQuery();
                MessageBox.Show("Item Updated Successfully in this order");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error at Updating item\n"+ex.ToString());
            }
            finally
            {
                conn.Close();
                int i = dg_OrderDetails.SelectedIndex;
                FillDataGrid();
                dg_OrderDetails.SelectedIndex = i;
            }
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)dg_OrderDetails.SelectedItem;
            if (dataRowView == null)
            {
                MessageBox.Show("Please select the item you want to Delete");
                return;
            }
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you want to delete that item", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No)
            {
                return;
            }
            string item_ID = dataRowView.Row[0].ToString();
            SqlConnection conn = new SqlConnection(App.connection);
            string query2 = "Delete From Order_Details where ID like @ID";
            SqlCommand cmd2 = new SqlCommand(query2, conn);
            cmd2.Parameters.AddWithValue("@ID", item_ID);
            try
            {
                conn.Open();
                cmd2.ExecuteNonQuery();
                MessageBox.Show("Item Deleted Successfully");
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

        private void btn_AddItem_Click(object sender, RoutedEventArgs e)
        {
            AddItemToOrderDetails win = new AddItemToOrderDetails();
            win.Width = editOrderDetailsWindow.Width;
            win.Height = editOrderDetailsWindow.Height;
            win.Left = editOrderDetailsWindow.Left;
            win.Top = editOrderDetailsWindow.Top;
            win.WindowState = editOrderDetailsWindow.WindowState;
            win.ShowDialog();
            FillDataGrid();
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //              Events
        private void dg_OrderDetails_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }

        private void textbox_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            FillDataGrid();
        }

        private void dg_OrderDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView roww = dg_OrderDetails.SelectedItem as DataRowView;
            if (roww != null)
            {
                textbox_Price.Text= roww.Row[7].ToString();
                textbox_Quantity.Text= roww.Row[8].ToString();
            }
        }
        //               Functions
        void FillDataGrid()
        {

            int row = -1;
            if (dg_OrderDetails.SelectedIndex != -1)
            {
                row = dg_OrderDetails.SelectedIndex;
            }
            SqlConnection conn = new SqlConnection(App.connection);
            string query = "Select Order_Details.ID, Order_Details.Order_ID,Order_Details.Item_ID,items.Name,Items.Category,Items.Barcode,Items.Price as 'Current Price',Order_Details.Price,Order_Details.Quantity,Order_Details.Total_Item_Price from Order_Details inner join Items on items.ID = Order_Details.Item_ID where (Order_Details.Order_ID like @Order_ID ) AND ( Order_Details.ID like @Search or Items.Name like @search or Items.Category like @search or Items.Barcode like @search or Items.Price like @search or Order_Details.Price like @search or Order_Details.Quantity like @Search or Order_Details.Total_Item_Price like @search )";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@search", "%"+ textbox_Search.Text.Trim()+"%");
            cmd.Parameters.AddWithValue("@Order_ID", EditOrders.getOrderDetailsID);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }

            dg_OrderDetails.ItemsSource = dt.DefaultView;
            if (row != -1)
            {
                dg_OrderDetails.SelectedIndex = row;
            }
            string query2 = "select SUM(Total_Item_Price) from Order_Details where Order_ID like @Order_ID";
            SqlCommand cmd2 = new SqlCommand(@query2, conn);
            cmd2.Parameters.AddWithValue("@Order_ID", EditOrders.getOrderDetailsID);
            double totalPrice;
            try
            {
                conn.Open();
                totalPrice = double.Parse(cmd2.ExecuteScalar().ToString());

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                return;
            }
            finally
            {

                conn.Close();
            }
            textblock_TotalPrice.Text="Total Price: " + totalPrice.ToString();
            string query3 = "Update Orders Set Total = @totalPrice where ID like @Order_ID";
            SqlCommand cmd3 = new SqlCommand(@query3, conn);
            cmd3.Parameters.AddWithValue("@totalPrice", totalPrice);
            cmd3.Parameters.AddWithValue("@Order_ID", EditOrders.getOrderDetailsID);
            try
            {
                conn.Open();
                cmd3.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {

                conn.Close();
            }

        }

    }
}

