<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="Profile_Photo_View.aspx.cs" Inherits="Profile_Photo_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="CurrentPosition">
        当前位置：<a href="File_Man.aspx">个人相册</a> >> <a href="#">照片浏览</a>
    </div>
    <div class="page-body">
        <div class="row">
            <div class="col-xs-12 col-md-12">
                <div class="widget">
                    <div class="widget-header">
                        <span class="widget-caption">
                            <asp:Label ID="LabelPhotoAlbum" runat="server" Text="照片浏览"></asp:Label></span><asp:Label ID="LabelID" runat="server" Text="Label" Visible="false"></asp:Label>
                        <asp:Label ID="LabelUserID" runat="server" Text="Label" Visible="false"></asp:Label>
                    </div>
                    <div class="widget-body">
                        <asp:Panel ID="Panel1" runat="server" Visible="false">
                            <div class="col-md-12" style="text-align:center;">
                                <h1>你尚未上传照片，是否上传照片</h1>
                                <p>&nbsp;</p>
                                <p><asp:Button ID="AddPhoto" runat="server" Text="上传照片" CssClass="btn btn-info btn-lg" OnClick="AddPhoto_Click" /></p>
                            </div>
                        </asp:Panel>
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <div style="margin-left:20px;margin-top:20px;float:left;">
                                    <asp:Image runat="server" Width="350px" Height="250px" ImageUrl='<%#Eval("PhotoPath") %>'></asp:Image>
                                    <p>&nbsp;&nbsp;<%#Eval("PhotoTrueName") %></p>
                                </div>     
                            </ItemTemplate>                            
                        </asp:Repeater>
                        <p>&nbsp;</p>
                        <div style="margin-top:1200px"></div>
                    </div>
                    <p>&nbsp;</p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

