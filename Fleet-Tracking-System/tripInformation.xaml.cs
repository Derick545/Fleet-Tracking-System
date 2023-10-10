using Fleet_Tracking_System.ParameterClass;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for tripInformation.xaml
    /// </summary>
    public partial class tripInformation : Window
    {
        //Inheriting
        tripInformationClass tripInfo = new tripInformationClass();
        userInfoClass userInfo = new userInfoClass();
        public tripInformation(string userName)
        {
            InitializeComponent();
            userInfo.userName = userName;
            //Calling methods
            displayData();
            displayCompleteData();
        }
        //Creating method to display data from database
        public void displayData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-NDSSI71A\SQLEXPRESS;Initial Catalog=Cargo;Integrated Security=True"))
                {
                    con.Open(); //Open Connection
                    string stat = "in-progress";
                    SqlCommand cmd = new SqlCommand("Select VehicleNumber,DriverName,DESTINATION,DISTANCETOTRAVEL,STATUS From TripTbl where STATUS = '" + stat+"'  ", con); //create command
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd); //create adapter
                    DataSet ds = new DataSet(); //create dataset
                    adapter.Fill(ds); //fill adapter using dataset
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        completeData.ItemsSource = ds.Tables[0].DefaultView;
                    }
                    con.Close(); //close connection
                }
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
           

        }
        public void displayCompleteData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-NDSSI71A\SQLEXPRESS;Initial Catalog=Cargo;Integrated Security=True"))
                {
                    con.Open(); //open Connection
                    string stat = "Completed";
                    SqlCommand cmd = new SqlCommand("Select VehicleNumber,DriverName,DESTINATION,DISTANCETOTRAVEL,STATUS From TripTbl where STATUS = '" + stat+"'  ", con); //create command
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd); //create adapter
                    DataSet ds = new DataSet(); //create dataset
                    adapter.Fill(ds); //fill adapter using dataset
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        requirementsData.ItemsSource = ds.Tables[0].DefaultView;
                    }
                    con.Close(); //close connection
                }
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void createBtn_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-NDSSI71A\SQLEXPRESS;Initial Catalog=Cargo;Integrated Security=True"))
                {
                    //Initialising variables
                    tripInfo.vehicleNumber = vehicleNumberTxt.Text.Trim();
                    string stat = "Completed";
                    tripInfo.distanceTraveled = Convert.ToInt32(distanceTxt.Text.Trim());
                    SqlCommand cmd = new SqlCommand("update TripTbl set DISTANCETRAVELED ='" + tripInfo.distanceTraveled + "',STATUS ='" + stat + "' where VehicleNumber = '" + tripInfo.vehicleNumber + "' ", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Vehicle information updated successfully");
                    displayCompleteData();
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
            tripReport trip = new tripReport(userInfo.userName);
            trip.Show(); // redirect user
            this.Hide(); //hide window
        }
    }
}
