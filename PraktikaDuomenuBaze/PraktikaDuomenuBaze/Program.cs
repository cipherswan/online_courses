using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace PraktikaDuomenuBaze
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LogInForm());
        }
    }




    class DatabaseAccess
    {
        public delegate void LogInResponse(string Un, string Pw, bool IsAdmin);
        public LogInResponse LinResponder;
        public delegate void RegistrationResponse();
        public RegistrationResponse RegResponder;
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public DatabaseAccess()
        {
            server = "25.109.243.244";
            database = "praktikadb";
            uid = "root";
            password = "canada1";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
        }

        private void OpenConnection()
        {
            try
            {
                connection.Open();
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
            }
        }

        private void CloseConnection()
        {
            try
            {
                connection.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LogIn(string Username, string Password)
        {
            try
            {
                string query = "Select * from account where Username = @Username and Password = @Password";
                MySqlCommand comm = new MySqlCommand(query, connection);
                comm.Parameters.AddWithValue("@Username", Username);
                comm.Parameters.AddWithValue("@Password", Password);
                OpenConnection();
                MySqlDataReader reader = comm.ExecuteReader();
                reader.Read();
                string un = reader["Username"].ToString();
                string pw = reader["Password"].ToString();
                bool isAdmin = Convert.ToBoolean(reader["IsAdmin"]);
                reader.Close();
                if(Username == un && Password == pw)
                    LinResponder(un, pw, isAdmin);
            }
            catch(Exception Ex)
            {
                if (Ex.Message.Contains("Invalid attempt to access a field before calling Read()"))
                    MessageBox.Show("Account doesn't exist or password invalid", "Login failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(Ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void RegisterSoloAccount(SoloAccount Person)
        {
            try
            {
                string query1 = "Insert into Account (Username, Password, IsAdmin, Person_idClient) values (@Username, @Password, @IsAdmin, @Id);";
                string query2 = "Insert into Person (Name, Surname, DateOfBirth, PhoneNumber, EmailAddress) values (@Name, @Surname, @Date, @Phone, @Email);";
                string query = "select idClient from Person where Name = @Name and Surname = @Surname";
                MySqlCommand comm = new MySqlCommand(query, connection);
                MySqlCommand command1 = new MySqlCommand(query1, connection);
                MySqlCommand command2 = new MySqlCommand(query2, connection);
                comm.Parameters.AddWithValue("@Name", Person.Name);
                comm.Parameters.AddWithValue("@Surname", Person.Surname);
                command2.Parameters.AddWithValue("@Name", Person.Name);
                command2.Parameters.AddWithValue("@Surname", Person.Surname);
                command2.Parameters.AddWithValue("@Date", Person.DateOfBirth.ToShortDateString());
                command2.Parameters.AddWithValue("@Phone", Person.PhoneNumber);
                command2.Parameters.AddWithValue("@Email", Person.EmailAddress);
                OpenConnection();
                command2.ExecuteNonQuery();
                MySqlDataReader reader = comm.ExecuteReader();
                reader.Read();
                int Id = Convert.ToInt32(reader["idClient"]);
                reader.Close();
                command1.Parameters.AddWithValue("@Id", Id);
                command1.Parameters.AddWithValue("@Username", Person.Username);
                command1.Parameters.AddWithValue("@Password", Person.Password);
                command1.Parameters.AddWithValue("@IsAdmin", 0);
                command1.ExecuteNonQuery();
                CloseConnection();
                MessageBox.Show("Registration successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RegResponder();
                Application.Run(new LogInForm());
            }
            catch(Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void RegisterCompanyAccount(CompanyAccount Company)
        {
            try
            {
                string query = "insert into Account (Username, Password, IsAdmin, Company_idClientCompany) values (@Name, @Password, @IsAdmin, @ID)";
                string query1 = "insert into Company (Name, CompanyID, PhoneNumer, EmailAddress, FAX, Address1, Address2) values (@Name, @ID, @Phone, @Email, @Fax, @Ad1, @Ad2)";
                string query2 = "select idClientCompany from Company where CompanyID = @ID";
                MySqlCommand comm = new MySqlCommand(query, connection);
                MySqlCommand comm1 = new MySqlCommand(query1, connection);
                MySqlCommand comm2 = new MySqlCommand(query2, connection);
                comm1.Parameters.AddWithValue("@Name", Company.CompanyName);
                comm1.Parameters.AddWithValue("@ID", Company.CompanyID);
                comm1.Parameters.AddWithValue("@Phone", Company.PhoneNumber);
                comm1.Parameters.AddWithValue("@Email", Company.EmailAddress);
                comm1.Parameters.AddWithValue("@Fax", Company.FAX);
                comm1.Parameters.AddWithValue("@Ad1", Company.Address1);
                comm1.Parameters.AddWithValue("@Ad2", Company.Address2);
                comm2.Parameters.AddWithValue("@ID", Company.CompanyID);
                OpenConnection();
                comm1.ExecuteNonQuery();
                MySqlDataReader reader = comm2.ExecuteReader();
                reader.Read();
                int Id = Convert.ToInt32(reader[0].ToString());
                reader.Close();
                comm.Parameters.AddWithValue("@Name", Company.Username);
                comm.Parameters.AddWithValue("@Password", Company.Password);
                comm.Parameters.AddWithValue("@IsAdmin", 0);
                comm.Parameters.AddWithValue("@ID", Id);
                comm.ExecuteNonQuery();
                CloseConnection();
                MessageBox.Show("Registration successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RegResponder();
                Application.Run(new LogInForm());
            }
            catch(Exception Ex)
            {
                //Duplicate entry
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
       
    }

    public class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class SoloAccount : Account
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }

    class CompanyAccount : Account
    {
        public string CompanyName { get; set; }
        public string CompanyID { get; set; }
        public string FAX { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}