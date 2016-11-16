using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Collections;

public partial class Tag_Edit : System.Web.UI.Page
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
            UserName.Text = Convert.ToString(Session["UserName"]);
            UserIP.Text = Request.UserHostAddress;//IP地址
            if (Request.QueryString["ID"] != null)
            {
               Label1.Text = Request.QueryString["ID"].ToString();
                MyInitForUpdate();

            }
            }
        }
    }

  
    private void MyInitForUpdate()
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from UserTags where ID =@ID"  ;
            cmd.Parameters.AddWithValue("@ID", Label1.Text);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                TagName.Text = rd["TagName"].ToString();
                TagDescription.Text = rd["Description"].ToString();

            }
        }
    }

    private void DoUpdate()
    {
      using (SqlConnection conn = new DB().GetConnection())
        {
            StringBuilder sb = new StringBuilder("Update UserTags set TagName=@TagName,Description=@Description where ID=@ID");
            StringBuilder sb1 = new StringBuilder("Update Users_UserTags set TagName=@TagName where UserTagID=@UserTagID");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            SqlCommand cmd1 = new SqlCommand(sb1.ToString(), conn);
            cmd.Parameters.AddWithValue("@ID", Label1.Text);
            cmd.Parameters.AddWithValue("@TagName", TagName.Text);
            cmd.Parameters.AddWithValue("@Description", TagDescription.Text);
            cmd1.Parameters.AddWithValue("@TagName", TagName.Text);
            cmd1.Parameters.AddWithValue("@UserTagID", Label1.Text);
            conn.Open();
            cmd.ExecuteNonQuery();
            cmd1.ExecuteNonQuery();
            conn.Close();
            Response.Write("<script language='javascript'> alert('更新成功');</script>");


        }
      

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string newTagName = "《" + TagName.Text + "》的标签";
        Util.UserUtil_Notes("更新了Id为", Convert.ToInt32(Label1.Text), newTagName, UserName.Text,"Tag_Edit.aspx", UserIP.Text);
        DoUpdate();
    }
}