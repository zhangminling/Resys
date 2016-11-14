using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class CatMenu_Man : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyDataBind2();

        }

    }

    private void MyDataBind2()
    {
        // string sql = "select ID,CatName from Cats";
        using (SqlConnection conn = (SqlConnection)new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select ID,CatMenuName,Valid from CatMenu order by Orders asc";
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
        Response.Redirect("SubMenu_Add.aspx");
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
        Response.Redirect("CatMenu_Add.aspx");
    }
}