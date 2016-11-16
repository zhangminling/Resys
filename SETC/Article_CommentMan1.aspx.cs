using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;

public partial class Article_CommentMan1 : System.Web.UI.Page
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

                if (Request.QueryString["IDS"] != null)
                {
                    IDSLabel.Text = Request.QueryString["IDS"].ToString();
                    MyInit1();
                }
                else MyInit();

            }
        }
    }
    private void MyInit()
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from ArticleView_Comment where  Visible=1  order by PublishTime desc";
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            GridView1.DataSource = rd;
            GridView1.DataBind();
            rd.Close();
            conn.Close();
        }
    }
    private void MyInit1()
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from ArticleView_Comment where ArticleID in (" + IDSLabel.Text + ") and Visible=1  order by PublishTime desc";
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            GridView1.DataSource = rd;
            GridView1.DataBind();
            rd.Close();
            conn.Close();
        }
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        int i = 0;
        string ids = "";
        for (int i1 = 0; i1 <= GridView1.Rows.Count - 1; i1++)
        {
            CheckBox checkBox = (CheckBox)GridView1.Rows[i1].FindControl("ChechBox1");
            if (checkBox.Checked == true)
            {
                ids += "," + GridView1.DataKeys[i1].Value;
            }
        }
        if (ids != "")
        {
            ids = ids.Substring(1);
            using (SqlConnection conn = new DB().GetConnection())
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "update ArticleView_Comment set Visible=0 where ID in (" + ids + ") ";
                conn.Open();
                i = cmd.ExecuteNonQuery();
                cmd.Dispose();

            }
            if (i > 0)
            {
                ResultLabel.Text = "成功删除" + i + "条留言！";
                ResultLabel.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                ResultLabel.Text = "操作失败，请重试！";
                ResultLabel.ForeColor = System.Drawing.Color.Red;
            }
        }
        else {
            ResultLabel.Text = "请择至少一条留言进行操作！";
            ResultLabel.ForeColor = System.Drawing.Color.Red;
        }
        MyInit();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect(Server.HtmlEncode("Article_Man.aspx"));
    }
}