using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class SearchControl_SearchContainsOfBackground : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PopulateControls();
    }

    private void PopulateControls()
    {
        // Retrieve Page from the query string
        string page = Request.QueryString["Page"];
        if (page == null) page = "1";
        // Retrieve Search string from query string
        string searchString = Request.QueryString["Search"];
        //Label1.Text = Request.QueryString["Search"];
        //Label2.Text = page; 
        // How many pages of products?
        int howManyPages = 1;
        // pager links format
        string firstPageUrl = "";
        string pagerFormat = "";

        // If performing a product search
        if (searchString != null)
        {
            // Retrieve AllWords from query string
            string allWords = Request.QueryString["AllWords"];
            // Perform search
            DataTable dt = Search_Access.Search(searchString, allWords, page, out howManyPages);
            //Label3.Text = dt.DefaultView.ToString();
            list.DataSource = dt;
            list.DataBind();
            // Display pager
            firstPageUrl = Link.ToSearch(searchString, allWords.ToUpper() == "TRUE", "1");
            pagerFormat = Link.ToSearch(searchString, allWords.ToUpper() == "TRUE", "{0}");

        }
        //else
        //{
        //    // Retrieve list of products on catalog promotion
        //    list.DataSource = Search_Access.GetProductsOnFrontPromo(page, out howManyPages);
        //    list.DataBind();
        //    // have the current page as integer
        //    int currentPage = Int32.Parse(page);

        //}

        // Display pager controls
        topPager.Show(int.Parse(page), howManyPages, firstPageUrl, pagerFormat, false);
        bottomPager.Show(int.Parse(page), howManyPages, firstPageUrl, pagerFormat, true);
    }

}