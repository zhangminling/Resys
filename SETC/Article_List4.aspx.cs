using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Article_List4 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Random r = new Random();
            Image1.ImageUrl = "images/random/V" + (r.Next(12) + 1) + ".jpg";
            string ArticleID = "0";
            int go = 0;
            if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                ViewState["SubID"] = Request.QueryString["ID"];
                //CategoryLabel.Text = CategoryHyperLink.Text = Request.QueryString["c"].Trim();
                //CategoryHyperLink.NavigateUrl = "Article_List.aspx?c=" + Request.QueryString["c"].Trim();

                // 如果栏目下，只有一篇文章，则直接显示文章                
                //string sql = "select ID from Articles  where SubID = " + ViewState["SubID"] + " and Status = 1 and Finished = 1 ";
                using (SqlConnection conn = new DB().GetConnection())
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "select ID from Articles  where SubID = @SubID and Status = 1 and Finished = 1 and Valid = 1 and IsList = 1 ";
                    cmd.Parameters.AddWithValue("@SubID", ViewState["SubID"]);
                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        ArticleID = rd["ID"].ToString();
                        go += 1;
                        if (go > 2)
                        {
                            break;
                        }
                    }
                    rd.Close();
                }

                if (go == 1)
                {
                    Response.Redirect("Article_View.aspx?ID=" + ArticleID);
                }
                else
                {
                    MyInit();
                   
                }
            }
        }
    }

    private void MyInit()
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            SqlDataReader rd = null;
            string CatID = "0";
            int IsShow = 0;
            int SubID = 0;
            cmd.CommandText = "Select ID,CatName,Description,IsShow From Cats Where ID = ( Select CatID From Subs Where ID = @SubID )";
            cmd.Parameters.AddWithValue("@SubID", ViewState["SubID"]);
            conn.Open();
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                CatID = rd["ID"].ToString();
                CategoryLabel.Text = CatHyperLink.Text = rd["CatName"].ToString();
                CatHyperLink.NavigateUrl = "Article_List3.aspx?c=" + rd["CatName"].ToString();
                DescriptionLabel.Text = rd["Description"].ToString();
                IsShow = Convert.ToInt16(rd["IsShow"]);
            }
            rd.Close();

            //cmd.CommandText = "Select SubName From Subs where ID = " + ViewState["SubID"];
            cmd.CommandText = "Select ID,SubName From Subs where ID = @SubID4";
            cmd.Parameters.AddWithValue("@SubID4", ViewState["SubID"]);
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                SubHyperLink.Text = rd["SubName"].ToString();
                SubHyperLink.NavigateUrl = "Article_List4.aspx?ID=" + ViewState["SubID"];
                SubID = Convert.ToInt16(rd["ID"]);
            }
            rd.Close();




            if (IsShow == 0)
            {

                if (SubID == 49)
                {
                    Panel3.Visible = true;
                    MyDataBind3();
                }
                else
                {
                    Panel2.Visible = true;
                    MyDataBind();
                }
            }
            else
            {
               
                Panel1.Visible = true;
                MyDataBind2();
                
            }



            cmd.CommandText = "select ID,SubName from Subs where Valid = 1 and CatID = " + CatID + " Order by Orders Desc";
            rd = cmd.ExecuteReader();
            Repeater2.DataSource = rd;
            Repeater2.DataBind();
            rd.Close();

            /*
            cmd.CommandText = "Select top " + PageSizeDDL.SelectedValue + " * from Articles where CatID = " + CategoryID + " and Status = 1 and Finished = 1 order by Orders Desc,ID Desc";
            rd = cmd.ExecuteReader();
            GridView1.DataSource = rd;
            GridView1.DataBind();
            rd.Close();
            */
        }
    }

    private void MyDataBind()
    {
        AspNetPager1.PageSize = Convert.ToInt16(PageSizeDDL.SelectedValue);

        string whereStr = " where SubID = @SubID and Status = 1 and Finished = 1 and Valid = 1 and IsList = 1 ";
        string sql = "select count(ID) as total from Articles " + whereStr;

        using (SqlConnection conn = (SqlConnection)new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@SubID", ViewState["SubID"]);
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

            //RecordCountLabel.Text = AspNetPager1.RecordCount + "";//总记录数
            TotalPagesLabel.Text = (AspNetPager1.RecordCount / AspNetPager1.PageSize) + 1 + "";//总页数            

            if (AspNetPager1.RecordCount / AspNetPager1.PageSize < 1)
            {
                AspNetPager1.Visible = false;
            }
            else
            {
                AspNetPager1.Visible = true;
            }

            whereStr = " where SubID = @SubID2 and Status = 1 and Finished = 1 and Valid = 1 and IsList = 1 ";
            if (AspNetPager1.CurrentPageIndex == 1)
            {
                sql = "Select top " + AspNetPager1.PageSize + " * from Articles " + whereStr + " Order by Orders Desc,CDT Desc,ID Desc";
            }
            else
            {
                // Select Top 页容量 * from 表 where 条件 and id not in	(Select Top 页容量*（当前页数-1） id 	from 表 where 条件 order by 排序条件) order by 排序条件
                sql = "Select top " + AspNetPager1.PageSize + " * from Articles " + whereStr + " and id not in ( select top " + AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1) + " id  from Articles " + whereStr + " Order by Orders Desc,CDT Desc,ID Desc" + " ) " + " Order by Orders Desc,CDT Desc,ID Desc";
                //sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER ( " + orderStr + ") AS MyRank,* FROM Article " + whereStr +" ) AS Rank " + whereStr + " and MyRank BETWEEN " +AspNetPager1.StartRecordIndex+" AND "+ (AspNetPager1.StartRecordIndex+AspNetPager1.PageSize-1) +orderStr;
            }
            //TestLabel.Text = sql;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@SubID2", ViewState["SubID"]);
            rd = cmd.ExecuteReader();
            Repeater1.DataSource = rd;
            Repeater1.DataBind();
            rd.Close();
           
        }
    }


    private void MyDataBind2()
    {
        AspNetPager2.PageSize = Convert.ToInt16(PageSizeDDL2.SelectedValue);

        string whereStr = " where SubID = @SubID and Status = 1 and Finished = 1 and Valid = 1 and IsList = 1";
        string sql = "select count(ID) as total from Articles " + whereStr;

        using (SqlConnection conn = (SqlConnection)new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@SubID", ViewState["SubID"]);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                AspNetPager2.RecordCount = Convert.ToInt16(rd[0]);
            }
            else
            {
                AspNetPager2.RecordCount = 0;
            }
            rd.Close();

            //RecordCountLabel.Text = AspNetPager1.RecordCount + "";//总记录数
            TotalPagesLabel2.Text = (AspNetPager2.RecordCount / AspNetPager2.PageSize) + 1 + "";//总页数            

            if (AspNetPager2.RecordCount / AspNetPager2.PageSize < 1)
            {
                AspNetPager2.Visible = false;
            }
            else
            {
                AspNetPager2.Visible = true;
            }

            whereStr = " where SubID = @SubID2 and Status = 1 and Finished = 1 and Valid = 1  and IsList = 1";
            if (AspNetPager2.CurrentPageIndex == 1)
            {
                sql = "Select top " + AspNetPager2.PageSize + " * from Articles " + whereStr + " Order by Orders Desc,CDT Desc,ID Desc";
            }
            else
            {
                // Select Top 页容量 * from 表 where 条件 and id not in	(Select Top 页容量*（当前页数-1） id 	from 表 where 条件 order by 排序条件) order by 排序条件
                sql = "Select top " + AspNetPager2.PageSize + " * from Articles " + whereStr + " and id not in ( select top " + AspNetPager2.PageSize * (AspNetPager2.CurrentPageIndex - 1) + " id  from Articles " + whereStr + " Order by Orders Desc,CDT Desc,ID Desc" + " ) " + " Order by Orders Desc,CDT Desc,ID Desc";
                //sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER ( " + orderStr + ") AS MyRank,* FROM Article " + whereStr +" ) AS Rank " + whereStr + " and MyRank BETWEEN " +AspNetPager1.StartRecordIndex+" AND "+ (AspNetPager1.StartRecordIndex+AspNetPager1.PageSize-1) +orderStr;
            }
            //TestLabel.Text = sql;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@SubID2", ViewState["SubID"]);
            rd = cmd.ExecuteReader();
            Repeater3.DataSource = rd;
            Repeater3.DataBind();
            rd.Close();

        }
    }


    private void MyDataBind3()
    {
        AspNetPager3.PageSize = Convert.ToInt16(PageSizeDDL3.SelectedValue);

       string whereStr = " where IsClass=1 ";
      //  string sql = "select count(ID) as total from Articles " + whereStr;
       string sql = "select count(ID) as total from Classes " + whereStr;

        using (SqlConnection conn = (SqlConnection)new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
          //  cmd.Parameters.AddWithValue("@SubID", ViewState["SubID"]);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                AspNetPager3.RecordCount = Convert.ToInt16(rd[0]);
            }
            else
            {
                AspNetPager3.RecordCount = 0;
            }
            rd.Close();

            RecordCountLabel3.Text = AspNetPager1.RecordCount + "";//总记录数
           // TotalPagesLabel3.Text = (AspNetPager2.RecordCount / AspNetPager2.PageSize) + 1 + "";//总页数            

            if (AspNetPager3.RecordCount / AspNetPager3.PageSize < 1)
            {
                AspNetPager3.Visible = false;
            }
            else
            {
                AspNetPager3.Visible = true;
            }

            whereStr = " where IsClass=1 ";
            if (AspNetPager3.CurrentPageIndex == 1)
            {
                sql = "Select top " + AspNetPager3.PageSize + " * from Classes " + whereStr + " Order by ID Desc";
            }
            else
            {
                // Select Top 页容量 * from 表 where 条件 and id not in	(Select Top 页容量*（当前页数-1） id 	from 表 where 条件 order by 排序条件) order by 排序条件
                sql = "Select top " + AspNetPager3.PageSize + " * from Classes " + whereStr + " and id not in ( select top " + AspNetPager3.PageSize * (AspNetPager3.CurrentPageIndex - 1) + " id  from Classes " + whereStr + " Order by ID Desc" + " ) " + " Order by ID Desc";
                //sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER ( " + orderStr + ") AS MyRank,* FROM Article " + whereStr +" ) AS Rank " + whereStr + " and MyRank BETWEEN " +AspNetPager1.StartRecordIndex+" AND "+ (AspNetPager1.StartRecordIndex+AspNetPager1.PageSize-1) +orderStr;
            }
            //TestLabel.Text = sql;
            cmd.CommandText = sql;
            rd = cmd.ExecuteReader();
            Repeater4.DataSource = rd;
            Repeater4.DataBind();
            rd.Close();

        }
    }





    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        MyDataBind();
   
    }

    protected void PageSizeDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        MyDataBind();
     
    }

    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        MyDataBind2();
    }

    protected void PageSizeDDL2_SelectedIndexChanged(object sender, EventArgs e)
    {
        MyDataBind2();
    }

    protected void AspNetPager3_PageChanged(object sender, EventArgs e)
    {
        MyDataBind3();
    }

    protected void PageSizeDDL3_SelectedIndexChanged(object sender, EventArgs e)
    {
        MyDataBind3();
    }

}