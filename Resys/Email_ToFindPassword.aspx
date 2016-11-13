<%@ Page Title="" Language="C#" MasterPageFile="~/MasterFrontPage.master" AutoEventWireup="true" CodeFile="Email_ToFindPassword.aspx.cs" Inherits="Email_ToFindPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:Panel ID="ResetPassword" runat="server" Visible="true" style="margin-left:40%;">
                  <p class="text-info" style="font-size:20px;padding:10px 130px;">修改密码</p>
                <div class="registerbox-textbox">
                <asp:TextBox ID="Password2" class="form-control"  placeholder="输入新密码" runat="server" TextMode="Password"
                MaxLength="30"  Width="400px"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="密码必填" ControlToValidate="Password2"></asp:RequiredFieldValidator>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
            ErrorMessage="不符合密码规则" ControlToValidate="Password2" 
            ValidationExpression="^[0-9a-zA-Z]{2,20}$"></asp:RegularExpressionValidator>
            </div>
                <div class="registerbox-textbox">
                 <asp:TextBox ID="Password3" placeholder="确认新密码" runat="server" TextMode="Password"
                MaxLength="30" class="form-control" Width="400px"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="密码必填" ControlToValidate="Password3"></asp:RequiredFieldValidator>
     <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
            ErrorMessage="不符合密码规则" ControlToValidate="Password3" 
            ValidationExpression="^[0-9a-zA-Z]{2,20}$"></asp:RegularExpressionValidator>
        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="两次输入密码不一致" ControlToCompare="Password2" ControlToValidate="Password3" Width="100%"></asp:CompareValidator>    
           </div>

            <div class="registerbox-submit" style="margin-left:10%;margin-top:-20px;">
               <asp:Label ID="Label2" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
               <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true"
                Text="显示密码"   oncheckedchanged="CheckBox1_CheckedChanged"  Visible="true" /> &nbsp;&nbsp; &nbsp;  
           <asp:Button ID="Button1" runat="server" class="btn btn-info" Text=" 确定修改" OnClick="Button1_Click" style="margin-top:-5px;"/>
            </div>

            </asp:Panel>
    <p></p>
</asp:Content>

