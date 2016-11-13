<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="Tag_Del.aspx.cs" Inherits="Tag_Delete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="IDSLabel" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="Label1" runat="server" Text="" Visible="true"></asp:Label>
    <asp:Label ID="Label2" runat="server" Text="" Visible="true"></asp:Label>

    <div id="CurrentPosition">
        当前位置：<a href="Tag_Man.aspx">标签管理</a> >> <a href="#">删除标签</a>
    </div>
    <p>&nbsp;</p>
    <h3 style="color: red">
        您确定要删除以下标签吗？一旦删除，将不可恢复！
    </h3>
    <p>&nbsp;</p>
    <div>
        <asp:Button ID="Button1" runat="server" Text="确定删除"
            class="btn btn-danger" OnClick="Button1_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:Button ID="Button2"
            runat="server" Text="取消返回" class="btn btn-default" OnClick="Button2_Click" />
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
                                                <ItemStyle Width="50px" HorizontalAlign="center"/>
                                            </asp:TemplateField>

                                            <asp:HyperLinkField DataNavigateUrlFields="ID"
                                                DataNavigateUrlFormatString="Tag_Edit.aspx?ID={0}" DataTextField="TagName"
                                                HeaderText="标签名" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" Target="_blank"></asp:HyperLinkField>
                                            <asp:BoundField DataField="Description" HeaderText="标签描述" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="#999999" />
                                            <asp:BoundField DataField="Users" HeaderText="用户数" ItemStyle-Width="110" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="#999999" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

