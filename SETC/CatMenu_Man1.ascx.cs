using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class CatMenu_Man1 : System.Web.UI.UserControl
{
    public string CatMenuID { set; get; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyInit();
        }
    }

    private void MyInit()
    {
        string sql = "select ID,SubMenuName,Valid from SubMenu where CatMenuID = " + CatMenuID + " Order by Orders asc";
        using (SqlConnection conn = (SqlConnection)new DB().GetConnection())
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            GridView1.DataSource = rd;
            GridView1.DataBind();
            rd.Close();
        }

    }
    protected void ButtonDel_Click(object sender, EventArgs e)
    {

    }

}