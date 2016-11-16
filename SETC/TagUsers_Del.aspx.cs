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

public partial class TagUsers_Del : System.Web.UI.Page
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
             
            IDSLabel.Text = Request.QueryString["ID"].ToString();
            if (Request.QueryString["ID"] != null)
            {
             using (SqlConnection conn = new DB().GetConnection())
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "select * from [UserTags] where ID= @TagID";
                    cmd.Parameters.AddWithValue("@TagID", IDSLabel.Text);
                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        TagName.Text = rd["TagName"].ToString();
                    }
                    rd.Close();

                    
                    cmd.CommandText = "select count(*) as maxrow from [Users_UserTags] where UserTagID= @TagID1";
                    cmd.Parameters.AddWithValue("@TagID1", IDSLabel.Text);
                    rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        count.Text = rd["maxrow"].ToString();
                    }
                    rd.Close();
                   }
                MyDataBind();
           }
            }
          
        }
    }

    private void MyDataBind()
    {

        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from Users_UserTags where UserTagID in (" + IDSLabel.Text + ") order by ID desc";
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            GridView1.DataSource = rd;
            GridView1.DataBind();
            rd.Close();
            conn.Close();

        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string ids = "";
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox checkBox = (CheckBox)GridView1.Rows[i].FindControl("ChechBox1");
            if (checkBox.Checked == true)
            {
                ids += "," + GridView1.DataKeys[i].Value;
            }
        }
        if (!String.IsNullOrEmpty(ids))
        {
            ids = ids.Substring(1);
            Ids.Text = ids;
            string[] array = ids.Split(',');
            int k = array.Length;
            using (SqlConnection conn = new DB().GetConnection())
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Delete from Users_UserTags where UserTagID=@TagID1 and ID in (" + Ids.Text + ") ";
                cmd.Parameters.AddWithValue("@TagID1", IDSLabel.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                int Users = Convert.ToInt32(count.Text);
                Users = Users - k;

                cmd.CommandText = "update UserTags set Users= @Users where ID=@ID";
                cmd.Parameters.AddWithValue("@ID", IDSLabel.Text);
                cmd.Parameters.AddWithValue("@Users", Users);
                cmd.ExecuteNonQuery();
                MyDataBind();
                conn.Close();
                Response.Write("<script language='javascript'> alert('成功删除" + k + "个标签关联用户');</script>");

            }
   }
        else
        {
            Response.Write("<script language='javascript'> alert('至少选择一项');</script>");
        }
    
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("Tags_Man.aspx");
    }
}