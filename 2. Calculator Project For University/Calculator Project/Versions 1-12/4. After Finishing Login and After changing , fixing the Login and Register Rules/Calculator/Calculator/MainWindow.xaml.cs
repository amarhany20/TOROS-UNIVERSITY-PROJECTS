using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

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
            //REGISTER START

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
            List<string> lstInvalidCharacters = new List<string> { "%", "-", "$", "/", "\\", "(", ")", "[", "]", ",", "!", "{", "}", "=", "&", "?", "\"", "’", "“", "”", "‘", ":" };
            foreach (var vrCheckingInvalidCharacters in lstInvalidCharacters)
            {
                if (newRegisterUsername.Contains(vrCheckingInvalidCharacters.ToString()))
                {
                    MessageBox.Show($"Invalid Username!\nInalid Character \"{vrCheckingInvalidCharacters}\" detected.\nPlease erase it as  % - $ / \\ ( ) [ ] , !" + " { } = & ? \" ’ “ ” ‘ : Characters are not accepted. ");
                    return;
                }
            }
            //Rule 5 end

            //Rule 6 :Checking if the username already exists.
            File.AppendAllText("Users.txt", "");
            foreach (var vrRegistarationUsernameChecking in File.ReadLines("Users.txt"))
            {
                if (vrRegistarationUsernameChecking.Split(registerationUsernamePasswordSeparator)[0] == newRegisterUsername)
                {
                    MessageBox.Show("Username already exists. Please try changing your username");
                    return;
                }
            }
            //Rule 6 end
            //Ending Registeration Username Rules




            //...........................................................................................................

            //Check
            //MessageBox.Show("CHECKING.. " + newRegisterUsername);
            //Check





            //...........................................................................................................

            //initializing Registeration Password Rules

            //Rule 1 : At Least 8 Characters Password
            if (newRegisterPassword.Length < 8)
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


            //Rule 2 : Banning invalid Charactters 
            //IF U WANT FULL INVALID CHARACTERS BANNING FROM THE PASSWORD the code is ready below
            //foreach (var vrCheckingInvalidCharacters in lstInvalidCharacters)
            //{
            //    if (newRegisterPassword.Contains(vrCheckingInvalidCharacters.ToString()))
            //    {
            //        MessageBox.Show($"Invalid Password!\nInalid Character \"{vrCheckingInvalidCharacters}\" detected.\nPlease erase it as  % - $ / \\ ( ) [ ] , !" + " { } = & ? \" ’ “ ” ‘ : Characters are not accepted. ");
            //        return;
            //    }
            //}
            if (newRegisterPassword.Contains(registerationUsernamePasswordSeparator))
            {
                MessageBox.Show($"Invalid Password!\nInalid Character \"{registerationUsernamePasswordSeparator}\"");
                return;
            }
            //Rule 2 end

            //Rule 3 : At Least 1 upper case
            if (!newRegisterPassword.Any(char.IsUpper))
            {
                MessageBox.Show("Invalid Password!\nThe password should contain at least one upper case letter.");
                return;
            }
            //Rule 3 end

            //Rule 4 : At Least 1 Lower Case
            if (!newRegisterPassword.Any(char.IsLower))
            {
                MessageBox.Show("Invalid Password!\nThe password should contain at least one lower case letter.");
                return;
            }
            //Rule 4 end

            //Rule 5 : At least 1 Number
            if (!newRegisterPassword.Any(char.IsNumber))
            {
                MessageBox.Show("Invalid Password!\nThe password should contain at least number.");
                return;
            }
            //Rule 5 End

            //Rule 6 : Repeat Password == Password ?
            if (newRegisterRepeatPassword != newRegisterPassword)
            {
                MessageBox.Show("Passwords doesn't match!\nPlease try typing your password again.");
                return;
            }
            //Rule 6 end

            //Ending Registeration Password Rules





            //...........................................................................................................

            //Regiseration Proccess

            File.AppendAllText("Users.txt", newRegisterUsername + registerationUsernamePasswordSeparator + Encryption.Encode(newRegisterPassword) + Environment.NewLine);
            MessageBox.Show($"Registeraion successful. Welcome {newRegisterUsername}");
            lblSignedInInfo.Content = $"Welcome {newRegisterUsername}";
            tabCalculatorMainWindow.IsEnabled = true;
            tabCalculatorMainWindow.Focus();
            //Registeration Process End
            //REGISTER END




            //...........................................................................................................

        }

        private void BtnLogin_Login_Click(object sender, RoutedEventArgs e)
        {
            //Login Start
            //Login variables initialization
            string newLoginUsername = txtLogin_Username.Text.ToLower();
            string newLoginPassword = pwLogin_Password.Password.ToString();

            //Login Variables end

            //Username Rules initialization

            //Rule 1 : Trimming Spaces
            newLoginUsername = newLoginUsername.Trim();
            //Rule 1 End

            //Rule 2 : Empty Username
            if (newLoginUsername.Length == 0)
            {
                MessageBox.Show("Username is empty!");
                return;
            }
            //Rule 2 End

            //Rule 3 

            //Rule 3 end

            //Rule 4

            //Rule 4 end

            //Rule 5 

            //Rule 5 end

            //Username Rules End



            //Password Rule initialization

            //Rule 1 : Empty Password
            if (newLoginPassword.Length == 0)
            {
                MessageBox.Show("Password is empty!");
                return;
            }
            //Rule 1 end
            //Password Rules End




            //...........................................................................................................
            //Login Process
            bool loginsuccessful = false;
            foreach (var vrLoginUsernameChecking in File.ReadLines("Users.txt"))
            {
                if (vrLoginUsernameChecking.Split(registerationUsernamePasswordSeparator)[0] == (newLoginUsername))
                {
                    //username exists
                    loginsuccessful = true;
                    if (vrLoginUsernameChecking == (newLoginUsername +registerationUsernamePasswordSeparator + Encryption.Encode(newLoginPassword)))
                    {
                        //Matching username and password then he enters the app
                        MessageBox.Show($"Login Successful! Welcome {newLoginUsername}");
                        lblSignedInInfo.Content = $"Welcome {newLoginUsername}";
                        tabCalculatorMainWindow.IsEnabled = true ;
                        tabCalculatorMainWindow.Focus();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Login Failed! Wrong Password");
                        return;
                    }

                }

            }
            if (!loginsuccessful)
            {
                MessageBox.Show("Login Failed! Username not found. Make sure you write the username correct or Register if you don't have a username.");
                return;
            }
        }
    }
}
