using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Cat_Man : System.Web.UI.Page
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
                int RoleID = Convert.ToInt16(Session["RoleID"].ToString());
                if (RoleID > 1)
                {
                    Util.ShowMessage("对不起，你无权访问该页面！", "User_Center.aspx");

                }
                else
                {

                    MyDataBind2();
                }
            }

        }

    }

    private void MyDataBind2()
    {
        // string sql = "select ID,CatName from Cats";
        using (SqlConnection conn = (SqlConnection)new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select ID,CatName,Valid from Cats";
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            Repeater1.DataSource = rd;
            Repeater1.DataBind();
            rd.Close();
        }
    }
    //protected void ButtonDel_Click(object sender, EventArgs e)
    //{
    //    string ids = "";
    //    for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
    //    {
    //        CheckBox checkBox = (CheckBox)GridView1.Rows[i].FindControl("ChechBox1");
    //        if (checkBox.Checked == true)
    //        {
    //            ids += "," + GridView1.DataKeys[i].Value;
    //        }
    //    }
    //    if (!String.IsNullOrEmpty(ids))
    //    {
    //        ids = ids.Substring(1);
    //        Response.Redirect(Server.HtmlEncode("CatSub_Del.aspx?IDS=" + ids));
    //    }
    //}
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("Sub_Add.aspx");
    }

    //protected void Button2_Click(object sender, EventArgs e)
    //{
    //    string ids = "";
    //    for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
    //    {
    //        CheckBox checkBox = (CheckBox)GridView1.Rows[i].FindControl("ChechBox1");
    //        if (checkBox.Checked == true)
    //        {
    //            ids = GridView1.DataKeys[i].Value.ToString();
    //        }
    //    }
    //    if (!String.IsNullOrEmpty(ids))
    //    {
    //        Response.Redirect(Server.HtmlEncode("Cat_Edi.aspx?ID=" + ids));
    //    }
    //}

    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("Cat_Add.aspx");
    }
    //protected void ButtonDel_Click(object sender, EventArgs e)
    //{
        
    //   //Response.Redirect(Server.HtmlEncode("CatSub_Del.aspx?IDS=" + ids));

    //}
}

    

  
                //Panel1.Visible = true;
              //  Panel2.Visible = false;
                
   

            /*
            cmd.CommandText = "Select top " + PageSizeDDL.SelectedValue + " * from Articles where CatID = " + CategoryID + " and Status = 1 and Finished = 1 order by Orders Desc,ID Desc";
            rd = cmd.ExecuteReader();
            GridView1.DataSource = rd;
            GridView1.DataBind();
            rd.Close();*/
   

 /*   private void MyDataBind()
    {
        AspNetPager1.PageSize = Convert.ToInt16(PageSizeDDL.SelectedValue);

        string whereStr = " where Finished = 1 ";
        string sql = "select count(ID) as total from Subs " + whereStr;

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
                sql = "Select top " + AspNetPager1.PageSize + " * from Subs " + whereStr + " Order by Orders ID Desc";
            }
            else
            {
                // Select Top 页容量 * from 表 where 条件 and id not in	(Select Top 页容量*（当前页数-1） id 	from 表 where 条件 order by 排序条件) order by 排序条件
                sql = "Select top " + AspNetPager1.PageSize + " * from Subs " + whereStr + " and id not in ( select top " + AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1) + " id  from Subs " + whereStr + " Order byID Desc" + " ) " + " Order by ID Desc";
                //sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER ( " + orderStr + ") AS MyRank,* FROM Article " + whereStr +" ) AS Rank " + whereStr + " and MyRank BETWEEN " +AspNetPager1.StartRecordIndex+" AND "+ (AspNetPager1.StartRecordIndex+AspNetPager1.PageSize-1) +orderStr;
            }
            //TestLabel.Text = sql;
            cmd.CommandText = sql;
            rd = cmd.ExecuteReader();
            GridView1.DataSource = rd;
            GridView1.DataBind();
            rd.Close();


            if (AspNetPager1.PageSize > GridView1.Rows.Count)
            {
                PageDiv.Visible = false;
            }
            else
            {
                PageDiv.Visible = true;
            }
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        MyDataBind();
    }

    protected void PageSizeDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        MyDataBind();
    }*/

