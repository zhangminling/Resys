using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Text;

public partial class Show_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && Session["UserID"]!=null)
        {
            if (Request.QueryString["ID"] != null && !String.IsNullOrEmpty(Request.QueryString["ID"].ToString()))
            {
                // 修改作品
                IDLabel.Text = Request.QueryString["ID"].ToString();
                MyInitForUpdate();
            }
            else
            {
                // 新增作品
                MyInitForAdd();
                RandomID.Text = ""; //第一次加载页面，设置一个空的RandomID
            }
        }
    }

    private void MyInitForUpdate()
    {
        string CatalogID = "";
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from Catalogs order by Orders desc";
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            CatalogsDDL.DataSource = rd;
            CatalogsDDL.DataValueField = "ID";
            CatalogsDDL.DataTextField = "Catalog";
            CatalogsDDL.DataBind();
            rd.Close();

            cmd.CommandText = "Select * from Shows where ID = " + IDLabel.Text;
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                TitleTB.Text = rd["Title"].ToString();
                CatalogID = rd["CatalogID"].ToString();
                CDT_TextBox.Text = String.Format("{0:yyyy-MM-dd}", rd["CDT"]);
                Orders.Text = rd["Orders"].ToString();
                AbsTextBox.Text = rd["Abs"].ToString();
                CoverPhoto.ImageUrl = rd["CoverPhotoURL"].ToString();
                RandomID.Text = rd["RandomID"].ToString();
            }
            rd.Close();
        }

        if (!String.IsNullOrEmpty(CatalogID) && CatalogsDDL.Items.FindByValue(CatalogID) !=null )
        {
            CatalogsDDL.Items.FindByValue(CatalogID).Selected = true;
        }
    }

    private void MyInitForAdd()
    {
        CDT_TextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from Catalogs order by Orders desc";
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            CatalogsDDL.DataSource = rd;
            CatalogsDDL.DataValueField = "ID";
            CatalogsDDL.DataTextField = "Catalog";
            CatalogsDDL.DataBind();
            rd.Close();

            cmd.CommandText = "select max(Orders)+1 as orders from Shows";
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                MaxOrders.Text = rd[0].ToString();
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
                string fileName = FileUpload1.FileName;
                string extension = System.IO.Path.GetExtension(fileName).ToLower();
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

                if (success && Session["UserID"]!=null)
                {
                    string dir = "Users/" + Session["UserID"].ToString() + "/Shows" ;

                    // 目录不存在,则创建目录
                    if(!Directory.Exists(Server.MapPath(dir))){
                        Directory.CreateDirectory(Server.MapPath(dir));
                    }
                    
                    string now = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    string number = String.Format("{0:0000}", new Random().Next(1000));//生产****四位数的字符串
                    string physicalName = dir + "/" + now + "_" + number + extension;

                    //int fileSizeInMB = FileUpload1.PostedFile.ContentLength / (1024*1024) ;
                    //if (fileSizeInMB == 0) fileSizeInMB = 1;         
          
                    // 保存图片到服务器
                    FileUpload1.SaveAs(Server.MapPath(physicalName));

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
            ResultLabel.Text = "请选择封面图片";
            ResultLabel.Visible = true;
        }

    }
    
    
    protected void NextButton_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(RandomID.Text))
        {
            Response.Redirect("ShowItem_Man.aspx?RandomID=" + RandomID.Text);
        }
        else
        {
            ResultLabel.Text = "上传并保存之后，才能进入下一步！";
            ResultLabel.Visible = true;
        }
    }
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(CoverPhoto.ImageUrl))
        {
            ResultLabel.Text = "请上传封面图片";
            ResultLabel.Visible = true;
        }
        else
        {
            int i = 0;
            if (String.IsNullOrEmpty(IDLabel.Text))
            {
                i = DoInsert(1);
            }
            else
            {
                i = DoUpdate(1);
            }

            if (i == 1)
            {
                ResultLabel.Text = "操作成功！";
                ResultLabel.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                ResultLabel.Text = "操作失败，请重试！";
                ResultLabel.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected int DoInsert(int finished)
    {
        int i = 0;

        using (SqlConnection conn = new DB().GetConnection())
        {
            StringBuilder sb = new StringBuilder("Insert into Shows ( Title,Abs,Catalog,CatalogID,CoverPhotoURL,AuthorID,Author,CDT,LDT,Status,AuditorID,Auditor,AuditedDateTime,ViewTimes,ReviewTimes,Orders,Finished,RandomID) ");
            sb.Append(" values ( @Title,@Abs,@Catalog,@CatalogID,@CoverPhotoURL,@AuthorID,@Author,@CDT,@LDT,@Status,@AuditorID,@Auditor,@AuditedDateTime,@ViewTimes,@ReviewTimes,@Orders,@Finished,@RandomID) ");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.AddWithValue("@Title", TitleTB.Text);
            cmd.Parameters.AddWithValue("@Abs", AbsTextBox.Text);
            cmd.Parameters.AddWithValue("@Catalog", CatalogsDDL.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@CatalogID", CatalogsDDL.SelectedValue);
            cmd.Parameters.AddWithValue("@CoverPhotoURL", CoverPhoto.ImageUrl);
            cmd.Parameters.AddWithValue("@AuthorID", Session["UserID"].ToString());
            cmd.Parameters.AddWithValue("@Author", Session["UserName"].ToString());
            cmd.Parameters.AddWithValue("@CDT", CDT_TextBox.Text);
            cmd.Parameters.AddWithValue("@LDT", DateTime.Now);

            // RoleID={1,Administrator},{2,Editor},{3,Contributor},{4,Author}            
            int RoleID = Convert.ToInt16(Session["RoleID"].ToString());
            if (RoleID > 2)
            {
                cmd.Parameters.AddWithValue("@Status", 0); //状态：新投稿/待审核=0，审核已过=1，审核未过=2            

            }
            else
            {
                cmd.Parameters.AddWithValue("@Status", 1); //状态：新投稿/待审核=0，审核已过=1，审核未过=2      
            }

            cmd.Parameters.AddWithValue("@AuditorID", Session["UserID"].ToString());
            cmd.Parameters.AddWithValue("@Auditor", Session["UserName"].ToString());
            cmd.Parameters.AddWithValue("@AuditedDateTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@ViewTimes", 0);
            cmd.Parameters.AddWithValue("@ReviewTimes", 0);
            cmd.Parameters.AddWithValue("@Orders", Orders.Text);
            cmd.Parameters.AddWithValue("@Finished", finished);


            RandomID.Text = Guid.NewGuid().ToString();
            cmd.Parameters.AddWithValue("@RandomID", RandomID.Text);

            conn.Open();
            i = cmd.ExecuteNonQuery();

        }
        return i;
    }
    protected int DoUpdate(int finished)
    {
        int i = 0;
        using (SqlConnection conn = new DB().GetConnection())
        {
            string sql = "Update Shows set Title=@Title,Abs=@Abs,Catalog=@Catalog,CatalogID=@CatalogID,CoverPhotoURL=@CoverPhotoURL,CDT=@CDT,Status=@Status,Orders=@Orders,Finished=@Finished where ID=@ID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Title", TitleTB.Text);
            cmd.Parameters.AddWithValue("@Abs", AbsTextBox.Text);
            cmd.Parameters.AddWithValue("@Catalog", CatalogsDDL.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@CatalogID", CatalogsDDL.SelectedValue);
            cmd.Parameters.AddWithValue("@CoverPhotoURL", CoverPhoto.ImageUrl);            
            cmd.Parameters.AddWithValue("@CDT", CDT_TextBox.Text); 
            // RoleID={1,Administrator},{2,Editor},{3,Contributor},{4,Author}            
            int RoleID = Convert.ToInt16(Session["RoleID"].ToString());
            if (RoleID > 2)
            {
                cmd.Parameters.AddWithValue("@Status", 0); //状态：新投稿/待审核=0，审核已过=1，审核未过=2            

            }
            else
            {
                cmd.Parameters.AddWithValue("@Status", 1); //状态：新投稿/待审核=0，审核已过=1，审核未过=2      
            }            
            cmd.Parameters.AddWithValue("@Orders", Orders.Text);
            cmd.Parameters.AddWithValue("@Finished", finished);
            cmd.Parameters.AddWithValue("@ID",IDLabel.Text);

            conn.Open();
            i = cmd.ExecuteNonQuery();
        }
        return i;
    }

    protected void CancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Show_Add.aspx?ID=" + IDLabel.Text);
    }
}
