using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
namespace Library_System
{
    public partial class MainWindow : Window
    {
        //Start
        public MainWindow()
        {
            try
            {
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;//centering
                InitializeComponent();
                InitialazingRegisterRankComboBox();
                tab_Student.Visibility = Visibility.Hidden;
                tab_Teacher.Visibility = Visibility.Hidden;
                tab_Admin.Visibility = Visibility.Hidden;
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 1 : \n" + x.ToString());
            }
        }
        //Non Static Methods:
        public void TapTransition(string username)
        {
            try
            {
                PublicVariables.currentlySignedInUsername = username;
                PublicVariables.currentlySignedInID = Convert.ToInt32(PublicMethods.GettingInfoFromSql(username.Trim().ToLower())[2]);
                //0 : Rank . 1 : SignedInInfo . 2 : SignedInID
                if (int.Parse(PublicMethods.GettingInfoFromSql(username.Trim().ToLower())[0]) == 3)
                {
                    label_SignedInNameAndSurname.Content = "Welcome " + PublicMethods.GettingInfoFromSql(username.Trim().ToLower())[1];
                    label_SignedInID.Content = "ID : " + PublicMethods.GettingInfoFromSql(username.Trim().ToLower())[2];
                    tab_Student.Visibility = Visibility.Visible;
                    tab_Student.IsEnabled = true;
                    tab_Student.Focus();
                    tab_Login.IsEnabled = false;
                    tab_Register.IsEnabled = false;
                    button_SignOut.Visibility = Visibility.Visible;
                    datagrid_LateBooks.ItemsSource = PublicMethods.StudentLateNotification(username).DefaultView;
                }
                else if (int.Parse(PublicMethods.GettingInfoFromSql(username.Trim().ToLower())[0]) == 2)
                {
                    label_SignedInNameAndSurname.Content = PublicMethods.GettingInfoFromSql(username.Trim().ToLower())[1];
                    label_SignedInID.Content = "ID : " + PublicMethods.GettingInfoFromSql(username.Trim().ToLower())[2];
                    tab_Student.Visibility = Visibility.Visible;
                    tab_Teacher.Visibility = Visibility.Visible;
                    tab_Teacher.IsEnabled = true;
                    tab_Teacher.Focus();
                    tab_Login.IsEnabled = false;
                    tab_Register.IsEnabled = false;
                    tab_Student.IsEnabled = false;
                    button_SignOut.Visibility = Visibility.Visible;
                }
                else if (int.Parse(PublicMethods.GettingInfoFromSql(username.Trim().ToLower())[0]) == 1)
                {
                    label_SignedInNameAndSurname.Content = PublicMethods.GettingInfoFromSql(username.Trim().ToLower())[1];
                    label_SignedInID.Content = "ID : " + PublicMethods.GettingInfoFromSql(username.Trim().ToLower())[2];
                    tab_Admin.Visibility = Visibility.Visible;
                    tab_Student.Visibility = Visibility.Visible;
                    tab_Teacher.Visibility = Visibility.Visible;
                    tab_Admin.IsEnabled = true;
                    tab_Admin.Focus();
                    tab_Login.IsEnabled = false;
                    tab_Register.IsEnabled = false;
                    tab_Student.IsEnabled = false;
                    tab_Teacher.IsEnabled = false;
                    button_SignOut.Visibility = Visibility.Visible;
                }
                else
                {
                    label_SignedInNameAndSurname.Content = "Welcome " + PublicMethods.GettingInfoFromSql(username.Trim().ToLower())[1];
                    label_SignedInID.Content = "ID : " + PublicMethods.GettingInfoFromSql(username.Trim().ToLower())[2];
                    tab_Student.Visibility = Visibility.Visible;
                    tab_Student.IsEnabled = true;
                    tab_Student.Focus();
                    tab_Login.IsEnabled = false;
                    tab_Register.IsEnabled = false;
                    button_SignOut.Visibility = Visibility.Visible;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 2 : \n" + x.ToString());
            }
        }
        private void InitialazingRegisterRankComboBox()
        {
            try
            {
                List<RegisterUserRanks> userRanks = new List<RegisterUserRanks>
            {
                new RegisterUserRanks { Rank = -1, RankTitle = "Select Rank" },
                new RegisterUserRanks { Rank = 1, RankTitle = "Admin" },
                new RegisterUserRanks { Rank = 2, RankTitle = "Teacher" },
                new RegisterUserRanks { Rank = 3, RankTitle = "Student" }
            };
                combobox_RegisterUserRank.ItemsSource = userRanks;
                combobox_RegisterUserRank.DisplayMemberPath = "RankTitle";
                combobox_RegisterUserRank.SelectedValuePath = "Rank";
                combobox_RegisterUserRank.SelectedIndex = 0;
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 3 : \n" + x.ToString());
            }
        }
        private void textbox_RegisterMobileNumber_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            try
            {
                Regex _regex = new Regex("[^0-9.-]+");
                e.Handled = !!_regex.IsMatch(e.Text);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 18 : " + x.ToString());
            }
        }
        private void textbox_RegisterMobileNumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Space)
                {
                    e.Handled = true;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 19 : " + x.ToString());
            }
        }
        //Login / Register Buttons
        private void button_LoginToRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tab_Register.Focus();
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 4 : \n" + x.ToString());
            }
        }
        private void button_RegisterToLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tab_Login.Focus();
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 5 : \n" + x.ToString());
            }
        }
        private void button_Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PublicMethods.LoginRules(textbox_LoginUsername.Text.Trim().ToLower(), passwordbox_LoginPassword.Password.Trim()))
                {
                    TapTransition(textbox_LoginUsername.Text.Trim().ToLower());
                }
                else
                {
                }
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 6 : \n" + x.ToString());
            }
        }
        private void button_Register_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //declaring variables
                PublicVariables.registerName = textbox_RegisterName.Text.Trim().ToLower();
                PublicVariables.registerSurname = textbox_RegisterSurname.Text.Trim().ToLower();
                PublicVariables.registerMobileNumber = textbox_RegisterMobileNumber.Text.Trim();
                PublicVariables.registerUsername = textbox_RegisterUsername.Text.Trim().ToLower();
                PublicVariables.registerPassword = passwordbox_RegisterPassword.Password;
                PublicVariables.registerRepeatPassword = passwordbox_RegisterRepeatPassword.Password;
                PublicVariables.registerDefaultRank = 3;
                PublicVariables.registerRequestedRank = (int)combobox_RegisterUserRank.SelectedValue;
                if (PublicMethods.RegisterationRules(PublicVariables.registerName, PublicVariables.registerSurname, PublicVariables.registerMobileNumber, PublicVariables.registerUsername, PublicVariables.registerPassword, PublicVariables.registerRepeatPassword, PublicVariables.registerDefaultRank, PublicVariables.registerRequestedRank))
                {
                    TapTransition(PublicVariables.registerUsername);
                }
                else
                {
                }
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 7 : \n" + x.ToString());
            }
        }
        private void button_SignOut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                label_SignedInNameAndSurname.Content = "Welcome";
                label_SignedInID.Content = "";
                tab_Login.IsEnabled = true;
                tab_Register.IsEnabled = true;
                tab_Login.Focus();
                tab_Student.IsEnabled = false;
                tab_Teacher.IsEnabled = false;
                tab_Admin.IsEnabled = false;
                button_SignOut.Visibility = Visibility.Hidden;
                tab_Admin.Visibility = Visibility.Hidden;
                tab_Student.Visibility = Visibility.Hidden;
                tab_Teacher.Visibility = Visibility.Hidden;
                textbox_RegisterMobileNumber.Text = "";
                textbox_RegisterName.Text = "";
                textbox_RegisterSurname.Text = "";
                textbox_RegisterUsername.Text = "";
                textbox_LoginUsername.Text = "";
                passwordbox_LoginPassword.Password = "";
                passwordbox_RegisterPassword.Password = "";
                passwordbox_RegisterRepeatPassword.Password = "";
                combobox_RegisterUserRank.SelectedIndex = 0;
                PublicVariables.currentlySignedInID = 0;
                PublicVariables.currentlySignedInUsername = "";
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 8 : \n" + x.ToString());
            }
        }
        //Student Buttons
        private void button_StudentBooks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Books searching_And_Displaying_Books = new Books();
                //this.Visibility = Visibility.Hidden;
                //this.Close();
                //searching_And_Displaying_Books.Show();
                searching_And_Displaying_Books.ShowDialog();
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 9 : " + x.ToString());
            }
        }
        private void button_StudentMyBooks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MyBooks MyBooks = new MyBooks();
                MyBooks.ShowDialog();
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 10 : " + x.ToString());
            }
        }
        //Teacher Buttons
        private void button_TeacherBooks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Books searching_And_Displaying_Books = new Books();
                searching_And_Displaying_Books.ShowDialog();
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 11 : " + x.ToString());
            }
        }
        private void button_TeacherMyBooks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MyBooks MyBooks = new MyBooks();
                MyBooks.ShowDialog();
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 12 : " + x.ToString());
            }
        }
        //Admin Buttons
        private void button_AdminBooks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Books searching_And_Displaying_Books = new Books();
                searching_And_Displaying_Books.ShowDialog();
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 13 : " + x.ToString());
            }
        }
        private void button_AdminMyBooks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MyBooks MyBooks = new MyBooks();
                MyBooks.ShowDialog();
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 14 : " + x.ToString());
            }
        }
        private void button_AdminRankApproval_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RankApprovingWindow RankApprovingWindowTransfere = new RankApprovingWindow();
                RankApprovingWindowTransfere.ShowDialog();
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 15 : " + x.ToString());
            }
        }
        private void button_AdminReturnedBooksList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ReturnedBooksApproval ReturnedBooksApprovalTransfere = new ReturnedBooksApproval();
                ReturnedBooksApprovalTransfere.ShowDialog();
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 16 : " + x.ToString());
            }
        }
        private void button_AdminAllUsers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AllUsers transfere = new AllUsers();
                transfere.ShowDialog();
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 17 : " + x.ToString());
            }
        }
        private void button_AdminBooksHistory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BooksHistory transfere = new BooksHistory();
                transfere.ShowDialog();
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 61 : " + x.ToString());
            }
        }
        private void button_AdminAllBooks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AllBooks transfer = new AllBooks();
                transfer.ShowDialog();
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 62 : " + x.ToString());
            }
        }
        private void button_TimePeriodEfit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TimePeriod transfer = new TimePeriod();
                transfer.ShowDialog();
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 82 : " + x.ToString());
            }
        }
    }
}
