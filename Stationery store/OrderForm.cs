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
    public partial class OrderForm : Form
    {
        public OrderForm()
        {
            InitializeComponent();
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT Orders.ID, Factory.name, Product.name, Colors.name, Prices.price, Orders.countProd, Orders.Suma  FROM Orders, Factory, Product, Colors, Prices WHERE Orders.ID = @oID AND Product.F_factoryID = Factory.ID AND Orders.F_productID = Product.ID AND Product.F_colorID = Colors.ID AND Orders.F_productID = Prices.F_productID ", db.getConnection());
            command.Parameters.Add("@oID", MySqlDbType.Int16).Value = KancShop.orderID;
            adapter.SelectCommand = command;
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                label3.Text += reader[0].ToString();
                label2.Text += reader[1].ToString();
                label4.Text += reader[2].ToString();
                label6.Text += reader[3].ToString();
                label7.Text += reader[4].ToString();
                label8.Text += reader[5].ToString();
                label1.Text += reader[6].ToString();
                
                
            }
            db.closeConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainShop shop = new MainShop();
            shop.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
