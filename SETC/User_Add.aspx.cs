using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Configuration;

public partial class User_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            int RoleID = Convert.ToInt16(Session["RoleID"].ToString());

            if (Session["RoleID"] == null || Session["UserID"] == null)
            {
                Util.ShowMessage("用户登录超时，请重新登录！", "Login.aspx");
            }
            else if (RoleID > 1)
            {
                Util.ShowMessage("您没有访问该页面的权限！", "Login.aspx");
            }
          else 
          {
          UserName.Focus();
          using (SqlConnection conn = new DB().GetConnection())
          {
            string sql1 = "select * from Roles order by ID asc";
            SqlCommand cmd1 = new SqlCommand(sql1, conn);
            conn.Open();
            SqlDataReader rd1 = cmd1.ExecuteReader();
            Role.DataSource = rd1;
            Role.DataTextField = "RoleName";
            Role.DataValueField = "ID";
            Role.DataBind();
            rd1.Close();
          }
            }
      
        }
    }


    private void AddNewUser() 
    {

        string username = Convert.ToString(Session["UserName"]);
        string IP = Request.UserHostAddress;//IP地址
        ErrorLabel.Text = "";
        int i = 0;
        string p = Password.Text.Trim();
        using (SqlConnection conn = new DB().GetConnection()) 
        {
            StringBuilder sb = new StringBuilder("insert into [Users]( UserName,TrueName,Password,Email,Avatar,TelePhone,Status,RoleID,RoleName,RegisterDateTime,Valid,RandomID,CongealDate)");
            sb.Append(" values ( @UserName,@TrueName,@Password,@Email,@Avatar,@TelePhone,@Status,@RoleID,@RoleName,@RegisterDateTime,@Valid,@RandomID,@CongealDate ) ");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.AddWithValue("@UserName", UserName.Text.Trim());
            cmd.Parameters.AddWithValue("@TrueName", TrueName.Text.Trim());
            cmd.Parameters.AddWithValue("@Password", Util.GetHash(p));
            cmd.Parameters.AddWithValue("@Email", Email.Text.Trim());
            cmd.Parameters.AddWithValue("@Avatar","Users/Avatars/20160908201834-270.png" );
            cmd.Parameters.AddWithValue("@TelePhone", TelePhone.Text.Trim());
            cmd.Parameters.AddWithValue("@Status", Status.Text);
            cmd.Parameters.AddWithValue("@RoleID", Role.SelectedValue);
            cmd.Parameters.AddWithValue("@RoleName", Role.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@RegisterDateTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@Valid", 1);
            RandomID.Text = Guid.NewGuid().ToString();
            cmd.Parameters.AddWithValue("@RandomID", RandomID.Text);
            cmd.Parameters.AddWithValue("@CongealDate", "2016-09-13 12:09:01.673");
            conn.Open();
            i=cmd.ExecuteNonQuery();
            cmd.Dispose();

         
        }
        if (i == 1)
        {
            string addUserName = "个用户名为“" + UserName.Text.Trim() + "”的用户";
            Util.UserUtil_Notes("成功增加了", 1, addUserName, username, "User_Add.aspx", IP);
            ErrorLabel.Text = "用户信息更新成功！";
            Response.Redirect("User_Edit2.aspx?RandomID=" + RandomID.Text);
        }
        else
        {
            ErrorLabel.Text = "用户信息更新失败，请重试！";
        }
    }

    private string Check()
    {
        int i = 0;
        string[] s = new string[6];
        s[0] = "";
        s[1] = "标签名不能为空！";
        s[4] = "标签名已经存在，请输入另外一个标签名！";

        string TagNameStr = UserName.Text;
        if (!String.IsNullOrEmpty(TagNameStr))
        {
            if (AlreadyExisted(TagNameStr.Trim()))
            {
                i = 4;//第五种情况，用户名已经存在
            }
            else
            {
                DataBind();

            }

        }
        else
        {
            i = 1;//第二种情况，用户名密码为空
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

    protected void Button1_Click(object sender, EventArgs e)
    {

        Label2.Text = Check();
        if (Label2.Text == "")
        {
            AddNewUser();
        }
      
    }

 
}