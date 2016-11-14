<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="Article_draft.aspx.cs" Inherits="Article_draft" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        #RightContent {
            margin-top: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="CurrentPosition">当前位置：<a href="Article_Man.aspx">文章模块</a> >> <a href="Article_draft.aspx">草稿箱</a> 
        <asp:Label ID="IDS" runat="server"  Visible="false"></asp:Label></div>
  
            <div class="page-body">
                <div class="row">
                    <div class="col-xs-12 col-md-12">
                        <div class="widget">
                            <div class="widget-header ">
                                <span class="widget-caption">草稿箱</span>
                            </div>
                            <div class="widget-body">
                                  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                                <div id="Man_Nav">
                                    <div class="form-group col-xs-3 col-md-3">
                                        <span class="input-icon">
                                            <asp:TextBox ID="SearchTB" runat="server" placeholder="查询文章" class="form-control input-sm"></asp:TextBox>
                                            <i class="glyphicon glyphicon-search danger circular"></i>
                                        </span>
                                    </div>
                                    <asp:Button ID="SearchBtn" runat="server" Text="搜索" class="btn btn-info"
                                        OnClick="SearchBtn_Click" />&nbsp;&nbsp;
                                    &nbsp;&nbsp;
                                    <asp:DropDownList ID="AuthorDDL" runat="server"
                                        OnSelectedIndexChanged="AuthorDDL_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;
                                    <asp:DropDownList ID="CatsDDL" runat="server"
                                        OnSelectedIndexChanged="CatsDDL_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;
                                    <asp:DropDownList ID="SubsDDL" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="SubsDDL_SelectedIndexChanged1">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;
                                    <asp:DropDownList ID="OrderDDL" runat="server"
                                        OnSelectedIndexChanged="OrderDDL_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Value=" Order by Orders Desc,ID Desc">默认排序</asp:ListItem>
                                        <asp:ListItem Value=" Order by LDT Desc">最后更新</asp:ListItem>
                                        <asp:ListItem Value=" Order by ID Asc">ID从小到大</asp:ListItem>
                                        <asp:ListItem Value=" Order by ID Desc">ID从大到小</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <br />
    </ContentTemplate></asp:UpdatePanel>
                                <div class=" col-xs-10 col-md-10">
                                    <asp:Button ID="UpdateBtn" runat="server" Text="修改" class="btn btn-info"
                                        OnClick="UpdateBtn_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                    <asp:Button ID="DelBtn" runat="server" Text="删除" class="btn btn-danger"
                                        OnClick="DelBtn_Click"  OnClientClick="return confirm('确定删除所选篇文章？')"/>&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server"><ContentTemplate>
                                <div style="margin-top: 60px;"></div>
                                <div class=" col-xs-12 col-md-12">
                                    <asp:CheckBox ID="SelectAllCheckBox" runat="server" Text=" 全选 " AutoPostBack="true" OnCheckedChanged="SelectAllCheckBox_CheckedChanged" />
                                    &nbsp;&nbsp;
                                    <div style="float: right;">
                                        总共：<asp:Label ID="Label1" runat="server" ForeColor="#5D7B9D" Font-Bold="true"></asp:Label>
                                        条记录，每页显示：<asp:DropDownList ID="PageSizeDDL" runat="server" AutoPostBack="true" Font-Bold="true"
                                            OnSelectedIndexChanged="PageSizeDDL_SelectedIndexChanged" ForeColor="#5D7B9D">
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="20" Selected="True">20</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            
                                        </asp:DropDownList>
                                        条记录，共<asp:Label ID="Label2" runat="server" ForeColor="#5D7B9D" Font-Bold="true"></asp:Label>页
                                    </div>
                                </div>
                                <br />
                                <div id="RightContent">
                                    <asp:GridView ID="GridView1" runat="server" DataKeyNames="ID" AutoGenerateColumns="false" class="table table-striped table-bordered table-hover"
                                        GridLines="Horizontal" Style="text-align: center;" ForeColor="#333333" HeaderStyle-HorizontalAlign="Center" Width="99%">
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="30px" HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="Orders" HeaderText="排序" ItemStyle-Width="30" Visible="false" />
                                            <asp:TemplateField HeaderText="序" HeaderStyle-HorizontalAlign="Center">
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
                                            <asp:HyperLinkField DataNavigateUrlFields="ID"
                                                DataNavigateUrlFormatString="Article_Preview.aspx?ID={0}" DataTextField="Title"
                                                HeaderText="标题" ItemStyle-Width="350" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Target="_blank"></asp:HyperLinkField>
                                            <asp:BoundField DataField="SubName" HeaderText="二级分类" ItemStyle-Width="60" />
                                            <asp:TemplateField HeaderText="排序" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="OrdersTB" runat="server" Text='<%# Eval("Orders") %>' Width="30" BorderWidth="1"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="30px" HorizontalAlign="center" />
                                                <HeaderStyle Width="30px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Author" HeaderText="作者" ItemStyle-Width="40" />
                                            <asp:BoundField DataField="Finished" HeaderText="完成" ItemStyle-Width="30" />
                                            <asp:BoundField DataField="StatusName" HeaderText="状态" ItemStyle-Width="80" />
                                            <asp:BoundField DataField="Valid" HeaderText="有效性" ItemStyle-Width="40" />
                                            <asp:BoundField DataField="LDT" HeaderText="日期" DataFormatString="{0:yy-MM-dd}" ItemStyle-Width="80" />
                                            <asp:BoundField DataField="ViewTimes" HeaderText="点击" ItemStyle-Width="30" />
                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                    <asp:Label ID="TestLabel" runat="server" Text="" Visible="true"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <webdiyer:AspNetPager CssClass="pages" class="pagination" CurrentPageButtonClass="cpb" AlwaysShow="true"
                                        ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                                        NextPageText="下一页" PrevPageText="上一页"
                                        OnPageChanged="AspNetPager1_PageChanged" LayoutType="Div">
                                    </webdiyer:AspNetPager>
                                </div>
                                        </ContentTemplate>
    </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    
</asp:Content>