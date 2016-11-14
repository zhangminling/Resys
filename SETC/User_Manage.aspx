<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="User_Manage.aspx.cs" Inherits="User_Manage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <link href="assets/css/style.css" rel="stylesheet" />
        <script type="text/javascript">
            $(document).ready(function (e) {
                $(".login").click(function (e) {
                    $('.blanks').show();
                    $('.blanks').height($(document).height());
                    $(".module-area").slideDown(400);//fadeIn()


                });
                $(".module-close").click(function (e) {
                    $('.blanks').hide();
                    $(".module-area").slideUp(400);//fadeOut()
                });
            });

    </script>
     <style type="text/css">
        #margin {
            margin-top: 20px;
        }

            #margin th {
                text-align: center;
            }

        #CurrentPosition {
            margin-left: 20px;
        }
    </style>

      <asp:Label ID="UserIDs" runat="server" Text="" Visible="false"></asp:Label>
      <asp:Label ID="Label5" runat="server" Text="" Visible="false"></asp:Label>
      <asp:Label ID="Label6" runat="server" Text="" Visible="false"></asp:Label>
      <asp:Label ID="IsTrueUsers" runat="server" Text="" Visible="false"></asp:Label>

     <div class="blanks"></div>
                    <!-- 模态框弹出部分  --->

                    <div class="module-area modal-content">
                        <div class="module-head">
                            <span>标签查询</span>
                            <div class="module-close"></div>

                        </div>
                        <div id="BDCenter">
                            <div id="loginInput">
                                <asp:CheckBoxList ID="CheckBoxList1" runat="server" Style="margin-left: 5px; margin-top: 0px; text-align: left; width: 100%"  RepeatDirection="Horizontal"
                                    RepeatLayout="Table" RepeatColumns="5" >
                                </asp:CheckBoxList>
                                <p class="loginBnt">
                                    <asp:Button ID="TagBtn" runat="server" Text="点击查询" class="btn btn-info " OnClick="TagBtn_Click" />
                                </p>
                            </div>
                        </div>
                    </div>

       <div id="CurrentPosition">当前位置：<a href="User_Manage.aspx">用户管理</a></div>
      <div class="page-body" style="padding: 18px 20px 24px;">
                            <div class="row">
                                <div class="col-xs-12 col-md-12">
                                    <div class="widget">
                                        <div class="widget-header ">
                                            <span class="widget-caption">用户管理</span>
                                            <div class="widget-buttons">
                                                <a href="#" data-toggle="maximize">
                                                    <i class="fa fa-expand"></i>
                                                </a>
                                                <a href="#" data-toggle="collapse">
                                                    <i class="fa fa-minus"></i>
                                                </a>
                                                <a href="#" data-toggle="dispose">
                                                    <i class="fa fa-times"></i>
                                                </a>
                                            </div>
                                        </div>
                                        <div class="widget-body">
                                          
                                            <div class="form-group col-xs-6 col-md-6">
                                                <span class="input-icon">
                                                    <asp:TextBox ID="SearchTB" runat="server" placeholder="查询用户名,真实姓名，联系方式，邮箱" class="form-control input-sm"></asp:TextBox>
                                                    <i class="glyphicon glyphicon-search danger circular"></i>

                                                </span>

                                            </div>

                                            <asp:Button ID="SearchBtn" runat="server" Text="搜索" class="btn btn-info" OnClick="SearchBtn_Click" />&nbsp;&nbsp;
         <asp:DropDownList ID="OrderDDL" runat="server" OnSelectedIndexChanged="OrderDDL_SelectedIndexChanged"
             AutoPostBack="True">
             <asp:ListItem Value=" Order by ID Desc">ID从大到小</asp:ListItem>
             <asp:ListItem Value=" Order by ID Asc">ID从小到大</asp:ListItem>
         </asp:DropDownList>
                                            &nbsp;&nbsp;  &nbsp;&nbsp;
          
          <asp:DropDownList ID="IsValid" runat="server" AutoPostBack="True" OnSelectedIndexChanged="IsValid_SelectedIndexChanged">
          </asp:DropDownList>

                                            &nbsp;&nbsp;  &nbsp;&nbsp;                          
       
          <asp:DropDownList ID="RoleTypeDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RoleTypeDDL_SelectedIndexChanged">
          </asp:DropDownList>


                                            <div class=" col-xs-10 col-md-10">
                                                <asp:Label ID="Label4" class="btn btn-info login" runat="server" Text="标签查询"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
   <%-- <asp:Button ID="TagBtn" runat="server" Text="标签查询" class="btn btn-info "  onclick="TagBtn_Click" />&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                                <asp:Button ID="UpdateBtn" runat="server" Text="修改" class="btn btn-info" OnClick="UpdateBtn_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
     <asp:Button ID="IsTrue" runat="server" Text="启用" AutoPostBack="true" class="btn btn-success" OnClick="IsTrue_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
     <asp:Button ID="IsFalse" runat="server" Text="禁用" AutoPostBack="true" class="btn btn-warning" OnClick="IsFalse_Click" />&nbsp;&nbsp;&nbsp;&nbsp;              
     <asp:Button ID="DelBtn" runat="server" Text="删除" class="btn btn-darkorange active" OnClick="DelBtn_Click" />
                                            </div>

                                            <div style="margin-top: 60px;"></div>
                                            <div class=" col-xs-12 col-md-12">
                                                <asp:CheckBox ID="SelectAllCheckBox" runat="server" Text=" 全选 " AutoPostBack="true" OnCheckedChanged="SelectAllCheckBox_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;
        
   

                                    
                                     <div style="float: right;">
                                         总共：<asp:Label ID="Label1" runat="server" ForeColor="#5D7B9D" Font-Bold="true"></asp:Label>
                                         条记录，每页显示：
        <asp:DropDownList ID="PageSizeDDL" runat="server" AutoPostBack="true" Font-Bold="true" OnSelectedIndexChanged="PageSizeDDL_SelectedIndexChanged" ForeColor="#5D7B9D">
            <asp:ListItem Value="10" >10</asp:ListItem>
            <asp:ListItem Value="30" Selected="True">30</asp:ListItem>
            <asp:ListItem Value="25">50</asp:ListItem>
            <asp:ListItem Value="30">100</asp:ListItem>
        </asp:DropDownList>
                                         条记录， 共<asp:Label ID="Label2" runat="server" ForeColor="#5D7B9D" Font-Bold="true"></asp:Label>页。
        <asp:Label ID="TestLabel" runat="server" Text="" Visible="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                     </div>
                                            </div>
                                            <div id="margin" style="margin-top: 100px;">
                                                <asp:GridView ID="GridView1" runat="server" DataKeyNames="ID" AutoGenerateColumns="false" class="table table-striped table-bordered table-hover"
                                                    GridLines="Horizontal" Style="text-align: center;" ForeColor="#333333" HeaderStyle-HorizontalAlign="Center" Width="99%">
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="30px" HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="序">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="30px" HorizontalAlign="center" />
                                                            <HeaderStyle Width="30px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="ChechBox1" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="30px" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="UserName" HeaderText="用户名" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="80px"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TrueName" HeaderText="姓名" ItemStyle-Width="80">
                                                            <ItemStyle Width="80px"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-Width="90" Visible="true" />
                                                        <asp:BoundField DataField="TelePhone" HeaderText="联系方式" ItemStyle-Width="90" Visible="true" />
                                                        <asp:TemplateField HeaderText="头像">
                                                            <ItemTemplate>
                                                                <img src='<%# Eval("Avatar")%>' alt='<%# Eval("UserName")%>' width="80" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Status" HeaderText="个性签名" SortExpression="Status" />
                                                        <asp:HyperLinkField DataNavigateUrlFields="ID"
                                                            DataNavigateUrlFormatString="User_Edit2.aspx?ID={0}" DataTextField="RoleName"
                                                            HeaderText="角色" Target="_blank" ItemStyle-Width="90"></asp:HyperLinkField>
                                                        <asp:BoundField DataField="Valid" HeaderText="有效性" SortExpression="Valid" ItemStyle-Width="90" />
                                                    </Columns>
                                                </asp:GridView>
                                                <p>
                                                    &nbsp;&nbsp;
        <webdiyer:AspNetPager CssClass="pages" CurrentPageButtonClass="cpb" class="pagination" AlwaysShow="true"
            ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页"
            PrevPageText="上一页" OnPageChanged="AspNetPager1_PageChanged" PagingButtonSpacing="0">
        </webdiyer:AspNetPager>
                                                </p>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>
        <script type="text/javascript">
            function InitiateWidgets()
            { $('.widget-buttons *[data-toggle="maximize"]').on("click", function (n) { n.preventDefault(); var t = $(this).parents(".widget").eq(0), i = $(this).find("i").eq(0), r = "fa-compress", u = "fa-expand"; t.hasClass("maximized") ? (i && i.addClass(u).removeClass(r), t.removeClass("maximized"), t.find(".widget-body").css("height", "auto")) : (i && i.addClass(r).removeClass(u), t.addClass("maximized"), maximize(t)) }); $('.widget-buttons *[data-toggle="collapse"]').on("click", function (n) { n.preventDefault(); var t = $(this).parents(".widget").eq(0), r = t.find(".widget-body"), i = $(this).find("i"), u = "fa-plus", f = "fa-minus", e = 300; t.hasClass("collapsed") ? (i && i.addClass(f).removeClass(u), t.removeClass("collapsed"), r.slideUp(0, function () { r.slideDown(e) })) : (i && i.addClass(u).removeClass(f), r.slideUp(200, function () { t.addClass("collapsed") })) }); $('.widget-buttons *[data-toggle="dispose"]').on("click", function (n) { n.preventDefault(); var i = $(this), t = i.parents(".widget").eq(0); t.hide(300, function () { t.remove() }) }) }
            function maximize(n) { if (n) { var t = $(window).height(), i = n.find(".widget-header").height(); n.find(".widget-body").height(t - i) } } function scrollTo(n, t) { var i = n && n.size() > 0 ? n.offset().top : 0; jQuery("html,body").animate({ scrollTop: i + (t ? t : 0) }, "slow") }
            function hasClass(n, t) { var i = " " + n.className + " ", r = " " + t + " "; return i.indexOf(r) != -1 } var themeprimary = getThemeColorFromCss("themeprimary"), themesecondary = getThemeColorFromCss("themesecondary"), themethirdcolor = getThemeColorFromCss("themethirdcolor"), themefourthcolor = getThemeColorFromCss("themefourthcolor"), themefifthcolor = getThemeColorFromCss("themefifthcolor"), rtlchanger, popovers, hoverpopovers; $("#skin-changer li a").click(function () { createCookie("current-skin", $(this).attr("rel"), 10); window.location.reload() }); rtlchanger = document.getElementById("rtl-changer"); location.pathname != "/index-rtl-fa.html" && location.pathname != "/index-rtl-ar.html" && (readCookie("rtl-support") ? (switchClasses("pull-right", "pull-left"), switchClasses("databox-right", "databox-left"), switchClasses("item-right", "item-left"), $(".navbar-brand small img").attr("src", "assets/img/logo-rtl.png"), rtlchanger != null && (document.getElementById("rtl-changer").checked = !0)) : rtlchanger != null && (rtlchanger.checked = !1), rtlchanger != null && (rtlchanger.onchange = function () { this.checked ? createCookie("rtl-support", "true", 10) : eraseCookie("rtl-support"); setTimeout(function () { window.location.reload() }, 600) })); $(window).load(function () { setTimeout(function () { $(".loading-container").addClass("loading-inactive") }, 0) }); $("#btn-setting").on("click", function () { $(".navbar-account").toggleClass("setting-open") }); $("#fullscreen-toggler").on("click", function () { var n = document.documentElement; $("body").hasClass("full-screen") ? ($("body").removeClass("full-screen"), $("#fullscreen-toggler").removeClass("active"), document.exitFullscreen ? document.exitFullscreen() : document.mozCancelFullScreen ? document.mozCancelFullScreen() : document.webkitExitFullscreen && document.webkitExitFullscreen()) : ($("body").addClass("full-screen"), $("#fullscreen-toggler").addClass("active"), n.requestFullscreen ? n.requestFullscreen() : n.mozRequestFullScreen ? n.mozRequestFullScreen() : n.webkitRequestFullscreen ? n.webkitRequestFullscreen() : n.msRequestFullscreen && n.msRequestFullscreen()) }); popovers = $("[data-toggle=popover]"); $.each(popovers, function () { $(this).popover({ html: !0, template: '<div class="popover ' + $(this).data("class") + '"><div class="arrow"><\/div><h3 class="popover-title ' + $(this).data("titleclass") + '">Popover right<\/h3><div class="popover-content"><\/div><\/div>' }) }); hoverpopovers = $("[data-toggle=popover-hover]"); $.each(hoverpopovers, function () { $(this).popover({ html: !0, template: '<div class="popover ' + $(this).data("class") + '"><div class="arrow"><\/div><h3 class="popover-title ' + $(this).data("titleclass") + '">Popover right<\/h3><div class="popover-content"><\/div><\/div>', trigger: "hover" }) }); $("[data-toggle=tooltip]").tooltip({ html: !0 }); InitiateSettings(); InitiateWidgets();
      </script>
</asp:Content>

