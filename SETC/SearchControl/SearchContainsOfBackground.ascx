<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchContainsOfBackground.ascx.cs" Inherits="SearchControl_SearchContainsOfBackground" %>
<%--<asp:Label ID="Label1" runat="server" Text="" Visible="true"></asp:Label>
<asp:Label ID="Label2" runat="server" Text="" Visible="true"></asp:Label>--%>
<asp:Label ID="Label3" runat="server" Text="" Visible="false"></asp:Label>
<%@ Register src="Pager.ascx" tagname="Pager" tagprefix="uc1" %>
<uc1:Pager ID="topPager" runat="server" Visible="false" />
<asp:DataList ID="list" runat="server" RepeatColumns="1">
  <ItemTemplate>
    <h3 >
      <a href=' Article_View2.aspx?ID=<%# Eval("ID") %> ' >
        <%# HttpUtility.HtmlEncode(Eval("Title").ToString()) %>
      </a>
    </h3>
    <%# HttpUtility.HtmlEncode(Eval("Summary").ToString()) %>
    <p >
      <span>作者:
      <%# HttpUtility.HtmlEncode(Eval("Author").ToString()) %> 
        <%--<hr style="border:1px solid;" />--%></span>
        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;<span>发表时间:
      <%# HttpUtility.HtmlEncode(Eval("CDT").ToString()) %> </span>
        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;<span>浏览次数:
      <%# HttpUtility.HtmlEncode(Eval("ViewTimes").ToString()) %> </span>
    </p>
    <asp:PlaceHolder ID="attrPlaceHolder" runat="server"></asp:PlaceHolder>
  </ItemTemplate>
</asp:DataList>
<uc1:Pager ID="bottomPager" runat="server" Visible="False" />

