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
using System.Text.RegularExpressions;

namespace 图书管理系统V0._0._1
{
    public partial class balance : Form
    {
        public balance()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox2.Text = "10";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox2.Text = "20";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.textBox2.Text = "50";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.textBox2.Text = "100";
        }

        private void balance_Load(object sender, EventArgs e)
        {
            //显示账户余额
            string user_name = sign_in.sign_name;
            string user_pass = sign_in.sign_password;

            string query_text_find_balance = "select balance from user_info where username = \'" + user_name + "\'";
            NpgsqlConnection conn = new NpgsqlConnection(sign_in.connstr);
            NpgsqlCommand cmd = new NpgsqlCommand(query_text_find_balance, conn);
            conn.Open();
            NpgsqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            textBox1.Text = reader.GetDouble(0).ToString();
            conn.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (label3.Visible)
            {
                MessageBox.Show("请输入有效的充值金额", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //直接将textbox2的值加到用户的balance里面
            string user_name = sign_in.sign_name;
            string user_pass = sign_in.sign_password;
            string query_text_add_balance = "update user_info set balance = balance + " + Double.Parse(textBox2.Text) + " where username = \'" + user_name + "\'";
            string query_text_find_balance = "select balance from user_info where username = \'" + user_name + "\'";
            NpgsqlConnection conn = new NpgsqlConnection(sign_in.connstr);
            NpgsqlCommand cmd = new NpgsqlCommand(query_text_add_balance, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("充值成功", "恭喜", MessageBoxButtons.OK);
            cmd.CommandText = query_text_find_balance;
            NpgsqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            textBox1.Text = reader.GetDouble(0).ToString();
            conn.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string text = textBox2.Text;
            string patten = @"^\d+$";

            MatchCollection mc = Regex.Matches(text, patten);
            if (mc.Count == 0)
            {
                label3.Visible = true;
            }
            else
            {
                label3.Visible = false;
            }
        }
    }
}
