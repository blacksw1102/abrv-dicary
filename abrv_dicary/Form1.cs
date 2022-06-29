using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace abrv_dicary
{
    public partial class Form1 : Form
    {
        private MariaDB mariaDB;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("용어명");
            comboBox1.Items.Add("용어영문명");
            comboBox1.SelectedIndex = 0;

            mariaDB = new MariaDB();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if(!CheckKeyword())
            {
                return;
            }
            
            string message = "";

            if(mariaDB.ConnectionTest())
            {
                message = "connection 성공";
            }
            else
            {
                message = "connection 성공";
            }

            Debug.WriteLine(message);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!CheckKeyword())
                {
                    return;
                }
            }
        }

        // 키워드 체크
        private bool CheckKeyword()
        {
            bool result = true;

            if (textBox1.Text.Length < 2)
            {
                MessageBox.Show("2글자 이상의 키워드를 입력해주세요.");
                result = false;
            }

            return result;
        }
    }
}
