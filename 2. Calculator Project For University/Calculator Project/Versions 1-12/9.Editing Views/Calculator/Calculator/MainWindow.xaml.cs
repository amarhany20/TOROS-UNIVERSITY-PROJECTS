using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
            //REGISTERATION START

            //initializing Register variables
            string newRegisterUsername = txtRegister_EnterUsername.Text;
            string newRegisterPassword = pwRegister_EnterPassword.Password;
            string newRegisterRepeatPassword = pwRegister_RepeatPassword.Password;
            //Ending initialization for Register Variables
            //...........................................................................................................

            //initializing Username Registration rules.
            //..............................
            //Rule 1 : lower cassing and english culture
            newRegisterUsername = newRegisterUsername.ToLower();//ToLower to make it always small case not capital
            
            //Rule 1 end
            //..............................
            //Rule 2 : trimming space in the end or the start
            newRegisterUsername = newRegisterUsername.Trim();
            //Rule 2 end
            //..............................
            //Rule 3 : Limit to 3 Characters
            if (newRegisterUsername.Length < 3)
            {
                MessageBox.Show("Invalid Username!\nYour username is smaller than 3 characters.\nTry adding more characters");
                return;
            }
            //Rule 3 end
            //..............................
            //Rule 4 : Rejecting Numbers at the start
            bool isfirstindexnumber = char.IsDigit(newRegisterUsername[0]);
            if (isfirstindexnumber)
            {
                MessageBox.Show("Invalid Username!\nThe first Character is a number.\nFirst Character should be a letter.");
                return;
            }
            //Rule 4 end
            //..............................
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
            //..............................
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
            //
            //..............................
            //Ending Registeration Username Rules

            //...........................................................................................................

            //Check
            //MessageBox.Show("CHECKING.. " + newRegisterUsername);
            //Check

            //...........................................................................................................

            //initializing Registeration Password Rules
            //..............................
            //Rule 1 : At Least 8 Characters Password
            if (newRegisterPassword.Length < 8)
            {
                MessageBox.Show($"Invalid Password!\nThe password must be at least 8 characters long. please try again and try adding more characters to the password");
                return;
            }
            //Rule 1 end
            //..............................

            //Notes for Hocam : The code below is made if you want the 3 rules of lower and upper and number to be checked in one process.
            //if (!newPassword.Any(char.IsUpper) || !newPassword.Any(char.IsLower) || !newPassword.Any(char.IsNumber))
            //{
            //    MessageBox.Show($"Invalid Password!\nThe password must have 1 upper case letter , 1 lower case letter and a number.");
            //    return;
            //}
            //Notes for Hocam : The code above is made if you want the 3 rules of lower and upper and number to be checked in one process.
            //..............................
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
            //..............................
            if (newRegisterPassword.Contains(registerationUsernamePasswordSeparator))
            {
                MessageBox.Show($"Invalid Password!\nInalid Character \"{registerationUsernamePasswordSeparator}\"");
                return;
            }
            //Rule 2 end
            //..............................
            //Rule 3 : At Least 1 upper case
            if (!newRegisterPassword.Any(char.IsUpper))
            {
                MessageBox.Show("Invalid Password!\nThe password should contain at least one upper case letter.");
                return;
            }
            //Rule 3 end
            //..............................
            //Rule 4 : At Least 1 Lower Case
            if (!newRegisterPassword.Any(char.IsLower))
            {
                MessageBox.Show("Invalid Password!\nThe password should contain at least one lower case letter.");
                return;
            }
            //Rule 4 end
            //..............................
            //Rule 5 : At least 1 Number
            if (!newRegisterPassword.Any(char.IsNumber))
            {
                MessageBox.Show("Invalid Password!\nThe password should contain at least number.");
                return;
            }
            //Rule 5 End
            //..............................
            //Rule 6 : Repeat Password == Password ?
            if (newRegisterRepeatPassword != newRegisterPassword)
            {
                MessageBox.Show("Passwords doesn't match!\nPlease try typing your password again.");
                return;
            }
            //Rule 6 end
            //..............................
            //Ending Registeration Password Rules

            //...........................................................................................................

            //Regiseration Proccess

            File.AppendAllText("Users.txt", newRegisterUsername + registerationUsernamePasswordSeparator + Encryption.Encode(newRegisterPassword) + Environment.NewLine);
            MessageBox.Show($"Registeraion successful. Welcome {newRegisterUsername}");
            lblSignedInInfo.Content = $"{newRegisterUsername}";
            UsernameAccepted();
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
            //..............................
            //Rule 1 : Trimming Spaces
            newLoginUsername = newLoginUsername.Trim();
            //Rule 1 End
            //..............................
            //Rule 2 : Empty Username
            if (newLoginUsername.Length == 0)
            {
                MessageBox.Show("Username is empty!");
                return;
            }
            //Rule 2 End
            //..............................
            //Password Rule initialization

            //Rule 1 : Empty Password
            if (newLoginPassword.Length == 0)
            {
                MessageBox.Show("Password is empty!");
                return;
            }
            //Rule 1 end
            //..............................
            //Password Rules End




            //...........................................................................................................
            //Login Process
            bool usernameLoginsuccessful = false;
            File.AppendAllText("Users.txt", "");
            foreach (var vrLoginUsernameChecking in File.ReadLines("Users.txt"))
            {
                if (vrLoginUsernameChecking.Split(registerationUsernamePasswordSeparator)[0] == (newLoginUsername))
                {
                    //username exists
                    usernameLoginsuccessful = true;
                    if (vrLoginUsernameChecking == (newLoginUsername + registerationUsernamePasswordSeparator + Encryption.Encode(newLoginPassword)))
                    {
                        //Matching username and password then he enters the app
                        MessageBox.Show($"Login Successful! Welcome {newLoginUsername}");
                        lblSignedInInfo.Content = $"{newLoginUsername}";
                        UsernameAccepted();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Login Failed! Wrong Password");
                        return;
                    }

                }

            }
            if (!usernameLoginsuccessful)
            {
                MessageBox.Show("Login Failed! Username not found. Make sure you write the username correct or Register if you don't have a username.");
                return;
            }


        }
        public void UsernameAccepted()//Method for the sign in process
        {
            tapCalculatorMainWindow.IsEnabled = true;
            tapCalculatorMainWindow.Focus();
            tapLogin.IsEnabled = false;
            tapRegister.IsEnabled = false;
            btn_LogOutButton.Visibility = Visibility.Visible;
            txt_InputPanel.Text = "";
            txt_Results.Text = "";
            lstBox_History.Items.Clear();
            ListBoxPresentingHistory();
            txt_InputPanel.Focus();


        }
        public void LogOutProcess()//Method for the log out process
        {
            MessageBox.Show("Logging out!");
            tapCalculatorMainWindow.IsEnabled = false;
            tapLogin.IsEnabled = true;
            tapRegister.IsEnabled = true;
            txt_InputPanel.Text = "";
            txtLogin_Username.Text = "";
            txtRegister_EnterUsername.Text = "";
            txt_Results.Text = "";
            pwLogin_Password.Password = "";
            pwRegister_EnterPassword.Password = "";
            pwRegister_RepeatPassword.Password = "";
            lstBox_History.Items.Clear();
            tapLogin.Focus();
            btn_LogOutButton.Visibility = Visibility.Collapsed;
            lblSignedInInfo.Content = "Welcome User.";
        }
        //CALCULATOR START//CALCULATOR START//CALCULATOR START//CALCULATOR START//CALCULATOR START//CALCULATOR START//CALCULATOR START//CALCULATOR START//CALCULATOR START//CALCULATOR START//CALCULATOR START//CALCULATOR START//CALCULATOR START//CALCULATOR START//CALCULATOR START//CALCULATOR START//CALCULATOR START//CALCULATOR START//CALCULATOR START//CALCULATOR START
        public void InputPanelFocus()//I made this to always focus the textbox because before when I press the numbers the focus of the text box is lost and I dont want that.
        {
            txt_InputPanel.Focus();
            txt_InputPanel.SelectionStart = txt_InputPanel.Text.Length;
        }
        private void Btn_1_Click(object sender, RoutedEventArgs e)
        {
            txt_InputPanel.Text = txt_InputPanel.Text + "1";
            InputPanelFocus();
        }

        private void Btn_2_Click(object sender, RoutedEventArgs e)
        {
            txt_InputPanel.Text = txt_InputPanel.Text + "2";
            InputPanelFocus();
        }

        private void Btn_3_Click(object sender, RoutedEventArgs e)
        {
            txt_InputPanel.Text = txt_InputPanel.Text + "3";
            InputPanelFocus();
        }

        private void Btn_4_Click(object sender, RoutedEventArgs e)
        {
            txt_InputPanel.Text = txt_InputPanel.Text + "4";
            InputPanelFocus();
        }

        private void Btn_5_Click(object sender, RoutedEventArgs e)
        {
            txt_InputPanel.Text = txt_InputPanel.Text + "5";
            InputPanelFocus();
        }

        private void Btn_6_Click(object sender, RoutedEventArgs e)
        {
            txt_InputPanel.Text = txt_InputPanel.Text + "6";
            InputPanelFocus();
        }

        private void Btn_7_Click(object sender, RoutedEventArgs e)
        {
            txt_InputPanel.Text = txt_InputPanel.Text + "7";
            InputPanelFocus();
        }

        private void Btn_8_Click(object sender, RoutedEventArgs e)
        {
            txt_InputPanel.Text = txt_InputPanel.Text + "8";
            InputPanelFocus();
        }

        private void Btn_9_Click(object sender, RoutedEventArgs e)
        {
            txt_InputPanel.Text = txt_InputPanel.Text + "9";
            InputPanelFocus();
        }

        private void Btn_0_Click(object sender, RoutedEventArgs e)
        {
            txt_InputPanel.Text = txt_InputPanel.Text + "0";
            InputPanelFocus();
        }

        private void Btn_BracketOpener_Click(object sender, RoutedEventArgs e)
        {
            txt_InputPanel.Text = txt_InputPanel.Text + "(";
            InputPanelFocus();
        }

        private void Btn_BracketCloser_Click(object sender, RoutedEventArgs e)
        {
            txt_InputPanel.Text = txt_InputPanel.Text + ")";
            InputPanelFocus();
        }

        private void Btn_Plus_Click(object sender, RoutedEventArgs e)
        {
            txt_InputPanel.Text = txt_InputPanel.Text + "+";
            InputPanelFocus();
        }

        private void Btn_Minus_Click(object sender, RoutedEventArgs e)
        {
            txt_InputPanel.Text = txt_InputPanel.Text + "-";
            InputPanelFocus();
        }

        private void Btn_Multiplication_Click(object sender, RoutedEventArgs e)
        {
            txt_InputPanel.Text = txt_InputPanel.Text + "*";
            InputPanelFocus();
        }

        private void Btn_Divided_Click(object sender, RoutedEventArgs e)
        {
            txt_InputPanel.Text = txt_InputPanel.Text + "/";
            InputPanelFocus();
        }

        private void Btn_Del_Click(object sender, RoutedEventArgs e)
        {
            txt_InputPanel.Text = txt_InputPanel.Text.Remove(txt_InputPanel.Text.Length - 1, 1);//Explain that code to me please.
            InputPanelFocus();
        }

        private void Btn_C_Click(object sender, RoutedEventArgs e)
        {
            txt_InputPanel.Text = "";
            txt_Results.Text = "";
            InputPanelFocus();

        }
        private void Btn_Dot_Click(object sender, RoutedEventArgs e)
        {
            txt_InputPanel.Text = txt_InputPanel.Text + ".";
            InputPanelFocus();
        }
        public void SavingProcess()//Saving to text Method used in Equal
        {
            string savedHistoryFileName = lblSignedInInfo.Content.ToString();
            File.AppendAllText(Path.Combine($"{savedHistoryFileName}.txt"), txt_InputPanel.Text.ToString() + " = " + txt_Results.Text.ToString() + Environment.NewLine);
        }
        public void ListBoxPresentingHistory()//inversing reading the text to be put in the history
        {
            string savedHistoryFileName = lblSignedInInfo.Content.ToString();
            try
            {
                string[] lines = File.ReadAllLines($"{savedHistoryFileName}.txt");
                foreach (var vrHistoryLines in lines)
                {
                    lstBox_History.Items.Add(vrHistoryLines);
                }
                for (int i = 0; i < lstBox_History.Items.Count / 2; i++)
                {
                    var tmp = lstBox_History.Items[i];
                    lstBox_History.Items[i] = lstBox_History.Items[lstBox_History.Items.Count - i - 1];
                    lstBox_History.Items[lstBox_History.Items.Count - i - 1] = tmp;
                }
            }
            catch (FileNotFoundException)
            {

            }
        }

        private void Btn_Equal_Click(object sender, RoutedEventArgs e)
        {
            string allCalculationsInInputPanel = txt_InputPanel.Text;
            //Rules of Equal initialization :
            foreach (char c in allCalculationsInInputPanel.Trim())
            {
                if (char.IsWhiteSpace(c) == true)
                {
                    MessageBox.Show("Error!\nSpace detected. Please erase it .");
                    return;
                }
            }
            if (allCalculationsInInputPanel.Length==0)
            {
                MessageBox.Show("input panel is empty!");
                return;
            }

            try
            {
                var result = new DataTable().Compute(allCalculationsInInputPanel, null);
                txt_Results.Text = result.ToString();
                lstBox_History.Items.Insert(0, allCalculationsInInputPanel + " = " + result);
                SavingProcess();
                txt_InputPanel.Clear();
                InputPanelFocus();


            }
            catch (SyntaxErrorException)
            {
                MessageBox.Show($"Error!\nOperators are placed wrong.Please check them and try again");
                return;
            }
            catch (OverflowException)//INCASE!
            {
                MessageBox.Show("Error!\n Very big numbers try to decrease the numbers a little bit ");
                return;
            }



        }
        private void Txt_InputPanel_PreviewTextInput_1(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9.-.+./.*.-]+").IsMatch(e.Text);//I need Regex Codes explanation
        }

        private void Btn_LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            LogOutProcess();
        }

        
    }

}
