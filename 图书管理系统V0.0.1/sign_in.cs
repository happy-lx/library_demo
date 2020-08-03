using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LitJson;
using System.IO;
using Npgsql;
using System.Security.Cryptography;

namespace 图书管理系统V0._0._1
{
    public partial class sign_in : Form
    {
        static string config_path = "../../../config/config.json";//配置文件所在的目录
        public static string manager_name;//管理员的账号名
        static string manager_password;//管理员的密码
        public static string sign_name;//实际登录的用户的账号
        public static string sign_password;//实际登录的用户的密码
        static string db_name;//数据库的名称
        static string db_user;//数据库使用者名字
        static string db_pass;//数据库使用者密码
        public static string connstr;//数据库连接字符串

        public sign_in()
        {
            InitializeComponent();
            this.pictureBox1.Image = Image.FromFile("../../../img/5.jpg");
            skinEngine1.SkinFile = Application.StartupPath + @"/Skins/MSN.ssk";
            skinEngine1.Active = true;
        }

        private void sign_in_Load(object sender, EventArgs e)
        {
            //读入配置文件
            JsonData config_data = JsonMapper.ToObject(File.ReadAllText(config_path));

            sign_in.manager_name = (string)config_data["manager_name"];
            sign_in.manager_password = (string)config_data["manager_password"];
            sign_in.db_name = (string)config_data["db_name"];
            sign_in.db_user = (string)config_data["db_user"];
            sign_in.db_pass = (string)config_data["db_pass"];

            //生成拼接字符串"Host=localhost;Username=*******;Password=*******;Database= *******"
            connstr = "Host=localhost;Username=" + db_user + ";Password=" + db_pass + ";Database=" + db_name;

            //密码改为不可见
            this.password.PasswordChar = '*';
        }

        private void sign_Click(object sender, EventArgs e)
        {
            foreach (char x in username.Text)
            {
                if (x == '-' || x == '\'')
                {
                    MessageBox.Show("用户名包含非法字符-,'等请重新输入", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (String.Equals(username.Text, "") || String.Equals(password.Text, ""))
            {
                MessageBox.Show("登录信息不完整", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            sign_in.sign_name = this.username.Text;
            sign_in.sign_password = this.password.Text;

            if (sign_in.sign_name == sign_in.manager_name && sign_in.sign_password == sign_in.manager_password)
            {
                manager_op form = new manager_op();
                form.ShowDialog();
                return;
            }
            else
            {
                using (MD5 mi = MD5.Create())
                {
                    //开始加密
                    byte[] newBuffer = mi.ComputeHash(System.Text.Encoding.Unicode.GetBytes(password.Text));
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < newBuffer.Length; i++)
                    {
                        sb.Append(newBuffer[i].ToString("x2"));
                    }
                    sign_in.sign_password = sb.ToString();
                }
            }
            //先检查系统中是否有这个用户
            string query_str_find_user = "select * from user_pass where username = \'" + sign_in.sign_name + "\'";
            NpgsqlConnection conn = new NpgsqlConnection(sign_in.connstr);
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query_str_find_user, conn);
            NpgsqlDataReader dreader = cmd.ExecuteReader();
            if (!dreader.HasRows)
            {
                MessageBox.Show("用户不存在，请先进行注册", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                dreader.Close();
                return;
            }
            dreader.Close();
            string query_str = "select * from user_pass where username = \'" + sign_in.sign_name + "\' and user_password = \'" + sign_in.sign_password + "\'";
            cmd.CommandText = query_str;
            dreader = cmd.ExecuteReader();

            if (dreader.HasRows)
            {

                user_op form = new user_op();
                form.ShowDialog();

            }
            else
            {
                MessageBox.Show("用户名或者密码错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            conn.Close();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void regist_Click(object sender, EventArgs e)
        {
            register rgr = new register();
            rgr.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {


        }
    }
}
