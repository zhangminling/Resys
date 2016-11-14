using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Article_Preview : System.Web.UI.Page
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
                
                Random r = new Random();
             Image1.ImageUrl = "images/random/V" + (r.Next(12) + 1) + ".jpg";


             if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
             {
                 ArticleID.Text = Request.QueryString["ID"].Trim();
                 int RoleID = Convert.ToInt16(Session["RoleID"].ToString());
                 if (RoleID <= 4)
                 {
                     MyInit();
                 }
                 else { Response.Write("<script language='javascript'> alert('你无法访问该篇文章');window.location ='User_Center.aspx';</script>"); }
             }
             else
             {   ArticleTitle.Text = Session["Title1"].ToString();
             Summary.Text  = Session["Summary1"].ToString() ;
             Content.Text = Session["Content1"].ToString();
             Author.Text = Session["UserName"].ToString();
             CDT.Text = Session["DataTime1"].ToString();
             TagName.Text = Session["TagName"].ToString();
            }
           
            }

     

        }

    }


    private void MyInit()
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();

            string CatID = "0";
            string SubID = "";
            string SubName = "";
            cmd.CommandText = "select * from Articles where ID = @ID2";
            cmd.Parameters.AddWithValue("@ID2", ArticleID.Text);
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                ArticleTitle.Text = rd["Title"].ToString();
                CatID = rd["CatID"].ToString();
                SubID = rd["SubID"].ToString();
                SubName = rd["SubName"].ToString();
                CDT.Text = String.Format("{0:yyyy-MM-dd}", rd["CDT"]);
                ViewTimes.Text = rd["ViewTimes"].ToString();
                Content.Text = rd["Content"].ToString();
                Summary.Text = rd["Summary"].ToString();
                Author.Text = rd["Author"].ToString();
                TagName.Text = rd["TagName"].ToString();

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

            cmd.CommandText = "select top 5* from Articles where SubID = @SubID and ID <>@ArticleID and Status = 1 and Finished = 1 and Valid = 1 order by ViewTimes Desc";
            cmd.Parameters.AddWithValue("@SubID", SubID);
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


            cmd.CommandText = "select top 5* from Articles where SubID = @SubID3 and ID <>@ArticleID4 and Status = 1 and Finished = 1 and Valid = 1 order by ViewTimes Desc";
            cmd.Parameters.AddWithValue("@SubID3", SubID);
            cmd.Parameters.AddWithValue("@ArticleID4", Request.QueryString["ID"].Trim());
            rd = cmd.ExecuteReader();
            if (!rd.Read()) { Panel2.Visible = false; }
            rd.Close();


        }
    }

}


  

