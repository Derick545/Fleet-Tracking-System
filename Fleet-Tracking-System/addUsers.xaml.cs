using Fleet_Tracking_System.ParameterClass;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;


namespace Fleet_Tracking_System
{
    /// <summary>
    /// Interaction logic for addUsers.xaml
    /// </summary>
    public partial class addUsers : Window
    {
        
        userInfoClass userInfo = new userInfoClass();
        public addUsers(string userName)
        {
            InitializeComponent();
            userInfo.userName = userName;
        }
       
        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-NDSSI71A\SQLEXPRESS;Initial Catalog=Cargo;Integrated Security=True"))
                {
                    //Initialising variables
                    userInfo.userName = usernameTxt.Text.Trim();
                    userInfo.name = nameTxt.Text.Trim();
                    userInfo.surname = surnameTxt.Text.Trim();
                    userInfo.role = roleCmb.Text;
                    userInfo.passWord = passwordTxt.Text.Trim();
                    

                    if (userInfo.userName.Equals("") || userInfo.name.Equals("") || userInfo.surname.Equals("") || userInfo.role.Equals("") || userInfo.passWord.Equals(""))
                    {
                        //Display Message to user
                        MessageBox.Show("All Fields are required", "Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else
                    {
                        //Verifying if username is already taken
                        con.Open();//opening connection
                        using (SqlCommand com = new SqlCommand("Select COUNT(*) FROM UsersTbl Where Username = @username", con))
                        {
                            com.Parameters.AddWithValue("@username", userInfo.userName);
                            //Declaring variables
                            int count = Convert.ToInt32(com.ExecuteScalar());

                            if(count > 0)
                            {
                                //Display Message to user
                                MessageBox.Show("User is already taken!!!", "Information", MessageBoxButton.OK, MessageBoxImage.Hand);
                            }
                            else
                            {
                                byte[] salt = new byte[16];
                                new RNGCryptoServiceProvider().GetBytes(salt);
                                //Create the Rfc2898DeriveBytes with the password and salt
                                var pbkdf2 = new Rfc2898DeriveBytes(userInfo.passWord, salt, 10000);
                                //Get hash bytes
                                byte[] hash = pbkdf2.GetBytes(20);
                                userInfo.saltPassword = Convert.ToBase64String(salt);
                                userInfo.hashPassword = Convert.ToBase64String(hash);
                                SqlCommand cmd = new SqlCommand("insert into UsersTbl values( '" + userInfo.userName + "','" + userInfo.hashPassword + "','" + userInfo.saltPassword + "', '" + userInfo.role + "','" + userInfo.name + "','" + userInfo.surname + "')", con); //creating command with query
                                cmd.ExecuteNonQuery(); //Executing command
                                con.Close(); //Closing Connection
                                             //Display Message to user
                                MessageBox.Show("User Registerd Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                       
                    }
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
            Users user = new Users(userInfo.userName);
            //Redirecting user
            user.Show();
            //HIde current window
            this.Hide();
        }

        
    }
}
