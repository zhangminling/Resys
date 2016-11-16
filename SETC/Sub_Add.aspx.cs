using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.IO;

public partial class Sub_Add : System.Web.UI.Page
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
                    using (SqlConnection conn = new DB().GetConnection())
                    {
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.CommandText = "select * from Cats order by Orders desc";
                        conn.Open();
                        SqlDataReader rd = cmd.ExecuteReader();
                        Cats.DataSource = rd;
                        Cats.DataValueField = "ID";
                        Cats.DataTextField = "CatName";
                        Cats.DataBind();
                        rd.Close();
                    }
                }
            }
        }
        

    }
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        int i;
       
        using (SqlConnection conn = new DB().GetConnection())
        {
            StringBuilder sb = new StringBuilder("Insert into Subs (SubName,Description,Valid,CatName,CatID) ");
            sb.Append(" values ( @SubName,@Description,@Valid,@CatName,@CatID) ");
            conn.Open();
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.AddWithValue("@Description", Description.Text);
            cmd.Parameters.AddWithValue("@SubName", SubName.Text);
            cmd.Parameters.AddWithValue("@CatName", Cats.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@CatID", Cats.SelectedValue);
            string radiobuttonvalue = "";
            if (true1.Checked)
            {
                radiobuttonvalue = true1.Text;
            }
            else if (false1.Checked)
            {
                radiobuttonvalue = false1.Text;
            }
            cmd.Parameters.AddWithValue("@Valid", radiobuttonvalue);

           

            i = cmd.ExecuteNonQuery();
            conn.Close();
            if (i == 1)
            {
                Response.Write("<script language='javascript'> alert('操作成功');window.location='Cat_Man.aspx';</script>");
            }
            else
            {
                Response.Write("<script language='javascript'> alert('操作失败，请重试！');window.location='Cat_Man.aspx';</script>");
            }
           
            //Response.Write("<script language='javascript'> alert('操作成功');</script>");

        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Cat_Man.aspx");
    }

}

