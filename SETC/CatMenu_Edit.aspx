<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/User.master" CodeFile="CatMenu_Edit.aspx.cs" Inherits="CatMenu_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="LabelUserID" runat="server" Visible="false" Text=""></asp:Label>
    <div id="CurrentPosition" >当前位置：<a href="Cat_Man.aspx">菜单管理</a>>><a href="Cat_Edi.aspx">菜单编辑</a></div>
    <p>&nbsp;</p>
    <div class="row">

         <div class="col-lg-6 col-sm-6 col-xs-12">
                            <div class="well with-header  with-footer">
                                <div class="header bordered-blue">一级菜单修改</div>
                                     <div class="form-group">
    <div class="auto-style1">一级菜单名称：</div>
            <div><asp:TextBox ID="CatMenuName" runat="server" Width="370px" Height="29px"></asp:TextBox></div>
        
     <div class="auto-style1">链接（URL）：</div>
            <div><asp:TextBox ID="Href" runat="server" Width="370px" Height="29px"></asp:TextBox></div>
     <div class="auto-style1">排序（数字越小越前）：：</div>
            <div><asp:TextBox ID="Orders" runat="server" Width="370px" Height="29px"></asp:TextBox></div>
        
        <div class="auto-style1">有效性：  
                <asp:RadioButton ID="true1" runat="server" GroupName="Valid" Checked="true" Text="True" />
          <asp:RadioButton ID="false1" runat="server" GroupName="Valid" Text="False" /></div>
           
        
                                         
                                         <p>&nbsp;</p>
            <div><asp:Button ID="CatSave" runat="server" Text="保存" Height="35px" Width="97px" class="btn btn-info" OnClick="ButtonSave_Click" />
            <asp:Button ID="Button1" runat="server" Text="取消" Height="35px" Width="97px" class="btn btn-default" OnClick="Button4_Click"/>
                <asp:Label ID="ErrorLabel" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
            </div>
                      </div>
                </div>
            </div>
        </div>
</asp:Content>