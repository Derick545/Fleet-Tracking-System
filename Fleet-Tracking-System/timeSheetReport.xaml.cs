using Fleet_Tracking_System.ParameterClass;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Fleet_Tracking_System
{
    /// <summary>
    /// Interaction logic for timeSheetReport.xaml
    /// </summary>
    public partial class timeSheetReport : Window
    {    
        //Inheriting
        timeSheetClass timeSheet = new timeSheetClass();
        userInfoClass userInfo = new userInfoClass();
        public timeSheetReport(string userName)
        {
            InitializeComponent();
            userInfo.userName = userName;
        }
        private void createBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-NDSSI71A\SQLEXPRESS;Initial Catalog=Cargo;Integrated Security=True"))
                {
                    //Initialising variables
                    timeSheet.empName = empNameTxt.Text.Trim();
                    timeSheet.empNr = empNumTxt.Text.Trim();
                    timeSheet.hoursWorked = hoursWorkedTxt.Text.Trim();

                    con.Open(); //open connection
                    SqlCommand cmd = new SqlCommand("insert into TimeSheetTbl values( '" + timeSheet.empName + "','" + timeSheet.empNr + "','" + timeSheet.hoursWorked + "')", con); //create Command
                    cmd.ExecuteNonQuery(); //execute command
                    con.Close(); //close connection
                                 //Display message to user
                    MessageBox.Show("Information Saved Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
           
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            //Inheriting
            Dashboard dashboard = new Dashboard(userInfo.userName);
            //Redirect user
            dashboard.Show();
            //hide window
            this.Hide();
        }

        private void reportBtn_Click(object sender, RoutedEventArgs e)
        {
            //Inheriting
            TimeSheetInformation information = new TimeSheetInformation(userInfo.userName);
            //Redirect user
            information.Show();
            //hide window 
            this.Hide();
        }
    }
}
