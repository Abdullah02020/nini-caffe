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
    public partial class ViewOrders : Form
    {
        public ViewOrders()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\LENOVO\Documents\Cafedb.mdf;Integrated Security=True;Connect Timeout=30");
        private void AddToCartBtn_Click(object sender, EventArgs e)
        {
            this.Hide();

        }

        void populate() //دالة تثبيت في خانة البيانات 
        {
            Con.Open();
            string query = "select * from OrdersTbl ";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con); //محول بيانات 
            SqlCommandBuilder buil = new SqlCommandBuilder(sda); // بناء بيانات
            var ds = new DataSet(); //وضع بيانات جديدة
            sda.Fill(ds); //ملء متغير وضع بيانات جديدة
            OrdersGV.DataSource = ds.Tables[0]; //لوحة عرض البيانات 
            Con.Close();
        }
            private void ViewOrders_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void OrdersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("=====NiniCafe Software=====", new Font("Abeezee", 20, FontStyle.Bold), Brushes.DeepPink, new Point(180, 40));
            e.Graphics.DrawString("=====Order Summary=====" ,new Font("Abeezee",20,FontStyle.Bold), Brushes.DeepPink, new Point(188,70));
            e.Graphics.DrawString(" Number:"+OrdersGV.SelectedRows[0].Cells[0].Value.ToString(), new Font("Abeezee", 15, FontStyle.Regular), Brushes.Black, new Point(100, 135));
            e.Graphics.DrawString(" Date:" + OrdersGV.SelectedRows[0].Cells[1].Value.ToString(), new Font("Abeezee", 15, FontStyle.Regular), Brushes.Black, new Point(100, 170));
            e.Graphics.DrawString("Seller :" + OrdersGV.SelectedRows[0].Cells[2].Value.ToString(), new Font("Abeezee", 15, FontStyle.Regular), Brushes.Black, new Point(105, 205));
            e.Graphics.DrawString(" Amount:" + OrdersGV.SelectedRows[0].Cells[3].Value.ToString(), new Font("Abeezee", 15, FontStyle.Regular), Brushes.Black, new Point(100, 240));
            e.Graphics.DrawString("=====Powered By Space=====", new Font("Abeezee", 20, FontStyle.Bold), Brushes.DeepPink, new Point(188, 340));
        }
    }
}
