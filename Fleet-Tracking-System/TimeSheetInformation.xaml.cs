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
    /// Interaction logic for TimeSheetInformation.xaml
    /// </summary>
    public partial class TimeSheetInformation : Window
    {
        userInfoClass userInfo = new userInfoClass();
        public TimeSheetInformation(string userName)
        {
            InitializeComponent();
            userInfo.userName = userName;
            displayData(); //Calling method displayData
        }
        //Creating method to display data from database
        public void displayData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-NDSSI71A\SQLEXPRESS;Initial Catalog=Cargo;Integrated Security=True"))
                {
                    con.Open(); //Open Connection
                    SqlCommand cmd = new SqlCommand("Select * From VehicleTbl ", con); //Create Command
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd); //Create adapter
                    DataSet ds = new DataSet(); //create dataset
                    adapter.Fill(ds); //fill adapter with dataset
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        timeData.ItemsSource = ds.Tables[0].DefaultView;
                    }
                    con.Close(); //close connection
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
            timeSheetReport timeSheet = new timeSheetReport(userInfo.userName);
            //redirect user
            timeSheet.Show();
            //hide window
            this.Hide();
        }
    }
}
