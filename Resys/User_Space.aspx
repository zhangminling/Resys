<%@ Page Language="C#" AutoEventWireup="true" CodeFile="User_Space.aspx.cs" Inherits="User_Space" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>个人空间</title>
    <link href="css3/bootstrap.css" rel='stylesheet' type='text/css' />
    <%--<link href="css3/style.css" rel='stylesheet' type='text/css' />--%>
    <link href="css3/Style1.css" rel='stylesheet' type='text/css' />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="header">
            <div class="container">
                <div class="header-profile">      
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/UserSpace/profile.png" />   
                </div>
            </div>
        </div>
        <div class="col-md-12">
            <div class="header-top">
                <p>&nbsp;<asp:Label ID="LabelPhotoSRC" runat="server" Text="Label" Visible="False"></asp:Label>
                    <asp:Label ID="LabelID" runat="server" Text="Label" Visible="False"></asp:Label>
                </p>
                <h2>
                    <asp:Label ID="LabelTrueName1" runat="server" Text="暂未填写"></asp:Label></h2>
                <p>&nbsp;</p>
                <small>
                    <asp:Label ID="LabelMotto" runat="server" Text="暂未填写"></asp:Label></small>
                <p>&nbsp;</p>
            </div>
        </div>       
        <div class="col-md-4">
            <div class="panel panel-success">
                <div class="panel-heading">基本信息</div>
                <p>&nbsp;</p>
                <div class="panel-body">
                    <ul class="p-info">
                        <li>
                            <div class="title">姓名</div>
                            <div class="desk">
                                <asp:Label ID="LabelTrueName" runat="server" Text="暂未填写"></asp:Label>
                            </div>
                        </li>
                        <li>
                            <div class="title">用户名</div>
                            <div class="desk">
                                <asp:Label ID="LabelUserName" runat="server" Text="暂未填写"></asp:Label>
                            </div>
                        </li>
                        <li>
                            <div class="title">邮箱</div>
                            <div class="desk">
                                <asp:Label ID="LabelEmail" runat="server" Text="暂未填写"></asp:Label>
                            </div>
                        </li>
                        <li>
                            <div class="title">电话</div>
                            <div class="desk">
                                <asp:Label ID="LabelTel" runat="server" Text="暂未填写"></asp:Label>
                            </div>
                        </li>
                        <li>
                            <div class="title">QQ</div>
                            <div class="desk">
                                <asp:Label ID="LabelQQ" runat="server" Text="暂未填写"></asp:Label>
                            </div>
                        </li>
                        <li>
                            <div class="title">家乡</div>
                            <div class="desk">
                                <asp:Label ID="LabelHometown" runat="server" Text="暂未填写"></asp:Label>
                            </div>
                        </li>
                        <li>
                            <div class="title">住址</div>
                            <div class="desk">
                                <asp:Label ID="LabelAddress" runat="server" Text="暂未填写"></asp:Label>
                            </div>
                        </li>
                        <li>
                            <div class="title">主页</div>
                            <div class="desk">
                                <asp:Label ID="LabelHomepage" runat="server" Text="暂未填写"></asp:Label>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>

            <div class="img2" style="padding-bottom: 20px; padding-top: 2px;">
                    <asp:Image ID="Image2" runat="server" Width="100%" Height="60" />
            </div>

            <div class="panel panel-success">
                <div class="panel-heading">关联社团</div>
                
                <div class="panel-body">
    <asp:Repeater ID="Repeater2" runat="server">
        <ItemTemplate>
            <%--<p><%# Eval("StdUnionName") %></p>--%>
            <a href="#" style="color:dodgerblue"><%# Eval("StdUnionName") %></a>
            <p>&nbsp;</p>
        </ItemTemplate>
    </asp:Repeater>
                </div>
           </div>
            

        </div>

        <div class="col-md-8">
            <div class="panel panel-success" style="display:none;">
                <div class="panel-heading">个人经历</div>
                <p>&nbsp;</p>
                <div class="panel-body">
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </div>
            </div>

            <div class="panel panel-success">
                <div class="panel-heading">个人简介</div>
                <p>&nbsp;</p>
                <div class="panel-body">
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                </div>
            </div>

            <div class="panel panel-success">
            <div class="panel-heading">日志</div>
            <p>&nbsp;</p>
            <div class="panel-body">
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <div class="col-md-12">
                            <%--<p><hr style="border:1 ;dashed #987cb9"; width="100%"; color=#987cb9 ;SIZE=4"/></p>--%>
                            <%--<p>
                                <hr style="border: 1px solid; width: 100%; color: #4cff00; font-size: 4px" />
                            </p>--%>

                            <div style="margin-right: 15px; float: left">
                                <asp:Image ID="ImageAvatar" runat="server" Height="60px" Width="60px" />
                                <p><span style="font-size: 18px; color: #1277c1"><%#Eval("UserName") %></span></p>
                            </div>
                            <div class="col-md-9">
                                <%--<p><%#Eval("LogContent") %></p>--%>
                                <asp:Label ID="LabelContent" runat="server" Text='<%#Bind("LogContent") %>'></asp:Label>
                                <p>&nbsp;</p>
                                <p><span style="color: #808080"><%# String.Format("{0:yyyy年MM月dd日 HH时}",Eval("CDT") ) %></span></p>
                                <p>&nbsp;</p>
                                <p>&nbsp;</p>
                            </div>
                            <%--<hr style="border:thin; color:#808080;width:100%; height:1px; "/>--%>
                        </div>

                    </ItemTemplate>
                </asp:Repeater>
            </div>
                <%--<h6 style="background-color: #DFF0D8; text-align: center; padding: 10px 0px 10px 0px;">点击显示<asp:LinkButton ID="LinkButton2" runat="server">所有评论>></asp:LinkButton></h6>--%>
                <%--<h6 style="background-color: #DFF0D8; text-align: center; padding: 10px 0px 10px 0px;">点击显示<a href="#" style="color:dodgerblue"></a>所有评论>></h6>--%>
            <asp:Panel ID="PanelMore" runat="server">
                <h4 style="background-color: #DFF0D8; text-align: center; padding: 10px 0px 10px 0px;"><asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">查看所有评论</asp:LinkButton></h4>
            </asp:Panel>      
        </div>

            <div class="panel panel-success">
                <div class="panel-heading">形象墙</div>
                <div class="panel-body">
                    <asp:Repeater ID="RepeaterPhoto" runat="server">
                        <ItemTemplate>
                            <div style="margin-top:25px;margin-left:8%;float:left;">
                                <asp:Image ID="ImagePhoto" runat="server" ImageUrl='<%#Eval("PhotoPath") %>' Width="175px" Height="125px" />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div style="margin-top:10px;width:100%;float:left"></div>
                    <div style="float:right;margin-top:20px; height: 20px;">
                        <%--<asp:LinkButton ID="BtnMore" runat="server" ForeColor="#0099FF">查看更多</asp:LinkButton>--%>
                        <a href="Space_Photo_View.aspx?ID=<%= Request.QueryString["ID"] %>" style="color:#0099FF">查看更多</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
