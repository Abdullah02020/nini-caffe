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
    public partial class UserOredr : Form
    {
        public UserOredr()
        {
            InitializeComponent();
        }

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
        void filterByCategory() //دالة تثبيت في خانة البيانات 
        {
            Con.Open();
            string query = "select * from ItemTb1 where ItemCat = '" + CategoryCb.SelectedItem.ToString() + "' ";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con); //محول بيانات 
            SqlCommandBuilder buil = new SqlCommandBuilder(sda); // بناء بيانات
            var ds = new DataSet(); //وضع بيانات جديدة
            sda.Fill(ds); //ملء متغير وضع بيانات جديدة
            ItemsGV.DataSource = ds.Tables[0]; //لوحة عرض البيانات 
            Con.Close();
        }
        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogInForm login = new LogInForm();
            login.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            this.Hide();
            UsersForm user= new UsersForm();
            user.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            ItemsForm item = new ItemsForm();
            item.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        DataTable table = new DataTable();
        int num = 0;
        int price, total;
        string item, cat;
        int flag = 0;
        int sum = 0;

        
        private void UserOredr_Load(object sender, EventArgs e)
        {
            populate(); //دالة تثبيت في خانة البيانات 
            table.Columns.Add("Number", typeof(int));
            table.Columns.Add("TheItem", typeof(string));
            table.Columns.Add("Category", typeof(string));
            table.Columns.Add("UnitPrice", typeof(int)); 
            table.Columns.Add("TheTotal", typeof(int));
            OrdersGv.DataSource = table;
            DateLbl.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
            SellerNameTb.Text = LogInForm.user;
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e) //تقسيم العناصر حسب الفئة
        {
           
            filterByCategory();
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Con.Open();
            string query = "insert into OrdersTbl values(" + OrderNumTb.Text + ",'" + DateLbl.Text + "','" + SellerNameTb.Text + "'," + LabelAmnt.Text + ")";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Order Successfull Created");
            Con.Close();
        }

        private void ViewOrdersBtn_Click(object sender, EventArgs e)
        {
            ViewOrders view = new ViewOrders();
            view.Show();
        }

        private void OrdersGv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Name = ItemsGV.SelectedRows[0].Cells[2].Value.ToString();
            cat = ItemsGV.SelectedRows[0].Cells[3].Value.ToString();
            price = Convert.ToInt32(ItemsGV.SelectedRows[0].Cells[4].Value.ToString());
            flag = 1;
        }

        private void AddToCart_Click(object sender, EventArgs e)
        {
            // تحويل الكمية إلى رقم
            int quantity;
            bool isNumeric = int.TryParse(QuantTb.Text, out quantity);

            // تحويل السعر إلى رقم
            int price;
            bool isPriceNumeric = int.TryParse(ItemsGV.SelectedRows[0].Cells[4].Value.ToString(), out price);

            // إظهار قيم الكمية والسعر للتصحيح
            MessageBox.Show("Quantity: " + quantity.ToString());
            MessageBox.Show("Price: " + price.ToString());

            // التحقق من أن الكمية والسعر أرقام
            if (!isNumeric || !isPriceNumeric)
            {
                MessageBox.Show("Please enter valid numbers for quantity and price.");
            }
            else
            {
                // الآن يمكنك استخدام 'quantity' و 'price' لحساب الإجمالي
                num = num + 1;
                item = ItemsGV.SelectedRows[0].Cells[2].Value.ToString();
                cat = ItemsGV.SelectedRows[0].Cells[3].Value.ToString();
                total = price * quantity;
                MessageBox.Show("Total: " + total.ToString());
                table.Rows.Add(num, item, cat, price, total);
                OrdersGv.DataSource = table;
                LabelAmnt.Visible = true;
            }
            //متغير الذي يجمع الاجمالي  النهائيلكل الطلب
            sum = sum + total;
            LabelAmnt.Text = "" + sum;
        }

    }
}
