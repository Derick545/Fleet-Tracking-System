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
    /// Interaction logic for serviceRequirementsJobSheet.xaml
    /// </summary>
    public partial class serviceRequirementsJobSheet : Window
    {
       
        //Inheriting 
        serviceRequirementsClass cservice = new serviceRequirementsClass();
        userInfoClass userInfo = new userInfoClass();
        public serviceRequirementsJobSheet(string userName)
        {
            InitializeComponent();
            //Calling methods
            userInfo.userName = userName;
            displayData();
            completeData();
        }
        //Creating Method to display data in DataGrid
        public void displayData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-NDSSI71A\SQLEXPRESS;Initial Catalog=Cargo;Integrated Security=True"))
                {
                    string stat = "in-progress";
                    con.Open(); //opening Connection
                    SqlCommand cmd = new SqlCommand("Select VehicleNumber,ServiceType,AppointmentDate,AppointmentTime,WorkToBeCompleted,Status From  ServiceRequirementsJobSheet where Status='"+stat+"' ", con); //Creating Command
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd); //create adapter
                    DataSet ds = new DataSet(); //usig dataset
                    adapter.Fill(ds); //Fill Table using dataset
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        requirementsData.ItemsSource = ds.Tables[0].DefaultView;
                    }
                    con.Close(); //closing connection
                }
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
           

        }
        public void completeData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-NDSSI71A\SQLEXPRESS;Initial Catalog=Cargo;Integrated Security=True"))
                {
                    string stat = "Completed";
                    con.Open(); //opening Connection
                    SqlCommand cmd = new SqlCommand("Select * From  ServiceRequirementsJobSheet where Status='"+stat+"'", con); //Creating Command
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd); //create adapter
                    DataSet ds = new DataSet(); //usig dataset
                    adapter.Fill(ds); //Fill Table using dataset
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        completedData.ItemsSource = ds.Tables[0].DefaultView;
                    }
                    con.Close(); //closing connection
                   
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
                    cservice.vehicleNumber = vehicleNumberTxt.Text.Trim();
                    string status = statusCmb.Text;
                    cservice.cost = Convert.ToDecimal(costTxt.Text.Trim());
                    cservice.completedDate = finalDate.SelectedDate.ToString();
                    con.Open(); //open Connection
                    SqlCommand cmd = new SqlCommand("update ServiceRequirementsJobSheet set Status = '" + status + "', AmountCost = '" + cservice.cost + "',DateCompleted = '" + cservice.completedDate + "' where VehicleNumber ='" + cservice.vehicleNumber + "' ", con); //Create Command
                    cmd.ExecuteNonQuery(); //Execute command
                    con.Close(); //close connection
                                 //Display Message to user
                    MessageBox.Show("User information updated successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    //Calling methods displayData and completeData
                    displayData();
                    completeData();
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
            serviceRequirements service = new serviceRequirements(userInfo.userName); //inheriting
            //Redirect user
            service.Show();
            //Hide Window
            this.Hide();
        }
    }
}
