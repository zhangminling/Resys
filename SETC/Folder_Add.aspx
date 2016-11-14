<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="Folder_Add.aspx.cs" Inherits="Folder_Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        input[type="radio"] {
            border: none;
        }

        input[type="radio"] + label {
            margin-right: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="FoldGUID" runat="server" Text="" Visible="false"></asp:Label>
    <div id="CurrentPosition">当前位置：<a href="Folder_Man.aspx">文件夹管理</a> >> <a href="#">新建文件夹</a></div>
    <p>&nbsp;</p>
    <div class="row">
        <div class="col-lg-8 col-sm-8 col-xs-12">
            <div class="well with-header  with-footer">
                <div class="header bordered-blue">新建文件夹</div>
                <div class="form-group">
                    <div id="RightContent">
                        <center>
                            <table width="760" align="center" class="MyTable" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td colspan="3"></td>
                                </tr>
                                <tr>
                                    <td width="180">&nbsp;</td>
                                    <td align="left" colspan="2">
                                        <asp:Label ID="ErrorLabel" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>请输入新的文件夹名：</td>
                                    <td width="300" align="left">
                                        <asp:TextBox ID="FolderName" runat="server"
                                            MaxLength="30" Width="300" CssClass="TextBox"></asp:TextBox></td>
                                    <td width="200" align="left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="3">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td width="200" align="left">
                                        <br />
                                        <asp:Button ID="Button1" runat="server" Text=" 确定新建 " class="btn btn-success"
                                            OnClick="Button1_Click" /></td>

                                    <td width="260" align="left">
                                        <br />
                                        <asp:Button ID="Button2" runat="server" Text=" 取消返回 " class="btn btn-default"
                                            OnClick="Button2_Click" />
                                    </td>
                                </tr>
                            </table>
                        </center>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

