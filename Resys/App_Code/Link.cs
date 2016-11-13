using System;
using System.Web;
using System.Text.RegularExpressions;

/// <summary>
/// Link 的摘要说明
/// </summary>
public class Link
{

	public Link()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    // regular expression that removes characters that aren't a-z, 0-9, dash, 
    // underscore or space
    private static Regex purifyUrlRegex = new Regex("[^-a-zA-Z0-9_ ]", RegexOptions.Compiled);

    // regular expression that changes dashes, underscores and spaces to dashes
    private static Regex dashesRegex = new Regex("[-_ ]+", RegexOptions.Compiled);

    // prepares a string to be included in an URL
    private static string PrepareUrlText(string urlText)
    {
        // remove all characters that aren't a-z, 0-9, dash, underscore or space
        urlText = purifyUrlRegex.Replace(urlText, "");

        // remove all leading and trailing spaces
        urlText = urlText.Trim();

        // change all dashes, underscores and spaces to dashes
        urlText = dashesRegex.Replace(urlText, "-");

        // return the modified string    
        return urlText;
    }

    // Builds an absolute URL
    private static string BuildAbsolute(string relativeUri)
    {
        // get current uri
        Uri uri = HttpContext.Current.Request.Url;
        // build absolute path
        string app = HttpContext.Current.Request.ApplicationPath;
        if (!app.EndsWith("/")) app += "/";
        relativeUri = relativeUri.TrimStart('/');
        // return the absolute path
        return HttpUtility.UrlPathEncode(
          String.Format("http://{0}:{1}{2}{3}",
          uri.Host, uri.Port, app, relativeUri));
    }



    public static string ToArticles(string ID)
    {
        // prepare product URL name
        ArctilesDetails p = Search_Access.GetArticleDetails(ID.ToString());
        string prodUrlName = PrepareUrlText(p.Title);

        // build product URL
        return BuildAbsolute(String.Format("{0}-p{1}/", prodUrlName, ID));
    }

    public static string ToSearch(string searchString, bool allWords, string page)
    {
        if (page == "1")
            return BuildAbsolute(
              String.Format("/Search.aspx?Search={0}&AllWords={1}",
                searchString, allWords.ToString()));
        else
            return BuildAbsolute(
              String.Format("/Search.aspx?Search={0}&AllWords={1}&Page={2}",
                searchString, allWords.ToString(), page));
    }

    public static string ToSearchAboutFront(string searchString, bool allWords, string page)
    {
        if (page == "1")
            return BuildAbsolute(
              String.Format("/SearchAboutFrontPage.aspx?Search={0}&AllWords={1}",
                searchString, allWords.ToString()));
        else
            return BuildAbsolute(
              String.Format("/SearchAboutFrontPage.aspx?Search={0}&AllWords={1}&Page={2}",
                searchString, allWords.ToString(), page));
    }
}