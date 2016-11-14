<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="Tag_Edit.aspx.cs" Inherits="Tag_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="Label1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="UserName" runat="server" Text="" Visible="false"></asp:Label>
     <asp:Label ID="UserIP" runat="server" Text="" Visible="false"></asp:Label>

    <div id="CurrentPosition">当前位置：<a href="Tags_Man.aspx">标签管理 </a>>><a href="#">用户标签</a> >> <a href="#">更新标签</a></div>
    <p>&nbsp;</p>
    <div class="col-lg-6 col-sm-6 col-xs-6">
        <div class="row">
            <div class="col-lg-12">
                <div class="well with-header">
                    <div class="header bordered-blue">修改标签</div>
                        <h6>标签名</h6>
                        <asp:TextBox ID="TagName" class="form-control" TextMode="MultiLine" Columns="20" Rows="1" placeholder="此字段不能为空" runat="server"></asp:TextBox>
                        <hr class="wide" />
                        <h6>标签描述</h6>
                        <asp:TextBox ID="TagDescription" class="form-control" TextMode="MultiLine" Columns="40" Rows="4" placeholder="关于你的标签" runat="server"></asp:TextBox>
                        <div style="margin-top: 20px;"></div>
                        <asp:Button ID="Button1" runat="server" 
                            class="btn btn-success" Text="确定修改" Style="margin-left: 5px; margin-top: 2px;" OnClick="Button1_Click" />
                </div>

            </div>
        </div>
    </div>
</asp:Content>

