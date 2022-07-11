using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
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
            File.AppendAllText("Users.txt", "");
            InitializeComponent();
            FillingTheDictionatary();
            Btn_0.ToolTip = "Click here to add number 0 to the input panel.";
            Btn_1.ToolTip = "Click here to add number 1 to the input panel.";
            Btn_2.ToolTip = "Click here to add number 2 to the input panel.";
            Btn_3.ToolTip = "Click here to add number 3 to the input panel.";
            Btn_4.ToolTip = "Click here to add number 4 to the input panel.";
            Btn_5.ToolTip = "Click here to add number 5 to the input panel.";
            Btn_6.ToolTip = "Click here to add number 6 to the input panel.";
            Btn_7.ToolTip = "Click here to add number 7 to the input panel.";
            Btn_8.ToolTip = "Click here to add number 8 to the input panel.";
            Btn_9.ToolTip = "Click here to add number 9 to the input panel.";
            Btn_BracketOpener.ToolTip = "Click here to add ( to the input panel.";
            Btn_BracketCloser.ToolTip = "Click here to add ) to the input panel.";
            Btn_Dot.ToolTip = "Click here to add a dot to the input panel.";
            Btn_Plus.ToolTip = "Click here to add a + to the input panel.";
            Btn_Minus.ToolTip = "Click here to add a - to the input panel.";
            Btn_Multiplication.ToolTip = "Click here to add a X sign to the input panel.";
            Btn_Divided.ToolTip = "Click here to add a ÷ to the input panel.";
            Btn_Equal.ToolTip = "Click here to calculate the resual";
            Btn_C.ToolTip = "Click here to clear the input panel";
            Btn_Del.ToolTip = "Click here to remove the last number or operator";
            btnLogin_Login.ToolTip = "Click here to Login";
            btnRegister_Register.ToolTip = "Click Here to Register";
        }





        //Separator Initialization

        public static char registerationUsernamePasswordSeparator = ':';

        //Separator End
        //____________________________________________________________________________________________________________________

        //METHODS START

        public void LogInProcess()//Method for the Log in process
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
        public void InputPanelFocus()//I made this to always focus the textbox because before when I press the numbers the focus of the text box is lost and I dont want that.
        {
            txt_InputPanel.Focus();
            txt_InputPanel.SelectionStart = txt_InputPanel.Text.Length;
        }
        public void SavingProcess()//Saving to text Method used in Equal
        {
            string savedHistoryFileName = lblSignedInInfo.Content.ToString();
            File.AppendAllText(Path.Combine($"{savedHistoryFileName}.txt"), txt_InputPanel.Text.ToString() + " = " + txt_Results.Text.ToString() + Environment.NewLine);
            FillingTheDictionatary();
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
        private string NormalizeUsername(string srUserNameofUser)
        {
            var vrnormal1 = srUserNameofUser.ToUpper(culture: System.Globalization.CultureInfo.CreateSpecificCulture("tr-TR")).ToLower(culture: System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));

            return RemoveDiacritics(vrnormal1);
        }
        static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
        public void DeleteHistory()
        {
            string savedHistoryFileName = lblSignedInInfo.Content.ToString();
            File.WriteAllText($"{savedHistoryFileName}.txt", String.Empty);
            lstBox_History.Items.Clear();

        }

        Dictionary<string, string> dicUsers;//That's the dictionary I used it in debugging and testing.
        public void FillingTheDictionatary()
        {

            dicUsers = new Dictionary<string, string>();
            foreach (var item in File.ReadLines("Users.txt"))
            {
                if (!dicUsers.ContainsKey(item.Split(':').First()))
                {    //first one username second one password
                    dicUsers.Add(item.Split(':')[0], item.Split(':')[1]);//first one key , second value
                }
            }

        }
        private bool checkUsernameAndPassword(string username, string password)
        {


            if (dicUsers.ContainsKey(username))
            {    //here we are getting password of that particular username and comparing
                if (dicUsers[username] == password)//this is comparing value of that particular key
                {
                    return true;
                }
                return false;
            }
            else
                return false;
        }

        //METHODS END
        //____________________________________________________________________________________________________________________

        //Program Start...........................................................................................................

        private void BtnRegister_Register_Click(object sender, RoutedEventArgs e)//Registeration Process
        {
            //REGISTERATION START

            //initializing Register variables

            GlobalVariables registeration = new GlobalVariables();
            registeration.newRegisterUsername = txtRegister_EnterUsername.Text;
            registeration.newRegisterPassword = pwRegister_EnterPassword.Password;
            registeration.newRegisterRepeatPassword = pwRegister_RepeatPassword.Password;

            //Ending initialization for Register Variables
            //____________________________________________________________________________________________________________________

            //initializing Username Registration rules.
            //..............................
            //Rule 1 : lower cassing and english culture
            registeration.newRegisterUsername = NormalizeUsername(registeration.newRegisterUsername);
            //Rule 1 end
            //..............................
            //Rule 2 : trimming space in the end or the start
            registeration.newRegisterUsername = registeration.newRegisterUsername.Trim();
            //Rule 2 end
            //..............................
            //Rule 3 : Limit to 3 Characters
            if (registeration.newRegisterUsername.Length < 3)
            {
                MessageBox.Show("Invalid Username!\nYour username is smaller than 3 characters.\nTry adding more characters");
                return;
            }
            //Rule 3 end
            //..............................
            //Rule 4 : Rejecting Numbers at the start
            bool isfirstindexnumber = char.IsDigit(registeration.newRegisterUsername[0]);
            if (isfirstindexnumber)
            {
                MessageBox.Show("Invalid Username!\nThe first Character is a number.\nFirst Character should be a letter.");
                return;
            }
            //Rule 4 end
            //..............................
            //Rule 5 : invalid characters
            InvalidCharacters invtemplist = new InvalidCharacters();
            foreach (var vrCheckingInvalidCharacters in invtemplist.lstInvalidCharacters)
            {
                if (registeration.newRegisterUsername.Contains(vrCheckingInvalidCharacters.ToString()))
                {
                    MessageBox.Show($"Invalid Username!\nInalid Character \"{vrCheckingInvalidCharacters}\" detected.\nPlease erase it as  % - $ / \\ ( ) [ ] , !" + " { } = & ? \" ’ “ ” ‘ : Characters are not accepted. ");
                    return;
                }
            }
            //Rule 5 end
            //..............................
            ////Rule 6 :Checking if the username already exists.
            //File.AppendAllText("Users.txt", "");
            //foreach (var vrRegistarationUsernameChecking in File.ReadLines("Users.txt"))
            //{
            //    if (vrRegistarationUsernameChecking.Split(registerationUsernamePasswordSeparator)[0] == registeration.newRegisterUsername)
            //    {
            //        MessageBox.Show("Username already exists. Please try changing your username");
            //        return;
            //    }
            //}

            if (dicUsers.ContainsKey(registeration.newRegisterUsername))
            {
                {
                    MessageBox.Show("Username already exists. Please try changing your username");
                    return;
                }
            }

            //Rule 6 end
            //..............................
            //Ending Registeration Username Rules
            //____________________________________________________________________________________________________________________
            //initializing Registeration Password Rules
            //..............................
            //Rule 1 : At Least 8 Characters Password
            if (registeration.newRegisterPassword.Length < 8)
            {
                MessageBox.Show($"Invalid Password!\nThe password must be at least 8 characters long. please try again and try adding more characters to the password");
                return;
            }
            //Rule 1 end
            //..............................
            //Rule 2 : ':' Banning
            if (registeration.newRegisterPassword.Contains(registerationUsernamePasswordSeparator))
            {
                MessageBox.Show($"Invalid Password!\nInalid Character \"{registerationUsernamePasswordSeparator}\"");
                return;
            }
            //Rule 2 end
            //..............................
            //Rule 3 : At Least 1 upper case
            if (!registeration.newRegisterPassword.Any(char.IsUpper))
            {
                MessageBox.Show("Invalid Password!\nThe password should contain at least one upper case letter.");
                return;
            }
            //Rule 3 end
            //..............................
            //Rule 4 : At Least 1 Lower Case
            if (!registeration.newRegisterPassword.Any(char.IsLower))
            {
                MessageBox.Show("Invalid Password!\nThe password should contain at least one lower case letter.");
                return;
            }
            //Rule 4 end
            //..............................
            //Rule 5 : At least 1 Number
            if (!registeration.newRegisterPassword.Any(char.IsNumber))
            {
                MessageBox.Show("Invalid Password!\nThe password should contain at least number.");
                return;
            }
            //Rule 5 End
            //..............................
            //Rule 6 : Repeat Password == Password ?
            if (registeration.newRegisterRepeatPassword != registeration.newRegisterPassword)
            {
                MessageBox.Show("Passwords doesn't match!\nPlease try typing your password again.");
                return;
            }
            //Rule 6 end
            //..............................
            //Ending Registeration Password Rules
            //____________________________________________________________________________________________________________________
            //Regiseration Proccess
            File.AppendAllText("Users.txt", registeration.newRegisterUsername + registerationUsernamePasswordSeparator + Encryption.Encode(registeration.newRegisterPassword) + Environment.NewLine);
            MessageBox.Show($"Registeraion successful. Welcome {registeration.newRegisterUsername}");
            lblSignedInInfo.Content = $"{registeration.newRegisterUsername}";
            LogInProcess();
            //Registeration Process End
            //____________________________________________________________________________________________________________________
            //REGISTER END
            //____________________________________________________________________________________________________________________
        }
        private void BtnLogin_Login_Click(object sender, RoutedEventArgs e)//Log in Processm
        {
            //Login Start
            //Login variables initialization
            GlobalVariables loggingIn = new GlobalVariables();
            loggingIn.newLoginUsername = txtLogin_Username.Text.ToLower();
            loggingIn.newLoginPassword = pwLogin_Password.Password.ToString();
            //Login Variables end
            //____________________________________________________________________________________________________________________
            //Username Rules initialization
            //..............................
            //Rule 1 : Trimming Spaces
            loggingIn.newLoginUsername = loggingIn.newLoginUsername.Trim();
            //Rule 1 End
            //..............................
            //Rule 2 : Empty Username
            if (loggingIn.newLoginUsername.Length == 0)
            {
                MessageBox.Show("Username is empty!");
                return;
            }
            //Rule 2 End
            //..............................
            //Username Rules End
            //____________________________________________________________________________________________________________________

            //Password Rule initialization

            //Rule 1 : Empty Password
            if (loggingIn.newLoginPassword.Length == 0)
            {
                MessageBox.Show("Password is empty!");
                return;
            }
            //Rule 1 end
            //..............................
            //Password Rules End
            //____________________________________________________________________________________________________________________
            //Login Process
            bool usernameLoginsuccessful = false;
            if (dicUsers.ContainsKey(loggingIn.newLoginUsername))
            {
                //username exists
                usernameLoginsuccessful = true;
                if (dicUsers[loggingIn.newLoginUsername] == Encryption.Encode(loggingIn.newLoginPassword))

                    //Matching username and password then he enters the app
                    MessageBox.Show($"Login Successful! Welcome {loggingIn.newLoginUsername}");
                lblSignedInInfo.Content = $"{loggingIn.newLoginUsername}";
                LogInProcess();
                return;
            }

            else
            {
                MessageBox.Show("Login Failed! Wrong Password");
                return;
            }
            if (!usernameLoginsuccessful)
            {
                MessageBox.Show("Login Failed! Username not found. Make sure you write the username correct or Register if you don't have a username.");
                return;
            }
    //____________________________________________________________________________________________________________________


}

//CALCULATOR START

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
    try
    {
        txt_InputPanel.Text = txt_InputPanel.Text.Remove(txt_InputPanel.Text.Length - 1, 1);//Explain that code to me please.
        InputPanelFocus();
    }
    catch (System.ArgumentOutOfRangeException)
    {
        Console.WriteLine("Can't Erase More");
    }

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
private void Btn_Equal_Click(object sender, RoutedEventArgs e)
{
    string allCalculationsInInputPanel = txt_InputPanel.Text.Replace(" ", String.Empty);
    //Rules of Equal initialization :
    foreach (char c in allCalculationsInInputPanel.Trim())
    {
        if (char.IsWhiteSpace(c) == true)
        {
            MessageBox.Show("Error!\nSpace detected. Please erase it .");
            return;
        }
    }
    if (allCalculationsInInputPanel.Length == 0)
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
    catch (System.Data.EvaluateException)
    {
        MessageBox.Show("Error! \n You entered wrong characters in the input panel . Only Numbers , Brackets and Operators.");
    }



}
private void Txt_InputPanel_PreviewTextInput_1(object sender, System.Windows.Input.TextCompositionEventArgs e)
{
    e.Handled = new Regex("[^0-9.-.+./.*.-.(.)]+").IsMatch(e.Text);//I need Regex Codes explanation
}
private void Btn_LogOutButton_Click(object sender, RoutedEventArgs e)
{
    LogOutProcess();
}

private void Btn_DeleteHistory_Click(object sender, RoutedEventArgs e)
{
    DeleteHistory();
}
    }
}
