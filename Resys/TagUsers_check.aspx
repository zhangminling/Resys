<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="TagUsers_check.aspx.cs" Inherits="TagUsers_check" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <style type="text/css">
#margin th {
  text-align:center;
           }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="IDSLabel" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="count" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="TagName" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="Ids" runat="server" Text="" Visible="false"></asp:Label>
<div class="col-lg-11 col-sm-15 col-xs-15">

                            <div class="row">
                                <div class="col-lg-15">
                                    <div class="widget">
                                         <div class="widget-header bordered-bottom bordered-themesecondary">
                                            <i class="widget-icon fa fa-tags themesecondary"></i>
                                            <span class="widget-caption themesecondary">查看标签用户</span>
                                        </div><!--Widget Header-->
                                        <div class="widget-main no-padding">
                                                <div class="tickets-container">
                                              <asp:Button ID="Button1" runat="server" Text="确定删除"
            class="btn btn-danger" OnClick="Button1_Click" />
   <p>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="ResultLabel" runat="server" Font-Bold="True" ForeColor="#00CC00"></asp:Label>
    </p>
                                                       <div id="margin">    
                                                    <ul class="tickets-list">
 <asp:GridView ID="GridView1" runat="server" DataKeyNames="ID" AutoGenerateColumns="false" class="table table-striped table-bordered table-hover"
                                        GridLines="Horizontal" Style="text-align: center;" ForeColor="#333333" HeaderStyle-HorizontalAlign="Center">
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
                                            <ItemStyle Width="20px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                            <asp:BoundField DataField="UserName" HeaderText="用户名" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="80px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TagName" HeaderText="标签名" ItemStyle-Width="80">
                                                <ItemStyle Width="80px"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                                        
                                                        </ul>
         </div>
                                                    </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
          </div>

</asp:Content>

