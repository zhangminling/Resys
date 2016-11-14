<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="ArticleTag_Del.aspx.cs" Inherits="ArticleTag_Del" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="IDSLabel" runat="server" Text="" Visible="false"></asp:Label>
    <div id="CurrentPosition">
        当前位置：<a href="ArticleTag_Man.aspx">标签管理</a> >> <a href="#">删除标签</a>
    </div>
    <p>&nbsp;</p>
    <h4 style="color: red">您确定要删除以下文章标签吗？一旦删除，将不可恢复！</h4>
    <p>&nbsp;</p>
    <div>
        <asp:Button ID="Button1" runat="server" Text="确定删除" 
            class="btn btn-danger" onclick="Button1_Click"  OnClientClick="return confirm('确认是否删除该标签？')"/>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:Button ID="Button2" runat="server" Text="取消返回" class="btn btn-default" onclick="Button2_Click" />
    </div>
    <p>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="ResultLabel" runat="server" Font-Bold="True" ForeColor="#00CC00"></asp:Label>
    </p>
    <div class="col-lg-11 col-sm-15 col-xs-15">
        <div class="row">
            <div class="col-lg-15">
                <div class="widget">
                    <div class="widget-header ">
                        <span class="widget-caption">删除标签</span>
                    </div>
                    <!--Widget Header-->
                    <div class="widget-body">
                        <div class="widget-main no-padding">
                            <div id="RightContent">
                                <asp:GridView ID="GridView1" DataKeyNames="ID" runat="server" AutoGenerateColumns="False" HeaderStyle-Height="24px" class="table table-striped table-bordered table-hover"
                                    GridLines="Horizontal" Style="text-align: center;" ForeColor="#333333" HeaderStyle-HorizontalAlign="Center" Width="99%">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="30px" HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="序" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" HorizontalAlign="center" />
                                        </asp:TemplateField>
                                        <asp:HyperLinkField DataNavigateUrlFields="ID"
                                            DataNavigateUrlFormatString="ArticleTag_Up.aspx?ID={0}" DataTextField="TagName"
                                            HeaderText="文章标签名" ItemStyle-Width="70" ItemStyle-HorizontalAlign="Left" Target="_blank"></asp:HyperLinkField>
                                        <asp:BoundField DataField="Description" HeaderText="标签描述" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="Articles" HeaderText="文章数" ItemStyle-Width="60" />
                                        <asp:BoundField DataField="UserName" HeaderText="创建人" ItemStyle-Width="80" />
                                    </Columns>
                                </asp:GridView>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

