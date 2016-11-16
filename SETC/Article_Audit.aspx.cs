using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;

public partial class Article_Audit : System.Web.UI.Page
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
            int RoleID = Convert.ToInt16(Session["RoleID"].ToString());
            if (RoleID > 2)
            {
                if (RoleID > 4)
                {
                    Util.ShowMessage("对不起，你无权访问该页面！", "User_Center.aspx");
                }
                else { 
                PassBtn.Visible = false;
                NoPassBtn.Visible = false;}
            }
            else
            {
               
                PassBtn.Visible = true;
                NoPassBtn.Visible = true;
            }

            MyInit();
            MyDataBind(); 
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

           // if (CatsDDL.Items.Count > 1)
           // {
           //     CatsDDL.SelectedIndex = 1;
           // }

            cmd.CommandText = "select * from Subs where CatID = " + CatsDDL.SelectedValue + " order by Orders desc;";
            rd = cmd.ExecuteReader();
            SubsDDL.DataSource = rd;
            SubsDDL.DataValueField = "ID";
            SubsDDL.DataTextField = "SubName";
            SubsDDL.DataBind();
            rd.Close();
            SubsDDL.Items.Insert(0, new ListItem("二级分类", "-1"));      

            // RoleID=1,2,3,4，分别对应Administrator,Editor,Contributor,Author
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
        StringBuilder whereStr = new StringBuilder(" where Valid = 1 and Finished=1 and Status<>1 ");
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
        if (Convert.ToInt16(PassDDL.SelectedValue) >= 0)
        {
            whereStr.Append(" and Status = ").Append(PassDDL.SelectedValue);
        }
       
       
        string sql = "select count(ID) as total from Articles " + whereStr.ToString();

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
                sql = "Select top " + AspNetPager1.PageSize + " * from Articles " + whereStr.ToString() + " " + OrderDDL.SelectedValue;
            }
            else
            {
                // Select Top 页容量 * from 表 where 条件 and id not in	(Select Top 页容量*（当前页数-1） id 	from 表 where 条件 order by 排序条件) order by 排序条件
                sql = "Select top " + AspNetPager1.PageSize + " * from Articles " + whereStr.ToString() + " and id not in ( select top " + AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1) + " id  from Articles " + whereStr.ToString() + " " + OrderDDL.SelectedValue + " ) " + OrderDDL.SelectedValue;
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

    protected void PassDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        MyDataBind();
    }


    protected void UpdateBtn_Click(object sender, EventArgs e)
    {
        string ids = "";
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox checkBox = (CheckBox)GridView1.Rows[i].FindControl("ChechBox1");
            if (checkBox.Checked == true)
            {
                ids = GridView1.DataKeys[i].Value.ToString();
            }
        }
        if (!String.IsNullOrEmpty(ids))
        {
            Response.Redirect(Server.HtmlEncode("Article_Add.aspx?ID=" + ids));
        }
    }

    protected void DelBtn_Click(object sender, EventArgs e)
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
            IDS.Text = ids;
            string[] array = ids.Split(',');
            int k = array.Length;
            
            using (SqlConnection conn = new DB().GetConnection())
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Delete from Articles  where ID in (" + IDS.Text + ")";
                //cmd.Parameters.AddWithValue("@ID", IDS.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                

                MyDataBind();


                conn.Close();

            }


        }
        else
        {
            Response.Write("<script language='javascript'> alert('至少选择一项');</script>");
        }
    
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

    protected void OrdersBtn_Click(object sender, EventArgs e)
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            string sql = "Update Articles set Orders = @Orders where ID = @ID";
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                TextBox OrdersTB = (TextBox)GridView1.Rows[i].FindControl("OrdersTB");
                if (!GridView1.Rows[i].Cells[0].Text.Trim().Equals(OrdersTB.Text.Trim()))
                {
                    cmd.CommandText = "Update Articles set Orders = " + OrdersTB.Text.Trim() + " where ID = " + GridView1.DataKeys[i].Value;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
        }
        MyDataBind();
    }

   /* protected void Media_Click(object sender, EventArgs e)
    {
        string id = "0";
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox checkBox = (CheckBox)GridView1.Rows[i].FindControl("ChechBox1");
            if (checkBox.Checked == true)
            {
                id = GridView1.DataKeys[i].Value.ToString();
                break;
            }
        }
        Response.Redirect("Article_Media.aspx?ID=" + id);
    }
   */
    protected void SubsDDL_SelectedIndexChanged1(object sender, EventArgs e)
    {
        MyDataBind();
    }
    protected void PassBtn_Click(object sender, EventArgs e)
    {

        int i = DoPass(1);
        Response.Write(i + " Records Passed!");
        Response.Redirect("Article_Audit.aspx");
       // Util.ShowMessage(i + " Records Passed!", "Article_Audit.aspx");
    }

    protected void NoPassBtn_Click(object sender, EventArgs e)
    {
        int i = DoPass(2);
        Response.Write(i + " Records Not Passed!");
        Response.Redirect("Article_Audit.aspx");
    }

    /*
     *articles表的status自读值
     * status=0；
     * status=1
     */

    private int DoPass(int status)
    {
        int result = 0;
        string ids = "";
     if (status == 1)
        {
            passname.Text = "通过审核";
        }
        else { passname.Text = "不通过"; }
     
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("ChechBox1");
            if (cbox.Checked == true)
            {
                ids += "," + GridView1.DataKeys[i].Value;
            }
        }
        if (!String.IsNullOrEmpty(ids))
        {
            ids = ids.Substring(1);
            string sql = "Update Articles set Status = @Status,StatusName=@StatusName,AuditorID=@AuditorID,Auditor=@Auditor where id in ( " + ids + " )";
            using (SqlConnection conn = new DB().GetConnection())
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@StatusName", passname.Text);
                cmd.Parameters.AddWithValue("@AuditorID", Session["UserID"].ToString());
                cmd.Parameters.AddWithValue("@Auditor", Session["UserName"].ToString());
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
        }
        return result;
    }


}