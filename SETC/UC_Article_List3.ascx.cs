using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class UC_Article_List3 : System.Web.UI.UserControl
{
    public string SubID { set; get; }  

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            MyInit();
        }
    }

    private void MyInit()
    {
        string sql = "select top 10 * from Articles where SubID = " + SubID + "and Status = 1 and Finished = 1 and Valid = 1 and IsList=1 Order by Orders Desc,CDT Desc,ID Desc";
        using (SqlConnection conn = (SqlConnection)new DB().GetConnection())
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            Repeater1.DataSource = rd;
            Repeater1.DataBind();
            rd.Close();
        }
    }
}
