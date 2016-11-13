<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="TagUsers_Add.aspx.cs" Inherits="TagUsers_Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="IDSLabel" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="Label3" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="count" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="UserIDs" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="Label6" runat="server" Text="" Visible="false"></asp:Label>

    <h5 class="row-title"><i class="typcn typcn-tag"></i>添加群组用户</h5>

    <div class="row">

        <div class="col-lg-11 col-sm-11 col-xs-17">
            <div class="well with-header">
                <div class="header bordered-sky">你的标签</div>
                <asp:CheckBoxList ID="TagsList" runat="server" Style="margin-left: 20px" RepeatDirection="Horizontal"
                    RepeatLayout="Table" RepeatColumns="7" Width="950px">
                </asp:CheckBoxList>

            </div>
        </div>
    </div>

    <div class="row">

        <div class="widget-body">

            <div class="form-group col-xs-8 col-md-8">
                <span class="input-icon">
                    <asp:TextBox ID="SearchTB" runat="server" placeholder="查询用户名" class="form-control input-sm"></asp:TextBox>
                    <i class="glyphicon glyphicon-search danger circular"></i>

                </span>


            </div>

            <asp:Button ID="SearchBtn" runat="server" Text="搜索" class="btn btn-info" OnClick="SearchBtn_Click" />&nbsp;&nbsp;

        <asp:DropDownList ID="OrderDDL" runat="server" OnSelectedIndexChanged="OrderDDL_SelectedIndexChanged"
            AutoPostBack="True">
            <asp:ListItem Value=" Order by ID Desc">ID从大到小</asp:ListItem>
            <asp:ListItem Value=" Order by ID Asc">ID从小到大</asp:ListItem>
        </asp:DropDownList>



            <asp:DropDownList ID="RoleTypeDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RoleTypeDDL_SelectedIndexChanged">
            </asp:DropDownList>

            &nbsp;&nbsp;&nbsp;&nbsp;
        
            
       <div class=" col-xs-10 col-md-10">
           <asp:Button ID="UpdateBtn" runat="server" Text="修改" class="btn btn-info" OnClick="UpdateBtn_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="DelBtn" runat="server" Text="删除" class="btn btn-danger" OnClick="DelBtn_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="AddBtn" runat="server" Text="确认添加" class="btn btn-success" OnClick="AddBtn_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
       </div>




            <div style="margin-top: 50px;"></div>

            <div style="margin-top: 50px;"></div>

            <div class=" col-xs-12 col-md-12">
                <asp:CheckBox ID="SelectAllCheckBox" runat="server" Text=" 全选 " AutoPostBack="true" OnCheckedChanged="SelectAllCheckBox_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;                            
                <div style="float: right;">
                    总共：<asp:Label ID="Label1" runat="server" ForeColor="#5D7B9D" Font-Bold="true"></asp:Label>
                    条记录，每页显示：
                <asp:DropDownList ID="PageSizeDDL" runat="server" AutoPostBack="true" Font-Bold="true" ForeColor="#5D7B9D" OnSelectedIndexChanged="PageSizeDDL_SelectedIndexChanged">
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="5">5</asp:ListItem>
                    <asp:ListItem Value="10" Selected="True">10</asp:ListItem>
                    <asp:ListItem Value="15">15</asp:ListItem>
                    <asp:ListItem Value="20">20</asp:ListItem>
                    <asp:ListItem Value="25">25</asp:ListItem>
                    <asp:ListItem Value="30">30</asp:ListItem>
                </asp:DropDownList>
                    条记录， 共<asp:Label ID="Label2" runat="server" ForeColor="#5D7B9D" Font-Bold="true"></asp:Label>页
                <asp:Label ID="TestLabel" runat="server" Text="" Visible="true"></asp:Label>
                </div>
            </div>
                <br />
                <div style="margin-top: 30px;"></div>

                <div id="margin">
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
                            <asp:HyperLinkField DataNavigateUrlFields="ID"
                                DataNavigateUrlFormatString="User_Edit2.aspx?ID={0}" DataTextField="RoleName"
                                HeaderText="角色" Target="_blank" ItemStyle-Width="90"></asp:HyperLinkField>
                            <asp:BoundField DataField="Valid" HeaderText="有效" SortExpression="Valid" ItemStyle-Width="90" />
                        </Columns>
                    </asp:GridView>


                    <p>
                        &nbsp;&nbsp;
        <webdiyer:AspNetPager CssClass="pages" CurrentPageButtonClass="cpb" class="pagination" AlwaysShow="true"
            ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页"
            PrevPageText="上一页" OnPageChanged="AspNetPager1_PageChanged" PagingButtonSpacing="0">
        </webdiyer:AspNetPager>
                        <p>
                        </p>
                    </p>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

