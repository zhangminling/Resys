<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="ShowItem_Man.aspx.cs" Inherits="ShowItem_Man" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="clear:both;"></div>
<asp:Label ID="RandomID" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="IDLabel" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="DirLabel" runat="server" Text="" Visible="true"></asp:Label>
<div id="CurrentPosition">当前位置：<a href="#">作品管理</a> >> 
    <asp:HyperLink ID="HyperLink1" runat="server">发布新作品</asp:HyperLink> >> 
    <asp:HyperLink ID="HyperLink2" runat="server">作品上传</asp:HyperLink></div>
<p>&nbsp;</p>
<table width="800" align="center" id="RegistTable">
    <tr><td align="center"><asp:Label ID="TitleLabel" runat="server" Text="作品名称" Font-Bold="true"></asp:Label></td></tr>
    <tr><td style="padding-left:80px;"><asp:Button ID="BackButton" runat="server" Text=" 返回上一步 " onclick="BackButton_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="ResultLabel" runat="server" Font-Bold="True" ForeColor="#00CC00"></asp:Label>
    </td></tr>
    <tr><td align="center">上传作品：<asp:FileUpload ID="FileUpload1" runat="server" Width="460px" BorderWidth="1" />&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="UploadButton" runat="server" Text=" 上  传 " 
            onclick="UploadButton_Click" />
    </td></tr>
    <tr><td align="center">Link作品：
    <asp:TextBox ID="LinkItemTextBox" runat="server" Text='' Width="460" BorderWidth="1"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="LinkItemButton" runat="server" Text=" 上  传 " 
            onclick="LinkItemButton_Click" />
    </td></tr>
    <tr><td style="padding-left:80px;">
    1、图片的宽度不要超过660像素，高度不限；图片文件的大小不要超过4M。<br />
    2、为获得最佳效果，请用Photoshop等工具把图片处理后，再上传！<br />
    3、全部上传完毕后，可以输入图片的文字说明和序号，然后点击保存按钮！<br />
    4、Videos Upload by Admin and Link！<br />
    </td></tr>    
</table>
<p style="text-align:center;"><asp:Button ID="ConfirmButton" runat="server" Text=" 保  存 " onclick="ConfirmButton_Click" /></p>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Width="800" HorizontalAlign="Center" DataKeyNames="ID" >
<Columns>
<asp:TemplateField ItemStyle-Width="30" HeaderText="排序">
<ItemTemplate>
    <asp:TextBox ID="OrdersTextBox" runat="server" Text='<%# Eval("Orders") %>' Width="30" BorderWidth="1"></asp:TextBox>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField ItemStyle-Width="640">
<ItemTemplate>
    <p align="center"><img src='<%# Eval("FileURL") %>' width="640" alt="" /></p>
    <p align="left">
    <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Eval("Description") %>' TextMode="MultiLine" Width="640" Height="32" ></asp:TextBox>    
    </p>
</ItemTemplate>
</asp:TemplateField>
</Columns>
</asp:GridView>
</asp:Content>

