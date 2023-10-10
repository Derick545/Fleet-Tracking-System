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
    /// Interaction logic for tripReport.xaml
    /// </summary>
    public partial class tripReport : Window
    {
        //Inheriting
        tripInformationClass tripInfo = new tripInformationClass();
        userInfoClass userInfo = new userInfoClass();
        public tripReport(string userName)
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
                    tripInfo.vehicleNumber = vehicleNumberTxt.Text.Trim();
                    tripInfo.driverName = driverNameTxt.Text.Trim();
                    tripInfo.destination = destinationTxt.Text.Trim();
                    tripInfo.kilometers = Convert.ToInt32(kilometersTxt.Text.Trim());
                    //Declaring Variables
                    string stat = "in-progress";
                    int distanceTraveled = 0;
                    con.Open(); //Open Connection
                    SqlCommand cmd = new SqlCommand("insert into Triptbl values( '" + tripInfo.vehicleNumber + "','" + tripInfo.driverName + "', '" + tripInfo.destination + "','" + tripInfo.kilometers + "','" + distanceTraveled + "','" + stat + "')", con); //create command
                    cmd.ExecuteNonQuery(); //execute command
                    con.Close(); //Close Connection
                                 //Display message to user
                    MessageBox.Show("Trip Successfully Created", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
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
            Dashboard dashboard = new Dashboard(userInfo.userName);
            //Redirect User
            dashboard.Show();
            //Hide Window
            this.Hide();
        }

        private void tripInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            //Inheriting     
            tripInformation trip = new tripInformation(userInfo.userName);
            //Redirect user 
            trip.Show();
            //Hide Window
            this.Hide();
        }
    }
}
