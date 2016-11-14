using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.IO;



public partial class Article_More : System.Web.UI.Page
{
    //上传封面图后更新数据表Resources用到的数据
    string ResourceName = "";
    string extension = "";
    string physicalName = "";
    string fileName = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RandomID.Text = Request["RandomID"];
            MyInitForUpdate();

        }

    }

    protected void MyInitForUpdate()
    {

        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from UserTags where classify=0 order by ID asc";
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            UserTag.DataSource = rd;
            UserTag.DataTextField = "TagName";
            UserTag.DataValueField = "ID";
            UserTag.DataBind();
            rd.Close();


            cmd.CommandText = "Select * from Articles where RandomID = '" + RandomID.Text + "'";
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                IDLabel.Text = rd["ID"].ToString();
                Title.Text = rd["Title"].ToString();
                CoverPhoto.ImageUrl = rd["CoverImageURL"].ToString();
                Orders.Text = rd["Orders"].ToString();
                UserName.Text = rd["Author"].ToString();

            }
            rd.Close();
         

        }

    }

    protected void UploadButton_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            try
            {
                ResourceName = FileUpload1.FileName;
                extension = System.IO.Path.GetExtension(ResourceName).ToLower();
                string allowExtension = ConfigurationManager.AppSettings["PhotoExtension"].ToString().ToLower();

                string[] ss = allowExtension.Split(',');
                bool success = false;
                foreach (string s in ss)
                {
                    if (extension.Equals(s.Trim()))
                    {
                        success = true;
                        break;
                    }
                }

                if (success && Session["UserID"] != null)
                {
                    string dir = "upload" + "/" + DateTime.Now.ToString("yyyyMM");

                    // 目录不存在,则创建目录
                    if (!Directory.Exists(Server.MapPath(dir)))
                    {
                        Directory.CreateDirectory(Server.MapPath(dir));
                    }

                    string now = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    string number = String.Format("{0:0000}", new Random().Next(1000));//生产****四位数的字符串
                    fileName = Session["UserID"].ToString() + "_" + now + "_" + number + extension;
                    physicalName = dir + "/" + Session["UserID"].ToString() + "_" + now + "_" + number + extension;

                    //int fileSizeInMB = FileUpload1.PostedFile.ContentLength / (1024*1024) ;
                    //if (fileSizeInMB == 0) fileSizeInMB = 1;         

                    // 保存图片到服务器
                    FileUpload1.SaveAs(Server.MapPath(physicalName));

                    //在数据表Resources插入一条记录操作
                    InsertDataBase();

                    /*
                    using (SqlConnection conn = new DB().GetConnection())
                    {
                        SqlCommand cmd = conn.CreateCommand();
                        if (String.IsNullOrEmpty(RandomID.Text))
                        {
                            RandomID.Text = Guid.NewGuid().ToString();
                            cmd.CommandText = "insert into Shows( CoverPhotoURL,RandomID ) values( @CoverPhotoURL,@RandomID )";
                            cmd.Parameters.AddWithValue("@CoverPhotoURL", physicalName);
                            cmd.Parameters.AddWithValue("@RandomID", RandomID.Text);
                        }
                        else
                        {
                            cmd.CommandText = "update Shows set CoverPhotoURL = @CoverPhotoURL where RandomID = @RandomID";
                            cmd.Parameters.AddWithValue("@CoverPhotoURL", physicalName);
                            cmd.Parameters.AddWithValue("@RandomID", RandomID.Text);
                        }
                        
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    */
                    if (!String.IsNullOrEmpty(CoverPhoto.ImageUrl))
                    {
                        File.Delete(Server.MapPath(CoverPhoto.ImageUrl));
                    }

                    CoverPhoto.ImageUrl = physicalName;

                    //MyDataBind();
                }
                else
                {
                    ResultLabel.Text = "上传图片格式错误！";
                    ResultLabel.Visible = true;
                    ResultLabel.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception exc)
            {
                ResultLabel.Text = "上传图片失败。请重试！或者与管理员联系！<br>" + exc.ToString();
                ResultLabel.Visible = true;
                ResultLabel.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            ResultLabel.Text = "请选择封面图片";
            ResultLabel.Visible = true;
        }

    }

    //在数据表Resources插入一条记录操作
    protected bool InsertDataBase()
    {
        bool result = false;

        using (SqlConnection conn = new DB().GetConnection())
        {
            //向resources插入一条记录操作
            StringBuilder sb = new StringBuilder("Insert into Resources (ResourceName,FileName,FilePath,FileSizeInKB,FileType,Extentsion,FolderID,FolderName,UserID,CDT,Status,UserName,Valid)");
            sb.Append(" values(@ResourceName,@FileName,@FilePath,@FileSize,@FileType,@Extentsion,@FolderID,@FolderName,@UserID,@CDT,@Status,@UserName,@Valid)");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.AddWithValue("@ResourceName", ResourceName);
            cmd.Parameters.AddWithValue("@FileName", fileName);
            cmd.Parameters.AddWithValue("@FilePath", physicalName);
            cmd.Parameters.AddWithValue("@FileSize", 0);
            cmd.Parameters.AddWithValue("@FileType", "图片");
            cmd.Parameters.AddWithValue("@Extentsion", extension);
            cmd.Parameters.AddWithValue("@FolderID", 101);
            cmd.Parameters.AddWithValue("@FolderName", "封面图");
            cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
            cmd.Parameters.AddWithValue("@UserName", Session["UserName"].ToString());
            cmd.Parameters.AddWithValue("@CDT", DateTime.Now);
            cmd.Parameters.AddWithValue("@Status", 0);
            cmd.Parameters.AddWithValue("@Valid", 1);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            conn.Open();
            cmd.CommandText = "SELECT count(*) from Resources where FolderID =101";
            int count = int.Parse(Convert.ToString(cmd.ExecuteScalar()));
            cmd.CommandText = "Update ResourceFolders set Counts = " + count.ToString() + " where ID = 101";
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            //插入成功
            result = true;
        }
        return result;

    }


    private int DoUpdate(int finished)
    {
        int i = 0;
        int TagID = 0;
        String TagName = "";
        if (UserTag.Items.Count > 0 && UserTag.SelectedValue != null)
        {
            TagID = Convert.ToInt16(UserTag.SelectedValue);
            TagName = UserTag.SelectedItem.Text;
        }
   
        using (SqlConnection conn = new DB().GetConnection())
        {
            StringBuilder sb =  new StringBuilder("Update Articles set CoverImageURL=@CoverImageURL,LDT=@LDT,Orders=@Orders,TagName=@TagName,TagID=@TagID where RandomID = @RandomID ");

            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.AddWithValue("@RandomID", RandomID.Text);
            cmd.Parameters.AddWithValue("@Orders", Orders.Text);
            cmd.Parameters.AddWithValue("@TagID", TagID);
            cmd.Parameters.AddWithValue("@TagName",TagName);
            cmd.Parameters.AddWithValue("@CoverImageURL", CoverPhoto.ImageUrl);
            cmd.Parameters.AddWithValue("@LDT", DateTime.Now);
            // RoleID={1,Administrator},{2,Editor},{3,Contributor},{4,Author}            
            /*  int RoleID = Convert.ToInt16(Session["RoleID"].ToString());
              if (RoleID > 2)
              {
                  cmd.Parameters.AddWithValue("@Status", 0); //状态：新投稿/待审核=0，审核已过=1，审核未过=2            
                  cmd.Parameters.AddWithValue("@StatusName", "未审核");
              }
              else
              {
                  cmd.Parameters.AddWithValue("@Status", 1); //状态：新投稿/待审核=0，审核已过=1，审核未过=2      
                  cmd.Parameters.AddWithValue("@StatusName", "通过审核");
              }            
              */
          
       

            conn.Open();
            i = cmd.ExecuteNonQuery();
        }
        /*   //同时更新微信的数据
           using (SqlConnection conn = new DB().GetConnection())
           {
               StringBuilder sb = new StringBuilder("Update Articles2 set Title = @Title,Content=@Content,CatID=@CatID,CatName=@CatName,SubID=@SubID,SubName=@SubName,AuthorID=@AuthorID,Author=@Author,");
               sb.Append("CDT=@CDT,LDT=@LDT,Orders=@Orders,Finished=@Finished where RandomID = @RandomID ");
               SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
               cmd.Parameters.AddWithValue("@Title", TitleTB.Text);
               cmd.Parameters.AddWithValue("@Content", Editor1.Text);
               cmd.Parameters.AddWithValue("@CatID", Cats.SelectedValue);
               cmd.Parameters.AddWithValue("@CatName", Cats.SelectedItem.Text);
               cmd.Parameters.AddWithValue("@SubID", SubID);
               cmd.Parameters.AddWithValue("@SubName", SubName);
               cmd.Parameters.AddWithValue("@AuthorID", Session["UserID"].ToString());
               cmd.Parameters.AddWithValue("@Author", Session["UserName"].ToString());
               cmd.Parameters.AddWithValue("@CDT", CDT_TextBox.Text);
               cmd.Parameters.AddWithValue("@LDT", DateTime.Now);
               cmd.Parameters.AddWithValue("@Orders", Orders.Text);
               cmd.Parameters.AddWithValue("@Finished", finished);
               cmd.Parameters.AddWithValue("@RandomID", RandomID.Text);
               conn.Open();
               cmd.ExecuteNonQuery();
           }*/
        return i;
    }





    protected void Button1_Click(object sender, EventArgs e)
    {
        int i = 0;  
        i=DoUpdate(1);

        if (i == 1)
        {
            ResultLabel.Text = "修改成功！";
            ResultLabel.ForeColor = System.Drawing.Color.Green;

        }
        else
        {
            ResultLabel.Text = "修改失败，请重试！";
            ResultLabel.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("Article_Add.aspx?ID=" + IDLabel.Text);
    }
   

}