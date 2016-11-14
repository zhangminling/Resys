<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="Cat_Del.aspx.cs" Inherits="Cat_Del" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="IDSLabel" runat="server" Text="" Visible="false"></asp:Label>
    <div id="CurrentPosition">当前位置：<a href="Cat_Man.aspx">分类管理</a>>><a href="#">删除一级分类</a></div>
    <p>&nbsp;</p>
    <h3 style="color: red">您确定要删除此一级分类吗？一旦删除，它相对应的二级分类也会被删除哦！</h3>
    <p>&nbsp;</p>
    <div>
        <asp:Button ID="Button1" runat="server" Text="确定删除"
            class="btn btn-danger" OnClick="Button1_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
         <asp:Button ID="Button2"
             runat="server" Text="取消返回" class="btn btn-default" OnClick="Button2_Click"/>
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
                        <span class="widget-caption">删除一级分类</span>
                    </div>
                    <!--Widget Header-->
                    <div class="widget-body">
                        <div class="widget-main no-padding">
                            <asp:GridView ID="GridView1" runat="server" DataKeyNames="ID" AutoGenerateColumns="false" class="table table-striped table-bordered table-hover"
                                GridLines="Horizontal" Style="text-align: center;" ForeColor="#333333" HeaderStyle-HorizontalAlign="Center" Width="99%">
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="30px" HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="序" HeaderStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="30px" HorizontalAlign="center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CatName" HeaderText="栏目名称" ItemStyle-Width="60" />
                                    <asp:BoundField DataField="Description" HeaderText="描述" ItemStyle-Width="80" />
                                    <asp:BoundField DataField="Valid" HeaderText="有效性" ItemStyle-Width="30" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>



