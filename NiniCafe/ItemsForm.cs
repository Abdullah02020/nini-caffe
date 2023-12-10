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
    public partial class ItemsForm : Form
    {
        public ItemsForm()
        {
            InitializeComponent();
        }
        //تعريف متغير من نوع قاعدة بيانات فيه معرفة قاعدة البيانات حتى نقوم بربطها بالواجهة
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\LENOVO\Documents\Cafedb.mdf;Integrated Security=True;Connect Timeout=30");
        void populate() //دالة تثبيت في خانة البيانات 
        {
            Con.Open();
            string query = "select * from ItemTb1 ";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con); //محول بيانات 
            SqlCommandBuilder buil = new SqlCommandBuilder(sda); // بناء بيانات
            var ds = new DataSet(); //وضع بيانات جديدة
            sda.Fill(ds); //ملء متغير وضع بيانات جديدة
            ItemsGV.DataSource = ds.Tables[0]; //لوحة عرض البيانات 
            Con.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            UsersForm user = new UsersForm();
            user.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserOredr order = new UserOredr();
            order.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            LogInForm login = new LogInForm();
            login.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (ItemNameTb.Text == "" || ItemNumTb.Text == "" || ItemPriceTb.Text == "")
            {
                MessageBox.Show("Fill All The Data");
            }
            else
            {
                Con.Open();
                string query = "insert into ItemTb1 values('" + ItemNumTb.Text + "','" + ItemNameTb.Text + "','" + CategCb.SelectedItem.ToString() + "','" + ItemPriceTb.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item Successfull Created");
                Con.Close();
               populate();
            }
        }

        private void ItemsForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void ItemsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ItemNumTb.Text = ItemsGV.SelectedRows[0].Cells[1].Value.ToString();
            ItemNameTb.Text = ItemsGV.SelectedRows[0].Cells[2].Value.ToString();
            CategCb.Text = ItemsGV.SelectedRows[0].Cells[3].Value.ToString();
            ItemPriceTb.Text = ItemsGV.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (ItemNumTb.Text == "")
            {
                MessageBox.Show("Select The Item to be Deleted");
            }

            else
            {
                Con.Open();
                string query = "delete from ItemTb1 where ItemNum = '" + ItemNumTb.Text + "'";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item Successfully Deleted");
                Con.Close();
                populate();
            }
        }

       private void Edit_Click(object sender, EventArgs e)
        {
            if (ItemNumTb.Text == "" || ItemNameTb.Text == "" || ItemPriceTb.Text == "")
            {
                MessageBox.Show("Fill All The Fields to be Edited");
            }

            else
            {
                Con.Open();
                string query = "update ItemTb1 set ItemName= '" + ItemNameTb.Text + "',ItemCat= '" + CategCb.SelectedItem.ToString() + "'where ItemPrice= " + ItemPriceTb.Text + "";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item Successfully updated");
                Con.Close();
                populate();
            }
        }
    }
}
