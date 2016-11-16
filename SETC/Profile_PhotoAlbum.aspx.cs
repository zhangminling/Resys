using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Profile_PhotoAlbum : System.Web.UI.Page
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
                MyInit();
            }
        }
    }

    private void MyInit()
    {
        LabelID.Text = Session["UserID"].ToString();
        using (SqlConnection conn = new DB().GetConnection())
        {
            string sql = "select * from PhotoAlbum where UserID=@UserID";
            //string sql = "select * from PhotoAlbum cross join PhotoAlbum_Photo where photoalbum.UserID=@UserID";
            //string sql="select min()"
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UserID", LabelID.Text);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            Repeater1.DataSource = rd;
            Repeater1.DataBind();
            rd.Close();
            conn.Close();
        }
    }

    protected void CreatAlbum_Click(object sender, EventArgs e)
    {
        Cover.Style["display"] = "block";
    }

    protected void EscCoverShow_Click(object sender, EventArgs e)
    {
        Cover.Style["display"] = "none";
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            string sql = "Insert into PhotoAlbum (UserID,PhotoAlbumName,Description,count,IsShare) Values (@UserIDPA,@PhotoAlbumNamePA,@DescriptionPA,@CountPA,@IsShare)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandText = "Insert into PhotoAlbum (UserID,PhotoAlbumName,Description,Photocounts) Values (@UserIDPA,@PhotoAlbumNamePA,@DescriptionPA,@CountPA)";
            cmd.Parameters.AddWithValue("@UserIDPA", LabelID.Text);
            cmd.Parameters.AddWithValue("@PhotoAlbumNamePA", PhotoAlbum1.Text);
            cmd.Parameters.AddWithValue("@DescriptionPA", txtDescription.Text);
            cmd.Parameters.AddWithValue("@CountPA", 0);
            cmd.Parameters.AddWithValue("@IsShare", IsShareDDL.SelectedValue);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        Cover.Style["display"] = "none";
        MyInit();
    }
    protected void AddPhoto_Click(object sender, EventArgs e)
    {
        Response.Redirect("Profile_Photo_Add.aspx");
    }
}