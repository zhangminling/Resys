using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class File_Preview : System.Web.UI.Page
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
                LabelResourceID.Text = Request.QueryString["ID"];
                using (SqlConnection conn = new DB().GetConnection())
                {
                    string sql = "Select * from [Resources] where ID = @ResourceID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    conn.Open();
                    cmd.Parameters.AddWithValue("@ResourceID", LabelResourceID.Text);
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        FileType.Text = rd["FileType"].ToString();
                        string resourcefolderID = rd["FolderID"].ToString();
                        if (FileType.Text.Equals("图片"))
                        {
                            Image1.ImageUrl = rd["FilePath"].ToString();
                        }
                        if (FileType.Text.Equals("压缩"))
                        {
                            Image1.ImageUrl = "upload/Resource_Preview/ys.png";
                        }
                        if (FileType.Text.Equals("文档"))
                        {
                            Image1.ImageUrl = "upload/Resource_Preview/wd.png";
                        }
                        if (FileType.Text.Equals("视频"))
                        {
                            Image1.ImageUrl = "upload/Resource_Preview/sp.png";
                        }
                        if (FileType.Text.Equals("音频"))
                        {
                            Image1.ImageUrl = "upload/Resource_Preview/yp.png";
                        }
                        if (FileType.Text.Equals("Flash"))
                        {
                            Image1.ImageUrl = "upload/Resource_Preview/fl.png";
                        }
                        if (FileType.Text.Equals("附件"))
                        {
                            Image1.ImageUrl = "upload/Resource_Preview/wd.png";
                        }
                    }
                }
            }
            
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("File_Browse2.aspx");
    }
}