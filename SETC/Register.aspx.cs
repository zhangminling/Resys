using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;

public partial class Register : System.Web.UI.Page
{
    //chenxilian :定义UserId,默认值为0
    string UserID = "0";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            UserName.Focus();
        }
    }
    private string Check()
    {
        string userNameStr = UserName.Text;
        string passwordStr = Password.Text;
        string email = Email.Text;
        int i=0;
        string [] s = new string[6];
        s[0] = "";
        s[1] = "用户名和密码不能为空！";
        s[2] = "用户名和密码的长度不能短于2个字符且不超过20个字符！";
        s[3] = "用户名和密码只能由中文、英文字母、数字及下划线_组成！";
        s[4] = "用户名已经存在，请重新输入！";
        s[5] = "用户注册失败，系统原因，请与管理员联系！";
       

        if ((!String.IsNullOrEmpty(userNameStr)) && (!String.IsNullOrEmpty(passwordStr)))
        {
            if (userNameStr.Trim().Length > 1 && userNameStr.Trim().Length < 21 && passwordStr.Trim().Length > 2 && passwordStr.Trim().Length < 21)
            {
                if (AlreadyExisted(userNameStr.Trim()))
                {
                    i = 4;//第五种情况，用户名已经存在
                }
                else
                {
                    string activecode = Guid.NewGuid().ToString().Substring(0, 8);//生成激活码 
                    int j = DoRegist(userNameStr.Trim(), passwordStr.Trim(), activecode);
                    if (j == 1)
                    {
                        //string activeCodeLock = Util.GetHash(activecode);  //随机码Guid加密
                        //sendMail(Email.Text, activecode);//给注册用户发邮件 
                        if (SendMail(Email.Text, activecode))
                        {
                            ErrorLabel.Text = "邮件发送成功！";
                            ErrorLabel.Visible = true;
                        }
                        Util.DoLogin(userNameStr.Trim(), passwordStr.Trim());
                        //Util.ShowMessage("恭喜注册成功！", "User_Edit2.aspx");
                    }
                    else
                    {
                        i = 5;//第六种情况，系统原因，用户注册失败
                    }
                }
            }
            else
            {
                i = 2; //第三种情况
            }
        }
        else
        {
            i = 1;//第二种情况，用户名密码为空
        }

        return s[i];
    }

    protected bool SendMail(string email,string activecode)
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
        Message.Subject = "教育技术与传播学院网站——激活账号邮件";

        //HTML格式邮件的内容
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("亲爱的用户 " + UserName.Text + "：您好！<br /><br />");
        sb.AppendLine("           感谢您注册教育技术与传播学院的账号，您只需要点击下面链接，激活您的帐户，您便可以享受我院的各项服务<br /><br />");
        sb.AppendLine("<a href=\"http://" + HttpContext.Current.Request.Url.Authority + "/activePage.aspx?user=" + UserName.Text + "&activecode=" + activecode + "\">");
        sb.AppendLine("http://" + HttpContext.Current.Request.Url.Authority + "/activePage.aspx?user=" + UserName.Text + "&activecode=" + activecode + "</a>");
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



    private int DoRegist(string u,string p,string activecode)
    {
        int i = 0;
        //int r = new Random().Next(10);
        //string avatar = @"images/avatars/" + r + ".png";
        using (SqlConnection conn = new DB().GetConnection())
        {
            StringBuilder sb = new StringBuilder("insert into [Users]( UserName,Password,Email,Avatar,Status,RoleID,RoleName,Grade,Shows,Articles,Credits,RegisterDateTime,LastLoginDateTime,Valid,RandomID,CongealDate )");
            sb.Append(" values ( @UserName,@Password,@Email,@Avatar,@Status,@RoleID,@RoleName,@Grade,@Shows,@Articles,@Credits,@RegisterDateTime,@LastLoginDateTime,@Valid,@RandomID,@CongealDate  ) ");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.AddWithValue("@UserName", u);
            cmd.Parameters.AddWithValue("@Password", Util.GetHash(p));//密码加密
            cmd.Parameters.AddWithValue("@Email", Email.Text);
            cmd.Parameters.AddWithValue("@Avatar", "Users/Avatars/20160908201834-270.png");
            cmd.Parameters.AddWithValue("@Status", "");
            cmd.Parameters.AddWithValue("@RoleID", 4); // 角色中，RoleID为4
            cmd.Parameters.AddWithValue("@RoleName", "User"); // 角色，RoleName为Author，即成员
            cmd.Parameters.AddWithValue("@Grade", "幼儿园");
            cmd.Parameters.AddWithValue("@Shows", 0);
            cmd.Parameters.AddWithValue("@Articles", 0);
            cmd.Parameters.AddWithValue("@Credits", 0);
            cmd.Parameters.AddWithValue("@RegisterDateTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@LastLoginDateTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@Valid", 0);
            cmd.Parameters.AddWithValue("@RandomID", activecode);
            cmd.Parameters.AddWithValue("@CongealDate", "2016-09-13 12:09:01.673");
            conn.Open();
            i = cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
        return i;
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        if(CheckBox1.Checked ==true)
        {
           ErrorLabel.Text = Check();
        }      
        else
        {
          ErrorLabel.Text ="未同意条款，注册失败！";
        }         
    }   
}
