using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Data;

public partial class Del_Files : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["RoleID"] == null || Session["UserID"] == null)
            {
                Util.ShowMessage("用户登录超时，请重新登录！", "Login.aspx");
            }
            else { 
            //Response.Write(Request.QueryString["type"] + "<br/>" + Request.QueryString["folderName"] + "<br/>" + Request.QueryString["IDs"]);
            string rootfolderType = Request.QueryString["folderTypes"];
            string folderType = Request.QueryString["type"];
            string folderName = Request.QueryString["folderName"];
            string UserID = Session["UserID"].ToString();
            //删除的是根目录文件夹，panel1可见
            if (!String.IsNullOrEmpty(rootfolderType))
            {
                Panel1.Visible = true;
                Panel2.Visible = false;
                Panel3.Visible = false;
                //panel1显示要删除的根目录文件夹
                getRootFolders();
            }
            else if (!String.IsNullOrEmpty(folderType) && String.IsNullOrEmpty(folderName))//删除的是二级根目录文件夹
            {
                Panel2.Visible = true;
                Panel1.Visible = false;
                Panel3.Visible = false;
                //panel2显示要删除的二级目录文件夹
                getSecondRootFolders();
            }
            //删除的是文件缩略图
            else if (!String.IsNullOrEmpty(folderType) && !String.IsNullOrEmpty(folderName))
            {
                Panel3.Visible = true;
                Panel2.Visible = false;
                Panel1.Visible = false;
                //panel3显示要删除的文件缩略图
                getFileimgs();
            }
        }
        }
    }
    //Repeater1显示要删除的根文件夹操作
    protected void getRootFolders()
    {
        string folderType = Request.QueryString["folderTypes"];
        string UserID = Session["UserID"].ToString();
        string sqlCon = "Select * from ResourceFolders where FolderType in (" + folderType + ") and ParentFolderID = 0 and UserID =" + UserID;
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = new SqlCommand(sqlCon,conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            Repeater1.DataSource = dr;
            Repeater1.DataBind();
            cmd.Dispose();
            conn.Close();
        }

    }
    //删除根文件夹操作
    protected bool DelRootFolders(string rootFolderTypes)
    {
        Boolean flag = false;
        string folderType = rootFolderTypes;
        string UserID = Session["UserID"].ToString();
        //将该根文件夹的数据（包括放在该根目录文件夹的二级根目录的文件夹）全部删除掉
        string sqlCon = "";
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            {
                sqlCon = "Delete from ResourceFolders where FolderType in (" + folderType + ") and UserID =" + UserID;
                cmd.CommandText = sqlCon;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                cmd.Dispose();
            }
            //删除服务器的数据
            {
                sqlCon = "Select * from Resources where FileType in (" + folderType + ") and UserID =" + UserID;
                cmd.CommandText = sqlCon;
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "FolderTypeFiles");
                foreach (DataRow drow in ds.Tables["FolderTypeFiles"].Rows)
                {
                    string FilePath = drow["FilePath"].ToString();
                    // 删除物理路径下的文件
                    System.IO.File.Delete(Server.MapPath(FilePath));
                }
                conn.Close();
            }
            //将在Resources表中放在改根目录下的记录都删掉
            {
                sqlCon = "Delete from Resources where FileType in (" + folderType + ") and UserID =" + UserID;
                cmd.CommandText = sqlCon;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                cmd.Dispose();
            }
            //删除成功
            flag = true;
        }
        return flag;
    }
    //Repeater2显示要删除的二级根目录文件夹操作
    protected void getSecondRootFolders()
    {
        string IDS = Request.QueryString["IDs"];
        string sqlCon = "Select * from ResourceFolders where ID in (" + IDS + ") ";
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = new SqlCommand(sqlCon, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            Repeater2.DataSource = dr;
            Repeater2.DataBind();
            cmd.Dispose();
            conn.Close();
        }

    }
    //删除二级目录文件夹的操作
    protected bool DelSecondFolders(string IDS)
    {
        Boolean flag = false;
        string ids = IDS;
        string UserID = Session["UserID"].ToString();
        //将该二级目录文件夹的数据（包括放在该二级根目录的文件）全部删除掉
        string sqlCon = "";
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            {
                sqlCon = "Delete from ResourceFolders where ID in (" + ids + ")";
                cmd.CommandText = sqlCon;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                cmd.Dispose();
            }
            //删除服务器的数据
            {
                sqlCon = "Select * from Resources where FolderID in (" + ids + ") and UserID =" + UserID;
                cmd.CommandText = sqlCon;
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "FolderFiles");
                foreach (DataRow drow in ds.Tables["FolderFiles"].Rows)
                {
                    string FilePath = drow["FilePath"].ToString();
                    // 删除物理路径下的文件
                    System.IO.File.Delete(Server.MapPath(FilePath));
                }
                conn.Close();
            }
            //将在Resources表中放在二级目录文件夹下的记录都删掉
            {
                sqlCon = "Delete from Resources where FolderID in (" + ids + ") and UserID =" + UserID;
                cmd.CommandText = sqlCon;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                cmd.Dispose();
            }
            //删除成功
            flag = true;
        }
        return flag;
    }
   //panel3显示要删除文件缩略图的操作
    protected void getFileimgs()
    {
        string IDS = Request.QueryString["IDs"];
        string sqlCon = "Select * from Resources where ID in (" + IDS + ") ";
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = new SqlCommand(sqlCon, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            Repeater3.DataSource = dr;
            Repeater3.DataBind();
            cmd.Dispose();
            conn.Close();
        }
    }
    //删除文件缩略图的操作
    protected bool DelFileImages(string IDS)
    {
        Boolean flag = false;
        string ids = IDS;
        string UserID = Session["UserID"].ToString();
        string sqlCon = "";
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            // 删除物理路径下的文件
            {
                sqlCon = "Select * from Resources where ID in (" + ids + ")";
                cmd.CommandText = sqlCon;
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds, "FileImages");
                foreach (DataRow drow in ds.Tables["FileImages"].Rows)
                {
                    string FilePath = drow["FilePath"].ToString();
                    // 删除物理路径下的文件
                    System.IO.File.Delete(Server.MapPath(FilePath));
                }
                conn.Close();
            }
            {
                sqlCon = "Delete from Resources where ID in (" + ids + ")";
                cmd.CommandText = sqlCon;
                conn.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
            }
            //删除成功
            flag = true;
        }
        return flag;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       // Response.Redirect("File_Browse.aspx?type=" + Request.QueryString["type"] + "&folderName=" + Request.QueryString["folderName"]);
        //若删除的是根文件夹
        if (Panel1.Visible == true)
        {
            string rootFolderTypes = null;
            //再次确认用户要删除的根文件夹，防止用户删除了自己手误删除的
            for (int i = 0; i < this.Repeater1.Items.Count; i++)
            {
                HtmlInputCheckBox CB = (HtmlInputCheckBox)this.Repeater1.Items[i].FindControl("Checkbox1");
                if (CB.Checked == true)
                {
                    rootFolderTypes += "," + "'" + CB.Value + "'";
                }
                if (!String.IsNullOrEmpty(rootFolderTypes))
                {
                    rootFolderTypes = rootFolderTypes.Substring(1);
                    DelRootFolders(rootFolderTypes);
                    Response.Redirect("File_Browse.aspx");
                }
            }
        }
            //删除的是二级根目录
        else if (Panel2.Visible == true)
        {
            string type = Request.QueryString["type"];
            string ids = null;
            //再次确认用户要删除的二级目录文件夹，防止用户删除了自己手误删除的
            for (int i = 0; i < this.Repeater2.Items.Count; i++)
            {
                HtmlInputCheckBox CB = (HtmlInputCheckBox)this.Repeater2.Items[i].FindControl("Checkbox2");
                if (CB.Checked == true)
                {
                    ids += ","  + CB.Value;
                }
                if (!String.IsNullOrEmpty(ids))
                {
                    ids = ids.Substring(1);
                    DelSecondFolders(ids);
                    Response.Redirect("File_Browse.aspx?type="+type);
                }
            }
        }
            //删除的是文件缩略图
        else if (Panel3.Visible == true)
        {
            string folderType = Request.QueryString["type"];
            string folderName = Request.QueryString["folderName"];
            string ids = null;
            //再次确认用户要删除的文件缩略图，防止用户删除了自己手误删除的
            for (int i = 0; i < this.Repeater3.Items.Count; i++)
            {
                HtmlInputCheckBox CB = (HtmlInputCheckBox)this.Repeater3.Items[i].FindControl("Checkbox3");
                if (CB.Checked == true)
                {
                    ids += "," + CB.Value;
                }
                if (!String.IsNullOrEmpty(ids))
                {
                    ids = ids.Substring(1);
                    DelFileImages(ids);
                    Response.Redirect("File_Browse.aspx?type=" + folderType + "&folderName="+folderName);
                }
            }
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string folderType = Request.QueryString["type"];
        string folderName = Request.QueryString["folderName"];
         if (!String.IsNullOrEmpty(folderType) && String.IsNullOrEmpty(folderName))
            {
                Response.Redirect("File_Browse.aspx?type=" + folderType);
            }
         else if (!String.IsNullOrEmpty(folderType) && !String.IsNullOrEmpty(folderName))
         {
             Response.Redirect("File_Browse.aspx?type=" + folderType + "&folderName=" + folderName);
         }
         else
         {
             Response.Redirect("File_Browse.aspx");
         }
    }
}
