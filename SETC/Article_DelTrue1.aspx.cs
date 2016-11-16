using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;

public partial class Article_DelTrue1 : System.Web.UI.Page
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
                if (Convert.ToInt16(Session["RoleID"]) > 0)
                {
                    Util.ShowMessage("对不起，你无权访问该页面！", "User_Center.aspx");
                }
                else
                {
                    if (Request.QueryString["IDS"] != null)
                    {
                        IDSLabel.Text = Request.QueryString["IDS"].ToString();
                        MyInit();
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
            cmd.CommandText = "select * from Articles where ID in (" + IDSLabel.Text + ") and Finished = 0  order by ID desc";
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            GridView1.DataSource = rd;
            GridView1.DataBind();
            rd.Close();


            cmd.CommandText = "select count(*) as maxrow from Articles where ID in (" + IDSLabel.Text + ") and Finished = 0 ";
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                Count.Text = rd["maxrow"].ToString();
            }
            rd.Close();


                string s = "";
                int i = Convert.ToInt32(Count.Text);
                string[] AuthorID = new string[i];
                string[] ArticleID = new string[i];
                cmd.CommandText = "select * from Articles where ID in (" + IDSLabel.Text + ")and Finished = 0  order by ID desc";
                SqlDataReader rd2 = cmd.ExecuteReader();
                if (i != 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (rd2.Read())
                        {
                            AuthorID[j] = rd2["AuthorID"].ToString();
                            ArticleID[j] = rd2["ID"].ToString();
                        
                            if (AuthorID[j] != Session["UserID"].ToString())
                            {
                                Response.Write("<script>alert('以下文章有其他作者的文章，你无法进行删除操作');</script>");
                              
                                Button1.Visible = false;
                                j = i;
                             
                            }


                            for (int k = 0; k < i; k++)
                            {
                                s = string.Join(",", ArticleID);
                                ArticleIDS.Text = s;
                            }
                        }
                    }
                }

                else
                {
                    int s1 = 0;
                    ArticleIDS.Text = s1.ToString();
                }

                rd2.Close();
            

 
            conn.Close();

        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        int i = 0;
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Delete from Articles where ID in (" + ArticleIDS.Text + ") ";
            conn.Open();
            i = cmd.ExecuteNonQuery();
            cmd.Dispose();

            //SqlCommand cmd1 = conn.CreateCommand();
            //cmd1.CommandText = "Delete from Articles_ArticleTags where ArticleID in (" + ArticleIDS.Text + ") ";
            //cmd1.ExecuteNonQuery();
            //cmd1.Dispose();

            SqlCommand cmd2 = conn.CreateCommand();
            cmd2.CommandText = "Delete from Files where ArticleID in (" + ArticleIDS.Text + ") ";
            cmd2.ExecuteNonQuery();
            cmd2.Dispose();


            cmd.CommandText = "select * from Articles where ID in (" + ArticleIDS.Text + ") order by ID desc";
            SqlDataReader rd = cmd.ExecuteReader();
            GridView1.DataSource = rd;
            GridView1.DataBind();
            rd.Close();
            conn.Close();

        }
        if (i > 0)
        {
            ResultLabel.Text = "成功删除" + i + "篇文章！";
            ResultLabel.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            ResultLabel.Text = "操作失败，请重试！";
            ResultLabel.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("Article_draft.aspx");
    }
    }
