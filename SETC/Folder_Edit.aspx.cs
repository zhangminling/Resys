using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public partial class Folder_Edit : System.Web.UI.Page
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
                int RoleID = Convert.ToInt16(Session["RoleID"].ToString());
                if (RoleID > 4)
                {
                    Util.ShowMessage("对不起，你无权访问该页面！", "User_Center.aspx");

                }
                else {
            LabelFolderID.Text = Request.QueryString["ID"];
            if (LabelFolderID.Text == "96" || LabelFolderID.Text == "101")
            {
                FolderName.Enabled = false;
                Button1.Enabled = false;
                Button2.Enabled = false;
                ErrorLabel.Text = "系统文件夹不能进行编辑";
            }


          //  int RoleID = Convert.ToInt16(Session["RoleID"].ToString());
            if (RoleID > 2)
            {
                Button2.Visible = false;

            }
            else
            {

                Button2.Visible = true;

            }

            using (SqlConnection conn = new DB().GetConnection())
            {
                string sql = "Select * from [ResourceFolders] where ID = @FolderID";                
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FolderID", LabelFolderID.Text);
                conn.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {

                    FolderName.Text = rd["FolderName"].ToString();
                    Counts.Text = rd["Counts"].ToString();
                    CreateDateTime.Text = rd["CDT"].ToString();
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
        AspNetPager1.PageSize = Convert.ToInt16(PageSizeDDL.SelectedValue);
        StringBuilder whereStr = new StringBuilder(" where Valid=1 and FolderID = @FolderID ");
        string sql = "select count(ID) as total from Resources " + whereStr.ToString();

        using (SqlConnection conn = (SqlConnection)new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@FolderID", LabelFolderID.Text);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                AspNetPager1.RecordCount = Convert.ToInt16(rd[0]);
            }
            else
            {
                AspNetPager1.RecordCount = 0;
            }
            rd.Close();

            Label1.Text = AspNetPager1.RecordCount + "";//总记录数
            Label2.Text = (AspNetPager1.RecordCount / AspNetPager1.PageSize) + 1 + "";//总页数            

            if (AspNetPager1.CurrentPageIndex == 1)
            {
                sql = "Select top " + AspNetPager1.PageSize + " * from Resources " + whereStr.ToString() + " " + " Order by ID Desc";
            }
            else
            {
                // Select Top 页容量 * from 表 where 条件 and id not in	(Select Top 页容量*（当前页数-1） id 	from 表 where 条件 order by 排序条件) order by 排序条件
                sql = "Select top " + AspNetPager1.PageSize + " * from Resources " + whereStr.ToString() + " and id not in ( select top " + AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1) + " id  from Resources " + whereStr.ToString() + " " + " Order by ID Desc" + " ) " + " Order by ID Desc";
                //sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER ( " + orderStr + ") AS MyRank,* FROM Article " + whereStr +" ) AS Rank " + whereStr + " and MyRank BETWEEN " +AspNetPager1.StartRecordIndex+" AND "+ (AspNetPager1.StartRecordIndex+AspNetPager1.PageSize-1) +orderStr;
            }
            //TestLabel.Text = sql;
            cmd.CommandText = sql;
            rd = cmd.ExecuteReader();
            GridView1.DataSource = rd;
            GridView1.DataKeyNames = new string[] { "ID" };
            GridView1.DataBind();
            rd.Close();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        ErrorLabel.Text = "";
        int i = 0;
        using (SqlConnection conn = new DB().GetConnection())
        {
            string sql = "Update [ResourceFolders] set FolderName=@FolderName where ID = @FolderID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@FolderName", FolderName.Text.Trim());
            cmd.Parameters.AddWithValue("@FolderID", LabelFolderID.Text);
            conn.Open();
            i = cmd.ExecuteNonQuery();
            conn.Close();
            //将在Resources表中放在文件夹下的资源都更新文件夹名
            {
                sql = "Update Resources set FolderName=@FolderName where FolderID = @FolderID";
                cmd.CommandText = sql;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                cmd.Dispose();
            }
        }

        if (i == 1)
        {
            ErrorLabel.Text = "媒体信息更新成功！";
            Response.Redirect(Server.HtmlEncode("Folder_Man.aspx"));
        }
        else
        {
            ErrorLabel.Text = "媒体信息更新失败，请重试！";
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        ErrorLabel.Text = "";
        int i = 0;
        int count = 0;
        string sqlCon = "";
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            //删除数据库的数据
            {
                string sql = "Delete from [ResourceFolders] where ID = @FolderID";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FolderID", LabelFolderID.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            ////删除服务器的数据
            //{
            //    sqlCon = "Select * from Resources where FolderID = @FolderID";
            //    cmd.CommandText = sqlCon;
            //    conn.Open();
            //    SqlDataAdapter sda = new SqlDataAdapter();
            //    sda.SelectCommand = cmd;
            //    DataSet ds = new DataSet();
            //    sda.Fill(ds, "FolderFiles");
            //    foreach (DataRow drow in ds.Tables["FolderFiles"].Rows)
            //    {
            //        string FilePath = drow["FilePath"].ToString();
            //        // 删除物理路径下的文件
            //        System.IO.File.Delete(Server.MapPath(FilePath));
            //    }
            //    conn.Close();
            //}
            ////将在Resources表中放在文件夹下的资源都删掉
            //{
            //    sqlCon = "Delete from Resources where FolderID  = @FolderID";
            //    cmd.CommandText = sqlCon;
            //    conn.Open();
            //    cmd.ExecuteNonQuery();
            //    conn.Close();
            //    cmd.Dispose();
            //}

            //删除文件夹时将文件夹下的资源移至默认文件夹（默认文件夹不可删除）
            {
                sqlCon = "Update [Resources] set FolderID=96,FolderName='默认文件夹' where FolderID = @FolderID";
                cmd.CommandText = sqlCon;
                conn.Open();
                count = cmd.ExecuteNonQuery();
                conn.Close();
                conn.Open();
                cmd.CommandText = "Update ResourceFolders set Counts = Counts+" + count.ToString() + " where ID = 96";
                cmd.ExecuteNonQuery();
                conn.Close();
                cmd.Dispose();
            }
            i = 1;
        }

        if (i == 1)
        {
            Response.Redirect(Server.HtmlEncode("Folder_Man.aspx"));
        }
        else
        {
            ErrorLabel.Text = "删除文件夹失败，请重试！";
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            string sql = "SELECT count(*) from Resources where FolderID =@FolderID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@FolderID", LabelFolderID.Text);
            conn.Open();
            int count = int.Parse(Convert.ToString(cmd.ExecuteScalar()));
            cmd.CommandText = "Update ResourceFolders set Counts = " + count.ToString() + " where ID = @FolderID";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "Select * from [ResourceFolders] where ID = @FolderID";
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {

                FolderName.Text = rd["FolderName"].ToString();
                Counts.Text = rd["Counts"].ToString();
                CreateDateTime.Text = rd["CDT"].ToString();
            }
            rd.Close();
            cmd.Dispose();
            conn.Close();
        }
    }
    protected void PageSizeDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        MyDataBind();
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        MyDataBind();
    }
}