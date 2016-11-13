<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchContains.ascx.cs" Inherits="SearchControls_SearchContains" %>
<%--<asp:Label ID="Label1" runat="server" Text="" Visible="true"></asp:Label>
<asp:Label ID="Label2" runat="server" Text="" Visible="true"></asp:Label>--%>
<asp:Label ID="Label3" runat="server" Text="" Visible="false"></asp:Label>
                <p> 当前位置>>   <asp:Label CssClass="CatalogTitle" ID="titleLabel" runat="server"></asp:Label>  >>搜索结果</p>
                         <!--  查找内容内容-->
 <h6>      <asp:Label  ID="descriptionLabel" runat="server"></asp:Label>  </h6> 
<%@ Register src="Pager.ascx" tagname="Pager" tagprefix="uc1" %>


 <div class="row">

          <asp:Panel id="Panel1" runat="server"> 
      <div class="col-lg-4 col-md-4 col-sm-12">
<asp:DropDownList ID="ReOrderArticles" runat="server"  OnSelectedIndexChanged="ReOrderArticles_SelectedIndexChanged" AutoPostBack="True">
     <asp:ListItem > 按权重默认排序</asp:ListItem>
    <asp:ListItem > 按浏览次数排序升序</asp:ListItem>    
     <asp:ListItem>按浏览次数排序降序</asp:ListItem>
    <asp:ListItem > 按文章发表时间排序升序</asp:ListItem>
    <asp:ListItem > 按文章发表时间排序降序</asp:ListItem>
</asp:DropDownList>

    </div>

    
      <div class="col-lg-3 col-md-3">

      </div>
        </asp:Panel>

      <div class="col-lg-5 col-md-5 col-sm-12" style="margin-top:-20px;">
      <asp:TextBox ID="SearchText" runat="server" Width="65%" />
  <asp:Button ID="TwoSearch" runat="server" Text="用户搜索"  OnClick="TwoSearch_Click" />
     
       </div>
     

        </div>
       
 <asp:Panel id="ArticlePanel" runat="server">  
<asp:DataList ID="list" runat="server" RepeatColumns="1" >
  <ItemTemplate>
     <div style="border-bottom:1px solid #dee0e5" >
    <p style=" margin-top:10px;">
       
      <a href=' Article_View2.aspx?ID=<%# Eval("ID") %> ' >
         
          <%# Container.ItemIndex + 1%> . <%# HttpUtility.HtmlEncode(Eval("Title").ToString()) %>
      </a>
    </p>
    <%# HttpUtility.HtmlEncode(Eval("Summary").ToString()) %>
   <p>
         <span>所属栏目:<%#  HttpUtility.HtmlEncode(Eval("CatName").ToString()) %></span>&nbsp; &nbsp; &nbsp; &nbsp;
       <span>作者:
      <%# HttpUtility.HtmlEncode(Eval("Author").ToString()) %> 
        <%--<hr style="border:1px solid;" />--%></span>
        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;<span>发表时间:
      <%# String.Format("{0:yyyy-MM-dd}",Eval("CDT") ) %> </span>
        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;<span>浏览次数:
      <%# HttpUtility.HtmlEncode(Eval("ViewTimes").ToString()) %> </span>
  </p>
         </ItemTemplate>
</asp:DataList>


<uc1:Pager ID="bottomPager" runat="server" Visible="False" />
      </asp:Panel>

<asp:Panel id="userpanel" runat="server">
<asp:Repeater ID="Repeater1" runat="server">
    <ItemTemplate>
     <div style="border-bottom:1px solid #dee0e5" >
    <p style=" margin-top:10px;">
      <a href=' Article_View2.aspx?ID=<%# Eval("ID") %> ' >
          <%# Container.ItemIndex + 1%> .<%# HttpUtility.HtmlEncode(Eval("UserName").ToString()) %></a></p>
   <p>
       <ItemTemplate>
              <img src='<%# Eval("Avatar")%>' alt='<%# Eval("UserName")%>' width="80" />
       </ItemTemplate>        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
       <span>注册时间:
      <%# String.Format("{0:yyyy-MM-dd}",Eval("RegisterDateTime") )%> 
        <%--<hr style="border:1px solid;" />--%></span>
        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;<span>积分:
      <%# HttpUtility.HtmlEncode(Eval("Credits").ToString()) %> </span>       
  </p>
        
   </div>
  </ItemTemplate>

 </asp:Repeater>
        </asp:Panel>
  

