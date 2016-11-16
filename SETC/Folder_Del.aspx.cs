using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Folder_Del : System.Web.UI.Page
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
                if (Convert.ToInt16(Session["RoleID"]) > 2)
                {
                    Util.ShowMessage("对不起，你无权访问该页面！", "User_Center.aspx");
                }
                else
                {

                    if (Request.QueryString["IDS"] != null)
                    {
                        IDSLabel.Text = Request.QueryString["IDS"].ToString();
                        string[] array = IDSLabel.Text.Split(',');
                        int k = array.Length;
                        for (int num = 0; num < k; num++)
                        {
                            if (array[num] == "96" || array[num] == "101")
                            {
                                Button3.Enabled = false;
                                ErrorLabel.Text = "以下文件夹包含系统文件夹，不能进行删除";
                            }
                        }
                        MyInit();

                    }
                }
            }
        }
    }
    private void MyInit()
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from ResourceFolders where ID in (" + IDSLabel.Text + ") order by ID desc";
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
        int i = 0;
        int count = 0;
        string sqlCon = "";
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            {
                sqlCon = "Delete from ResourceFolders where ID in (" + IDSLabel.Text + ")";
                cmd.CommandText = sqlCon;
                conn.Open();
                i = cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
            }
            ////删除服务器的数据
            //{
            //    sqlCon = "Select * from Resources where FolderID in (" + IDSLabel.Text + ")";
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
            //    sqlCon = "Delete from Resources where FolderID in (" + IDSLabel.Text + ")";
            //    cmd.CommandText = sqlCon;
            //    conn.Open();
            //    cmd.ExecuteNonQuery();
            //    conn.Close();
            //    cmd.Dispose();
            //}

            //删除文件夹时将文件夹下的资源移至默认文件夹（默认文件夹不可删除）
            {
                sqlCon = "Update [Resources] set FolderID=96,FolderName='默认文件夹' where FolderID in (" + IDSLabel.Text + ")";
                cmd.CommandText = sqlCon;
                conn.Open();
                count=cmd.ExecuteNonQuery();
                conn.Close();
                conn.Open();
                cmd.CommandText = "Update ResourceFolders set Counts = Counts+" + count.ToString() + " where ID = 96";
                cmd.ExecuteNonQuery();
                conn.Close();
                cmd.Dispose();
            }

        }
        if (i > 0)
        {
            ResultLabel.Text = "成功删除" + i + "个文件夹！";
            ResultLabel.ForeColor = System.Drawing.Color.Green;
            Response.Redirect("Folder_Man.aspx");
        }
        else
        {
            ResultLabel.Text = "操作失败，请重试！";
            ResultLabel.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("Folder_Man.aspx");
    }
}