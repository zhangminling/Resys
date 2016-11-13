<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="User_History.aspx.cs" Inherits="User_Center" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <style type="text/css">
        #margin {
            margin-top: 20px;
        }

            #margin th {
                text-align: center;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
    <div class="row">
       
         <div class="col-lg-11 col-sm-11 col-xs-12">
            <div class="well with-header  with-footer">
                <div class="header bordered-blue">操作历史：</div>

                <div style="float: right;">
                                         总共：<asp:Label ID="Label1" runat="server" ForeColor="#5D7B9D" Font-Bold="true"></asp:Label>
                                         条记录，每页显示：
        <asp:DropDownList ID="PageSizeDDL" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="PageSizeDDL_SelectedIndexChanged" Font-Bold="true"  ForeColor="#5D7B9D">
            <asp:ListItem Value="2">2</asp:ListItem>
            <asp:ListItem Value="5">5</asp:ListItem>
            <asp:ListItem Value="10" Selected="True">10</asp:ListItem>
            <asp:ListItem Value="15">15</asp:ListItem>
            <asp:ListItem Value="20">20</asp:ListItem>
            <asp:ListItem Value="25">25</asp:ListItem>
            <asp:ListItem Value="30">30</asp:ListItem>
        </asp:DropDownList>
                                         条记录， 共<asp:Label ID="Label2" runat="server" ForeColor="#5D7B9D" Font-Bold="true"></asp:Label>页。
        <asp:Label ID="TestLabel" runat="server" Text="" Visible="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                     </div>
    <div style="margin-bottom:40px;"></div>
                <div id="margin">
                                <asp:GridView ID="GridView1" runat="server" DataKeyNames="ID"  AutoGenerateColumns="false"  class="table table-striped table-bordered table-hover" 
        GridLines="Horizontal"  style="text-align:center;" ForeColor="#333333" HeaderStyle-HorizontalAlign="Center" Width="99%"  >
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
                                        <asp:BoundField DataField="UserName" HeaderText="操作用户" ItemStyle-Width="50" Visible="true" />
                                         <asp:BoundField DataField="VisitPage" HeaderText="访问页面" ItemStyle-Width="50" Visible="true" />
                                        <asp:BoundField DataField="Util" HeaderText="操作内容" ItemStyle-Width="150">
                                            <ItemStyle Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="VisitDatetime" HeaderText="访问时间" ItemStyle-Width="100" Visible="true" />
                                       <asp:BoundField DataField="UserIP" HeaderText="用户IP" ItemStyle-Width="100" Visible="true" />

                                    </Columns>
                                </asp:GridView>

                           <p>
                                                    &nbsp;&nbsp;
        <webdiyer:AspNetPager CssClass="pages" CurrentPageButtonClass="cpb" class="pagination" AlwaysShow="true"
            ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页"
            PrevPageText="上一页" OnPageChanged="AspNetPager1_PageChanged"  PagingButtonSpacing="0">
        </webdiyer:AspNetPager>
                                                </p>
                            </div>

                </div>
             </div>
  
         </div>
</asp:Content>

