using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 图书管理系统V0._0._1
{
    public partial class book_brief_info : Form
    {
        public book_brief_info()
        {
            InitializeComponent();
        }

        private void book_brief_info_Load(object sender, EventArgs e)
        {
            textBox1.Text = borrow_and_return.brw.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = borrow_and_return.brw.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = borrow_and_return.brw.dataGridView1.CurrentRow.Cells[3].Value.ToString();
            richTextBox1.Text = borrow_and_return.brw.dataGridView1.CurrentRow.Cells[4].Value.ToString();

        }
    }
}
