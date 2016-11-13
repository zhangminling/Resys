using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class User_Space : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null && !String.IsNullOrEmpty(Request.QueryString["ID"].ToString()))
            {
                LabelID.Text = Request.QueryString["ID"];
                Random r = new Random();
                Image2.ImageUrl = "images/random/V" + (r.Next(12) + 1) + ".jpg";
                MyInit();
            }
            else if (Session["UserID"] != null)
            {
                LabelID.Text = Session["UserID"].ToString();
                Random r = new Random();
                Image2.ImageUrl = "images/random/V" + (r.Next(12) + 1) + ".jpg";
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
            string sql = "select * from Profile1 where UserID=@UserID1";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UserID1", LabelID.Text);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                LabelTrueName.Text = rd["TrueName"].ToString();
                LabelUserName.Text = rd["UserName"].ToString();
                LabelEmail.Text = rd["Email"].ToString();
                LabelTel.Text = rd["Telphone"].ToString();
                LabelQQ.Text = rd["QQ"].ToString();
                LabelHometown.Text = rd["Hometown"].ToString();
                //LabelHomepage.Text = rd["PersonalHomepage"].ToString();
                LabelAddress.Text = rd["Address"].ToString();
                LabelMotto.Text = rd["motto"].ToString();
                LabelTrueName1.Text = rd["TrueName"].ToString();
                Label2.Text = rd["Achievement"].ToString();
                if (rd["PhotoSRC"].ToString() == "")
                {
                    LabelPhotoSRC.Text = "~/images/UserSpace/profile.png";
                }
                else
                {
                    LabelPhotoSRC.Text = rd["PhotoSRC"].ToString();
                }
                
            }
            Image1.ImageUrl = LabelPhotoSRC.Text.ToString();
            rd.Close();

            cmd.CommandText= "select top 10 * from log where UserID=@UserID2";
            cmd.Parameters.AddWithValue("@UserID2", LabelID.Text);
            rd = cmd.ExecuteReader();
            Repeater1.DataSource = rd;
            Repeater1.DataBind();
            rd.Close();

            cmd.CommandText = "select * from Users Where ID=@UserID3";
            cmd.Parameters.AddWithValue("@UserID3", LabelID.Text);
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                LabelUserName.Text = rd["UserName"].ToString();
            }
            rd.Close();

            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                Image im = Repeater1.Items[i].FindControl("ImageAvatar") as Image;
                im.ImageUrl = LabelPhotoSRC.Text.ToString();
                //Label Lb = Repeater1.Items[i].FindControl("LabelContent") as Label;
                //Lb.BackColor = System.Drawing.ColorTranslator.FromHtml("#EEEEEE");
            }

            cmd.CommandText = "select * from Student_StudentUnion where UserID=@UserID4";
            cmd.Parameters.AddWithValue("@UserID4", LabelID.Text);
            rd = cmd.ExecuteReader();
            Repeater2.DataSource = rd;
            Repeater2.DataBind();
            rd.Close();

            cmd.CommandText = "select top 4 * from PhotoAlbum_Photo where UserID=@UserID5 order by id desc";
            cmd.Parameters.AddWithValue("@UserID5", LabelID.Text);
            rd = cmd.ExecuteReader();
            RepeaterPhoto.DataSource = rd;
            RepeaterPhoto.DataBind();
            rd.Close();

            conn.Close();
        }
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from log where UserID=@UserID2";
            cmd.Parameters.AddWithValue("@UserID2", LabelID.Text);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            Repeater1.DataSource = rd;
            Repeater1.DataBind();
            rd.Close();
            conn.Close();

            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                Image im = Repeater1.Items[i].FindControl("ImageAvatar") as Image;
                im.ImageUrl = LabelPhotoSRC.Text.ToString();
                //Label Lb = Repeater1.Items[i].FindControl("LabelContent") as Label;
                //Lb.BackColor = System.Drawing.ColorTranslator.FromHtml("#EEEEEE");
            }
            PanelMore.Visible = false;
        }
    }
}