using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Profile_Log_Edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserID"] == null)
            {
                Util.ShowMessage("会话超期，请重新登录", "Login2.aspx");
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
            string sql = "select * from log where UserID=@UserID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UserID", LabelID.Text);
            conn.Open();
            SqlDataReader rd=cmd.ExecuteReader();
            Repeater1.DataSource = rd;
            Repeater1.DataBind();
            rd.Close();

            cmd.CommandText = "select * from Users Where ID=@UserID1";
            cmd.Parameters.AddWithValue("@UserID1", LabelID.Text);
            rd=cmd.ExecuteReader();
            if (rd.Read())
            {
                LabelUserName.Text = rd["UserName"].ToString();
                LabelPhotoSRC.Text = rd["Avatar"].ToString();
                //Image Ig = (Image)(Repeater1.FindControl("ImageAvatar"));
                //Ig.ImageUrl = rd["Avatar"].ToString();
                rd.Close();
            }
            else
            {
                rd.Close();
            }

            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                Image im = Repeater1.Items[i].FindControl("ImageAvatar") as Image;
                im.ImageUrl = LabelPhotoSRC.Text.ToString();
                //Label Lb = Repeater1.Items[i].FindControl("LabelContent") as Label;
                //Lb.BackColor = System.Drawing.ColorTranslator.FromHtml("#EEEEEE");
            }

                conn.Close();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            string sql = "insert into [log] (LogContent,CDT,UserID,UserName) values (@LogContent,@CDT,@UserID2,@UserName)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@LogContent", Editor1.Text);
            cmd.Parameters.AddWithValue("@CDT", DateTime.Now);
            cmd.Parameters.AddWithValue("@UserID2", LabelID.Text);
            cmd.Parameters.AddWithValue("@UserName", LabelUserName.Text);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        MyInit();
    }

    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            if (e.CommandName == "Delete")
            {
                string sql = "delete from Log where id=@IDLog";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@IDLog", e.CommandArgument.ToString());
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }
        MyInit();

    }
}