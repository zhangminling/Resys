using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;

public partial class ShowItem_Man : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RandomID.Text = Request["RandomID"];
            MyInit();
            MyDataBind();
        }
    }

    protected void MyInit()
    {
        string s = "";
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Select ID,Title,CoverPhotoURL from Shows where RandomID = '" + RandomID.Text + "'";
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                IDLabel.Text = rd["ID"].ToString();
                TitleLabel.Text = rd["Title"].ToString();
                s = rd["CoverPhotoURL"].ToString();
            }
            rd.Close();
            conn.Close();
        }
        if (!String.IsNullOrEmpty(s)) { s = s.Substring(s.LastIndexOf('/')+1, s.LastIndexOf('.') - s.LastIndexOf('/') - 1); }
        DirLabel.Text = s;
    }

    protected void MyDataBind()
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Select * from Items where ShowID = '" + IDLabel.Text + "' order by Orders desc, ID desc";
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            GridView1.DataSource = rd;
            GridView1.DataBind();
            rd.Close();
            conn.Close();
        }
    }

    protected void BackButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Show_Add.aspx?ID=" + IDLabel.Text);
    }
    protected void UploadButton_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            try
            {
                string fileName = FileUpload1.FileName;
                string extension = System.IO.Path.GetExtension(fileName).ToLower();
                // 检测是否是合法的文件后缀名
                string fileType = Util.GetFileType(extension);

                if ( !String.IsNullOrEmpty(fileType) && Session["UserID"] != null)
                {
                    string dir = "Users/" + Session["UserID"].ToString() + "/Shows/" + DirLabel.Text;

                    // 目录不存在,则创建目录
                    if (!Directory.Exists(Server.MapPath(dir)))
                    {
                        Directory.CreateDirectory(Server.MapPath(dir));
                    }

                    string now = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    string number = String.Format("{0:0000}", new Random().Next(1000));//生产****四位数的字符串
                    string physicalName = dir + "/" + now + "_" + number + extension;

                    int fileSizeInMB = FileUpload1.PostedFile.ContentLength / (1024*1024) ;
                    if (fileSizeInMB == 0) fileSizeInMB = 1;
                    if (fileSizeInMB < 5)
                    {
                        // 保存图片到服务器
                        FileUpload1.SaveAs(Server.MapPath(physicalName));

                        using (SqlConnection conn = new DB().GetConnection())
                        {
                            SqlCommand cmd = conn.CreateCommand();
                            cmd.CommandText = "insert into Items( ShowID,FileType,FileName,FileExtension,FileSizeInMB,FileURL,Orders,AuthorID ) values( @ShowID,@FileType,@FileName,@FileExtension,@FileSizeInMB,@FileURL,@Orders,@AuthorID )";
                            cmd.Parameters.AddWithValue("@ShowID", IDLabel.Text);
                            cmd.Parameters.AddWithValue("@FileType", fileType);
                            cmd.Parameters.AddWithValue("@FileName", fileName);
                            cmd.Parameters.AddWithValue("@FileExtension", extension);
                            cmd.Parameters.AddWithValue("@FileSizeInMB", fileSizeInMB);
                            cmd.Parameters.AddWithValue("@FileURL", physicalName);
                            cmd.Parameters.AddWithValue("@Orders", 1);
                            cmd.Parameters.AddWithValue("@AuthorID", Session["UserID"].ToString());
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }

                        MyDataBind();
                    }
                    else
                    {
                        ResultLabel.Text = "上传图片的太大，只能上传<4M的图片！";
                        ResultLabel.Visible = true;
                    }
                    
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
            ResultLabel.Text = "请选择上传文件！";
            ResultLabel.Visible = true;
        }
    }
    protected void ConfirmButton_Click(object sender, EventArgs e)
    {
        int k = 0;
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow r = GridView1.Rows[i];
                string orders = (r.FindControl("OrdersTextBox") as TextBox).Text;
                string description = (r.FindControl("DescriptionTextBox") as TextBox).Text;
                cmd.CommandText = "Update Items set Orders = " + orders + ",Description='" + description + "' where ID = " + GridView1.DataKeys[i].Value;
                k+=cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }
        ResultLabel.Text = "保存" + k + "条记录，操作成功！";
        ResultLabel.Visible = true;

        MyDataBind();
    }
    protected void LinkItemButton_Click(object sender, EventArgs e)
    {
        string FileURL = LinkItemTextBox.Text;
        if (!String.IsNullOrEmpty(FileURL))
        {
            using (SqlConnection conn = new DB().GetConnection())
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "insert into Items( ShowID,FileType,FileName,FileExtension,FileSizeInMB,FileURL,Orders,AuthorID ) values( @ShowID,@FileType,@FileName,@FileExtension,@FileSizeInMB,@FileURL,@Orders,@AuthorID )";
                cmd.Parameters.AddWithValue("@ShowID", IDLabel.Text);
                cmd.Parameters.AddWithValue("@FileType", "Images");
                cmd.Parameters.AddWithValue("@FileName", " ");
                cmd.Parameters.AddWithValue("@FileExtension", ".jpg");
                cmd.Parameters.AddWithValue("@FileSizeInMB", 1);
                cmd.Parameters.AddWithValue("@FileURL", FileURL);
                cmd.Parameters.AddWithValue("@Orders", 1);
                cmd.Parameters.AddWithValue("@AuthorID", Session["UserID"].ToString());
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            MyDataBind();
        }
    }
    
}
