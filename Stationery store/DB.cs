using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stationery_store
{
    class DB
    {
        MySqlConnection connection = new MySqlConnection("server=mysql316.1gb.ua; port = 3306; username=gbua_khoruzhidb;password=6z2ac82c5ty;database=gbua_khoruzhidb");
        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }
        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public MySqlConnection getConnection()
        {
            return connection;
        }
    }
}
