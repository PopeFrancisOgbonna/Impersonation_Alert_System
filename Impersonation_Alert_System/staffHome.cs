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
using System.IO;
using System.Drawing.Imaging;

namespace Impersonation_Alert_System
{
    public partial class staffHome : Form
    {
        public staffHome()
        {
            InitializeComponent();
            scrollBar.Top = btnDashboard.Top;
            scrollBar.Height = btnDashboard.Height;
            grpStudentDetails.Visible = false;
            lblstafName.Text = lecturerName;
            //load();
        }

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Pope Francis\documents\visual studio 2015\Projects\Impersonation_Alert_System\Impersonation_Alert_System\ExamVerificationDB.mdf;Integrated Security=True";
        string picsPath = null;
        string fingerPath = null;
        public string lecturerName;
        public string post;
        DataTable tbl = new DataTable();
        Bitmap bit1 = null;
        Bitmap bit2 = null;

        private void btnAddStaff_Click(object sender, EventArgs e)
        {
            if (post.ToLower().Equals("h.o.d") || post.ToLower().Equals("head of department") || post.ToLower().Equals("dean"))
            {
                scrollBar.Top = btnAddStaff.Top;
                scrollBar.Height = btnAddStaff.Height;
                staffDetail_Pane.Visible = true;
                grpStudentDetails.Visible = false;
                uploadCourse_Pane.Visible = false;
                dashBoardPane.Visible = false;
                upLoadResultPane.Visible = false;
                verifyStudentPane.Visible = false;
            }
            else
            {
                MessageBox.Show("You don't have Permission to Add Staff. Contact the Dean or H.O.D", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Information);
             
            }


            //Clear the input fields
            txtStaffDept.Clear();
            txtStaffFaculty.Clear();
            txtStafffName.Clear();
            txtStafflName.Clear();
            txtStaffpixName.Clear();
            txtStafID.Clear();
            txtstafPost.Clear();
            staffPics.ImageLocation = "";
            txtStaffpixName.Visible = false;
        }

        private void btnAdmitStudent_Click(object sender, EventArgs e)
        {
            if (post.ToLower().Equals("h.o.d") || post.ToLower().Equals("head of department") || post.ToLower().Equals("dean"))
            {
                scrollBar.Top = btnAdmitStudent.Top;
                scrollBar.Height = btnAdmitStudent.Height;
                grpStudentDetails.Visible = true;
                staffDetail_Pane.Visible = false;
                uploadCourse_Pane.Visible = false;
                dashBoardPane.Visible = false;
                upLoadResultPane.Visible = false;
                verifyStudentPane.Visible = false;
            }
            else
            {
                MessageBox.Show("You don't have Permission to Admit Students. Contact the Dean or H.O.D", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Information);
           
            }



            //Clear input field
            txtPicsName.Clear();
            txtReg.Clear();
            txtlName.Clear();
            txtfName.Clear();
            txtFaculty.Clear();
            txtDept.Clear();
            Picture.ImageLocation = "";
            fingerPrint.ImageLocation = "";
            txtPicsName.Visible = false;
        }

        private void btnLoadCourse_Click(object sender, EventArgs e)
        {
            if (post.ToLower().Equals("lecturer") || post.ToLower().Equals("h.o.d") || post.ToLower().Equals("hod") || post.ToLower().Equals("head of department"))
            {
                scrollBar.Top = btnLoadCourse.Top;
                scrollBar.Height = btnLoadCourse.Height;
                uploadCourse_Pane.Visible = true;
                grpStudentDetails.Visible = false;
                staffDetail_Pane.Visible = false;
                dashBoardPane.Visible = false;
                upLoadResultPane.Visible = false;
                verifyStudentPane.Visible = false;
            }
            else
            {
                MessageBox.Show("Contact the course Lecturer to Upload the course.", "Acess Denied", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            //Clear input
            txtcTitle.Clear();
            txtcLecturer.Clear();
            txtcDept.Clear();
            txtcCode.Clear();
        }

        private void btnLoadResult_Click(object sender, EventArgs e)
        {
            if (post.ToLower().Equals("lecturer"))
            {
                scrollBar.Top = btnLoadResult.Top;
                scrollBar.Height = btnLoadResult.Height;

                grpStudentDetails.Visible = false;
                staffDetail_Pane.Visible = false;
                uploadCourse_Pane.Visible = false;
                dashBoardPane.Visible = false;
                upLoadResultPane.Visible = true;
                verifyStudentPane.Visible = false;
            }
            else
            {
                MessageBox.Show("Contact the course Lecturer to Upload the Result.", "Acess Denied", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           

        }

        private void btnVerifyStudent_Click(object sender, EventArgs e)
        {
            scrollBar.Top = btnVerifyStudent.Top;
            scrollBar.Height = btnVerifyStudent.Height;

            grpStudentDetails.Visible = false;
            staffDetail_Pane.Visible = false;
            uploadCourse_Pane.Visible = false;
            dashBoardPane.Visible = false;
            upLoadResultPane.Visible = false;
            verifyStudentPane.Visible = true;

        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            scrollBar.Top = btnDashboard.Top;
            scrollBar.Height = btnDashboard.Height;
            grpStudentDetails.Visible = false;
            staffDetail_Pane.Visible = false;
            uploadCourse_Pane.Visible = false;
            dashBoardPane.Visible = true;
            upLoadResultPane.Visible = false;
            verifyStudentPane.Visible = false;
            lblstafName.Text = lecturerName;
        }
        public void loadStudent()
        {
            SqlConnection connect = new SqlConnection(connectionString);
            string query2 = "select * from Student";
            SqlCommand comm = new SqlCommand(query2, connect);
            SqlDataAdapter adapt = new SqlDataAdapter(comm);
            adapt.Fill(tbl);
            studentDataView.DataSource = tbl;

        }
        public void loadStudentforVerification()
        {
            SqlConnection connect = new SqlConnection(connectionString);
            string query = "select firstName,lastName,Department,Level,Faculty,Picture,Fingerprint from Student where Reg_No=@param";
            SqlCommand command = new SqlCommand(query, connect);
            command.Parameters.AddWithValue("@param", txtvRegNo.Text.Trim());
            if (connect.State == ConnectionState.Closed)
            {
                connect.Open();
            }
            SqlDataReader read = command.ExecuteReader();
            while (read.Read())
            {
                string fName = read[0].ToString();
                string lName = read[1].ToString();
                txtvDepartment.Text = read[2].ToString();
                txtvLevel.Text = read[3].ToString();
                txtvFaculty.Text = read[4].ToString();
                txtvName.Text = fName + " " + lName;
                byte[] pix = (Byte[])(read[5]);
                byte[] finger = (Byte[])(read[6]);
               
                MemoryStream s = new MemoryStream(pix);
                verifiedImage.Image = Image.FromStream(s);
                s.Position = 0;
                s = new MemoryStream(finger);
                bit1 = new Bitmap(s);
            }
        }
        public void loadResult()
        {
            SqlConnection connect = new SqlConnection(connectionString);
            string query = "select Id,Name,Reg_No as 'Reg No',Course_Code as 'Course Code',Course_Work as 'Course Work',Exam,Score,Grade from Result where Course_Code=@param";
            SqlCommand command = new SqlCommand(query, connect);
            command.Parameters.AddWithValue("@param", txtrCourseCode.Text.Trim());
            SqlDataAdapter adapt = new SqlDataAdapter(command);
            DataTable tbl = new DataTable();
            adapt.Fill(tbl);
            resultViewGride.DataSource = tbl;
        }
        public void loadStaff()
        {
            SqlConnection connect = new SqlConnection(connectionString);
            string query = "select * from Lecturers";
            SqlCommand command = new SqlCommand(query, connect);
            SqlDataAdapter adapt = new SqlDataAdapter(command);
            DataTable tbl = new DataTable();
            adapt.Fill(tbl);
            studentDataView.DataSource = tbl;
        }
        public void loadCourse()
        {
            SqlConnection connect = new SqlConnection(connectionString);
            string query = "select * from Courses";
            SqlCommand command = new SqlCommand(query, connect);
            SqlDataAdapter adapt = new SqlDataAdapter(command);
            DataTable tbl = new DataTable();
            adapt.Fill(tbl);
            studentDataView.DataSource = tbl;
        }
        //Fingerprint Matching algorithm================
        private bool compareFingerprint(Bitmap fingerImage1,Bitmap fingerImage2)
        {
            MemoryStream stream = new MemoryStream();
            fingerImage1.Save(stream, ImageFormat.Png);
            string firstImage = Convert.ToBase64String(stream.ToArray());
            stream.Position = 0;
            fingerImage2.Save(stream, ImageFormat.Png);
            string secondImage = Convert.ToBase64String(stream.ToArray());
            if (firstImage.Equals(secondImage))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //============== code for Admision of students begins her e==========================
        private void btnPics_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
          //  file.Filter = "All JPEG Files(*.jpeg)|*.jpeg|All PNG Images(*.png)|*.png";
            file.Multiselect = false;
            if (file.ShowDialog() == DialogResult.OK)
            {
                string name = file.SafeFileName;
               picsPath = file.FileName;
                Picture.ImageLocation = picsPath;
                txtPicsName.Text = name;
                txtPicsName.Visible = true;
            }
        }

        private void btnfinger_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
           // file.Filter = "All JPEG Files(*.jpeg)|*.jpeg|All PNG Images(*.png)|*.png";
            file.Multiselect = false;
            if (file.ShowDialog() == DialogResult.OK)
            {
                string name = file.SafeFileName;
                fingerPath = file.FileName;
                fingerPrint.ImageLocation = fingerPath;
            }
        }

        private void btnSaveStudent_Click(object sender, EventArgs e)
        {
            if (txtDept.Text == "" || txtFaculty.Text == "" || txtfName.Text == "" || txtlName.Text == "" || txtReg.Text == "")
            {
                MessageBox.Show("Please fill all Fields", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (Picture.Image==null || fingerPrint.Image == null)
            {
                MessageBox.Show("Please Upload student's Picture and Fingerprint", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //code to convert student picture to binary image
                byte[] img = null;
                FileStream stream = new FileStream(picsPath, FileMode.Open, FileAccess.Read);
                BinaryReader binary = new BinaryReader(stream);
                img = binary.ReadBytes((int)stream.Length);

                //code to convert student fingerprint to binary image
                byte[] fingerImg = null;
                 stream = new FileStream(fingerPath, FileMode.Open, FileAccess.Read);
                 binary = new BinaryReader(stream);
                fingerImg = binary.ReadBytes((int)stream.Length);

                //connecting and saving student's data to database
                SqlConnection connect = new SqlConnection(connectionString);
                string query="insert into Student(Reg_No,firstName,lastName,Department,Level,Faculty,Picture,Fingerprint,Date_Enrolled) values(@param,@param1,@param2,@param3,@param4,@param5,@param6,@param7,@param8)";
                SqlCommand command = new SqlCommand(query, connect);
        
                command.Parameters.AddWithValue("@param", txtReg.Text);
                command.Parameters.AddWithValue("@param1", txtfName.Text);
                command.Parameters.AddWithValue("@param2", txtlName.Text);
                command.Parameters.AddWithValue("@param3", txtDept.Text);
                command.Parameters.AddWithValue("@param4", level.Value);
                command.Parameters.AddWithValue("@param5", txtFaculty.Text);
                command.Parameters.AddWithValue("@param6", img);
                command.Parameters.AddWithValue("@param7", fingerImg);
                command.Parameters.AddWithValue("@param8", dateTimePicker1.Value.ToShortDateString());
              
                try
                {
                    connect.Open();
                    int i= command.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Record Saved");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        private void btnUpdateStudent_Click(object sender, EventArgs e)
        {
            if (txtReg.Text == "")
            {
                MessageBox.Show("To Update Please Enter Student's Reg No", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

            }
        }

        private void btnDeleteStudent_Click(object sender, EventArgs e)
        {
            if (txtReg.Text == "")
            {
                MessageBox.Show("Please Enter Student's Reg No", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

            }
        }
        //========Code for  Addition of Staff Starts here ==========================
   
        private void btnStaffSave_Click_1(object sender, EventArgs e)
        {
            if (txtStaffFaculty.Text == "" || txtStafffName.Text == "" || txtStafflName.Text == "" || txtStafID.Text == "" || txtstafPost.Text == "" || txtStaffDept.Text == "")
            {
                MessageBox.Show("Please fill all Fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (staffPics.Image == null)
            {
                MessageBox.Show("Please Upload Staff Image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

            }
        }

        private void btnImage_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
           // file.Filter = "All JPEG Images(*.jpeg)|*.jpeg|All PNG Images(*.png)|*.png";
            file.Multiselect = false;
            if (file.ShowDialog() == DialogResult.OK)
            {
                string name = file.SafeFileName;
                string path = file.FileName;
                staffPics.ImageLocation = path;
                txtStaffpixName.Text = name;
                txtStaffpixName.Visible = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtStaffDept.Clear();
            txtStaffFaculty.Clear();
            txtStafffName.Clear();
            txtStafflName.Clear();
            txtStaffpixName.Clear();
            txtStafID.Clear();
            txtstafPost.Clear();
            staffPics.ImageLocation = "";
            txtStaffpixName.Visible = false;
        }
        //Clear Student Admission input
        private void btnClearStudent_Click(object sender, EventArgs e)
        {
            txtPicsName.Clear();
            txtReg.Clear();
            txtlName.Clear();
            txtfName.Clear();
            txtFaculty.Clear();
            txtDept.Clear();
            Picture.ImageLocation = "";
            fingerPrint.ImageLocation = "";
            txtPicsName.Visible = false;
        }
        //Code for Course Upload ======================================
        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (txtcCode.Text == "" || txtcDept.Text == "" || txtcLecturer.Text == "" || txtcTitle.Text == "")
            {
                MessageBox.Show("Please Fill out all Field", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }else
            {
                SqlConnection connect = new SqlConnection(connectionString);
                  string query = "insert into Courses(Course_Code,Course_Title,Units,Semester,Level,Department,Faculty,Course_Lecturer) values(@param,@param1,@param2,@param3,@param4,@param5,@param6,@param7)";
                SqlCommand command = new SqlCommand(query, connect);
                command.Parameters.AddWithValue("@param", txtcCode.Text);
                command.Parameters.AddWithValue("@param1",txtcTitle.Text);
                command.Parameters.AddWithValue("@param2", cUnit.Value);
                command.Parameters.AddWithValue("@param3", cSemester.Value);
                command.Parameters.AddWithValue("@param4", cLevel.Value);
                command.Parameters.AddWithValue("@param5", txtcDept.Text);
                command.Parameters.AddWithValue("@param6", txtcFaculty.Text);
                command.Parameters.AddWithValue("@param7", txtcLecturer.Text);
                try
                {
                    connect.Open();
                    int i = command.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Record Saved successfully");
                    }
                    else
                    {
                        MessageBox.Show("Error ocuured transaction aborted");
                    }
                    connect.Close();
                    loadCourse();
                    clearInput();
                    studentDataView.Visible = true;
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString());
                }
            }
        
        }

        private void btnClear_course_Click(object sender, EventArgs e)
        {
            clearInput();
        }
        //Code to clear course inputs
        public void clearInput()
        {
            txtcTitle.Clear();
            txtcLecturer.Clear();
            txtcDept.Clear();
            txtcCode.Clear();
            txtcFaculty.Clear();
            cUnit.Value = 1;
            cLevel.Value = 100;
            cSemester.Value = 1;
        }
        private void lblStaffWelcome_Click(object sender, EventArgs e)
        {

        }

        private void btnStaffLogout_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.ShowDialog();
            this.Close(); 
        }
        public string courseKode = null;
        //===========Code for uploading students result ==================================
        private void btnRcourseSearch_Click(object sender, EventArgs e)
        {
            if (txtrCourseCode.Text == "")
            {
                MessageBox.Show("Please Enter Course code","Invalid Input",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                loadResult();
                txtrCourseCode.Clear();
                resultViewGride.Visible = true;
                btnUploadResult.Visible = true;
                MessageBox.Show("To avoid Error, Please Upload Result for Individual Student at a Time", "Caution", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //string courseWork = null;
        //string exams = null;
        //string score = null;
        //string grade = null;
        private void btnUploadResult_Click(object sender, EventArgs e)
        {
                SqlConnection connect = new SqlConnection(connectionString);
                string query = "update Result SET Course_Work=@param,Exam=@param1,Score=@param2,Grade=@param3 where Id=@code";
            for(int i = 0; i <= resultViewGride.SelectedRows.Count; i++)
            {
                SqlCommand command = new SqlCommand(query, connect);
               command.Parameters.AddWithValue("@code", resultViewGride.CurrentRow.Cells[0].Value.ToString().Trim());
                command.Parameters.AddWithValue("@param", resultViewGride.CurrentRow.Cells[4].Value.ToString().Trim());
                command.Parameters.AddWithValue("@param1", resultViewGride.CurrentRow.Cells[5].Value.ToString().Trim());
                command.Parameters.AddWithValue("@param2", resultViewGride.CurrentRow.Cells[6].Value.ToString().Trim());
                command.Parameters.AddWithValue("@param3", resultViewGride.CurrentRow.Cells[7].Value.ToString().Trim().ToUpper());
                connect.Open();
                try
                {
                    int a = command.ExecuteNonQuery();
                    if (a > 0)
                    {
                        MessageBox.Show("Result Uploaded");
                    }
                    connect.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }
            }
           
            
           
        }

        //=== Code for Students Verification============
        private void btnFingerCapture_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                string path = file.FileName;
                vFingerprint.ImageLocation = path;
                bit2 = new Bitmap(path); 
            }
        }

        private void btnVerifyFingerPrint_Click(object sender, EventArgs e)
        {
            if (txtvCourseCode.Text == "" || txtvRegNo.Text == ""|| vFingerprint.Image == null)
            {
                MessageBox.Show("Fill all Fields and Upload student's Fingerprint for Verification", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlConnection connect = new SqlConnection(connectionString);
                string query = "select * from Student_Courses where Reg_No=@param and Course_code=@param2";
                SqlCommand command = new SqlCommand(query, connect);
                command.Parameters.AddWithValue("@param", txtvRegNo.Text.Trim());
                command.Parameters.AddWithValue("@param2", txtvCourseCode.Text.Trim());
                connect.Open();
                SqlDataReader read = command.ExecuteReader();
                if (read.HasRows)
                {
                    loadStudentforVerification();
                  
                    if (compareFingerprint(bit1, bit2))
                    {
                       
                        verifiedPix.Visible = true;
                        verifiedImage.Visible = true;
                        vStudentDetails.Visible = true;
                        notVerifiedPix.Visible = false;

                        lblInfo.Visible = false;
                        lblvfake.Visible = false;
                    }
                    else
                    {
                        notVerifiedPix.Visible = true;
                        lblInfo.Text = "Impersonator Please Detain Student.";
                        lblInfo.ForeColor = Color.Red;
                        lblInfo.Visible = true;
                        lblvfake.Visible = true;
                        verifiedImage.Visible = false;
                        verifiedPix.Visible = false;
                        vStudentDetails.Visible = false;
                    }  
                }
                else
                {
                    notVerifiedPix.Visible = true;
                    lblInfo.Text = "Impersonator Please Detain Student.";
                    lblInfo.ForeColor = Color.Red;
                    lblInfo.Visible = true;
                    lblvfake.Visible = true;

                    verifiedImage.Visible = false;
                    verifiedPix.Visible = false;
                    vStudentDetails.Visible = false;
                }
              
                connect.Close();
                btnVerifyAnother.Visible = true;
            }
        }

        private void btnVerifyAnother_Click(object sender, EventArgs e)
        {
            verifiedImage.Visible = false;
            verifiedPix.Visible = false;
            vStudentDetails.Visible = false;

            notVerifiedPix.Visible = false;
            lblInfo.Visible = false;
            lblvfake.Visible = false;

            txtvCourseCode.Clear();
            txtvRegNo.Clear();
            vFingerprint.Image = null;
        }
    }
}

