using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.IO;

public partial class CatMenu_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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

            StringBuilder sb = new StringBuilder("Insert into CatMenu (CatMenuName,Valid,Href,Orders) ");
            sb.Append(" values ( @CatMenuName,@Valid,@Href,@Orders) ");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);

            cmd.Parameters.AddWithValue("@CatMenuName", CatMenuName.Text);
            cmd.Parameters.AddWithValue("@Href", Href.Text);
            cmd.Parameters.AddWithValue("@Orders", Orders.Text);
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
            i = cmd.ExecuteNonQuery();
            conn.Close();
            if (i == 1)
            {
                Response.Write("<script language='javascript'> alert('操作成功');window.location='CatMenu_Man.aspx';</script>");
            }
            else
            {
                Response.Write("<script language='javascript'> alert('操作失败，请重试！');window.location='CatMenu_Man.aspx';</script>");
            }

        }
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("CatMenu_Man.aspx");
    }
}