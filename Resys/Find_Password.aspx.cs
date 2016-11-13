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
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;


public partial class Find_Password : System.Web.UI.Page
{
    static int i = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

     }
       
   


    protected void btnNext_Click(object sender, EventArgs e)
    {
        string userNameStr = txtName.Text;
        ErrorLabel.Text = Check();

        using (SqlConnection conn = new DB().GetConnection())
        {
            
           string sql = "select CongealDate from [Users] where UserName = @UserName2";
           SqlCommand cmd = new SqlCommand(sql, conn);
           cmd.Parameters.AddWithValue("@UserName2", txtName.Text);
           conn.Open();
           SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read()) 
                {
                    //获取冻结密码找回的日期
                    DateTime congeal = Convert.ToDateTime(rd["CongealDate"]);
                    //创建TimeSpan对象，该对象表示两个时间的间隔
                    TimeSpan ts = DateTime.Now - congeal;
                    //获取两个时间的间隔以小时表示
                    int hours = Convert.ToInt32(ts.TotalHours);
                    //判断时间是否大于个24，如果大于将显示回答密码提示问题区域
                    if (hours > 24)
                    {
                        PanelInputName.Visible = false;
                        WayToFind.Visible = true;
                    }
                    else
                    {
                 
                        Response.Write("<script language='javascript'> alert('还有" + (24 - hours) + "小时后可以使用该功能！！');</script>");
                    }
                }
           

     }

    }


    private string Check() 
    {
        int i = 0;
        string[] s = new string[3];
        s[0] = "用户名不存在，请重新输入！";
        s[1] = "用户名不能为空";
        s[2] = "";
        string userNameStr = txtName.Text;
        if (!String.IsNullOrEmpty(userNameStr))
        {
            if (AlreadyExisted(userNameStr.Trim())) 
            {
                i = 2;
              
            }
        }
        else 
        {
            i = 1;//第二种情况，用户名为空
        }

        return s[i];
    }

    private bool AlreadyExisted(string param)
    {
        bool a = false;

        using (SqlConnection conn = new DB().GetConnection())
        {
            string sql = "select id from [Users] where UserName = @UserName";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UserName", param);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                a = true;
            }
            rd.Close();
        }

        return a;
    }

    protected void WayToFindPsd_SelectedIndexChanged(object sender, EventArgs e)
    {

        ChooseWay();
    }

    private void ChooseWay() 
    {
       
        if (WayToFindPsd.SelectedIndex == 1) 
        {
            EmailPanel.Visible = true;
            WayToFind.Visible = false;

        }
        else if (WayToFindPsd.SelectedIndex == 2) 
        {
            using (SqlConnection conn = new DB().GetConnection())
            { 
            string sql = "select top 1 ID,Question1,Question2,Question3 from [UserQuestion_ForFindPassword] where UserName = @UserName ORDER by ID DESC";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UserName", txtName.Text);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {

                txtQuestion1.Text = rd["Question1"].ToString();
                txtQuestion2.Text = rd["Question2"].ToString();
                txtQuestion3.Text = rd["Question3"].ToString();


            }
            rd.Close();
            if (txtQuestion1.Text == "")
            {
                Response.Write("<script language='javascript'> alert('抱歉！您没有设置密保问题。');</script>");
            }
            else 
            {
                WayToFind.Visible = false;
                QuestionPanel1.Visible = true;
            }
            }
            
        }
    }

    protected void EmailToFind_Click(object sender, EventArgs e) 
    {
        //string EmailStr = Email.Text;
        EmailError.Text = CheckEmail();
       
    }

    protected bool SendMail(string email, string username , string activecode)
    {
        SmtpClient client = new SmtpClient();
        client.Host = "smtp.qq.com";
        client.Port = 25;
        //超时时间  
        client.EnableSsl = false;
        client.Timeout = 10000;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.UseDefaultCredentials = false;
        //身份验证
        client.Credentials = new NetworkCredential("1285718219@qq.com", "qukhzdtwonlejbjh");
        client.EnableSsl = true;
        MailMessage Message = new MailMessage();
        Message.From = new MailAddress("1285718219@qq.com", "发信人");
        Message.To.Add(email);
        Message.Subject = "教育技术与传播学院网站——找回密码邮件";

        //HTML格式邮件的内容
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("亲爱的用户 " +  username + "：您好！<br /><br />");
        sb.AppendLine("           感谢您使用教育技术与传播学院的账号，您可以通过点击以下链接找回您的密码<br /><br />");
        sb.AppendLine("<a href=\"http://" + HttpContext.Current.Request.Url.Authority + "/Email_ToFindPassword.aspx?user=" + username + "&activecode=" + activecode + "\">");
        sb.AppendLine("http://" + HttpContext.Current.Request.Url.Authority + "/Email_ToFindPassword.aspx?user=" + username + "&activecode=" + activecode + "</a>");
        sb.AppendLine("(如果无法点击该URL链接地址，请将它复制并粘帖到浏览器的地址输入框，然后单击回车即可<br /><br />");
        sb.AppendLine("注意:请您在收到邮件30天内(" + 30 + "前)进行激活，否则该激活码将会失效。 请您在90天内进行帐户激活，否则您的帐户将会失效。");

        string str = "";
        str = sb.ToString();
        string regexstr = @"<[^>]*>";
        str = Regex.Replace(str, regexstr, string.Empty, RegexOptions.IgnoreCase);
        Message.Body = str;
        Message.SubjectEncoding = System.Text.Encoding.UTF8;
        Message.BodyEncoding = System.Text.Encoding.UTF8;
        Message.IsBodyHtml = false;
        Message.Priority = System.Net.Mail.MailPriority.High;
        bool ret = true;
        try
        {
            client.Send(Message);
        }
        catch (SmtpException ex)
        {
            ErrorLabel.Text = ex.Message;
            ErrorLabel.Visible = true;
            ret = false;
        }
        catch (Exception ex2)
        {
            ErrorLabel.Text = ex2.Message;
            ErrorLabel.Visible = true;
            ret = false;
        }
        return ret;
    }


    protected void EmailToReturn_Click(object sender, EventArgs e) 
    {
        WayToFind.Visible = true;
        EmailPanel.Visible = false;
    }

    protected void WayToFindReturn_Click(object sender, EventArgs e)
    {
        WayToFind.Visible = false;
        PanelInputName.Visible = true;
    } 

    private bool EmailAlreadyExisted(string param,string Email)
    {
        bool a = false;

        using (SqlConnection conn = new DB().GetConnection())
        {
            string sql = "select Email,RandomID from [Users] where UserName = @UserName";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UserName", param);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                ActiveCode.Text = rd["RandomID"].ToString();
                if (Email.Equals(rd["Email"].ToString())) 
                {
                    a = true;
                    if (SendMail(Email, txtName.Text.Trim(), ActiveCode.Text))
                    {
                        EmailError.Text = "邮件发送成功！";
                        EmailError.Visible = true;
                    }
                  
                }
               
            }
            rd.Close();
        }

        return a;

    }

    private string CheckEmail()
    {
        int i = 0;
        string[] s = new string[3];
        s[0] = "邮箱输入错误，请重新输入！";
        s[1] = "邮箱号不能为空";
        s[2] = "";
        string EmailStr = Email.Text;
        if (!String.IsNullOrEmpty(EmailStr))
        {
            if (EmailAlreadyExisted(txtName.Text.Trim(), EmailStr.Trim()))
            {
                i = 2;

            }
        }
        else
        {
            i = 1;//第二种情况，用户名为空
        }

        return s[i];
    }


    protected void btnGet_Click(object sender, EventArgs e)
    {
        i++;
        string userNameStr = txtName.Text;
        string answer1 = "";
        string answer2 = "";
        string answer3 = "";
        using (SqlConnection conn = new DB().GetConnection()) 
        {
            string sql = "select top 1 ID,Answer1,Answer2,Answer3 from [UserQuestion_ForFindPassword] where UserName = @UserName ORDER by ID DESC";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UserName", txtName.Text);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
              answer1 = rd["Answer1"].ToString();
              answer2 = rd["Answer2"].ToString();
              answer3 = rd["Answer3"].ToString();
            }
            rd.Close();
            if (txtAnswer1.Text == answer1 && txtAnswer2.Text == answer2 && txtAnswer3.Text == answer3)
            {

                PanelGetPass.Visible = false;
                ResetPassword.Visible = true;
            }
            else
            {

                Label1.Text = "答案不正确";
                if (i < 5)
                {
                    Response.Write("<script language='javascript'> alert('答案不正确,你还有" + (5 - i) + "次机会');</script>");
                }
                else
                {
                    i = 0;
                    cmd.CommandText = "update Users set CongealDate = @CongealDate where UserName = @UserName2 ";
                    cmd.Parameters.AddWithValue("@UserName2", txtName.Text);
                    cmd.Parameters.AddWithValue("@CongealDate", DateTime.Now.ToString());
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    Response.Write("<script language='javascript'> alert('答案不正确,您的5次机会已经用完，在24小时以后才能使用此功能找回密码');</script>");
                    PanelInputName.Visible = true;
                    PanelGetPass.Visible = false;

                }
            }
            conn.Close();
        
        }
     
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int i = 0;
        string userNameStr = txtName.Text;
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

    protected int DoUpdate()
    {
        int i = 0;
        string userNameStr = txtName.Text;
        using (SqlConnection conn = new DB().GetConnection())
        {
            string sql = "Update [Users] set Password = @Password where UserName = @UserName";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Password", Util.GetHash(Password2.Text.Trim()));
            cmd.Parameters.AddWithValue("@UserName", txtName.Text);
            conn.Open();
            i = cmd.ExecuteNonQuery();
            Label2.Text = "修改成功";
            conn.Close();

         }
        return i;
      }

    protected void Button3_Click(object sender, EventArgs e)
    {
        QuestionPanel1.Visible = false;
        QuestionPanel2.Visible = true;
    }
    
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        QuestionPanel2.Visible = true;
        PanelGetPass.Visible = false;
    }
   
    protected void Button5_Click(object sender, EventArgs e)
    {
        PanelGetPass.Visible = true;
        QuestionPanel2.Visible = false;
    }
   
    protected void Button4_Click(object sender, EventArgs e)
    {
        QuestionPanel1.Visible = true;
        QuestionPanel2.Visible = false;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        QuestionPanel1.Visible = false;
        WayToFind.Visible = true;
    }
}