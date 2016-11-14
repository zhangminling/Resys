using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class User : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["RoleID"] == null || Session["UserID"] == null)
            {
                Util.ShowMessage("用户登录超时，请重新登录！", "Login2.aspx");
            }
            else
            {
                // load search box controls' values
                string allWords = Request.QueryString["AllWords"];
                string searchString = Request.QueryString["Search"];
                if (allWords != null)
                    allWordsCheckBox.Checked = (allWords.ToUpper() == "TRUE");
                if (searchString != null)
                    searchTextBox.Text = searchString;

                string username = Convert.ToString(Session["UserName"]);
                int roleID = Util.UpdateAvatar(username);
                AvatarImage.ImageUrl = Session["Avatar"].ToString();
                AvatarImage1.ImageUrl = Session["Avatar"].ToString();
                if (roleID == 1)
                {
                    AdminUser.Visible = true;
                    EditorArctile.Visible = true;
                    FilePanel.Visible = true;
                    ClassPanel.Visible = true;
                    FocusPanel.Visible = true;
                    MenuPanel.Visible = true;
                    UserTagPanel.Visible = true;
                  
                }
                else if (roleID == 2)
                {
                    
                    AdminUser.Visible = false;
                    EditorArctile.Visible = true;
                    FilePanel.Visible = true;
                    ClassPanel.Visible = false;
                    FocusPanel.Visible = true;
                    MenuPanel.Visible = false;
                    UserTagPanel.Visible = false;
                  
                }
                else if (roleID == 3)
                {
                    AdminUser.Visible = false;
                    EditorArctile.Visible = false;
                    FilePanel.Visible = true;
                    ClassPanel.Visible = false;
                    FocusPanel.Visible = false;
                    MenuPanel.Visible = false;
                    UserTagPanel.Visible = false;
                 
                }
                else if (roleID == 4)
                {
                    AdminUser.Visible = false;
                    EditorArctile.Visible = false;
                    FilePanel.Visible = true;
                    ClassPanel.Visible = false;
                    FocusPanel.Visible = false;
                    MenuPanel.Visible = false;
                    UserTagPanel.Visible = false;
                   
                }
                else 
                {
                    AdminUser.Visible = false;
                    EditorArctile.Visible = false;
                    FilePanel.Visible = false;
                    ClassPanel.Visible = false;
                    FocusPanel.Visible = false;
                    MenuPanel.Visible = false;
                    UserTagPanel.Visible = false;
                    Article.Visible = false;
                    Files.Visible = false;
                   
                }
                if (HasPossess())
                {
                    profilePanel.Visible = true;
                }
              
            }
        }
       
        
    }

    protected void goButton_Click(object sender, EventArgs e)
    {
        ExecuteSearch();
    }

    // Redirect to the search results page
    private void ExecuteSearch()
    {
        string searchText = searchTextBox.Text;
        bool allWords = allWordsCheckBox.Checked;
        if (searchTextBox.Text.Trim() != "")
            Response.Redirect(Link.ToSearch(searchText, allWords, "1"));
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

    private bool HasPossess()
    {
        bool r = false;
        using (SqlConnection conn = new DB().GetConnection())
        {
            string sql = "select * from profile1 where UserID=@UserIDProfile";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UserIDProfile",Session["UserID"].ToString());
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                r = true;
            }
            else
            {
                r = false;
            }
            rd.Close();
        }
        return r;
    }
}
  

