using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using Winsteps.Validator;
using System.Data.SqlClient;

public partial class MasterFrontPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        {
            AbsoluteUrl.Text = Request.Url.AbsoluteUri;
            string con = PageOperate.GetNullToString(AbsoluteUrl.Text.Trim());
            if (con == "")
            {
                PageOperate.AlertAndRedirect("请填写内容", "Build.aspx");
                return;
            }
            if (ImgCode.ImageUrl == "")
            {
                ImgCode.ImageUrl = "Handler.ashx?data=" + Server.HtmlEncode(con) + "&len=4";
            }
            if (Session["RoleID"] == null || Session["UserID"] == null)
            {
                Literal1.Text = "<a href='Login.aspx?u=0' target='_blank'>用户登录</a>";
                Literal2.Text = "<a href='Register.aspx' target='_blank'>用户注册</a>";
            }
            else
            {
                string username = "欢迎您，" + Convert.ToString(Session["UserName"]);
                Literal1.Text = "<a href='User_Center.aspx?' target='_blank'>" + username + "</a>";
                Literal2.Text = "<a href='Login.aspx' target='_blank'>退出</a>";
            }
        }
    }

    override protected void OnInit(EventArgs e)
    {
        if (Cache != null)
        {
            IDictionaryEnumerator idE = Cache.GetEnumerator();
            while (idE.MoveNext())
            {
                if (idE.Key != null && idE.Key.ToString().Equals(Session.SessionID))
                {
                    //已经登录
                    if (idE.Value != null && "XXXXXX".Equals(idE.Value.ToString()))
                    {

                        Cache.Remove(Session.SessionID);
                        Session["UserID"] = null;
                        Session["UserName"] = null;
                        Util.ShowMessage("您的帐号已在别处登陆，您被强迫下线！", "Login2.aspx");
                        Response.End();

                    }
                    break;
                }
            }
        }

    }
}
