using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // fill the table contents
        string searchString = Request.QueryString["Search"];
        titleLabel.Text = "全文检索";
        descriptionLabel.Text = "您输入的搜索词为 \"" + searchString + "\"";
        // set the title of the page
        this.Title = SearchConfiguration.SiteName +
                   " : Product Search : " + searchString;
    }
}