<%@ Page Title="" Language="C#" MasterPageFile="~/MasterFrontPage.master" AutoEventWireup="true" CodeFile="Article_Preview.aspx.cs" Inherits="Article_Preview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <link rel="stylesheet" href="pager.css" type="text/css" />
    <div class="list2 ">
    <div class="row">
       
     
        <div class="col-lg-9 col-md-9 col-sm-12 col-lg-push-3 col-md-push-3 col-sm-push-0">
            <div class="row">
                <!----- 主体1111111-----> 

                <asp:Label ID="ArticleID" runat="server" Text="Label" Visible="false"></asp:Label>
                <asp:Label ID="ArticleRandomID" runat="server" Text="Label" Visible="false"></asp:Label>
               <asp:Label ID="AbsoluteUrl" runat="server"  Visible="false"></asp:Label>
                <div id="CurrentPosition">
                    当前位置：<a href="Index2.aspx">网站首页</a> >>
                    <asp:HyperLink ID="CategoryHyperLink" runat="server"></asp:HyperLink>
                    <asp:Label ID="SubLabel" runat="server" Text=""></asp:Label>
                    <asp:HyperLink ID="SubHyperLink" runat="server"></asp:HyperLink>
                    <p></p>
              
                  
                      </div>
            
                <p>&nbsp;</p>
             
   <div id="myPrintArea" >
   
                <div class="ArticleTitle" style="color: #0097B3;">
                         <asp:Label ID="ArticleTitle" runat="server" Text="Label" Font-Bold="true" Font-Size="16"></asp:Label>
                </div>
                <br />
                <div class="ArticleTitle" style="color: #999;"> 
                    作者：<asp:Label ID="TagName" runat="server" Text="Label"></asp:Label>
                   <asp:Label ID="Author" runat="server" Text="Label"></asp:Label>&nbsp;丨&nbsp;
                    日期：<asp:Label ID="CDT" runat="server" Text="Label"></asp:Label>&nbsp;丨&nbsp;
                    浏览次数：<asp:Label ID="ViewTimes" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;<asp:Label ID="ReviewTimes" runat="server" Text="Label" Visible="false"></asp:Label>
                    <input id="Abtn2" type="button" value="A-" />
                    <input id="Abtn3" type="button" value="A" />
                    <input id="Abtn1" type="button" value="A+" />
                    <span id="biuuu_button" ><i class="icons icon-print-2" title="打印文本"></i></span>
               <br />
                   
                     </div>
      
                <div class="Articledashed"></div>

                <br />
              
                <div class="summary">
                 <asp:Panel ID="Panel1" runat="server" >
                     <%--margin-left:5%;--%>
                   <div class="summary2">
                        <p>简介：<asp:Label ID="Summary" runat="server" Text="Label" ></asp:Label></p>
                    </div>   
                     <br />
                     <%--<h4 style="float:left; margin: 10px;" > 简介：</h4>--%>
                </asp:Panel>
               </div>
                <div id="p1" class="view-content">
                    
                    <asp:Label ID="Content" runat="server" Text="Label" CssClass="font"></asp:Label>

                </div>
                    <p style="display:none;right;margin-top:10px;">
                     <asp:Image ID="ImgCode" runat="server"  Width="80px"/>
                    </p>

    </div><%--附件--%>
                  <asp:Repeater ID="Repeater3" runat="server">
                      <ItemTemplate>
                        <p> 附件<%# Container.ItemIndex + 1%>： <a href='<%# Eval("FileURL") %>'> <%# Eval("FileName") %></a></p> 
                      </ItemTemplate>
                  </asp:Repeater>
                <br />
               <p class="ATag">  <asp:Label ID="Tag" runat="server" Text="文章标签：" Visible="false" Font-Size="16px" Font-Bold="true"></asp:Label></p>
                <p class="ATag">
                       <asp:Repeater ID="Repeater1" runat="server"> 
                        <ItemTemplate>
                            <a href='Article_ListbyTag.aspx?ID=<%# Eval("ArticleTagID") %>' > <%# Eval("ArticleTagName") %> </a>&nbsp; 
                        </ItemTemplate>
                       </asp:Repeater>
                </p>
                <!-- JiaThis Button BEGIN -->
                <div class="jiathis_style" id="jiathis">
                    <%--<span class="jiathis_txt">分享到：</span>--%>
                    <a class="jiathis_button_tools_1"></a>
                    <a class="jiathis_button_tools_2"></a>
                    <a class="jiathis_button_tools_3 jiathis_my"></a>
                    <a class="jiathis_button_tools_4 jiathis_my "></a>
                    <%--<a href="http://www.jiathis.com/share" class="jiathis jiathis_txt jiathis_separator jtico jtico_jiathis" target="_blank">更多</a>--%>
                    <a class="jiathis_counter_style jiathis_my"></a>
                </div>

                <%--<script type="text/javascript" src="http://v3.jiathis.com/code/jia.js" charset="utf-8"></script>--%>
                <!-- JiaThis Button END -->

                <!-- JW Player Library -->
    <script type="text/javascript" src="jwplayer/jwplayer.js"></script>
    <!-- JW HTML Config Library -->
    <script type="text/javascript" src="jwplayer/jwplayer-html-config.min.js"></script>

                <br />
         
               



                    <%--相关文章--%>
                <div style="margin-top:0px;">
               <asp:Panel ID="Panel2" runat="server">
                    <div class="RelatedArticles">
                    <h4>相关文章</h4>       
                    </div>
                    <div class="RelatedArticles2"></div>
                  <asp:Repeater ID="rpt_message" runat="server">
                     <ItemTemplate>
                          <div class="Sublist2">
              <%--<div style="border-bottom:1px dashed #eee;">--%>
                            <div id="ItemIndex" style="float: left; width: 5%; ">
                                <p><%# Container.ItemIndex + 1%> </p>
                            </div>

                            <div id="List-Title">
                                <p><a href='Article_View2.aspx?ID=<%# Eval("ID") %>'><%# Eval("Title") %>  </a></p>
                            </div>
                              
                            <div id="CDT" style="float: right; width: 30%; text-align: center;">
                                <p><%# String.Format("{0:yyyy-MM-dd}",Eval("AuditedDateTime") ) %></p>
                            </div>
                           <%--</div>--%>
     
                                               
 
                                       
                                    
                                </div>

                     </ItemTemplate>
                 </asp:Repeater>
                   </asp:Panel>
                    </div>
                <!----- /主体----->
            </div>
        </div>


        <div id="sidebar" class="col-lg-3 col-md-3 col-sm-4  col-lg-pull-9 col-md-pull-9 col-sm-pull-8 sidebar" style="background: white;">
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



            <div style="padding-bottom: 20px; padding-top: 20px; padding-left: 3px;">
                <div style="width: 92%">
                    <img src="images/h/h1.jpg" height="60"></div>
            </div>

            <div class="my-box">
                <a class="banner" href="#" style="opacity: 1;">
                    <i class="icons icon-link icons-fadeout"></i><i class="icons icon-link"></i>
                    <h4>友情链接</h4>
                    <br />
                </a>

                <div class="my-box-content">
                            <p style="width: 100%;"><a href="http://www.gdin.edu.cn" target="_blank">1、广东技术师范学院</a></p>
                            <p><a href="http://setc.gdin.edu.cn:9000/" target="_blank">2、数字传媒实验中心</a></p>
                            <p><a href="http://202.192.72.100:88/cbxy/" target="_blank">3、综合测评管理系统</a></p>
                            <p><a href="http://www.edu.cn" target="_blank">4、中国教育与科研计算机网</a></p>
                            <p><a href="http://www.gdhed.org.cn" target="_blank">5、广东省教育厅</a></p>
                            <p><a href="http://www.moe.gov.cn" target="_blank">6、中华人民共和国教育部</a></p>
                </div>

            </div>

            <div style="padding-bottom: 20px; padding-top: 20px; padding-left: 3px;">
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

</asp:Content>

