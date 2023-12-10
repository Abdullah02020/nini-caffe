using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; //تعريف مكتبة قاعدة البيانات

namespace NiniCafe
{
    public partial class UsersForm : Form
    {
        public UsersForm()
        {
            InitializeComponent();
        }
        //تعريف متغير من نوع قاعدة بيانات فيه معرفة قاعدة البيانات حتى نقوم بربطها بالواجهة
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\LENOVO\Documents\Cafedb.mdf;Integrated Security=True;Connect Timeout=30");
        void populate() //دالة تثبيت في خانة البيانات 
        {
            Con.Open();
            string query = "select * from UsersTb1 ";
            SqlDataAdapter sda = new SqlDataAdapter(query,Con); //محول بيانات 
            SqlCommandBuilder buil = new SqlCommandBuilder(sda); // بناء بيانات
            var ds = new DataSet(); //وضع بيانات جديدة
            sda.Fill(ds); //ملء متغير وضع بيانات جديدة
            UsersGV.DataSource = ds.Tables[0]; //لوحة عرض البيانات 
            Con.Close();
        }
        private void orderform_Click(object sender, EventArgs e)
        {
            UserOredr uorder = new UserOredr();
            uorder.Show();
            this.Hide();
        }

        private void itemform_Click(object sender, EventArgs e)
        {
            ItemsForm item = new ItemsForm();
            item.Show();
            this.Hide();
        }

        private void logoutLable_Click(object sender, EventArgs e)
        {
            LogInForm login = new LogInForm();
            login.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void Add_Click(object sender, EventArgs e) //بتن الاضافة
        {
            if (UphoneTb.Text == "" || UpassTb.Text == "" || UnameTb.Text == "")
            {
                MessageBox.Show("Fill All The Fields ");
            }
            else
            {
                Con.Open();
                string query = "insert into UsersTb1 values('" + UnameTb.Text + "','" + UphoneTb.Text + "','" + UpassTb.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Successfull Created");
                Con.Close();
                populate();
            }
        }

        private void UsersForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void UsersGV_CellContentClick(object sender, DataGridViewCellEventArgs e) //عرض بيانات اليوزر
        {
            UnameTb.Text = UsersGV.SelectedRows[0].Cells[1].Value.ToString();
            UphoneTb.Text = UsersGV.SelectedRows[0].Cells[2].Value.ToString();
            UpassTb.Text = UsersGV.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void Delete_Click(object sender, EventArgs e) //بتن الحذف
        {
            if (UphoneTb.Text == "")
            {
                MessageBox.Show("Select The user to be Deleted");
                    }
            
            else
            {
                Con.Open();
                string query = "delete from UsersTb1 where Uphone = '" + UphoneTb.Text + "'";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Successfully Deleted");
                Con.Close();
                populate();
            }
        }

        private void Edit_Click(object sender, EventArgs e)  //بتن التعديل
        {
            if (UphoneTb.Text == "" || UpassTb.Text == "" || UnameTb.Text == "")
            {
                MessageBox.Show("Fill All The Fields to be Edited");
            }
            else
            {
                Con.Open();
                string query = "update UsersTb1 set Uname= '" + UnameTb.Text + "',Upassword= '" + UpassTb.Text + "'where id= "+UphoneTb.Text+"";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Successfully updated");
                Con.Close();
                populate();
            }
        }

       
    }
}
