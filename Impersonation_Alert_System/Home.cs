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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            scrollBar.Top = btnDashboard.Top;
            scrollBar.Height = btnDashboard.Height;
            lblDate.Visible = true;
            lblTime.Visible = true;
            btnLogout.Visible = true;
            

            timer1.Start();
        }
        string connectionString= @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Pope Francis\documents\visual studio 2015\Projects\Impersonation_Alert_System\Impersonation_Alert_System\ExamVerificationDB.mdf;Integrated Security=True";
       public string regNo;
        public string names;
        public void register()
        {
            SqlConnection connect = new SqlConnection(connectionString);
            string queryInsert = "insert into Student_Courses(Reg_No,Course_Code) values(@param,@param1)";
            SqlCommand insert = new SqlCommand(queryInsert, connect);
            insert.Parameters.AddWithValue("@param", txtcRegNo.Text);
            insert.Parameters.AddWithValue("@param1", txtcCourseCode.Text);
            if (connect.State == ConnectionState.Closed)
            {
                connect.Open();
            }
            int i = insert.ExecuteNonQuery();
            if (i > 0)
            {
                MessageBox.Show("Course Registered Successfully");
            }
        }
        public void addToResultSheet()
        {
            SqlConnection connect = new SqlConnection(connectionString);
                       string query = "insert into Result(Name,Reg_No,Course_Code)values(@name,@regNo,@param)";

            SqlCommand command = new SqlCommand(query, connect);
            command.Parameters.AddWithValue("@name", names);
            command.Parameters.AddWithValue("@regNo", regNo);
            command.Parameters.AddWithValue("@param", txtcCourseCode.Text);
            if (connect.State == ConnectionState.Closed)
            {
                connect.Open();
            }
            int i = command.ExecuteNonQuery();          
        }
        public void viewCourse()
        {
            SqlConnection connect = new SqlConnection(connectionString);
             string query = "select Reg_No as 'Reg No', Course_Code as 'Course Code' from Student_Courses where Reg_No=@param";
            SqlCommand command = new SqlCommand(query, connect);
            command.Parameters.AddWithValue("@param", regNo);
            SqlDataAdapter adapt = new SqlDataAdapter(command);
            DataTable tbl = new DataTable();
            adapt.Fill(tbl);
            dataGridView1.DataSource = tbl;
        }
        public void viewResult()
        {
            SqlConnection connect = new SqlConnection(connectionString);
            string query = "select * from Result where Reg_No=@param";
            SqlCommand command = new SqlCommand(query, connect);
            command.Parameters.AddWithValue("@param", regNo);
            SqlDataAdapter adapt = new SqlDataAdapter(command);
            DataTable tbl = new DataTable();
            adapt.Fill(tbl);
            dataGridView1.DataSource = tbl;
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            scrollBar.Top = btnDashboard.Top;
            scrollBar.Height = btnDashboard.Height;

            lblDate.Visible = true;
            lblTime.Visible = true;
            btnLogout.Visible = true;

            HomePane.Visible = true;

            coursePane.Visible = false;
        }

        private void btnRegisterCourse_Click(object sender, EventArgs e)
        {
            scrollBar.Top = btnRegisterCourse.Top;
            scrollBar.Height = btnRegisterCourse.Height;
            coursePane.Visible = true;
            viewPane.Visible = false;

            HomePane.Visible = false;

            lblDate.Visible = false;
            lblTime.Visible = false;
            btnLogout.Visible = false;
        }

        private void btnViewCourse_Click(object sender, EventArgs e)
        {
            scrollBar.Top = btnViewCourse.Top;
            scrollBar.Height = btnViewCourse.Height;

            coursePane.Visible = false;
            viewPane.Visible = true;

            lblDate.Visible = false;
            lblTime.Visible = false;
            btnLogout.Visible = false;

            HomePane.Visible = false;

            viewCourse();

        }

        private void btnViewResult_Click(object sender, EventArgs e)
        {
            scrollBar.Top = btnViewResult.Top;
            scrollBar.Height = btnViewResult.Height;

            coursePane.Visible = false;
            viewPane.Visible = true;
            lblDate.Visible = false;
            lblTime.Visible = false;
            btnLogout.Visible = false;

            HomePane.Visible = false;

            viewResult();

        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToLongDateString();
            lblTime.Text = DateTime.Now.ToLongTimeString();
        }

        private void btnRegCourse_Click(object sender, EventArgs e)
        {
            if (txtcCourseCode.Text==""|| txtcCourseTitle.Text==""|| txtcRegNo.Text == "")
            {
                MessageBox.Show("Please fill all Field");
            }
            if(txtcRegNo.Text != regNo)
            {
                MessageBox.Show("Please confirm Your Reg No.");
            }
            else
            {
                SqlConnection connect = new SqlConnection(connectionString);
                string query = "select * from Courses where Course_Code=@param";

                SqlCommand command = new SqlCommand(query, connect);
                command.Parameters.AddWithValue("@param", txtcCourseCode.Text);
                 connect.Open();
                SqlDataReader read = command.ExecuteReader();
                if (read.HasRows)
                {
                    register();
                    addToResultSheet();
                    read.Close();
                    connect.Close();
                }
                else
                {
                    MessageBox.Show("Course Yet to be uploaded! Please check back later.", "Course Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void lblDownloadCourse_Click(object sender, EventArgs e)
        {

        }

        private void btnCloseCourse_Click(object sender, EventArgs e)
        {
            showCourseGride.Visible = false;
            btnSeeCourses.Visible = true;
            btnCloseCourse.Visible = false;
            lblUser.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            btnLogout.Visible = true;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
                  form.ShowDialog();
                 this.Close();
        }

        private void btnSeeCourses_Click(object sender, EventArgs e)
        {
            showCourseGride.Visible = false; ;
            btnCloseCourse.Visible = false;
            btnSeeCourses.Visible = false;
            btnLogout.Visible = false;
            btnQuery.Visible = true;
            lblQuery.Visible = true;
            txtquery.Visible = true;
            label3.Visible = false;
            label2.Visible = false;
            lblUser.Visible = false;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (txtquery.Text == "")
            {
                MessageBox.Show("Please Enter your department");
            }
            else
            {
                SqlConnection connect = new SqlConnection(connectionString);
                string query = "select Course_Title,Course_Code from Courses where Department=@param";
                SqlCommand command = new SqlCommand(query, connect);
                command.Parameters.AddWithValue("@param", txtquery.Text);
                SqlDataAdapter adapt = new SqlDataAdapter(command);
                DataTable tbl = new DataTable();
                adapt.Fill(tbl);
                showCourseGride.DataSource = tbl;

                showCourseGride.Visible = true;
                txtquery.Visible = false;
                lblQuery.Visible = false;
                btnQuery.Visible = false;

                btnCloseCourse.Visible = true;
            }
        }
    }
}
