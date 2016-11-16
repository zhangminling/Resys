using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class User_Del : System.Web.UI.Page
{
    int t;
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
                Util.ShowMessage("您没有访问该页面的权限！", "Login2.aspx");
            }
            else
            {
                if (Request.QueryString["IDS"] != null)
                {
                    IDSLabel.Text = Request.QueryString["IDS"].ToString();
                    string[] array = IDSLabel.Text.Split(',');
                    t = array.Length;
                    UserIP.Text = Request.UserHostAddress;//IP地址
                    UserName.Text = Convert.ToString(Session["UserName"]);
                    MyInit();
                }
            }
        }
    }

    private void MyInit()
    {
        string s = "";
        string[] UserID = new string[t];
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from Users where ID in (" + IDSLabel.Text + ") order by ID desc";
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            GridView1.DataSource = rd;
            GridView1.DataBind(); 
            rd.Close();

            cmd.CommandText = "select UserName from Users where ID in (" + IDSLabel.Text + ") order by ID desc";
            rd = cmd.ExecuteReader();
            for (int j = 0; j < t; j++)
            {
                if (rd.Read())
                {
                    UserID[j] = rd["UserName"].ToString();
                    for (int k = 0; k < t; k++)
                    {
                        s = string.Join(",", UserID);
                        DeleteUserName.Text = s;
                    }
                }
            }
            rd.Close();
            conn.Close();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
          int i = 0;
          using (SqlConnection conn = new DB().GetConnection()) 
          {
              SqlCommand cmd = conn.CreateCommand();
              cmd.CommandText = "Delete from Users where ID in (" + IDSLabel.Text + ") ";
              SqlCommand cmd1 = conn.CreateCommand();
              cmd1.CommandText = "Delete from Users_UserTags where UserID in (" + IDSLabel.Text + ") ";
              conn.Open();
              i = cmd.ExecuteNonQuery();
              cmd1.ExecuteNonQuery();
              cmd.Dispose();
              cmd1.Dispose();

              cmd.CommandText = "select * from Users where ID in (" + IDSLabel.Text + ") order by ID desc";
              SqlDataReader rd = cmd.ExecuteReader();
              GridView1.DataSource = rd;
              GridView1.DataBind();
              rd.Close();
              conn.Close();
          }
          if (i > 0)
          {
              string deleteUserName = "个用户，分别为“" + DeleteUserName.Text + " ”" ;
              Util.UserUtil_Notes("成功删除了", i, deleteUserName, UserName.Text, "User_Del.aspx", UserIP.Text);
              ResultLabel.Text = "成功删除" + i + "个用户！";
              ResultLabel.ForeColor = System.Drawing.Color.Green;
          }
          else
          {
              ResultLabel.Text = "操作失败，请重试！";
              ResultLabel.ForeColor = System.Drawing.Color.Red;
          }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("User_Manage.aspx");
    }
}