using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Configuration;

public partial class User_Edit2 : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        LabelUserID.Text = Request.QueryString["ID"];
        LabelRandomID.Text = Request.QueryString["RandomID"];
        if (!IsPostBack) 
        {
            string username = Convert.ToString(Session["UserName"]);
            string userid = Convert.ToString(Session["UserID"]);
            int RoleID = Convert.ToInt16(Session["RoleID"].ToString());
            if (!string.IsNullOrEmpty(LabelUserID.Text) && RoleID <= 1  ) 
            {
                PlaceHolder1.Visible = false;
                PlaceHolder2.Visible = true;
                PlaceHolder3.Visible = false;
            }
            else if (!string.IsNullOrEmpty(LabelRandomID.Text) && RoleID <= 1)
            {
                PlaceHolder1.Visible = false;
                PlaceHolder2.Visible = false;
                PlaceHolder3.Visible = true;
            }
            else 
            {
                PlaceHolder1.Visible = true;
                PlaceHolder2.Visible = false;
                PlaceHolder3.Visible = false;
            }
            if (Session["RoleID"] == null || Session["UserID"] == null)
            {
                Util.ShowMessage("用户登录超时，请重新登录！", "Login.aspx");
            }
            //else if ( username != LabelUserID.Text)
            //{
            //    Util.ShowMessage("您没有访问该页面的权限！", "Login2.aspx");
            //}
            else 
            {
             //int updateavatar = Util.UpdateAvatar(username);
            //Image1.ImageUrl = Session["Avatar"].ToString();
            if (Session["RoleID"] != null && !String.IsNullOrEmpty(Session["RoleID"].ToString()) && (Session["RoleID"].ToString() == "1"))
            {
                Role.Enabled = true;
                PasswordPanel.Visible = true;
                init();
               
           
            }
            else
            {
                Role.Enabled = false;
                RolePanel.Visible = false;
                PasswordPanel.Visible = false;
                ValidPanel.Visible = false;
            }
            using (SqlConnection conn = new DB().GetConnection())
            {
                string sql = "select * from Roles order by ID asc";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                Role.DataSource = rd;
                Role.DataTextField = "RoleName";
                Role.DataValueField = "ID";
                Role.DataBind();
                rd.Close();

            
                cmd.CommandText = "select UserName from Users where ID = @ID ";
                cmd.Parameters.AddWithValue("@ID", LabelUserID.Text);
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    LabelUserName.Text = rd["UserName"].ToString();
                }
                rd.Close();
               
                

                cmd.CommandText = "select UserName from Users where RandomID = @RandomID2  ";
                cmd.Parameters.AddWithValue("@RandomID2", LabelRandomID.Text);
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    LabelUserName2.Text = rd["UserName"].ToString();
                }
                rd.Close();



                cmd.CommandText = "Select * from [Users] where ID = @UserID2";
                cmd.Parameters.AddWithValue("@UserID2", userid);
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    Label1.Text = rd["UserName"].ToString();
                    TrueName.Text = rd["TrueName"].ToString();
                    Email.Text = rd["Email"].ToString();
                    OldPassword.Text = Password.Text = rd["Password"].ToString(); 
                    Image1.ImageUrl = rd["Avatar"].ToString();
                    TelePhone.Text = rd["TelePhone"].ToString();
                    string roleID = rd["RoleID"].ToString();
                    if (Role.Items.FindByValue(roleID) != null)
                    {
                        Role.ClearSelection();
                        Role.Items.FindByValue(roleID).Selected = true;
                    }
                    int valid = Convert.ToInt32(rd["Valid"]);
                    if (valid == 1)
                    {
                        true1.Checked = true;
                    }
                    else
                    {
                        false1.Checked = true;
                    }

                    Status.Text = rd["Status"].ToString();
                    RegisterDateTime.Text = rd["RegisterDateTime"].ToString();
                    LastLoginDateTime.Text = rd["LastLoginDateTime"].ToString();
                }
                rd.Close();


                if (RoleID <= 1) 
                {
                cmd.CommandText = "Select * from [Users] where ID = @UserID";
                cmd.Parameters.AddWithValue("@UserID", LabelUserID.Text);
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    Label1.Text = rd["UserName"].ToString();
                    TrueName.Text = rd["TrueName"].ToString();
                    Email.Text = rd["Email"].ToString();
                    OldPassword.Text = Password.Text = rd["Password"].ToString(); 
                    Image1.ImageUrl = rd["Avatar"].ToString();
                    TelePhone.Text = rd["TelePhone"].ToString();
                    string roleID = rd["RoleID"].ToString();
                    if (Role.Items.FindByValue(roleID) != null)
                    {
                        Role.ClearSelection();
                        Role.Items.FindByValue(roleID).Selected = true;
                    }
                    int valid = Convert.ToInt32(rd["Valid"]);
                    if (valid == 1)
                    {
                        true1.Checked = true;
                    }
                    else
                    {
                        false1.Checked = true;
                    }

                    Status.Text = rd["Status"].ToString();
                    RegisterDateTime.Text = rd["RegisterDateTime"].ToString();
                    LastLoginDateTime.Text = rd["LastLoginDateTime"].ToString();
                }
                rd.Close();
                }

                if (RoleID <= 1) 
                { 
                cmd.CommandText = "Select * from [Users] where RandomID = @RandomID";
                cmd.Parameters.AddWithValue("@RandomID", LabelRandomID.Text);
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    Label1.Text = rd["UserName"].ToString();
                    TrueName.Text = rd["TrueName"].ToString();
                    Email.Text = rd["Email"].ToString();
                    OldPassword.Text = Password.Text = rd["Password"].ToString();
                    Image1.ImageUrl = rd["Avatar"].ToString();
                    TelePhone.Text = rd["TelePhone"].ToString();
                    string roleID = rd["RoleID"].ToString();
                    if (Role.Items.FindByValue(roleID) != null)
                    {
                        Role.ClearSelection();
                        Role.Items.FindByValue(roleID).Selected = true;
                    }
                    int valid = Convert.ToInt32(rd["Valid"]);
                    if (valid == 1)
                    {
                        true1.Checked = true;
                    }
                    else
                    {
                        false1.Checked = true;
                    }

                    Status.Text = rd["Status"].ToString();
                    RegisterDateTime.Text = rd["RegisterDateTime"].ToString();
                    LastLoginDateTime.Text = rd["LastLoginDateTime"].ToString();
                }
                rd.Close();
                }

                //cmd.CommandText = "Select Avatar from [Users] where ID = @UserID3";
                //cmd.Parameters.AddWithValue("@UserID3", LabelUserID.Text);
                // rd = cmd.ExecuteReader();
                // if (rd.Read())
                // {
                    
                // }
                // else 
                // {
                //     Image1.ImageUrl = "Users/Avatars/20160908201834-270.png";
                // }

            }

            }
         
        }
    }

    private void init()
    {
        string UserID = Session["UserID"].ToString();
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from [Users] where [ID] = @UserID";
            cmd.Parameters.AddWithValue("@UserID", UserID);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                Image1.ImageUrl = rd["Avatar"].ToString();
            }
            rd.Close();
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
       
        if (FileUpload1.HasFile)
        {
            ResultLabel.Text = "";
            try
            {
                string fileName = FileUpload1.FileName;
                string extension = System.IO.Path.GetExtension(fileName).ToLower();
                string photoExtension = ConfigurationManager.AppSettings["PhotoExtension"].ToString();
                string[] ss = photoExtension.Split(',');
                string username = Convert.ToString(Session["UserName"]);
                string userid = Convert.ToString(Session["UserID"]);
                bool success = false;
                foreach (string s in ss)
                {
                    if (extension.Equals(s.Trim()))
                    {
                        success = true;
                        break;
                    }
                }
                if (success)
                {
                    string now = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string number = new Random().Next(1000).ToString();

                    // 头像的路径统一为根目录下的Users/Avatars目录下，这个文件夹，已经存在，不需要检查是否存在
                    string avatar = "Users/Avatars/" + now + "-" + number + extension;
                    FileUpload1.SaveAs(Server.MapPath("~/" + avatar));

                    using (SqlConnection conn = new DB().GetConnection())
                    {
                        SqlCommand cmd = (SqlCommand)conn.CreateCommand();
                        cmd.CommandText = "Update [Users] set Avatar = @Avatar where ID = @UserID";
                        cmd.Parameters.AddWithValue("@Avatar", avatar);
                        if (LabelUserID.Text == "" && LabelRandomID.Text == "")
                        {
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
                        }
                        else 
                        {
                            cmd.Parameters.AddWithValue("@UserID", LabelUserID.Text);
                        }
                        conn.Open();
                        int i = cmd.ExecuteNonQuery();

                        cmd.CommandText = "Update [Users] set Avatar = @Avatar3 where RandomID = @RandomID";
                        cmd.Parameters.AddWithValue("@Avatar3", avatar);
                        cmd.Parameters.AddWithValue("@RandomID", LabelRandomID.Text);
                        int q = cmd.ExecuteNonQuery();
                        conn.Close();

                        if (i == 1)
                        {
                            Image1.ImageUrl = avatar;
                           // Image AvatarImage = Master.FindControl("AvatarImage") as Image;
                            //AvatarImage.ImageUrl = avatar;
                            ResultLabel.Text = "恭喜，头像上传成功！";
                        }
                        else if (q == 1)
                        {
                            Image1.ImageUrl = avatar;
                            //Image AvatarImage = Master.FindControl("AvatarImage") as Image;
                            //AvatarImage.ImageUrl = avatar;
                            ResultLabel.Text = "恭喜，头像上传成功！";
                        }
                        else
                        {
                            ResultLabel.Text = "头像上传失败，请重试或与管理员联系！";
                        }

                        
                    }
                }
                else
                {
                    ResultLabel.Text = "头像文件的格式只能是 .jpg 、.png 或者.gif ！";
                }

            }
            catch (Exception exc)
            {
                // ResultLabel.Text = "上传文件失败。请重试！或者与管理员联系！<br>" + exc.ToString();
                // ResultLabel.Visible = true;
            }
        }
        else
        {
            ResultLabel.Text = "所选文件格式不符合要求";
        }


    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ErrorLabel.Text = "";
        int RoleID = Convert.ToInt16(Session["RoleID"].ToString());
        int i = 0, j = 0, q = 0;
        string username = Convert.ToString(Session["UserName"]);
        string userid = Convert.ToString(Session["UserID"]);
        string ip = Request.UserHostAddress;//IP地址
        using (SqlConnection conn = new DB().GetConnection()) 
        {
            string sql = "Update [Users] set TrueName=@TrueName,Password=@Password,Email = @Email,RoleID = @RoleID,RoleName = @RoleName,Valid=@Valid,TelePhone = @TelePhone,Status = @Status where ID = @UserID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@TrueName", TrueName.Text.Trim());
            cmd.Parameters.AddWithValue("@Email", Email.Text.Trim());
            cmd.Parameters.AddWithValue("@TelePhone", TelePhone.Text.Trim());
            cmd.Parameters.AddWithValue("@Status", Status.Text);
            if (OldPassword.Text != Password.Text)
            {
                cmd.Parameters.AddWithValue("@Password", Util.GetHash(Password.Text.Trim()));
            }
            else
            {
                cmd.Parameters.AddWithValue("@Password", Password.Text);
            }
            //cmd.Parameters.AddWithValue("@Password", Util.GetHash(Password.Text.Trim()));
            cmd.Parameters.AddWithValue("@UserID", userid);
            cmd.Parameters.AddWithValue("@RoleID", Role.SelectedValue);
            cmd.Parameters.AddWithValue("@RoleName", Role.SelectedItem.Text);
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
        }
        if (i == 1)
        {
            string newTagName = "用户名为“" + username + "”的信息";
            Util.UserUtil_Notes("更改了Id为", Convert.ToInt32(userid), newTagName, username, "User_Edit2.aspx", ip);
            ErrorLabel.Text = "用户信息更新成功！";
        }
       else
      {
         ErrorLabel.Text = "用户信息更新失败，请重试！";
      }
    }

    protected void Button3_Click(object sender, EventArgs e) 
    {
        ErrorLabel.Text = "";
        int RoleID = Convert.ToInt16(Session["RoleID"].ToString());
        int i;
        string username = Convert.ToString(Session["UserName"]);
        string userid = Convert.ToString(Session["UserID"]);
        string ip = Request.UserHostAddress;//IP地址
        using (SqlConnection conn = new DB().GetConnection()) 
        {
            string sql = "Update [Users] set TrueName=@TrueName3,Password=@Password3,Email = @Email3,RoleID = @RoleID3,RoleName = @RoleName3,Valid=@Valid3,TelePhone = @TelePhone3,Status = @Status3 where ID = @ID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ID", LabelUserID.Text);
            cmd.Parameters.AddWithValue("@TrueName3", TrueName.Text.Trim());
            cmd.Parameters.AddWithValue("@Email3", Email.Text.Trim());
            cmd.Parameters.AddWithValue("@TelePhone3", TelePhone.Text.Trim());
            cmd.Parameters.AddWithValue("@Status3", Status.Text);
            if (OldPassword.Text != Password.Text)
            {
                cmd.Parameters.AddWithValue("@Password3", Util.GetHash(Password.Text.Trim()));
            }
            else
            {
                cmd.Parameters.AddWithValue("@Password3", Password.Text);
            }
            //cmd.Parameters.AddWithValue("@Password", Util.GetHash(Password.Text.Trim()));
            //cmd.Parameters.AddWithValue("@UserID", LabelUserID.Text);
            cmd.Parameters.AddWithValue("@RoleID3", Role.SelectedValue);
            cmd.Parameters.AddWithValue("@RoleName3", Role.SelectedItem.Text);
            string radiobuttonvalue3 = "";
            if (true1.Checked)
            {
                radiobuttonvalue3 = true1.Text;
            }
            else if (false1.Checked)
            {
                radiobuttonvalue3 = false1.Text;
            }
            cmd.Parameters.AddWithValue("@Valid3", radiobuttonvalue3);
            conn.Open();
            i = cmd.ExecuteNonQuery();
            conn.Close();
        }
        if (i == 1)
        {
            string newTagName = "用户名为“" + username + "”的信息";
            Util.UserUtil_Notes("更改了Id为", Convert.ToInt32(userid), newTagName, username, "User_Edit2.aspx", ip);
            ErrorLabel.Text = "用户信息更新成功！";
        }
        else
        {
            ErrorLabel.Text = "用户信息更新失败，请重试！";
        }
    }

    protected void Button4_Click(object sender, EventArgs e) 
    {
       ErrorLabel.Text = "";
        int RoleID = Convert.ToInt16(Session["RoleID"].ToString());
        int i;
        string username = Convert.ToString(Session["UserName"]);
        string userid = Convert.ToString(Session["UserID"]);
        string ip = Request.UserHostAddress;//IP地址
        using (SqlConnection conn = new DB().GetConnection()) 
        {
            string sql = "Update [Users] set TrueName=@TrueName2,Password=@Password2,Email = @Email2,RoleID = @RoleID2,RoleName = @RoleName2,Valid=@Valid2,TelePhone = @TelePhone2,Status = @Status2 where RandomID = @RandomID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@RandomID", LabelRandomID.Text);
            cmd.Parameters.AddWithValue("@TrueName2", TrueName.Text.Trim());
            cmd.Parameters.AddWithValue("@Email2", Email.Text.Trim());
            cmd.Parameters.AddWithValue("@TelePhone2", TelePhone.Text.Trim());
            cmd.Parameters.AddWithValue("@Status2", Status.Text);
            if (OldPassword.Text != Password.Text)
            {
                cmd.Parameters.AddWithValue("@Password2", Util.GetHash(Password.Text.Trim()));
            }
            else
            {
                cmd.Parameters.AddWithValue("@Password2", Password.Text);
            }
            //cmd.Parameters.AddWithValue("@Password", Util.GetHash(Password.Text.Trim()));
            //cmd.Parameters.AddWithValue("@UserID", LabelUserID.Text);
            cmd.Parameters.AddWithValue("@RoleID2", Role.SelectedValue);
            cmd.Parameters.AddWithValue("@RoleName2", Role.SelectedItem.Text);
            string radiobuttonvalue2 = "";
            if (true1.Checked)
            {
                radiobuttonvalue2 = true1.Text;
            }
            else if (false1.Checked)
            {
                radiobuttonvalue2 = false1.Text;
            }
            cmd.Parameters.AddWithValue("@Valid2", radiobuttonvalue2);
            i = cmd.ExecuteNonQuery();
            conn.Close();
        }
        if (i == 1)
        {
            string newTagName = "用户名为“" + username + "”的信息";
            Util.UserUtil_Notes("更改了Id为", Convert.ToInt32(userid), newTagName, username, "User_Edit2.aspx", ip);
            ErrorLabel.Text = "用户信息更新成功！";
        }
        else
        {
            ErrorLabel.Text = "用户信息更新失败，请重试！";
        }
    } 

    protected void NextQuestion1_Click(object sender, EventArgs e) 
    {
        Question1.Visible = false;
        Question2.Visible = true;
    }

    protected void PreviousQuestion2_Click(object sender, EventArgs e) 
    {
        Question1.Visible = true;
        Question2.Visible = false;
    }

    protected void NextQuestion2_Click(object sender, EventArgs e) 
    {
        Question3.Visible = true;
        Question2.Visible = false;
    }

    protected void PreviousQuestion3_Click(object sender, EventArgs e) 
    {
        Question2.Visible = true;
        Question3.Visible = false;
    }

    protected void Complete_Click(object sender, EventArgs e) 
    {
        ResultReback.Text = "";
        string username = Convert.ToString(Session["UserName"]);
        int i = 0,j = 0;
        using (SqlConnection conn = new DB().GetConnection())
        {
            StringBuilder sb = new StringBuilder("insert into [UserQuestion_ForFindPassword]( UserName,Question1,Answer1,Question2,Answer2,Question3,Answer3)");
            sb.Append(" values ( @UserName,@Question1,@Answer1,@Question2,@Answer2,@Question3,@Answer3 ) ");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            if (LabelUserName.Text == "") 
            { 
            cmd.Parameters.AddWithValue("@UserName", LabelUserName2.Text); 
            
            }
            else if (LabelUserName2.Text == "")
            {
                cmd.Parameters.AddWithValue("@UserName", LabelUserName.Text);

            }
            else 
            { 
            cmd.Parameters.AddWithValue("@UserName", username);
            }
            cmd.Parameters.AddWithValue("@Question1", dropQues1.SelectedValue);
            cmd.Parameters.AddWithValue("@Answer1", TextBox2.Text.Trim());
            cmd.Parameters.AddWithValue("@Question2", dropQues2.SelectedValue);
            cmd.Parameters.AddWithValue("@Answer2", TextBox4.Text.Trim());
            cmd.Parameters.AddWithValue("@Question3", dropQues3.SelectedValue);
            cmd.Parameters.AddWithValue("@Answer3", TextBox6.Text.Trim());
            conn.Open();
            i = cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
       }
        if (i == 1)
        {
            ResultReback.Text = "密保问题保存成功！";
        }
        else if (j == 1) 
        {
            ResultReback.Text = "密保问题保存成功！";
        }
        else
        {
            ResultReback.Text = "保存密保问题出现未知错误！";
        }
    }
 
}