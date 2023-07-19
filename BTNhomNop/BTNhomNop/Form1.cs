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

namespace BTNhomNop
{
    public partial class Form1 : Form
    {
        string cnn = "Data Source=LAPTOP-RVCHSRUB\\MSSQLSERVER01;Initial Catalog=QLSinhVien;Integrated Security=SSPI;";
        BindingSource db = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection strcnn = new SqlConnection(cnn);
            if (strcnn == null)
            {
                strcnn = new SqlConnection(cnn);
            }
            SqlDataAdapter adt = new SqlDataAdapter("Select * from Student", strcnn);
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adt);
            DataSet ds = new DataSet();
            adt.Fill(ds, "student");
            db = new BindingSource(ds, "student");
            tb_ma.DataBindings.Add("text", db, "StudentID");
            tb_ten.DataBindings.Add("text", db, "FullName");
            tb_diem.DataBindings.Add("text", db, "AverageScore");
            cb_khoa.DataBindings.Add("text", db, "FacultyID");
            int num1 = db.Position;
            int num2 = db.Count - 1;
            string text = num1.ToString() + "/" + num2.ToString();
            label_num.Text = text;
            button7.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (db.Position > 0)
            {
                db.Position--;
                int num1 = db.Position;
                int num2 = db.Count - 1;
                string text = num1.ToString() + "/" + num2.ToString();
                label_num.Text = text;
            }else
            {
                MessageBox.Show("Error !");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (db.Position < db.Count - 1)
            {
                db.Position++;
                int num1 = db.Position;
                int num2 = db.Count - 1;
                string text = num1.ToString() + "/" + num2.ToString();
                label_num.Text = text;
            }
            else
            {
                MessageBox.Show("Error !");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            db.Position = 0;
            int num1 = db.Position;
            int num2 = db.Count - 1;
            string text = num1.ToString() + "/" + num2.ToString();
            label_num.Text = text;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            db.Position = db.Count - 1;
            int num1 = db.Position;
            int num2 = db.Count - 1;
            string text = num1.ToString() + "/" + num2.ToString();
            label_num.Text = text;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            button7.Enabled = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(cnn);
            conn.Open();
            SqlCommand com = new SqlCommand("insert into Student values ('" + tb_ma.Text + "','" + tb_ten.Text + "','" + float.Parse(tb_diem.Text) + "','" + int.Parse(cb_khoa.Text) + "')", conn);
            com.ExecuteNonQuery();
            SqlDataAdapter adt = new SqlDataAdapter("Select * from Student", conn);
            DataSet ds = new DataSet();
            adt.Fill(ds);
            db.DataSource = ds.Tables[0];
            int num1 = db.Position;
            int num2 = db.Count - 1;
            string text = num1.ToString() + "/" + num2.ToString();
            label_num.Text = text;
            MessageBox.Show("Luu vao CSDL Thanh Cong !");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var ask= MessageBox.Show("Ban co muon xoa sinh vien nay?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question); 
            if (ask == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection(cnn);
                conn.Open();
                SqlCommand com = new SqlCommand("delete Student where StudentID = '"+tb_ma.Text+"'", conn);
                com.ExecuteNonQuery();
                SqlDataAdapter adt = new SqlDataAdapter("Select * from Student", conn);
                DataSet ds = new DataSet();
                adt.Fill(ds);
                db.DataSource = ds.Tables[0];
                db.Position = db.Count - 1;
                int num1 = db.Position;
                int num2 = db.Count - 1;
                string text = num1.ToString() + "/" + num2.ToString();
                label_num.Text = text;
                MessageBox.Show("Xoa sinh vien Thanh Cong !");
            }
        }
    }
}
