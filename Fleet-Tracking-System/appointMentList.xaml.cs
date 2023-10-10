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
    /// Interaction logic for appointMentList.xaml
    /// </summary>
    public partial class appointMentList : Window
    {
        
        //Inheriting 
        appointmentListClass appointment = new appointmentListClass();
        userInfoClass userInfo = new userInfoClass();
        public appointMentList(string userName)
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
                    appointment.appointmentDate = apointmentDateDp.SelectedDate.ToString();
                    appointment.vehicleNumber = vehicleNumberTxt.Text.Trim();
                    appointment.serviceToBePeformed = severviceToBePerformedTxt.Text.Trim();
                    appointment.procedureCode = procedureCodeTxt.Text.Trim();
                    appointment.description  = new TextRange(descriptionTxt.Document.ContentStart, descriptionTxt.Document.ContentEnd).Text;
                    con.Open(); //Open Connection
                    SqlCommand cmd = new SqlCommand("insert into ServiceAppointmentsTbl values( '" + appointment.appointmentDate + "','" + appointment.vehicleNumber + "', '" + appointment.serviceToBePeformed + "','" + appointment.procedureCode + "','" + appointment.description + "')", con);
                    cmd.ExecuteNonQuery(); //execute command
                    con.Close();//close connection
                                //Display message to user
                    MessageBox.Show("Appointment Successfully Created", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
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
            //Inheriting 
            Dashboard dashboard = new Dashboard(userInfo.userName);
            //Redirect user
            dashboard.Show();
            //Hide Window
            this.Hide();
        }

        private void ViewBtn_Click(object sender, RoutedEventArgs e)
        {
            //Inheriting
            appointmentListTable listTable = new appointmentListTable(userInfo.userName);
            //Redirect User
            listTable.Show();
            //Hide window
            this.Hide();
        }
    }
}
