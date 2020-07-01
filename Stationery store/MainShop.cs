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
    public partial class MainShop : Form
    {
        public MainShop()
        {
            InitializeComponent();

        }

        private void MainShop_Load(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT Product.ID,Factory.name,Product.name,Categories.name,  Materials.name,Colors.name FROM Product, Factory, Categories,Colors, Materials WHERE Product.F_factoryID = Factory.ID AND Product.F_CategoriesID = Categories.ID AND Product.F_materialID = Materials.ID AND Product.F_colorID = Colors.ID ORDER BY Product.ID", db.getConnection());

            adapter.SelectCommand = command;
            MySqlDataReader reader = command.ExecuteReader();

            List<String[]> data = new List<String[]>();

            while (reader.Read())
            {
                data.Add(new string[6]);

                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();
                data[data.Count - 1][5] = reader[5].ToString();



            }
            reader.Close();

            for (int i = 0; i < data.Count; i++)
                dataGridView1.Rows.Add(data[i][0], data[i][1], data[i][2], data[i][3], data[i][4], data[i][5]);

            command = new MySqlCommand("SELECT name FROM Factory", db.getConnection());
            adapter.SelectCommand = command;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                comboBox1.Items.Add(reader[0].ToString());
            }
            reader.Close();

            command = new MySqlCommand("SELECT name FROM Categories", db.getConnection());
            adapter.SelectCommand = command;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox2.Items.Add(reader[0].ToString());
            }
            reader.Close();
            db.closeConnection();

            richTextBox1.Text = "\n";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            KancShop.prodID = Convert.ToInt16(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            DB db = new DB();
            db.openConnection();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT Prices.price FROM  Prices,Product WHERE  Prices.F_productID = @pID", db.getConnection());
            command.Parameters.Add("@pID", MySqlDbType.Int16).Value = Convert.ToInt16(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            adapter.SelectCommand = command;
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                richTextBox1.Text = "Цена канцтовара: " + reader[0].ToString() + "\n";
                   
                    
            }
            reader.Close();

            db.closeConnection();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            DB db = new DB();
            db.openConnection();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT Product.ID,Factory.name,Product.name,Categories.name,  Materials.name,Colors.name FROM Product, Factory, Categories,Colors, Materials WHERE Product.F_factoryID = Factory.ID AND Product.F_CategoriesID = Categories.ID AND Product.F_materialID = Materials.ID AND Product.F_colorID = Colors.ID AND Factory.name = @fID ORDER BY Product.ID", db.getConnection());
            if (comboBox2.Text != "Тип")
               {
                   command = new MySqlCommand("SELECT Product.ID,Factory.name,Product.name,Categories.name,  Materials.name,Colors.name FROM Product, Factory, Categories,Colors, Materials WHERE Product.F_factoryID = Factory.ID AND Product.F_CategoriesID = Categories.ID AND Product.F_materialID = Materials.ID AND Product.F_colorID = Colors.ID AND Factory.name = @fID AND Categories.name = @cID  ORDER BY Product.ID", db.getConnection());
                command.Parameters.Add("@cID", MySqlDbType.VarChar).Value = Convert.ToString(comboBox2.SelectedItem);
              }
            command.Parameters.Add("@fID", MySqlDbType.VarChar).Value = Convert.ToString(comboBox1.SelectedItem);

            adapter.SelectCommand = command;
            MySqlDataReader reader = command.ExecuteReader();

            List<String[]> data = new List<String[]>();

            while (reader.Read())
            {
                data.Add(new string[6]);

                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();
                data[data.Count - 1][5] = reader[5].ToString();



            }
            reader.Close();

            for (int i = 0; i < data.Count; i++)
                dataGridView1.Rows.Add(data[i][0], data[i][1], data[i][2], data[i][3], data[i][4], data[i][5]);
            db.closeConnection();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            DB db = new DB();
            db.openConnection();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT Product.ID,Factory.name,Product.name,Categories.name,  Materials.name,Colors.name FROM Product, Factory, Categories,Colors, Materials WHERE Product.F_factoryID = Factory.ID AND Product.F_CategoriesID = Categories.ID AND Product.F_materialID = Materials.ID AND Product.F_colorID = Colors.ID AND Categories.name = @cID ORDER BY Product.ID", db.getConnection());
            if (comboBox1.Text != "Виробник")
            {
                command = new MySqlCommand("SELECT Product.ID,Factory.name,Product.name,Categories.name,  Materials.name,Colors.name FROM Product, Factory, Categories,Colors, Materials WHERE Product.F_factoryID = Factory.ID AND F_categoriesID = Categories.ID AND Product.F_materialID = Materials.ID AND Product.F_colorID = Colors.ID AND Factory.name = @fID AND Categories.name = @cID   ORDER BY Product.ID", db.getConnection());
                command.Parameters.Add("@fID", MySqlDbType.VarChar).Value = Convert.ToString(comboBox1.SelectedItem);
            }
            command.Parameters.Add("@cID", MySqlDbType.VarChar).Value = Convert.ToString(comboBox2.SelectedItem);

            adapter.SelectCommand = command;
            MySqlDataReader reader = command.ExecuteReader();

            List<String[]> data = new List<String[]>();

            while (reader.Read())
            {
                data.Add(new string[6]);

                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();
                data[data.Count - 1][5] = reader[5].ToString();



            }
            reader.Close();

            for (int i = 0; i < data.Count; i++)
                dataGridView1.Rows.Add(data[i][0], data[i][1], data[i][2], data[i][3], data[i][4], data[i][5]);
            db.closeConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm login = new LoginForm();
            login.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int orderCount = Convert.ToInt16(numericUpDown1.Value);
            DB db = new DB();
            db.openConnection();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT Product_count FROM Storage WHERE F_productID = @pID", db.getConnection());
            command.Parameters.Add("@pID", MySqlDbType.Int16).Value = KancShop.prodID;
            if ((Convert.ToInt16(command.ExecuteScalar()) - orderCount) < 1)
            {
                MessageBox.Show("Столько товара нету на складе!");
                return;
            }

            command = new MySqlCommand("UPDATE Storage SET Product_count=Product_count-@orderCount WHERE F_productID = @pID", db.getConnection());
            command.Parameters.Add("@orderCount", MySqlDbType.Int16).Value = orderCount;
            command.Parameters.Add("@pID", MySqlDbType.Int16).Value = KancShop.prodID;
            adapter.SelectCommand = command;
            command.ExecuteNonQuery();


            command = new MySqlCommand("SELECT Prices.price*@count FROM Prices WHERE Prices.F_productID = @pID", db.getConnection());
            command.Parameters.Add("@count", MySqlDbType.Int16).Value = orderCount;
            command.Parameters.Add("@pID", MySqlDbType.Int16).Value = KancShop.prodID;
            int sum = Convert.ToInt32(command.ExecuteScalar());

            command = new MySqlCommand("INSERT INTO Orders(F_productID, countProd, Suma) VALUES (@prodID, @prodCount, @sum)", db.getConnection());
            command.Parameters.Add("@prodID", MySqlDbType.Int16).Value = KancShop.prodID;

            command.Parameters.Add("@prodCount", MySqlDbType.Int16).Value = orderCount;
            command.Parameters.Add("@sum", MySqlDbType.Int32).Value = Convert.ToInt32(sum);
            adapter.SelectCommand = command;
            command.ExecuteNonQuery();

            command = new MySqlCommand("SELECT ID FROM Orders WHERE F_productID  = @prodID", db.getConnection());
            command.Parameters.Add("@prodID", MySqlDbType.Int16).Value = KancShop.prodID;
            adapter.SelectCommand = command;
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                KancShop.orderID = Convert.ToInt16(reader[0]);
            }

            this.Hide();
            OrderForm order = new OrderForm();
            order.Show();
        }
    }
 }

