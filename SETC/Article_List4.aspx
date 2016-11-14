<%@ Page Title="" Language="C#" MasterPageFile="~/MasterFrontPage.master" AutoEventWireup="true" CodeFile="Article_List4.aspx.cs" Inherits="Article_List4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="pager.css" type="text/css" />
    <div class="list ">
        <div class="row">
            <div class="col-lg-9 col-md-9 col-sm-12 col-lg-push-3 col-md-push-3 col-sm-push-0">

                <!----- 主体1111111----->
                <asp:Label ID="CategoryIDLabel" runat="server" Text="Label" Visible="false"></asp:Label>
                <div id="CurrentPosition">
                    当前位置：<a href="Index2.aspx">网站首页</a> >> 
                    <asp:HyperLink ID="CatHyperLink" runat="server"></asp:HyperLink>
                    >> 
                    <asp:HyperLink ID="SubHyperLink" runat="server"></asp:HyperLink>
                </div>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <!--- 作品类列表--->
                      <asp:Panel ID="Panel1" runat="server" Visible="false">
                        <br />
                          <asp:UpdatePanel ID="UpdatePanel2" runat="server"><ContentTemplate>
                        <div class="media-items row" id="MixItUpEFCCC8" >
                            <asp:Repeater ID="Repeater3" runat="server">
                                <ItemTemplate>
                                    <div class="col-lg-4 col-md-4 col-sm-4 mix category-photos" data-nameorder="1" data-dateorder="3" style="display: inline-block;" >
                                     <div class="media-item animate-onscroll " >
                                        <div class="media-image"><a href='Article_View2.aspx?ID=<%# Eval("ID") %>&c=<%# Eval("SubName") %>'>
                                            <img src='<%# Eval("CoverImageURL") %>' /></a>
                                        </div>
                                        <div class="media-info">
                                            <div class="media-header">
                                                <div class="media-caption">
                                                    <h4 class="post-title"><a href='Article_View.aspx?ID=<%# Eval("ID") %>&c=<%# Eval("SubName") %>'><%# Eval("Title") %></a></h4>
                                                    <p class="author">作者：<a href="#"><%# Eval("Author") %></a> &nbsp;&nbsp;日期：<%# String.Format("{0:yyyy-MM-dd}",Eval("CDT") ) %></p>
                                                </div>
                                            </div>
                                            <div class="media-description">
                                                <p><%# Eval("Summary") %></p>
                                            </div>
                                            <div class="media-button">
                                                <a href='Article_View.aspx?ID=<%# Eval("ID") %>&c=<%# Eval("SubName") %>' class="button big button-arrow">详细</a>
                                            </div>

                                        </div>
                                    </div>

                                    </div>

                                </ItemTemplate>
                            </asp:Repeater>

                            <!---分页--->
                            <div style="clear: both;">&nbsp;</div>
                           
                            <div class="row">
                            <div style="text-align: center;margin-top:20px;">
                                <webdiyer:AspNetPager CssClass="pages" CurrentPageButtonClass="cpb" AlwaysShow="true"
                                    ID="AspNetPager2" runat="server" FirstPageText="首页" LastPageText="尾页"
                                    NextPageText="下一页" PrevPageText="上一页" NumericButtonCount="3"
                                    OnPageChanged="AspNetPager2_PageChanged" LayoutType="Div">
                                </webdiyer:AspNetPager>

                            </div>
                            <div id="txt" style="display: none;">
                                每页显示：<asp:DropDownList ID="PageSizeDDL2" runat="server" AutoPostBack="true" Font-Bold="true"
                                    OnSelectedIndexChanged="PageSizeDDL2_SelectedIndexChanged" ForeColor="#5D7B9D" Width="70px">
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="12" Selected="True">12</asp:ListItem>
                                        <asp:ListItem Value="20">20</asp:ListItem>
                                        <asp:ListItem Value="50">50</asp:ListItem>
                                        <asp:ListItem Value="100">100</asp:ListItem>
                                        <asp:ListItem Value="200">200</asp:ListItem>
                                        <asp:ListItem Value="500">500</asp:ListItem>
                                </asp:DropDownList>
                                条记录，共<asp:Label ID="TotalPagesLabel2" runat="server" ForeColor="#5D7B9D" Font-Bold="true"></asp:Label>页
                                共<asp:Label ID="RecordCountLabe2" runat="server" ForeColor="#5D7B9D" Font-Bold="true"></asp:Label>页
                            </div>
                            </div>
                            <p>&nbsp;</p>
                        </div>
                          </ContentTemplate></asp:UpdatePanel>
                    </asp:Panel>
               




                <!--- 文章类列表--->
                <asp:Panel ID="Panel2" runat="server" Visible="false">
<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
                <div style="width: 100%;">
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <!-- Event -->

                            <div class="Sublist2" style="clear: both; width:100%;margin-top:5px;">
                                <div  id="ItemIndex" style="float: left; width:5%; text-align: left;">
                                    <p><%# Container.ItemIndex + 1%> </p>
                                </div>
                                <div  id="List-Title">
                                    <p><a href='Article_View2.aspx?ID=<%# Eval("ID") %>'><%# Eval("Title") %></a></p>

                                </div>
                                <div id="CDT" style="float: right; width: 30%; text-align:right ;">
                                    <p><%# String.Format("{0:yyyy-MM-dd}",Eval("CDT") ) %></p>
                                </div>
                                
                            </div>
                            <!-- /Event -->
                        </ItemTemplate>
                    </asp:Repeater>

                    <div class="row">
                    <div style="text-align: center;margin-top:80px;">
                        <webdiyer:AspNetPager CssClass="pages" CurrentPageButtonClass="cpb" AlwaysShow="true"
                            ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                            NextPageText="下一页" PrevPageText="上一页" NumericButtonCount="3"
                            OnPageChanged="AspNetPager1_PageChanged" LayoutType="Div">
                        </webdiyer:AspNetPager>

                    </div>
                    <div id="txt" style="display: none;">
                        每页显示：<asp:DropDownList ID="PageSizeDDL" runat="server" AutoPostBack="true" Font-Bold="true"
                            OnSelectedIndexChanged="PageSizeDDL_SelectedIndexChanged" ForeColor="#5D7B9D" Width="70px">
                            <asp:ListItem Value="2">2</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="35" Selected="True">35</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
                            <asp:ListItem Value="100">100</asp:ListItem>
                            <asp:ListItem Value="200">200</asp:ListItem>
                            <asp:ListItem Value="500">500</asp:ListItem>
                        </asp:DropDownList>
                        条记录，共<asp:Label ID="TotalPagesLabel" runat="server" ForeColor="#5D7B9D" Font-Bold="true"></asp:Label>页
                    </div>
                    </div>
                </div>
</ContentTemplate></asp:UpdatePanel>

</asp:Panel>

                  <!--- 花名册列表--->
                <asp:Panel ID="Panel3" runat="server" Visible="false">

                         <div style="width: 100%;">
                    <asp:Repeater ID="Repeater4" runat="server">
                        <ItemTemplate>
                            <!-- Event -->

                            <div class="Sublist2" style="clear: both; width:100%;margin-top:5px;">
                                <div  id="ItemIndex" style="float: left; width:5%; text-align: left;">
                                    <p><%# Container.ItemIndex + 1%> </p>
                                </div>
                                <div  id="List-Title">
                                    <p><a href='Article_View2.aspx?ID=<%# Eval("ArticleID") %>'><%# Eval("ClassName") %></a></p>

                                </div>                            
                            </div>
                            <!-- /Event -->
                        </ItemTemplate>
                    </asp:Repeater>
                              <div class="row">
                    <div style="text-align: center;margin-top:80px;">
                        <webdiyer:AspNetPager CssClass="pages" CurrentPageButtonClass="cpb" AlwaysShow="true"
                            ID="AspNetPager3" runat="server" FirstPageText="首页" LastPageText="尾页"
                            NextPageText="下一页" PrevPageText="上一页" NumericButtonCount="3"
                            OnPageChanged="AspNetPager3_PageChanged" LayoutType="Div">
                        </webdiyer:AspNetPager>

                    </div>
                    <div id="txt" style="display: none;">
                        每页显示：<asp:DropDownList ID="PageSizeDDL3" runat="server" AutoPostBack="true" Font-Bold="true"
                            OnSelectedIndexChanged="PageSizeDDL3_SelectedIndexChanged" ForeColor="#5D7B9D" Width="70px">
                            <asp:ListItem Value="2">2</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="35" Selected="True">35</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
                            <asp:ListItem Value="100">100</asp:ListItem>
                            <asp:ListItem Value="200">200</asp:ListItem>
                            <asp:ListItem Value="500">500</asp:ListItem>
                        </asp:DropDownList>
                        条记录，共<asp:Label ID="RecordCountLabel3" runat="server" ForeColor="#5D7B9D" Font-Bold="true"></asp:Label>页
                    </div>
                    </div>

</div>

      </asp:Panel>

            </div>


            <div class="col-lg-3 col-md-3 col-sm-4  col-lg-pull-9 col-md-pull-9 col-sm-pull-8 sidebar" style="background: white;" id="sidebar">
                <!----- 侧边栏22222---->

                <div style="padding-bottom: 20px; padding-top: 2px;">
                    <asp:Image ID="Image1" runat="server" Width="93%" Height="60" />
                </div>
                <div class="my-box">

                    <a class="banner" href="#" style="opacity: 1;">
                        <i class="icons icon-list icons-fadeout"></i><i class="icons icon-list"></i>
                        <h4>
                            <asp:Label ID="CategoryLabel" runat="server" Text="Label"></asp:Label>
                        </h4>
                        <br />
                    </a>
                    <div class="my-box-content">
                        <p><asp:Label ID="DescriptionLabel" runat="server" Text="Label"></asp:Label></p>

                        <asp:Repeater ID="Repeater2" runat="server">
                            <ItemTemplate>
                                <div>
                                    <p>
                                        <img src="images/irow_o_r.gif" />
                                        <a href='Article_List4.aspx?ID=<%# Eval("ID") %>'><%# Eval("SubName") %></a>
                                    </p>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                </div>




                <%--<div style="padding-bottom: 20px; padding-top: 20px; padding-left: 3px;">
                    <div style="width: 92%">
                        <img src="images/h/h1.jpg" height="60" />
                    </div>
                </div>

                <div class="my-box">
                    <a class="banner" href="#" style="opacity: 1;">
                        <i class="icons icon-calendar icons-fadeout"></i><i class="icons icon-calendar"></i>
                        <h4>荣誉榜</h4>
                        <br />
                    </a>

                    <p class="strong"><a href="#">教育技术学—广东省综合教学改革试点项目</a></p>
                    <p class="strong"><a href="#">数字传媒实验中心—广东省实验教学示范中心</a></p>
                    <p class="strong"><a href="#">广师视频—大学生影视制作实践基地</a></p>
                    <br />

                </div>--%>
                <div style="padding-bottom: 20px; padding-top: 20px; padding-left: 3px;">
                    <div style="width: 92%">
                        <img src="images/h/h3.jpg" height="60" />
                    </div>
                </div>
                <div class="my-box">
                    <a class="banner" href="#" style="opacity: 1;">
                        <i class="icons icon-link icons-fadeout"></i><i class="icons icon-link"></i>
                        <h4>友情链接</h4>
                        <br />
                    </a>

                    <div class="my-box-content">
                                <p><a href="http://www.gdin.edu.cn" target="_blank">1、广东技术师范学院</a></p>
                                <p><a href="http://setc.gdin.edu.cn:9000/" target="_blank">2、数字传媒实验中心</a></p>
                                <p><a href="http://202.192.72.100:88/cbxy/" target="_blank">3、综合测评管理系统</a></p>
                                <p><a href="http://www.edu.cn" target="_blank">4、中国教育与科研计算机网</a></p>
                                <p><a href="http://www.gdhed.org.cn" target="_blank">5、广东省教育厅</a></p>
                                <p><a href="http://www.moe.gov.cn" target="_blank">6、中华人民共和国教育部</a></p>
                    </div>

                </div>
                <div style="padding-bottom: 20px; padding-top: 20px; padding-left: 3px;">
                    <div style="width: 92%">
                        <img src="images/h/h5.jpg" height="60" />
                    </div>
                </div>
                <div class="my-box">
                    <a class="banner" href="#" style="opacity: 1;">
                        <i class="icons icon-phone icons-fadeout"></i><i class="icons icon-phone"></i>
                        <h4>联系我们</h4>
                        <br />
                    </a>

                    <div class="my-box-content">
                        <p class="strong">教育技术与传播学院</p>
                        <p class="strong">教育技术中心</p>
                        <p>地址：广州市天河区中山大道西293号实验楼505</p>
                        <p>邮编：510665</p>
                        <p>电话：020-38256633</p>
                        <p>邮箱：21646523@QQ.com</p>
                        <br />
                    </div>

                </div>

            </div>
        </div>
    </div>
</asp:Content>

