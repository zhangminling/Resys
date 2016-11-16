using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.IO;

public partial class Cat_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
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
        }
       
        }              
    
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        int i;
        /*SqlConnection conn = new SqlConnection(@"server=QH-20160713TJQE\SQLEXPRESS;database=SETC;Trusted_Connection=True");
        string Sql = "INSERT INTO Cats (CatName,Description) values ('" + CatName.Text + "','" + Description.Text + "')";
        SqlCommand cmd = new SqlCommand(Sql, conn);
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();*/
        using (SqlConnection conn = new DB().GetConnection())
        {

            StringBuilder sb = new StringBuilder("Insert into Cats (CatName,Description,Valid,IsShow) ");
            sb.Append(" values ( @CatName,@Description,@Valid,@IsShow) ");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.AddWithValue("@Description", Description.Text);
            cmd.Parameters.AddWithValue("@CatName", CatName.Text);
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
            conn.Open();

            string radiobuttonIsShow = "";
            if (true2.Checked)
            {
                radiobuttonvalue = "1";
            }
            else if (false2.Checked)
            {
                radiobuttonvalue = "0";
            }
            cmd.Parameters.AddWithValue("@IsShow", radiobuttonIsShow);
            i=cmd.ExecuteNonQuery();
            conn.Close();
            if (i == 1)
            {
                Response.Write("<script language='javascript'> alert('操作成功');window.location='Cat_Man.aspx';</script>");
            }
            else
            {
                Response.Write("<script language='javascript'> alert('操作失败，请重试！');window.location='Cat_Man.aspx';</script>");
            }
           
        }  
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("Cat_Man.aspx");
    }
}