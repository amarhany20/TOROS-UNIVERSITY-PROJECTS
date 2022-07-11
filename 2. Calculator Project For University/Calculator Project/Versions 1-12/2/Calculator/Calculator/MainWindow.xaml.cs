using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public static char registerationUsernamePasswordSeparator = ':';
        private void BtnRegister_Register_Click(object sender, RoutedEventArgs e)
        {
            //initializing Register variables
            string newRegisterUsername = txtRegister_EnterUsername.Text;
            string newRegisterPassword = pwRegister_EnterPassword.Password;
            string newRegisterRepeatPassword = pwRegister_RepeatPassword.Password;
            //Ending initialization for Register Variables
            //...........................................................................................................
            //initializing Username Registration rules.

            //Rule 1 : lower cassing
            newRegisterUsername = newRegisterUsername.ToLower();//ToLower to make it always small case not capital
            //Rule 2 : trimming space in the end or the start
            newRegisterUsername = newRegisterUsername.Trim();
            //Rule 2 end

            //Rule 3 : Limit to 3 Characters
            if (newRegisterUsername.Length < 3)
            {
                MessageBox.Show("Invalid Username!\nYour username is smaller than 3 characters.\nTry adding more characters");
                return;
            }
            //Rule 3 end

            //Rule 4 : Rejecting Numbers at the start
            bool isfirstindexnumber = char.IsDigit(newRegisterUsername[0]);
            if (isfirstindexnumber)
            {
                MessageBox.Show("Invalid Username!\nThe first Character is a number.\nFirst Character should be a letter.");
                return;
            }
            //Rule 4 end

            //Rule 5 : invalid characters
            List<string> lstInvalidCharacters = new List<string> { "%", "-", "$", "/", "\\", "(", ")", "[", "]", ",", "!", "{", "}", "=", "&", "?", "\"", "’", "“", "”", "‘" , ":" };
            foreach (var vrCheckingInvalidCharacters in lstInvalidCharacters)
            {
                if (newRegisterUsername.Contains(vrCheckingInvalidCharacters.ToString()))
                {
                    MessageBox.Show($"Invalid Username!\nInalid Character \"{vrCheckingInvalidCharacters}\" detected.\nPlease erase it as  % - $ / \\ ( ) [ ] , !"+" { } = & ? \" ’ “ ” ‘ : Characters are not accepted. ");
                    return;
                }
            }
            //Rule 5 end

            //Rule 6 :Checking if the username already exists.
            foreach (var vrRegistarationUsernameChecking in File.ReadLines("user.txt"))
            {
                if (vrRegistarationUsernameChecking.Split(registerationUsernamePasswordSeparator)[0]==newRegisterUsername)
                {
                    MessageBox.Show("Username already exists. Please try changing your username");
                    return;
                }
            }
            //Rule 6 end
            //...........................................................................................................
            //Ending Registeration Username Rules

            //Check
            MessageBox.Show("CHECKING.. " + newRegisterUsername);
            //Check

            //initializing Registeration Password Rules

            //Rule 1 : At Least 8 Characters Password
            if (newRegisterPassword.Length<8)
            {
                MessageBox.Show($"Invalid Password!\nThe password must be at least 8 characters long. please try again and try adding more characters to the password");
                return;
            }
            //Rule 1 end

            //Notes for Hocam : The code below is made if you want the 3 rules of lower and upper and number to be checked in one process.
            //if (!newPassword.Any(char.IsUpper) || !newPassword.Any(char.IsLower) || !newPassword.Any(char.IsNumber))
            //{
            //    MessageBox.Show($"Invalid Password!\nThe password must have 1 upper case letter , 1 lower case letter and a number.");
            //    return;
            //}
            //Notes for Hocam : The code above is made if you want the 3 rules of lower and upper and number to be checked in one process.

            //Rule 2 : At Least 1 upper case
            if (!newRegisterPassword.Any(char.IsUpper))
            {
                MessageBox.Show("Invalid Password!\nThe password should contain at least one upper case letter.");
                return;
            }
            //Rule 2 end

            //Rule 3 : At Least 1 Lower Case
            if (!newRegisterPassword.Any(char.IsLower))
            {
                MessageBox.Show("Invalid Password!\nThe password should contain at least one lower case letter.");
                return;
            }
            //Rule 3 end

            //Rule 4 : At least 1 Number
            if (!newRegisterPassword.Any(char.IsNumber))
            {
                MessageBox.Show("Invalid Password!\nThe password should contain at least number.");
                return;
            }
            //Rule 4 End

            //Rule 5 : Repeat Password == Password ?
            if (newRegisterRepeatPassword != newRegisterPassword)
            {
                MessageBox.Show("Passwords doesn't match!\nPlease try typing your password again.");
                return;
            }
            //Rule 5 end

            //Ending Registeration Password Rules

            //...........................................................................................................

            //Username Saving Proccess
            File.AppendAllText("User.txt", newRegisterUsername + registerationUsernamePasswordSeparator + newRegisterPassword + Environment.NewLine);




        }
    }
}
