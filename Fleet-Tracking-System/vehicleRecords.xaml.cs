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
    /// Interaction logic for vehicleRecords.xaml
    /// </summary>
    public partial class vehicleRecords : Window
    {
        
        //Inheriting
        vehicleRecordsClass vehicle = new vehicleRecordsClass();
        userInfoClass userInfo = new userInfoClass();
        public vehicleRecords(string userName)
        {
            InitializeComponent();
            //Calling Method displayData
            userInfo.userName = userName;
            displayData();
        }
        //Creating method to display data from database
        public void displayData()
        {
            try 
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-NDSSI71A\SQLEXPRESS;Initial Catalog=Cargo;Integrated Security=True"))
                {
                    con.Open(); //Opening Connection
                    SqlCommand cmd = new SqlCommand("Select * From VehiclesTbl ", con);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        vehicleData.ItemsSource = ds.Tables[0].DefaultView;
                    }
                    con.Close(); //Close Connection
                }
            }
            catch(Exception ex)
            {
                // Handle the exception or log the error.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
          
        }



        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-NDSSI71A\SQLEXPRESS;Initial Catalog=Cargo;Integrated Security=True"))
                {
                    //Initialising variables
                    vehicle.vehicleNumber = vehicleNumberTxt.Text.Trim();
                    vehicle.regNumber = registrationNumberTxt.Text.Trim();
                    vehicle.vehicleType = VehicleTypeTxt.Text.Trim();
                    vehicle.manufacture = manufactureTxt.Text.Trim();
                    vehicle.engineSize = EngineSizeTxt.Text.Trim();
                    vehicle.currentOdometer = odometerReadingTxt.Text.Trim();
                    vehicle.serviceOdometerReading = nextOdometerDp.SelectedDate.ToString();
                    //if statement 
                    if(vehicle.vehicleNumber.Equals("") && vehicle.regNumber.Equals("") && vehicle.vehicleType.Equals("") && vehicle.manufacture.Equals("") && vehicle.manufacture.Equals("") 
                        && vehicle.engineSize.Equals("") && vehicle.currentOdometer.Equals("") && vehicle.serviceOdometerReading.Equals(""))
                    {
                        MessageBox.Show("All Fields are required", "Stop", MessageBoxButton.OK, MessageBoxImage.Hand);
                    }
                    else 
                    {
                        con.Open(); //opening connection
                        SqlCommand cmd = new SqlCommand("insert into VehiclesTbl values( '" + vehicle.vehicleNumber + "','" + vehicle.regNumber + "', '" + vehicle.vehicleType + "','" + vehicle.manufacture + "','" + vehicle.engineSize + "','" + vehicle.currentOdometer + "','" + vehicle.serviceOdometerReading + "')", con); //creating command to insert values into database
                        cmd.ExecuteNonQuery(); //Executing command
                        con.Close(); //closing connection
                        MessageBox.Show("Vehicle Registration Successful", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        //Calling Method displayData
                        displayData();
                    }
                }
            }
            catch(Exception ex)
            {
                // Handle the exception or log the error.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
           
        }

        private void getBtn_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-NDSSI71A\SQLEXPRESS;Initial Catalog=Cargo;Integrated Security=True"))
                {
                    //Initialising variables
                    vehicle.vehicleNumber = vehicleNumberTxt.Text.Trim();

                    if(vehicle.vehicleNumber.Equals(""))
                    {
                        MessageBox.Show("Enter Veehicle Number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        con.Open(); //open connection
                        SqlCommand cmd = new SqlCommand("select RegistrationNumber, VehicleType, Manufacture, EngineSize, CurrentOdometerReading, NextOdometerReading  From VehiclesTbl where VehicleNumber = '" + vehicle.vehicleNumber + "'", con); //Create command
                        SqlDataReader srd = cmd.ExecuteReader(); //Executing command and Reader
                        if (srd.Read())
                        {
                            registrationNumberTxt.Text = srd["RegistrationNumber"].ToString();
                            VehicleTypeTxt.Text = srd["VehicleType"].ToString();
                            manufactureTxt.Text = srd["Manufacture"].ToString();
                            EngineSizeTxt.Text = srd["EngineSize"].ToString();
                            odometerReadingTxt.Text = srd["CurrentOdometerReading"].ToString();
                            nextOdometerDp.Text = srd["NextOdometerReading"].ToString();

                        }
                        else
                        {
                            //Display Message to User
                            MessageBox.Show("Vehicle Does not Exist", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        con.Close();
                    }

                   
                }
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
           
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-NDSSI71A\SQLEXPRESS;Initial Catalog=Cargo;Integrated Security=True"))
                {
                    //Initialising variables
                    vehicle.vehicleNumber = vehicleNumberTxt.Text.Trim();
                    vehicle.regNumber = registrationNumberTxt.Text.Trim();
                    vehicle.vehicleType = VehicleTypeTxt.Text.Trim();
                    vehicle.manufacture = manufactureTxt.Text.Trim();
                    vehicle.engineSize = EngineSizeTxt.Text.Trim();
                    vehicle.currentOdometer = odometerReadingTxt.Text.Trim();
                    vehicle.serviceOdometerReading = nextOdometerDp.SelectedDate.ToString();
                    con.Open(); //open Connection
                    SqlCommand cmd = new SqlCommand("update VehiclesTbl set VehicleNumber ='" + vehicle.vehicleNumber + "', RegistrationNumber = '" + vehicle.regNumber + "', VehicleType = '" + vehicle.vehicleType + "',Manufacture = '" + vehicle.manufacture + "',EngineSize ='" + vehicle.engineSize + "',CurrentOdometerReading ='" + vehicle.currentOdometer + "',NextOdometerReading ='" + vehicle.serviceOdometerReading + "'where VehicleNumber = '" + vehicle.vehicleNumber + "' ", con); //Create command
                    cmd.ExecuteNonQuery(); //Execute command
                    con.Close(); //close connection
                                 //Display message to user
                    MessageBox.Show("Vehicle information updated successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    displayData();
                }
               
            }
            catch(Exception ex)
            {
                // Handle the exception or log the error.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-NDSSI71A\SQLEXPRESS;Initial Catalog=Cargo;Integrated Security=True"))
                {
                    //Initialising variables
                    vehicle.vehicleNumber = vehicleNumberTxt.Text.Trim();
                    //if statement to determine if user is sure
                    if (vehicle.vehicleNumber != "")
                    {
                        if (MessageBox.Show("Are you sure you want to delete this Vehicle?", "Delete Record", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            con.Open();//opening connection
                            SqlCommand cmd = new SqlCommand("Delete VehiclesTbl where VehicleNumber = '" + vehicle.vehicleNumber + "'", con); //Command for sql database  
                            cmd.ExecuteNonQuery(); //Execute command
                            con.Close(); //close connection
                                         //display message to user
                            MessageBox.Show("Successfully Deleted Vehicle", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            con.Close();//close connection
                            displayData();
                        }
                    }
                }
               
            }
            catch(Exception ex)
            {
                // Handle the exception or log the error.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
           
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            //inheriting
            Dashboard dashboard = new Dashboard(userInfo.userName);
            //Redirect User
            dashboard.Show();
            //hide window
            this.Hide();
        }
    }
}
