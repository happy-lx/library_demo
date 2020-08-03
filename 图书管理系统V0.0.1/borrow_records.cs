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
    public partial class borrow_records : Form
    {
        public borrow_records()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void borrow_records_Load(object sender, EventArgs e)
        {
            string user_name = sign_in.sign_name;
            string user_pass = sign_in.sign_password;
            //把用户的所有借书记录显示出来
            string query_text_all_records =
            "select record.book_id,book_name,borrow_time,return_time from record,book_info where record.book_id = book_info.book_id and record.username = '"+user_name+"'";

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
    }
}
