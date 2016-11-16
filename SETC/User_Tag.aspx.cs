using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.IO;


public partial class User_Tag : System.Web.UI.Page
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
                string UserID = Convert.ToString(Session["UserID"]);


                if (Session["RoleID"] != null && !String.IsNullOrEmpty(Session["RoleID"].ToString()) && (Session["RoleID"].ToString() == "1" || Session["RoleID"].ToString() == "2"))
                {
                    //ClassifyPanel.Visible = true;
                }
                else
                {
                    //ClassifyPanel.Visible = false;
                }

                using (SqlConnection conn = new DB().GetConnection())
                {

                    string sql = "select * from  [UserTags]  where [UserID] = @UserID order by ID asc   ";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    TagsList.DataSource = rd;
                    TagsList.DataTextField = "TagName";
                    TagsList.DataValueField = "ID";
                    TagsList.DataBind();
                    rd.Close();
                }

            }
        }

     }

    private void  MyDataBind()
    {
        int i = 0;
        string UserID = Convert.ToString(Session["UserID"]);
        string username = Convert.ToString(Session["UserName"]);
        int a = 0;
        //if (CheckRole.Checked)
        //{
        //    a = 1;
        //}
        //else if (PublishDepartment.Checked)
        //{
        //    a = 0;
        //}
        using (SqlConnection conn = new DB().GetConnection())
        {
            
            StringBuilder sb = new StringBuilder("insert into UserTags(TagName,Description,UserID,UserName )");
            sb.Append(" values ( @TagName,@Description,@UserID,@UserName ) ");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.AddWithValue("@TagName", TagName.Text);
            cmd.Parameters.AddWithValue("@Description", TagDescription.Text);
            //cmd.Parameters.AddWithValue("@Classify", a);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@UserName", username);
            conn.Open();
            i = cmd.ExecuteNonQuery();
            conn.Close();
            Response.Write("<script language='javascript'> alert('操作成功');</script>");
            TagName.Text = "";
            TagDescription.Text = "";
            //CheckRole.Checked = false;
            //PublishDepartment.Checked = false;
        }
    }

    private void UpdateTags()
    {
        string userid = Convert.ToString(Session["UserID"]);
        using (SqlConnection conn = new DB().GetConnection())
        {
            string sql = "select * from UserTags where UserID = @UserID order by ID desc";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UserID", userid);
            conn.Open();

            SqlDataReader rd = cmd.ExecuteReader();
            TagsList.DataSource = rd;
            TagsList.DataTextField = "TagName";
            TagsList.DataValueField = "ID";
            TagsList.DataBind();
            rd.Close();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        MyDataBind();
        UpdateTags();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string ids = "";
        for (int i = 0; i <= TagsList.Items.Count - 1; i++)
        {

            if (TagsList.Items[i].Selected == true)
            {
                ids += "," + TagsList.Items[i].Value;
            }
        }
        if (!String.IsNullOrEmpty(ids))
        {
            ids = ids.Substring(1);
            Response.Redirect(Server.HtmlEncode("TagUsers_Add.aspx?IDS=" + ids));
        }
    }
}


