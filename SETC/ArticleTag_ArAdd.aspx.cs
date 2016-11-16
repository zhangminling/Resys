using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.IO;
using System.Collections;

public partial class ArticleTag_ArAdd : System.Web.UI.Page
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
                if (Convert.ToInt16(Session["RoleID"]) > 4)
                {
                    Util.ShowMessage("对不起，你无权访问该页面！", "User_Center.aspx");
                }
                else
                {
                    if (Request.QueryString["ID"] != null)
                    {
                        TagID.Text = Request.QueryString["ID"].ToString();

                        using (SqlConnection conn = new DB().GetConnection())
                        {
                            SqlCommand cmd = conn.CreateCommand();
                            cmd.CommandText = "select * from [ArticleTags] where ID= @TagID";
                            cmd.Parameters.AddWithValue("@TagID", TagID.Text);
                            conn.Open();
                            SqlDataReader rd = cmd.ExecuteReader();

                            if (rd.Read())
                            {
                                TagName.Text = rd["TagName"].ToString();
                            }
                            rd.Close();



                            cmd.CommandText = "select count(*) as maxrow from [Articles_ArticleTags] where ArticleTagID= @TagID1";
                            cmd.Parameters.AddWithValue("@TagID1", TagID.Text);
                            SqlDataReader rd1 = cmd.ExecuteReader();
                            if (rd1.Read())
                            {
                                Count.Text = rd1["maxrow"].ToString();
                            }
                            rd1.Close();

                            string s = "";
                            int i = Convert.ToInt32(Count.Text);
                            string[] ArticleID = new string[i];
                            cmd.CommandText = "select * from [Articles_ArticleTags] where ArticleTagID= @TagID2";
                            cmd.Parameters.AddWithValue("@TagID2", TagID.Text);
                            SqlDataReader rd2 = cmd.ExecuteReader();
                            if (i != 0)
                            {
                                for (int j = 0; j < i; j++)
                                {
                                    if (rd2.Read())
                                    {
                                        ArticleID[j] = rd2["ArticleID"].ToString();
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





                            MyInit();
                            MyDataBind();
                        }
                    }

                    //string[] ids;         

                }
            }
        }
    }





    private void MyInit()
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from Cats order by Orders desc;";
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            CatsDDL.DataSource = rd;
            CatsDDL.DataValueField = "ID";
            CatsDDL.DataTextField = "CatName";
            CatsDDL.DataBind();
            rd.Close();
            CatsDDL.Items.Insert(0, new ListItem("所有分类", "-1"));
            SubsDDL.Items.Insert(0, new ListItem("二级分类", "-1"));

            //if (CatsDDL.Items.Count > 1)
            //{
            //    CatsDDL.SelectedIndex = 1;
            //}

            cmd.CommandText = "select * from Subs where CatID = " + CatsDDL.SelectedValue + " order by Orders desc;";
            rd = cmd.ExecuteReader();
            SubsDDL.DataSource = rd;
            SubsDDL.DataValueField = "ID";
            SubsDDL.DataTextField = "SubName";
            SubsDDL.DataBind();
            rd.Close();
            SubsDDL.Items.Insert(0, new ListItem("二级分类", "-1"));


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

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        MyDataBind();
    }

    private void MyDataBind()
    {
        AspNetPager1.PageSize = Convert.ToInt16(PageSizeDDL.SelectedValue);
        string param = SearchTB.Text;
        StringBuilder whereStr = new StringBuilder(" where Valid=1 and Finished=1 and Status=1");
        if (!String.IsNullOrEmpty(param))
        {
            whereStr.Append(" and [title] like '%").Append(Server.HtmlEncode(param.Trim().Replace("'", ""))).Append("%' ");
        }
        if (Convert.ToInt16(CatsDDL.SelectedValue) > 0)
        {
            whereStr.Append(" and CatID = ").Append(CatsDDL.SelectedValue);
        }
        if (Convert.ToInt16(SubsDDL.SelectedValue) > 0)
        {
            whereStr.Append(" and SubID = ").Append(SubsDDL.SelectedValue);
        }
        if (Convert.ToInt16(AuthorDDL.SelectedValue) > 0)
        {
            whereStr.Append(" and AuthorID = ").Append(AuthorDDL.SelectedValue);
        }

        string sql = "select count(ID) as total from Articles " + whereStr.ToString() + "and ID not in (" + ArticleIDS.Text + ")";

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
                sql = "Select top " + AspNetPager1.PageSize + " * from Articles " + whereStr.ToString() + "and ID not in (" + ArticleIDS.Text + ") " + OrderDDL.SelectedValue;
            }
            else
            {
                // Select Top 页容量 * from 表 where 条件 and id not in	(Select Top 页容量*（当前页数-1） id 	from 表 where 条件 order by 排序条件) order by 排序条件
                sql = "Select top " + AspNetPager1.PageSize + " * from Articles " + whereStr.ToString() + "and ID not in (" + ArticleIDS.Text + ")  and id not in ( select top " + AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1) + " id  from Articles " + whereStr.ToString() + " " + OrderDDL.SelectedValue + " ) " + OrderDDL.SelectedValue;
                //sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER ( " + orderStr + ") AS MyRank,* FROM Article " + whereStr +" ) AS Rank " + whereStr + " and MyRank BETWEEN " +AspNetPager1.StartRecordIndex+" AND "+ (AspNetPager1.StartRecordIndex+AspNetPager1.PageSize-1) +orderStr;
            }
            //TestLabel.Text = sql;
            cmd.CommandText = sql;
            rd = cmd.ExecuteReader();
            GridView1.DataSource = rd;
            GridView1.DataBind();
            rd.Close();
        }
    }

    protected void SelectAllCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("ChechBox1");
            if (SelectAllCheckBox.Checked == true)
            {
                cbox.Checked = true;
            }
            else
            {
                cbox.Checked = false;
            }
        }
    }

    protected void PageSizeDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        MyDataBind();
    }
    protected void AuthorDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        MyDataBind();
    }

    protected void CatsDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from Subs where CatID = " + CatsDDL.SelectedValue + " order by Orders desc;";
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            SubsDDL.DataSource = rd;
            SubsDDL.DataValueField = "ID";
            SubsDDL.DataTextField = "SubName";
            SubsDDL.DataBind();
            rd.Close();
        }
        SubsDDL.Items.Insert(0, new ListItem("二级分类", "-1"));

        MyDataBind();
    }

    protected void OrderDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        MyDataBind();
    }

    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        MyDataBind();
    }
    protected void SubsDDL_SelectedIndexChanged1(object sender, EventArgs e)
    {
        MyDataBind();
    }

    protected void ArAddBtn_Click(object sender, EventArgs e)
    {
        string ids = "";
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox checkBox = (CheckBox)GridView1.Rows[i].FindControl("ChechBox1");
            if (checkBox.Checked == true)
            {
                ids += "," + GridView1.DataKeys[i].Value;
            }
        }
        if (!String.IsNullOrEmpty(ids))
        {
            ids = ids.Substring(1);
            Label3.Text = ids;
            string[] array = ids.Split(',');
            int k = array.Length;
            string[] ArticleID = new string[10];
            string[] ArticleName = new string[10];
            string AtricleTagName = "";
            string ArticleTagID = "";
            // string[] USERID = new string[10];
            //string[] USERNAME = new string[10];
            //string USERTAGID = "";
            // string USERTAGNAME = "";
            int p;

            string count = Count.Text;
            using (SqlConnection conn = new DB().GetConnection())
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from Articles where ID in (" + Label3.Text + ") order by ID desc  ";
                conn.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                for (p = 0; p <= k - 1; p++)
                {
                    if (rd.Read())
                    {
                        ArticleID[p] = rd["ID"].ToString();
                        ArticleName[p] = rd["Title"].ToString();

                    }
                }
                rd.Close();

                Label3.Text = ArticleID[p];
                cmd.CommandText = "select * from ArticleTags where ID =@ArticleTagID";
                cmd.Parameters.AddWithValue("@ArticleTagID", TagID.Text);
                SqlDataReader rd1 = cmd.ExecuteReader();

                if (rd1.Read())
                {
                    ArticleTagID = rd1["ID"].ToString();
                    AtricleTagName = rd1["TagName"].ToString();
                    // Articles1 = rd1["Articles"].ToString();
                }
                rd1.Close();


                int Articles = Convert.ToInt32(count);
                Articles = Articles + k;
                cmd.CommandText = "update ArticleTags set Articles= @Articles where ID=@ID";
                cmd.Parameters.AddWithValue("@ID", TagID.Text);
                cmd.Parameters.AddWithValue("@Articles", Articles);
                cmd.ExecuteNonQuery();



                //string[] Arrays = USERID.Split(',');
                //string[] Arrays1 = USERNAME.Split(',');
                for (int j = 0; j <= k - 1; j++)
                {
                    //USERID = Arrays[j];
                    //USERNAME = Arrays1[j];
                    StringBuilder sb = new StringBuilder("insert into Articles_ArticleTags(ArticleID,Title,ArticleTagID,ArticleTagName )");
                    sb.Append(" values ( @ArticleID,@Title,@ArticleTagID,@ArticleTagName ) ");
                    SqlCommand cmd1 = new SqlCommand(sb.ToString(), conn);
                    cmd1.Parameters.AddWithValue("@ArticleID", ArticleID[j]);
                    cmd1.Parameters.AddWithValue("@Title", ArticleName[j]);
                    cmd1.Parameters.AddWithValue("@ArticleTagID", ArticleTagID);
                    cmd1.Parameters.AddWithValue("@ArticleTagName", AtricleTagName);
                    cmd1.ExecuteNonQuery();
                    
                    
                } 
                
               
                conn.Close();
                Response.Write("<script language='javascript'> alert('操作成功');</script>");
                Response.Redirect("ArticleTag_ArAdd.aspx?ID="+TagID.Text);
              

            }
        }
        else
        {
            Response.Write("<script language='javascript'> alert('请至少选择一项');</script>");
        }
        }
    

}