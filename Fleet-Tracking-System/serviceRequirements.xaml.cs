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
    /// Interaction logic for serviceRequirements.xaml
    /// </summary>
    public partial class serviceRequirements : Window
    {
       
        //Inhetiting 
        serviceRequirementsClass service = new serviceRequirementsClass();
        userInfoClass userInfo = new userInfoClass();

        public serviceRequirements(string userName)
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
                    service.vehicleNumber = vehicleNumberTxt.Text.Trim();
                    service.serviceType = serviceTypeTxt.Text.Trim();
                    service.appointmentDate = appointmentDataDp.SelectedDate.ToString();
                    service.appointmentTime = appointmentTimeTxt.Text.Trim();
                    service.workToBeCompleted = wtbcTxt.Text.Trim();
                    //Declaring variables
                    decimal amount = 0;
                    string date = null;
                    string status = "in-progress";
                    con.Open(); //open connection
                    SqlCommand cmd = new SqlCommand("insert into ServiceRequirementsJobSheet values( '" + service.vehicleNumber + "','" + service.serviceType + "', '" + service.appointmentDate + "','" + service.appointmentTime + "','" + service.workToBeCompleted + "','" + status + "','" + amount + "','" + date + "')", con); //create command
                    cmd.ExecuteNonQuery(); //execute command
                    con.Close(); //close connection
                                 //Display message to user
                    MessageBox.Show("Service Successfully Created", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
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
            Dashboard dashboard = new Dashboard(userInfo.userName); //Inheriting
            dashboard.Show(); //Redirect user
            this.Hide(); //Hide Window
        }

        private void jobSheetBtn_Click(object sender, RoutedEventArgs e)
        {
            //Inheriting
            serviceRequirementsJobSheet sheet = new serviceRequirementsJobSheet(userInfo.userName);
            //Redirect user
            sheet.Show();
            //Hide Window
            this.Hide();
        }

       

    }
}
