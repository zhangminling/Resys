<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="Show_Add.aspx.cs" Inherits="Show_Add"  ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="clear:both;"></div>
<asp:Label ID="RandomID" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="IDLabel" runat="server" Text="" Visible="false"></asp:Label>
<div id="CurrentPosition">当前位置：<a href="#">作品管理</a> >> <a href="#">发布新作品</a></div>
<p>&nbsp;</p>
<table width="680" align="center" id="RegistTable">
    <tr><td width="120" align="center">&nbsp;</td>
    <td width="200" align="left"><asp:Button ID="CancelButton" runat="server" 
            Text=" 取  消 " onclick="CancelButton_Click" />
    </td>
    <td width="120" align="left">
        <asp:Button ID="SaveButton" runat="server" Text=" 保  存 " CssClass="Button" onclick="SaveButton_Click" />&nbsp;</td>
    <td width="200"><asp:Button ID="NextButton" runat="server" Text=" 下一步 " CssClass="Button" onclick="NextButton_Click"  /></td>
    </tr>
    <tr>
    <td width="120">&nbsp;</td>
    <td align="left" colspan="3"><asp:Label ID="ResultLabel" runat="server" 
            Font-Bold="True" ForeColor="#00CC00"></asp:Label></td>    
    </tr>
    <tr><td width="120">作品名称：</td>
    <td width="300" align="left" colspan="3"><asp:TextBox ID="TitleTB" runat="server" MaxLength="30" CssClass="TextBox" Width="560"></asp:TextBox></td>
    
    </tr>    
    <tr><td width="120">&nbsp;</td>
    <td align="left" colspan="3"><div id="div4" style="font-size:12px;color:#999;"> 给你作品设置一个描述性的标题，请勿超过20个字符。</div></td>    
    </tr>
    <tr><td width="120">作品分类：</td>
    <td width="200" align="left">

        <asp:DropDownList ID="CatalogsDDL" runat="server" Height="24" >
        </asp:DropDownList>
        &nbsp;
    </td>
    <td align="left">作品排序：</td>
    <td align="left"><asp:TextBox ID="Orders" runat="server" CssClass="TextBox" Text="1"></asp:TextBox>
        </td>
    </tr>    
    <tr><td>发表日期：</td><td>
        <asp:TextBox ID="CDT_TextBox" runat="server" CssClass="TextBox"></asp:TextBox>&nbsp;2012-1-1
        </td><td>&nbsp;</td>
    <td align="left"><div id="div1" style="font-size:12px;color:#999;">如果需要文章置顶，排序值需><asp:Label ID="MaxOrders" runat="server" Text="0"></asp:Label></div></td>    
    </tr>
    <tr><td width="120">作品简介：</td><td colspan="3">
        <asp:TextBox ID="AbsTextBox" runat="server" TextMode="MultiLine" Width="560px" 
            Height="120px"></asp:TextBox></td></tr>     
    <tr><td width="120">封面图片：</td>
    <td colspan="2">
        <asp:FileUpload ID="FileUpload1" runat="server" />
    &nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="UploadButton" runat="server" Text=" 上传 " 
            CssClass="Button" onclick="UploadButton_Click" />
    </td>
    <td align="left"><div id="div2" style="font-size:12px;color:#999;">图片最佳分辨率为320*160。</div></td>
    </tr>
    <tr><td colspan="4" align="center">
        <asp:Image ID="CoverPhoto" runat="server" Width="320" Height="160" /></td>
    </tr>
    
    <tr><td colspan="4">&nbsp;</td></tr>
</table>


</asp:Content>

