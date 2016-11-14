using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Article_List3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Random r = new Random();
        Image1.ImageUrl = "images/random/V" + (r.Next(12) + 1) + ".jpg";

        if (!String.IsNullOrEmpty(Request.QueryString["c"]) && String.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            CategoryLabel.Text = CategoryHyperLink.Text = CategoryHyperLink1.Text = Request.QueryString["c"].Trim();
            CategoryHyperLink.NavigateUrl = "Article_List3.aspx?c=" + Request.QueryString["c"].Trim();
            CategoryHyperLink1.NavigateUrl = "Article_List3.aspx?c=" + Request.QueryString["c"].Trim();
            MyInit();
        }

      
    }

    private void MyInit()
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            SqlDataReader rd = null;
            string CategoryID = "0";
            //cmd.CommandText = "select ID,Description,Subs from Cats where CatName = '" + CategoryLabel.Text + "'";
            cmd.CommandText = "select ID,Description,Subs,IsShow,IsMixed from Cats where CatName = @CatName";
            cmd.Parameters.AddWithValue("@CatName", CategoryLabel.Text);
            conn.Open();
            rd = cmd.ExecuteReader();

            int subs = 0;
            int IsShow=0;
            int IsMixed = 0;
            if (rd.Read())
            {
                CategoryID = rd["ID"].ToString();
                CategoryIDLabel.Text = CategoryID;
                DescriptionLabel.Text = rd["Description"].ToString();
                subs = Convert.ToInt16(rd["Subs"]);
                IsShow=Convert.ToInt16(rd["IsShow"]);
                IsMixed = Convert.ToInt16(rd["IsMixed"]);
                }
            rd.Close();

            cmd.CommandText = "select ID,SubName,CatName from Subs where Valid = 1 and CatID = " + CategoryID + " Order By Orders Desc";
            rd = cmd.ExecuteReader();
                Repeater2.DataSource = rd;
                Repeater2.DataBind();
                rd.Close();
          

            if (IsShow ==0)
            {
                
                Panel1.Visible = false;
                Panel2.Visible = true;
                MyDataBind2();      
                MyDataBind3();
                
            }
            else
            {
                
                Panel1.Visible = true;
                Panel2.Visible = false;     
                MyDataBind();
            }

        }
    }

    private void MyDataBind2()
    {
        string sql = "select ID,SubName from Subs where Valid = 1 and CatID = " + CategoryIDLabel.Text + " Order By Orders Desc";
        using (SqlConnection conn = (SqlConnection)new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select ID,SubName from Subs where Valid = 1 and CatID = @CatID Order By Orders Desc";
            cmd.Parameters.AddWithValue("@CatID", CategoryIDLabel.Text);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            Repeater1.DataSource = rd;
            Repeater1.DataBind();
            rd.Close();
        }
    } 

    private void MyDataBind()
    {
        AspNetPager1.PageSize = Convert.ToInt16(PageSizeDDL.SelectedValue);

        string whereStr = " where CatID = " + CategoryIDLabel.Text + " and Status = 1 and Finished = 1 and Valid = 1 and IsList=1 ";
        string sql = "select count(ID) as total from Articles " + whereStr;

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

            RecordCountLabel.Text = AspNetPager1.RecordCount + "";//总记录数
            TotalPagesLabel.Text = (AspNetPager1.RecordCount / AspNetPager1.PageSize) + 1 + "";//总页数            

            if (AspNetPager1.RecordCount / AspNetPager1.PageSize < 1)
            {
                AspNetPager1.Visible = false;
            }
            else
            {
                AspNetPager1.Visible = true;
            }

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
            rd = cmd.ExecuteReader();
            Repeater3.DataSource = rd;
            Repeater3.DataBind();
            rd.Close();


           /* if (AspNetPager1.PageSize > GridView1.Rows.Count)
            {
                PageDiv.Visible = false;
            }
            else
            {
                PageDiv.Visible = true;
            }*/
        }
    }



    private void MyDataBind3()
    {
        AspNetPager2.PageSize = Convert.ToInt16(PageSizeDDL2.SelectedValue);

        string whereStr = " where CatID = @CatID and Status = 1 and Finished = 1 and Valid = 1 and IsList = 1";
        string sql = "select count(ID) as total from Articles " + whereStr;

        using (SqlConnection conn = (SqlConnection)new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@CatID", CategoryIDLabel.Text);
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

            whereStr = " where CatID = @CatID2 and Status = 1 and Finished = 1 and Valid = 1 and IsList = 1 ";
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
            cmd.Parameters.AddWithValue("@CatID2", CategoryIDLabel.Text);
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
        MyDataBind3();
    }

    protected void PageSizeDDL2_SelectedIndexChanged(object sender, EventArgs e)
    {
        MyDataBind3();
    }
}
