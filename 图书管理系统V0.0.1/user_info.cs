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
    public partial class user_info : Form
    {
        public user_info()
        {
            InitializeComponent();
        }

        private void user_info_Load(object sender, EventArgs e)
        {
            //直接把信息填入到textbox中
            this.label8.Visible = true;
            string user_name = sign_in.sign_name;
            string user_pass = sign_in.sign_password;
            string query_text_find_user = "select * from user_info where username = \'" + user_name + "\'";
            NpgsqlConnection conn = new NpgsqlConnection(sign_in.connstr);
            NpgsqlCommand cmd = new NpgsqlCommand(query_text_find_user, conn);
            conn.Open();
            NpgsqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            textBox1.Text = reader.GetString(0);
            textBox2.Text = reader.GetString(1);
            try
            {
                textBox3.Text = reader.GetString(2);
            }catch(Exception ex)
            {
                textBox3.Text = "";
            }
            try
            {
                textBox4.Text = reader.GetString(3);
            }catch(Exception ex)
            {
                textBox4.Text = "";
            }
            try
            {
                textBox5.Text = reader.GetString(4);
            }catch(Exception ex)
            {
                textBox5.Text = "";
            }
            if(reader.GetString(5) == "男")
            {
                comboBox1.SelectedIndex = comboBox1.Items.IndexOf("男");
            }else
            {
                comboBox1.SelectedIndex = comboBox1.Items.IndexOf("女");
            }
            try
            {
                richTextBox1.Text = reader.GetString(7);
            }catch(Exception ex)
            {
                richTextBox1.Text = "";
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(label8.Visible)
            {
                MessageBox.Show("请填写正确的个人信息", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string user_name = sign_in.sign_name;
            string user_pass = sign_in.sign_password;
            string query_text_update_user =
            "update user_info set name = '" + textBox2.Text + "' , mail = '" + textBox3.Text + "' , address = '" + textBox4.Text + "' , occupy = '" + textBox5.Text + "' , sex = '" + comboBox1.SelectedItem.ToString() + "' , self_info = '" + richTextBox1.Text + "' where username = '" + user_name + "'";
            NpgsqlConnection conn = new NpgsqlConnection(sign_in.connstr);
            NpgsqlCommand cmd = new NpgsqlCommand(query_text_update_user, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("修改成功", "恭喜", MessageBoxButtons.OK);
            conn.Close();

            //用户信息更新
            user_info_Load(null,null);

            this.Hide();
            this.Dispose();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string text = textBox3.Text;
            string patten = @"^([0-9]|[a-z]|[A-Z])*@.+?\..+$";
            MatchCollection mc = Regex.Matches(text, patten);
            if(mc.Count == 0)
            {
                this.label8.Visible = true;
            }else
            {
                this.label8.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
