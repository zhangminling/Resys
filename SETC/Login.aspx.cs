using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Web.Caching;

public partial class Login : System.Web.UI.Page
{
    static int i = 0;
    string valid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            U_Label.Text = Request.QueryString["u"];
            Label1.Text = Request.UrlReferrer.ToString();
            ErrorLabel.Text = "";
            UserName.Focus();
            if (U_Label.Text != "0")
            {
                Cache.Remove(Session.SessionID);
                Session["UserID"] = null;
                Session["UserName"] = null;
            }
            else if (U_Label.Text == "0" && Session["UserID"] != null)
            {
                Util.ShowMessage("你已经成功登录！", "User_Center.aspx");
            }
            //ImageButton1.ImageUrl = "ValidateCode.aspx?ID=" + new Random().Next(100);
            this.Form.DefaultButton = Button1.UniqueID;
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string IP = Request.UserHostAddress;//IP地址
        using (SqlConnection conn = new DB().GetConnection())
        {
            string sql  = "select CongealDate,Valid from [Users] where UserName = @UserName2";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UserName2", UserName.Text);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                valid = rd["Valid"].ToString();
                //获取冻结密码找回的日期
                DateTime congeal = Convert.ToDateTime(rd["CongealDate"]);
                //创建TimeSpan对象，该对象表示两个时间的间隔
                TimeSpan ts = DateTime.Now - congeal;
                //获取两个时间的间隔以小时表示
                int hours = Convert.ToInt32(ts.TotalHours);
                //判断时间是否大于个24，如果大于将显示回答密码提示问题区域
                if (hours > 24)
                {
                    i++;
                    //判断用户输入验证码是否相等
                    if (Session["CheckCode"].ToString().ToLower() == ValidateCode.Text.ToLower().Trim())
                    {
                        //执行用户登录
                        int roleID = Util.DoLogin(UserName.Text.Trim(), Password.Text.Trim());

                        if (roleID == -1)
                        {
                            ErrorLabel.Text = "用户名或密码错误！";
                            if (i < 10)
                            {
                                Util.ShowMessage("登录失败！你还有" + (10 - i) + "次机会", "Login.aspx");
                            }
                            else
                            {
                                i = 0;
                                cmd.CommandText = "update Users set CongealDate = @CongealDate where UserName = @UserName ";
                                cmd.Parameters.AddWithValue("@UserName", UserName.Text);
                                cmd.Parameters.AddWithValue("@CongealDate", DateTime.Now.ToString());
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                           
                                Response.Write("<script language='javascript'> alert('答案不正确,您的10次机会已经用完，在24小时以后才能使用此功能找回密码');</script>");
                                Button1.Visible = false;
                            }

                        }
                        else
                        {
                            if (valid == "True")
                            {
                                //单点登录判断
                                if (Cache != null)
                                {
                                    IDictionaryEnumerator CacheIDE = Cache.GetEnumerator();
                                    string strKey = "";
                                    while (CacheIDE.MoveNext())
                                    {
                                        if (CacheIDE.Value != null && CacheIDE.Value.ToString().Equals(UserName.Text))
                                        {
                                            //已经登录
                                            strKey = CacheIDE.Key.ToString();
                                            Cache[strKey] = "XXXXXX";
                                            break;
                                        }
                                    }
                                }
                                TimeSpan SessTimeOut = new TimeSpan(0, 0, HttpContext.Current.Session.Timeout, 0, 0);
                                HttpContext.Current.Cache.Insert(Session.SessionID, UserName.Text, null, DateTime.MaxValue, SessTimeOut, CacheItemPriority.NotRemovable, null);
                                if (roleID == 1)
                                {
                                    Util.ShowMessage("登录成功！", Label1.Text);
                                    string addUserName = "位用户名为" + UserName.Text.Trim() + "”的用户成功登陆";
                                    Util.UserUtil_Notes("", 1, addUserName, UserName.Text, "User_Center.aspx", IP);
                                }
                                else
                                {
                                    Util.ShowMessage("登录成功！", "User_Center.aspx");
                                    string addUserName2 = "位用户名为" + UserName.Text.Trim() + "”的用户成功登陆";
                                    Util.UserUtil_Notes("", 1, addUserName2, UserName.Text, "User_Center.aspx", IP);                              
                                }
                            }
                            else 
                            {
                                ErrorLabel.Text = "您的账号还没有激活，请查看您的邮件激活账号！";
                            }
                       
                        }

                    }
                    else
                    {
                        ErrorLabel.Text = "验证码输入错误！";
                    }
                }
                else
                {
                    Button1.Visible = false;
                    Response.Write("<script language='javascript'> alert('还有" + (24 - hours) + "小时后可以使用该功能！！');</script>");
                }
            }

        }
      
    }
}