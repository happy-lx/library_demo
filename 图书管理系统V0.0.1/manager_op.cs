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
    public partial class manager_op : Form
    {
        public static borrow_manage borrow;
        public manager_op()
        {
            InitializeComponent();
        }

        private void manager_op_Load(object sender, EventArgs e)
        {
            this.pictureBox1.Image = Image.FromFile("../../../img/7.jpg");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            book_manage book = new book_manage();
            book.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            borrow = new borrow_manage();
            borrow.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            user_manage user = new user_manage();
            user.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
