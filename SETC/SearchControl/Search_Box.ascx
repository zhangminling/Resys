﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search_Box.ascx.cs" Inherits="SearchControl_Search_Box" %>
<asp:Panel ID="searchPanel" runat="server" DefaultButton="goButton">
    <div id="searchTextBoxDiv">
        <asp:TextBox ID="searchTextBox" Runat="server" Width="175px" placeholder="全站搜索"
          MaxLength="50" />
        <asp:Button ID="goButton" Runat="server" 
          Text="搜索"  onclick="goButton_Click" /><br />
        <asp:CheckBox ID="allWordsCheckBox" Runat="server" 
          Text="精确搜索" />
    </div>
</asp:Panel>
