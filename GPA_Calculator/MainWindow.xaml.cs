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

namespace GPA_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    // Grading Scale
    // 1- Low Fail, < 25%
    // 2- Fail, 25 - 39%
    // 3- Marginal Fail, 40 - 49%
    // 4- Pass, 50 - 64%
    // 5- Credit, 65 - 74%
    // 6- Distinction, 75 - 84%
    // 7- High Distinction, 85 - 100%

    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int[,] Grade_Ranges = new int[7, 2] { { 0, 24 }, { 25, 39 }, { 40, 49 }, 
                                              { 50, 64 }, { 65, 74 }, { 75, 84 }, { 85, 100 } };
        float Users_Current_Total = 0;
        const float RESET_VALUE = 0.0F;
        int Chosen_Grade;

        //-= The Addding Button =-//
        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            //Converts the users marks to a float "Normal Percent Tab"
            if (float.TryParse(this.Users_Marks.Text, out float Users_Mark_Percentage))
            {
                //Checks if the users inputted mark is less than 100
                if (Users_Mark_Percentage <= 100)
                {
                    float temp = Users_Current_Total;

                    //Checks if the current total will be realistic
                    if (temp + Users_Mark_Percentage <= 100 && temp + Users_Mark_Percentage >= 0)
                    {
                        Users_Current_Total += Users_Mark_Percentage;
                    }
                }

                for (int i = 0; i < Grade_Ranges.GetLength(0); i++)
                {
                    if (Users_Current_Total >= Grade_Ranges[i, 0] && Users_Current_Total <= Grade_Ranges[i, 1])
                    {
                        this.Current_Grade.Text = $"Current Grade: {i+1}";
                    }
                }

                this.Users_Marks.Text = "";
            }

            //Converts users marks to a float "Covert Percent Tab"
            if (float.TryParse(this.User_Con_Per.Text, out float Users_Convert_Percentage) &&
                float.TryParse(this.Weight_Con_Per.Text, out float Users_Convert_Weight_Percentage))
            {
                if (Users_Convert_Percentage <= 100 && Users_Convert_Weight_Percentage <= 100)
                {
                    float total = Users_Convert_Weight_Percentage * (Users_Convert_Percentage / 100);
                    float temp = Users_Current_Total;
                    
                    //Checks if the current total will be realistic
                    if (temp + total <= 100 && temp + total >= 0)
                    {
                        Users_Current_Total += total;
                    }
                }

                for (int i = 0; i < Grade_Ranges.GetLength(0); i++)
                {
                    if (Users_Current_Total >= Grade_Ranges[i, 0] && Users_Current_Total <= Grade_Ranges[i, 1])
                    {
                        this.Current_Grade.Text = $"Current Grade: {i + 1}";
                    }
                }

                this.User_Con_Per.Text = "";
                this.Weight_Con_Per.Text = "";
            }

            if (Users_Current_Total > 9)
            {
                Current_Total.Margin = new Thickness(-20, 0, 0, 0);
            }

            else
            {
                Current_Total.Margin = new Thickness(0, 0, 0, 0);
            }

            this.Current_Total.Text = Users_Current_Total.ToString();
        }

        //-= The Clearing Button =-//
        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            Users_Current_Total = RESET_VALUE;
            Current_Total.Margin = new Thickness(0, 0, 0, 0);
            Percent_Needed.Margin = new Thickness(0, 0, 0, 0);
            this.Current_Grade.Text = $"Current Grade: N/A"; 
            this.Current_Total.Text = RESET_VALUE.ToString();
            this.Percent_Needed.Text = RESET_VALUE.ToString();
        }

        //-= The Calculate Button =-//
        private void Calculate_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Grade_Combo.SelectedItem != null)
            {
                float Percent_Needed = Grade_Ranges[Chosen_Grade - 1, 0] - Users_Current_Total;

                if (Percent_Needed >= 0)
                {
                    this.Percent_Needed.Text = Percent_Needed.ToString();
                }

                if (Percent_Needed > 9)
                {
                    this.Percent_Needed.Margin = new Thickness(-20, 0, 0, 0);
                }

                else
                {
                    this.Percent_Needed.Margin = new Thickness(0, 0, 0, 0);
                }

            }
        }

        //-= Getting the item in the Combo Box (Grade) =-//
        private void Grade_Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            if (Grade_Combo.SelectedItem != null)
            {
                Chosen_Grade = int.Parse((string)((ComboBoxItem)((ComboBox)sender).SelectedValue).Content);
            }
        }
    }
}
