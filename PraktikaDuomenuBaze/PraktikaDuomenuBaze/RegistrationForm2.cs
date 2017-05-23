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
    public partial class RegistrationForm2 : Form
    {
        //Label label6;
        //Label label7;
        //TextBox textBox6;
        //TextBox textBox7;
        public RegistrationForm2()
        {
            InitializeComponent();
            label6.Visible = false;
            label7.Visible = false;
            textBox6.Visible = false;
            textBox7.Visible = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = "Company name";
            label2.Text = "Company ID";
            label3.Text = "E-mail address";
            label5.Text = "Phone number";
            label4.Text = "Fax (optional)";
            label6.Visible = true;
            label7.Visible = true;
            textBox6.Visible = true;
            textBox7.Visible = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = "Name";
            label2.Text = "Surname";
            label3.Text = "Date of birth";
            label4.Text = "E-mail address";
            label5.Text = "Phone number";
            label6.Visible = false;
            label7.Visible = false;
            textBox6.Visible = false;
            textBox7.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
            Thread t = new Thread(() => Application.Run(new RegistrationForm()));
            t.Start();
            this.Close();
        }
    }
}
