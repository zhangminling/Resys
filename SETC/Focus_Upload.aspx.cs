using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

public partial class Focus_Upload : System.Web.UI.Page
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
                  if (RoleID > 2)
                  {
                      Util.ShowMessage("对不起，你无权访问该页面！", "User_Center.aspx");

                  }
                  else
                  {
                      ExtensionLabel.Text = ConfigurationManager.AppSettings["PhotoExtension"].ToString();
                  }
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            try
            {
                string fileName = FileUpload1.FileName;
                string extension = System.IO.Path.GetExtension(fileName).ToLower();
                string allowExtension = ConfigurationManager.AppSettings["PhotoExtension"].ToString();
                if (allowExtension.Contains(extension))
                {
                    string now = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    string number = String.Format("{0:0000}", new Random().Next(1000));//生产****四位数的字符串
                    string physicalName = "upload/Focus/" + Session["UserID"].ToString() + "_" + now + "_" + number + extension;
                    //int fileSize = FileUpload1.PostedFile.ContentLength / 1024 ;
                    //if (fileSize == 0) fileSize = 1;                   
                    FileUpload1.SaveAs(Server.MapPath(physicalName));
                    using (SqlConnection conn = new DB().GetConnection())
                    {
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.CommandText = "insert into Focuses( PhotoSrc,UserID,UserName ) values( @PhotoSrc,@UserID,@UserName )";
                        cmd.Parameters.AddWithValue("@PhotoSrc", physicalName);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
                        cmd.Parameters.AddWithValue("@UserName", Session["UserName"].ToString());
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }

                    Response.Redirect(Server.HtmlEncode("Focus_Man.aspx"));


                }
                else
                {
                    ResultLabel.Text = "上传图片格式错误！";
                    ResultLabel.Visible = true;
                }
            }
            catch (Exception exc)
            {
                ResultLabel.Text = "上传图片失败。请重试！或者与管理员联系！<br>" + exc.ToString();
                ResultLabel.Visible = true;
            }
        }
        else
        {
            ResultLabel.Text = "所选图片格式不符合要求";
            ResultLabel.Visible = true;
        }

    }
}