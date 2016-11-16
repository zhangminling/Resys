using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text;
using System.Data;

public partial class Folder_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["RoleID"] == null || Session["UserID"] == null)
        {
            Util.ShowMessage("用户登录超时，请重新登录！", "Login.aspx");
        }
        else
        {
              int RoleID = Convert.ToInt16(Session["RoleID"].ToString());
                if (RoleID > 4)
                {
                    Util.ShowMessage("对不起，你无权访问该页面！", "User_Center.aspx");

                }
                
        }
            

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int i = 0;
        string sqlCon = "";
        using (SqlConnection conn = new DB().GetConnection())
        {

            SqlCommand cmd = conn.CreateCommand();
            sqlCon = "select * from ResourceFolders where FolderName = @FolderName";
            cmd.CommandText = sqlCon;
            cmd.Parameters.AddWithValue("@FolderName", FolderName.Text);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            cmd.Dispose();
            if (!rd.Read())
            {
                conn.Close();
                //向resources插入一条记录操作
                StringBuilder sb = new StringBuilder("Insert into ResourceFolders (FolderName,FolderType,UserID,CDT,Orders,Resources,ParentFolderID,IsSharedFolder,FoldGUID,Counts)");
                sb.Append(" values(@FolderName,@FolderType,@UserID,@CDT,@Orders,@Resources,@ParentFolderID,@IsSharedFolder,@FoldGUID,@Counts)");
                cmd = new SqlCommand(sb.ToString(), conn);
                cmd.Parameters.AddWithValue("@FolderName", FolderName.Text);
                cmd.Parameters.AddWithValue("@FolderType", "");
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
                cmd.Parameters.AddWithValue("@CDT", DateTime.Now);
                cmd.Parameters.AddWithValue("@Orders", 1);
                cmd.Parameters.AddWithValue("@Resources", 0);
                cmd.Parameters.AddWithValue("@ParentFolderID", 0);
                cmd.Parameters.AddWithValue("@IsSharedFolder", 1);
                FoldGUID.Text = Guid.NewGuid().ToString();
                cmd.Parameters.AddWithValue("@FoldGUID", FoldGUID.Text);
                cmd.Parameters.AddWithValue("@Counts", 0);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                //插入成功
                i = 1;
            }
            else 
            {
                ErrorLabel.Text = "文件夹命名重复，请重新输入！";
            }
            rd.Close();
        }
        if (i == 1)
        {
            Response.Redirect(Server.HtmlEncode("Folder_Man.aspx"));
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("Folder_Man.aspx");
    }
}