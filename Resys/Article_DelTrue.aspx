<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="Article_DelTrue.aspx.cs" Inherits="Article_DelTrue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="IDSLabel" runat="server" Text="" Visible="false"></asp:Label>
    <div id="CurrentPosition">
        当前位置：<a href="Article_Recycle.aspx">回收站</a> >> <a href="#">彻底删除文章</a>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <p>&nbsp;</p>
            <h4 style="color: red;">您确定要删除以下文章吗？一旦删除，将不可恢复！</h4>
            <p>&nbsp;</p>
            <div>
                <asp:Button ID="Button1" runat="server" Text="确定删除"
                    class="btn btn-danger" OnClick="Button1_Click" OnClientClick="return confirm('是否确定删除？')" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                <asp:Button ID="Button2" runat="server" Text="取消返回" class="btn btn-default" OnClick="Button2_Click" />
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
                                <span class="widget-caption">彻底删除文章</span>
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
                                                    DataNavigateUrlFormatString="Article_Preview.aspx?ID={0}" DataTextField="Title"
                                                    HeaderText="标题" ItemStyle-Width="350" ItemStyle-HorizontalAlign="Left" Target="_blank"></asp:HyperLinkField>
                                                <asp:BoundField DataField="Author" HeaderText="作者" ItemStyle-Width="60" />
                                                <asp:BoundField DataField="LDT" HeaderText="最后修改日期" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-Width="80" />
                                                <asp:BoundField DataField="ViewTimes" DataFormatString="{0}" HeaderText="浏览" ItemStyle-Width="30" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

