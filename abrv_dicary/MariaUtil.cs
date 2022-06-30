﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace abrv_dicary
{
    class MariaUtil
    {
        public static MariaUtil mariaDB;
        private MySqlConnection dbConnection;

        private string url = "127.0.0.1";
        private string port = "3306";
        private string db = "abrv_dicary";
        private string uid = "root";
        private string pwd = "root";

        public static MariaUtil Instance
        {
            get
            {
                if(mariaDB == null)
                {
                    mariaDB = new MariaUtil();
                }
                return mariaDB;
            }
        }

        public bool IsOpen()
        {
            return (dbConnection == null) ? false : true;
        }

        public bool Open()
        {
            if (IsOpen())
            {
                Close();
            }

            bool result = true;

            string connectionString = string.Format("Server={0};Port={1};Database={2};Uid ={3};Pwd={4};Charset=utf8", url, port, db, uid, pwd);
            dbConnection = new MySqlConnection(connectionString);
            try
            {
                dbConnection.Open();
                Console.Write("마리아DB 오픈완료");
            }
            catch (Exception e)
            {
                Console.Write("DB 오픈 : " + e.Message.ToString());
                Close();
                result = false;
            }

            return result;
        }

        public bool Close()
        {
            bool result = true;

            try
            {
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    Debug.WriteLine("DB 클로즈");
                }
            }
            catch (Exception e)
            {
                result = false;
            }
            dbConnection = null;

            return result;
        }

        public DataTable Select(String sql)
        {
            var mySqlDataTable = new DataTable();
            try
            {
                MySqlCommand mySqlCommand = new MySqlCommand(sql, dbConnection);
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                mySqlDataTable.Load(mySqlDataReader);

                StringBuilder output = new StringBuilder();
                foreach (DataColumn col in mySqlDataTable.Columns)
                {
                    output.AppendFormat("{0} ", col);
                }
                output.AppendLine();
                foreach (DataRow page in mySqlDataTable.Rows)
                {
                    foreach (DataColumn col in mySqlDataTable.Columns)
                    {
                        output.AppendFormat("{0} ", page[col]);
                    }
                    output.AppendLine();
                }
                Console.WriteLine(output.ToString());

                mySqlDataReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return mySqlDataTable;
        }
    }
}
