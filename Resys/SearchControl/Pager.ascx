<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Pager.ascx.cs" Inherits="SearchControls_Pager" %>
<div style="margin-top:20px;text-align:center">
当前页 ：<asp:Label ID="currentPageLabel" runat="server"  />
丨总页数：<asp:Label ID="howManyPagesLabel" runat="server" />
<div style="margin-top:15px;">
<asp:HyperLink ID="previousLink" Runat="server" CssClass="pagesLink">上一页</asp:HyperLink>
<asp:Repeater ID="pagesRepeater" runat="server">
  <ItemTemplate>
    <asp:HyperLink ID="hyperlink" runat="server" CssClass="pagesRepeater" Text='<%# Eval("Page")  %>' NavigateUrl='<%# Eval("Url") %>' />
  </ItemTemplate>
</asp:Repeater>
<asp:HyperLink ID="nextLink" Runat="server" CssClass="pagesLink">下一页</asp:HyperLink>
  
</div>
</div>