using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace WindowsFormsApplication5
{
    class MySQL
    {
        public MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=db1");
        public MySqlDataReader dr;
        public MySqlCommand cmd = new MySqlCommand();

        internal static DataTable dtReports = new DataTable();
        public void Connect()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }
        public void Disconnect()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}
