using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using EasyXLS;
using EasyXLS.Constants;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Xml;
using EasyXLS.Formulas.Functions;
using ClosedXML.Excel;

namespace summer_trining
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //this.Load += new System.EventHandler(this.data_fill);
            data_fill();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void الطلاب_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection();
            try
            {
                // ------------------------------------------------------

                if (string.IsNullOrEmpty(StuNameEditComboBox.Text))
                { lbl_error_msg.Text = "يرجى اختيار اسم الطالب للتعديل"; return; }

                if (string.IsNullOrEmpty(StuNameEditTextBox.Text))
                    { lbl_error_msg.Text = "يرجى إدخال اسم الطالب"; return; }

                if (string.IsNullOrEmpty(TextBoxStuNumEdit.Text))
                    { lbl_error_msg.Text = "يرجى إدخال الرقم الجامعي"; return; }

                if (!int.TryParse(TextBoxStuNumEdit.Text, out int studentIDNew))
                    { lbl_error_msg.Text = "الرقم الجامعي غير صالح.. تحقق من إدخال أرقام فقط!"; return; }

                if (string.IsNullOrEmpty(ComboBoxDepNameEdit.Text))
                    { lbl_error_msg.Text = "يرجى اختيار القسم"; return; }

                if (StartDateEdit.Value == DateTimePicker.MinimumDateTime)
                    { lbl_error_msg.Text = "يرجى إدخال تاريخ بداية التدريب"; return; }

                if (EndDateEdit.Value == DateTimePicker.MinimumDateTime)
                    { lbl_error_msg.Text = "يرجى إدخال تاريخ نهاية التدريب"; return; }


                // ------------------------------------------------------

                String connection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SummerTrainingDB_Updat.accdb;Persist Security Info=True";
                string sql = "UPDATE StudentInformation SET StudentID = "+ studentIDNew + ", StudentName = @StudentName, DepartmentID = @DepartmentID, StartDate = @StartDate, EndDate = @EndDate WHERE StudentID = @StudentIDOld"; 
                conn.ConnectionString = connection;
                conn.Open();

                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentName", StuNameEditTextBox.Text);
                    cmd.Parameters.AddWithValue("@DepartmentID", ComboBoxDepNameEdit.SelectedValue.ToString()); // Ensure this is a valid integer
                    cmd.Parameters.AddWithValue("@StartDate", StartDateEdit.Value);
                    cmd.Parameters.AddWithValue("@EndDate", EndDateEdit.Value);
                    cmd.Parameters.AddWithValue("@StudentIDOld", StuNameEditComboBox.SelectedValue.ToString());
                    //cmd.Parameters.AddWithValue("@StudentIDNew", TextBoxStuNumEdit.Text.ToString());


                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        lbl_error_msg.Text = "تم تحديث البانات بنجاح!";

                    }
                    else
                    {
                        lbl_error_msg.Text = "لم يتم العثور على الطالب";
                    }
                }

                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, conn);

            }
            catch (Exception ex)
            {
                lbl_error_msg.Text = "رقم الطالب المدخل مسجل مسبقا لطالب آخر"; //+ "\n" + ex.Message.ToString(); ;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                RefreshComboBoxes();

            }
        }

        private void ButtonSaveEdit_Click(object sender, EventArgs e)
        {
           
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(ComboBoxStuNumDel.Text))
                { lbl_error_msg.Text = "يرجى اختيار اسم الطالب"; return; }

            OleDbConnection conn = new OleDbConnection();
            try
            {
                String connection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SummerTrainingDB_Updat.accdb;Persist Security Info=True";
                string sql = "Delete FROM StudentInformation WHERE StudentID = @StudentID";
                conn.ConnectionString = connection;
                conn.Open();

                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    if (MessageBox.Show("هل أنت متأكد أنك تريد حذف هذا الطالب؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        cmd.Parameters.AddWithValue("@StudentID", ComboBoxStuNumDel.SelectedValue.ToString());
                    

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            lbl_error_msg.Text = "تم حذف الطاب بنجاح!";
                        }
                        else
                        {
                            lbl_error_msg.Text = "لم يتم العثور على الطالب";
                        }
                    }
                    else
                    {
                        lbl_error_msg.Text = "";
                    }
                }


            }
            catch (Exception ex)
            {
                lbl_error_msg.Text = "حدث خطأ : " + ex.Message.ToString();
            }
            finally
            {

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                RefreshComboBoxes();
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection();
            try
            {
                String connection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SummerTrainingDB_Updat.accdb;Persist Security Info=True";
                string sql = "Delete * FROM StudentInformation";
                conn.ConnectionString = connection;
                conn.Open();

                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    if (MessageBox.Show("هل أنت متأكد أنك تريد حذف جميع الطلاب ؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        cmd.Parameters.AddWithValue("@StudentID", ComboBoxStuNumDel.SelectedValue.ToString());

                        // Execute the update command
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            lbl_error_msg.Text = "تم حذف جميع الطلاب بنجاح!";
                        }
                        else
                        {
                            lbl_error_msg.Text = "لا توجد بيانات";
                        }
                    }
                    else
                    {
                        lbl_error_msg.Text = "";
                    }

                }


            }
            catch (Exception ex)
            {
                lbl_error_msg.Text = "حدث خطأ : " + ex.Message.ToString();
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                RefreshComboBoxes();
            }
        }


        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void label23_Click_1(object sender, EventArgs e)
        {

        }

        private void dateTimePicker6_ValueChanged(object sender, EventArgs e)
        {

        }

        private void الأقسام_Click(object sender, EventArgs e)
        {

        }

        private void AddStuButton_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection();
            String connection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SummerTrainingDB_Updat.accdb;Persist Security Info=True";
            string sql = "INSERT INTO StudentInformation (StudentName, StudentID, DepartmentID, StartDate, EndDate) VALUES (@StuName, @StuNum, @DepName1, @StartDateAdd, @EndDateAdd)";
            try
            {

                if (string.IsNullOrEmpty(StuName.Text))
                { lbl_error_msg.Text = "يرجى إدخال اسم الطالب"; return; }

                if (string.IsNullOrEmpty(StuNum.Text))
                { lbl_error_msg.Text = "يرجى إدخال الرقم الجامعي"; return; }

                if (!int.TryParse(StuNum.Text, out int studentID))
                { lbl_error_msg.Text = "الرقم الجامعي غير صالح.. تحقق من إدخال أرقام فقط!"; return; }

                if (string.IsNullOrEmpty(DepName1.Text))
                { lbl_error_msg.Text = "يرجى اختيار القسم"; return; }

                if (StartDateEdit.Value == DateTimePicker.MinimumDateTime)
                { lbl_error_msg.Text = "يرجى إدخال تاريخ بداية التدريب"; return; }

                if (EndDateEdit.Value == DateTimePicker.MinimumDateTime)
                { lbl_error_msg.Text = "يرجى إدخال تاريخ نهاية التدريب"; return; }


                conn.ConnectionString = connection;
                conn.Open();

                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {    
                    cmd.Parameters.Add("@StuName", OleDbType.VarChar).Value = StuName.Text;
                    cmd.Parameters.Add("@StuNum", OleDbType.Integer).Value = int.Parse(StuNum.Text);
                    cmd.Parameters.Add("@DepName1", OleDbType.VarChar).Value = DepName1.SelectedValue.ToString();
                    cmd.Parameters.Add("@StartDateAdd", OleDbType.Date).Value = StartDateAdd.Value.Date; // Only date part
                    cmd.Parameters.Add("@EndDateAdd", OleDbType.Date).Value = EndDateAdd.Value.Date; // Only date part



                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        lbl_error_msg.Text = "تم إضافة الطالب بنجاح";
                    }
                    else
                    {
                        lbl_error_msg.Text = "بيانات الطالب موجودة مسبقا";
                    }
                }




            }
            catch (Exception ex)
            {
                lbl_error_msg.Text = "الرقم الجامعي للطالب موجود مسبقا : " + ex.Message.ToString();
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                RefreshComboBoxes();
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ButtonDelDep_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection();

            if (string.IsNullOrEmpty(ComboBoxDepNumDel.Text))
            {
                lbl_error_msg_dep.Text = "يرجى اختيار القسم";
                return;
            }
            try
            {
                String connection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SummerTrainingDB_Updat.accdb;Persist Security Info=True";
                string sql = "Delete FROM DepartmentInformation WHERE DepartmentID=@DepartmentID"; 
                conn.ConnectionString = connection;
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    if (MessageBox.Show("هل أنت متأكد أنك تريد حذف هذا القسم؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        cmd.Parameters.AddWithValue("@DepartmentID", ComboBoxDepNumDel.SelectedValue.ToString());
                        // Execute the update command
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            lbl_error_msg_dep.Text = "تم حذف القسم بنجاح!";
                        }
                        else
                        {
                            lbl_error_msg_dep.Text = "لم يتم العثور على القسم";
                        }
                    }
                    else
                    {
                        lbl_error_msg_dep.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                lbl_error_msg_dep.Text = "لايمكن حذف القسم لوجود طالب مسجل : " + ex.Message.ToString();
            }
            finally
            {

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                RefreshComboBoxes();

            }
        }

        private void choic_Click(object sender, EventArgs e)
        {

        }


        //        private void data_fill(object sender, EventArgs e)
        //private void data_fill()
        //{
        //    OleDbConnection conn = new OleDbConnection();
        //    try
        //    {
        //        String connection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SummerTrainingDB_Updat.accdb;Persist Security Info=True";
        //        conn.ConnectionString = connection;
        //        conn.Open();


        //        string sql_ds_student = "SELECT StudentName , StudentID FROM StudentInformation";
        //        DataSet ds_student = new DataSet();
        //        OleDbDataAdapter adapter_student = new OleDbDataAdapter(sql_ds_student, conn);
        //        adapter_student.Fill(ds_student);
        //        if (ds_student.Tables.Count > 0)
        //        {
        //            DataTable dt = ds_student.Tables[0];

        //            StuNameEditComboBox.DisplayMember = "StudentName";
        //            StuNameEditComboBox.ValueMember = "StudentID";
        //            StuNameEditComboBox.DataSource = dt;


        //            ComboBoxStuNumDel.DisplayMember = "StudentName";
        //            ComboBoxStuNumDel.ValueMember = "StudentID";
        //            ComboBoxStuNumDel.DataSource = dt;


        //            ComboBoxStuNameReport.DisplayMember = "StudentName";
        //            ComboBoxStuNameReport.ValueMember = "StudentID";
        //            ComboBoxStuNameReport.DataSource = dt;

        //            comboBoxStuExcuse.DisplayMember = "StudentName"; 
        //            comboBoxStuExcuse.ValueMember = "StudentID";
        //            comboBoxStuExcuse.DataSource = dt;


        //        }



        //        string sql_ds_department = "SELECT DepartmentName , DepartmentID FROM DepartmentInformation";
        //        DataSet ds_department = new DataSet();
        //        OleDbDataAdapter adapter_department = new OleDbDataAdapter(sql_ds_department, conn);
        //        adapter_department.Fill(ds_department);
        //        if (ds_department.Tables.Count > 0)
        //        {
        //            DataTable dt_department = ds_department.Tables[0];

        //            ComboBoxDepNameEdit.DisplayMember = "DepartmentName";
        //            ComboBoxDepNameEdit.ValueMember = "DepartmentID";
        //            ComboBoxDepNameEdit.DataSource = dt_department;

        //            DepName1.DisplayMember = "DepartmentName";
        //            DepName1.ValueMember = "DepartmentID";
        //            DepName1.DataSource = dt_department;


        //            ComboBoxDepNumDel.DisplayMember = "DepartmentName";
        //            ComboBoxDepNumDel.ValueMember = "DepartmentID";
        //            ComboBoxDepNumDel.DataSource = dt_department;


        //            ComboBoxDepNameReport.DisplayMember = "DepartmentName";
        //            ComboBoxDepNameReport.ValueMember = "DepartmentID";
        //            ComboBoxDepNameReport.DataSource = dt_department;


        //            ComboBoxDepNameForEdit.DisplayMember = "DepartmentName";
        //            ComboBoxDepNameForEdit.ValueMember = "DepartmentID";
        //            ComboBoxDepNameForEdit.DataSource = dt_department;

        //        }


        //        string sql_ds_ex = "SELECT ExcuseDescription , ExcuseID FROM Excuses"; DataSet ds_ex = new DataSet();
        //        OleDbDataAdapter adapter_ex = new OleDbDataAdapter(sql_ds_ex, conn); adapter_ex.Fill(ds_ex);
        //        if (ds_ex.Tables.Count > 0)
        //        {
        //            DataTable dt = ds_ex.Tables[0];
        //            comboBoxEX.DisplayMember = "ExcuseDescription";
        //            comboBoxEX.ValueMember = "ExcuseID";
        //            comboBoxEX.DataSource = dt;
        //        }

        //        conn.Close();


        //    }

        //    catch (Exception ex)
        //    {
        //        lbl_error_msg.Text = "حدث خطأ : " + ex.Message.ToString();
        //    }
        //    finally
        //    {
        //        if (conn.State == System.Data.ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }
        //    }

        //}

        private void data_fill()
        {
            using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SummerTrainingDB_Updat.accdb;Persist Security Info=True"))
            {
                try
                {
                    conn.Open();

                    BindComboBox(StuNameEditComboBox, "SELECT StudentName, StudentID FROM StudentInformation", "StudentName", "StudentID");
                    BindComboBox(ComboBoxStuNumDel, "SELECT StudentName, StudentID FROM StudentInformation", "StudentName", "StudentID");
                    BindComboBox(ComboBoxStuNameReport, "SELECT StudentName, StudentID FROM StudentInformation", "StudentName", "StudentID");
                    BindComboBox(comboBoxStuExcuse, "SELECT StudentName, StudentID FROM StudentInformation", "StudentName", "StudentID");

                    BindComboBox(ComboBoxDepNameEdit, "SELECT DepartmentName, DepartmentID FROM DepartmentInformation", "DepartmentName", "DepartmentID");
                    BindComboBox(DepName1, "SELECT DepartmentName, DepartmentID FROM DepartmentInformation", "DepartmentName", "DepartmentID");
                    BindComboBox(ComboBoxDepNumDel, "SELECT DepartmentName, DepartmentID FROM DepartmentInformation", "DepartmentName", "DepartmentID");
                    BindComboBox(ComboBoxDepNameReport, "SELECT DepartmentName, DepartmentID FROM DepartmentInformation", "DepartmentName", "DepartmentID");
                    BindComboBox(ComboBoxDepNameForEdit, "SELECT DepartmentName, DepartmentID FROM DepartmentInformation", "DepartmentName", "DepartmentID");

                    BindComboBox(comboBoxEX, "SELECT ExcuseDescription, ExcuseID FROM Excuses", "ExcuseDescription", "ExcuseID");
                }
                catch (Exception ex)
                {
                    lbl_error_msg.Text = "حدث خطأ : " + ex.Message.ToString();
                }
                finally
                {

                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
        }


        private void RefreshComboBoxes()
        {
            // Refresh the ComboBoxes to update their content
            SetComboBoxDataSource(ComboBoxDepNameEdit, "DepartmentName", "DepartmentID");
            SetComboBoxDataSource(DepName1, "DepartmentName", "DepartmentID");
            SetComboBoxDataSource(ComboBoxDepNumDel, "DepartmentName", "DepartmentID");
            SetComboBoxDataSource(ComboBoxDepNameReport, "DepartmentName", "DepartmentID");
            SetComboBoxDataSource(ComboBoxDepNameForEdit, "DepartmentName", "DepartmentID");
            SetComboBoxDataSource(StuNameEditComboBox, "StudentName", "StudentID");
            SetComboBoxDataSource(ComboBoxStuNumDel, "StudentName", "StudentID");
            SetComboBoxDataSource(ComboBoxStuNameReport, "StudentName", "StudentID");
            SetComboBoxDataSource(comboBoxStuExcuse, "StudentName", "StudentID");
            SetComboBoxDataSource(comboBoxEX, "ExcuseDescription", "ExcuseID");

            // Re-bind data
            data_fill();
        }

        private void SetComboBoxDataSource(ComboBox comboBox, string displayMember, string valueMember)
        {
            comboBox.DataSource = null;
            comboBox.DisplayMember = displayMember;
            comboBox.ValueMember = valueMember;
        }

        private void BindComboBox(ComboBox comboBox, string query, string displayMember, string valueMember)
        {
            using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SummerTrainingDB_Updat.accdb;Persist Security Info=True"))
            {
                try
                {
                    conn.Open();
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                    {
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        if (ds.Tables.Count > 0)
                        {
                            DataTable dt = ds.Tables[0];

                            // Insert empty row at the beginning
                            DataRow emptyRow = dt.NewRow();
                            emptyRow[displayMember] = "";
                            emptyRow[valueMember] = DBNull.Value;
                            dt.Rows.InsertAt(emptyRow, 0);

                            comboBox.DisplayMember = displayMember;
                            comboBox.ValueMember = valueMember;
                            comboBox.DataSource = dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    lbl_error_msg.Text = "حدث خطأ : " + ex.Message.ToString();
                }
            }
        }


        private void StuNameEditComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StuNameEditComboBox.SelectedValue == null || StuNameEditComboBox.SelectedValue == DBNull.Value)
                return;

            using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SummerTrainingDB_Updat.accdb;Persist Security Info=True"))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT StudentName, StudentID, StudentInformation.DepartmentID, DepartmentName, StartDate, EndDate FROM StudentInformation INNER JOIN DepartmentInformation ON StudentInformation.DepartmentID = DepartmentInformation.DepartmentID WHERE StudentID = @StudentID";

                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentID", StuNameEditComboBox.SelectedValue.ToString());

                        DataSet ds_Student_info = new DataSet();
                        using (OleDbDataAdapter adapter_Student_info = new OleDbDataAdapter(cmd))
                        {
                            adapter_Student_info.Fill(ds_Student_info);
                            if (ds_Student_info.Tables.Count > 0 && ds_Student_info.Tables[0].Rows.Count > 0)
                            {
                                DataRow row = ds_Student_info.Tables[0].Rows[0];
                                StuNameEditTextBox.Text = row["StudentName"].ToString();
                                TextBoxStuNumEdit.Text = row["StudentID"].ToString();
                                ComboBoxDepNameEdit.SelectedValue = row["DepartmentID"];
                                StartDateEdit.Value = Convert.ToDateTime(row["StartDate"]);
                                EndDateEdit.Value = Convert.ToDateTime(row["EndDate"]);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lbl_error_msg.Text = "حدث خطأ : " + ex.Message.ToString();
                }
            }
        }

        private void ComboBoxDepNameForEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxDepNameForEdit.SelectedValue == null || ComboBoxDepNameForEdit.SelectedValue == DBNull.Value)
                return;

            using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SummerTrainingDB_Updat.accdb;Persist Security Info=True"))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM DepartmentInformation WHERE DepartmentID = @DepartmentID";

                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@DepartmentID", ComboBoxDepNameForEdit.SelectedValue.ToString());

                        DataSet ds_dept_info = new DataSet();
                        using (OleDbDataAdapter adapter_Dept_info = new OleDbDataAdapter(cmd))
                        {
                            adapter_Dept_info.Fill(ds_dept_info);
                            if (ds_dept_info.Tables.Count > 0 && ds_dept_info.Tables[0].Rows.Count > 0)
                            {
                                DataRow row = ds_dept_info.Tables[0].Rows[0];
                                TextBoxDepNumEdit.Text = row["DepartmentID"].ToString();
                                TextBoxDepNameEdit.Text = row["DepartmentName"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lbl_error_msg.Text = "حدث خطأ : " + ex.Message.ToString();
                }
            }
        }

        //private void StuNameEditComboBox_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (StuNameEditComboBox.SelectedValue == null)
        //        return; // SelectedValue is null, exit

        //    using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SummerTrainingDB_Updat.accdb;Persist Security Info=True"))
        //    {
        //        try
        //        {
        //            conn.Open();
        //            string sql = "SELECT StudentName, StudentID, StudentInformation.DepartmentID, DepartmentName, StartDate, EndDate FROM StudentInformation, DepartmentInformation WHERE StudentID = @StudentID AND StudentInformation.DepartmentID = DepartmentInformation.DepartmentID";

        //            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
        //            {
        //                cmd.Parameters.AddWithValue("@StudentID", StuNameEditComboBox.SelectedValue.ToString());

        //                DataSet ds_Student_info = new DataSet();
        //                using (OleDbDataAdapter adapter_Student_info = new OleDbDataAdapter(cmd))
        //                {
        //                    adapter_Student_info.Fill(ds_Student_info);
        //                    if (ds_Student_info.Tables.Count > 0 && ds_Student_info.Tables[0].Rows.Count > 0)
        //                    {
        //                        DataRow row = ds_Student_info.Tables[0].Rows[0];
        //                        StuNameEditTextBox.Text = row["StudentName"].ToString();
        //                        TextBoxStuNumEdit.Text = row["StudentID"].ToString();
        //                        ComboBoxDepNameEdit.Text = row["DepartmentName"].ToString();
        //                        StartDateEdit.Value = Convert.ToDateTime(row["StartDate"]);
        //                        EndDateEdit.Value = Convert.ToDateTime(row["EndDate"]);
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            lbl_error_msg.Text = "حدث خطأ : " + ex.Message.ToString();
        //        }
        //        finally
        //        {
        //            if (conn.State == ConnectionState.Open)
        //            {
        //                conn.Close();
        //            }
        //        }
        //    }
        //}

        //private void ComboBoxDepNameForEdit_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ComboBoxDepNameForEdit.SelectedValue == null)
        //        return; // SelectedValue is null, exit

        //    using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SummerTrainingDB_Updat.accdb;Persist Security Info=True"))
        //    {
        //        try
        //        {
        //            conn.Open();
        //            string sql = "SELECT * FROM DepartmentInformation WHERE DepartmentID = @DepartmentID";

        //            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
        //            {
        //                cmd.Parameters.AddWithValue("@DepartmentID", ComboBoxDepNameForEdit.SelectedValue.ToString());

        //                DataSet ds_dept_info = new DataSet();
        //                using (OleDbDataAdapter adapter_Dept_info = new OleDbDataAdapter(cmd))
        //                {
        //                    adapter_Dept_info.Fill(ds_dept_info);
        //                    if (ds_dept_info.Tables.Count > 0 && ds_dept_info.Tables[0].Rows.Count > 0)
        //                    {
        //                        DataRow row = ds_dept_info.Tables[0].Rows[0];
        //                        TextBoxDepNumEdit.Text = row["DepartmentID"].ToString();
        //                        TextBoxDepNameEdit.Text = row["DepartmentName"].ToString();
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            lbl_error_msg.Text = "حدث خطأ : " + ex.Message.ToString();
        //        }
        //        finally
        //        {
        //            if (conn.State == ConnectionState.Open)
        //            {
        //                conn.Close();
        //            }
        //        }
        //    }
        //}
        ////private void RefreshComboBoxes()
        ////{
        ////    // Refresh the ComboBoxes to update their content
        ////    ComboBoxDepNameEdit.DataSource = null;
        ////    ComboBoxDepNameEdit.DisplayMember = "DepartmentName";
        ////    ComboBoxDepNameEdit.ValueMember = "DepartmentID";

        ////    DepName1.DataSource = null;
        ////    DepName1.DisplayMember = "DepartmentName";
        ////    DepName1.ValueMember = "DepartmentID";

        ////    ComboBoxDepNumDel.DataSource = null;
        ////    ComboBoxDepNumDel.DisplayMember = "DepartmentName";
        ////    ComboBoxDepNumDel.ValueMember = "DepartmentID";

        ////    ComboBoxDepNameReport.DataSource = null;
        ////    ComboBoxDepNameReport.DisplayMember = "DepartmentName";
        ////    ComboBoxDepNameReport.ValueMember = "DepartmentID";


        ////    //----------------------------------

        ////    ComboBoxDepNameForEdit.DataSource = null;
        ////    ComboBoxDepNameForEdit.DisplayMember = "DepartmentName";
        ////    ComboBoxDepNameForEdit.ValueMember = "DepartmentID";


        ////    StuNameEditComboBox.DataSource = null;
        ////    StuNameEditComboBox.DisplayMember = "StudentName";
        ////    StuNameEditComboBox.ValueMember = "StudentID";

        ////    //----------------------------------


        ////    ComboBoxStuNumDel.DataSource = null;
        ////    ComboBoxStuNumDel.DisplayMember = "StudentName";
        ////    ComboBoxStuNumDel.ValueMember = "StudentID";


        ////    ComboBoxStuNameReport.DataSource = null;
        ////    ComboBoxStuNameReport.DisplayMember = "StudentName";
        ////    ComboBoxStuNameReport.ValueMember = "StudentID";


        ////    comboBoxStuExcuse.DataSource = null;
        ////    comboBoxStuExcuse.DisplayMember = "StudentName"; 
        ////    comboBoxStuExcuse.ValueMember = "StudentID";

        ////    comboBoxEX.DataSource = null;
        ////    comboBoxEX.DisplayMember = "ExcuseDescription";
        ////    comboBoxEX.ValueMember = "ExcuseID";
        ////    // Re-bind data
        ////    data_fill();
        ////}

        ////private void StuNameEditComboBox_SelectedIndexChanged(object sender, EventArgs e)
        ////{

        ////        OleDbConnection conn = new OleDbConnection();
        ////        try
        ////        {
        ////            String connection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SummerTrainingDB_Updat.accdb;Persist Security Info=True";
        ////            //string sql = "SELECT * FROM StudentInformation WHERE StudentID = @StudentID";
        ////            string sql = "SELECT StudentName, StudentID, StudentInformation.DepartmentID , DepartmentName, StartDate, EndDate FROM StudentInformation, DepartmentInformation WHERE StudentID = @StudentID AND StudentInformation.DepartmentID = DepartmentInformation.DepartmentID";
        ////            conn.ConnectionString = connection;
        ////            conn.Open();

        ////            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
        ////            {
        ////                cmd.Parameters.AddWithValue("@StudentID", StuNameEditComboBox.SelectedValue.ToString()); //***************************************

        ////            DataSet ds_Student_info = new DataSet();
        ////                OleDbDataAdapter adapter_Student_info = new OleDbDataAdapter(cmd);
        ////                adapter_Student_info.Fill(ds_Student_info);

        ////                if (ds_Student_info.Tables.Count > 0 && ds_Student_info.Tables[0].Rows.Count > 0)
        ////                {
        ////                    DataRow row = ds_Student_info.Tables[0].Rows[0];
        ////                    StuNameEditTextBox.Text = row["StudentName"].ToString();
        ////                    TextBoxStuNumEdit.Text = row["StudentID"].ToString();
        ////                    ComboBoxDepNameEdit.Text = row["DepartmentName"].ToString();
        ////                    StartDateEdit.Value = Convert.ToDateTime(row["StartDate"]);
        ////                    EndDateEdit.Value = Convert.ToDateTime(row["EndDate"]);
        ////                }

        ////                conn.Close();
        ////            }
        ////        }
        ////        catch (Exception ex)
        ////        {
        ////            lbl_error_msg.Text = "حدث خطأ : " + ex.Message.ToString();
        ////        }
        ////        finally
        ////        {
        ////            if (conn.State == ConnectionState.Open)
        ////            {
        ////                conn.Close();
        ////            }
        ////        }
        ////}



        private void EditStu_Enter(object sender, EventArgs e)
        {

        }

        private void LabelStuNumDel_Click(object sender, EventArgs e)
        {

        }

        //private void ComboBoxDepNameForEdit_SelectedIndexChanged(object sender, EventArgs e)
        //{


        //    OleDbConnection conn = new OleDbConnection();
        //    try
        //    {
        //        String connection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SummerTrainingDB_Updat.accdb;Persist Security Info=True";
        //        string sql = "SELECT * FROM DepartmentInformation WHERE DepartmentID = @DepartmentID";
        //        conn.ConnectionString = connection;
        //        conn.Open();

        //        using (OleDbCommand cmd = new OleDbCommand(sql, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@DepartmentID", ComboBoxDepNameForEdit.SelectedValue.ToString()); //***************************************

        //            DataSet ds_dept_info = new DataSet();
        //            OleDbDataAdapter adapter_Dept_info = new OleDbDataAdapter(cmd);
        //            adapter_Dept_info.Fill(ds_dept_info);

        //            if (ds_dept_info.Tables.Count > 0 && ds_dept_info.Tables[0].Rows.Count > 0)
        //            {
        //                DataRow row = ds_dept_info.Tables[0].Rows[0];
        //                TextBoxDepNumEdit.Text= row["DepartmentID"].ToString();
        //                TextBoxDepNameEdit.Text = row["DepartmentName"].ToString();

        //            }

        //            conn.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lbl_error_msg.Text = "حدث خطأ : " + ex.Message.ToString();
        //    }
        //    finally
        //    {
        //        if (conn.State == ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }
        //    }
        //}



        private void ButtonSaveDepEdit_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection();
            try
            {

                if (string.IsNullOrEmpty(ComboBoxDepNameForEdit.Text))
                {
                    lbl_error_msg_dep.Text = "يرجى اختيار القسم";
                    return;
                }

                if (string.IsNullOrEmpty(TextBoxDepNameEdit.Text))
                {
                    lbl_error_msg_dep.Text = "يرجى إدخال اسم القسم";
                    return;
                }

                if (string.IsNullOrEmpty(TextBoxDepNumEdit.Text))
                {
                    lbl_error_msg_dep.Text = "يرجى إدخال رقم القسم";
                    return;
                }

                if (!int.TryParse(TextBoxDepNumEdit.Text, out int departmentID))
                {
                    lbl_error_msg_dep.Text = "رقم القسم غير صالح.. تحقق من إدخال أرقام فقط!";
                    return;
                }



                String connection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SummerTrainingDB_Updat.accdb;Persist Security Info=True";
                string sql = "UPDATE DepartmentInformation SET DepartmentName = @DepartmentName, DepartmentID = @DepartmentID WHERE DepartmentID = @OldDepartmentID";

                conn.ConnectionString = connection;
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    // Add parameters
                    cmd.Parameters.AddWithValue("@DepartmentName", TextBoxDepNameEdit.Text);
                    cmd.Parameters.AddWithValue("@DepartmentID", departmentID);
                    cmd.Parameters.AddWithValue("@OldDepartmentID", int.Parse(ComboBoxDepNameForEdit.SelectedValue.ToString()));

                    // Execute the updated command
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        lbl_error_msg_dep.Text = "تم تحديث البانات بنجاح";
                    }
                    else
                    {
                        lbl_error_msg_dep.Text = "لم يتم العثور على القسم";
                    }
                }
            }
            catch (Exception ex)
            {
                lbl_error_msg_dep.Text =  "رقم القسم المدخل مسجل مسبقا لقسم آخر" + "\n" + ex.Message.ToString();


            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                RefreshComboBoxes();
            }
        }
    



    private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void ButtonAddDep_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection();
            String connection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SummerTrainingDB_Updat.accdb;Persist Security Info=True";
            string sql = "INSERT INTO DepartmentInformation (DepartmentID, DepartmentName) VALUES (@DepNumAdd,@DepNameAdd)";
            try
            {

                conn.ConnectionString = connection;


                if (string.IsNullOrEmpty(DepNameAdd.Text))
                { lbl_error_msg_dep.Text = "يرجى إدخال اسم القسم "; return; }



                if (string.IsNullOrEmpty(DepNumAdd.Text))
                { lbl_error_msg_dep.Text = "يرجى إدخال رقم القسم"; return; }

                if (!int.TryParse(DepNumAdd.Text, out int DepartmentID))
                { lbl_error_msg_dep.Text = "رقم القسم غير صالح.. تحقق من إدخال أرقام فقط!"; return; }

                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {

                    cmd.Parameters.Add("@DepNumAdd", OleDbType.Integer).Value = int.Parse(DepNumAdd.Text);
                    cmd.Parameters.Add("@DepNameAdd", OleDbType.VarChar).Value = DepNameAdd.Text;


                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        lbl_error_msg_dep.Text = "تم إضافة القسم بنجاح";
                        RefreshComboBoxes();
                    }
                    else
                    {
                        lbl_error_msg_dep.Text = "بيانات القسم موجودة مسبقا";
                    }
                }




            }
            catch (Exception ex)
            {
                lbl_error_msg_dep.Text = "رقم القسم موجود مسبقا" + "\n"+ ex.Message.ToString();

            }
            finally
            {

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                RefreshComboBoxes();
            }
        }

        private void groupBox1_Enter_2(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection();

            if (string.IsNullOrEmpty(comboBoxStuExcuse.Text))
            { lbl_error_msg_Excuse.Text = "يرجى اختيار اسم الطالب"; return; }

            if (string.IsNullOrEmpty(comboBoxEX.Text))
            { lbl_error_msg_Excuse.Text = "يرجى إختيار العذر"; return; }

            if (dateTimeExcuse.Value == DateTimePicker.MinimumDateTime)
            { lbl_error_msg_Excuse.Text = "يرجى اختيار تاريخ العذر "; return; }


            String connection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SummerTrainingDB_Updat.accdb;Persist Security Info=True";
            string sql = "INSERT INTO StudentExcuses (StudentID, ExcuseDate, ExcuseID) VALUES (" + int.Parse(comboBoxStuExcuse.SelectedValue.ToString()) + ", @ExcuseDate, @ExcuseID)"; 
            try
            {


                conn.ConnectionString = connection; 
                conn.Open();

                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Add("@ExcuseDate", OleDbType.Date).Value = dateTimeExcuse.Value.Date; 
                    //cmd.Parameters.AddWithValue("@StudentID", comboBoxStuExcuse.SelectedValue);
                    cmd.Parameters.AddWithValue("@ExcuseID", comboBoxEX.SelectedValue);  
                    
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        lbl_error_msg_Excuse.Text = "تم إضافة العذر بنجاح";
                    }
                    else
                    {
                        lbl_error_msg_Excuse.Text = "تم إضافة العذر مسبقا";
                    }
                }


            }
            catch (Exception ex)
            {
                lbl_error_msg_Excuse.Text = "حدث خطأ : " + ex.Message.ToString();
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void comboBoxStuExcuse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void EditDep_Enter(object sender, EventArgs e)
        {

        }

        private void ComboBoxStuNumDel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void ExStuButton_Click(object sender, EventArgs e)
        {

            try
            {

                if (string.IsNullOrEmpty(ComboBoxStuNameReport.Text))
                { lbl_error_msg_report.Text = "يرجى اختيار اسم الطالب"; return; }

                if (StartDateStuReport.Value == DateTimePicker.MinimumDateTime)
                { lbl_error_msg_report.Text = "يرجى اختيار تاريخ البداية "; return; }

                if (StartDateStuReport.Value == DateTimePicker.MinimumDateTime)
                { lbl_error_msg_report.Text = "يرجى اختيار تاريخ النهاية "; return; }

                DateTime startDate = StartDateStuReport.Value;
                DateTime endDate = EndDateStuReport.Value;
                if (endDate < startDate)
                { lbl_error_msg_report.Text = "تاريخ النهاية لا يمكن أن يكون قبل تاريخ البداية"; return; }

                String sutdentID = ComboBoxStuNameReport.SelectedValue.ToString();


                DateTime startDateS = StartDateStuReport.Value;
                DateTime endDateS = EndDateStuReport.Value;

                String sutdentIDS = ComboBoxStuNameReport.SelectedValue.ToString();



                var (OlddateTable, hours) = OneStudentRecord(sutdentIDS, startDateS, endDateS);

                if (OlddateTable == null)
                { lbl_error_msg_report.Text = "حدث خطأ.. يرجى المحاولة مرة أخرى"; return; }



                DataTable dateTable = ReverseDataTableColumns(OlddateTable);



                string fileName = GetFileName(ComboBoxStuNameReport);


                //var worksheet = workbook.Worksheets.Add("$\"تقرير_{studentName}\"");
                // Create a new workbook
                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("$\"{sutdentIDS}_{studentName}\"");

                // Set the column order to right-to-left
                worksheet.RightToLeft = true;


                // Write the DataTable headers to the worksheet in reverse order
                for (int i = dateTable.Columns.Count - 1; i >= 0; i--)
                {
                    worksheet.Cell(1, dateTable.Columns.Count - i).Value = dateTable.Columns[i].ColumnName;
                }


                // Write the DataTable data to the worksheet, starting from row 2
                for (int row = 0; row < OlddateTable.Rows.Count; row++)
                {
                    for (int col = 0; col < OlddateTable.Columns.Count; col++)
                    {
                        object cellValue = OlddateTable.Rows[row][col];
                        Type cellType = cellValue.GetType();

                        if (cellType == typeof(string))
                        {
                            worksheet.Cell(row + 2, col + 1).SetValue((string)cellValue);
                        }
                        else if (cellType == typeof(int))
                        {
                            worksheet.Cell(row + 2, col + 1).SetValue((int)cellValue);
                        }
                        else if (cellType == typeof(double))
                        {
                            worksheet.Cell(row + 2, col + 1).SetValue((double)cellValue);
                        }
                        else if (cellType == typeof(DateTime))
                        {
                            worksheet.Cell(row + 2, col + 1).SetValue((DateTime)cellValue);
                        }
                        else
                        {
                            worksheet.Cell(row + 2, col + 1).SetValue(cellValue.ToString());
                        }
                    }
                }

                // Add the "Total Hours" row
                int lastRow = worksheet.LastRowUsed().RowNumber() + 1;
                worksheet.Cell(lastRow, 1).Value = $"مجموع الساعات: {hours}";

                // Save the workbook to the specified file name
                workbook.SaveAs(fileName);




                // Open excel file automatically after creation
                try
                {
                    Process.Start(fileName);
                }
                catch (System.ComponentModel.Win32Exception ex)
                {
                    // erro in default Excel application 
                    MessageBox.Show($"Error opening the Excel file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                dateTable.Dispose();
                Console.ReadLine();

            }


            catch (Exception ex)
            {
                lbl_error_msg_report.Text = "حدث خطأ : " + ex.Message.ToString();



            }

        }

        //private void ExStuButton_Click(object sender, EventArgs e)
        //{

        //    try
        //    {

        //        if (string.IsNullOrEmpty(ComboBoxStuNameReport.Text))
        //            { lbl_error_msg_report.Text = "يرجى اختيار اسم الطالب"; return; }

        //        if (StartDateStuReport.Value == DateTimePicker.MinimumDateTime)
        //            { lbl_error_msg_report.Text = "يرجى اختيار تاريخ البداية "; return; }

        //        if (StartDateStuReport.Value == DateTimePicker.MinimumDateTime)
        //            { lbl_error_msg_report.Text = "يرجى اختيار تاريخ النهاية "; return; }

        //        DateTime startDate = StartDateStuReport.Value;
        //        DateTime endDate = EndDateStuReport.Value;
        //        if (endDate < startDate)
        //            { lbl_error_msg_report.Text = "تاريخ النهاية لا يمكن أن يكون قبل تاريخ البداية"; return; }

        //        String sutdentID = ComboBoxStuNameReport.SelectedValue.ToString();



        //        var (Student_Table, hours) = OneStudentRecord(sutdentID, startDate, endDate);

        //        if (Student_Table == null)
        //        { lbl_error_msg_report.Text = "حدث خطأ.. يرجى المحالولة مرة أخرى"; return; }


        //        DataSet Student_Dataset = new DataSet();
        //        Student_Dataset.Tables.Add(Student_Table);



        //        //*************************************************************************************
        //        // getting the student's name 
        //        OleDbConnection conn1 = new OleDbConnection();
        //        string connection1 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SummerTrainingDB_Updat.accdb;Persist Security Info=True";
        //        conn1.ConnectionString = connection1;
        //        conn1.Open();

        //        string sql_studentName = "SELECT StudentName from StudentInformation where StudentID = @StudentID";
        //        OleDbCommand cmd_studentName = new OleDbCommand(sql_studentName, conn1);
        //        cmd_studentName.Parameters.AddWithValue("@StudentID", ComboBoxStuNameReport.SelectedValue);
        //        string studentName = cmd_studentName.ExecuteScalar().ToString();


        //        //*******************
        //        // creating the excel file 
        //        ExcelDocument Workbook = new ExcelDocument();
        //        string fileName = $"تقرير الطالبة_{studentName}_{DateTime.Now.ToString("dd-MM-yyyy")}.xlsx";
        //        Workbook.easy_WriteXLSXFile_FromDataSet(fileName, Student_Dataset, new ExcelAutoFormat(Styles.AUTOFORMAT_EASYXLS1), "ds_student");

        //        // Confirm export of Excel file
        //        String sError = Workbook.easy_getError();
        //        if (sError.Equals(""))
        //        {
        //            lbl_error_msg_report.Text = "\nتم إنشاء الملف بنجاح ";

        //            // Open excel file automatically after creation
        //            try
        //            {
        //                Process.Start(fileName);
        //            }
        //            catch (System.ComponentModel.Win32Exception ex)
        //            {
        //                // erro in default Excel application 
        //                MessageBox.Show($"Error opening the Excel file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //        }

        //        else
        //            lbl_error_msg_report.Text = "\nError creating the Excel file " + sError;

        //        Workbook.Dispose();
        //        Student_Dataset.Dispose();
        //        Console.ReadLine();

        //    }


        //    catch (Exception ex)
        //    {
        //        lbl_error_msg_report.Text = "حدث خطأ : " + ex.Message.ToString();
        //    }

        //}


        private void ExDepButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ComboBoxDepNameReport.Text))
            { lbl_error_msg_report.Text = "يرجى اختيار اسم القسم"; return; }

            if (StartDateDepReport.Value == DateTimePicker.MinimumDateTime)
            { lbl_error_msg_report.Text = "يرجى اختيار تاريخ البداية "; return; }

            if (EndDateDepReport.Value == DateTimePicker.MinimumDateTime)
            { lbl_error_msg_report.Text = "يرجى اختيار تاريخ النهاية "; return; }

            DateTime startDate = StartDateDepReport.Value;
            DateTime endDate = EndDateDepReport.Value;
            if (endDate < startDate)
            { lbl_error_msg_report.Text = "تاريخ النهاية لا يمكن أن يكون قبل تاريخ البداية"; return; }

            String departmentID = ComboBoxDepNameReport.SelectedValue.ToString();



            Tuple<string, string, DataTable>[] department_Arr = DepartmentRecord(departmentID, startDate, endDate);

            // Joooooooooooooooooooooooooooddddddd
            for (int i = 0; i < department_Arr.Length; i++)
            {
                string day_Day = department_Arr[i].Item1; // First string in the tuple
                string date_Date = department_Arr[i].Item2; // Second string in the tuple
                DataTable sue_dep_table = department_Arr[i].Item3; // DataTable in the tuple




            }

            //if (department_Table == null)
            //{ lbl_error_msg_report.Text = "حدث خطأ.. يرجى المحالولة مرة أخرى"; return; }


            //DataSet department_Dataset = new DataSet();
            //department_Dataset.Tables.Add(department_Table);
        }




        private Tuple<string, string, DataTable>[] DepartmentRecord(String departmentID, DateTime startDate, DateTime endDate)
        {
            OleDbConnection conn1 = new OleDbConnection();
            OleDbConnection conn2 = new OleDbConnection();



            try
            {

                string connection1 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SummerTrainingDB_Updat.accdb;Persist Security Info=True";
                string connection2 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\10.103.131.5_27_5_2024_7_33_36.mdb;Persist Security Info=True";

                conn1.ConnectionString = connection1;
                conn1.Open();

                conn2.ConnectionString = connection2;
                conn2.Open();

                //------------------------------------------------------
                string sql_StuDep = "SELECT StudentID, StudentName " +
                                      "FROM StudentInformation " +
                                      "WHERE DepartmentID = " + departmentID + "  ;";


                DataTable dt_StuDep= new DataTable();
                OleDbDataAdapter adapter_StuDep= new OleDbDataAdapter(sql_StuDep, conn1);
                adapter_StuDep.Fill(dt_StuDep);

                int nember_of_stu = dt_StuDep.Rows.Count;
                DataTable[] stu_Arr = new DataTable[nember_of_stu];

                int i = 0;
                foreach(DataRow row in dt_StuDep.Rows)
                {
                    string studentID = row["StudentID"].ToString();
                    var(Student_Table, hours) = OneStudentRecord(studentID, startDate, endDate);
                    stu_Arr[i] = Student_Table;
                    i++;
                }

                //-------------------------------------

                DataTable dateTable = CreateDateTable(startDate, endDate);
                int nember_of_days = dateTable.Rows.Count;

                Tuple<string, string, DataTable>[] department_Arr = new Tuple<string, string, DataTable>[nember_of_days];


                int j = 0;
                foreach (DataRow row in dateTable.Rows)
                {
                    string day_Day = row["DayOfWeekArabic"].ToString();

                    DateTime dateValue = Convert.ToDateTime(row["Date"]);
                    string date_Date = dateValue.ToString("dd/MM/yyyy");


                    DataTable dt_stu_dep = new DataTable();

                    dt_stu_dep.Columns.Add("StuID", typeof(string));
                    dt_stu_dep.Columns.Add("StuName", typeof(string));
                    dt_stu_dep.Columns.Add("ComeIn", typeof(string));
                    dt_stu_dep.Columns.Add("LeaveOut", typeof(string));
                    dt_stu_dep.Columns.Add("HoursBetween", typeof(string));
                    dt_stu_dep.Columns.Add("Excuses", typeof(string));

                    //foreach (DataRow dt_StuDep_row in dt_StuDep.Rows)
                    //{

                    //}
                    for (int k = 0; k < nember_of_stu; k++){

                        DataRow newRow = dt_stu_dep.NewRow();

                        newRow["StuID"] = dt_StuDep.Rows[k]["StudentID"].ToString();
                        newRow["StuName"] = dt_StuDep.Rows[k]["StudentName"].ToString();
                        newRow["ComeIn"] = stu_Arr[k].Rows[j]["وقت الحضور"].ToString();
                        newRow["LeaveOut"] = stu_Arr[k].Rows[j]["وقت الانصراف"].ToString();
                        newRow["HoursBetween"] = stu_Arr[k].Rows[j]["ساعات الدوام"].ToString();
                        newRow["Excuses"] = stu_Arr[k].Rows[j]["ملاحظات"].ToString();


                        dt_stu_dep.Rows.Add(newRow);

                    }
                    department_Arr[j] = new Tuple<string, string, DataTable>(day_Day, date_Date, dt_stu_dep);

                    j++;
                }


               //------------------------------------------------------

                conn1.Close();
                conn2.Close();

                return department_Arr;
            }


            catch (Exception ex)
            {
                if (conn1.State == ConnectionState.Open) { conn1.Close(); }
                if (conn2.State == ConnectionState.Open) { conn2.Close(); }

                lbl_error_msg_report.Text = "حدث خطأ : " + ex.Message.ToString();
                return null ;
            }
        
        }

            private (DataTable , int) OneStudentRecord(String sutdentID, DateTime startDate, DateTime endDate)
        {
            OleDbConnection conn1 = new OleDbConnection();
            OleDbConnection conn2 = new OleDbConnection();
            
            try
            {

                string connection1 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SummerTrainingDB_Updat.accdb;Persist Security Info=True";
                string connection2 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\10.103.131.5_27_5_2024_7_33_36.mdb;Persist Security Info=True";

                conn1.ConnectionString = connection1;
                conn1.Open();

                conn2.ConnectionString = connection2;
                conn2.Open();


                int add_count = -1;

                string sql_student = "SELECT sdwEnrollNumber, mdate1, time1 from Table1 where sdwEnrollNumber = '" + sutdentID + "' ORDER BY mdate1 ASC, time1 DESC;";
                DataSet ds_student = new DataSet();
                OleDbDataAdapter adapter_student = new OleDbDataAdapter(sql_student, conn2);
                adapter_student.Fill(ds_student);


                string sql_student_count = "SELECT sdwEnrollNumber, mdate1, COUNT(*) AS RecordCount FROM Table1 WHERE sdwEnrollNumber = '" + sutdentID + "' GROUP BY sdwEnrollNumber, mdate1 order by mdate1;";

                DataSet ds_student_count = new DataSet();
                OleDbDataAdapter adapter_student_count = new OleDbDataAdapter(sql_student_count, conn2);
                adapter_student_count.Fill(ds_student_count);



                string sql_excuse = "SELECT StudentExcuses.StudentID, StudentExcuses.ExcuseDate, Excuses.ExcuseDescription " +
                                     "FROM StudentExcuses, Excuses " +
                                     "WHERE StudentExcuses.StudentID = " + sutdentID + " AND StudentExcuses.ExcuseID = Excuses.ExcuseID ;";
                DataSet ds_excuse = new DataSet();
                OleDbDataAdapter adapter_excuse = new OleDbDataAdapter(sql_excuse, conn1);
                adapter_excuse.Fill(ds_excuse);



                DateTime train_start = DateTime.MinValue;
                DateTime train_end = DateTime.MinValue;

                string sql_train_date = "SELECT StartDate, EndDate " +
                                        "FROM StudentInformation " +
                                        "WHERE StudentID = " + sutdentID + "  ;";

                using (OleDbCommand cmd = new OleDbCommand(sql_train_date, conn1))
                {
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            train_start = reader.GetDateTime(reader.GetOrdinal("StartDate"));
                            train_end = reader.GetDateTime(reader.GetOrdinal("EndDate"));
                        }
                    }
                }

                // Create a new DataTable to hold the result
                DataTable dt_result = new DataTable();
                dt_result.Columns.Add("sdwEnrollNumber", typeof(string));
                dt_result.Columns.Add("mdate1", typeof(DateTime));
                dt_result.Columns.Add("ComeIn", typeof(DateTime));
                dt_result.Columns.Add("LeaveOut", typeof(DateTime));
                dt_result.Columns.Add("MinutesBetween", typeof(int));
                dt_result.Columns.Add("HoursBetween", typeof(string));


                // Access the first table in the DataSet
                DataTable dt_student = ds_student.Tables[0];
                DataTable dt_student_count = ds_student_count.Tables[0];
                DataTable dt_excuse = ds_excuse.Tables[0];


                DateTime parsedDate;

                // Define the format and the culture
                string[] formats = { "M/d/yyyy hh:mm:ss tt", "d/M/yyyy hh:mm:ss tt", "yyyy/M/d hh:mm:ss tt" };
                CultureInfo provider = new CultureInfo("en-US");

                // Override the AM/PM designators to match Arabic
                provider.DateTimeFormat.AMDesignator = "ص";
                provider.DateTimeFormat.PMDesignator = "م";


                foreach (DataRow row in dt_student_count.Rows)
                {
                    add_count++;
                    DataRow newRow = dt_result.NewRow();
                    newRow["sdwEnrollNumber"] = row["sdwEnrollNumber"];
                    newRow["mdate1"] = row["mdate1"];


                    // -----------------------------------

                    string time1String = dt_student.Rows[add_count]["time1"].ToString();
                    if (DateTime.TryParseExact(time1String, formats, provider, DateTimeStyles.None, out parsedDate))
                    { newRow["ComeIn"] = parsedDate; }
                    else
                    { newRow["ComeIn"] = DBNull.Value; }

                    // -----------------------------------

                    if (row["RecordCount"] != DBNull.Value && Convert.ToInt32(row["RecordCount"]) > 1)
                    {
                        add_count = add_count + Convert.ToInt32(row["RecordCount"]) - 1;
                        string time2String = dt_student.Rows[add_count]["time1"].ToString();
                        if (DateTime.TryParseExact(time2String, formats, provider, DateTimeStyles.None, out parsedDate))
                        { newRow["LeaveOut"] = parsedDate; }
                        else
                        { newRow["LeaveOut"] = DBNull.Value; }
                    }
                    else
                    { newRow["LeaveOut"] = DBNull.Value; }



                    // Calculate minutes between ComeIn and LeaveOut if both are not DBNull.Value
                    if (newRow["ComeIn"] != DBNull.Value && newRow["LeaveOut"] != DBNull.Value)
                    {
                        TimeSpan duration = ((DateTime)newRow["LeaveOut"]) - ((DateTime)newRow["ComeIn"]);
                        if ((int)duration.TotalMinutes > 5)
                        {
                            newRow["MinutesBetween"] = (int)duration.TotalMinutes;
                            newRow["HoursBetween"] = string.Format("{0:00}:{1:00}", (int)duration.TotalHours, duration.Minutes);
                        }
                        else
                        {
                            newRow["MinutesBetween"] = DBNull.Value;
                            newRow["HoursBetween"] = DBNull.Value;
                            newRow["LeaveOut"] = DBNull.Value;
                        }
                    }
                    else
                    {
                        newRow["MinutesBetween"] = DBNull.Value;
                        newRow["HoursBetween"] = DBNull.Value;

                    }

                    dt_result.Rows.Add(newRow);


                }



                DataTable dateTable = CreateDateTable(startDate, endDate);

                dateTable.Columns.Add("DateString", typeof(string));
                dateTable.Columns.Add("ComeIn", typeof(string));
                dateTable.Columns.Add("LeaveOut", typeof(string));
                dateTable.Columns.Add("MinutesBetween", typeof(int));
                dateTable.Columns.Add("HoursBetween", typeof(string));
                dateTable.Columns.Add("Excuses", typeof(string));




                foreach (DataRow dateTable_row in dateTable.Rows)
                {

                    DateTime dateTable_row_date = Convert.ToDateTime(dateTable_row["Date"]);

                    dateTable_row["DateString"] = dateTable_row_date.ToString("dd/MM/yyyy");

                    foreach (DataRow dt_result_row in dt_result.Rows)
                    {
                        DateTime resultDate = Convert.ToDateTime(dt_result_row["mdate1"]);
                        if (dateTable_row_date.Date == resultDate.Date)
                        {
                            dateTable_row["ComeIn"] = Convert.ToDateTime(dt_result_row["ComeIn"]).ToString("HH:mm tt");

                            if (dt_result_row["LeaveOut"] != DBNull.Value)
                            {
                                dateTable_row["LeaveOut"] = Convert.ToDateTime(dt_result_row["LeaveOut"]).ToString("HH:mm tt");
                            }
                            dateTable_row["MinutesBetween"] = dt_result_row["MinutesBetween"];
                            dateTable_row["HoursBetween"] = dt_result_row["HoursBetween"];
                            break;
                        }

                    }

                    foreach (DataRow dt_excuse_row in dt_excuse.Rows)
                    {
                        DateTime excuseDate = Convert.ToDateTime(dt_excuse_row["ExcuseDate"]);
                        if (dateTable_row_date.Date == excuseDate.Date)
                        {
                            dateTable_row["Excuses"] = dt_excuse_row["ExcuseDescription"];
                            break;
                        }
                        else
                        {
                            dateTable_row["Excuses"] = DBNull.Value;
                        }
                    }


                    if (train_start != DateTime.MinValue && dateTable_row_date.Date < train_start.Date && dateTable_row["ComeIn"] == DBNull.Value)
                    {
                        dateTable_row["Excuses"] = "لم يتم بدء التدريب";
                    }

                    if (train_end != DateTime.MinValue && dateTable_row_date.Date > train_end.Date && dateTable_row["ComeIn"] == DBNull.Value)
                    {
                        dateTable_row["Excuses"] = "تم إنهاء التدريب";
                    }

                    if (dateTable_row["ComeIn"] != DBNull.Value && dateTable_row["LeaveOut"] == DBNull.Value && dateTable_row["Excuses"] == DBNull.Value)
                    {
                        dateTable_row["Excuses"] = "تم إدخال بصمة واحدة لليوم";
                    }

                }


                conn1.Close();
                conn2.Close();


                int total_Minutes = 0;
                foreach (DataRow row in dateTable.Rows)
                {
                    if (row["MinutesBetween"] != DBNull.Value)
                    {
                        total_Minutes += row.Field<int>("MinutesBetween");
                    }
                }

                int total_Hours = (int)Math.Round((double)total_Minutes / 60);

                dateTable.Columns.Remove("MinutesBetween");
                dateTable.Columns.Remove("Date");

                dateTable.Columns["DayOfWeekArabic"].ColumnName = "اليوم";
                dateTable.Columns["DateString"].ColumnName = "التاريخ";
                dateTable.Columns["ComeIn"].ColumnName = "وقت الحضور";
                dateTable.Columns["LeaveOut"].ColumnName = "وقت الانصراف";
                dateTable.Columns["HoursBetween"].ColumnName = "ساعات الدوام";
                dateTable.Columns["Excuses"].ColumnName = "ملاحظات";

                return (dateTable, total_Hours);

            }


            catch (Exception ex)
            {
                if (conn1.State == ConnectionState.Open) { conn1.Close(); }
                if (conn2.State == ConnectionState.Open) { conn2.Close(); }

                lbl_error_msg_report.Text = "حدث خطأ : " + ex.Message.ToString();
                return (null, 0);
            }

        }









        private DataTable CreateDateTable(DateTime startDate, DateTime endDate)
        {
            DataTable table = new DataTable(); 
            table.Columns.Add("DayOfWeekArabic", typeof(string));
            table.Columns.Add("Date", typeof(DateTime));

            DateTime currentDate = startDate;
            while (currentDate <= endDate)
            {
                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Friday)
                {
                    //string dayOfWeekEnglish = currentDate.DayOfWeek.ToString();
                    string dayOfWeekArabic = GetArabicDayOfWeek(currentDate.DayOfWeek);
                    table.Rows.Add(dayOfWeekArabic, currentDate);
                }
                currentDate = currentDate.AddDays(1);
            }
            return table;
        }
        
        private static string GetArabicDayOfWeek(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return "الأحد";
                case DayOfWeek.Monday:
                    return "الاثنين";
                case DayOfWeek.Tuesday:
                    return "الثلاثاء";
                case DayOfWeek.Wednesday:
                    return "الأربعاء";
                case DayOfWeek.Thursday:
                    return "الخميس";
                case DayOfWeek.Friday:
                    return "الجمعة";
                case DayOfWeek.Saturday:
                    return "السبت";
                default:
                    return string.Empty;
            }
        }

        private void ComboBoxStuNameReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxStuNameReport.SelectedValue == null || ComboBoxStuNameReport.SelectedValue == DBNull.Value) return;
            using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SummerTrainingDB_Updat.accdb;Persist Security Info=True"))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT StartDate, EndDate FROM StudentInformation WHERE StudentID = @StudentID";
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentID", ComboBoxStuNameReport.SelectedValue.ToString());
                        DataSet ds_Student_info = new DataSet(); using (OleDbDataAdapter adapter_Student_info = new OleDbDataAdapter(cmd))
                        {
                            adapter_Student_info.Fill(ds_Student_info);
                            if (ds_Student_info.Tables.Count > 0 && ds_Student_info.Tables[0].Rows.Count > 0)
                            {
                                DataRow row = ds_Student_info.Tables[0].Rows[0];
                                StartDateStuReport.Value = Convert.ToDateTime(row["StartDate"]); EndDateStuReport.Value = Convert.ToDateTime(row["EndDate"]);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lbl_error_msg.Text = "حدث خطأ : " + ex.Message.ToString();
                }
            }
        }


        public string GetFileName(ComboBox comboBox)
        {
            string studentName = GetStudentNameFromComboBox(comboBox);
            string fileName = $"تقرير الطالبة_{studentName}_{DateTime.Now.ToString("dd-MM-yyyy")}.xlsx";
            return fileName;
        }

        private string GetStudentNameFromComboBox(ComboBox comboBox)
        {
            using (OleDbConnection conn = new OleDbConnection())
            {
                string connection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\SummerTrainingDB_Updat.accdb;Persist Security Info=True";
                conn.ConnectionString = connection;
                conn.Open();

                string sql = "SELECT StudentName FROM StudentInformation WHERE StudentID = @StudentID";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@StudentID", comboBox.SelectedValue);
                string studentName = cmd.ExecuteScalar().ToString();

                conn.Close();
                return studentName;
            }

        }



        public static DataTable ReverseDataTableColumns(DataTable input)
        {
            DataTable output = new DataTable();

            // Add the columns in reverse order
            for (int i = input.Columns.Count - 1; i >= 0; i--)
            {
                output.Columns.Add(input.Columns[i].ColumnName);
            }

            // Add the rows with the columns in reverse order
            foreach (DataRow row in input.Rows)
            {
                DataRow newRow = output.NewRow();
                for (int i = 0; i < input.Columns.Count; i++)
                {
                    newRow[i] = row[input.Columns.Count - 1 - i];
                }
                output.Rows.Add(newRow);
            }

            return output;
        }

    }






}
