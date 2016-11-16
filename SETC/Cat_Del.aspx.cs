using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.IO;

public partial class Cat_Del : System.Web.UI.Page
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
                if (RoleID > 1)
                {
                    Util.ShowMessage("对不起，你无权访问该页面！", "User_Center.aspx");

                }
                else {

            IDSLabel.Text = Request.QueryString["ID"];
            if (Request.QueryString["IDS"] != null)
            {
                IDSLabel.Text = Request.QueryString["IDS"].ToString();

            }
            else {MyInit(); }
                }
            }
        }  
        

    }


    private void MyInit()
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from Cats where ID in (" + IDSLabel.Text + ") order by ID desc";
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            GridView1.DataSource = rd;
            GridView1.DataBind();
            rd.Close();
            conn.Close();
        }
    }


    private void Del()
    {
        int i = 0;
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Delete from Cats where ID in (" + IDSLabel.Text + ") ";
            conn.Open();
            i = cmd.ExecuteNonQuery();
            cmd.Dispose();

            cmd.CommandText = "select * from Cats where ID in (" + IDSLabel.Text + ") order by ID desc";
            SqlDataReader rd = cmd.ExecuteReader();
            GridView1.DataSource = rd;
            GridView1.DataBind();
            rd.Close();
            conn.Close();

        }
        if (i > 0)
        {
            ResultLabel.Text = "成功删除！";
            ResultLabel.ForeColor = System.Drawing.Color.Green;

        }
        else
        {
            ResultLabel.Text = "操作失败，请重试！";
            ResultLabel.ForeColor = System.Drawing.Color.Red;
        }

    }




    protected void Button1_Click(object sender, EventArgs e)
    {
        string Count = "0";
        string Count1 = "0";
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select count(*) as Count from Articles where CatID=@CatID";
            cmd.Parameters.AddWithValue("@CatID", IDSLabel.Text);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                Count = rd["Count"].ToString();
            }
            rd.Close();


            cmd.CommandText = "select count(*) as Count2 from Subs where CatID=@CatID2";
            cmd.Parameters.AddWithValue("@CatID2", IDSLabel.Text);
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                Count1 = rd["Count2"].ToString();
            }
            rd.Close();



            if (Count == "0" && Count1 == "0")
            {
                Del();
            }
            else
            {
                if (Count1 != "0")
                    Response.Write("<script>alert(' 操作失败！！ \\n  \\n 该一级栏目下仍存在二级栏目，请删除该一级栏目下的二级栏目后，才能对栏目进行删除!')</script>");

                else
                {
                    Response.Write("<script>alert(' 操作失败！！ \\n  \\n 该栏目下仍存在文章，请删除该栏目下的文章后，才能对栏目进行删除!')</script>");
                }

            }


            conn.Close();
        }

    }




    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("Cat_Man.aspx");
    }
}
