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
    public partial class LogInForm : Form
    {
        public LogInForm()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\LENOVO\Documents\Cafedb.mdf;Integrated Security=True;Connect Timeout=30");

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void GuestLbl_Click(object sender, EventArgs e)
        {
            this.Hide();
            GuestOrder guest = new GuestOrder();
            guest.Show();
        }
        public static string user;
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            user = UnameTb.Text;
            if (UnameTb.Text == "" || UpassTb.Text == "")
            {
                MessageBox.Show("Enter UserName or Password");
            }
            else
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select count(*) from UsersTB1 where uname= '"+UnameTb.Text+"' and Upassword= '"+UpassTb.Text+"'", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {     UserOredr uoredr = new UserOredr();
                    uoredr.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Wrong Username or Password");
                }
                    Con.Close();
            }
        }

        private void LogInForm_Load(object sender, EventArgs e)
        {

        }
    }
}
