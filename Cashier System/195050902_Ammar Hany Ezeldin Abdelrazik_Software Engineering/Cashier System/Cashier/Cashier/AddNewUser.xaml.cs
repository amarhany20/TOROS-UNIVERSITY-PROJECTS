using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cashier
{
    /// <summary>
    /// Interaction logic for AddNewUser.xaml
    /// </summary>
    public partial class AddNewUser : Window
    {
        public AddNewUser()
        {
            InitializeComponent();
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void btn_Register_Click(object sender, RoutedEventArgs e)
        {
            string username = textbox_Username.Text.Trim();
            string password = password_Password.Password;
            string birthdate = textbox_Birthday.Text.Trim();
            bool isAdmin = false;
            DateTime dtbirthdate;
            if (username == "")
            {
                System.Windows.Forms.MessageBox.Show("Username can't be empty");
                return;
            }
            if (password.Trim() == "")
            {
                System.Windows.Forms.MessageBox.Show("Password can't be empty");
                return;
            }
            if (!DateTime.TryParse(birthdate, out dtbirthdate))
            {
                System.Windows.Forms.MessageBox.Show("Please write the date in a correct format");
                return;
            }
            if (combobox_AccessType.SelectedValue.ToString() == "Admin")
            {
                isAdmin=true;
            }
            SqlConnection con = new SqlConnection(App.connection);
            SqlCommand cmd = new SqlCommand("Select count(*) from Users where Username like @Username", con);
            cmd.Parameters.AddWithValue("@Username", username);
            try
            {

                con.Open();
                var result = cmd.ExecuteScalar();
                if (result.ToString() != "0")
                {
                    MessageBox.Show(string.Format("Username {0} already exist", username)); return;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            string query = "Insert Into Users (Username,Password,IsAdmin,Gender,Birthday) values (@username,@password,@isadmin,@gender,@birthday)";
            SqlCommand cmd2 = new SqlCommand(query, con);
            cmd2.Parameters.AddWithValue("@username", username);
            cmd2.Parameters.AddWithValue("@password", password);
            cmd2.Parameters.AddWithValue("@isadmin", isAdmin);
            cmd2.Parameters.AddWithValue("@gender", combobox_Gender.SelectedValue.ToString().ToLower());
            cmd2.Parameters.AddWithValue("@birthday", dtbirthdate.Date);
            try
            {
                con.Open();
                cmd2.ExecuteNonQuery();
                System.Windows.Forms.MessageBox.Show("User inserted succesfully, Please activate it from the edit users wiondow\'s grid");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                return;
            }
            finally 
            {
                con.Close();
            }
            this.Close();
        }

        //          Error control
        // detecting if birthday is in a correct format or not
        private void textbox_Birthday_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(textbox_Birthday.Text, @"^(((0[1-9]|[12][0-9]|3[01])[- /.](0[13578]|1[02])|(0[1-9]|[12][0-9]|30)[- /.](0[469]|11)|(0[1-9]|1\d|2[0-8])[- /.]02)[- /.]\d{4}|29[- /.]02[- /.](\d{2}(0[48]|[2468][048]|[13579][26])|([02468][048]|[1359][26])00))$"))
            {
                textblock_Dateright.Visibility = Visibility.Visible;
            }
            else
            {
                textblock_Dateright.Visibility = Visibility.Hidden;
            }
        }
        // detecting if birthday is in a correct format or not
        private void textbox_Birthday_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Regex regex = new Regex("[^0-9]+");
            if (Regex.IsMatch(textbox_Birthday.Text+e.Text, @"^(((0[1-9]|[12][0-9]|3[01])[- /.](0[13578]|1[02])|(0[1-9]|[12][0-9]|30)[- /.](0[469]|11)|(0[1-9]|1\d|2[0-8])[- /.]02)[- /.]\d{4}|29[- /.]02[- /.](\d{2}(0[48]|[2468][048]|[13579][26])|([02468][048]|[1359][26])00))$"))
            {
                textblock_Dateright.Visibility = Visibility.Visible;
            }
            else
            {
                textblock_Dateright.Visibility = Visibility.Hidden;
            }
            //e.Handled = regex.IsMatch(e.Text);
            //e.Handled = Regex.IsMatch(textbox_Birtyday.Text, @"^(((0[1-9]|[12][0-9]|3[01])[- /.](0[13578]|1[02])|(0[1-9]|[12][0-9]|30)[- /.](0[469]|11)|(0[1-9]|1\d|2[0-8])[- /.]02)[- /.]\d{4}|29[- /.]02[- /.](\d{2}(0[48]|[2468][048]|[13579][26])|([02468][048]|[1359][26])00))$");
        }

    }
}
