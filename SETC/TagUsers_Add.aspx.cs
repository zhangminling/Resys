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

public partial class TagUsers_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int RoleID = Convert.ToInt16(Session["RoleID"].ToString());

            if (Session["RoleID"] == null || Session["UserID"] == null)
            {
                Util.ShowMessage("用户登录超时，请重新登录！", "Login.aspx");
            }
            else if (RoleID > 1)
            {
                Util.ShowMessage("您没有访问该页面的权限！", "Login.aspx");
            }
            else 
            { 
            if (Request.QueryString["ID"] != null)
            {
                
                IDSLabel.Text = Request.QueryString["ID"];
                string[] array = IDSLabel.Text.Split(',');
                int t = array.Length;
                Label6.Text = t.ToString();
                using (SqlConnection conn = new DB().GetConnection())
                {

                    string userid = Convert.ToString(Session["UserID"]);
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


                    cmd.CommandText = "select * from UserTags where ID in (" + IDSLabel.Text + ") order by ID desc";
                    rd = cmd.ExecuteReader();
                    TagsList.DataSource = rd;
                    TagsList.DataTextField = "TagName";
                    TagsList.DataValueField = "ID";
                    TagsList.DataBind();
                    rd.Close();

                    cmd.CommandText = "select count(*) as maxrow from [Users_UserTags] where UserTagID=@TagID1";
                    cmd.Parameters.AddWithValue("@TagID1", IDSLabel.Text);
                    SqlDataReader rd1 = cmd.ExecuteReader();
                    if (rd1.Read())
                    {
                        count.Text = rd1["maxrow"].ToString();
                    }
                    rd1.Close();


                    string s = "";
                    int i = Convert.ToInt32(count.Text);
                    string[] UserID = new string[i];
                    cmd.CommandText = "select * from [Users_UserTags] where UserTagID in (" + IDSLabel.Text + ")";
                    SqlDataReader rd2 = cmd.ExecuteReader();
                    if (i != 0)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            if (rd2.Read())
                            {
                                UserID[j] = rd2["UserID"].ToString();
                                for (int k = 0; k < i; k++)
                                {
                                    s = string.Join(",", UserID);
                                    UserIDs.Text = s;
                                }
                            }
                        }
                    }

                    else
                    {
                        int s1 = 0;
                        UserIDs.Text = s1.ToString();
                    }
                    rd2.Close();
                    MyDataBind2();
                }

             }
            
        
            for (int i = 0; i <= TagsList.Items.Count - 1; i++)
            {
                 TagsList.Items[i].Selected = true;
            }

        }
        }
    }


   

    private void MyDataBind2()
    {
        AspNetPager1.PageSize = Convert.ToInt16(PageSizeDDL.SelectedValue);
        string param = SearchTB.Text;
        StringBuilder whereStr = new StringBuilder(" where 1= 1 ");
        if (!String.IsNullOrEmpty(param))
        {
            whereStr.Append(" and [UserName] like '%").Append(Server.HtmlEncode(param.Trim().Replace("'", ""))).Append("%' ");
        }
        if (RoleTypeDDL.SelectedIndex > 0)
        {
            whereStr.Append(" and RoleName = '").Append(RoleTypeDDL.SelectedValue).Append("'");
        }

        //string sql = "select count(ID) as total from Users " + whereStr.ToString();
        string sql = "select count(ID) as total from Users " + whereStr.ToString() + "and ID not in (" + UserIDs.Text + ")";
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
                sql = "Select top " + AspNetPager1.PageSize + " * from Users " + whereStr.ToString() + "and ID not in (" + UserIDs.Text + ") " + OrderDDL.SelectedValue;
            }
            else
            {
                // Select Top 页容量 * from 表 where 条件 and id not in	(Select Top 页容量*（当前页数-1） id 	from 表 where 条件 order by 排序条件) order by 排序条件
                sql = "Select top " + AspNetPager1.PageSize + " * from Users " + whereStr.ToString() + "and ID not in (" + UserIDs.Text + ")  and id not in ( select top " + AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1) + " id  from Users " + whereStr.ToString() + " " + OrderDDL.SelectedValue + " ) " + OrderDDL.SelectedValue;
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

    protected void AddBtn_Click(object sender, EventArgs e)
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
        ids = ids.Substring(1);
        //Label3.Text = ids; 
        string[] array = ids.Split(',');
        string[] array2 = IDSLabel.Text.Split(',');
        int k = array.Length;
        //int k1 = array2.Length;
        string[] USERID = new string[20];
        string[] USERNAME = new string[20];
        string USERTAGID = "";
        string USERTAGNAME = "";
        int p;
        string coun = count.Text;
        using (SqlConnection conn = new DB().GetConnection()) 
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from Users where ID in (" + ids + ") order by ID desc  ";
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            for ( p = 0; p <= k - 1; p++) { 
            if (rd.Read()) 
            {
                USERID[p] = rd["ID"].ToString();
                USERNAME[p] = rd["UserName"].ToString();
              
            }
            }
            rd.Close();

            Label3.Text = USERID[p];
            cmd.CommandText = "select * from UserTags where ID in (" + IDSLabel.Text + ") order by ID desc";
            SqlDataReader rd1 = cmd.ExecuteReader();
            //for (p = 0; p <= k1 - 1; p++)
            //{
                if (rd1.Read())
                {
                    USERTAGID = rd1["ID"].ToString();
                    USERTAGNAME = rd1["TagName"].ToString();

                }
            //}
            rd1.Close();

            //cmd.CommandText = "update UserTags set Users= @Users where  ID in (" + IDSLabel.Text + ")";
            //cmd.Parameters.AddWithValue("@Users", k);
            //cmd.ExecuteNonQuery();

            int Users = Convert.ToInt32(coun);
            Users = Users + k;
            cmd.CommandText = "update UserTags set Users= @Users where ID in (" + IDSLabel.Text + ")";
            cmd.Parameters.AddWithValue("@Users", Users);
            cmd.ExecuteNonQuery();
            
            //string[] Arrays = USERID.Split(',');
           //string[] Arrays1 = USERNAME.Split(',');
            //for (int j1 = 0; j1 <= k1 - 1; j1++) {
                for (int j = 0; j <= k - 1; j++)
                {
                    //USERID = Arrays[j];
                    //USERNAME = Arrays1[j];
                    StringBuilder sb = new StringBuilder("insert into Users_UserTags(UserID,UserName,UserTagID,TagName )");
                    sb.Append(" values ( @UserID,@UserName,@UserTagID,@TagName ) ");
                    SqlCommand cmd1 = new SqlCommand(sb.ToString(), conn);
                    cmd1.Parameters.AddWithValue("@UserID", USERID[j]);
                    cmd1.Parameters.AddWithValue("@UserName", USERNAME[j]);
                    cmd1.Parameters.AddWithValue("@UserTagID", USERTAGID);
                    cmd1.Parameters.AddWithValue("@TagName", USERTAGNAME);
                    cmd1.ExecuteNonQuery();
                }
            //}
            conn.Close();
            Util.ShowMessage("操作成功", "Tags_Man.aspx");

        }

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

    protected void RoleTypeDDL_SelectedIndexChanged(object sender, EventArgs e)
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

   
}