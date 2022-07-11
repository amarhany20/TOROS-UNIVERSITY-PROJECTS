using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// <summary>
    /// Interaction logic for AddItemToOrderDetails.xaml
    /// </summary>
    public partial class AddItemToOrderDetails : Window
    {
        private static double orderTotalPrice = 0;
        public AddItemToOrderDetails()
        {
            InitializeComponent();
            FillItemsDatagrid();
            FillOrder_DetailsDataGrid();
        }
        //Add button from Items DG to Order_Details DG
        private void button_Add_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)dg_ItemsDatagrid.SelectedItem;
            if (dataRowView == null)
            {
                MessageBox.Show("Please select the item you want from the table to add the order and enter the quantity of that item");
                return;
            }
            string item_ID = dataRowView.Row[0].ToString();
            double price = double.Parse(dataRowView.Row[3].ToString());
            int stock = int.Parse(dataRowView.Row[4].ToString());


            int quantity;


            if (int.TryParse(textbox_Quantity.Text.Trim(), out quantity))
            {
                if (quantity == 0)
                {
                    MessageBox.Show("Enter a correct quantity (0 can't be an amount in the order)");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Enter a correct quantity");
                return;
            }
            
            double totalPrice = (double)quantity * price;
            SqlConnection conn = new SqlConnection(App.connection);
            string query = "Insert Into Order_Details (Item_ID,Order_ID,Price,Quantity,Total_Item_Price) Values (@item_ID,@order_ID,@price,@quantity,@totalPrice)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@item_ID", item_ID);
            cmd.Parameters.AddWithValue("@order_ID", EditOrders.getOrderDetailsID);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@totalPrice", totalPrice);
            cmd.Parameters.AddWithValue("@price", price);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in inserting data to order. Maybe sql connection error \n"+ ex.ToString());
            }
            finally
            {
                conn.Close();
                FillOrder_DetailsDataGrid();
                FillItemsDatagrid();
            }

        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        //----------------------------Functions----------------------
        //Showing details upon adding and starting for calculating the reciept
        private void FillOrder_DetailsDataGrid()

        {
            string ConString = App.connection;
            string CmdString = string.Empty;
            string search = textbox_OrderDetailsSearch.Text.Trim();
            using (SqlConnection con = new SqlConnection(ConString))

            {
                CmdString = "SELECT Order_Details.ID,Order_Details.Order_ID, Order_Details.Item_ID,Items.Name,Items.Category,Order_Details.Price,Order_Details.Quantity,Order_Details.Total_Item_Price as 'Total Price',Items.Barcode,Items.Description FROM Order_Details INNER JOIN Items ON Order_Details.Item_ID = Items.ID where Order_Details.Order_ID like @ID AND ( Order_Details.ID like @search or Items.Name like @search or Items.Category like @search or Order_Details.Price like @search or Items.Barcode like @search or Items.Description like @search or Order_Details.Total_Item_Price like @search ) ";

                SqlCommand cmd = new SqlCommand(CmdString, con);
                cmd.Parameters.AddWithValue("@ID", EditOrders.getOrderDetailsID);
                cmd.Parameters.AddWithValue("@search", "%"+ search + "%");
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("t1");
                sda.Fill(dt);
                dg_OrderDetailsDatagrid.ItemsSource = dt.DefaultView;
                orderTotalPrice = 0;
                foreach (DataRow row in dt.Rows)
                {
                    orderTotalPrice += double.Parse(row["Total Price"].ToString());
                }
                textblock_TotalPrice.Text= ("Total Price: "+ orderTotalPrice);
                textbox_Quantity.Text="1";
            }

        }
        //Showing Items in datagrid
        private void FillItemsDatagrid()
        {
            SqlConnection con = new SqlConnection(App.connection);
            string query = "Select [ID],[Name],[Category],[Price],Stock,[Barcode],[Description] from Items where (ID like @search or Name like @search or Category like @search or Price like @search or Barcode like @search) And Show_Item like '1'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@search", "%"+textbox_Items.Text.Trim()+"%");
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dg_ItemsDatagrid.ItemsSource = dt.DefaultView;
        }
        //---------------------------------Error Control----------------------
        //Controling Numbers only in quantity
        private void textbox_Quantity_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void textbox_Quantity_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String input = (String)e.DataObject.GetData(typeof(String));
                if (new Regex("[^0-9]+").IsMatch(input))
                {
                    e.CancelCommand();
                }
            }
            else e.CancelCommand();
        }



        //Auto Update Table on text changed
        private void textbox_Items_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            FillItemsDatagrid();
        }

        private void textbox_OrderDetailsSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            FillOrder_DetailsDataGrid();
        }

        private void dg_ItemsDatagrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void dg_ItemsDatagrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            button_Add_Click(sender, e);
        }

       
    }
}
