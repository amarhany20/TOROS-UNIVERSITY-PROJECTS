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
    /// <summary>
    /// Interaction logic for EditUsers.xaml
    /// </summary>
    public partial class EditUsers : Window
    {
        public EditUsers()
        {
            InitializeComponent();
            Filldatagrid();
        }
        //      Buttons
        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Insert_Click(object sender, RoutedEventArgs e)
        {
            AddNewUser win = new AddNewUser();
            win.Width = window_EditUsers.Width;
            win.Height = window_EditUsers.Height;
            win.Left = window_EditUsers.Left;
            win.Top = window_EditUsers.Top;
            win.WindowState = window_EditUsers.WindowState;
            win.ShowDialog();
            Filldatagrid();
        }

        private void button_Update_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)dg_Users.SelectedItem;
            if (dataRowView == null)
            {
                MessageBox.Show("Please select the user that you want to Update");
                return;
            }
            DateTime dtbirthdate;
            if (!DateTime.TryParse(textbox_Birthday.Text.Trim(),out dtbirthdate))
            {
                System.Windows.Forms.MessageBox.Show("Please write the date in the correct format (DD/MM/YYYY)");
                return;
            }
            
            
            string item_ID = dataRowView.Row[0].ToString();
            
            string query2 = "Update Users set username = @username , password = @password , gender=@gender , birthday=@birthday where ID like @ID";
            SqlConnection conn = new SqlConnection(App.connection);
            SqlCommand cmd2 = new SqlCommand(query2, conn);
            cmd2.Parameters.AddWithValue("@ID", item_ID);
            cmd2.Parameters.AddWithValue("@username", textbox_username.Text.Trim());
            cmd2.Parameters.AddWithValue("@password", textbox_password.Text);
            cmd2.Parameters.AddWithValue("@gender", combobox_Gender.SelectedValue.ToString().Trim().ToLower());
            cmd2.Parameters.AddWithValue("@birthday", dtbirthdate.Date);
            try
            {
                conn.Open();
                cmd2.ExecuteNonQuery();
                MessageBox.Show("Users Updated Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error at Updating user\n"+ex.ToString());
            }
            finally
            {
                conn.Close();
                int i = dg_Users.SelectedIndex;
                Filldatagrid();
                dg_Users.SelectedIndex = i;
            }
        }

        private void btn_Activate_Deactivate_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)dg_Users.SelectedItem;
            if (dataRowView == null)
            {
                MessageBox.Show("Please select the user that you want to activate");
                return;
            }
           
            string item_ID = dataRowView.Row[0].ToString();
            if (item_ID == MainWindow.userID.ToString())
            {
                System.Windows.Forms.MessageBox.Show("You can't Deactivate your account");
                return;
            }
            string activated = dataRowView.Row[7].ToString();
            SqlConnection conn = new SqlConnection(App.connection);
            string query;
            if (activated == "True")
            {
                query = "Update Users set Activated = '0' where ID like @id ";

            }
            else
            {
                query = "Update Users set Activated = '1' where  ID like @id";
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
                MessageBox.Show("Error at Activating user\n"+ex.ToString());
            }
            finally
            {
                conn.Close();
                Filldatagrid();
            }
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            int count;
            DataRowView dataRowView = (DataRowView)dg_Users.SelectedItem;
            if (dataRowView == null)
            {
                MessageBox.Show("Please select the user that you want to activate");
                return;
            }
            string item_ID = dataRowView.Row[0].ToString();
            if (item_ID == MainWindow.userID.ToString())
            {
                System.Windows.Forms.MessageBox.Show("You can't delete your own user!");
                return;
            }
            SqlConnection conn = new SqlConnection(App.connection);
            string query = "select Count(user_ID) from orders where user_ID like @ID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ID", item_ID);
            try
            {
                conn.Open();
                count = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error at checking if user can be deleted or not\n"+ex.ToString());
                conn.Close();
                return;
            }
            finally
            {
                conn.Close();
            }
            if (count > 0)
            {
                MessageBox.Show($"Can't Delete User because it has been already been registered on orders {count} times");
                return;
            }
            else
            {

                string query2 = "Delete From Users where ID like @ID";
                SqlCommand cmd2 = new SqlCommand(query2, conn);
                cmd2.Parameters.AddWithValue("@ID", item_ID);
                try
                {
                    conn.Open();
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("User Deleted Successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error at deleting item\n"+ex.ToString());
                }
                finally
                {
                    conn.Close();
                    Filldatagrid();
                }
            }
        }
        private void btn_SetAdmin_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)dg_Users.SelectedItem;
            if (dataRowView == null)
            {
                MessageBox.Show("Please select the user that you want to set User/Admin");
                return;
            }

            string item_ID = dataRowView.Row[0].ToString();
            if (item_ID == MainWindow.userID.ToString())
            {
                System.Windows.Forms.MessageBox.Show("You can't change your account access type");
                return;
            }
            string admin = dataRowView.Row[3].ToString();
            SqlConnection conn = new SqlConnection(App.connection);
            string query;
            if (admin == "True")
            {
                query = "Update Users set IsAdmin = '0' where ID like @id ";

            }
            else
            {
                query = "Update Users set IsAdmin = '1' where  ID like @id";
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
                MessageBox.Show("Error at Activating user\n"+ex.ToString());
            }
            finally
            {
                conn.Close();
                Filldatagrid();
            }
        }
        // Functions
        void Filldatagrid()
        {
            DateTime dt;
            bool isdt = false;
            isdt = DateTime.TryParseExact(textbox_Search.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);

            SqlConnection con = new SqlConnection(App.connection);
            string query = "Select * from Users where ((Birthday >= @date AND Birthday < @date2) or (CreationDate >= @date AND CreationDate < @date2) or ID like @search or Username like @search or Password like @search or Gender like @search)";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                if (isdt)
                {
                    cmd.Parameters.AddWithValue("@search", "");
                    cmd.Parameters.AddWithValue("@date", dt.Date);
                    cmd.Parameters.AddWithValue("@date2", dt.AddDays(1).Date);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@date", "");
                    cmd.Parameters.AddWithValue("@date2","");
                    cmd.Parameters.AddWithValue("@search","%"+ textbox_Search.Text +"%");
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dat = new DataTable();
                da.Fill(dat);
                dg_Users.ItemsSource=dat.DefaultView;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }

        private void dg_Users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView row = dg_Users.SelectedItem as DataRowView;
            if (row != null)
            {
                textbox_username.Text= row.Row[1].ToString();
                textbox_password.Text= row.Row[2].ToString();
                if (row.Row[4].ToString() == "male")
                {
                    combobox_Gender.SelectedIndex=0;
                }
                else
                {
                    combobox_Gender.SelectedIndex=1;
                }
                
                textbox_Birthday.Text= row.Row[5].ToString();
            }
        }


        private void textbox_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filldatagrid();
        }

        private void dg_Users_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                if (e.Column.Header.ToString() == "Birthday")
                {
                    (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
                }
                else
                {
                    (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy hh:MM tt";
                }
        }

       
    }
}
