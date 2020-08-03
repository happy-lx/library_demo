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
    public partial class user_manage : Form
    {
        public user_manage()
        {
            InitializeComponent();
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

        private void user_manage_Load(object sender, EventArgs e)
        {
            string user_name = sign_in.sign_name;
            string user_pass = sign_in.sign_password;
            string query_text_find_user =
            "select user_pass.username , name , mail , address , occupy , sex , balance , self_info from user_pass,user_info where user_pass.username = user_info.username";

            DataSet data = new DataSet();
            NpgsqlConnection conn = new NpgsqlConnection(sign_in.connstr);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query_text_find_user, conn);
            adapter.Fill(data, "用户");
            dataGridView1.DataSource = data.Tables["用户"];
            dataGridView1.Columns[0].HeaderText = "用户名";
            dataGridView1.Columns[1].HeaderText = "姓名";
            dataGridView1.Columns[2].HeaderText = "邮箱";
            dataGridView1.Columns[3].HeaderText = "地址";
            dataGridView1.Columns[4].HeaderText = "职业";
            dataGridView1.Columns[5].HeaderText = "性别";
            dataGridView1.Columns[6].HeaderText = "账户余额";
            dataGridView1.Columns[7].HeaderText = "个人简介";
            conn.Close();
        }

        private void 注销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (String.Equals(dataGridView1.CurrentRow.Cells[0].Value.ToString(), "") && String.Equals(dataGridView1.CurrentRow.Cells[1].Value.ToString(), "") && String.Equals(dataGridView1.CurrentRow.Cells[2].Value.ToString(), "") && String.Equals(dataGridView1.CurrentRow.Cells[3].Value.ToString(), "") && String.Equals(dataGridView1.CurrentRow.Cells[4].Value.ToString(), "") && String.Equals(dataGridView1.CurrentRow.Cells[5].Value.ToString(), "") && String.Equals(dataGridView1.CurrentRow.Cells[6].Value.ToString(), "") && String.Equals(dataGridView1.CurrentRow.Cells[7].Value.ToString(), ""))
            {
                MessageBox.Show("不可对空用户进行注销", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string user_name = sign_in.sign_name;
            string user_pass = sign_in.sign_password;
            string query_text_delete_user =
            "delete from user_info where username = '" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";

            if (MessageBox.Show("确定要注销这个用户吗？", "请确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                NpgsqlConnection conn = new NpgsqlConnection(sign_in.connstr);
                NpgsqlCommand cmd = new NpgsqlCommand(query_text_delete_user, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("注销成功", "恭喜", MessageBoxButtons.OK);
                user_manage_Load(null, null);
            }
            else
            {
                return;
            }
        }
    }
}
