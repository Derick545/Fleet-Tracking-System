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
using System.Windows.Shapes;

namespace Fleet_Tracking_System
{
    /// <summary>
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Users : Window
    {
      
        //Inheriting 
        userInfoClass userInfo = new userInfoClass();
       
       
        public Users(string userName)
        {
            InitializeComponent();
            //Calling Method Display Data
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
                   
                  
                    con.Open(); //opening connection
                    SqlCommand cmd = new SqlCommand("Select * From UsersTbl ", con); //Creaet command
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd); //Creat adapter
                    DataSet ds = new DataSet(); //Create Dataset
                    adapter.Fill(ds); //fill dataset using adapter
                                      //if statement to check if there is data in table database
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        userData.ItemsSource = ds.Tables[0].DefaultView;
                    }
                    con.Close(); //close connection 
                }
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error.
                MessageBox.Show("An error occurred: " + ex.Message);
            }

           
        }

        private void getBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-NDSSI71A\SQLEXPRESS;Initial Catalog=Cargo;Integrated Security=True"))
                {
                    //Initialising variables
                    string username = usernameTxt.Text.Trim();


                    //if statement to determine if username is not equals to null
                    if (username.Equals(""))
                    {
                        //Display message to user
                        MessageBox.Show("Username Is Required", "Username", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                    else
                    {
                        con.Open(); //open connection
                        SqlCommand cmd = new SqlCommand("select Username, Name, Surname, Role FROM UsersTbl where username = '" + username + "' ", con); //command to get information from database
                        SqlDataReader srd = cmd.ExecuteReader(); //execute reader to read information from database
                                                                 //while loop to determine if reader is reading
                        while (srd.Read())
                        {
                            //setting values to textboxs
                            nameTxt.Text = srd["Name"].ToString();
                            surnameTxt.Text = srd["Surname"].ToString();
                            roleTxt.Text = srd["Role"].ToString();

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

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-NDSSI71A\SQLEXPRESS;Initial Catalog=Cargo;Integrated Security=True"))
                {
                    //Initialising variables
                    userInfo.userName = usernameTxt.Text.Trim();
                    userInfo.name = nameTxt.Text.Trim();
                    userInfo.surname = surnameTxt.Text.Trim();
                    userInfo.role = roleTxt.Text.Trim();
                    userInfo.passWord = passwordTxt.Text.Trim();
                    userInfo.hashPassword = userInfo.passWord;
                    //If statement to check if all variables are enterd
                    if (userInfo.userName != null && userInfo.name != null && userInfo.surname != null && userInfo.role != null && userInfo.passWord != null)
                    {
                        byte[] salt = new byte[16];
                        new RNGCryptoServiceProvider().GetBytes(salt);
                        //Create the Rfc2898DeriveBytes with the password and salt
                        var pbkdf2 = new Rfc2898DeriveBytes(userInfo.passWord, salt, 10000);
                        //Get hash bytes
                        byte[] hash = pbkdf2.GetBytes(20);
                        userInfo.saltPassword = Convert.ToBase64String(salt);
                        userInfo.hashPassword = Convert.ToBase64String(hash);
                        con.Open(); //opening connection
                        SqlCommand cmd = new SqlCommand("update UsersTbl set Username ='" + userInfo.userName + "',Name = '" + userInfo.name + "', Surname = '" + userInfo.surname + "',Role = '" + userInfo.role + "',Password ='" + userInfo.hashPassword + "',SaltPassword ='" + userInfo.saltPassword + "' where Username ='" + userInfo.userName + "' ", con); //Creating command
                        cmd.ExecuteNonQuery(); //Excecuting command
                        con.Close(); //closing connection
                                     //Display Message to user
                        MessageBox.Show("User information updated successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        //Calling method display data
                        displayData();

                    }
                    else
                    {
                        //Display Message to user
                        MessageBox.Show("All Fields are required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error.
                MessageBox.Show("An error occurred: " + ex.Message);
            }


           
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-NDSSI71A\SQLEXPRESS;Initial Catalog=Cargo;Integrated Security=True"))
                {
                    //Initialising variables
                    string username = usernameTxt.Text.Trim();
                    //Exception Handling


                    //if statement to determine if user is sure
                    if (username != "")
                    {
                        //If Statement to get user input
                        if (MessageBox.Show("Are you sure you want to delete this user?", "Delete Record", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            con.Open();//opening connection
                            SqlCommand cmd = new SqlCommand("Delete UsersTbl where Username = '" + username + "'", con); //Command for sql database  
                            cmd.ExecuteNonQuery(); //Executing command
                            con.Close();//closing connection
                            MessageBox.Show($"Successfully Deleted User", "Successful", MessageBoxButton.OK, MessageBoxImage.Information);//Display Message to user
                            con.Close(); //close connection
                            displayData(); //update Table
                        }
                    }
                    else
                    {
                        //Display message to user
                        MessageBox.Show("Oops we all make mistakes", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error.
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            //Inheriting
            addUsers add = new addUsers(userInfo.userName);
            //Redirect User
            add.Show();
            //Hide Window
            this.Hide();
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            //Inheriting
            Dashboard dashboard = new Dashboard(userInfo.userName);
            //Redirect to user to dashboard wpf
            dashboard.Show();
            //hide this window
            this.Hide();
        }
    }
}
