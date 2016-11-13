using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;

public partial class Email_ToFindPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string activeCode = Request["activecode"].ToString();
        string username = Request["user"].ToString();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int i = 0;
        string userNameStr = Request["user"].ToString();
        string[] s = new string[7];
        s[0] = "密码更新失败，请与管理员联系！";
        s[1] = "密码更新成功！";
        s[2] = "旧密码和新密码的长度不能短于2个字符且不超过20个字符！";
        s[3] = "新密码只能由中文、英文字母、数字及下划线_组成！";
        s[4] = "旧密码错误！";
        s[5] = "新密码两次输入不一致！";
        s[6] = "密码和确认密码均不能为空！";
        string pw2 = Password2.Text;
        string pw3 = Password3.Text;
        if (String.IsNullOrEmpty(pw2) || String.IsNullOrEmpty(pw3))
        {
            i = 6;
        }
        else
        {
            pw2 = pw2.Trim();
            pw3 = pw3.Trim();
            if (pw2.Length < 3 || pw2.Length > 21 || pw3.Length < 3 || pw3.Length > 21)
            {
                i = 2;
            }
            else
            {
                if (pw2.Equals(pw3))
                {
                    i = DoUpdate();

                }
                else
                {
                    i = 5;
                }
            }

            Label2.Text = s[i];
        }
    }

    protected int DoUpdate()
    {
        int i = 0;
        string userNameStr = Request["user"].ToString();
        using (SqlConnection conn = new DB().GetConnection())
        {
            string sql = "Update [Users] set Password = @Password where UserName = @UserName";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Password", Util.GetHash(Password2.Text.Trim()));
            cmd.Parameters.AddWithValue("@UserName", userNameStr);
            conn.Open();
            i = cmd.ExecuteNonQuery();
            Label2.Text = "修改成功";
            conn.Close();

        }
        return i;
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked)
        {

            Password2.TextMode = TextBoxMode.SingleLine;
            Password3.TextMode = TextBoxMode.SingleLine;
        }
        else
        {

            Password2.TextMode = TextBoxMode.Password;
            Password3.TextMode = TextBoxMode.Password;
        }
    }
}