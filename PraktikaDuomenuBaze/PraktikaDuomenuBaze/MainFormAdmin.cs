using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PraktikaDuomenuBaze
{
    public partial class MainFormAdmin : Form
    {

        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;



        public MainFormAdmin()
        {
            InitializeComponent();
        }

        private void MainFormAdmin_Load(object sender, EventArgs e)
        {
            Connect_to_database();
            Load_datatables();

        }

        public void Load_datatables()
        {
            Get_all_course_categorys();
            Get_all_courses();
            Get_all_Ratings();
            Get_all_lessons();
            Get_all_teachers();
            Get_all_people();
            Get_all_providers();
            Get_all_company();
            Get_all_accounts();
            Get_all_orders();
        }




        public void Connect_to_database()
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

             //   MessageBox.Show("Hello from login acc");





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



               listView3.Items.Clear();
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                  //  MessageBox.Show(dataReader["idCategory"] + " " + dataReader["name"] + " ");

                   

                
                        ListViewItem listitem = new ListViewItem(dataReader["idCategory"].ToString());
                        listitem.SubItems.Add(dataReader["name"].ToString());

                        listView3.Items.Add(listitem);
                


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

        public void Get_all_courses()
        {
            //string query = "SELECT * FROM course";

              string query = "SELECT course.idCourse , course.Name , course.Duration , course.Cost , coursecategory.Name AS courseName" +
                  " FROM coursecategory " +
                  " INNER JOIN course ON  course.Category_idCategory= coursecategory.idCategory " +
              " ORDER BY course.idCourse";
             

            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();



                listView5.Items.Clear();
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    //  MessageBox.Show(dataReader["idCategory"] + " " + dataReader["name"] + " ");




                    ListViewItem listitem = new ListViewItem(dataReader["idCourse"].ToString());
                    listitem.SubItems.Add(dataReader["Name"].ToString());
                    listitem.SubItems.Add(dataReader["Duration"].ToString());
                    listitem.SubItems.Add(dataReader["Cost"].ToString() + " Eur");
                    listitem.SubItems.Add(dataReader["courseName"].ToString());
                 

                    listView5.Items.Add(listitem);



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

        public void Get_all_Ratings()
        {
           // string query = "SELECT * FROM rating";

            string query = "SELECT rating.idRating , rating.Rating  , rating.Comment ,  course.Name , account.Username" +
                " FROM ((rating" +
                " INNER JOIN course ON  rating.Course_idCourse = course.idCourse) " +
                " INNER JOIN account ON   rating.Account_idAccount =  account.idAccount)" +
            " ";


            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();



                listView1.Items.Clear();
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    //  MessageBox.Show(dataReader["idCategory"] + " " + dataReader["name"] + " ");




                    ListViewItem listitem = new ListViewItem(dataReader["idRating"].ToString());
                    listitem.SubItems.Add(dataReader["Rating"].ToString());
                    listitem.SubItems.Add(dataReader["Comment"].ToString());
                    listitem.SubItems.Add(dataReader["Name"].ToString());
                    listitem.SubItems.Add(dataReader["Username"].ToString());


                    listView1.Items.Add(listitem);



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

     
        public void Get_all_lessons()
        {
           // string query = "SELECT * FROM lesson";

            string query = "SELECT lesson.idLesson , lesson.Name , course.Name AS courseName" +
                 " FROM course " +
                 " INNER JOIN lesson ON  lesson.Course_idCourse = course.idCourse " +
             " ORDER BY lesson.idLesson";

            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();



                listView4.Items.Clear();
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    //  MessageBox.Show(dataReader["idCategory"] + " " + dataReader["name"] + " ");




                    ListViewItem listitem = new ListViewItem(dataReader["idLesson"].ToString());
                    listitem.SubItems.Add(dataReader["Name"].ToString());
                    listitem.SubItems.Add(dataReader["courseName"].ToString());


                    listView4.Items.Add(listitem);



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

        public void Get_all_teachers()
        {
            string query = "SELECT * FROM teacher";


            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();



                listView6.Items.Clear();
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    //  MessageBox.Show(dataReader["idCategory"] + " " + dataReader["name"] + " ");




                    ListViewItem listitem = new ListViewItem(dataReader["idTeacher"].ToString());
                    listitem.SubItems.Add(dataReader["Name"].ToString());
                    listitem.SubItems.Add(dataReader["Surname"].ToString());
                    listitem.SubItems.Add(dataReader["PhoneNumber"].ToString());
                    listitem.SubItems.Add(dataReader["EmailAddress"].ToString());
                    listitem.SubItems.Add(dataReader["Fax"].ToString());
              

                    listView6.Items.Add(listitem);



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


        public void Get_all_people()
        {
            string query = "SELECT * FROM person";


            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();



                listView7.Items.Clear();
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    //  MessageBox.Show(dataReader["idCategory"] + " " + dataReader["name"] + " ");




                    ListViewItem listitem = new ListViewItem(dataReader["idClient"].ToString());
                    listitem.SubItems.Add(dataReader["Name"].ToString());
                    listitem.SubItems.Add(dataReader["Surname"].ToString());
                    listitem.SubItems.Add(dataReader["DateOfBirth"].ToString());
                    listitem.SubItems.Add(dataReader["PhoneNumber"].ToString());
                    listitem.SubItems.Add(dataReader["EmailAddress"].ToString());
                    listitem.SubItems.Add(dataReader["Group_idGroup"].ToString());
                    listitem.SubItems.Add(dataReader["ClientCompany_idClientCompany"].ToString());
    

                    listView7.Items.Add(listitem);



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


        public void Get_all_providers()
        {
            string query = "SELECT * FROM provider";


            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();



                listView8.Items.Clear();
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    //  MessageBox.Show(dataReader["idCategory"] + " " + dataReader["name"] + " ");




                    ListViewItem listitem = new ListViewItem(dataReader["idProvider"].ToString());
                    listitem.SubItems.Add(dataReader["Name"].ToString());
                    listitem.SubItems.Add(dataReader["Surname"].ToString());
                    listitem.SubItems.Add(dataReader["phone_number"].ToString());
                    listitem.SubItems.Add(dataReader["email"].ToString());
                    listitem.SubItems.Add(dataReader["DateOfBirth"].ToString());


                    listView8.Items.Add(listitem);



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


        public void Get_all_company()
        {
            string query = "SELECT * FROM company";


            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();



                listView9.Items.Clear();
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    //  MessageBox.Show(dataReader["idCategory"] + " " + dataReader["name"] + " ");




                    ListViewItem listitem = new ListViewItem(dataReader["idClientCompany"].ToString());
                    listitem.SubItems.Add(dataReader["Name"].ToString());
                    listitem.SubItems.Add(dataReader["CompanyID"].ToString());
                    listitem.SubItems.Add(dataReader["PhoneNumer"].ToString());
                    listitem.SubItems.Add(dataReader["EmailAddress"].ToString());
                    listitem.SubItems.Add(dataReader["Fax"].ToString());
                    listitem.SubItems.Add(dataReader["Address1"].ToString());
                    listitem.SubItems.Add(dataReader["Address2"].ToString());

                    listView9.Items.Add(listitem);



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

        public void Get_all_accounts()
        {
            string query = "SELECT * FROM account";


            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();



                listView10.Items.Clear();
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    //  MessageBox.Show(dataReader["idCategory"] + " " + dataReader["name"] + " ");




                    ListViewItem listitem = new ListViewItem(dataReader["idAccount"].ToString());
                    listitem.SubItems.Add(dataReader["Username"].ToString());
                    listitem.SubItems.Add(dataReader["Password"].ToString());

                    string adm;

                    if (dataReader["IsAdmin"].ToString() == Convert.ToString(1))
                    {
                        adm = "true";
                    }

                    else adm = "false";
                    
                    listitem.SubItems.Add(adm);
                    listView10.Items.Add(listitem);



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

        public void Get_all_orders()
        {
            
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)// IGNORE
        {

        }

        private void listView7_SelectedIndexChanged(object sender, EventArgs e) // IGNORE x2
        {

        }

        private void listView10_SelectedIndexChanged(object sender, EventArgs e) // IGNORE x3
        {

        }

        private void listView9_SelectedIndexChanged(object sender, EventArgs e) // man do i suck at this
        {

        }

        private void listView4_SelectedIndexChanged(object sender, EventArgs e)// IGNORE x n
        {

        }

        private void Addacc_Click(object sender, EventArgs e) // Add_account
        {
            try
            {
                string query = "INSERT INTO account (Username,Password,IsAdmin) VALUES (@Username , @Password, @IsAdmin) ";




                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);



                    cmd.Parameters.AddWithValue("@Username", textBox1.Text); // pridejimas
                    cmd.Parameters.AddWithValue("@Password", textBox2.Text); // pridejimas
                    // cmd.Parameters.AddWithValue("@IsAdmin", textBox12.Text);

                    if (comboBox1.SelectedIndex == 0)
                    {
                        cmd.Parameters.AddWithValue("@IsAdmin", '0'); // not admin
                    }

                    if (comboBox1.SelectedIndex == 1)
                    {
                        cmd.Parameters.AddWithValue("@IsAdmin", '1'); // is admin
                    }

                    cmd.ExecuteNonQuery();

                    Get_all_accounts();

                    //close Data Reader


                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed

                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
            catch
            {
                MessageBox.Show("Irašymas nepavyko, ivyko klaida");
            }
        }

        private void EditAcc_Click(object sender, EventArgs e) // update acc
        {
            try
            {
            //string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'";
            string query = "UPDATE account SET Username=@Username ,Password=@Password , IsAdmin=@IsAdmin WHERE  idAccount=@ID";
            



            //Open connection
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);



                cmd.Parameters.AddWithValue("@Username", textBox1.Text); // pridejimas
                cmd.Parameters.AddWithValue("@Password", textBox2.Text); // pridejimas
                // cmd.Parameters.AddWithValue("@IsAdmin", textBox12.Text);

                if (comboBox1.SelectedIndex == 0)
                {
                    cmd.Parameters.AddWithValue("@IsAdmin", '0'); // not admin
                }

                if (comboBox1.SelectedIndex == 1)
                {
                    cmd.Parameters.AddWithValue("@IsAdmin", '1'); // is admin
                }
                cmd.Parameters.AddWithValue("@ID", listView10.SelectedItems[0].Text);

                cmd.ExecuteNonQuery();
              //  cmd.ExecuteScalar();

                Get_all_accounts();
                //close connection
                this.CloseConnection();
            }

            }
            catch{
                  MessageBox.Show("Atnaujinimas nepavyko, ivyko klaida");
            }
        }

        private void DeleteAcc_Click(object sender, EventArgs e) // delete acc -----------[acc->person->orders]---------------needs improvememnt to delete person and his orders
        {
           // string query = "DELETE FROM tableinfo WHERE name='John Smith'";
            try
            {


                string query = "DELETE FROM account WHERE  idAccount=@ID";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);


                    cmd.Parameters.AddWithValue("@ID", listView10.SelectedItems[0].Text);

                    cmd.ExecuteNonQuery();

                    Get_all_accounts();

                    this.CloseConnection();
                }
            }
            catch
            {
                MessageBox.Show("Ištrinimas nepavyko, ivyko klaida");
            }
        }

        private void button21_Click(object sender, EventArgs e) // Add_course
        {
            try
            {



                string query = "INSERT INTO course (Name,Duration,Cost,Category_idCategory) VALUES (@Name , @Duration, @Cost,@Category_idCategory) ";




                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);



                    cmd.Parameters.AddWithValue("@Name", textBox36.Text); // pridejimas
                    cmd.Parameters.AddWithValue("@Duration", textBox35.Text); 
                  
                        cmd.Parameters.AddWithValue("@Cost", textBox38.Text); 
                        cmd.Parameters.AddWithValue("@Category_idCategory", textBox37.Text); 
                    

                    cmd.ExecuteNonQuery();

                    Get_all_courses();

                    //close Data Reader


                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed

                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
            catch
            {
                MessageBox.Show("Irašymas nepavyko, ivyko klaida");
            }
        }

        private void button20_Click(object sender, EventArgs e) // Update_course
        {
            try
            {

                string query = "UPDATE course SET Name=@Name ,Duration=@Duration , Cost=@Cost, Category_idCategory=@Category_idCategory  WHERE  idCourse=@ID";

             

                //Open connection
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);



                    cmd.Parameters.AddWithValue("@Name", textBox36.Text); 
                    cmd.Parameters.AddWithValue("@Duration", textBox35.Text); 
                        cmd.Parameters.AddWithValue("@Cost", textBox38.Text); 
                        cmd.Parameters.AddWithValue("@Category_idCategory", textBox37.Text);

                        cmd.Parameters.AddWithValue("@ID", listView5.SelectedItems[0].Text);
                    
                  
                    cmd.ExecuteNonQuery();
                    //  cmd.ExecuteScalar();

                    Get_all_courses();
                    //close connection
                    this.CloseConnection();
                }

            }
            catch
            {
                MessageBox.Show("Atnaujinimas nepavyko, ivyko klaida");
            }
        }

        private void button22_Click(object sender, EventArgs e) // Delete Rating
        {
        
            try
            {


                string query = "DELETE FROM rating WHERE  idRating=@ID";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);


                    cmd.Parameters.AddWithValue("@ID", listView1.SelectedItems[0].Text);

                    cmd.ExecuteNonQuery();

                    Get_all_Ratings();

                    this.CloseConnection();
                }
            }
            catch
            {
                MessageBox.Show("Ištrinimas nepavyko, ivyko klaida");
            }
        }

        private void button19_Click(object sender, EventArgs e) // Delete Course
        {
            try
            {


                string query = "DELETE FROM course WHERE  idCourse=@ID";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);


                    cmd.Parameters.AddWithValue("@ID", listView5.SelectedItems[0].Text);

                    cmd.ExecuteNonQuery();

                    Get_all_courses();

                    this.CloseConnection();
                }
            }
            catch
            {
                MessageBox.Show("Ištrinimas nepavyko, ivyko klaida");
            }
        }

        private void button18_Click(object sender, EventArgs e) // Add course category
        {
            try
            {
                string query = "INSERT INTO coursecategory (Name) VALUES (@Name) ";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    cmd.Parameters.AddWithValue("@Name", textBox31.Text); 
                  


                    cmd.ExecuteNonQuery();

                    Get_all_course_categorys();

                    //close Data Reader


                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed

                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
            catch
            {
                MessageBox.Show("Irašymas nepavyko, ivyko klaida");
            }
        }

        private void button17_Click(object sender, EventArgs e) // UPDATE course category
        {
            try
            {

                string query = "UPDATE coursecategory SET Name=@Name  WHERE  idCategory=@ID";
                //Open connection
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    cmd.Parameters.AddWithValue("@Name", textBox31.Text);
              
                    cmd.Parameters.AddWithValue("@ID", listView3.SelectedItems[0].Text);


                    cmd.ExecuteNonQuery();
                    //  cmd.ExecuteScalar();

                    Get_all_course_categorys();
                    //close connection
                    this.CloseConnection();
                }

            }
            catch
            {
                MessageBox.Show("Atnaujinimas nepavyko, ivyko klaida");
            }
        }

        private void button16_Click(object sender, EventArgs e) // Delete course category
        {
            try
            {


                string query = "DELETE FROM coursecategory WHERE  idCategory=@ID";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);


                    cmd.Parameters.AddWithValue("@ID", listView3.SelectedItems[0].Text);

                    cmd.ExecuteNonQuery();

                    Get_all_course_categorys();

                    this.CloseConnection();
                }
            }
            catch
            {
                MessageBox.Show("Ištrinimas nepavyko, ivyko klaida");
            }
        }

        private void button15_Click(object sender, EventArgs e) // ADD lesson
        {
            try
            {
                string query = "INSERT INTO lesson (Name,Course_idCourse) VALUES (@Name,@Course_idCourse) ";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    cmd.Parameters.AddWithValue("@Name", textBox33.Text);
                    cmd.Parameters.AddWithValue("@Course_idCourse", textBox32.Text);



                    cmd.ExecuteNonQuery();

                    Get_all_lessons();

                    //close Connection
                    this.CloseConnection();
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
            catch
            {
                MessageBox.Show("Irašymas nepavyko, ivyko klaida");
            }
        }

        private void button14_Click(object sender, EventArgs e) // Update lesson
        {
            try
            {

                string query = "UPDATE lesson SET Name=@Name ,Course_idCourse=@Course_idCourse  WHERE  idLesson=@ID";



                //Open connection
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);



                    cmd.Parameters.AddWithValue("@Name", textBox33.Text);
                    cmd.Parameters.AddWithValue("@Course_idCourse", textBox32.Text);
                

                    cmd.Parameters.AddWithValue("@ID", listView4.SelectedItems[0].Text);


                    cmd.ExecuteNonQuery();
                    //  cmd.ExecuteScalar();

                    Get_all_lessons();
                    //close connection
                    this.CloseConnection();
                }

            }
            catch
            {
                MessageBox.Show("Atnaujinimas nepavyko, ivyko klaida");
            }
        }

        private void button13_Click(object sender, EventArgs e) // Delete lesson
        {
            try
            {


                string query = "DELETE FROM lesson WHERE  idLesson=@ID";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);


                    cmd.Parameters.AddWithValue("@ID", listView4.SelectedItems[0].Text);

                    cmd.ExecuteNonQuery();

                    Get_all_lessons();

                    this.CloseConnection();
                }
            }
            catch
            {
                MessageBox.Show("Ištrinimas nepavyko, ivyko klaida");
            }
        }

        private void button12_Click(object sender, EventArgs e) // Add teacher
        {
            try
            {
                string query = "INSERT INTO teacher (Name,Surname,PhoneNumber,EmailAddress,Fax) VALUES (@Name,@Surname,@PhoneNumber,@EmailAddress,@Fax) ";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    cmd.Parameters.AddWithValue("@Name", textBox30.Text);
                    cmd.Parameters.AddWithValue("@Surname", textBox29.Text);
                    cmd.Parameters.AddWithValue("@PhoneNumber", textBox28.Text);
                    cmd.Parameters.AddWithValue("@EmailAddress", textBox27.Text);
                    cmd.Parameters.AddWithValue("@Fax", textBox26.Text);
               



                    cmd.ExecuteNonQuery();

                    Get_all_teachers();

                    //close Data Reader


                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed

                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
            catch
            {
                MessageBox.Show("Irašymas nepavyko, ivyko klaida");
            }
        }

        private void button11_Click(object sender, EventArgs e) // Update teacher
        {
            try
            {
               
                string query = "UPDATE teacher SET Name=@Name ,Surname=@Surname, PhoneNumber=@PhoneNumber, EmailAddress=@EmailAddress , Fax=@Fax  WHERE  idTeacher=@ID";



                //Open connection
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);


                    cmd.Parameters.AddWithValue("@Name", textBox30.Text);
                    cmd.Parameters.AddWithValue("@Surname", textBox29.Text);
                    cmd.Parameters.AddWithValue("@PhoneNumber", textBox28.Text);
                    cmd.Parameters.AddWithValue("@EmailAddress", textBox27.Text);
                    cmd.Parameters.AddWithValue("@Fax", textBox26.Text);


                    cmd.Parameters.AddWithValue("@ID", listView6.SelectedItems[0].Text);

                    cmd.ExecuteNonQuery();
                    //  cmd.ExecuteScalar();

                    Get_all_teachers();
                    //close connection
                    this.CloseConnection();
                }

            }
            catch
            {
                MessageBox.Show("Atnaujinimas nepavyko, ivyko klaida");
            }
        }

        private void button10_Click(object sender, EventArgs e) // Delete teacher
        {
            try
            {


                string query = "DELETE FROM teacher WHERE  idTeacher=@ID";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);


                    cmd.Parameters.AddWithValue("@ID", listView6.SelectedItems[0].Text);

                    cmd.ExecuteNonQuery();

                    Get_all_teachers();

                    this.CloseConnection();
                }
            }
            catch
            {
                MessageBox.Show("Ištrinimas nepavyko, ivyko klaida");
            }
        }

        private void button23_Click(object sender, EventArgs e) // Refresh tables!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        {
            Load_datatables();
        }

        private void button10_Click_1(object sender, EventArgs e) // This is getting weird, sorry
        {

        }

        private void button9_Click(object sender, EventArgs e)  // Add person
        {
                  try
            {
                string query = "INSERT INTO person (Name,Surname,DateOfBirth,PhoneNumber,EmailAddress,Group_idGroup,ClientCompany_idClientCompany) VALUES " + 
                    "(@Name,@Surname,@DateOfBirth,@PhoneNumber,@EmailAddress,@Group_idGroup,@ClientCompany_idClientCompany) ";




                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);



                    cmd.Parameters.AddWithValue("@Name", textBox23.Text);
                    cmd.Parameters.AddWithValue("@Surname", textBox22.Text);
                    cmd.Parameters.AddWithValue("@DateOfBirth", textBox21.Text);
                    cmd.Parameters.AddWithValue("@PhoneNumber", textBox20.Text);
                    cmd.Parameters.AddWithValue("@EmailAddress", textBox19.Text);
                    cmd.Parameters.AddWithValue("@Group_idGroup", textBox18.Text);
                    cmd.Parameters.AddWithValue("@ClientCompany_idClientCompany", textBox17.Text);
           
                    

                    cmd.ExecuteNonQuery();

                    Get_all_people();

                    //close Data Reader


                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed

                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
            catch
            {
                MessageBox.Show("Irašymas nepavyko, ivyko klaida");
            }
        }

        private void button8_Click(object sender, EventArgs e) // Update person
        {
            try
            {          
                string query = "UPDATE person SET Name=@Name,Surname=@Surname,DateOfBirth=@DateOfBirth,PhoneNumber=@PhoneNumber,EmailAddress=@EmailAddress,Group_idGroup=@Group_idGroup,ClientCompany_idClientCompany=@ClientCompany_idClientCompany WHERE " +
                   " idClient=@ID";

                //Open connection
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);



                    cmd.Parameters.AddWithValue("@Name", textBox23.Text);
                    cmd.Parameters.AddWithValue("@Surname", textBox22.Text);
                    cmd.Parameters.AddWithValue("@DateOfBirth", textBox21.Text);
                    cmd.Parameters.AddWithValue("@PhoneNumber", textBox20.Text);
                    cmd.Parameters.AddWithValue("@EmailAddress", textBox19.Text);
                    cmd.Parameters.AddWithValue("@Group_idGroup", textBox18.Text);
                    cmd.Parameters.AddWithValue("@ClientCompany_idClientCompany", textBox17.Text);

                    cmd.Parameters.AddWithValue("@ID", listView7.SelectedItems[0].Text);


                    cmd.ExecuteNonQuery();
                    //  cmd.ExecuteScalar();

                    Get_all_people();
                    //close connection
                    this.CloseConnection();
                }

            }
            catch
            {
                MessageBox.Show("Atnaujinimas nepavyko, ivyko klaida");
            }
        }

        private void button7_Click(object sender, EventArgs e) // Delete person
        {
            try
            {


                string query = "DELETE FROM person WHERE  idClient=@ID";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);


                    cmd.Parameters.AddWithValue("@ID", listView7.SelectedItems[0].Text);

                    cmd.ExecuteNonQuery();

                    Get_all_people();

                    this.CloseConnection();
                }
            }
            catch
            {
                MessageBox.Show("Ištrinimas nepavyko, ivyko klaida");
            }
        }

        private void button6_Click(object sender, EventArgs e) // ADD provider
        {
           // try
           // {
                string query = "INSERT INTO provider (Name,Surname,phone_number,email,DateOfBirth) VALUES " +
                    "(@Name,@Surname,@phone_number,@email,@DateOfBirth) ";




                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    cmd.Parameters.AddWithValue("@Name", textBox16.Text);
                    cmd.Parameters.AddWithValue("@Surname", textBox15.Text);
                    cmd.Parameters.AddWithValue("@phone_number", textBox14.Text);
                    cmd.Parameters.AddWithValue("@email", textBox13.Text);
                    cmd.Parameters.AddWithValue("@DateOfBirth", textBox12.Text);
               



                    cmd.ExecuteNonQuery();

                    Get_all_providers();

                    //close Data Reader


                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed

                }
                else
                {
                    MessageBox.Show("Error");
                }
          //  }
          //  catch
          //  {
          //      MessageBox.Show("Irašymas nepavyko, ivyko klaida");
          //  }
        }



        private void tabPage10_Click(object sender, EventArgs e) // My bad.....again...
        {

        }

        private void button5_Click(object sender, EventArgs e) // Edit provider
        {
            try
            {
                string query = "UPDATE provider SET Name=@Name,Surname=@Surname,phone_number=@phone_number,email=@email,DateOfBirth=@DateOfBirth  WHERE " +
                   " idProvider=@ID";

              
                //Open connection
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);



                    cmd.Parameters.AddWithValue("@Name", textBox16.Text);
                    cmd.Parameters.AddWithValue("@Surname", textBox15.Text);
                    cmd.Parameters.AddWithValue("@phone_number", textBox14.Text);
                    cmd.Parameters.AddWithValue("@email", textBox13.Text);
                    cmd.Parameters.AddWithValue("@DateOfBirth", textBox12.Text);

                    cmd.Parameters.AddWithValue("@ID", listView8.SelectedItems[0].Text);


                    cmd.ExecuteNonQuery();
                    //  cmd.ExecuteScalar();

                    Get_all_providers();
                    //close connection
                    this.CloseConnection();
                }

            }
            catch
            {
                MessageBox.Show("Atnaujinimas nepavyko, ivyko klaida");
            }   
        }

        private void button4_Click(object sender, EventArgs e) // REVOME K...provider
        {
            try
            {


                string query = "DELETE FROM provider WHERE  idProvider=@ID";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);


                    cmd.Parameters.AddWithValue("@ID", listView8.SelectedItems[0].Text);

                    cmd.ExecuteNonQuery();

                    Get_all_providers();

                    this.CloseConnection();
                }
            }
            catch
            {
                MessageBox.Show("Ištrinimas nepavyko, ivyko klaida");
            }
        }

        private void button3_Click(object sender, EventArgs e) // ADD company
        {
            try
            {
                string query = "INSERT INTO company (Name,CompanyID,PhoneNumer,EmailAddress,FAX,Address1,Address2) VALUES " +
                    "(@Name,@CompanyID,@PhoneNumer,@EmailAddress,@FAX,@Address1,@Address2) ";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    cmd.Parameters.AddWithValue("@Name", textBox4.Text);
                    cmd.Parameters.AddWithValue("@CompanyID", textBox3.Text);
                    cmd.Parameters.AddWithValue("@PhoneNumer", textBox6.Text);
                    cmd.Parameters.AddWithValue("@EmailAddress", textBox5.Text);
                    cmd.Parameters.AddWithValue("@FAX", textBox8.Text);
                    cmd.Parameters.AddWithValue("@Address1", textBox7.Text);
                    cmd.Parameters.AddWithValue("@Address2", textBox9.Text);



                    cmd.ExecuteNonQuery();

                    Get_all_company();

                    //close Data Reader


                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed

                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
            catch
            {
                MessageBox.Show("Irašymas nepavyko, ivyko klaida");
            }
        }

        private void button2_Click(object sender, EventArgs e) // Edit company
        {
            try
            {
                string query = "UPDATE company SET Name=@Name,CompanyID=@CompanyID,PhoneNumer=@PhoneNumer,EmailAddress=@EmailAddress,FAX=@FAX,Address1=@Address1,Address2=@Address2 WHERE " +
                   " idClientCompany=@ID";


            
                //Open connection
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);



                    cmd.Parameters.AddWithValue("@Name", textBox4.Text);
                    cmd.Parameters.AddWithValue("@CompanyID", textBox3.Text);
                    cmd.Parameters.AddWithValue("@PhoneNumer", textBox6.Text);
                    cmd.Parameters.AddWithValue("@EmailAddress", textBox5.Text);
                    cmd.Parameters.AddWithValue("@FAX", textBox8.Text);
                    cmd.Parameters.AddWithValue("@Address1", textBox7.Text);
                    cmd.Parameters.AddWithValue("@Address2", textBox9.Text);

                    cmd.Parameters.AddWithValue("@ID", listView9.SelectedItems[0].Text);




                    cmd.ExecuteNonQuery();
                    //  cmd.ExecuteScalar();

                    Get_all_company();
                    //close connection
                    this.CloseConnection();
                }

            }
            catch
            {
                MessageBox.Show("Atnaujinimas nepavyko, ivyko klaida");
            }
        }

        private void button1_Click(object sender, EventArgs e) // Remove Company
        {
            try
            {


                string query = "DELETE FROM company WHERE  idClientCompany=@ID";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);


                    cmd.Parameters.AddWithValue("@ID", listView9.SelectedItems[0].Text);

                    cmd.ExecuteNonQuery();

                    Get_all_company();

                    this.CloseConnection();
                }
            }
            catch
            {
                MessageBox.Show("Ištrinimas nepavyko, ivyko klaida");
            }
        }




    }
}
