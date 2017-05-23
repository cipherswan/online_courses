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
namespace PraktikaDuomenuBaze
{
    public partial class RegistrationForm : Form
    {
        Account account;
        public RegistrationForm()
        {
            InitializeComponent();
            account = new Account();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox4.Text == textBox3.Text)
            {
                account.Username = textBox1.Text;
                account.Password = textBox3.Text;
                Application.ExitThread();
                Thread t = new Thread(() => Application.Run(new RegistrationForm2(account)));
                t.Start();
                this.Close();
            }
            else
            {
                MessageBox.Show("Some fields are left empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
            Thread t = new Thread(() => Application.Run(new LogInForm()));
            t.Start();
            this.Close();
        }
    }
}
