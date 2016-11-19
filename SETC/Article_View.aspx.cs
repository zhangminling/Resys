using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using Winsteps.Validator;
using System.Configuration;
using System.Text;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;

public partial class Article_View : System.Web.UI.Page
{
    static string USERIP = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        USERIP = System.Web.HttpContext.Current.Request.UserHostAddress;
        if (!IsPostBack)
        {
            User_id.Text = Session["UserID"] == null ? "" : Session["UserID"].ToString();//提取UserID
            //客户端弹出验证消息框
            //WinVal WinValInstance = new WinVal();
            //WinValInstance.ValStyle = "toppoptip";
            //WinValInstance.SetValidator();

            AbsoluteUrl.Text = Request.Url.AbsoluteUri;
            string con = PageOperate.GetNullToString(AbsoluteUrl.Text.Trim());
            if (con == "")
            {
                PageOperate.AlertAndRedirect("请填写内容", "Build.aspx");
                return;
            }
            if (ImgCode.ImageUrl == "")
            {
                ImgCode.ImageUrl = "Handler.ashx?data=" + Server.HtmlEncode(con) + "&len=4";
            }

            Random r = new Random();
            Image1.ImageUrl = "images/random/V" + (r.Next(12) + 1) + ".jpg";
            if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                ArticleID.Text = Request.QueryString["ID"].Trim();
                MyInit();
            }

        }
        DBView();
        System.Threading.Thread.Sleep(500);
    }

    private void MyInit()
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandText = "Update Articles set ViewTimes = ViewTimes + 1 where ID = @ID";
            cmd.Parameters.AddWithValue("@ID", ArticleID.Text);
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            string CatID = "0";
            string SubID = "";
            string SubName = "";
            cmd.CommandText = "select * from Articles where ID = @ID2 and Status =1 and Finished = 1 and Valid = 1";
            cmd.Parameters.AddWithValue("@ID2", ArticleID.Text);
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                ArticleTitle.Text = rd["Title"].ToString();
                CatID = rd["CatID"].ToString();
                SubID = rd["SubID"].ToString();
                SubName = rd["SubName"].ToString();
                TagName.Text = rd["TagName"].ToString();
                CDT.Text = String.Format("{0:yyyy-MM-dd}", rd["CDT"]);
                ViewTimes.Text = rd["ViewTimes"].ToString();
                ReviewTimes.Text = rd["ReviewTimes"].ToString();
                zoom.Text = rd["Content"].ToString();
                Summary.Text = rd["Summary"].ToString();

                Author.Text = rd["Author"].ToString();

            }
            else
            {

                Response.Write("<script language='javascript'> alert('你无法访问该篇文章');window.location ='Index2.aspx';</script>");
            }
            rd.Close();



            if (!String.IsNullOrEmpty(SubName))
            {
                SubLabel.Text = " >> ";
                SubHyperLink.Text = SubName;
                SubHyperLink.NavigateUrl = "Article_List4.aspx?ID=" + SubID;
            }


            cmd.CommandText = "select ID,CatName,Subs,Description,IsShow from Cats where ID = @CatID";
            cmd.Parameters.AddWithValue("@CatID", CatID);
            rd = cmd.ExecuteReader();
            int subs = 0;
            int IsShow = 0;
            if (rd.Read())
            {
                CategoryHyperLink.Text = CategoryLabel.Text = rd["CatName"].ToString();
                CategoryHyperLink.NavigateUrl = "Article_List3.aspx?c=" + rd["CatName"].ToString();
                subs = Convert.ToInt16(rd["Subs"]);
                DescriptionLabel.Text = rd["Description"].ToString();
                IsShow = Convert.ToInt16(rd["IsShow"]);
            }
            rd.Close();

            if (IsShow == 0)
            {
                Panel1.Visible = false;
            }
            else
            {
                Panel1.Visible = true;
            }

            if (subs > 0)
            {
                cmd.CommandText = "select ID,SubName from Subs where Valid = 1 and CatID = @CatID2 Order By Orders Desc";
                cmd.Parameters.AddWithValue("@CatID2", CatID);
                rd = cmd.ExecuteReader();
                Repeater2.DataSource = rd;
                Repeater2.DataBind();
                rd.Close();
            }

            cmd.CommandText = "select top 5* from Articles where CatID = @CatID3 and ID <>@ArticleID and Status = 1 and Finished = 1 and IsList=1 and Valid = 1 order by ViewTimes Desc";
            cmd.Parameters.AddWithValue("@CatID3", CatID);
            cmd.Parameters.AddWithValue("@ArticleID", Request.QueryString["ID"].Trim());
            rd = cmd.ExecuteReader();
            rpt_message.DataSource = rd;
            rpt_message.DataBind();
            rd.Close();




            cmd.CommandText = "select * from Articles_ArticleTags where ArticleID=@ArticleID2";
            cmd.Parameters.AddWithValue("@ArticleID2", Request.QueryString["ID"].Trim());
            rd = cmd.ExecuteReader();
            Repeater1.DataSource = rd;
            Repeater1.DataBind();
            rd.Close();

            cmd.CommandText = "select * from Files where ArticleID=@ArticleID5";
            cmd.Parameters.AddWithValue("@ArticleID5", Request.QueryString["ID"].Trim());
            rd = cmd.ExecuteReader();
            Repeater3.DataSource = rd;
            Repeater3.DataBind();
            rd.Close();


            cmd.CommandText = "select * from Articles_ArticleTags where ArticleID=@ArticleID3";
            cmd.Parameters.AddWithValue("@ArticleID3", Request.QueryString["ID"].Trim());
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                Tag.Visible = true;
            }
            rd.Close();


            int liketimes = 0;
            cmd.CommandText = "select * from ArticleView_Like where ArticleID=@ArticleID10";
            cmd.Parameters.AddWithValue("@ArticleID10", Request.QueryString["ID"].Trim());
            rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                liketimes += 1;
                if (rd["UserIP"].ToString() == USERIP && rd["LikeDate"].ToString() == DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    if (User_id.Text == "") { like1.Style["background-position"] = "right"; like1.Enabled = false; }
                    else
                    {
                        if (rd["UserID"].ToString() == User_id.Text.ToString()) { like1.Style["background-position"] = "right"; like1.Enabled = false; }
                    }
                }
            }
            LikeTimes.Text = liketimes + "";
            likeCount1.Text = liketimes + "";
            rd.Close();


            cmd.CommandText = "select top 5* from Articles where CatID = @CatID4 and ID <>@ArticleID4 and Status = 1 and Finished = 1 and Valid = 1 and IsList=1 order by ViewTimes Desc";
            cmd.Parameters.AddWithValue("@CatID4", CatID);
            cmd.Parameters.AddWithValue("@ArticleID4", Request.QueryString["ID"].Trim());
            rd = cmd.ExecuteReader();
            if (!rd.Read()) { Panel2.Visible = false; }
            rd.Close();


        }
    }

    protected void like1_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            using (SqlConnection conn = new DB().GetConnection())
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from [ArticleView_Like] where [UserIP] = @UserIP and [ArticleID]=@ArticleID and [LikeDate]=@LikeDate and [UserID] = @UserID";
                cmd.Parameters.AddWithValue("@UserIP", USERIP);
                cmd.Parameters.AddWithValue("@UserID", (User_id.Text == "" ? "Null" : User_id.Text));
                cmd.Parameters.AddWithValue("@ArticleID", int.Parse(ArticleID.Text));
                cmd.Parameters.AddWithValue("@LikeDate", DateTime.Now.ToString("yyyy-MM-dd"));
                conn.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read()) { Response.Write("<script>alert('您今天已经点过赞了');</script>"); }
                else
                {
                    cmd.CommandText = "Insert into ArticleView_Like (ArticleID,UserIP,LikeDate,UserID)values(@ArticleID,@UserIP,@LikeDate,@UserID)";
                    rd.Close();
                    cmd.ExecuteNonQuery();
                    likeCount1.Text = (Convert.ToInt32(likeCount1.Text) + 1).ToString();
                    like1.Style["background-position"] = "right";
                    like1.Enabled = false;
                    LikeTimes.Text = (Convert.ToInt32(LikeTimes.Text) + 1).ToString();
                }
                rd.Close();
            }
        }
    }

    protected void Publish_Click(object sender, EventArgs e)
    {
        if (Session["CheckCode"].ToString().ToLower() == ValidateCode.Text.ToLower().Trim())
        {
            if (Editor1.Text != "")
            {
                string _userName = "";
                string _avatar = "";
                using (SqlConnection conn = new DB().GetConnection())
                {
                    SqlCommand cmd = conn.CreateCommand();
                    conn.Open();
                    cmd.CommandText = "select * from Users where ID=@ID";
                    cmd.Parameters.AddWithValue("@ID", User_id.Text);
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        _userName = rd["TrueName"].ToString();
                        _avatar = rd["Avatar"].ToString();
                    }
                    rd.Close();
                    cmd.Dispose();
                    //向ArticleView_Comment插入一条记录操作
                    StringBuilder sb = new StringBuilder("Insert into ArticleView_Comment (ArticleID,PublisherID,[Comment],PublishTime,IsAnonymous,ShowName,Visible,PublisherName,PublisherAvatar)");
                    sb.Append(" values(@ArticleID,@PublisherID,@Comment,@PublishTime,@IsAnonymous,@ShowName,@Visible,@PublisherName,@PublisherAvatar)");
                    cmd = new SqlCommand(sb.ToString(), conn);
                    cmd.Parameters.AddWithValue("@ArticleID", int.Parse(ArticleID.Text));
                    cmd.Parameters.AddWithValue("@PublisherID", int.Parse(User_id.Text));
                    cmd.Parameters.AddWithValue("@Comment", Editor1.Text);
                    cmd.Parameters.AddWithValue("@PublisherName", _userName);
                    cmd.Parameters.AddWithValue("@PublisherAvatar", CheckBox1.Checked == true ? "images/users/1.png" : _avatar);
                    cmd.Parameters.AddWithValue("@PublishTime", DateTime.Now.ToUniversalTime().ToString());
                    cmd.Parameters.AddWithValue("@IsAnonymous", CheckBox1.Checked == true ? 1 : 0);
                    cmd.Parameters.AddWithValue("@ShowName", CheckBox1.Checked == true ? "匿名用户" : _userName);
                    cmd.Parameters.AddWithValue("@Visible", 1);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    //插入成功
                }
                DBView();
                Editor1.Text = "";
            }
            else Response.Write("<script>alert('请编辑评论');</script>");
        }
        else Response.Write("<script>alert('验证码错误');</script>");
    }

    protected void DBView()
    {
        int _comment = 0;
        if (Session["UserID"] == null)
        {
            NotLoggedIn.Visible = true;
            Write.Visible = false;
        }
        else
        {
            NotLoggedIn.Visible = false;
            Write.Visible = true;
        }
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select  TOP 5 * from ArticleView_Comment where ArticleID=@ArticleID and Visible=1 order by PublishTime desc";
            cmd.Parameters.AddWithValue("@ArticleID", Request.QueryString["ID"].Trim());
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            rptFeedBackList.DataSource = rd;
            rptFeedBackList.DataBind();
            rd.Close();

            cmd.CommandText = "select * from ArticleView_Comment where ArticleID=@ArticleID and Visible=1 order by PublishTime desc";
            rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                _comment++;
            }
            if (_comment <= 5) Div1.Style["visibility"] = "hidden"; else Div1.Style["visibility"] = "visible";
            rd.Close();

            cmd.CommandText = "select * from Articles where ID = @ID";
            cmd.Parameters.AddWithValue("@ID", ArticleID.Text);
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                if (rd["IsComment"].ToString() == "False") { CommentDiv.Style["visibility"] = "hidden"; NotLoggedIn.Style["visibility"] = "hidden"; }
            }
            rd.Close();
        }
        Div1.Visible = true;
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from ArticleView_Comment where ArticleID=@ArticleID and Visible=1 order by PublishTime desc";
            cmd.Parameters.AddWithValue("@ArticleID", Request.QueryString["ID"].Trim());
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            rptFeedBackList.DataSource = rd;
            rptFeedBackList.DataBind();
            rd.Close();
        }
        Div1.Visible = false;
    }
}