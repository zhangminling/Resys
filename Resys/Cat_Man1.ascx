<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Cat_Man1.ascx.cs" Inherits="Cat_Man1" %>
<style type="text/css">
    .margin {
        padding-right: 5px;
    }
</style>
<asp:GridView ID="GridView1" DataKeyNames="ID" runat="server" AutoGenerateColumns="false" Width="99%" HeaderStyle-Height="24px" class="table table-striped table-bordered table-hover"
    GridLines="Horizontal" Style="text-align: center;" ForeColor="#333333" HeaderStyle-HorizontalAlign="Center">
    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="30px" HorizontalAlign="Center" />
    <HeaderStyle HorizontalAlign="Center" />
    <Columns>
        <asp:TemplateField HeaderText="序" HeaderStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="60px" HorizontalAlign="center" />
        </asp:TemplateField>
        <asp:HyperLinkField DataNavigateUrlFields="ID,SubName,Valid"
            DataNavigateUrlFormatString="Sub_Edit.aspx?ID={0}&T={1}" DataTextField="SubName"
            HeaderText="二级分类" ItemStyle-HorizontalAlign="Left" ControlStyle-Height="20px"></asp:HyperLinkField>
        <asp:BoundField DataField="Valid" HeaderText="有效性" SortExpression="Valid" ItemStyle-Width="90" />
        <asp:HyperLinkField DataNavigateUrlFields="ID,SubName"
            DataNavigateUrlFormatString="Sub_Edit.aspx?ID={0}&T={1}"
            ItemStyle-HorizontalAlign="Center" Text="编辑" ItemStyle-Width="90" ControlStyle-CssClass="btn btn-info" ControlStyle-Height="30px" ItemStyle-CssClass="margin"></asp:HyperLinkField>
        <asp:HyperLinkField DataNavigateUrlFields="ID,SubName" DataNavigateUrlFormatString="Sub_Del.aspx?ID={0}&T={1}"
            ItemStyle-HorizontalAlign="Center" Text="删除" ItemStyle-Width="90" ControlStyle-CssClass="btn btn-danger" ControlStyle-Height="30px"></asp:HyperLinkField>
    </Columns>
</asp:GridView>
