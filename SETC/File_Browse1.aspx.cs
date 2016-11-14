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


public partial class File_Browse1 : System.Web.UI.Page
{
    //存储允许上传的资源的后缀名，key=Extension，value=TypeName
    Dictionary<string, string> ResourceTypes = new Dictionary<string, string>();

    //设置默认的RoleID    
    string RoleID = "5";
    string UserID = "0";
    //chenxilian:定义一个guid变量，来接收系统生成的唯一GUID值
    string guid = "0";
    //获得上传图片的路劲
    public string ImgData = null;
    //插入ckEditor的图片的路径
    public string insertFileHTML = "<strong>插入资源错误，请重试</strong>";


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["RoleID"] == null || Session["UserID"] == null)
            {
                Util.ShowMessage("用户登录超时，请重新登录！", "Login2.aspx");
            }
            else
            {
                if (Convert.ToInt16(Session["RoleID"]) > 4)
                {
                    Util.ShowMessage("对不起，你无权访问该页面！", "User_Center.aspx");
                }
                else
                {
                    UserID = Session["UserID"].ToString();

                    using (SqlConnection conn = new DB().GetConnection())
                    {
                        SqlDataReader rd;
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.CommandText = "select * from ResourceTypes";
                        conn.Open();
                        rd = cmd.ExecuteReader();
                        while (rd.Read())
                        {
                            ResourceTypes.Add(rd["Extension"].ToString(), rd["TypeName"].ToString());
                        }
                        rd.Close();
                        conn.Close();

                        /*
                        cmd.CommandText = "select * from  Resources where UserID = " + UserID + " order by id desc";
                        rd = cmd.ExecuteReader();
                        GridView1.DataSource = rd;
                        GridView1.DataBind();
                        rd.Close();
                        */

                        MyInit();
                        MyDataBind();
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
            cmd.CommandText = "select distinct typename as typename from ResourceTypes";
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            ResourceTypeDDL.DataSource = rd;
            ResourceTypeDDL.DataValueField = "typename";
            ResourceTypeDDL.DataTextField = "typename";
            ResourceTypeDDL.DataBind();
            rd.Close();
            ResourceTypeDDL.Items.Insert(0, new ListItem("所有资源类型", ""));

            cmd.CommandText = "Select * from ResourceFolders where ParentFolderID = '0' order by ID asc";
            rd = cmd.ExecuteReader();
            FolderDDL.DataSource = rd;
            FolderDDL.DataValueField = "ID";
            FolderDDL.DataTextField = "FolderName";
            FolderDDL.DataBind();
            rd.Close();
            FolderDDL.Items.Insert(0, new ListItem("所有文件夹", "-1"));

            // RoleID=1,2,3,4，分别对应Administrator,Editor,Contributor,Author
            int RoleID = Convert.ToInt16(Session["RoleID"].ToString());
            if (RoleID == 1 || RoleID == 2)
            {
                // 只有Administrator和Editor才可以看到所有作者的文章
                AuthorDDL.Items.Clear();
                cmd.CommandText = "select * from Users order by ID desc";
                rd = cmd.ExecuteReader();
                AuthorDDL.DataSource = rd;
                AuthorDDL.DataValueField = "ID";
                AuthorDDL.DataTextField = "UserName";
                AuthorDDL.DataBind();
                rd.Close();
                AuthorDDL.Items.Insert(0, new ListItem("所有用户", "-1"));
            }
            else
            {
                // Contributor和Author只能看到自己的文章
                AuthorDDL.Items.Clear();
                AuthorDDL.Items.Insert(0, new ListItem(Session["UserName"].ToString(), Session["UserID"].ToString()));
            }
        }
    }

    private void MyDataBind()
    {
        AspNetPager1.PageSize = Convert.ToInt16(PageSizeDDL.SelectedValue);
        string param = SearchTB.Text;
        StringBuilder whereStr = new StringBuilder(" where Valid=1 ");
        if (!String.IsNullOrEmpty(param))
        {
            whereStr.Append(" and [ResourceName] like '%").Append(Server.HtmlEncode(param.Trim().Replace("'", ""))).Append("%' ");
        }
        if (ResourceTypeDDL.SelectedIndex > 0)
        {
            whereStr.Append(" and FileType = '").Append(ResourceTypeDDL.SelectedValue).Append("'");
        }
        if (Convert.ToInt16(AuthorDDL.SelectedValue) > 0)
        {
            whereStr.Append(" and UserID = ").Append(AuthorDDL.SelectedValue);
        }
        if (Convert.ToInt16(FolderDDL.SelectedValue) > 0)
        {
            whereStr.Append(" and FolderID = ").Append(FolderDDL.SelectedValue);
        }

        string sql = "select count(ID) as total from Resources " + whereStr.ToString();

        using (SqlConnection conn = (SqlConnection)new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
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
                sql = "Select top " + AspNetPager1.PageSize + " * from Resources " + whereStr.ToString() + " " + OrderDDL.SelectedValue;
            }
            else
            {
                // Select Top 页容量 * from 表 where 条件 and id not in	(Select Top 页容量*（当前页数-1） id 	from 表 where 条件 order by 排序条件) order by 排序条件
                sql = "Select top " + AspNetPager1.PageSize + " * from Resources " + whereStr.ToString() + " and id not in ( select top " + AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1) + " id  from Resources " + whereStr.ToString() + " " + OrderDDL.SelectedValue + " ) " + OrderDDL.SelectedValue;
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


    protected void Insert_Click1(object sender, EventArgs e)
    {
        string ids = "";
        string filePath = "";
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox checkBox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
            if (checkBox.Checked == true && GridView1.DataKeys.Count != 0 && GridView1.DataKeys[i].Value != null)
            {
                ids = GridView1.DataKeys[i].Value.ToString();
            }
        }
        if (!String.IsNullOrEmpty(ids))
        {
            using (SqlConnection conn = new DB().GetConnection())
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Select * from [Resources] where ID = @ResourceID";
                cmd.Parameters.AddWithValue("@ResourceID", ids);
                conn.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {

                    filePath = rd["FilePath"].ToString();
                    ResourceTypeLabel.Text = rd["FileType"].ToString();
                    TextBox1.Text = rd["ResourceName"].ToString();
                }
                //将媒体库里选择的图片在页面中显示出来
                if (ResourceTypeLabel.Text.Equals("图片"))
                {
                    insertFileHTML = "<img src='" + filePath + "' width='640' />";
                }
                if (ResourceTypeLabel.Text.Equals("文档") || ResourceTypeLabel.Text.Equals("压缩"))
                {
                    insertFileHTML = "<a href=\'" + filePath + "\'>" + TextBox1.Text + "</a>";
                }

                //if (ResourceTypeLabel.Text.Equals("视频") || ResourceTypeLabel.Text.Equals("音频"))
                //{
                //  insertFileHTML = "<div id='mediaplayer' mediaURL='" + filePath + "'>Media Player</div>";
                //   insertFileHTML = "<object classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' height='480' width='640'><param name='movie' value='/ckeditor201507/plugins/jwplayer/jwplayer/player.swf' /><param name='allowfullscreen' value='true' /><param name='allowscriptaccess' value='always' /><param name='flashvars' value='file=/" + filePath + "&amp;autostart=false' /><embed allowfullscreen='true' allowscriptaccess='always' flashvars='file=/" + filePath + "&amp;autostart=false' height='480' id='player1' name='player1' src='/ckeditor201507/plugins/jwplayer/jwplayer/player.swf' width='640'></embed></object>";
                //}
                if (ResourceTypeLabel.Text.Equals("视频"))
                {
                    insertFileHTML = "<div class='jw-settings' id='jws" + new Random().Next(10, 100).ToString() + "'><video id='v"+new Random().Next(10, 100).ToString() + "' preload='none'><source src='" + filePath + "' type='video/mp4' /></video><div class='jw-modes'><div class='jw-mode' data-type='html5' style='display: none;'></div><div class='jw-mode' data-type='flash' style='display: none;'></div></div></div><p>&nbsp;</p>";
                    
                }
                if (ResourceTypeLabel.Text.Equals("音频"))
                {
                    insertFileHTML = "<div><audio src='" + filePath + "' controls='controls'>出错</audio></div><p>&nbsp;</p>";
                }
                if (ResourceTypeLabel.Text.Equals("Flash"))
                {
                    insertFileHTML = "<p><object classid='clsid:d27cdb6e-ae6d-11cf-96b8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0' height='480' width='640'><param name='allowFullScreen' value='false' /><param name='loop' value='false' /><param name='play' value='false' /><param name='quality' value='high' /><param name='scale' value='showall' /><param name='movie' value='" + filePath + "' /><embed allowfullscreen='false' height='480' loop='false' play='false' pluginspage='http://www.macromedia.com/go/getflashplayer' quality='high' scale='showall' src='" + filePath + "' type='application/x-shockwave-flash' width='640'></embed></object></p>";
                }
                conn.Close();
            }
            //执行前台js代码中已定义的ckeditor()插入函数（Asp.net 后台调用js方法）
            ClientScript.RegisterStartupScript(this.GetType(), "ckeditorscript", "<script> ckeditor();</script>");
        }
        
    }

    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        MyDataBind();
    }
    protected void AuthorDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        MyDataBind();
    }
    protected void ResourceTypeDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        MyDataBind();
    }
    protected void OrderDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        MyDataBind();
    }
    protected void PageSizeDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        MyDataBind();
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        MyDataBind();
    }
    protected void FolderDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        MyDataBind();
    }
    //protected void SelectAllCheckBox_CheckedChanged(object sender, EventArgs e)
    //{
    //    for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
    //    {
    //        CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
    //        if (SelectAllCheckBox.Checked == true)
    //        {
    //            cbox.Checked = true;
    //        }
    //        else
    //        {
    //            cbox.Checked = false;
    //        }
    //    }

    //}

}