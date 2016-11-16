using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Collections;

public partial class ArticleTag_Up : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            if (Session["RoleID"] == null || Session["UserID"] == null)
            {
                Util.ShowMessage("用户登录超时，请重新登录！", "Login.aspx");
            }
            else
            {

                if (Request.QueryString["ID"] != null)
                {
                    int RoleID = Convert.ToInt16(Session["RoleID"].ToString());
                    if (RoleID > 4)
                    {
                        Util.ShowMessage("对不起，你无权访问该页面！", "User_Center.aspx");
                    }
                    else
                    {

                        TagID.Text = Request.QueryString["ID"].ToString();
                        MyInitForUpdate();


                    }
                }
            }
        }
    }

    private void MyInitForUpdate()
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from ArticleTags where ID =" + TagID.Text;
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read()) {
                TagName.Text = rd["TagName"].ToString();
                TagDescription.Text = rd["Description"].ToString();   
                Articles.Text=rd["Articles"].ToString();
            
            }
        }
    }

    private void DoUpdate()
    {
       
        using (SqlConnection conn = new DB().GetConnection())
        {
            StringBuilder sb = new StringBuilder("Update ArticleTags set TagName=@TagName,Description=@Description, Articles=@Articles where ID=@ID");
            StringBuilder sb1 = new StringBuilder("Update Articles_ArticleTags set ArticleTagName=@ArticleTagName where ArticleTagID=@ArticleTagID");
            SqlCommand cmd = new SqlCommand(sb.ToString(),conn);
            SqlCommand cmd1 = new SqlCommand(sb1.ToString(), conn);
            cmd.Parameters.AddWithValue("@ID", TagID.Text);  
            cmd.Parameters.AddWithValue("@TagName", TagName.Text);
            cmd.Parameters.AddWithValue("@Description", TagDescription.Text);
            cmd.Parameters.AddWithValue("@Articles", Articles.Text);
            cmd1.Parameters.AddWithValue("@ArticleTagName", TagName.Text);
            cmd1.Parameters.AddWithValue("@ArticleTagID", TagID.Text); 
          
            conn.Open();
            cmd.ExecuteNonQuery();
            cmd1.ExecuteNonQuery();
            conn.Close();
            Response.Write("<script language='javascript'> alert('更新成功');</script>");
            
            
        }
        
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
      
         
             DoUpdate();
         
    }
   


      protected void Last_Click(object sender, EventArgs e)
    {
        Response.Redirect("ArticleTag_Man.aspx");
    }
      protected void UpAr_Click(object sender, EventArgs e)
      {
          using (SqlConnection conn = new DB().GetConnection())
          {
              SqlCommand cmd = conn.CreateCommand();
              cmd.CommandText = "select count(*) as maxrow from [Articles_ArticleTags] where ArticleTagID= @TagID1";
              conn.Open();  
              cmd.Parameters.AddWithValue("@TagID1", TagID.Text);
              SqlDataReader rd1 = cmd.ExecuteReader();
              if (rd1.Read())
              {
                  Articles.Text = rd1["maxrow"].ToString();
              }
              rd1.Close();         
          }
      }
}