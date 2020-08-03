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
using System.Security.Cryptography;


namespace 图书管理系统V0._0._1
{
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (char x in textBox1.Text)
            {
                if (x == '-' || x == '\'')
                {
                    MessageBox.Show("用户名包含非法字符-,'等请重新输入", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (String.Equals(textBox1.Text, "") || String.Equals(textBox2.Text, ""))
            {
                MessageBox.Show("请完善登录信息", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(String.Equals(textBox1.Text,sign_in.manager_name))
            {
                MessageBox.Show("您不能注册管理员的账号", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //1.首先判断数据库里是否已经存在这个用户
            string user_name = sign_in.sign_name;
            string user_pass = sign_in.sign_password;
            string user_pass_temp = textBox2.Text;

            string query_text_find_user = "select * from user_info where username = \'" + textBox1.Text + "\'";
            NpgsqlConnection conn = new NpgsqlConnection(sign_in.connstr);
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query_text_find_user, conn);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            //2.存在报错
            if (reader.HasRows)
            {
                MessageBox.Show("该用户名已存在，请重新输入", "请重试", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                reader.Close();
                return;
            }
            reader.Close();
            //3.不存在插入
            using (MD5 mi = MD5.Create())
            {
                //开始加密
                byte[] newBuffer = mi.ComputeHash(System.Text.Encoding.Unicode.GetBytes(textBox2.Text));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                textBox2.Text = sb.ToString();
            }

            string query_text_insert_user = "insert into user_info(username,name,sex,balance) values(\'" + textBox1.Text + "\',\'" + textBox3.Text + "\',\'" + comboBox1.SelectedItem.ToString() + "\',0)";
            string query_text_insert_pass = "insert into user_pass values(\'" + textBox1.Text + "\',\'" + textBox2.Text + "\')";

            cmd.CommandText = query_text_insert_user;
            cmd.ExecuteNonQuery();
            cmd.CommandText = query_text_insert_pass;
            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("注册成功！", "恭喜", MessageBoxButtons.OK);

            //为登录界面填入用户名和密码
            Program.sign.username.Text = textBox1.Text;
            Program.sign.password.Text = user_pass_temp;
            this.Hide();
            this.Dispose();
        }

        private void register_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            comboBox1.SelectedIndex = comboBox1.Items.IndexOf("男");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
