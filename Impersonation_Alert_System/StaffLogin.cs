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
    public partial class StaffLogin : Form
    {
        public StaffLogin()
        {
            InitializeComponent();
        }
        public static string name = null;
        public static string post = null;
        private void btnOk_Click(object sender, EventArgs e)
        {
            if(txtPost.Text==""|| txtStaffId.Text==""|| txtStaffName.Text==""|| txtStaffPass.Text == "")
            {
                MessageBox.Show("Please fill out all Fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                name = txtStaffName.Text.Trim();
                post = txtPost.Text.Trim();
                staffHome staff = new staffHome();
                staff.lecturerName = name;
                staff.lblstafName.Text = name;
                staff.post = post;
                staff.ShowDialog();
                this.Close();
            }
        }
    }
}
