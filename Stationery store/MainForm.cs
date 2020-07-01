using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stationery_store
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            adapter = new MySqlDataAdapter("SELECT * From Users", db.getConnection());
            table = new DataTable();

            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void Mainpanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainShop shop = new MainShop();
            shop.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            adapter = new MySqlDataAdapter("SELECT SUM(Suma) AS OrderTotal FROM Orders", db.getConnection());
            table = new DataTable();

            adapter.Fill(table);
            dataGridView2.DataSource = table;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddProduct product = new AddProduct();
            product.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Orders order = new Orders();
            order.Show();
        }
    }
}
