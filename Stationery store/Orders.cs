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
    public partial class Orders : Form
    {
        public Orders()
        {
            InitializeComponent();
        }

        private void Orders_Load(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();
            MySqlCommand command = new MySqlCommand("SELECT Orders.id, Factory.name, Product.name, " + "Prices.price, Orders.countProd, Orders.Suma " + "FROM Orders, Factory, Product, Prices " + "WHERE Factory.ID = Product.F_factoryID " + "AND Product.ID = Orders.F_productID " + "AND Prices.F_productID = Orders.F_productID " +
                "ORDER BY Orders.ID", db.getConnection());
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                dataGridView1.Rows.Add(
                    reader[0].ToString(),
                    reader[1].ToString(),
                    reader[2].ToString(),
                    reader[3].ToString(),
                    reader[4].ToString(),
                    reader[5].ToString());
                    
            }
            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm shop = new MainForm();
            shop.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainShop shop = new MainShop();
            shop.Show();

        }
    }
}
