using Fleet_Tracking_System.ParameterClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fleet_Tracking_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        userInfoClass userInfo = new userInfoClass();
       
        public MainWindow()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            //If statement to determine if user enterd username and password
            if (PasswordTxt.Password.Length == 0)
            {
                //Display message to user
                MessageBox.Show("Enter Username And Password", "Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                //Clear textFields
                usernameTxt.Clear();
                PasswordTxt.Clear();

            }
            else
            {
                //Create connection to database
                SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-NDSSI71A\SQLEXPRESS;Initial Catalog=Cargo;Integrated Security=True");
                //Inheriting 
                //Initialising Variables
                userInfo.userName = usernameTxt.Text.Trim();
                userInfo.passWord = PasswordTxt.Password.Trim();
                
               
               
                //Command to read/write to database
                SqlCommand cmd = new SqlCommand("Select * From UsersTbl where Username='" + userInfo.userName + "' ", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter(); //Create adapter
                adapter.SelectCommand = cmd; //link adapter to command
                DataSet dataSet = new DataSet(); //create dataset
                adapter.Fill(dataSet); //fill adapter with dataset
                //If statement to determine if values enterd exist in database
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    //Convert the stored  salt to a byte array
                    byte[] saltBytes = Convert.FromBase64String(dataSet.Tables[0].Rows[0]["SaltPassword"].ToString());

                    //Create the Rfc2898DeriveBytes  with the enterd password and stored salt
                    var pbkdf2 = new Rfc2898DeriveBytes(userInfo.passWord, saltBytes, 10000);

                    //Get the hash bytes for the enterd password
                    byte[] enteredHashBytes = pbkdf2.GetBytes(20);

                    //Convert the enterd hash bytes to a base64 string
                    string enterdHashedPassword = Convert.ToBase64String(enteredHashBytes);

                    if(enterdHashedPassword == dataSet.Tables[0].Rows[0]["Password"].ToString())
                    {
                        Dashboard dashboard = new Dashboard(userInfo.userName);
                        dashboard.Show();
                        this.Hide();
                    }
                    else
                    {
                        //Display message to user
                        MessageBox.Show("Wrong password enterd", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                   
                }
                else 
                {
                    //Display Message to user
                    MessageBox.Show("Invalid Information Enterd", "Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                con.Close(); //Close Connection 

            }
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
          
        }
    }
}
