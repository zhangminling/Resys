<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="Cat_Man.aspx.cs" Inherits="Cat_Man" %>
<%@ Register TagPrefix="UserControl" TagName="Cat_Man1" Src="~/Cat_Man1.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .Cat {
            padding-left: 0px;
            padding-bottom: 15px;
            margin-bottom: 20px;
            margin-top: 5px;
            border-bottom: 2px solid #2dc3e8;
        }
        .Cat H3 {
            height: 24px;
            line-height: 24px;
            color: #04377c;
            text-indent: 20px;
            background-image: url(images/vbar_blue.png);
            background-repeat: no-repeat;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- 当前位置 -->
    <asp:Label ID="CategoryIDLabel" runat="server" Text="Label" Visible="false"></asp:Label>
    <asp:Label ID="IDSLabel" runat="server" Text="" Visible="false"></asp:Label>
    <div id="CurrentPosition">当前位置：<a href="Cat_Man.aspx">分类管理</a></div>
    <div class="page-body">
        <div class="row">
            <div class="col-xs-12 col-md-12">
                <div class="widget">
                    <div class="widget-header ">
                        <span class="widget-caption">分类管理</span>
                    </div>
                    <div class="widget-body">
                        <div style="margin-top: 20px;"></div>
                        <div align="left">
                            <asp:Button ID="CatAdd" runat="server" Text="新增一级分类" Height="32px" class="btn btn-info" OnClick="Button3_Click" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="SubAdd" runat="server" Text="新增二级分类" Height="32px" class="btn btn-info" OnClick="Button4_Click" />&nbsp;&nbsp;&nbsp;
                        </div>
                        <!-- 显示主栏目列表 -->
                        <asp:Panel ID="Panel2" runat="server">
                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <div class="Cat">
                                        <h3><a href='Cat_Edit.aspx?ID=<%# Eval("ID") %>'><%# Eval("CatName") %></a></h3>
                                        <div style="float: right; margin-top: -30px;margin-bottom:8px; margin-right:29px;">
                                            <a href='Cat_Edit.aspx?ID=<%# Eval("ID") %>' class="btn btn-default"><%# Eval("Valid") %></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <a href='Cat_Edit.aspx?ID=<%# Eval("ID") %>' class="btn btn-info">编辑</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <a href='Cat_Del.aspx?ID=<%# Eval("ID") %>' class="btn btn-danger">删除</a>
                                        </div>
                                        <div>
                                            <UserControl:Cat_Man1 ID="UC_Article_List1" runat="server" CatID='<%# Eval("ID") %>' />
                                        </div>
                                        <%--<div style="float: right; height: 30px; line-height: 30px;">
                                            <a href='Cat_Edit.aspx?ID=<%# Eval("ID") %>'></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </div>--%>
                                        <!-- <img src="images/random/H1.jpg" /> -->
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
