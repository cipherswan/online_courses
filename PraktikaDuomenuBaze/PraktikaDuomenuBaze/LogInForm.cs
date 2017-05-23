using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using MySql.Data.MySqlClient;

namespace PraktikaDuomenuBaze
{
    public partial class LogInForm : Form
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;


        public LogInForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login_account();

          //  Get_all_course_categorys();
            
            MainFormAdmin MainForm = new MainFormAdmin();
            MainForm.ShowDialog();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
            Thread t = new Thread(() =>Application.Run(new RegistrationForm()));
            t.Start();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


     public void Login_account()
   {
       try
       {
           
     

           server = "25.109.243.244";
           database = "praktikadb";
           uid = "root";
           password = "canada1";
           string connectionString;

           connectionString = "SERVER=" + server + ";" + "DATABASE=" +
           database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

           connection = new MySqlConnection(connectionString);

          // MessageBox.Show("Hello from login acc");

           

    
           
       }
       catch (Exception ex)
       {
           MessageBox.Show(ex.Message);
       }

   }

     private bool OpenConnection()
     {
         try
         {
             connection.Open();
             return true;
         }
         catch (MySqlException ex)
         {
             //When handling errors, you can your application's response based 
             //on the error number.
             //The two most common error numbers when connecting are as follows:
             //0: Cannot connect to server.
             //1045: Invalid user name and/or password.
             switch (ex.Number)
             {
                 case 0:
                     MessageBox.Show("Cannot connect to server.  Contact administrator");
                     break;

                 case 1045:
                     MessageBox.Show("Invalid username/password, please try again");
                     break;
             }
             return false;
         }
     }


     private bool CloseConnection()
     {
         try
         {
             connection.Close();
             return true;
         }
         catch (MySqlException ex)
         {
             MessageBox.Show(ex.Message);
             return false;
         }
     }

   
 

      public void Get_all_course_categorys()
     {
         string query = "SELECT * FROM coursecategory";
 

         if (this.OpenConnection() == true)
         {
             //Create Command
             MySqlCommand cmd = new MySqlCommand(query, connection);
             //Create a data reader and Execute the command
             MySqlDataReader dataReader = cmd.ExecuteReader();




             //Read the data and store them in the list
             while (dataReader.Read())
             {
                 MessageBox.Show(dataReader["idCategory"] + " " + dataReader["name"] + " ");
          
                
             }

             //close Data Reader
             dataReader.Close();

             //close Connection
             this.CloseConnection();

             //return list to be displayed
            
         }
         else
         {
             MessageBox.Show("Error");
         }
     }




    }


    
}
