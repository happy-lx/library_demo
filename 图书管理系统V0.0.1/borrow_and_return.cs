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
    public partial class borrow_and_return : Form
    {
        public static borrow brw;
        public borrow_and_return()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            brw = new borrow();
            brw.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            @return ret = new @return();
            ret.ShowDialog();
        }

        private void borrow_and_return_Load(object sender, EventArgs e)
        {

            this.pictureBox1.Image = Image.FromFile("../../../img/6.jpg");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
