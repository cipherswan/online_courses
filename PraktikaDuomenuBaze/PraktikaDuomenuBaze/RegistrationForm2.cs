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
        private DatabaseAccess Da;
        private Account account;
        public RegistrationForm2(Account _account)
        {
            InitializeComponent();
            label6.Visible = false;
            label7.Visible = false;
            textBox6.Visible = false;
            textBox7.Visible = false;
            textBox3.Visible = false;
            account = _account;
            Da = new DatabaseAccess();
            Da.RegResponder = (() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    Application.ExitThread();
                    this.Close();
                });
            });
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
            textBox3.Visible = true;
            dateTimePicker.Visible = false;
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
            dateTimePicker.Visible = true;
            textBox3.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
            Thread t = new Thread(() => Application.Run(new RegistrationForm()));
            t.Start();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox5.Text != "" && textBox6.Text != "")
                {
                    CompanyAccount Company = new CompanyAccount();
                    Company.Username = account.Username;
                    Company.Password = account.Password;
                    Company.CompanyName = textBox1.Text;
                    Company.CompanyID = textBox2.Text;
                    Company.EmailAddress = textBox3.Text;
                    Company.FAX = textBox4.Text;
                    Company.PhoneNumber = textBox5.Text;
                    Company.Address1 = textBox6.Text;
                    Company.Address2 = textBox7.Text;
                    this.UseWaitCursor = true;
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    Task T = new Task(() =>Da.RegisterCompanyAccount(Company));
                    T.Start();
                }
                else
                {
                    MessageBox.Show("Some fields are left empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else if(radioButton2.Checked)
            {
                if (textBox1.Text != "" && textBox2.Text != "" && textBox5.Text != "" && textBox4.Text != "")
                {
                    SoloAccount Person = new SoloAccount();
                    Person.Username = account.Username;
                    Person.Password = account.Password;
                    Person.Name = textBox1.Text;
                    Person.Surname = textBox2.Text;
                    Person.DateOfBirth = dateTimePicker.Value;
                    Person.EmailAddress = textBox4.Text;
                    Person.PhoneNumber = textBox5.Text;
                    this.UseWaitCursor = true;
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    Task T = new Task(() =>Da.RegisterSoloAccount(Person));
                    T.Start();
                }
                else
                {
                    MessageBox.Show("Mandatory fields are empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
    }
}