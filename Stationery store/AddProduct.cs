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
    public partial class AddProduct : Form
    {
        public AddProduct()
        {
            InitializeComponent();
        }

        private void AddProduct_Load(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT `name` FROM `Factory`", db.getConnection());
            adapter.SelectCommand = command;
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                comboBox1.Items.Add(reader[0].ToString());
            }
            reader.Close();

            command = new MySqlCommand("SELECT `name` FROM `Categories`", db.getConnection());
            adapter.SelectCommand = command;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                comboBox2.Items.Add(reader[0].ToString());
            }
            reader.Close();

            command = new MySqlCommand("SELECT `name` FROM `Colors`", db.getConnection());
            adapter.SelectCommand = command;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                comboBox3.Items.Add(reader[0].ToString());
            }
            reader.Close();

            command = new MySqlCommand("SELECT `name` FROM `Materials`", db.getConnection());
            adapter.SelectCommand = command;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                comboBox4.Items.Add(reader[0].ToString());
            }
            reader.Close();

            db.closeConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((comboBox1.Text == "Производитель" || comboBox2.Text == "Тип" || comboBox3.Text == "Цвет" || comboBox4.Text == "Материал") ||
                          (textBox1.Text == "" || textBox2.Text == "" || textBox8.Text == "" ))
            {
                MessageBox.Show("Всі поля мають бути заповнені!", "Помилка!");
                return;
            }

            DB db = new DB();
            db.openConnection();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT `Colors`.`ID`, `Factory`.`ID`, `Categories`.`ID`, `Materials`.`ID` FROM `Colors`,`Factory`,`Categories`,`Materials` WHERE `Colors`.`name`=@Col AND `Factory`.`name`=@Fac AND `Categories`.`name` = @Cat AND `Materials`.`name` = @Mat ", db.getConnection());
            command.Parameters.Add("@Mat", MySqlDbType.VarChar).Value = Convert.ToString(comboBox4.SelectedItem);
            command.Parameters.Add("@Col", MySqlDbType.VarChar).Value = Convert.ToString(comboBox3.SelectedItem);
            command.Parameters.Add("@Fac", MySqlDbType.VarChar).Value = Convert.ToString(comboBox1.SelectedItem);
            command.Parameters.Add("@Cat", MySqlDbType.VarChar).Value = Convert.ToString(comboBox2.SelectedItem);
            adapter.SelectCommand = command;
            MySqlDataReader reader = command.ExecuteReader();
            int[] param = new int[4];

            while (reader.Read())
            {
                param[0] = Convert.ToInt16(reader[0]);
                param[1] = Convert.ToInt16(reader[1]);
                param[2] = Convert.ToInt16(reader[2]);
                param[3] = Convert.ToInt16(reader[2]);



            }
            reader.Close();
            command = new MySqlCommand("INSERT INTO `Product`(`name`, `F_colorID`, `F_factoryID`, `F_categoriesID`,`F_materialID`) VALUES(@name,@color,@factory,@categories,@material)", db.getConnection());
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = Convert.ToString(textBox2.Text);
            command.Parameters.Add("@color", MySqlDbType.Int16).Value = Convert.ToInt16(param[0]);
            command.Parameters.Add("@factory", MySqlDbType.Int16).Value = Convert.ToInt16(param[1]);
            command.Parameters.Add("@categories", MySqlDbType.Int16).Value = Convert.ToInt16(param[2]);
            command.Parameters.Add("@material", MySqlDbType.Int16).Value = Convert.ToInt16(param[3]);
            adapter.SelectCommand = command;
            command.ExecuteNonQuery();

            command = new MySqlCommand("SELECT `ID` FROM `Product` WHERE `name` = @name", db.getConnection());
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = Convert.ToString(textBox2.Text);
            adapter.SelectCommand = command;
           
            int IDprod = Convert.ToInt16(command.ExecuteScalar().ToString());


            command = new MySqlCommand("INSERT INTO `Prices`(`price`, `F_productID`) VALUES(@price, @pID)", db.getConnection());
            command.Parameters.Add("@pID", MySqlDbType.Int16).Value = IDprod;
            command.Parameters.Add("@price", MySqlDbType.Int32).Value = Convert.ToInt32(textBox1.Text);
            adapter.SelectCommand = command;
            command.ExecuteNonQuery();

            command = new MySqlCommand("INSERT INTO `Storage`(`F_productID`, `Product_count`) VALUES (@pID, @pCount)", db.getConnection());
            command.Parameters.Add("@pID", MySqlDbType.Int16).Value = IDprod;
            command.Parameters.Add("@pCount", MySqlDbType.Int16).Value = Convert.ToInt16(textBox8.Text);
            adapter.SelectCommand = command;
            command.ExecuteNonQuery();

            db.closeConnection();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "" || textBox7.Text == "" || textBox5.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Вcі поля мають бути заповнені!");
                return;
            }
            DB db = new DB();
            db.openConnection();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("INSERT INTO `Factory`(`name`, `city`, `adress`, `email`) VALUES (@n, @c, @a, @e)", db.getConnection());
            command.Parameters.Add("@n", MySqlDbType.VarChar).Value = Convert.ToString(textBox4.Text);
            command.Parameters.Add("@c", MySqlDbType.VarChar).Value = Convert.ToString(textBox6.Text);
            command.Parameters.Add("@a", MySqlDbType.VarChar).Value = Convert.ToString(textBox5.Text);
            command.Parameters.Add("@e", MySqlDbType.VarChar).Value = Convert.ToString(textBox7.Text);
            adapter.SelectCommand = command;
            command.ExecuteNonQuery();
            db.closeConnection();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "" )
            {
                MessageBox.Show("Вcі поля мають бути заповнені!");
                return;
            }
            DB db = new DB();
            db.openConnection();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("INSERT INTO `Colors`(`name`) VALUES (@n)", db.getConnection());
            command.Parameters.Add("@n", MySqlDbType.VarChar).Value = Convert.ToString(textBox3.Text);
           
            adapter.SelectCommand = command;
            command.ExecuteNonQuery();
            db.closeConnection();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox9.Text == "")
            {
                MessageBox.Show("Вcі поля мають бути заповнені!");
                return;
            }
            DB db = new DB();
            db.openConnection();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("INSERT INTO `Materials`(`name`) VALUES (@n)", db.getConnection());
            command.Parameters.Add("@n", MySqlDbType.VarChar).Value = Convert.ToString(textBox9.Text);

            adapter.SelectCommand = command;
            command.ExecuteNonQuery();
            db.closeConnection();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainShop shop = new MainShop();
            shop.Show();
        }
    }
    
}
