using System;
using System.Collections.Generic;
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
    /// Interaction logic for InsertWindow.xaml
    /// </summary>
    public partial class InsertWindow : Window
    {
        public InsertWindow()
        {
            InitializeComponent();
        }


        //                  Buttons


        private void btn_Insert_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_Name.Text.Trim() == "")
            {
                MessageBox.Show("Item Name Can't be Empty");
                return;
            }
            if (textbox_Category.Text == "")
            {
                MessageBox.Show("Item Category Can't be Empty");
                return;
            }
            double price = 0;
            if (!(double.TryParse(textbox_Price.Text.Trim(), out price)))
            {
                MessageBox.Show("Enter the correct price please");
                return;

            }
            int stock = 0;
            if (!(int.TryParse(textbox_Stock.Text.Trim(), out stock)))
            {
                MessageBox.Show("Please enter the correct Stock");
                return;

            }
            if (MessageBox.Show("Are you sure you want to insert this item in the database?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question)==MessageBoxResult.Yes)
            {
                SqlConnection conn = new SqlConnection(App.connection);
                string query = "Insert into Items (Name,Category,Barcode,Price,Description,Stock) values (@name,@category,@barcode,@price,@description,@stock)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", textbox_Name.Text.Trim());
                cmd.Parameters.AddWithValue("@category", textbox_Category.Text.Trim());
                cmd.Parameters.AddWithValue("@barcode", textbox_Barcode.Text.Trim());
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@stock", stock);
                cmd.Parameters.AddWithValue("@description", textbox_Description.Text.Trim());
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error in inserting to sql \n"+ex.ToString());
                    conn.Close();
                    return;
                }
                conn.Close();
                this.Close();
            }
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        //                  Error Control

        private static bool IsTextAllowed(string text)
        {
            double result;
            return double.TryParse(text, out result);
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

        private void textbox_Stock_Pasting(object sender, DataObjectPastingEventArgs e)
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

        private void textbox_Stock_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
