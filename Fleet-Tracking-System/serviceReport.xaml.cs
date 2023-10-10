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
    /// Interaction logic for serviceReport.xaml
    /// </summary>
    public partial class serviceReport : Window
    {
        userInfoClass userInfo = new userInfoClass();

        public serviceReport(string userName)
        {
            InitializeComponent();
            userInfo.userName = userName;
            displayData();
        }

        //Creating Method to display data in DataGrid
        public void displayData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-NDSSI71A\SQLEXPRESS;Initial Catalog=Cargo;Integrated Security=True"))
                {
                    con.Open(); //opening Connection
                    SqlCommand cmd = new SqlCommand("Select * From  SeviceReportTbl ", con); //Creating Command
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd); //create adapter
                    DataSet ds = new DataSet(); //usig dataset
                    adapter.Fill(ds); //Fill Table using dataset
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        reportData.ItemsSource = ds.Tables[0].DefaultView;
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

        private void createBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-NDSSI71A\SQLEXPRESS;Initial Catalog=Cargo;Integrated Security=True"))
                {
                    //Initialising variables
                    string vehicleNumber = vehicleNumberTxt.Text.Trim();
                    //Setting richText
                  
                    string serviceInformation = new TextRange(serviceInformationTxt.Document.ContentStart, serviceInformationTxt.Document.ContentEnd).Text;
                    con.Open(); //open connection
                    SqlCommand cmd = new SqlCommand("insert into SeviceReportTbl values( '" + vehicleNumber + "','" + serviceInformation + "')", con); //create Command
                    cmd.ExecuteNonQuery(); //execute command
                    con.Close(); //close connection
                                 //Display message to user
                    MessageBox.Show("Information Saved Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    //Calling method display data
                    displayData();
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
            //Inhetiting from other classes
            Dashboard dashboard = new Dashboard(userInfo.userName);
            //Redirect User
            dashboard.Show();
            //Hide window
            this.Hide();
        }
    }
}
