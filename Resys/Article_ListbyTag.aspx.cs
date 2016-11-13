using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Article_ListbyTag : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Random r = new Random();
            Image1.ImageUrl = "images/random/V" + (r.Next(12) + 1) + ".jpg";

            if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                TagID.Text = Request.QueryString["ID"].Trim();
                MyInit();
            }

        }
     
    }


    private void MyInit()
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            SqlDataReader rd = null;
              cmd.CommandText = "Select * From Articles_ArticleTags Where ArticleTagID = @TagID";
              cmd.Parameters.AddWithValue("@TagID", TagID.Text);
             conn.Open();
             rd = cmd.ExecuteReader(); 
            Repeater1.DataSource = rd;
            Repeater1.DataBind();
            rd.Close();



            cmd.CommandText = "Select * From ArticleTags Where ID = @TagID2";
            cmd.Parameters.AddWithValue("@TagID2", TagID.Text);
             rd = cmd.ExecuteReader();
             if (rd.Read()) { 
             TagName.Text=rd["TagName"].ToString();
             }
        }
    }


}