<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="Profile_PhotoAlbum.aspx.cs" Inherits="Profile_PhotoAlbum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">
     $(document).ready(function (e) {
         $(".module-close2").click(function (e) {
             $('.blanks2').hide();
             $(".module-area2").slideUp(400);//fadeOut()
         });
     });
    </script>  
    <style type="text/css">
        .module-close2 {
            background: url('../../images/close.png') no-repeat;
            width: 30px;
            height: 30px;
            cursor: pointer;
            position: absolute;
            right: 5px;
            top: 3px;
            text-indent: -999em;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- 新建相册模态对话框  --->
    
    <div id="Cover" runat="server" style="background-color: rgba(114, 114, 114, 0.5); position: fixed; padding: 10px 10px; width: 50%; height: 65%; overflow-y: scroll; z-index: 1000; margin: 0 10%; display: none;">
        <div class="col-lg-12">
            <div class="widget">
                <div class="widget-header">
                    <asp:Button ID="EscCoverShow" runat="server" class="module-close2" BorderStyle="None" OnClick="EscCoverShow_Click" />
                        <span class="widget-caption" style="margin-left:5px;">&nbsp;&nbsp;&nbsp;&nbsp;新建相册</span>            
                </div>
                <div runat="server" class="widget-body">
                    <form class="form">
                        <div class="form-group">
                            <asp:Label ID="LabelPhotoAlbum1" runat="server" Text="相册名称" Width="100px"></asp:Label>
                            <asp:TextBox ID="PhotoAlbum1" runat="server" MaxLength="30" CssClass="TextBox"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label1" runat="server" Text="相册权限：" Width="100px"></asp:Label>
                            <asp:DropDownList ID="IsShareDDL" runat="server" CssClass="btn btn-default">
                                <asp:ListItem Value="1">展示在个人空间</asp:ListItem>
                                <asp:ListItem Value="0">不展示在个人空间</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <p>相册描述</p>
                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Columns="50" Rows="6" CssClass="TextBoxMultiLine" MaxLength="140">
                            </asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="当前状态，不超过100个字符！" ControlToValidate="txtDescription" ValidationExpression=".{0,100}"> </asp:RegularExpressionValidator>
                        </div>      
                        <p>&nbsp;</p>
                        <asp:Button ID="btnAdd" runat="server" Text="确定" CssClass="btn btn-success" OnClick="btnAdd_Click"  />
                    </form>
                </div>
            </div>
        </div>
    </div>
       <!-- //结束  --->
    <div id="CurrentPosition">
        当前位置：<a href="File_Man.aspx">个人相册</a> >> <a href="#">相册管理</a>
    </div>
    <div class="page-body">
        <div class="row">
            <div class="col-xs-12 col-md-12">
                <div class="widget">
                    <div class="widget-header">
                        <span class="widget-caption">相册管理</span>
                    </div>
                    <div class="widget-body">
                        <div class="col-md-12">

                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="CreatAlbum" runat="server" Text="创建相册"  CssClass="btn btn-success" OnClick="CreatAlbum_Click"/>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="AddPhoto" runat="server" Text="上传图片" CssClass="btn btn-primary" OnClick="AddPhoto_Click" />
                            <asp:Label ID="LabelID" runat="server" Text="" Visible="false"></asp:Label>
                        </div>
                        <div style="margin-top: 60px;"></div>
                        <div class="col-md-12">
                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <div style="margin-left:30px;float:left;margin-top:20px;">
                                        <asp:Image ID="BtnPhotoAlbum" runat="server" Width="100px" Height="100px" ImageUrl="~/UserSpace/Folder.png" />
                                        <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href='Profile_Photo_View.aspx?ID=<%# Eval("ID") %>'><%#Eval("PhotoAlbumName") %></a></p>                 
                                        <%--<p>&nbsp;&nbsp;<%#Eval("PhotoAlbumName") %></p>--%>
                                    </div>
                                </ItemTemplate>        
                            </asp:Repeater>
                        </div>
                        <div style="margin-top:1200px"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

