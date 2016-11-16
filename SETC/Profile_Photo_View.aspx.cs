using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Profile_Photo_View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserID"] == null)
            {
                Util.ShowMessage("会话超期，请重新登录", "Login.aspx");
            }
            else
            {
                LabelID.Text = Request.QueryString["ID"];
                LabelUserID.Text = Session["UserID"].ToString();
                MyInit();
            }
        }
    }

    private void MyInit()
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            string sql = "select * from PhotoAlbum_Photo where UserID=@UserID and PhotoAlbumID=@PhotoAlbumID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UserID", LabelUserID.Text);
            cmd.Parameters.AddWithValue("@PhotoAlbumID", LabelID.Text);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                Panel1.Visible = false;
            }
            else
            {
                Panel1.Visible = true;
            }
            Repeater1.DataSource = rd;
            Repeater1.DataBind();
            rd.Close();

            cmd.CommandText = "select * from PhotoAlbum where ID=@ID";
            cmd.Parameters.AddWithValue("@ID", LabelID.Text);
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                LabelPhotoAlbum.Text = rd["PhotoAlbumName"].ToString();
            }
            rd.Close();
            conn.Close();
        }
    }
    protected void AddPhoto_Click(object sender, EventArgs e)
    {
        Response.Redirect("Profile_Photo_Add.aspx");
        Panel1.Visible = false;
    }
}