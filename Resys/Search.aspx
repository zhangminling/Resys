<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>

<%@ Register Src="~/SearchControl/SearchContainsOfBackground.ascx" TagPrefix="uc1" TagName="SearchContains" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:Label CssClass="CatalogTitle" ID="titleLabel" runat="server"></asp:Label>
  <br />
  <asp:Label CssClass="CatalogDescription" ID="descriptionLabel" runat="server"></asp:Label>
  <br />
    <uc1:SearchContains ID="SearchContains1" runat="server" />

</asp:Content>

