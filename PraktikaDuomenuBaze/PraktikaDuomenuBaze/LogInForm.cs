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
        DatabaseAccess Da;
        public LogInForm()
        {
            InitializeComponent();
            Da = new DatabaseAccess();
            Da.LinResponder = (Username, Password, IsAdmin) =>
            {
                if(IsAdmin == true)
                {
                    Application.ExitThread();
                    Thread t = new Thread(() => Application.Run(new MainFormAdmin()));
                    t.Start();
                    this.Close();
                }
            };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (UnTb.Text != "" && PwTb.Text != "")
                Da.LogIn(UnTb.Text, PwTb.Text);
            else
                MessageBox.Show("Username or password text boxes empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
            Thread t = new Thread(() => Application.Run(new RegistrationForm()));
            t.Start();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }



}