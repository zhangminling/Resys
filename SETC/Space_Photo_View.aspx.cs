using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Space_Photo_View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null && !String.IsNullOrEmpty(Request.QueryString["ID"].ToString()))
            {
                MyInit();
            }
            else
            {
                Util.ShowMessage("不能跳转", "Index2.aspx");
            }
        }
        
    }

    private void MyInit()
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            string sql = "select * from PhotoAlbum_Photo where UserID=@UserID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UserID", Request.QueryString["ID"].ToString());
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            Repeater1.DataSource = rd;
            Repeater1.DataBind();
            rd.Close();
            conn.Close();
        }
    }
}