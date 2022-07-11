using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Library_System
{
    /// <summary>
    /// Interaction logic for TimePeriod.xaml
    /// </summary>
    public partial class TimePeriod : Window
    {
        //TimePeriod Initialazation Start
        public TimePeriod()
        {
            InitializeComponent();
            datagrid_TimePeriod.ItemsSource = PublicMethods.ShowingTimePeriod(textbox_Search.Text).DefaultView;
        }
        //TimePeriod Initialazation End

        //Local Methods Start
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            try
            {
                return !_regex.IsMatch(text);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error ** : " + x.ToString());
                return false;
            }

        }
        private void textbox_days_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {

            try
            {
                e.Handled = !IsTextAllowed(e.Text);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 67 : " + x.ToString());
            }

        }
       
        //Local methods end

        //All Buttons Start
        private void button_AddTime_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                object item = datagrid_TimePeriod.SelectedItem;
                int id = int.Parse((datagrid_TimePeriod.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                PublicMethods.AddingTime(int.Parse(textbox_days.Text), id);
                datagrid_TimePeriod.ItemsSource = PublicMethods.ShowingTimePeriod(textbox_Search.Text).DefaultView;

            }
            catch (System.ArgumentOutOfRangeException)
            {
                MessageBox.Show("Please Select Book to change it's time limit");
                return;
            }
            catch (Exception x) 
            {
                MessageBox.Show("Error ** : " + x.ToString());
            }


        }

        private void button_ReturnToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                datagrid_TimePeriod.ItemsSource = PublicMethods.ShowingTimePeriod(textbox_Search.Text).DefaultView;
            }
            catch (Exception x)
            {
                MessageBox.Show("Error 89 : " + x.ToString());
            }
            
        }
        //All Buttons End
    }
}
