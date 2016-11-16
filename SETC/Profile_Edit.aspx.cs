using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class Profile_Edit : System.Web.UI.Page
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
    protected void btnAlter_Click(object sender, EventArgs e)
    {
        ErrorLabel.Text = "";
        int i = 0;
        using (SqlConnection conn = new DB().GetConnection())
        {
            StringBuilder sb = new StringBuilder("Update [profile1] set TrueName=@TrueName3,Email=@Email3,Telphone=@Telphone3,motto=@motto3,");
            sb.Append("sex=@sex3,ClassID=@ClassID3,ClassName=@ClassName3 where UserID=@UserID3");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.AddWithValue("@TrueName3",TrueName1.Text);
            cmd.Parameters.AddWithValue("@Email3", Email.Text);
            cmd.Parameters.AddWithValue("@Telphone3", Tel.Text);
            cmd.Parameters.AddWithValue("@motto3", txtmotto.Text);
            cmd.Parameters.AddWithValue("@sex3", SexDDL.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@ClassID3", ClassesDDL.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@ClassName3", ClassesDDL.SelectedItem.Text);
            //cmd.Parameters.AddWithValue("@StdUnionID3", StdUnionDDL.SelectedItem.Value);
            //cmd.Parameters.AddWithValue("@StdUnionName3", StdUnionDDL.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@UserID3", LabelSno.Text);
            conn.Open();
            i = cmd.ExecuteNonQuery();

            cmd.CommandText = "delete from Student_StudentUnion where UserID=@UserIDStd";
            cmd.Parameters.AddWithValue("@UserIDStd", LabelSno.Text);
            cmd.ExecuteNonQuery();
            conn.Close();

            for (int q = 0; q < StdUnionCBL.Items.Count; q++)
            {
                SqlCommand cmd2 = conn.CreateCommand();
                cmd2.CommandText = "insert into [Student_StudentUnion] ( UserID,UserName,StdUnionID,StdUnionName) values (@UserIDStd4,@UserNameStd4,@StdUnionID,@StdUnionName)";
                cmd2.Parameters.AddWithValue("@UserIDStd4", LabelSno.Text);
                cmd2.Parameters.AddWithValue("@UserNameStd4", LabelName.Text);
                cmd2.Parameters.AddWithValue("@StdUnionID", StdUnionCBL.Items[q].Value.ToString());
                cmd2.Parameters.AddWithValue("@StdUnionName", StdUnionCBL.Items[q].Text.ToString());
                if (StdUnionCBL.Items[q].Selected == true)
                {
                    conn.Open();
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                }
            }

            
        }
        if (i == 1)
        {
            ErrorLabel.Text = "更新成功";
        }
        else
        {
            ErrorLabel.Text = "用户信息更新失败，请重试！";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }

    private void MyInit()
    {
        LabelSno.Text = Session["UserID"].ToString();

        if (HasPossess())
        {
            ErrorPanel.Visible = false;
            MainContent.Visible = true;
        }
        else
        {
            ErrorPanel.Visible = true;
            MainContent.Visible = false;
        }
    }

    private bool HasPossess()
    {
        bool r = false;
        using (SqlConnection conn = new DB().GetConnection())
        {
            string sql = "select * from profile1 where UserID=@UserID1";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UserID1", LabelSno.Text);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                r = true;
                TrueName1.Text = rd["TrueName"].ToString();
                Email.Text = rd["Email"].ToString();
                Tel.Text = rd["Telphone"].ToString();
                txtmotto.Text = rd["motto"].ToString();
                Editor1.Text = rd["Achievement"].ToString();
                Image1.ImageUrl = rd["PhotoSrc"].ToString();
            }
            else
            {
                r = false;
            }
            rd.Close();

            cmd.CommandText = "select * from users where ID=@ID1";
            cmd.Parameters.AddWithValue("@ID1", LabelSno.Text);
            rd=cmd.ExecuteReader();
            if (rd.Read())
            {
                LabelName.Text = rd["UserName"].ToString();
                rd.Close();
            }
            else
            {
                rd.Close();
            }

            cmd.CommandText = "Select * from Classes where Valid = 1 order by ID asc";
            rd = cmd.ExecuteReader();
            ClassesDDL.DataSource = rd;
            ClassesDDL.DataTextField = "ClassName";
            ClassesDDL.DataValueField = "ID";
            ClassesDDL.DataBind();
            rd.Close();

            cmd.CommandText = "Select * from StudentUnion order by ID asc";
            rd = cmd.ExecuteReader();
            StdUnionCBL.DataSource = rd;
            StdUnionCBL.DataTextField = "StdUnionName";
            StdUnionCBL.DataValueField = "ID";
            StdUnionCBL.DataBind();
            rd.Close();

            cmd.CommandText = "Select * from Profile_Experience where UserID=@UserIDExp";
            cmd.Parameters.AddWithValue("@UserIDExp", LabelSno.Text);
            rd = cmd.ExecuteReader();
            RepeaterExp.DataSource = rd;
            RepeaterExp.DataBind();
            rd.Close();

            cmd.CommandText = "select * from [Student_StudentUnion] where UserID=@UserID_SS";
            cmd.Parameters.AddWithValue("@UserID_SS", LabelSno.Text);
            rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                string StdUnionID = rd["StdUnionID"].ToString();
                StdUnionCBL.Items.FindByValue(StdUnionID).Selected = true;
            }


            conn.Close();
        }       
        return r;
    }

    protected void Btn_back_Click(object sender, EventArgs e)
    {

    }

    protected void btnIntroduction_Click(object sender, EventArgs e)
    {
        Button b = (Button)sender;
        b.CssClass = "btn btn-success";
        if (b.ID == "menu_1")
        {
            MultiView1.SetActiveView(View1);
            menu_2.CssClass = "btn btn-defaul";
            menu_3.CssClass = "btn btn-defaul";
            menu_4.CssClass = "btn btn-defaul";
        }
        else if (b.ID == "menu_2")
        {
            MultiView1.SetActiveView(View2);
            menu_1.CssClass = "btn btn-defaul";
            menu_3.CssClass = "btn btn-defaul";
            menu_4.CssClass = "btn btn-defaul";
        }
        else if (b.ID == "menu_3")
        {
            MultiView1.SetActiveView(View3);
            menu_1.CssClass = "btn btn-defaul";
            menu_2.CssClass = "btn btn-defaul";
            menu_4.CssClass = "btn btn-defaul";

        }
        else if (b.ID == "menu_4")
        {
            MultiView1.SetActiveView(View4);
            menu_1.CssClass = "btn btn-defaul";
            menu_2.CssClass = "btn btn-defaul";
            menu_3.CssClass = "btn btn-defaul";
        }
    }

    protected void GoBtn_Click(object sender, EventArgs e)
    {
        int q=-1;
        ErrorPanel.Visible = false;
        MainContent.Visible = true;
        using (SqlConnection conn = new DB().GetConnection())
        {
            try
            {
                string sql = "insert into profile1(UserID) values(@UserID)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UserID",LabelSno.Text);
                conn.Open();
                q=cmd.ExecuteNonQuery();
                if(q!=1)
                {
                    Response.Write("<script>alert('开始填写信息')</script>");
                }
            }
            catch(Exception ex)
            {
                //Response.Write("更新失败，失败原因：" + ex.Message);
                //int HasEx = ex.Message.ToString().IndexOf("唯一约束");
                if (ex.Message.ToString().IndexOf("唯一索引") > 0)
                {
                    //Response.Write("更新失败，失败原因：" + ex.Message);
                    //Response.Write("含有相同的学号,操作已取消");
                    Response.Write("<script>alert('含有相同学号，操作已取消')</script>");
                }
                else
                {
                    //Response.Write("含有相同的学号,操作已取消");
                    Response.Write("更新失败，失败原因：" + ex.Message);
                }
            }
            finally
            {
                conn.Close();
            }

            SqlCommand cmd1 = conn.CreateCommand();
            cmd1.CommandText = "Insert into PhotoAlbum (UserID,PhotoAlbumName,Description,Photocounts,IsShare) Values (@UserIDPA,@PhotoAlbumNamePA,@DescriptionPA,@CountPA,@IsShare)";
            cmd1.Parameters.AddWithValue("@UserIDPA", LabelSno.Text);
            cmd1.Parameters.AddWithValue("@PhotoAlbumNamePA", "日志相册");
            cmd1.Parameters.AddWithValue("@DescriptionPA", "日志相册，保存您在日志中上传的照片。默认仅自己可见");
            cmd1.Parameters.AddWithValue("@CountPA", 0);
            cmd1.Parameters.AddWithValue("@IsShare", 1);
            conn.Open();
            cmd1.ExecuteNonQuery();
            conn.Close();
        }
    }
    protected void Btn_Ok_Click(object sender, EventArgs e)
    {
        DoInsertArticle();
    }

    private void DoInsertArticle()
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            int i = 0;
            string sql = "update [Profile1] set Achievement=@Achievement1 where UserID=@UserID12";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Achievement1", Editor1.Text);
            cmd.Parameters.AddWithValue("@UserID12", LabelSno.Text);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }

    protected void AddExperience_Click(object sender, EventArgs e)
    {
        PanelExperience.Visible = true;

    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            Year2.Visible = false;
            MonthDDL2.Visible = false;
            zhijin.Visible = true;
        }
        else
        {
            Year2.Visible = true;
            MonthDDL2.Visible = true;
            zhijin.Visible = false;
        }
    }

    protected void BtnSava_Click(object sender, EventArgs e)
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            StringBuilder sb = new StringBuilder("insert into [Profile_Experience] ( UserID,SchoolName,PreviousPosts,Province,City,Year1,Year2,Month1,Month2,IsPresent,SchoolDescription)");
            sb.Append(" Values (@UserIDExp1,@SchoolName,@PreviousPosts,@Province,@City,@Year1,@Year2,@Month1,@Month2,@IsPresent,@SchoolDescription)");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.AddWithValue("@UserIDExp1", LabelSno.Text);
            cmd.Parameters.AddWithValue("@SchoolName",SchoolName.Text);
            cmd.Parameters.AddWithValue("@PreviousPosts",Job.Text);
            cmd.Parameters.AddWithValue("@Province",Province.Text);
            cmd.Parameters.AddWithValue("@City",City.Text);
            cmd.Parameters.AddWithValue("@Year1",Year1.Text);        
            cmd.Parameters.AddWithValue("@Month1",MonthDDL1.SelectedItem.Text);      
            cmd.Parameters.AddWithValue("@SchoolDescription",SchoolDescription.Text);
            if (CheckBox1.Checked == true)
            {
                cmd.Parameters.AddWithValue("@IsPresent", 1);
                cmd.Parameters.AddWithValue("@Year2", "");
                cmd.Parameters.AddWithValue("@Month2","");
            }
            else
            {
                cmd.Parameters.AddWithValue("@IsPresent", 0);
                cmd.Parameters.AddWithValue("@Month2", MonthDDL2.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Year2", Year2.Text);
            }
            conn.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }

        PanelExperience.Visible = false;
        SchoolName.Text = "";
        Job.Text = "";
        Province.Text = "";
        City.Text = "";
        MonthDDL1.SelectedIndex = 0;
        MonthDDL2.SelectedIndex = 0;
        CheckBox1.Checked = false;
        SchoolDescription.Text = "";
    }

    protected void ButCutImage_Click(object sender, EventArgs e)
    {
        Response.Write("<script type='text/javascript'>window.open('CutImage.aspx', 'newwindow', 'height = 1000, width = 1000, toolbar = no, menubar = no, scrollbars = no, resizable = no, location = no, status = no');</script>");//这里可以设置弹窗的大小，初始位置，标题等值
    }
}