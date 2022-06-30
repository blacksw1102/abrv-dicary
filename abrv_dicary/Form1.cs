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
        private readonly string[] columnNames = {"NO", "용어명", "용어영문명", "영문약어명", "정의"};

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("용어명");
            comboBox1.Items.Add("용어영문명");
            comboBox1.SelectedIndex = 0;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            SelectWords();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if(!CheckKeyword())
            {
                return;
            }

            SelectWords();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!CheckKeyword())
                {
                    return;
                }

                SelectWords();
            }
        }

        // 키워드 체크
        private bool CheckKeyword()
        {
            bool result = true;

            if (textBox1.Text.Length == 1)
            {
                MessageBox.Show("2글자 이상의 키워드를 입력해주세요.");
                result = false;
            }

            return result;
        }

        private bool SelectWords()
        {
            bool result = true;
            string keyword = textBox1.Text;

            MariaUtil.Instance.Open();

            if (MariaUtil.Instance.IsOpen())
            {
                String query = "";
                query += "SELECT word_id";
                query += "       ,word_nm";
                query += "       ,word_en_nm";
                query += "       ,word_abrv_nm";
                query += "       ,word_dc";
                query += "  FROM word";
                if(!string.IsNullOrEmpty(keyword))
                {
                query += " WHERE MATCH(word_nm) AGAINST('" + keyword + "' IN BOOLEAN MODE)";
                }

                DataTable dt = MariaUtil.Instance.Select(query);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dt.Columns[i].ColumnName = columnNames[i];
                }
                dataGridView1.DataSource = dt;

                Debug.WriteLine("Column size : " + dataGridView1.Columns.Count);
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].Frozen = false;
                }
            }
            else
            {
                result = false;
                MessageBox.Show("데이터베이스가 연결되있지 않습니다.");
            }

            return result;
        }
    }
}
