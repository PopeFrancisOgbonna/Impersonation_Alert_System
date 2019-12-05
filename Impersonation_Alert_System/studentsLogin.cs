using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Impersonation_Alert_System
{
    public partial class studentsLogin : Form
    {
        public studentsLogin()
        {
            InitializeComponent();
        }
        public static string user = null;
        public static string regNo = null;
        public static string name = null;
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Pope Francis\documents\visual studio 2015\Projects\Impersonation_Alert_System\Impersonation_Alert_System\ExamVerificationDB.mdf;Integrated Security=True";
        private void btnOk_Click(object sender, EventArgs e)
        {
            if(txtRegNo.Text==""|| txtStudentName.Text=="" || txtStudentPass.Text == "")
            {
                MessageBox.Show("Please fill all Fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                SqlConnection connect = new SqlConnection(connectionString);
                string query = "select * from Student where Reg_No=@param";

                SqlCommand command = new SqlCommand(query, connect);
                command.Parameters.AddWithValue("@param", txtRegNo.Text);
                try
                {
                    connect.Open();
                    SqlDataReader read = command.ExecuteReader();
                    if (read.Read())
                    {
                        string f = read[1].ToString();
                        string l = read[2].ToString();
                        name = f + " " + l;
                        read.Close();
                        connect.Close();

                        user = txtStudentName.Text;
                        regNo = txtRegNo.Text;
                        Home home = new Home();
                        home.lblUser.Text = user;
                        home.regNo = regNo;
                        home.names = name;
                        home.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please visit the school Portal to confirm your Admission Status.", "Admission Status Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
