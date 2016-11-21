<%@ Page Title="" Language="C#" MasterPageFile="~/MasterFrontPage.master" AutoEventWireup="true" CodeFile="Article_List3.aspx.cs" Inherits="Article_List3" %>

<%@ Register TagPrefix="UserControl" TagName="UC_Article_List3" Src="~/UC_Article_List3.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link rel="stylesheet" href="pager.css" type="text/css" />
     <link href="css2/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css2/fontello.css" rel="stylesheet" type="text/css" />
    <link href="css2/style.css" rel="stylesheet" type="text/css" />
     <script src="assets/js/jquery.cookie.js"></script>
    <script src="assets/js/jquery-2.0.3.min.js"></script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <script type="text/javascript">
     $(document).ready(function Order(e) {
            var cookieCount = {};
            cookieCount.count = function () {
                var count = parseInt(this.getCount('myCount'));
                $(".trigger").click(function (e) {
                    $('.Admix').show();
                    $('.trigger2').show();
                    $('.Order').hide();
                    $('.trigger').hide();
                    count++;
                    document.cookie = 'myCount=' + count + '';

                });



                $(".trigger2").click(function (e) {
                    $('.Order').show();
                    $('.trigger').show();
                    $('.Admix').hide();
                    $('.trigger2').hide();
                    count++;
                    document.cookie = 'myCount=' + count + '';


                });
                if (count % 2 == 0) {
                    $('.Order').show();
                    $('.trigger').show();
                    $('.Admix').hide();
                    $('.trigger2').hide();
                }
                else {
                    $('.Admix').show();
                    $('.trigger2').show();
                    $('.Order').hide();
                    $('.trigger').hide();

                }

            }
            cookieCount.setCount = function () {
                //首先得创建一个名为myCount的cookie
                var expireDate = new Date();
                expireDate.setDate(expireDate.getDate() + 1);
                document.cookie = 'myCount=' + '0' + ';expires=' + expireDate.toGMTString();
            }
            cookieCount.getCount = function (countName) {
                //获取名为计数cookie,为其加1
                var arrCookie = document.cookie.split('; ');
                var arrLength = arrCookie.length;
                var ini = true;
                for (var i = 0; i < arrLength; i++) {
                    if (countName == arrCookie[i].split('=')[0]) {
                        return parseInt(arrCookie[i].split('=')[1]);
                        break;
                    } else {
                        ini = false;
                    }
                }
                if (ini == false) this.setCount();
                return 0;
            }
            cookieCount.count();
        })
   </script>
   
         <div class="container wrapper">
	<div class="inner_content toleft">
              <div class="span7 pad8">
                  <div class="row">
                    <!----- 主体1111111----->
                    <asp:Label ID="CategoryIDLabel" runat="server" Text="Label" Visible="false"></asp:Label>
                    <div id="CurrentPosition">
                        当前位置：<a href="index.aspx">网站首页</a> >>
                        <asp:HyperLink ID="CategoryHyperLink" runat="server"></asp:HyperLink>
                    </div>
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <!-- 显示主栏目列表 -->
                    <asp:Panel ID="Panel1" runat="server">
                        <br />
                    <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">--%>
                            <%--<ContentTemplate>--%>
                                <div class="media-items row" id="MixItUpEFCCC8">
                                    <asp:Repeater ID="Repeater3" runat="server">
                                        <ItemTemplate>
                                            <div class="col-lg-4 col-md-4 col-sm-4 mix category-photos" data-nameorder="1" data-dateorder="3" style="display: inline-block;" >
                                                <div class="media-item animate-onscroll " >
                                                    <div class="media-image">
                                                        <a href='Article_View.aspx?ID=<%# Eval("ID") %>&c=<%# Eval("SubName") %>'>
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
                            <%--<div class="numeric-pagination">
                                <webdiyer:AspNetPager CssClass="pages" CurrentPageButtonClass="cpb" AlwaysShow="true"
                                    ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                                    NextPageText="下一页" PrevPageText="上一页" NumericButtonCount="3" 
                                    OnPageChanged="AspNetPager1_PageChanged" LayoutType="Div">
                                </webdiyer:AspNetPager>
                                共<asp:Label ID="TotalPagesLabel" runat="server" ForeColor="#5D7B9D" Font-Bold="true"></asp:Label>页
                                <p>&nbsp;</p>
                                <div id="PageDiv" runat="server" style="display: none;">
                                    总共：<asp:Label ID="RecordCountLabel" runat="server" ForeColor="#5D7B9D" Font-Bold="true"></asp:Label>
                                    条记录，每页显示：<asp:DropDownList ID="PageSizeDDL" runat="server" AutoPostBack="true" Font-Bold="true"
                                        OnSelectedIndexChanged="PageSizeDDL_SelectedIndexChanged" ForeColor="#5D7B9D">
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="9" Selected="True">9</asp:ListItem>
                                        <asp:ListItem Value="20">20</asp:ListItem>
                                        <asp:ListItem Value="50">50</asp:ListItem>
                                        <asp:ListItem Value="100">100</asp:ListItem>
                                        <asp:ListItem Value="200">200</asp:ListItem>
                                        <asp:ListItem Value="500">500</asp:ListItem>
                                    </asp:DropDownList>
                                    条记录，

                                </div>
                                <p>
                                    <asp:Label ID="TestLabel" runat="server" Text=""></asp:Label></p>
                            </div>--%>
                            <div class="row">
                            <div style="text-align: center;margin-top:20px;">
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
                                        <asp:ListItem Value="12" Selected="True">12</asp:ListItem>
                                        <asp:ListItem Value="20">20</asp:ListItem>
                                        <asp:ListItem Value="50">50</asp:ListItem>
                                        <asp:ListItem Value="100">100</asp:ListItem>
                                        <asp:ListItem Value="200">200</asp:ListItem>
                                        <asp:ListItem Value="500">500</asp:ListItem>
                                </asp:DropDownList>
                                条记录，共<asp:Label ID="TotalPagesLabel" runat="server" ForeColor="#5D7B9D" Font-Bold="true"></asp:Label>页
                                共<asp:Label ID="RecordCountLabel" runat="server" ForeColor="#5D7B9D" Font-Bold="true"></asp:Label>页
                            </div>
                            </div>
                            <p>&nbsp;</p>
                        </div>
    <%--</ContentTemplate></asp:UpdatePanel>--%>
                    </asp:Panel>

                    <!-- 显示子栏目列表 -->
                    
                    <asp:Panel ID="Panel2" runat="server">
                       <div >
                 
                     <div  class="trigger" title="不混排"><i class="icons icon-th-list"></i></div>
                      <div class="trigger2" title="混排"><i class="icons icon-menu"></i></div>

       <!--- 不混排----->
                      <div class="Order" style="position:relative;">
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <div style="margin-top:5px;">
                                <ul class="accordions">
                                    <li class="accordion accordion-active2">
                                        <div class="accordion-header2">
                                            <h3><a style="color:#FF5240;" href='Article_List4.aspx?ID=<%# Eval("ID") %>'><%# Eval("SubName") %></a></h3>
                                            <div style="float: right; height: 30px; line-height: 0px;"><a href='Article_List4.aspx?ID=<%# Eval("ID") %>'>更多 >> </a></div>
                                        </div>
                                        <div class="accordion-content2">
                                            <div class="row">
                                                <UserControl:UC_Article_List3 ID="UC_Article_List3" runat="server" SubID='<%# Eval("ID") %>' />
                                            </div>
                                                     
                                        </div>
                                    </li>
                                </ul>
                                </div>
                                <!-- <img src="images/random/H1.jpg" /> -->
                            </ItemTemplate>
                        </asp:Repeater>
                          </div>
           
            <!--- 混排--->

                              <div class="Admix">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
                                  <div class="Admix-head">
                                    <h3><asp:HyperLink ID="CategoryHyperLink1" runat="server" ForeColor="#FF5240"></asp:HyperLink></h3>
                                  </div>

                 <asp:Repeater ID="Repeater4" runat="server">
                     <ItemTemplate>
                          <div class="Sublist2" style="clear: both; width:100%;margin-top:10px;">
                            <div id="ItemIndex" style="float: left; width: 5%; text-align: left;">
                                <p><%# Container.ItemIndex + 1%> </p>
                                     </div>

                             <div id="List-Title" style="font-size:14px;">
                             <p> <a href='Article_List4.aspx?ID=<%# Eval("SubID") %>' >【<%# Eval("SubName") %>】 </a>----<a href='Article_View.aspx?ID=<%# Eval("ID") %>'><%# Eval("Title") %>  </a></p>
                               </div>

                             <div id="CDT" style="float: right; width: 30%; text-align: right;">
                                <p><%# String.Format("{0:yyyy-MM-dd}",Eval("CDT") ) %></p>
                                </div>
                           <%--</div>--%>                        
                                </div>

                     </ItemTemplate>
                 </asp:Repeater>
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
                                        <asp:ListItem Value="50" >50</asp:ListItem>
                                                                            
                                </asp:DropDownList>
                                条记录，共<asp:Label ID="TotalPagesLabel2" runat="server" ForeColor="#5D7B9D" Font-Bold="true"></asp:Label>页
                                共<asp:Label ID="RecordCountLabe2" runat="server" ForeColor="#5D7B9D" Font-Bold="true"></asp:Label>页
                            </div>
                            </div>

    </ContentTemplate></asp:UpdatePanel>

                              </div>

           </div>
                    </asp:Panel>


                </div>
            </div>


                    <div class="sidebar span3" style="background: white;" id="sidebar">
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




                <div style="padding-bottom: 20px; padding-top: 20px;padding-left:3px;">
                    <div style="width: 92%">
                        <img src="images/h/h1.jpg" height="60"/></div>
                </div>

                <div class="my-box">
                    <a class="banner" href="#" style="opacity: 1;">
                        <i class="icons icon-crown icons-fadeout"></i><i class="icons icon-crown"></i>
                        <h4>荣誉榜</h4>
                        <br />
                    </a>
                    <div class="my-box-content">
                        <p class="strong"><a href="#">● 教育技术学—广东省综合教学改革试点项目</a></p>
                        <p class="strong"><a href="#">● 数字传媒实验中心—广东省实验教学示范中心</a></p>
                        <p class="strong"><a href="#">● 广师视频—大学生影视制作实践基地</a></p>
                        <br />
                    </div>
                </div>
                <div style="padding-bottom: 20px; padding-top: 20px;">
                    <div style="width: 92%">
                        <img src="images/h/h3.jpg" height="60" /></div>
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
                <div style="padding-bottom: 20px; padding-top: 20px;">
                    <div style="width: 92%">
                        <img src="images/h/h5.jpg" height="60" /></div>
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
  <div class="changeblank"></div>  
</asp:Content>

