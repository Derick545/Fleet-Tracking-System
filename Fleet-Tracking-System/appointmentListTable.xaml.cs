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
    /// Interaction logic for appointmentListTable.xaml
    /// </summary>
    public partial class appointmentListTable : Window
    {
        //Inheriting
        userInfoClass userInfo = new userInfoClass();
        public appointmentListTable(string userName)
        {
            InitializeComponent();
            userInfo.userName = userName;
            displayData(); //calling method displayData
        }
        public void displayData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-NDSSI71A\SQLEXPRESS;Initial Catalog=Cargo;Integrated Security=True"))
                {
                    con.Open(); //opening Connection
                    SqlCommand cmd = new SqlCommand("Select * From ServiceAppointmentsTbl ", con); //Creating Command
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd); //create adapter
                    DataSet ds = new DataSet(); //usig dataset
                    adapter.Fill(ds); //Fill Table using dataset
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        appointmentData.ItemsSource = ds.Tables[0].DefaultView;
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

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            //Inheriting 
            appointMentList appointment = new appointMentList(userInfo.userName);
            //Redirect user
            appointment.Show();
            //Hide window
            this.Hide();
        }
    }
}
