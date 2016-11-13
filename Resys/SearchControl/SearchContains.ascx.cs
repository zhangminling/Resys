using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class SearchControls_SearchContains : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // fill the table contents
        string searchString = Request.QueryString["Search"];
        titleLabel.Text = "全文检索";
        descriptionLabel.Text = "您输入的搜索词为 \"" + searchString + "\"";
        // set the title of the page
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
            firstPageUrl = Link.ToSearchAboutFront(searchString, allWords.ToUpper() == "TRUE", "1");
            pagerFormat = Link.ToSearchAboutFront(searchString, allWords.ToUpper() == "TRUE", "{0}");
   
        }
        //else
        //{
        //    // Retrieve list of products on catalog promotion
        //    list.DataSource =Search_Access.GetProductsOnFrontPromo(page, out howManyPages);
        //    list.DataBind();
        //    // have the current page as integer
        //    int currentPage = Int32.Parse(page);

        //}

        // Display pager controls
        if (howManyPages <= 10)
        {
            bottomPager.Show(int.Parse(page), howManyPages, firstPageUrl, pagerFormat, true);
        }
        else
        {
            bottomPager.Show(int.Parse(page), 10, firstPageUrl, pagerFormat, true);
        }
      
    }

    protected void ReOrderArticles_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        PopulateControls2();
    }

    private void PopulateControls2()
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

        if (ReOrderArticles.SelectedIndex == 2)
        {

            // Retrieve AllWords from query string
            string allWords = Request.QueryString["AllWords"];
            // Perform search
            DataTable dt = Search_Access.SearchOrderByViewtimes(searchString, allWords, page, out howManyPages);
            //Label3.Text = dt.DefaultView.ToString();
            list.DataSource = dt;
            list.DataBind();
            // Display pager
            firstPageUrl = Link.ToSearchAboutFront(searchString, allWords.ToUpper() == "TRUE", "1");
            pagerFormat = Link.ToSearchAboutFront(searchString, allWords.ToUpper() == "TRUE", "{0}");

        }
        else if (ReOrderArticles.SelectedIndex == 1)
        {
            // Retrieve AllWords from query string
            string allWords = Request.QueryString["AllWords"];
            // Perform search
            DataTable dt = Search_Access.SearchArticlesOrderByViewtimesAsc(searchString, allWords, page, out howManyPages);
            //Label3.Text = dt.DefaultView.ToString();
            list.DataSource = dt;
            list.DataBind();
            // Display pager
            firstPageUrl = Link.ToSearchAboutFront(searchString, allWords.ToUpper() == "TRUE", "1");
            pagerFormat = Link.ToSearchAboutFront(searchString, allWords.ToUpper() == "TRUE", "{0}");
        }
        else if (ReOrderArticles.SelectedIndex == 3)
        {
            // Retrieve AllWords from query string
            string allWords = Request.QueryString["AllWords"];
            // Perform search
            DataTable dt = Search_Access.SearchArticlesOrderByCDTAsc(searchString, allWords, page, out howManyPages);
            //Label3.Text = dt.DefaultView.ToString();
            list.DataSource = dt;
            list.DataBind();
            // Display pager
            firstPageUrl = Link.ToSearchAboutFront(searchString, allWords.ToUpper() == "TRUE", "1");
            pagerFormat = Link.ToSearchAboutFront(searchString, allWords.ToUpper() == "TRUE", "{0}");
        }
        if (ReOrderArticles.SelectedIndex == 4)
        {

            // Retrieve AllWords from query string
            string allWords = Request.QueryString["AllWords"];
            // Perform search
            DataTable dt = Search_Access.SearchArticlesOrderByCDTDesc(searchString, allWords, page, out howManyPages);
            //Label3.Text = dt.DefaultView.ToString();
            list.DataSource = dt;
            list.DataBind();
            // Display pager
            firstPageUrl = Link.ToSearchAboutFront(searchString, allWords.ToUpper() == "TRUE", "1");
            pagerFormat = Link.ToSearchAboutFront(searchString, allWords.ToUpper() == "TRUE", "{0}");

        }


        // Display pager controls

        if (howManyPages <= 10)
        {
            bottomPager.Show(int.Parse(page), howManyPages, firstPageUrl, pagerFormat, true);
        }
        else
        {
            bottomPager.Show(int.Parse(page), 10, firstPageUrl, pagerFormat, true);
        }
    }


    protected void TwoSearch_Click(object sender, EventArgs e)
    {
        descriptionLabel.Text = "您输入的搜索词为 \"" + SearchText.Text + "\"";
        ArticlePanel.Visible = false;
        userpanel.Visible = true;
        Panel1.Visible = false;
        MyDataBind();
    }


    private void MyDataBind()
    {
        string param = SearchText.Text;
        StringBuilder whereStr = new StringBuilder(" where 1= 1 ");
        if (!String.IsNullOrEmpty(param))
        {
            whereStr.Append(" and [UserName] like '%").Append(Server.HtmlEncode(param.Trim().Replace("'", ""))).Append("%' ");
        }
        string sql = "select count(ID) as total from Users " + whereStr.ToString();

        //string sql = "select ID,UserName,RegisterDateTime,Credits from Users where UserName like 's%'";

        using (SqlConnection conn = (SqlConnection)new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            rd.Close();

            sql = "Select  ID,UserName,RegisterDateTime,Credits,Avatar from Users  " + whereStr.ToString();
            cmd.CommandText = sql;
            rd = cmd.ExecuteReader();
            Repeater1.DataSource = rd;
            Repeater1.DataBind();
            rd.Close();
        }
    }
}