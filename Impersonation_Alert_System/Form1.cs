using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Impersonation_Alert_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string question = "Login as  a Student?";
            string log = "Login Option";
         var result= MessageBox.Show(question, log,MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (result==DialogResult.Yes)
            {
                studentsLogin student = new studentsLogin();
                student.ShowDialog();
                this.Close();
            }
            else
            {
                StaffLogin staff = new StaffLogin();
                staff.ShowDialog();
                this.Close();
            }
         
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToLongDateString();
            lbltime.Text = DateTime.Now.ToLongTimeString();
        }
    }
}
