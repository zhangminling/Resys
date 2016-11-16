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
using System.Data;
using System.Text.RegularExpressions;



public partial class Article_Add : System.Web.UI.Page
{
    
    //上传封面图后更新数据表Resources用到的数据
    string ResourceName = "";
    string extension = "";
    string physicalName = "";
    string fileName = "";
    static int variable = 2;
    static string files = "";
    static string filesize = "";

    public string RandomIDCD = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //LinkButton1.Attributes.Add("OnClick", "return  jsFunction()");
        Page.MaintainScrollPositionOnPostBack = true;
        if (!IsPostBack)
        {
            

            if (Session["RoleID"] == null || Session["UserID"] == null)
            {
                Util.ShowMessage("用户登录超时，请重新登录！", "Login.aspx");
            }
            else
            {   
                filereturn();
                file_clear();//File_Return();
                string UserID = Session["UserID"].ToString();
                string UserDict = "~/Users/" + UserID;
             
               
           using (SqlConnection conn = new DB().GetConnection())
           {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from [Users] where [ID] = @UserID ";
            cmd.Parameters.AddWithValue("@UserID", UserID);           
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
                          
                Session["TrueName"]=rd["TrueName"].ToString();
            }
                //string FullPath = UserDict + "/Uploads";
                if (!Directory.Exists(Server.MapPath(UserDict)))
                {
                    Directory.CreateDirectory(UserDict);
                }

               
             

                if (Request.QueryString["ID"] != null && !String.IsNullOrEmpty(Request.QueryString["ID"].ToString()))
                {
                    // 修改文章
                     string ArticleID = Request.QueryString["ID"];
                     int authorID = Util.permission(ArticleID);
                     IDLabel.Text = Request.QueryString["ID"].ToString();
                     if ((Convert.ToInt16(Session["RoleID"]) < 3 )|| (authorID == Convert.ToInt16(Session["UserID"])))
                     {
                         //选择哪个文章的ID   
                         MyInitForUpdate();//调用函数
                         files = "";
                         filesize = "";
                         File_Return();
                     }
                     else {
                        
                       Response.Write("<script language='javascript'> alert('你无法访问该篇文章' );window.location ='User_Center.aspx';</script>");
                       
                     }
                }
                else
                {
                    // 新增文章 
                    files = "";
                    filesize = "";
                    file_clear();
                    MyInitForAdd();
                    RandomID.Text = Guid.NewGuid().ToString();
                    RandomIDCD = RandomID.Text;

                    //RandomID.Text = "";//第一次加载页面，为空
                }

               // TagDataShow();
               
                 MyDataBind();
            }
            tag_return.Text = ",";
            foreach (ListItem item in TagsList.Items)
            {
                if (item.Selected)
                {
                    tag_return.Text += item.Text + ",";
                }
            }
        }
        Show_Tag();
        coverphotoshow();
        fileshow();
    }


    // --------文章标签操作------//

    private void Show_Tag() 
    {
        /*这里刷新标签*/
        int a = 0;
        foreach (ListItem item in TagsList.Items)
        {
            if (item.Selected)
            {
                a++;
                Label tag_show1 = new Label();
                tag_show1.CssClass = "label label-blueberry";
                tag_show1.Text = item.Text;
                LinkButton tag_close = new LinkButton();
                tag_close.Text = "x";
                tag_close.Style["color"] = "#000";
                tag_close.Click += new EventHandler(tagclose);
                Panel tag_shows = new Panel();
                tag_shows.CssClass = "label label-blueberry";
                tag_shows.Font.Size = FontUnit.Larger;
                //tag_shows.ID = item.Text;
                //tag_shows.Text = item.Text;
                tag_shows.Style["margin"] = "2px 5px";
                tag_shows.Controls.Add(tag_show1);
                tag_shows.Controls.Add(tag_close);
                Tag_Show.Controls.Add(tag_shows);

            }
        }
        /*这里刷新标签*/
    }
    //TagsLis显示所有的文章标签,并将原来有的打上勾。///
    private void MyDataBind()
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
             SqlCommand cmd = conn.CreateCommand();
            conn.Open();
             cmd.CommandText = "select * from  [ArticleTags]  order by ID desc  ";
            
            SqlDataReader rd = cmd.ExecuteReader();
            TagsList.DataSource = rd;
            TagsList.DataTextField = "TagName";
            TagsList.DataValueField = "ID";
            TagsList.DataBind();
          
            rd.Close();

            if (Request.QueryString["ID"] != null && !String.IsNullOrEmpty(Request.QueryString["ID"].ToString()))
            {
               /* SqlDataAdapter sda = new SqlDataAdapter("select * from [Articles_ArticleTags] where ArticleID = " + IDLabel.Text + " ", conn);
                // cmd.Parameters.AddWithValue("@ArticleID", IDLabel.Text);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string strTerm = ds.Tables[0].Rows[i]["ArticleTagName"].ToString();
                    string[] strTerms = strTerm.Split(',');
                    for (int p = 0; p < TagsList.Items.Count; p++)
                    {
                        if (strTerm == TagsList.Items[p].Text)
                        {
                            TagsList.Items[p].Selected = true;
                           
                            //  TextBox2.Text = TagsList.Items[p].Text;
                        }
                    }
                    TextBox2.Text = strTerm;
                    Label6.Text = ds.Tables[0].Rows.Count.ToString(); ;

                }*/

                string sql1 = "select count(*) as Maxrow from [Articles_ArticleTags] where ArticleID=@ArticleID";
                string sql2 = "select ArticleTagName from [Articles_ArticleTags] where ArticleID=@ArticleID";
                SqlCommand cmd1 = conn.CreateCommand();
                SqlCommand cmd2 = conn.CreateCommand();
                cmd1.Parameters.AddWithValue("@ArticleID", IDLabel.Text);
                cmd2.Parameters.AddWithValue("@ArticleID", IDLabel.Text);
                cmd1.CommandText = sql1;
                cmd2.CommandText = sql2;
               
                int count = int.Parse(Convert.ToString(cmd1.ExecuteScalar()));
              
                string[] TagName = new string[count];
                SqlDataReader rd2 = cmd2.ExecuteReader();
                for (int j = 0; j < count; j++)
                {
                    if (rd2.Read())
                    {
                        TagName[j] = rd2["ArticleTagName"].ToString();
                        for (int p = 0; p < TagsList.Items.Count; p++)
                        {
                            if (TagName[j] == TagsList.Items[p].Text)
                            {
                                TagsList.Items[p].Selected = true;

                                //  TextBox2.Text = TagsList.Items[p].Text;
                            }
                        }
                    }
                }
                rd2.Close();
            } 
             
        }
    
    }
    //修改时附件加载
    protected void File_Return()
    {
        files = "";
        string filesid = "";
        //IDLabel.Text = Request.QueryString["ID"].ToString();
         using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandText = "select * from  [Files] where ArticleID=@ArticleID";
            cmd.Parameters.AddWithValue("@ArticleID", Request.QueryString["ID"] != null ? Request.QueryString["ID"] : IDLabel.Text.ToString());
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read()) 
            {
                files += rd["FileName"].ToString()+"," ;
                filesize += rd["FileSizeInMB"].ToString() + ",";
                filesid += rd["FileID"].ToString() + ",";
            }
            rd.Close();
            string[] fileid1 = filesid.Split(',');
            using (SqlConnection conn1 = new DB().GetConnection())
            {
                SqlCommand cmd2 = conn.CreateCommand();
                for (int i = 0; i < fileid1.Length; i++)
                {
                    cmd2.CommandText = "update Resources set Status =1 where ID ='" + fileid1[i] + "'";
                    cmd2.ExecuteNonQuery();
                    cmd2.Dispose();
                }
            }
            conn.Close();
        }

    }



    //标签关闭按钮
    protected void tagclose(object sender, EventArgs e)
    {
        LinkButton b = (LinkButton)sender;
        Panel b1 = (Panel)b.Parent;
        foreach (Control a in b.Parent.Controls) 
        {
            if (a is Label)
            {
                Label a1 = (Label)a;
                foreach (ListItem item in TagsList.Items)
                {
                    if (item.Text==a1.Text)
                    {
                        Tag_Show.Controls.Remove(b1);
                        item.Selected = false;
                    }
                }
            }
        }
        tag_return.Text = ",";
        foreach (ListItem item in TagsList.Items)
        {
            if (item.Selected)
            {
                tag_return.Text += item.Text + ",";
            }
        }
        
    }



    //模态框中输入框的操作，对标签进行添加，选择 //
    private void ArticleTag()
    {  string[] ArTag = ArTagName.Text.Split(',','，',' ');

    int k = ArTagName.Text.Split(',', '，', ' ').Length;
           
          // string[] TagName = new string[100];        
                using (SqlConnection conn = new DB().GetConnection())
                {
                  
                    for (int j = 0; j < k; j++)
                    {
                        TextBox1.Text = ArTag[j].ToString();
                        SqlCommand cmd = conn.CreateCommand();
                        conn.Open();
                       cmd.CommandText = "select * from  [ArticleTags] where TagName=@TagName order by ID desc  ";                  
                       cmd.Parameters.AddWithValue("@TagName", TextBox1.Text);
                       SqlDataReader rd = cmd.ExecuteReader();
                 
                       if(!rd.Read())
                       {
                           using (SqlConnection conn1 = new DB().GetConnection())
                           {
                               StringBuilder sb = new StringBuilder("insert into ArticleTags(TagName,Description,Articles,UserID,UserName )");
                               sb.Append(" values ( @TagName,@Description,@Articles,@UserID,@UserName) ");
                               SqlCommand cmd1 = new SqlCommand(sb.ToString(), conn1);
                               cmd1.Parameters.AddWithValue("@TagName", TextBox1.Text);
                               cmd1.Parameters.AddWithValue("@Description", "无");
                               cmd1.Parameters.AddWithValue("@Articles", 1);                             
                               cmd1.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
                               cmd1.Parameters.AddWithValue("@UserName", Session["UserName"].ToString());


                               conn1.Open();
                               cmd1.ExecuteNonQuery();

                           }
                           MyDataBind();         
                       }                     
                       rd.Close();
                       conn.Close();
                }            
            }

                using (SqlConnection conn = new DB().GetConnection())
                {

                    for (int j = 0; j < k; j++)
                    {
                        TextBox1.Text = ArTag[j].ToString();
                        SqlCommand cmd = conn.CreateCommand();
                        conn.Open();
                        cmd.CommandText = "select * from  [ArticleTags] where TagName=@TagName order by ID desc  ";
                        cmd.Parameters.AddWithValue("@TagName", TextBox1.Text);
                        SqlDataReader rd = cmd.ExecuteReader();

                        if (rd.Read())
                        {

                            string text = string.Empty;
                            text = TextBox1.Text;

                            for (int p = 0; p < TagsList.Items.Count; p++)
                            {
                                if (text == TagsList.Items[p].Text)
                                {
                                    TagsList.Items[p].Selected = true;
                                                                      

                                  //  TextBox2.Text = TagsList.Items[p].Text;
                                }
                            }
                        }

                        rd.Close();
                        conn.Close();
                    }
                }
    






               //
          }



     
                         
  

    protected void AddArTag_Click(object sender, EventArgs e)
    {  
        string a=Regex.Replace(ArTagName.Text.ToString (), " ", "");
        if (a == "") { }
        else ArticleTag();
    }


    ///  将选择的标签 和 文章添加到关联表里/?///
    private void ArticleTagAdd()
    {
        string tags = "";
        for (int i = 0; i <= TagsList.Items.Count - 1; i++)
        {
            if (TagsList.Items[i].Selected == true)
            {
                tags += "," + TagsList.Items[i].Value;
            }

        }
        if (tags != "")
        {
            tags = tags.Substring(1);
            TagiIDs.Text = tags;   ///选择的“文章标签”对应的ID
            string[] array = tags.Split(',');
            int k = array.Length;
            //string[] ArticleID = new string[10];
            //string[] ArticleName = new string[10];
            string[] ArticleTagID = new string[10];
            string[] ArticleTagName = new string[10];
            string ArticleTitle = "";
            string ArticleID = "";
            int p;

            using (SqlConnection conn = new DB().GetConnection())
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from ArticleTags where ID in (" + TagiIDs.Text + ") order by ID desc  ";
                conn.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                for (p = 0; p <= k - 1; p++)
                {
                    if (rd.Read())
                    {
                        ArticleTagID[p] = rd["ID"].ToString();
                        ArticleTagName[p] = rd["TagName"].ToString();
                    }
                }
                rd.Close();
                conn.Close();



                //TagiIDs.Text = ArticleTagID[p];
                cmd.CommandText = "select * from Articles where ID =@ArticleID";
                cmd.Parameters.AddWithValue("@ArticleID", IDLabel.Text);
                conn.Open();
                SqlDataReader rd1 = cmd.ExecuteReader();
                if (rd1.Read())
                {
                    ArticleID = rd1["ID"].ToString();
                    ArticleTitle = rd1["Title"].ToString();
                    // Articles1 = rd1["Articles"].ToString();
                }
                rd1.Close();
                conn.Close();



                for (int j = 0; j <= k - 1; j++)
                {
                    SqlCommand cmd3 = conn.CreateCommand();
                    cmd3.CommandText = "select * from [Articles_ArticleTags] where ArticleID=@ArticleID and ArticleTagID=@ArticleTagID";
                    cmd3.Parameters.AddWithValue("@ArticleID", ArticleID);
                    cmd3.Parameters.AddWithValue("@ArticleTagID", ArticleTagID[j]);
                    conn.Open();
                    SqlDataReader rd3 = cmd3.ExecuteReader();
                    if (rd3.Read())
                    {
                        rd3.Close();
                        conn.Close();
                    }
                    else
                    {
                        rd3.Close();
                        conn.Close();
                        StringBuilder sb = new StringBuilder("insert into Articles_ArticleTags(ArticleID,Title,ArticleTagID,ArticleTagName )");
                        sb.Append(" values ( @ArticleID,@Title,@ArticleTagID,@ArticleTagName ) ");
                        SqlCommand cmd1 = new SqlCommand(sb.ToString(), conn);
                        cmd1.Parameters.AddWithValue("@ArticleID", ArticleID);
                        cmd1.Parameters.AddWithValue("@Title", ArticleTitle);
                        cmd1.Parameters.AddWithValue("@ArticleTagID", ArticleTagID[j]);
                        cmd1.Parameters.AddWithValue("@ArticleTagName", ArticleTagName[j]);
                        conn.Open();
                        cmd1.ExecuteNonQuery();
                        conn.Close();

                    }




                }

                cmd.CommandText = "Delete from Articles_ArticleTags where ArticleID=@ArticleID1 and ArticleTagID not in (" + TagiIDs.Text + ") ";
                cmd.Parameters.AddWithValue("@ArticleID1", ArticleID);
                conn.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();



            }
        }

        if (tags == "" && IDLabel.Text != "")
        {
            using (SqlConnection conn = new DB().GetConnection())
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Delete from Articles_ArticleTags where ArticleID=@ArticleID2 ";
                cmd.Parameters.AddWithValue("@ArticleID2", IDLabel.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
            }
        }
      
    }


    private void ArticleTagDel() 
    {
        string tags = "";
        for (int i = 0; i <= TagsList.Items.Count - 1; i++)
        {
            if (TagsList.Items[i].Selected == false)
            {
                tags += "," + TagsList.Items[i].Value;
            }

        }

    }


    protected void Clearn_Click(object sender, EventArgs e)
    {
        tag_return.Text = ",";
        Tag_Show.Controls.Clear();
        for (int i = 0; i <= TagsList.Items.Count - 1; i++)
        {
            if (TagsList.Items[i].Selected == true)
            {
                TagsList.Items[i].Selected = false;
            }
        }
    }




  

// --------文章标签操作结束------//



    private string Check()
    {
        int i = 0;
        string[] s = new string[6];
        s[0] = "";
        s[1] = "操作失败！ 文章标题不能为空！";
        s[2] = "操作失败！ 文章内容不能为空！";
        string TitleStr = TitleTB.Text;
        string Content=Editor1.Text;
        if ((!String.IsNullOrEmpty(TitleStr)) && (!String.IsNullOrEmpty(Content)))
        {
            
                DataBind();

        }
        if (String.IsNullOrEmpty(TitleStr))
        {
            i = 1;//第二种情况，用户名密码为空
        }
        if (String.IsNullOrEmpty(Content))
        {
            i = 2;
        }
        return s[i];
    }
    // 修改文章函数
    private void MyInitForUpdate()
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from Cats order by Orders desc";
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            Cats.DataSource = rd;
            Cats.DataValueField = "ID";
            Cats.DataTextField = "CatName";
            Cats.DataBind();
            rd.Close();



       


            int RoleID = Convert.ToInt16(Session["RoleID"].ToString());
            if (RoleID > 3)
            {
                cmd.Parameters.AddWithValue("@Status", 0);
                cmd.Parameters.AddWithValue("@StatusName", "未审核");//状态：新投稿/待审核=0，审核已过=1，审核未过=2            

            }
            else
            {
                cmd.Parameters.AddWithValue("@Status", 1);
                cmd.Parameters.AddWithValue("@StatusName", "通过审核");//状态：新投稿/待审核=0，审核已过=1，审核未过=2      
            }

            cmd.CommandText = "select max(Orders) as orders from Articles";
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                MaxOrders.Text = rd[0].ToString();
            }

            rd.Close();


            string SubID = "";
            string ClassID = "";
            string FirstAuthorID = "";
            cmd.CommandText = "select * from Articles where ID =" + IDLabel.Text;
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                TitleTB.Text = rd["Title"].ToString();
                Summary.Text=rd["Summary"].ToString();
                FirstAuthorID = rd["AuditorID"].ToString();
                string UserTagID = rd["TagID"].ToString();
                if (UserTag.Items.FindByValue(UserTagID) != null)
                {
                    UserTag.ClearSelection();
                    UserTag.Items.FindByValue(UserTagID).Selected = true;
                }

                string IsList1 = rd["IsList"].ToString();
           
                if (IsList1 == "False")
                {
                    IsList.Items.FindByValue("0").Selected = true;
                }
                else
                { IsList.Items.FindByValue("1").Selected = true; }

                string IsComment1 = rd["IsComment"].ToString();

                if (IsComment1 == "False")
                {
                    IsComment.Items.FindByValue("0").Selected = true;
                }
                else
                { IsComment.Items.FindByValue("1").Selected = true; }

                string CatID = rd["CatID"].ToString();
                if (Cats.Items.FindByValue(CatID) != null)
                {
                    Cats.ClearSelection();
                    Cats.Items.FindByValue(CatID).Selected = true;
                }

                SubID = rd["SubID"].ToString();                
                CDT_TextBox.Text = String.Format("{0:yyyy-MM-dd}", rd["CDT"]);
                Orders.Text = rd["Orders"].ToString();
                Editor1.Text = rd["Content"].ToString();
                CoverPhoto1.ImageUrl = rd["CoverImageURL"].ToString();      
                RandomID.Text = rd["RandomID"].ToString();
                RandomIDCD = RandomID.Text;
                int PC1 = Convert.ToInt32(rd["PC"]);
                if (PC1 == 1) { PC.Checked = true; }
                int Phone1 = Convert.ToInt32(rd["Phone"]);
                if (Phone1 == 1) { Phone.Checked=true; }
                int Wechat1 = Convert.ToInt32(rd["Wechat"]);
                if (Wechat1 == 1) { Wechat.Checked = true; }
                int iPad1 = Convert.ToInt32(rd["iPad"]);
                if (iPad1 == 1) { iPad.Checked = true; }
                int APP1 = Convert.ToInt32(rd["APP"]);
                if (APP1 == 1) { APP.Checked = true; }
                int TV1 = Convert.ToInt32(rd["TV"]);
                if (TV1 == 1) { TV.Checked = true; }

            }
            rd.Close();

            cmd.CommandText = "select * from Users_UserTags  where UserID=@UserID order by TagSelect desc";
            cmd.Parameters.AddWithValue("@UserID", FirstAuthorID);
            rd = cmd.ExecuteReader();
            UserTag.DataSource = rd;
            UserTag.DataTextField = "TagName";
            UserTag.DataValueField = "UserTagID";
            UserTag.DataBind();
            rd.Close();
           




            cmd.CommandText = "select * from Classes  where ArticleID =" + IDLabel.Text;
            rd = cmd.ExecuteReader();        
            if (rd.Read())
            {   ClassID = rd["ID"].ToString();
            if (Class.Items.FindByValue(ClassID) != null)
            {
                Class.ClearSelection();
                Class.Items.FindByValue(ClassID).Selected = true;
            }
            }
            rd.Close();


            cmd.CommandText = "select * from Subs where CatID = " + Cats.SelectedValue + " order by Orders desc";
            rd = cmd.ExecuteReader();
            Subs.DataSource = rd;
            Subs.DataTextField = "SubName";
            Subs.DataValueField = "ID";
            Subs.DataBind();
            rd.Close();



            cmd.CommandText = "select * from Classes order by ID desc";
            rd = cmd.ExecuteReader();
            Class.DataSource = rd;
            Class.DataTextField = "ClassName";
            Class.DataValueField = "ID";
            Class.DataBind();
            rd.Close();
            if (Convert.ToInt16(SubID) == 49)
            {
                Class.Visible = true;
                Class.ClearSelection();
                Class.Items.FindByValue(ClassID).Selected = true;
            }
            else { Class.Visible = false; }
          
 
            if (Subs.Items.Count > 0)
            {
                Subs.Visible = true;
                          
                if (Subs.Items.FindByValue(SubID) != null)
                {
                    Subs.ClearSelection();
                    Subs.Items.FindByValue(SubID).Selected = true;                   
                }
            }
            else
            {
                Subs.Visible = false;                        
            }
        }


       



    }

    private void MyInitForAdd()
    {
        CDT_TextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from Cats order by Orders desc";
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            Cats.DataSource = rd;
            Cats.DataValueField = "ID";
            Cats.DataTextField = "CatName";
            Cats.DataBind();
            rd.Close();


            cmd.CommandText = "select * from Users_UserTags  where UserID=@UserID order by  TagSelect desc";
            cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
            rd = cmd.ExecuteReader();
            UserTag.DataSource = rd;
            UserTag.DataTextField = "TagName";
            UserTag.DataValueField = "UserTagID";
            UserTag.DataBind();
            rd.Close();


            cmd.CommandText = "select max(Orders) as orders from Articles";
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                MaxOrders.Text = rd[0].ToString();
            }
            rd.Close();


            cmd.CommandText = "select * from Subs where CatID = " + Cats.SelectedValue + " order by Orders desc";
            rd = cmd.ExecuteReader();
            Subs.DataSource = rd;
            Subs.DataTextField = "SubName";
            Subs.DataValueField = "ID";
            Subs.DataBind();
            rd.Close();

       
            cmd.CommandText = "select * from Classes order by ID desc";
            rd = cmd.ExecuteReader();
            Class.DataSource = rd;
            Class.DataTextField = "ClassName";
            Class.DataValueField = "ID";
            Class.DataBind();
            rd.Close();
            
        }

    }

    

    protected void UploadButton_Click(object sender, EventArgs e)
    {
        CoverPhoto1.Visible = false;
        CoverPhoto.Visible = true;
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
                    string dir = "upload" + "/" + "CoverPhoto" + "/" + DateTime.Now.ToString("yyyyMM");

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
                        int a=0;
                        File.Delete(Server.MapPath(CoverPhoto.ImageUrl));
                        using (SqlConnection conn = new DB().GetConnection())
                        {
                            SqlCommand cmd = conn.CreateCommand();
                          
                            conn.Open();
                            cmd.CommandText = "select * from Resources order by ID desc";
                            SqlDataReader rd = cmd.ExecuteReader();
                            if (rd.Read())
                            {
                                a=int.Parse(rd["ID"].ToString());
                                a -= 1;
                                //cmd1.CommandText = "delete from Resources where ID=@ID";
                                //cmd1.Parameters.AddWithValue("@ID", a);
                                //cmd1.ExecuteNonQuery();

                            }
                            rd.Close();
                            //已在后面加多一条更新“封面图”文件夹下的资源数的操作
                            cmd.CommandText = "delete from Resources where id in(select top 1 id from Resources where id in( select top 2 id from Resources where UserID=@UserID order by id desc)order by id);Update ResourceFolders set Counts = Counts-1 where ID = 101";
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                            cmd.ExecuteNonQuery();
                            rd.Close();

                                 conn.Close();
                        }

                    }

                    CoverPhoto.ImageUrl = physicalName;
                    variable = 1;
                 
                 
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


   
   


    
  
    // 发表新文章
    protected void Button11_Click(object sender, EventArgs e)
    {
        Response.Redirect("Article_Add.aspx");
    }
   

    // 预览文章
    protected void Button2_Click(object sender, EventArgs e)
    {
        Session["Title1"] = TitleTB.Text;
        Session["Summary1"] = Summary.Text;
        Session["Content1"] = Editor1.Text;
        Session["DataTime1"] = CDT_TextBox.Text;
        Session["TagName"] = UserTag.SelectedItem.Text;

   //  Response.Redirect("Article_Preview.aspx");
         Response.Write("<script>window.open('Article_Preview.aspx','_blank')</script>");
        // ResultLabel.Text = Check();
        //ResultLabel.ForeColor = System.Drawing.Color.Red;
        //if (ResultLabel.Text == "")
        //{
        //    Article_Add1();
        //   // Response.Redirect("Article_View.aspx?RandomID=" + RandomID.Text);
        //   // Server.Transfer("Article_View.aspx?RandomID=" + RandomID.Text);
        //    Response.Write("<script>window.open('Article_View.aspx?RandomID=" + RandomID.Text + "','_blank')</script>");

          
        //}
    


    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        Response.Redirect("Article_Add.aspx");
    }

    // 保存并提交文章
    protected void Button3_Click(object sender, EventArgs e)
    {
        ResultLabel.Text = Check();
        ResultLabel.ForeColor = System.Drawing.Color.Red;
        if (ResultLabel.Text == "")
         {
             Article_Add1();
             ArticleTagAdd();
             Response.Redirect("Article_Add.aspx?ID=" + IDLabel.Text);

           
         }
        
    }


    private void Article_Add1()
    {
        if (!PC.Checked && !Phone.Checked && !Wechat.Checked && !iPad.Checked && !APP.Checked && !TV.Checked)
        {
            ResultLabel.Text = "至少选择一个发布位置！";
            ResultLabel.ForeColor = System.Drawing.Color.Red;
        }
        else
        {

            int i = 0;
            if (String.IsNullOrEmpty(IDLabel.Text))
            {
                //RandomID.Text = Guid.NewGuid().ToString();
                i = DoInsert(1);
               


            }
            else
            {
                i = DoUpdate(1);
                
            }

            if (i == 1)
            {
                ResultLabel.Text = "保存文章成功！";
                ResultLabel.ForeColor = System.Drawing.Color.Green;
            

                //Timer1.Enabled = true;            
            }
            else
            {
                ResultLabel.Text = "保存文章失败，请重试！";
                ResultLabel.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    // 保存草稿
    protected void Button1_Click(object sender, EventArgs e)
    {
         ResultLabel.Text = Check();
        ResultLabel.ForeColor = System.Drawing.Color.Red;
        if (ResultLabel.Text == "")
        {
            int i = 0;
            if (String.IsNullOrEmpty(IDLabel.Text))
            {
                //RandomID.Text = Guid.NewGuid().ToString();
                i = DoInsert(0);
            }
            else
            {
                i = DoUpdate(0);
            }

            if (i == 1)
            {
                ResultLabel.Text = "保存草稿成功！";
                ResultLabel.ForeColor = System.Drawing.Color.Green;

            }
            else
            {
                ResultLabel.Text = "保存草稿失败，请重试！";
                ResultLabel.ForeColor = System.Drawing.Color.Red;
            }
        }
    }





    // 插入操作
    private int DoInsert(int finished)
    {

        string[] str1 = files.Split(',');
        int i = 0;
        int SubID = 0;
        String SubName = "";
        string cpimgurl;
         if (  variable==2) { 
             cpimgurl = CoverPhoto1.ImageUrl;
       
         }
         else cpimgurl = CoverPhoto.ImageUrl;
  
        if (Subs.Items.Count > 0 && Subs.SelectedValue != null)
        {
            SubID = Convert.ToInt16(Subs.SelectedValue);
            SubName = Subs.SelectedItem.Text;
        }

        String cdt = DateTime.Now.ToString();
        if (!String.IsNullOrEmpty(CDT_TextBox.Text.Trim()))
        {
            cdt = CDT_TextBox.Text;
        }
        //插入到校园网的表
        using (SqlConnection conn = new DB().GetConnection())
        {


            StringBuilder sb = new StringBuilder("Insert into Articles ( Title,Content,Summary,CatID,CatName,SubID,SubName,AuthorID,Author,TagName,TagID,CDT,LDT,Status,Valid,AuditorID,Auditor,AuditedDateTime,ViewTimes,ReviewTimes,Orders,Finished,RandomID,CoverImageURL,StatusName,PC,Phone,Wechat,iPad,APP,TV,IsList,IsComment) ");
            sb.Append(" values ( @Title,@Content,@Summary,@CatID,@CatName,@SubID,@SubName,@AuthorID,@Author,@TagName,@TagID,@CDT,@LDT,@Status,@Valid,@AuditorID,@Auditor,@AuditedDateTime,@ViewTimes,@ReviewTimes,@Orders,@Finished,@RandomID,@CoverImageURL,@StatusName,@PC,@Phone,@Wechat,@iPad,@APP,@TV,@IsList,@IsComment) ");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.AddWithValue("@Title", TitleTB.Text);
            cmd.Parameters.AddWithValue("@IsList", IsList.SelectedValue);
            cmd.Parameters.AddWithValue("@IsComment", IsComment.SelectedValue);
            cmd.Parameters.AddWithValue("@Content", Editor1.Text);
            cmd.Parameters.AddWithValue("@CatID", Cats.SelectedValue);
            cmd.Parameters.AddWithValue("@CatName", Cats.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@SubID", SubID);
            cmd.Parameters.AddWithValue("@SubName", SubName);
            cmd.Parameters.AddWithValue("@AuthorID", Session["UserID"].ToString());
            if (Session["TrueName"].ToString() =="")
            {
                cmd.Parameters.AddWithValue("@Author", Session["UserName"].ToString());
            }
            else { cmd.Parameters.AddWithValue("@Author", Session["TrueName"].ToString()); }
           
            cmd.Parameters.AddWithValue("@CDT", cdt);
            cmd.Parameters.AddWithValue("@LDT", DateTime.Now);

            if (UserTag.Items.Count > 0)
            {
                cmd.Parameters.AddWithValue("@TagName", UserTag.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@TagID", UserTag.SelectedValue);
            }
            else {
                cmd.Parameters.AddWithValue("@TagName"," ");
                cmd.Parameters.AddWithValue("@TagID", 0);
            }


            if (cpimgurl == "")
            {
                cpimgurl = "upload/CoverPhoto/school.jpg";
                cmd.Parameters.AddWithValue("@CoverImageURL", cpimgurl);
   
            }
            else {
                cmd.Parameters.AddWithValue("@CoverImageURL", cpimgurl);
           }
          
            if (Summary.Text == "")
            {
                string str = "";
                string contenttext = Editor1.Text.Substring(0, Editor1.Text.LastIndexOf("</"));
                str = contenttext;
                str = Regex.Replace(str, "<br />", "  ");
                string regexstr = @"<[^>]*>";
                str = Regex.Replace(str, regexstr, string.Empty, RegexOptions.IgnoreCase);
                Regex reg = new Regex(@"(?i)(&nbsp;)+");
                str = reg.Replace(str, " ");
                str = Regex.Replace(str, "&gt;", ">");
                str = Regex.Replace(str, "&lt;", "<");
                str = Regex.Replace(str, "&frasl;", "/");
                str = Regex.Replace(str, "&sim;", "~");
                str = Regex.Replace(str, "&laquo;", "《");
                str = Regex.Replace(str, "&lsquo;", "'");
                str = Regex.Replace(str, "&ldquo;", "''");
                str = Regex.Replace(str, "&hellip;", "…");
                str = Regex.Replace(str, "#39;", "'");
                str = Regex.Replace(str, "&amp;", "&");
                str = Regex.Replace(str, "&mdash;", "—");
                str = Regex.Replace(str, "&quot;", "''");
                if (str.Length < 30)
                {
                    //Summary.Text = Editor1.Text.Substring(0, Editor1.Text.LastIndexOf(">"));
                    
                    Summary.Text = str;

                }

                else
                {
                    Summary.Text = str.Substring(0,30);
                }

                cmd.Parameters.AddWithValue("@Summary", Summary.Text);

            }
            else { 
               cmd.Parameters.AddWithValue("@Summary", Summary.Text);

            }


            // RoleID={1,Administrator},{2,Editor},{3,Contributor},{4,Author}
             int RoleID = Convert.ToInt16(Session["RoleID"].ToString());
            if (RoleID > 3)
            {
                cmd.Parameters.AddWithValue("@Status", 0); //状态：新投稿/待审核=0，审核已过=1，审核未过=2            
                cmd.Parameters.AddWithValue("@StatusName", "未审核");
                cmd.Parameters.AddWithValue("@AuditorID", 0);
                cmd.Parameters.AddWithValue("@Auditor", "无");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Status", 1); //状态：新投稿/待审核=0，审核已过=1，审核未过=2   
                cmd.Parameters.AddWithValue("@StatusName", "通过审核");
                cmd.Parameters.AddWithValue("@AuditorID", Session["UserID"].ToString());
                cmd.Parameters.AddWithValue("@Auditor", Session["UserName"].ToString());
            }
           
            cmd.Parameters.AddWithValue("@Valid", 1);
            
            cmd.Parameters.AddWithValue("@AuditedDateTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@ViewTimes", 0);
            cmd.Parameters.AddWithValue("@ReviewTimes", 0);
            cmd.Parameters.AddWithValue("@Orders", Orders.Text);
           
           
            cmd.Parameters.AddWithValue("@Finished", finished);
            //RandomID.Text = Guid.NewGuid().ToString();
            cmd.Parameters.AddWithValue("@RandomID", RandomID.Text);
            if (PC.Checked) { cmd.Parameters.AddWithValue("@PC", 1); } else { cmd.Parameters.AddWithValue("@PC", 0); }
            if (Phone.Checked) { cmd.Parameters.AddWithValue("@Phone", 1); } else { cmd.Parameters.AddWithValue("@Phone", 0); }
            if (Wechat.Checked) { cmd.Parameters.AddWithValue("@Wechat", 1); } else { cmd.Parameters.AddWithValue("@Wechat", 0); }
            if (iPad.Checked) { cmd.Parameters.AddWithValue("@iPad", 1); } else { cmd.Parameters.AddWithValue("@iPad", 0); }
            if (APP.Checked) { cmd.Parameters.AddWithValue("@APP", 1); } else { cmd.Parameters.AddWithValue("@APP", 0); }
            if (TV.Checked) { cmd.Parameters.AddWithValue("@TV ", 1); } else { cmd.Parameters.AddWithValue("@TV", 0); }
            conn.Open();
            i = cmd.ExecuteNonQuery();
            variable = 2;
     

        }
        //同时数据插入到微信的表
        using (SqlConnection conn = new DB().GetConnection())
        {

            StringBuilder sb = new StringBuilder("Insert into Articles2 ( Title,Content,CatID,CatName,SubID,SubName,AuthorID,Author,CDT,LDT,Status,AuditorID,Auditor,AuditedDateTime,ViewTimes,ReviewTimes,Orders,Finished,RandomID) ");
            sb.Append(" values ( @Title,@Content,@CatID,@CatName,@SubID,@SubName,@AuthorID,@Author,@CDT,@LDT,@Status,@AuditorID,@Auditor,@AuditedDateTime,@ViewTimes,@ReviewTimes,@Orders,@Finished,@RandomID) ");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.AddWithValue("@Title", TitleTB.Text);
            cmd.Parameters.AddWithValue("@Content", Editor1.Text);
            cmd.Parameters.AddWithValue("@CatID", Cats.SelectedValue);
            cmd.Parameters.AddWithValue("@CatName", Cats.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@SubID", SubID);
            cmd.Parameters.AddWithValue("@SubName", SubName);
            cmd.Parameters.AddWithValue("@AuthorID", Session["UserID"].ToString());
            cmd.Parameters.AddWithValue("@Author", Session["UserName"].ToString());
            cmd.Parameters.AddWithValue("@CDT", cdt);
            cmd.Parameters.AddWithValue("@LDT", DateTime.Now);
            cmd.Parameters.AddWithValue("@Status", 1);
            cmd.Parameters.AddWithValue("@AuditorID", Session["UserID"].ToString());
            cmd.Parameters.AddWithValue("@Auditor", Session["UserName"].ToString());
            cmd.Parameters.AddWithValue("@AuditedDateTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@ViewTimes", 0);
            cmd.Parameters.AddWithValue("@ReviewTimes", 0);
            cmd.Parameters.AddWithValue("@Orders", 1);
            cmd.Parameters.AddWithValue("@Finished", finished);
            cmd.Parameters.AddWithValue("@RandomID", RandomID.Text);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        
     }
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from Articles where RandomID = @RandomID";
            cmd.Parameters.AddWithValue("@RandomID",RandomID.Text);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                IDLabel.Text = rd["ID"].ToString();
            }
            rd.Close();
            conn.Close();
        }
        //如果为SubName为花名册，同时更新到Class表里
        using (SqlConnection conn = new DB().GetConnection())
        {
            StringBuilder sb1 = new StringBuilder("Update Classes set ArticleID=@ArticleID where ID=@ClassID ");
            SqlCommand cmd1 = new SqlCommand(sb1.ToString(), conn);
            cmd1.Parameters.AddWithValue("@ClassID", Class.SelectedValue);
            cmd1.Parameters.AddWithValue("@ArticleID", IDLabel.Text);
            conn.Open();
            cmd1.ExecuteNonQuery();
        }
        //关联文章与附件
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Delete from Files where ArticleID ='" + IDLabel.Text + "'";
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            for (int j = 0;j<200; j++)
            {
                if (str1[j] == null || str1[j] == "") break;
                else
                {
                    cmd.CommandText = "select * from Resources where ResourceName = '" + str1[j]+"' order by id desc";
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        cmd.CommandText = "Insert into Files (FileID,FileName,FileURL,ArticleID,FileSizeInMB)values ('" + rd["ID"] + "','" + str1[j] + "','" + rd["FilePath"] + "','" + IDLabel.Text + "','" + rd["FileSizeInKB"] + "')";
                        rd.Close();
                        cmd.ExecuteNonQuery();
                    }
                    rd.Close();
                }
            }
        }
     
        return i;
        
    }
    // 更新操作
    private int DoUpdate(int finished)
    {
        string[] str1 = files.Split(',');
        int i = 0;
        int SubID = 0;
        int TagID = 0;
        String SubName = "";
        String TagName = "";
        string cpimgurl;
        if ( variable == 2) { cpimgurl = CoverPhoto1.ImageUrl;

        }
        else cpimgurl = CoverPhoto.ImageUrl;
        if (Subs.Items.Count > 0 && Subs.SelectedValue != null)
        {
            SubID = Convert.ToInt16(Subs.SelectedValue);
            SubName = Subs.SelectedItem.Text;
        }
        if (UserTag.Items.Count > 0 && UserTag.SelectedValue != null)
        {
            TagID = Convert.ToInt16(UserTag.SelectedValue);
            TagName = UserTag.SelectedItem.Text;
        }
        //更新校园网的数据
        using (SqlConnection conn = new DB().GetConnection())
        {


            StringBuilder sb = new StringBuilder("Update Articles set Title = @Title,Content=@Content,Summary=@Summary,CatID=@CatID,CatName=@CatName,SubID=@SubID,AuditorID=@AuditorID,Auditor=@Auditor,SubName=@SubName,UpdateAuthorID=@UpdateAuthorID,UpdateAuthor=@UpdateAuthor,CoverImageURL=@CoverImageURL,TagName=@TagName,TagID=@TagID,CDT=@CDT,LDT=@LDT,Orders=@Orders,Status=@Status,StatusName=@StatusName,Finished=@Finished,PC=@PC,Phone=@Phone,Wechat=@Wechat,iPad=@iPad,APP=@APP,TV=@TV,IsList=@IsList,IsComment=@IsComment where RandomID = @RandomID  ");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.AddWithValue("@Title", TitleTB.Text);
            cmd.Parameters.AddWithValue("@Content", Editor1.Text);
            cmd.Parameters.AddWithValue("@Summary", Summary.Text); 
            cmd.Parameters.AddWithValue("@CatID", Cats.SelectedValue);
            cmd.Parameters.AddWithValue("@CatName", Cats.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@SubID", SubID);
            cmd.Parameters.AddWithValue("@SubName", SubName);
            cmd.Parameters.AddWithValue("@IsList", IsList.SelectedValue);
            cmd.Parameters.AddWithValue("@IsComment", IsComment.SelectedValue);
            cmd.Parameters.AddWithValue("@UpdateAuthorID", Session["UserID"].ToString());
            cmd.Parameters.AddWithValue("@UpdateAuthor", Session["UserName"].ToString());

            cmd.Parameters.AddWithValue("@TagID", TagID);
            cmd.Parameters.AddWithValue("@TagName", TagName);
            cmd.Parameters.AddWithValue("@CoverImageURL", cpimgurl);
            cmd.Parameters.AddWithValue("@Orders", Orders.Text);

            cmd.Parameters.AddWithValue("@CDT", CDT_TextBox.Text);
            cmd.Parameters.AddWithValue("@LDT", DateTime.Now);
            // RoleID={1,Administrator},{2,Editor},{3,Contributor},{4,Author}            
            int RoleID = Convert.ToInt16(Session["RoleID"].ToString());
            if (RoleID > 3)
            {
                cmd.Parameters.AddWithValue("@Status", 0); //状态：新投稿/待审核=0，审核已过=1，审核未过=2            
                cmd.Parameters.AddWithValue("@StatusName", "未审核");
                cmd.Parameters.AddWithValue("@AuditorID", 0);
                cmd.Parameters.AddWithValue("@Auditor", "无");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Status", 1); //状态：新投稿/待审核=0，审核已过=1，审核未过=2      
                cmd.Parameters.AddWithValue("@StatusName", "通过审核");
                cmd.Parameters.AddWithValue("@AuditorID", Session["UserID"].ToString());
                cmd.Parameters.AddWithValue("@Auditor", Session["UserName"].ToString());
            }

            if (PC.Checked) { cmd.Parameters.AddWithValue("@PC", 1); } else { cmd.Parameters.AddWithValue("@PC", 0); }
            if (Phone.Checked) { cmd.Parameters.AddWithValue("@Phone", 1); } else { cmd.Parameters.AddWithValue("@Phone", 0); }
            if (Wechat.Checked) { cmd.Parameters.AddWithValue("@Wechat", 1); } else { cmd.Parameters.AddWithValue("@Wechat", 0); }
            if (iPad.Checked) { cmd.Parameters.AddWithValue("@iPad", 1); } else { cmd.Parameters.AddWithValue("@iPad", 0); }
            if (APP.Checked) { cmd.Parameters.AddWithValue("@APP", 1); } else { cmd.Parameters.AddWithValue("@APP", 0); }
            if (TV.Checked) { cmd.Parameters.AddWithValue("@TV ", 1); } else { cmd.Parameters.AddWithValue("@TV", 0); }
                
            //cmd.Parameters.AddWithValue("@Orders", 1);
            cmd.Parameters.AddWithValue("@Finished", finished);
            cmd.Parameters.AddWithValue("@RandomID", RandomID.Text);
            conn.Open();
            i = cmd.ExecuteNonQuery();
            variable = 2;
         
        }
        //同时更新数据库Articles_ArticleTags表
        using (SqlConnection conn = new DB().GetConnection())
        {
            StringBuilder sb1 = new StringBuilder("Update Articles_ArticleTags set Title=@Title where ArticleID=@ArticleID");
            SqlCommand cmd1 = new SqlCommand(sb1.ToString(), conn);
            cmd1.Parameters.AddWithValue("@Title",TitleTB.Text );
            cmd1.Parameters.AddWithValue("@ArticleID", IDLabel.Text);
            conn.Open();
            cmd1.ExecuteNonQuery();    
        }

        //如果为SubName为花名册，同时更新到Class表里
        using (SqlConnection conn = new DB().GetConnection())
        {
            StringBuilder sb1 = new StringBuilder("Update Classes set ArticleID=@ArticleID where ID=@ClassID ");
            SqlCommand cmd1 = new SqlCommand(sb1.ToString(), conn);
            cmd1.Parameters.AddWithValue("@ClassID", Class.SelectedValue);
            cmd1.Parameters.AddWithValue("@ArticleID", IDLabel.Text);
            conn.Open();
            cmd1.ExecuteNonQuery();
        }
        //关联文章与附件
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Delete from Files where ArticleID ='"+IDLabel.Text+"'";
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            for (int j = 0; j < 200; j++)
            {
                if (str1[j] == null || str1[j] == "") break;
                else
                {
                    cmd.CommandText = "select * from Resources where ResourceName = '" + str1[j] + "' order by id desc";
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        cmd.CommandText = "Insert into Files (FileID,FileName,FileURL,ArticleID,FileSizeInMB)values ('" + rd["ID"] + "','" + str1[j] + "','" + rd["FilePath"] + "','" + IDLabel.Text + "','" + rd["FileSizeInKB"] + "')";
                        rd.Close();
                        cmd.ExecuteNonQuery();
                    }
                    rd.Close();
                }
            }
        }
        //同时更新微信的数据
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
            cmd.Parameters.AddWithValue("@Orders", 1);
            cmd.Parameters.AddWithValue("@Finished", finished);
            cmd.Parameters.AddWithValue("@RandomID", RandomID.Text);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
     
        return i;
    }

    protected void Cats_SelectedIndexChanged(object sender, EventArgs e)
    {
        int CatID = Convert.ToInt16(Cats.SelectedValue);
        int IsShow = 0;
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from Subs where CatID = " + Cats.SelectedValue + " order by Orders desc";
            conn.Open();
            SqlDataReader rd = null;
            rd = cmd.ExecuteReader(); 
            Subs.DataSource = rd;
            Subs.DataTextField = "SubName";
            Subs.DataValueField = "ID";
            Subs.DataBind();
            rd.Close();

            cmd.CommandText = "select * from Cats where ID = @ID";
            cmd.Parameters.AddWithValue("@ID", Cats.SelectedValue);
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                IsShow = Convert.ToInt16(rd["IsShow"]);
            }
            rd.Close();

            if (IsShow == 1)
            {
                Warning.Visible = true;
            }
            else
            {
                Warning.Visible = false;
            }

        }

        if (Subs.Items.Count > 0)
        {
            Subs.Visible = true;

        }
        else
        {
            Subs.Visible = false;

        }
        Class.Visible = false;
        
    }

    protected void Subs_SelectedIndexChanged(object sender, EventArgs e)
    {
       // int CatID = Convert.ToInt16(Cats.SelectedValue);
        int SubID = Convert.ToInt16(Subs.SelectedValue);
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from Classes order by ID desc";
            conn.Open();
            SqlDataReader rd = null;
            rd = cmd.ExecuteReader();
            Class.DataSource = rd;
            Class.DataTextField = "ClassName";
            Class.DataValueField = "ID";
            Class.DataBind();
            rd.Close();
        }
        if (SubID == 5)
        {
            Warning.Visible = true;
        }
        else
        {
            Warning.Visible = false;
        }
        if ( SubID == 49)
        {
            Class.Visible = true;

        }
        else
        {
            Class.Visible = false;

        }         
    
    }
    protected void UserTag_SelectedIndexChanged(object sender, EventArgs e)
    {
        int UserTagID = Convert.ToInt16(UserTag.SelectedValue);
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            SqlCommand cmd1 = conn.CreateCommand(); ;
            conn.Open()   ;

            cmd.CommandText = "Update Users_UserTags set TagSelect = 1 where UserTagID = @ID and UserID=@UserID";
            cmd1.CommandText = "Update Users_UserTags set TagSelect = 0 where UserTagID <> @ID2 and UserID=@UserID2";
            cmd.Parameters.AddWithValue("@ID", UserTagID);   
            cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());;
            cmd1.Parameters.AddWithValue("@ID2", UserTagID);
            cmd1.Parameters.AddWithValue("@UserID2", Session["UserID"].ToString()); ;
            
            cmd.ExecuteNonQuery();
            cmd1.ExecuteNonQuery();
               
            
        }

    }






    protected void Button6_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        Button6.BackColor = System.Drawing.ColorTranslator.FromHtml("#f0f0f0");
        Button7.BackColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
        Button5.BackColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
        Button6.BackColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
        Button7.BackColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
        Button5.BackColor = System.Drawing.ColorTranslator.FromHtml("#f0f0f0");
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 2;
        Button6.BackColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
        Button7.BackColor = System.Drawing.ColorTranslator.FromHtml("#f0f0f0");
        Button5.BackColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
         tag_return.Text = ",";
         TagiIDs.Text = "";
         foreach (ListItem item in TagsList.Items)
         {
             if (item.Selected)
             {
                 tag_return.Text += item.Text + ",";
                 TagiIDs.Text += item.Value + ",";
             }
         }
    }
    private void close_tag() 
    {
        TagsList.ClearSelection();
        string[] Tagreturn = tag_return.Text.ToString().Split(',');
        for (int i = 0; i < Tagreturn.Length; i++)
        {
            if (Tagreturn[i] != "")
            {
                foreach (ListItem item in TagsList.Items)
                {
                    if (item.Text == Tagreturn[i])
                    {
                        item.Selected = true;
                    }
                }
            }
        }
        Tag_Show.Controls.Clear();
        Show_Tag();
        //Response.Write(a);
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        close_tag();
    }
    protected void Unnamed1_Click(object sender, EventArgs e)
    {
        close_tag();
    }
    protected void Button10_Click(object sender, EventArgs e)
    {
        Cover.Style["display"] = "block";
    }

    private void coverphotoshow()
    {
        int a = 0;
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandText = "select * from  [Resources] where FolderName='封面图' order by ID desc  ";
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read()) 
            {
                a++;
                ImageButton img1 = new ImageButton();
                img1.BorderStyle = BorderStyle.None;
                img1.Width = Unit.Percentage(25);
                img1.Height = Unit.Percentage(25);
                img1.Style["padding"] = "3px";
                img1.ImageUrl = rd["FilePath"].ToString();
                img1.ID = "im" + a.ToString();
                img1.Click += new ImageClickEventHandler(coverphoto_return);
                coverphotos.Controls.Add(img1);
            }
            rd.Close();
        }

    }

    protected void coverphoto_return(object sender, EventArgs e)
    {
        ImageButton ib = (ImageButton)sender;
        //Response.Write("<script>alert('"+ib.ImageUrl.ToString()+"');</script>");
        Cover.Style["display"] = "none";
        CoverPhoto1.ImageUrl = ib.ImageUrl;
        variable = 2;
        CoverPhoto1.Visible = true;
        CoverPhoto.Visible = false;
    }

    protected void EscCoverShow_Click(object sender, EventArgs e)
    {
        Cover.Style["display"] = "none";

    }

    protected bool InsertDataBase1()
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
            cmd.Parameters.AddWithValue("@FileType", "附件");
            cmd.Parameters.AddWithValue("@Extentsion", extension);
            cmd.Parameters.AddWithValue("@FolderID", 128);
            cmd.Parameters.AddWithValue("@FolderName", "附件");
            cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
            cmd.Parameters.AddWithValue("@UserName", Session["UserName"].ToString());
            cmd.Parameters.AddWithValue("@CDT", DateTime.Now);
            cmd.Parameters.AddWithValue("@Status", 0);
            cmd.Parameters.AddWithValue("@Valid", 1);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            conn.Open();
            cmd.CommandText = "SELECT count(*) from Resources where FolderID =128";
            int count = int.Parse(Convert.ToString(cmd.ExecuteScalar()));
            cmd.CommandText = "Update ResourceFolders set Counts = " + count.ToString() + " where ID = 128";
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            //插入成功
            result = true;
        }
        return result;

    }
    private void fileshow() 
    {
        int j=1;
        string[] str = files.Split(',');
        string[] strsize = filesize.Split(',');
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == "" || str[i] == null) break;
            Panel p1 = new Panel();
            p1.ID = "file_panel" + i.ToString();
            p1.Style["padding"] = "1px 10px";
           // p1.Style["background-color"] = "#8aac82";
            Label im1 = new Label();
            im1.CssClass = "menu-icon glyphicon glyphicon-paperclip";
            im1.ID = "fileaddimg" + i.ToString();
            Label l2 = new Label();
            l2.Text = "&nbsp;&nbsp;&nbsp;附件" + j.ToString() + "：";
            LinkButton file1 = new LinkButton();
            file1.ID = "file" + i.ToString();
            file1.Text = str[i];
            Label l1 = new Label();
            l1.Text = " ( "+strsize[i].ToString()+"KB ) ";
            l1.Style["color"] = "#080808";
            LinkButton file_close = new LinkButton();
            //file_close.Style["float"] = "right";
            file_close.ID = "file_close" + i.ToString();
            file_close.Text = "删除";
            //file_close.Style["float"] = "right";
            file_close.Click += File_Close;
            p1.Controls.Add(im1);
            p1.Controls.Add(l2);
            p1.Controls.Add(file1);
            p1.Controls.Add(l1);
            p1.Controls.Add(file_close);
            File_Box.Controls.Add(p1);
            j++;
        }
    }
    protected void File_Close(object sender, EventArgs e) 
    {
        string sql = "";
        LinkButton b = (LinkButton)sender;
        foreach (Control cont in b.Parent.Controls)
        {
            if (cont is LinkButton) { 
            if (((LinkButton)cont).Text != "删除")
            {
                using (SqlConnection conn = new DB().GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    sql = "Update [Resources] set Status=@Status where ResourceName = @ResourceName";
                    cmd = new SqlCommand(sql.ToString(), conn);
                    cmd.Parameters.AddWithValue("@Status", "0");
                    cmd.Parameters.AddWithValue("@ResourceName", ((LinkButton)cont).Text.ToString());
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            }
        }
        File_Box.Controls.Clear();
        filereturn();
        fileshow();
    }
    protected void filereturn()
    {
        files = "";
        filesize = "";
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from [Resources] where [UserID] = @UserID and Status=1 ";
            cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                files += rd["ResourceName"].ToString() + ",";
                filesize += rd["FileSizeInKB"].ToString() + ",";
            }
            rd.Close();
            conn.Close();
        }
    }
    protected void file_clear()
    {
        string sql;
        using (SqlConnection conn = new DB().GetConnection())
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            sql = "Update [Resources] set Status=0 where UserName = @UserName and Status=1";
            cmd = new SqlCommand(sql.ToString(), conn);
            cmd.Parameters.AddWithValue("@Status", "0");
            cmd.Parameters.AddWithValue("@UserName", Session["UserName"].ToString());
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }
    }
    protected void file_chose_Click(object sender, EventArgs e)
    {
        files_add.Style["display"] = "block";
    }
    protected void file_sure_Click(object sender, EventArgs e)
    {
        files_add.Style["display"] = "none";
        string FNText = Util.GetFileName();
        //return_file1 = Util.GetFileName();
        string[] array = FNText.Split(',');
        int k = array.Length;
        int s = k - 1;
        int i = 0;
        string sql = "";
        using (SqlConnection conn = new DB().GetConnection())
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            for (int num = 0; num < k; num++)
            {
                sql = "Update [Resources] set FolderID=@FolderID,FolderName=@FolderName,Status=@Status where FileName = @FileName";
                cmd = new SqlCommand(sql.ToString(), conn);
                cmd.Parameters.AddWithValue("@FileName", array[num]);
                cmd.Parameters.AddWithValue("@FolderID", "128");
                cmd.Parameters.AddWithValue("@Status", "1");
                cmd.Parameters.AddWithValue("@FolderName", "附件");
                cmd.ExecuteNonQuery();
            }
            Util.CleanFileName();
            conn.Close();
            conn.Open();
            cmd.CommandText = "Update ResourceFolders set Counts = Counts - " + s.ToString() + " where ID = 96;Update ResourceFolders set Counts = Counts + " + s.ToString() + " where ID = @ID";
            cmd.Parameters.AddWithValue("@ID", "128");
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
        filereturn();
        File_Box.Controls.Clear();
        fileshow();
    }
    protected void File_Close_Click(object sender, EventArgs e)
    {
        files_add.Style["display"] = "none";
    }


}
