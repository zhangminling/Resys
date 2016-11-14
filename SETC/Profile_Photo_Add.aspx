<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="Profile_Photo_Add.aspx.cs" Inherits="Profile_Photo_Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" type="text/css" href="webuploader/style2.css" />
    <link rel="stylesheet" type="text/css" href="webuploader/webuploader.css" />

    <script type="text/javascript" src="webuploader/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" src="webuploader/webuploader.js"></script>
    <script type="text/javascript" src="webuploader/ImagUpload3.js"></script>
    <script type="text/javascript">
        $().ready(function () {
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="CurrentPosition">
            当前位置：<a href="#">个人相册</a> >> <a href="#">上传照片</a>
    </div>
        <p>&nbsp;</p>
    <div class="row">
        <div class="col-md-10">
            <div class="well with-header  with-footer">
                <div class="header bordered-blue">上传照片</div>

                <div class="form-group">
                    <p>1、选择要上传的图片</p>
                    <asp:Label ID="LabelID" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div id="wrapper">
                        <div id="container">
                            <!--头部，相册选择和格式选择-->

                            <div id="uploader">
                                <div class="queueList">
                                    <div id="dndArea" class="placeholder">
                                        <div id="filePicker"></div>
                                        <p>或将文件拖到这里，单次最多可选50个文件</p>
                                    </div>
                                </div>
                                <div class="statusBar" style="display: none;">
                                    <div class="progress">
                                        <span class="text">0%</span>
                                        <span class="percentage"></span>
                                    </div>
                                    <div class="info"></div>
                                    <div class="btns">
                                        <div id="filePicker2"></div>
                                        <div class="uploadBtn">开始上传</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div>
                        &nbsp;&nbsp;&nbsp;          
                    <asp:Label ID="FET" runat="server" Text="Label" Visible="false"></asp:Label>
                        <asp:Label ID="FS" runat="server" Text="Label" Visible="false"></asp:Label>
                        <asp:Label ID="FN" runat="server" Text="Label" Visible="false"></asp:Label>
                        <asp:Label ID="FP" runat="server" Text="Label" Visible="false"></asp:Label>
                        <asp:Label ID="ResourceTypeLabel" runat="server" Text="" Visible="false"></asp:Label>
                    </div>

                    <asp:TextBox ID="TextBox1" runat="server" Width="280px" Visible="false"></asp:TextBox>

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <p>2、选择保存的设置</p>
                            <p>&nbsp;</p>
                            <div>
                                &nbsp;&nbsp;&nbsp;
       
                            <asp:DropDownList ID="PhotoAlbumDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="PhotoAlbumDDL_SelectedIndexChanged">
                            </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="IsShareDDL" runat="server" AutoPostBack="True">
                                <asp:ListItem Value="1">展示在个人空间</asp:ListItem>
                                <asp:ListItem Value="0">不展示在个人空间</asp:ListItem>
                            </asp:DropDownList>                                   
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <p>&nbsp;</p>
                    <p>3、更改照片名字</p>
                    <p>&nbsp;</p>
                    <p>&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="TextBoxName" runat="server" CssClass="btn btn-default"></asp:TextBox></p>
                    <p>&nbsp;</p>
                    <div>
                        &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text="确定保存" class="btn btn-info" OnClick="Button1_Click1" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

