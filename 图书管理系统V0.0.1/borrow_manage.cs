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
    public partial class borrow_manage : Form
    {
        public borrow_manage()
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

        private void borrow_manage_Load(object sender, EventArgs e)
        {
            string query_text_find_records =
            "select username,book_name,record.book_id,borrow_time,return_time from record,book_info where record.book_id = book_info.book_id";

            DataSet data = new DataSet();
            NpgsqlConnection conn = new NpgsqlConnection(sign_in.connstr);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query_text_find_records, conn);
            adapter.Fill(data, "记录");
            dataGridView1.DataSource = data.Tables["记录"];
            dataGridView1.Columns[0].HeaderText = "用户名";
            dataGridView1.Columns[1].HeaderText = "书名";
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            conn.Close();
        }

        private void 查看详情ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            brief_record_info record = new brief_record_info();
            record.ShowDialog();
        }
    }
}
