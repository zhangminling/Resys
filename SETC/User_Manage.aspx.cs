using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;

public partial class User_Manage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            string userid = Convert.ToString(Session["UserID"]);
            int RoleID = Convert.ToInt16(Session["RoleID"].ToString());

            if (Session["RoleID"] == null || Session["UserID"] == null )
            {
                Util.ShowMessage("用户登录超时，请重新登录！", "Login.aspx");
            }
            else if(RoleID > 1)
            {
                Util.ShowMessage("您没有访问该页面的权限！", "Login.aspx");
            }
            else
            {
             
                string username = Convert.ToString(Session["UserName"]);
                int roleID = Util.UpdateAvatar(username);
                using (SqlConnection conn = new DB().GetConnection())
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "select distinct RoleName as RoleName from Roles";
                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    RoleTypeDDL.DataSource = rd;
                    RoleTypeDDL.DataValueField = "RoleName";
                    RoleTypeDDL.DataTextField = "RoleName";
                    RoleTypeDDL.DataBind();
                    rd.Close();
                    RoleTypeDDL.Items.Insert(0, new ListItem("角色类型", ""));


                    cmd.CommandText = "select distinct Valid as Valid from Users";
                    rd = cmd.ExecuteReader();
                    IsValid.DataSource = rd;
                    IsValid.DataValueField = "Valid";
                    IsValid.DataTextField = "Valid";
                    IsValid.DataBind();
                    rd.Close();
                    IsValid.Items.Insert(0, new ListItem("状态", ""));

                    cmd.CommandText = "select * from UserTags where UserID = @UserID and Users <> 0 order by ID desc";
                    cmd.Parameters.AddWithValue("@UserID", userid);
                    rd = cmd.ExecuteReader();
                    CheckBoxList1.DataSource = rd;
                    CheckBoxList1.DataTextField = "TagName";
                    CheckBoxList1.DataValueField = "ID";
                    CheckBoxList1.DataBind();
                    rd.Close();
                }

                MyDataBind2();

            }
         
        }
    }

    private void MyDataBind2()
    {
        string userid = Convert.ToString(Session["UserID"]);
        AspNetPager1.PageSize = Convert.ToInt16(PageSizeDDL.SelectedValue);
        string param = SearchTB.Text;
        StringBuilder whereStr = new StringBuilder(" where 1= 1 ");
        if (!String.IsNullOrEmpty(param))
        {
            whereStr.Append(" and [UserName] like '%").Append(Server.HtmlEncode(param.Trim().Replace("'", ""))).Append("%' ").Append(" or [TrueName] like '%").Append(Server.HtmlEncode(param.Trim().Replace("'", ""))).Append("%' ").Append(" or [Email] like '%").Append(Server.HtmlEncode(param.Trim().Replace("'", ""))).Append("%' ").Append(" or [TelePhone] like '%").Append(Server.HtmlEncode(param.Trim().Replace("'", ""))).Append("%' ");
        }

        if (RoleTypeDDL.SelectedIndex > 0)
        {
            whereStr.Append(" and RoleName = '").Append(RoleTypeDDL.SelectedValue).Append("'");
        }
        if (IsValid.SelectedIndex > 0)
        {
            whereStr.Append(" and Valid = '").Append(IsValid.SelectedValue).Append("'");
        }
        string sql = "select count(ID) as total from Users " + whereStr.ToString();

        using (SqlConnection conn = new DB().GetConnection())
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
                sql = "Select top " + AspNetPager1.PageSize + " * from Users " + whereStr.ToString() + "and ID not in (" + userid + ") " + OrderDDL.SelectedValue;
            }
            else
            {
                // Select Top 页容量 * from 表 where 条件 and id not in	(Select Top 页容量*（当前页数-1） id 	from 表 where 条件 order by 排序条件) order by 排序条件
                sql = "Select top " + AspNetPager1.PageSize + " * from Users " + whereStr.ToString() + "and ID not in (" + userid + ")  and id not in ( select top " + AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1) + " id  from Users " + whereStr.ToString() + " " + OrderDDL.SelectedValue + " ) " + OrderDDL.SelectedValue;
                //sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER ( " + orderStr + ") AS MyRank,* FROM Article " + whereStr +" ) AS Rank " + whereStr + " and MyRank BETWEEN " +AspNetPager1.StartRecordIndex+" AND "+ (AspNetPager1.StartRecordIndex+AspNetPager1.PageSize-1) +orderStr;
            }
            //TestLabel.Text = sql;
            cmd.CommandText = sql;
            rd = cmd.ExecuteReader();
            GridView1.DataSource = rd;
            GridView1.DataKeyNames = new string[] { "ID" };
            GridView1.DataBind();
            rd.Close();


        }
    }

    private void TagsUsersSearch()
    {
        string tags = "";
        AspNetPager1.PageSize = Convert.ToInt16(PageSizeDDL.SelectedValue);
        StringBuilder whereStr = new StringBuilder(" where 1= 1 ");
        for (int i = 0; i <= CheckBoxList1.Items.Count - 1; i++)
        {
            if (CheckBoxList1.Items[i].Selected == true)
            {
                whereStr.Append(" and UserTagID = '").Append(CheckBoxList1.Items[i].Value).Append("'");
                tags += "," + CheckBoxList1.Items[i].Value;
            }
        }

        if (tags == "")
        {
            Util.ShowMessage("请先选择要查询的标签", "");
        }
        else
        {
            tags = tags.Substring(1);
            string sql = "select UserID from Users_UserTags where UserTagID in (" + tags + ") ";
            string sql2 = "SELECT count(UserID) from Users_UserTags where UserTagID in (" + tags + ")";
            using (SqlConnection conn = new DB().GetConnection())
            {
                SqlCommand cmd = conn.CreateCommand();
                SqlCommand cmd2 = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd2.CommandText = sql2;
                conn.Open();
                //sql = cmd.ExecuteScalar().ToString();

                //SqlDataReader rd2 = cmd2.ExecuteReader();

                int count = int.Parse(Convert.ToString(cmd2.ExecuteScalar()));
                AspNetPager1.RecordCount = count;
                string[] USERID = new string[count];
                SqlDataReader rd = cmd.ExecuteReader();
                for (int j = 0; j < count; j++)
                {
                    if (rd.Read())
                    {

                        USERID[j] = rd["UserID"].ToString();
                    }
                }
                rd.Close();

                string s = "";
                for (int j = 0; j < count; j++)
                {
                    s = string.Join(",", USERID);
                }
                //  Label4.Text = USERID[0];
                Label5.Text = s;
                Label6.Text = tags;


                if (Label5.Text == null) { }
                else
                {
                    if (AspNetPager1.CurrentPageIndex == 1)
                    {
                        sql = "Select top " + AspNetPager1.PageSize + " * from Users where ID in (" + Label5.Text + ") " + OrderDDL.SelectedValue;
                    }
                    else
                    {
                        // Select Top 页容量 * from 表 where 条件 and id not in	(Select Top 页容量*（当前页数-1） id 	from 表 where 条件 order by 排序条件) order by 排序条件
                        sql = "Select top " + AspNetPager1.PageSize + " * from Users " + whereStr.ToString() + " and id not in ( select top " + AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1) + " id  from Users " + whereStr.ToString() + " " + OrderDDL.SelectedValue + " ) " + OrderDDL.SelectedValue;
                        //sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER ( " + orderStr + ") AS MyRank,* FROM Article " + whereStr +" ) AS Rank " + whereStr + " and MyRank BETWEEN " +AspNetPager1.StartRecordIndex+" AND "+ (AspNetPager1.StartRecordIndex+AspNetPager1.PageSize-1) +orderStr;
                    }
                    //TestLabel.Text = sql;
                    cmd.CommandText = sql;
                    rd = cmd.ExecuteReader();
                    GridView1.DataSource = rd;
                    GridView1.DataKeyNames = new string[] { "ID" };
                    GridView1.DataBind();
                    rd.Close();
                }



            }
        }
    }

    //启用用户操作
    protected void IsTrue_Click(object sender, EventArgs e)
    {
        string ids = "";
        int j;
        string IP = Request.UserHostAddress;//IP地址
        string username = Convert.ToString(Session["UserName"]);
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox checkBox = (CheckBox)GridView1.Rows[i].FindControl("ChechBox1");
            if (checkBox.Checked == true)
            {
                ids += "," + GridView1.DataKeys[i].Value;
            }
        }
        if (ids == "")
        {
            Util.ShowMessage("请先选择要启用的用户", "");
        }
        else
        {
            ids = ids.Substring(1);
            UserIDs.Text = ids;
            string[] array = UserIDs.Text.Split(',');
            int k = array.Length;
            string s = "";
            string[] UserID = new string[k];
            string t = "True";
            using (SqlConnection conn = new DB().GetConnection())
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Update [Users] set Valid = @Valid  where ID in (" + UserIDs.Text + ") ";
                cmd.Parameters.AddWithValue("@Valid", t);
                j = cmd.ExecuteNonQuery();
                cmd.Dispose();

                cmd.CommandText = "select UserName from Users where ID in (" + UserIDs.Text + ") order by ID desc";
                SqlDataReader rd = cmd.ExecuteReader();
                for (int num1 = 0; num1 < k; num1++)
                {
                    if (rd.Read())
                    {
                        UserID[num1] = rd["UserName"].ToString();
                        for (int num2 = 0; num2 < k; num2++)
                        {
                            s = string.Join(",", UserID);
                            IsTrueUsers.Text = s;
                        }
                    }
                }
                rd.Close();
                conn.Close();

                string isTrueUserName = "个用户名为“" + IsTrueUsers.Text.Trim() + "”的用户";
                Util.UserUtil_Notes("成功启用了", j, isTrueUserName, username, "User_Manage.aspx", IP);
                Response.Write("<script language='javascript'> alert('操作成功');</script>");
            }
            MyDataBind2();
        }
    }

    //禁用用户操作
    protected void IsFalse_Click(object sender, EventArgs e)
    {
        string ids = "";
        int j;
        string IP = Request.UserHostAddress;//IP地址
        string username = Convert.ToString(Session["UserName"]);
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox checkBox = (CheckBox)GridView1.Rows[i].FindControl("ChechBox1");
            if (checkBox.Checked == true)
            {
                ids += "," + GridView1.DataKeys[i].Value;
            }
        }
        if (ids == "")
        {
            Util.ShowMessage("请先选择要禁用的用户", "");
        }
        else
        {
            ids = ids.Substring(1);
            UserIDs.Text = ids;
            string[] array = UserIDs.Text.Split(',');
            int k = array.Length;
            string s = "";
            string[] UserID = new string[k];
            string f = "False";
            using (SqlConnection conn = new DB().GetConnection())
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Update [Users] set Valid = @Valid  where ID in (" + UserIDs.Text + ") ";
                cmd.Parameters.AddWithValue("@Valid", f);
                j = cmd.ExecuteNonQuery();
                cmd.Dispose();

                cmd.CommandText = "select UserName from Users where ID in (" + UserIDs.Text + ") order by ID desc";
                SqlDataReader rd = cmd.ExecuteReader();
                for (int num1 = 0; num1 < k; num1++)
                {
                    if (rd.Read())
                    {
                        UserID[num1] = rd["UserName"].ToString();
                        for (int num2 = 0; num2 < k; num2++)
                        {
                            s = string.Join(",", UserID);
                            IsTrueUsers.Text = s;
                        }
                    }
                }
                rd.Close();
                conn.Close();

                string isTrueUserName = "个用户名为“" + IsTrueUsers.Text.Trim() + "”的用户";
                Util.UserUtil_Notes("成功禁用了", j, isTrueUserName, username, "User_Manage.aspx", IP);
                Response.Write("<script language='javascript'> alert('操作成功');</script>");
            }
            MyDataBind2();
        }
    }

    //全选操作
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

    //修改操作
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
            Response.Redirect(Server.HtmlEncode("User_Edit2.aspx?ID=" + ids));
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
            Response.Redirect(Server.HtmlEncode("User_Del.aspx?IDS=" + ids));
        }
    }

    protected void RoleTypeDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        MyDataBind2();
    }

    protected void IsValid_SelectedIndexChanged(object sender, EventArgs e)
    {
        MyDataBind2();
    }

    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        MyDataBind2();
    }
    protected void PageSizeDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        MyDataBind2();
    }
    protected void OrderDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        MyDataBind2();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        MyDataBind2();
    }

    protected void TagBtn_Click(object sender, EventArgs e)
    {
        TagsUsersSearch();
    }

}