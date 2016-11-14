<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/User.master"  CodeFile="Cat_Edit.aspx.cs" Inherits="Cat_Edit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="LabelUserID" runat="server" Visible="false" Text=""></asp:Label>
    <div id="CurrentPosition" >当前位置：<a href="Cat_Man.aspx">分类管理</a>>><a href="Cat_Edi.aspx">分类编辑</a></div>
    <p>&nbsp;</p>
    <div class="row">

         <div class="col-lg-6 col-sm-6 col-xs-12">
                            <div class="well with-header  with-footer">
                                <div class="header bordered-blue">一级分类修改</div>
                                     <div class="form-group">
    <div class="auto-style1">一级分类名称：</div>
            <div><asp:TextBox ID="CatName" runat="server" Width="370px" Height="29px"></asp:TextBox></div>
        
     
        <div class="auto-style1">描述：</div>
            <div><asp:TextBox ID="Description" runat="server" TextMode="MultiLine" Columns="50" Rows="5" CssClass="TextBoxMultiLine" MaxLength="140" Height="65px" Width="370px" ></asp:TextBox></div>
       
        
        <div class="auto-style1">有效性：  
                <asp:RadioButton ID="true1" runat="server" GroupName="Valid" Checked="true" Text="True" />
          <asp:RadioButton ID="false1" runat="server" GroupName="Valid" Text="False" /></div>

        <div class="auto-style1">显示状态： 
                <asp:RadioButton ID="false2" runat="server" GroupName="IsShow" Checked="true"  Text="文章列表" />   
               <asp:RadioButton ID="true2" runat="server" GroupName="IsShow" Text="作品列表" /></div>
                                         
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
   

