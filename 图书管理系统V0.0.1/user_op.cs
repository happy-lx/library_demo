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
    public partial class user_op : Form
    {
        public user_op()
        {
            InitializeComponent();
            this.pictureBox1.Image = Image.FromFile("../../../img/8.jpg");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            balance blc = new balance();
            blc.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            user_info info = new user_info();
            info.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            borrow_and_return brw = new borrow_and_return();
            brw.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            borrow_records records = new borrow_records();
            records.ShowDialog();
        }

        private void user_op_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
