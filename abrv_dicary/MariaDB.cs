using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace abrv_dicary
{
    class MariaDB
    {
        private MySqlConnection dbConnection;
        private static MariaDB mariaDB;
        
        public static MariaDB GetInstance()
        {
            if(mariaDB == null)
            {
                mariaDB = new MariaDB();
            }
            return mariaDB;
        }

        public bool IsOpen()
        {
            return (dbConnection == null) ? false : true;
        }

        public bool ConnectionTest()
        {
            string url = "127.0.0.1";
            string db = "abrv_dicary";
            string uid = "root";
            string pwd = "root";
            string connectString = string.Format("Server={0};Database={1};Uid ={2};Pwd={3};", url, db, uid, pwd);
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectString))
                {
                    conn.Open();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
