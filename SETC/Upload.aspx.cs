using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using CuteWebUI;
using System.IO;
using System.Text;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class Upload : System.Web.UI.Page
{
    string UserID = "0";
    //存储允许上传的资源的后缀名，key=Extension，value=TypeName
    Dictionary<string, string> ResourceTypes = new Dictionary<string, string>();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserID = Session["UserID"].ToString();
        string _targDir =DateTime.Now.ToString("yyyyMM");
        string basePath = Server.MapPath("~/upload/" + _targDir);
        string FileNames = "";
        HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;

        //产生随机四位数
        //Random rdm = new Random();
        //int a = rdm.Next(1000, 9999);
        
        //如果目录不存在，则创建目录
        if (files != null) {
            if (!Directory.Exists(basePath)) {
                //创建文件夹
                Directory.CreateDirectory(basePath);
            }
            
            for (int i = 0; i < files.Count; i++) {
                //string ResourceName = "1a2b3c4d5e6f7d8e9f10g11h12i13j14k15l16m17n18o19p20q";
                //是否为公开资源
                //string IsPublic = Session["IsPublic"].ToString();
                //虚拟存放目录ID
                //string UntrueFoldersID = Session["UntrueFoldersID"].ToString();
                //文件原名称
                string OldFileName = files[i].FileName;
                //文件大小（字节为单位）
                int Size = files[i].ContentLength;
                string size;
                if (Size > 1024)
                {
                    Size /= 1024;
                    size = Size.ToString();
                }
                else {
                    size = "0";
                }
                string fileSize=size;
                //文件后缀名
                string Extentsion = Path.GetExtension(files[i].FileName).ToLower();
                //自动命名文件名称
                string fileName = UserID+ "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + new Random().Next(1000, 10000).ToString()  + Extentsion;
                FileNames += "," + fileName;
                string FT = Extentsion.Substring(1);
                //文件上传保存物理路径
                string filePath = "upload" + "/" + _targDir + "/" + fileName;
                //文件类型  FileType
                
                using (SqlConnection conn = new DB().GetConnection())
                {
                   
                    SqlCommand cmd = conn.CreateCommand();
                    conn.Open();
                    cmd.CommandText = "select * from ResourceTypes where Extension=@Extension";
                    cmd.Parameters.AddWithValue("@Extension", FT);
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                       Label1.Text= rd["TypeName"].ToString();
                    }
                    
                    rd.Close();
                    conn.Close();
                }
                String fileType = Label1.Text;


                //写入文件
                files[i].SaveAs(basePath +"/"+fileName);


                //using (SqlConnection conn = new DB().GetConnection())
                //{
                //    //向resources插入一条记录操作
                //    StringBuilder sb = new StringBuilder("Insert into Resources (ResourceName,FileName,OldFileName,FilePath,FileSize,FileType,Extentsion,VirtualFoldersID,IsPublic,UserID,CDT,Status ,UseTimes,Belongs)");
                //    sb.Append(" values(@ResourceName,@FileName,@OldFileName,@FilePath,@FileSize,@FileType,@Extentsion,@VirtualFoldersID,@IsPublic,@UserID,@CDT,@Status,@UseTimes,@Belongs)");
                //    SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
                //    cmd.Parameters.AddWithValue("@ResourceName", ResourceName);
                //    cmd.Parameters.AddWithValue("@FileName", FileName);
                //    cmd.Parameters.AddWithValue("@OldFileName", OldFileName);
                //    cmd.Parameters.AddWithValue("@FilePath", FilePath);
                //    cmd.Parameters.AddWithValue("@FileSize", FileSize);
                //    cmd.Parameters.AddWithValue("@FileType", FileType);
                //    cmd.Parameters.AddWithValue("@Extentsion", Extentsion);
                //    cmd.Parameters.AddWithValue("@VirtualFoldersID", UntrueFoldersID);
                //    cmd.Parameters.AddWithValue("@IsPublic", IsPublic);
                //    cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
                //    cmd.Parameters.AddWithValue("@CDT", DateTime.Now);
                //    cmd.Parameters.AddWithValue("@Status", 0);
                //    cmd.Parameters.AddWithValue("@UseTimes", 0);
                //    cmd.Parameters.AddWithValue("@Belongs", "无");
                //    conn.Open();
                //    cmd.ExecuteNonQuery();
                //    conn.Close();

                //}
                using (SqlConnection conn = new DB().GetConnection())
                {
                    //向resources插入一条记录操作
                    StringBuilder sb = new StringBuilder("Insert into Resources (ResourceName,FileName,FilePath,FileSizeInKB,FileType,Extentsion,FolderID,FolderName,UserID,CDT,Status,UserName,Valid)");
                    sb.Append(" values(@ResourceName,@FileName,@FilePath,@FileSize,@FileType,@Extentsion,@FolderID,@FolderName,@UserID,@CDT,@Status,@UserName,@Valid)");
                    SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
                    cmd.Parameters.AddWithValue("@ResourceName", OldFileName);
                    cmd.Parameters.AddWithValue("@FileName", fileName);
                    cmd.Parameters.AddWithValue("@FilePath", filePath);
                    cmd.Parameters.AddWithValue("@FileSize", fileSize);
                    cmd.Parameters.AddWithValue("@FileType", fileType);
                    cmd.Parameters.AddWithValue("@Extentsion", Extentsion);
                    cmd.Parameters.AddWithValue("@FolderID", 1);
                    cmd.Parameters.AddWithValue("@FolderName", "默认文件夹");
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
                    cmd.Parameters.AddWithValue("@UserName", Session["UserName"].ToString());
                    cmd.Parameters.AddWithValue("@CDT", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Status", 0);
                    cmd.Parameters.AddWithValue("@Valid", 1);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    cmd.CommandText = "SELECT count(*) from Resources where FolderID =@FolderID";
                    int count = int.Parse(Convert.ToString(cmd.ExecuteScalar()));
                    cmd.CommandText = "Update ResourceFolders set Counts = " + count.ToString() + " where ID = 96";
                    //cmd.Parameters.AddWithValue("@ID", FolderDDL.SelectedItem.Value);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    //插入成功
                }
            
            }
            Util.SetFileName(FileNames);
        }
    }
}