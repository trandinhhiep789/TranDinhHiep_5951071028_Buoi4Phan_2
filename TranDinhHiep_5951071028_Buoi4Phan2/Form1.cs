using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TranDinhHiep_5951071028_Buoi4Phàn
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GetStudentRecord();
            //IsValidData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-NOA56JL\SQLEXPRESS;Initial Catalog=DemoCRUD2;Integrated Security=True");
        private void GetStudentRecord()
        {
            //Kết nối DB


            //Truy vấn DB
            SqlCommand cmd = new SqlCommand("SELECT * FROM StudentTb", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            StudentRecordData.DataSource = dt;
        }

        private bool IsValidData()
        {
            if (txtHo.Text == string.Empty ||
                txtTen.Text == string.Empty ||
                txtDiaChi.Text == string.Empty ||
                string.IsNullOrEmpty(txtSoDienThoai.Text) ||
                string.IsNullOrEmpty(txtSoBaoDanh.Text))
            {
                MessageBox.Show("Có chỗ chưa nhập liệu !!!",
                    "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO StudentsTb VALUES " + "(@Name, @FatherName, @Rol1Number, @Address, @Mobile)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", txtHo.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtTen.Text);
                cmd.Parameters.AddWithValue("@RollNumber", txtSoBaoDanh.Text);
                cmd.Parameters.AddWithValue("@Address", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtSoDienThoai.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentRecord();
            }
        }

        public int StudenID;
        private void StudentRecordData_CellClick(object sender, DataGridViewCellEventArgs e)
        {


            int numrow = e.RowIndex;
            if (numrow == -1)
            {
                return;
            }
            else
            {
                StudenID = Convert.ToInt32(StudentRecordData.Rows[0].Cells[0].Value);
                txtHo.Text = StudentRecordData.Rows[numrow].Cells[1].Value.ToString();
                txtTen.Text = StudentRecordData.Rows[numrow].Cells[2].Value.ToString();
                txtSoBaoDanh.Text = StudentRecordData.Rows[numrow].Cells[3].Value.ToString();
                txtDiaChi.Text = StudentRecordData.Rows[numrow].Cells[4].Value.ToString();
                txtSoDienThoai.Text = StudentRecordData.Rows[numrow].Cells[5].Value.ToString();
            }
        }

        public void RestData()
        {
            txtHo.Text = "";
            txtTen.Text = "";
            txtSoBaoDanh.Text = "";
            txtDiaChi.Text = "";
            txtSoDienThoai.Text = "";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (StudenID > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE StudentsTb SET" +
                    "Name = @Name, FatherName = @FatherName, " +
                    "RollNumber = @RollNumber, Adddress = @Adddress, " +
                    "Mobile = @Mobile WHERE StudentID = @ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", txtHo.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtTen.Text);
                cmd.Parameters.AddWithValue("@RollNumber", txtSoBaoDanh.Text);
                cmd.Parameters.AddWithValue("@Address", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtSoDienThoai.Text);
                cmd.Parameters.AddWithValue("@ID", this.StudenID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                GetStudentRecord();
                RestData();
            }
            else
            {
                MessageBox.Show("Cập nhật lỗi!", "lỗi!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (StudenID > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM StudentsTb WHERE StudentID = QID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", this.StudenID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentRecord();
                RestData();
            }
            else
            {
                MessageBox.Show("Xóa lỗi!", "lỗi!",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
