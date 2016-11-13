<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="Focus_Man.aspx.cs" Inherits="Focus_Man" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="CurrentPosition">µ±«∞Œª÷√£∫<a href="#">Ωπµ„Õºπ‹¿Ì</a></div>
    <div class="page-body">
        <div class="row">
            <div class="col-xs-12 col-md-12">
                <div class="widget">
                    <div class="widget-header ">
                        <span class="widget-caption">Ωπµ„Õºπ‹¿Ì</span>
                    </div>
                    <div class="widget-body">
                        <div style="margin-top: 20px;"></div>
                        <div>
                            <asp:Button ID="OrdersBtn" runat="server" Text="∏¸∏ƒ≈≈–Ú" class="btn btn-success"
                                OnClick="OrdersBtn_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="AddBtn" runat="server" Text="…œ¥´Ωπµ„Õº" class="btn btn-info" OnClick="AddBtn_Click" />
                        </div>
                        <div>
                            <p>&nbsp;</p>
                            <asp:Label ID="ID_Label" runat="server" Visible="false" Text="" />
                            <asp:GridView ID="GridView1" runat="server" DataKeyNames="ID" AutoGenerateColumns="False" HeaderStyle-Height="24px" class="table table-striped table-bordered table-hover"
                                GridLines="Horizontal" Style="text-align: center;" ForeColor="#333333" HeaderStyle-HorizontalAlign="Center" Width="99%"
                                OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                OnRowCommand="GridView1_RowCommand">
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="30px" HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:BoundField DataField="Orders" HeaderText="≈≈–Ú" ItemStyle-Width="30"
                                        Visible="false">
                                        <ItemStyle Width="30px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="–Ú" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="30px" HorizontalAlign="center" />
                                        <HeaderStyle Width="30px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ωπµ„Õº" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <img src='<%# Eval("PhotoSrc") %>' alt='<%# Eval("LinkURL") %>' width="500" height="150" />
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="≈≈–Ú" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="OrdersTB" runat="server" Text='<%# Eval("Orders") %>' Width="30" BorderWidth="1"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="60px" HorizontalAlign="center" />
                                        <HeaderStyle Width="60px" />
                                    </asp:TemplateField>

                                    <asp:CheckBoxField DataField="Valid" HeaderText="œ‘ æ" />
                                    <asp:ButtonField ButtonType="Button" CommandName="Edit2" HeaderText="±‡º≠" ShowHeader="True" Text="±‡º≠" ControlStyle-CssClass="btn btn-info" />

                                    <asp:ButtonField ButtonType="Button" CommandName="Del2" HeaderText="…æ≥˝"
                                        ShowHeader="True" Text="…æ≥˝" ControlStyle-CssClass="btn btn-danger" />
                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <p>&nbsp;</p>
</asp:Content>

