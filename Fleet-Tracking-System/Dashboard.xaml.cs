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
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        //Creating connection
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-NDSSI71A\SQLEXPRESS;Initial Catalog=Cargo;Integrated Security=True");
        //Inheriting 
        userInfoClass userInfo = new userInfoClass();
       
      
      
         

        public Dashboard(string userName)
        {
            InitializeComponent();
            //Declaring Variables
           
            userLbl.Content = userName;
            userInfo.userName = userName;
            countUsers();
            countVehicles();
            calculateTotalAmount();
            countServiceReports();
            getUserInfo();
        }

      

        private void vehichleRecordsBtn_Click(object sender, RoutedEventArgs e)
        {
           
            this.Hide();
        }

        private void usersBtn1_Click(object sender, RoutedEventArgs e)
        {
            
        }
        //Method to count total users
        public void countUsers()
        {

            con.Open();//Opening Connection
            //sqlCommand to count all users
            string query = "Select COUNT(*) From UsersTbl";
            SqlCommand command = new SqlCommand(query, con);

            //Executing query
            int userCount = (int)command.ExecuteScalar();

            userNumberLbl.Content = userCount;
            con.Close();
        }
        public void calculateTotalAmount()
        {

            con.Open();//Opening Connection
            //sqlCommand to count all users
            string query = "Select SUM(AmountCost) From ServiceRequirementsJobSheet";
            SqlCommand command = new SqlCommand(query, con);

            //Executing query
            decimal totalAmount = (decimal)command.ExecuteScalar();

            amountLbl.Content = totalAmount;
            con.Close();
        }
        //Method to calculate Vehicle 
        public void countVehicles()
        {
            con.Open();
            //sqlCommand to count all users
            string query = "Select COUNT(*) From VehicleTbl";
            SqlCommand command = new SqlCommand(query, con);

            //Executing query
            int vehicleCount = (int)command.ExecuteScalar();
            vehiclesLbl.Content = vehicleCount;
            con.Close();
        }
        //Method to count all service reports
        public void countServiceReports()
        {

            con.Open();//Opening Connection
            //sqlCommand to count all users
            string query = "Select COUNT(*) From SeviceReportTbl";
            SqlCommand command = new SqlCommand(query, con);

            //Executing query
            int reportCount = (int)command.ExecuteScalar();

            serviceReportLbl.Content = reportCount;
            con.Close();
        }
        //Method to get user information from database
        public void getUserInfo()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-NDSSI71A\SQLEXPRESS;Initial Catalog=Cargo;Integrated Security=True"))
                {
                    con.Open();//Open connection
                    using(SqlCommand cmd = new SqlCommand("Select * From  UsersTbl WHERE Username = '"+userInfo.userName+"' ", con))
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            userInfo.role = reader["Role"].ToString();
                            roleLbl.Content = userInfo.role;

                            //If Statement 
                            if(userInfo.role == "Admin")
                            {
                                dashNameLbl.Content = "Admin";
                                timeLbl.Visibility = Visibility.Hidden;
                                timesheetBtn.Visibility = Visibility.Hidden;
                                clockImg.Visibility = Visibility.Hidden;
                            }
                            else if(userInfo.role == "Time Manager")
                            {
                                dashNameLbl.Content = "Timesheet Manager";
                                usersBtn1.Visibility = Visibility.Hidden;
                                userLbl1.Visibility = Visibility.Hidden;
                                userImg.Visibility = Visibility.Hidden;
                                userNumberLbl.Visibility = Visibility.Hidden;

                            }
                            else
                            {
                                dashNameLbl.Content = "User";
                                usersBtn1.Visibility = Visibility.Hidden;
                                userLbl1.Visibility = Visibility.Hidden;
                                userImg.Visibility = Visibility.Hidden;
                                userNumberLbl.Visibility = Visibility.Hidden;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error Occured: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void usersBtn1_Click_1(object sender, RoutedEventArgs e)
        {
            Users users = new Users(userInfo.userName);
            //Redirecting user to user.wpf
            users.Show();
            //Hiding this window
            this.Hide();  
        }

        private void vehicleRecordsBtn_Click(object sender, RoutedEventArgs e)
        {
            vehicleRecords vehicle = new vehicleRecords(userInfo.userName);
            //Redirect User
            vehicle.Show();
            //Hide Window
            this.Hide();
        }

        private void repairsBtn_Click(object sender, RoutedEventArgs e)
        {
            //Inheriting
            serviceRequirements service = new serviceRequirements(userInfo.userName);
            //Redirect user
            service.Show();
            //hide window
            this.Hide();
        }

        private void serviceReportBtn_Click(object sender, RoutedEventArgs e)
        {
            //Inheriting
            serviceReport report = new serviceReport(userInfo.userName);
            //Redirect user
            report.Show();
            //hide window
            this.Hide();
        }

        private void tripInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            //Inheriting
            tripReport trip = new tripReport(userInfo.userName);
            //Redirect User
            trip.Show();
            //Hide Window
            this.Hide();
        }

        private void appointmentBtn_Click(object sender, RoutedEventArgs e)
        {
            //Inheriting
            appointMentList list = new appointMentList(userInfo.userName);
            //Redirect User
            list.Show();
            //Hide Window
            this.Hide();
        }

        private void timesheetBtn_Click(object sender, RoutedEventArgs e)
        {
            //Inheriting
            timeSheetReport time = new timeSheetReport(userInfo.userName);
            //Redirect User
            time.Show();
            //Hide Window
            this.Hide();
        }

      
    }
}
