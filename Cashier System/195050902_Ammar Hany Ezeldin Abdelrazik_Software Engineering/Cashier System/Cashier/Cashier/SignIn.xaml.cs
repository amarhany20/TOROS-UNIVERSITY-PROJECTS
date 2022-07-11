using System;
using System.Data.SqlClient;
using System.Windows;

namespace Cashier
{
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        public SignIn()
        {
            InitializeComponent();
            if (!IsServerConnected(App.connection))
            {
                System.Windows.Forms.MessageBox.Show("Can't Connect to Server");
            }
        }
        // Buttons
        private void button_SignIn_Click(object sender, RoutedEventArgs e)
        {

            if (textbox_Username.Text.Trim().Length == 0)
            {
                System.Windows.Forms.MessageBox.Show("Username is empty");
                return;
            } if (password_Password.Password.Trim().Length == 0)
            {
                System.Windows.Forms.MessageBox.Show("Password is empty");
                return;
            }
            SqlConnection con = new SqlConnection(App.connection);
            SqlCommand cmd = new SqlCommand("Select * from Users where username=@username", con);
            cmd.Parameters.AddWithValue("@username", textbox_Username.Text.Trim().ToString());
            try
            {
               
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                    if (reader["Password"].ToString().Equals(password_Password.Password.ToString(), StringComparison.InvariantCulture))
                    {
                        if (reader["Activated"].ToString()=="True")
                        {

                            //signed in successfully
                            MessageBox.Show("Welcome "+ reader["Username"].ToString() +". your ID is "+ reader["ID"].ToString());
                            MainWindow.userID = int.Parse(reader["ID"].ToString());
                            MainWindow.username = reader["Username"].ToString();
                            MainWindow.isAdmin = bool.Parse(reader["IsAdmin"].ToString());
                            MainWindow.signInSuccessful=true;
                            this.Close();
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("Your Account isn't activated yet, Please tell any admin to activate it!");
                            return;
                        }
                    }
                    else
                    {//password wrong
                        MessageBox.Show("Password is wrong!");
                        return;
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Username is not found");
                }

                reader.Close();
                reader.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show("server Error"+ex.ToString());
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }

        private void button_SetServer_Click(object sender, RoutedEventArgs e)//that's for setting the server if changed
        {
            if (textbox_ServerName.Text.Trim().Length == 0)
            {
                MessageBox.Show("Server is empty");
                return;
            }
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you want to set this server connection string?", "Server String Change Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                if (!IsServerConnected(textbox_ServerName.Text))
                {
                    System.Windows.Forms.MessageBox.Show("Can't Connect to Server");
                    return;
                }
                App.connection = textbox_ServerName.Text;
                System.Windows.Forms.MessageBox.Show("Done! Server Connected Succesfully");
            }
        }

        //functions
        private static bool IsServerConnected(string connectionString)//this function is made for checking if the connection string is connectable or not
        {
            
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                using (connection)
            {
               
                    connection.Open();
                    return true;

                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
