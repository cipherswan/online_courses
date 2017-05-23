using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;

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
        static SqlConnection connection = new SqlConnection("user id=root;password=canada1;server=25.109.243.244;database=praktikadb");

        SqlCommand command = new SqlCommand("insert into coursecategory (Name) values (Professionalismismismismismism);", connection);

        public void doIt()
        {
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

    }

}
