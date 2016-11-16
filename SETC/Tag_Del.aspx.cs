using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Tag_Delete : System.Web.UI.Page
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
                Util.ShowMessage("您没有访问该页面的权限！", "Login2.aspx");
            }
            else if (Request.QueryString["IDS"] != null)
            {
                IDSLabel.Text = Request.QueryString["IDS"].ToString();
                MyInit();
            }
           
        }
    }


    private void MyInit()
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from UserTags where ID in (" + IDSLabel.Text + ") order by ID desc";
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            GridView1.DataSource = rd;
            GridView1.DataBind();
            rd.Close();
            conn.Close();
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("Tags_Man.aspx");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int i = 0;

        //string sql = "select UserID from Users_UserTags where UserTagID in (" + IDSLabel.Text + ") ";
        //string sql2 = "SELECT count(UserID) from Users_UserTags where UserTagID in (" + IDSLabel.Text + ")";
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Delete from UserTags where ID in (" + IDSLabel.Text + ") ";
            SqlCommand cmd1 = conn.CreateCommand();
            cmd1.CommandText = "Delete from Users_UserTags where UserTagID in (" + IDSLabel.Text + ") ";

            conn.Open();
            cmd1.ExecuteNonQuery();
            i = cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd1.Dispose();

            cmd.CommandText = "select * from  UserTags where ID in (" + IDSLabel.Text + ") order by ID desc";
            SqlDataReader rd = cmd.ExecuteReader();
            GridView1.DataSource = rd;
            GridView1.DataBind();
            rd.Close();
            conn.Close();
        }
        if (i > 0)
        {
            ResultLabel.Text = "成功删除" + i + "个标签！";
            ResultLabel.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            ResultLabel.Text = "操作失败，请重试！";
            ResultLabel.ForeColor = System.Drawing.Color.Red;
        }
    }
}