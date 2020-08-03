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

namespace 图书管理系统V0._0._1
{
    public partial class borrow : Form
    {
        public borrow()
        {
            InitializeComponent();
        }

        private void borrow_Load(object sender, EventArgs e)
        {
            //把图书信息在datagridview1中展示出来
            string user_name = sign_in.sign_name;
            string user_pass = sign_in.sign_password;
            string query_text_find_books =
            "select book_info.book_id,book_name,author,publisher,self_info,remain from book_info , book_num where book_info.book_id = book_num.book_id";

            DataSet data = new DataSet();
            NpgsqlConnection conn = new NpgsqlConnection(sign_in.connstr);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query_text_find_books, conn);
            adapter.Fill(data, "书籍");
            dataGridView1.DataSource = data.Tables["书籍"];
            dataGridView1.Columns[0].HeaderText = "序号";
            dataGridView1.Columns[1].HeaderText = "书名";
            dataGridView1.Columns[2].HeaderText = "作者";
            dataGridView1.Columns[3].HeaderText = "出版社";
            dataGridView1.Columns[4].HeaderText = "简介";
            dataGridView1.Columns[5].HeaderText = "剩余数量";
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

        private void 查看简介ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (String.Equals(dataGridView1.CurrentRow.Cells[0].Value.ToString(), "") && String.Equals(dataGridView1.CurrentRow.Cells[1].Value.ToString(), "") && String.Equals(dataGridView1.CurrentRow.Cells[2].Value.ToString(), "") && String.Equals(dataGridView1.CurrentRow.Cells[3].Value.ToString(), "") && String.Equals(dataGridView1.CurrentRow.Cells[4].Value.ToString(), "") && String.Equals(dataGridView1.CurrentRow.Cells[5].Value.ToString(), ""))
            {
                MessageBox.Show("请不要点击空白区域", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            book_brief_info info = new book_brief_info();
            info.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void 借书ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(String.Equals(dataGridView1.CurrentRow.Cells[0].Value.ToString(),"") && String.Equals(dataGridView1.CurrentRow.Cells[1].Value.ToString(), "") && String.Equals(dataGridView1.CurrentRow.Cells[2].Value.ToString(), "") && String.Equals(dataGridView1.CurrentRow.Cells[3].Value.ToString(), "")  && String.Equals(dataGridView1.CurrentRow.Cells[4].Value.ToString(), "") && String.Equals(dataGridView1.CurrentRow.Cells[5].Value.ToString(), ""))
            {
                MessageBox.Show("请不要点击空白区域", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(int.Parse(dataGridView1.CurrentRow.Cells[5].Value.ToString()) == 0)
            {
                MessageBox.Show("此书剩余0本，无法借阅", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(MessageBox.Show("确认借书","请确认",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //1.首先将这本书的数量减一
                string user_name = sign_in.sign_name;
                string user_pass = sign_in.sign_password;
                string query_text_sub_number = "update book_num set remain = remain - 1 where book_id = "+int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                string query_text_add_number = "update book_num set remain = remain + 1 where book_id = " + int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                string query_text_find_user_record = "select * from record where username = '"+user_name+"' and book_id = "+ int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString())+ " and return_time is null";

                NpgsqlConnection conn = new NpgsqlConnection(sign_in.connstr);
                NpgsqlCommand cmd = new NpgsqlCommand(query_text_sub_number, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                //2.然后增加一条用户的借书记录
                //如果该用户已经借过这本书还没有还，就不能再借
                cmd.CommandText = query_text_find_user_record;
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    //这里该用户已经借过这本书了，不让用户再次借书，然后把已经减掉的书的数量加回来
                    MessageBox.Show("您已经借过此书，请不要重复借同一本书", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    reader.Close();
                    cmd.CommandText = query_text_add_number;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return;
                }
                reader.Close();
                //现在可以借出
                string query_text_add_record = "insert into record values('"+user_name+"',"+int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString())+",to_date('"+ DateTime.Now.ToString("yyyy-MM-dd")+"','yyyy-mm-dd'),null)";
                cmd.CommandText = query_text_add_record;
                cmd.ExecuteNonQuery();
                
                conn.Close();
                MessageBox.Show("借书成功", "恭喜", MessageBoxButtons.OK);
                borrow_Load(null,null);


            }
            else
            {
                return;
            }
        }
    }
}
