using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.Text.RegularExpressions;

namespace 图书管理系统V0._0._1
{
    public partial class book_manage : Form
    {
        public book_manage()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void book_manage_Load(object sender, EventArgs e)
        {
            label7.Visible = false;
            string user_name = sign_in.sign_name;
            string user_pass = sign_in.sign_password;
            string query_text_find_books =
            "select * from book_info";

            NpgsqlConnection conn = new NpgsqlConnection(sign_in.connstr);
            DataSet dataSet = new DataSet();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query_text_find_books, conn);
            adapter.Fill(dataSet, "图书");
            dataGridView1.DataSource = dataSet.Tables["图书"];
            dataGridView1.Columns[0].HeaderText = "序号";
            dataGridView1.Columns[1].HeaderText = "书名";
            dataGridView1.Columns[2].HeaderText = "作者";
            dataGridView1.Columns[3].HeaderText = "出版社";
            dataGridView1.Columns[4].HeaderText = "简介";
            conn.Close();

        }

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[e.RowIndex].Selected = true;
                    dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (String.Equals(dataGridView1.CurrentRow.Cells[0].Value.ToString(), "") && String.Equals(dataGridView1.CurrentRow.Cells[1].Value.ToString(), "") && String.Equals(dataGridView1.CurrentRow.Cells[2].Value.ToString(), "") && String.Equals(dataGridView1.CurrentRow.Cells[3].Value.ToString(), "") && String.Equals(dataGridView1.CurrentRow.Cells[4].Value.ToString(), ""))
            {
                MessageBox.Show("不能删除空的书籍", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //删除选定的图书
            string user_name = sign_in.sign_name;
            string user_pass = sign_in.sign_password;
            string query_text_delete_book =
            "delete from book_info where book_id = " + int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());

            if(MessageBox.Show("您确定要删除该书吗？","请确认",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                NpgsqlConnection conn = new NpgsqlConnection(sign_in.connstr);
                NpgsqlCommand cmd = new NpgsqlCommand(query_text_delete_book, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("删除成功", "恭喜", MessageBoxButtons.OK);

                book_manage_Load(null, null);
            }
            else
            {
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(label7.Visible)
            {
                MessageBox.Show("请填写正确的图书信息", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(String.Equals(textBox1.Text,"")|| String.Equals(textBox2.Text, "") || String.Equals(textBox3.Text, "") || String.Equals(textBox4.Text, ""))
            {
                MessageBox.Show("请填写正确的图书信息", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach(char c in textBox1.Text)
            {
                if(c == ' ' || c == '*' || c == '-')
                {
                    MessageBox.Show("输入书名无效，请不要使用特殊字符", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            foreach (char c in textBox2.Text)
            {
                if (c == ' ' || c == '*' || c == '-')
                {
                    MessageBox.Show("输入作者无效，请不要使用特殊字符", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            foreach (char c in textBox3.Text)
            {
                if (c == ' ' || c == '*' || c == '-')
                {
                    MessageBox.Show("输入出版社无效", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            string user_name = sign_in.sign_name;
            string user_pass = sign_in.sign_password;
            string query_text_find_max =
            "select max(book_id) from book_info ";
            
            

            //1.先在系统中找到编号最大的书，然后+1
            NpgsqlConnection conn = new NpgsqlConnection(sign_in.connstr);
            NpgsqlCommand cmd = new NpgsqlCommand(query_text_find_max, conn);
            conn.Open();
            NpgsqlDataReader reader =  cmd.ExecuteReader();
            reader.Read();
            long id = reader.GetInt64(0);
            id++;
            reader.Close();

            //2.然后插入图书信息表
            try
            {
                
                string query_text_insert_info =
                "insert into book_info values("+id+",'"+textBox1.Text+"','"+ textBox2.Text + "','"+ textBox3.Text + "','"+richTextBox1.Text+"')";
                cmd.CommandText = query_text_insert_info;
                cmd.ExecuteNonQuery();

                //3.然后插入图书数量表

                string query_text_insert_num =
                "insert into book_num values("+id+","+textBox4.Text+")";
                cmd.CommandText = query_text_insert_num;
                cmd.ExecuteNonQuery();
                conn.Close();

            }catch(Exception ex)
            {
                MessageBox.Show("您的输入有误，请重试","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
                conn.Close();
                return;
            }

            MessageBox.Show("添加成功", "恭喜", MessageBoxButtons.OK);
            book_manage_Load(null, null);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string text = textBox4.Text;
            string patten = @"^\d+$";

            MatchCollection mc = Regex.Matches(text, patten);

            if(mc.Count == 0)
            {
                label7.Visible = true;
            }else
            {
                label7.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
