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
    public partial class @return : Form
    {
        public @return()
        {
            InitializeComponent();
        }

        private void return_Load(object sender, EventArgs e)
        {
            string user_name = sign_in.sign_name;
            string user_pass = sign_in.sign_password;
            //把用户的所有未还书的借书记录显示出来
            string query_text_all_records =
            "select record.book_id,book_name,borrow_time,return_time from record,book_info where record.book_id = book_info.book_id and record.username = '" + user_name + "' and return_time is null";

            NpgsqlConnection conn = new NpgsqlConnection(sign_in.connstr);
            DataSet dataset = new DataSet();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query_text_all_records, conn);
            adapter.Fill(dataset, "借书记录");
            dataGridView1.DataSource = dataset.Tables["借书记录"];

            dataGridView1.Columns[0].HeaderText = "书籍序号";
            dataGridView1.Columns[1].HeaderText = "书籍名称";
            dataGridView1.Columns[2].HeaderText = "借出时间";
            dataGridView1.Columns[3].HeaderText = "返还时间";

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

        private void 还书ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (String.Equals(dataGridView1.CurrentRow.Cells[0].Value.ToString(), "") && String.Equals(dataGridView1.CurrentRow.Cells[1].Value.ToString(), "") && String.Equals(dataGridView1.CurrentRow.Cells[2].Value.ToString(), "") && String.Equals(dataGridView1.CurrentRow.Cells[3].Value.ToString(), ""))
            {
                MessageBox.Show("请不要点击空白区域", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string user_name = sign_in.sign_name;
            string user_pass = sign_in.sign_password;

            if(MessageBox.Show("确认要归还该书", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //1.首先计算借书和还书之间的时间间隔计算费用
                DateTime borrrow_time = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[2].Value.ToString());
                DateTime return_time = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                TimeSpan timeSpan = return_time - borrrow_time;
                int days = timeSpan.Days;
                double fee = days * 0.5;
                //2.在用户表余额中减去费用

                string query_text_sub_fee =
                    "update user_info set balance = balance - "+fee+" where username = '"+user_name+"';";

                NpgsqlConnection conn = new NpgsqlConnection(sign_in.connstr);
                NpgsqlCommand cmd = new NpgsqlCommand(query_text_sub_fee, conn);
                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }catch(Exception ex)
                {
                    MessageBox.Show("您的账户余额已经不足，请充值后再还书", "错误", MessageBoxButtons.OK);
                    conn.Close();
                    return;
                }
                //3.在借书记录表中填写归还的日期
                string query_text_return_book =
                "update record set return_time = to_date('"+ DateTime.Now.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') where username = '"+user_name+"' and book_id = "+int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()) + " and return_time is  null";

                cmd.CommandText = query_text_return_book;
                cmd.ExecuteNonQuery();

                //4.该书可用数量加一
                string query_text_add_number = "update book_num set remain = remain + 1 where book_id = " + int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                cmd.CommandText = query_text_add_number;
                cmd.ExecuteNonQuery();

                MessageBox.Show("还书成功", "恭喜", MessageBoxButtons.OK);
                conn.Close();
                return_Load(null,null);

            }
            else
            {
                return;
            }
        }
    }
}
