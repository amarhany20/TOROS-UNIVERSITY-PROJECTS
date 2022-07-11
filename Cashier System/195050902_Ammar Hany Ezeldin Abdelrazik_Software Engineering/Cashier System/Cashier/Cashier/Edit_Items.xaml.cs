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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.Forms.MessageBox;
using TextBox = System.Windows.Controls.TextBox;

namespace Cashier
{
    /// <summary>
    /// Interaction logic for Edit_Items.xaml
    /// </summary>
    public partial class Edit_Items : Window
    {
        public Edit_Items()
        {
            InitializeComponent();
            fillDatagrid();
        }
        //-------------------------Buttons---------------------
        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
                window_Edit_Items.Close();
        }

        private void button_Search_Click(object sender, RoutedEventArgs e)
        {
            fillDatagrid();

        }
        private void btn_Hide_Item_Click(object sender, RoutedEventArgs e)// for hiding item from the store
        {
            DataRowView dataRowView = (DataRowView)dg_Items.SelectedItem;
            if (dataRowView == null)
            {
                MessageBox.Show("Please select the item you want to Show/Hide");
                return;
            }
            string item_ID = dataRowView.Row[0].ToString();
            string Show_Item = dataRowView.Row[7].ToString();
            SqlConnection conn = new SqlConnection(App.connection);
            string query;
            if (Show_Item == "True")
            {
                query = "Update Items set Show_Item = '0' where ID like @id ";

            }
            else
            {
                query = "Update Items set Show_Item = '1' where  ID like @id";
            }

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ID", item_ID);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error at Showing/Hiding item\n"+ex.ToString());
            }
            finally
            {
                conn.Close();
                fillDatagrid();
            }
        }
        private void btn_Delete_Click(object sender, RoutedEventArgs e)// for deleting item from the store
        {
           
            int count;
            DataRowView dataRowView = (DataRowView)dg_Items.SelectedItem;
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
            string query = "select Count(Item_ID) from Order_Details where Item_ID = @ID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ID", item_ID);
            try
            {
                conn.Open();
                count = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error at Checking Orderdetails database if there is an already item available\n"+ex.ToString());
                conn.Close();
                return;
            }
            finally
            {
                conn.Close();
            }
            if (count > 0)
            {
                MessageBox.Show($"Can't Delete Item because it has been already purchased {count} times");
            }
            else
            {
                string query2 = "Delete From Items where ID like @ID";
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
                    fillDatagrid();
                }
            }
        }
        private void button_Update_Click(object sender, RoutedEventArgs e)//for updating an item info
        {
            DataRowView dataRowView = (DataRowView)dg_Items.SelectedItem;
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
            int stock = 0;
            if (!(int.TryParse(textbox_Stock.Text.Trim(),out stock)))
            {
                MessageBox.Show("Please enter the correct Stock");
                return;

            }
            string item_ID = dataRowView.Row[0].ToString();
            string query2 = "Update items set Name = @name , Category = @category , Barcode=@barcode , Price=@price , Stock = @Stock , Description = @description where ID like @ID";
            SqlConnection conn = new SqlConnection(App.connection);
            SqlCommand cmd2 = new SqlCommand(query2, conn);
            cmd2.Parameters.AddWithValue("@ID", item_ID);
            cmd2.Parameters.AddWithValue("@name", textbox_Name.Text.Trim());
            cmd2.Parameters.AddWithValue("@category", textbox_Category.Text.Trim());
            cmd2.Parameters.AddWithValue("@barcode", textbox_Barcode.Text.Trim());
            cmd2.Parameters.AddWithValue("@price", price);
            cmd2.Parameters.AddWithValue("@Stock", stock);
            cmd2.Parameters.AddWithValue("@description", textbox_Description.Text.Trim());
            try
            {
                conn.Open();
                cmd2.ExecuteNonQuery();
                MessageBox.Show("Item Updated Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error at Updating item\n"+ex.ToString());
            }
            finally
            {
                conn.Close();
                int i = dg_Items.SelectedIndex;
                fillDatagrid();
                dg_Items.SelectedIndex = i;
            }
        }
        private void btn_Insert_Click(object sender, RoutedEventArgs e)//for inserting
        {
            InsertWindow win = new InsertWindow();
            win.ShowDialog();
            fillDatagrid();
        }


        //---------------Functions----------------
        private void fillDatagrid()
        {
            int row = -1;
            if (dg_Items.SelectedIndex != -1)
            {
                row = dg_Items.SelectedIndex;
            }
            SqlConnection conn = new SqlConnection(App.connection);
            string query = "Select * from Items where ID like @Search or Name like @search or Category like @search or Barcode like @search or Price like @search or Description like @search ";
            SqlCommand cmd =new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@search", "%"+ textbox_Search.Text.Trim()+"%");
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dg_Items.ItemsSource = dt.DefaultView;
            if (row != -1)
            {
                dg_Items.SelectedIndex = row;
            }
        }


        //-------------------auto update table-----------
        private void textbox_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            fillDatagrid();
        }

        private void dg_Items_SelectionChanged(object sender, SelectionChangedEventArgs e)//inserting data from datagrid to textbox
        {
            DataRowView row = dg_Items.SelectedItem as DataRowView;
            if (row != null)
            {
                textbox_Name.Text= row.Row[1].ToString();
                textbox_Category.Text= row.Row[2].ToString();
                textbox_Barcode.Text= row.Row[3].ToString();
                textbox_Price.Text= row.Row[4].ToString();
                textbox_Stock.Text = row.Row[5].ToString();
                textbox_Description.Text= row.Row[6].ToString();
            }
        }


        
        private static bool IsTextAllowed(string text)
        {
            double result;
            return double.TryParse(text,out result);
        }
        private void textbox_Price_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void textbox_Price_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
           bool approvedDecimalPoint = false;

            if (e.Text == ".")
            {
                if (!((TextBox)sender).Text.Contains("."))
                    approvedDecimalPoint = true;
            }

            if (!(char.IsDigit(e.Text, e.Text.Length - 1) || approvedDecimalPoint))
                e.Handled = true;
        }

        private void textbox_stock_Pasting(object sender, DataObjectPastingEventArgs e)
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

        private void textbox_stock_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
