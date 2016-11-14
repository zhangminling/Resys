<%@ Page Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="Article_More.aspx.cs" Inherits="Article_More" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .txt p {
            float: left;
            width: 20%;
        }

        .txt {
            clear: both;
            width: 90%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="clear: both;"></div>
    <asp:Label ID="RandomID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="IDLabel" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="UserName" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="UserID" runat="server" Text="" Visible="false"></asp:Label>
    <div id="CurrentPosition">当前位置：<a href="Article_Man.aspx">文章管理</a> >> <a href="#">发表新文章</a></div>
    <p>&nbsp;</p>
    <div class="well with-header">
        <div class="header bordered-blue">
            <strong>对文章“
                <asp:Label ID="Title" runat="server" Text=""></asp:Label>”进行完善</strong>
        </div>
        <div class="row">
            <div style="margin: auto; width: 90%;">                
                <asp:Label ID="ResultLabel" runat="server" Font-Bold="True" ForeColor="#00CC00"></asp:Label>
                <p></p>
                <div class="txt">
                    <p></p>
                    <p>文章排序：</p>
                    <p>
                        <asp:TextBox ID="Orders" runat="server" CssClass="TextBox" Text="1"></asp:TextBox></p>
                    <p style="font-size: 12px; color: #999;">如需置顶，排序值需><asp:Label ID="MaxOrders" runat="server" Text="0"></asp:Label></p>
                </div>
                <p></p>
                <div class="txt">
                    <p></p>
                    <p>发表部门：</p>
                    <p>
                        <asp:DropDownList ID="UserTag" runat="server" Height="30" Width="100px"></asp:DropDownList>
                    </p>
                    <p></p>
                </div>
                <p></p>
                <div class="txt">
                    <p></p>
                    <p>关键字(标签)：</p>
                    <p>
                        <asp:TextBox ID="ArticleTag" runat="server" CssClass="TextBox"></asp:TextBox></p>
                    <p></p>
                </div>
                <p></p>
                <div class="txt">
                    <p></p>
                    <p>文章封面图：</p>
                    <p>
                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="Button" />
                    </p>
                    <p>&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="UploadButton" runat="server" Text=" 上传 " class="btn btn-info" OnClick="UploadButton_Click" /></p>
                    <p style="width: 5%"></p>
                </div>
                <p></p>
                <div class="txt">
                    <p></p>
                    <p></p>
                    <p>
                        <asp:Image ID="CoverPhoto" runat="server" Width="320" Height="160" AlternateText="文章封面图" /></p>
                </div>
            </div>            
        </div>
        <p>&nbsp;</p>
        <div style="text-align: center;">
            <asp:Button ID="Button1" runat="server" Text="确定保存" OnClick="Button1_Click" class="btn btn-success" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" Text="取消返回" OnClick="Button2_Click" class="btn btn-default" />
        </div>
    </div>
</asp:Content>

