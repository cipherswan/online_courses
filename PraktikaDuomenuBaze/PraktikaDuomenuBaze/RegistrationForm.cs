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
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
            Thread t = new Thread(() => Application.Run(new RegistrationForm2()));
            t.Start();
            this.Close();
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
