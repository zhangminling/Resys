using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;

public partial class User_Password : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["RoleID"] == null || Session["UserID"] == null)
            {
                Util.ShowMessage("用户登录超时，请重新登录！", "Login.aspx");
            }
            else
            {
                ErrorLabel.Text = "";
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        int i = 0;
        string userid = Convert.ToString(Session["UserID"]);
        string username = Convert.ToString(Session["UserName"]);
        string ip = Request.UserHostAddress;//IP地址
        string[] s = new string[7];
        s[0] = "密码更新失败，请与管理员联系！";
        s[1] = "密码更新成功！"; 
        s[2] = "旧密码和新密码的长度不能短于2个字符且不超过20个字符！";
        s[3] = "新密码只能由中文、英文字母、数字及下划线_组成！";
        s[4] = "旧密码错误！";
        s[5] = "新密码两次输入不一致！";
        s[6] = "旧密码和新密码均不能为空！";      
        string pw1 = Password1.Text;
        string pw2 = Password2.Text;
        string pw3 = Password3.Text;
        if (String.IsNullOrEmpty(pw1) || String.IsNullOrEmpty(pw2) || String.IsNullOrEmpty(pw3))
        {
            i = 6;
        }
        else
        {
            pw1 = pw1.Trim();
            pw2 = pw2.Trim();
            pw3 = pw3.Trim();
            if (pw1.Length < 3 || pw1.Length > 21 || pw2.Length < 3 || pw2.Length > 21 || pw3.Length < 3 || pw3.Length > 21)
            {
                i = 2;
            }
            else
            {
                if(pw2.Equals(pw3))
                {
                    i = DoUpdate();
                    string newTagName = "用户名为“" + username + "”的密码";
                    Util.UserUtil_Notes("更新了Id为", Convert.ToInt32(userid), newTagName, username, "User_Password.aspx", ip);
               

                }
                else
                {
                    i = 5;
                }
            }
         }
        
        ErrorLabel.Text = s[i];
        
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked)
        {       
            Password1.TextMode = TextBoxMode.SingleLine;
            Password2.TextMode = TextBoxMode.SingleLine;
            Password3.TextMode = TextBoxMode.SingleLine;
        }
        else
        {
            Password1.TextMode = TextBoxMode.Password;
            Password2.TextMode = TextBoxMode.Password;
            Password3.TextMode = TextBoxMode.Password;
        }
    }

    protected int DoUpdate()
    {
        int i = 0;
        string oldPassword = "";
        using (SqlConnection conn = new DB().GetConnection())
        {
            string sql = "select Password from [Users] where ID = @UserID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                oldPassword = rd["Password"].ToString();
            }
            rd.Close();

            if (oldPassword.Equals(Util.GetHash(Password1.Text.Trim())))
            {
                cmd.CommandText = "Update [Users] set Password = @Password where ID = @UserID2";
                cmd.Parameters.AddWithValue("@Password", Util.GetHash(Password2.Text.Trim()));
                cmd.Parameters.AddWithValue("@UserID2", Session["UserID"].ToString());
                i = cmd.ExecuteNonQuery();
                ErrorLabel.Text = "修改成功";
              
            }
            else
            {
                i = 4;//第四种情况，旧密码错误。
            }
            conn.Close();
        }
        return i;
    }
}
