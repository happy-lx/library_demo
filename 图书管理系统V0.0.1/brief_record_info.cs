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
    public partial class brief_record_info : Form
    {
        public brief_record_info()
        {
            InitializeComponent();
        }

        private void brief_record_info_Load(object sender, EventArgs e)
        {
            textBox1.Text = manager_op.borrow.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = manager_op.borrow.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = manager_op.borrow.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = manager_op.borrow.dataGridView1.CurrentRow.Cells[3].Value.ToString().Split(' ')[0];
            textBox5.Text = manager_op.borrow.dataGridView1.CurrentRow.Cells[4].Value.ToString().Split(' ')[0];
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
