<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="User_Password.aspx.cs" Inherits="User_Password" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">           
<div id="CurrentPosition">当前位置：<a href="#">用户管理</a> >> <a href="User_Password.aspx">修改密码</a></div>
           <asp:UpdatePanel ID="UpdatePanel2" runat="server">
         <ContentTemplate>              
          <div class="register-container animated fadeInDown">
        <div class="registerbox bg-white">
            <p class="text-info" style="font-size:20px;padding:10px 130px;">修改密码</p>
            <div class="registerbox-textbox">
                 <asp:TextBox ID="Password1" class="form-control"  placeholder="请输入旧密码"  runat="server" TextMode="Password"
                MaxLength="30" ></asp:TextBox>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="密码必填" ControlToValidate="Password1"></asp:RequiredFieldValidator>
     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ErrorMessage="不符合密码规则" ControlToValidate="Password1" 
            ValidationExpression="^[0-9a-zA-Z]{2,20}$"></asp:RegularExpressionValidator>
         
            </div>
            <div class="registerbox-textbox">
                <asp:TextBox ID="Password2" class="form-control"  placeholder="输入新密码" runat="server" TextMode="Password"
                MaxLength="30" ></asp:TextBox>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="密码必填" ControlToValidate="Password2"></asp:RequiredFieldValidator>
     <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
            ErrorMessage="不符合密码规则" ControlToValidate="Password2" 
            ValidationExpression="^[0-9a-zA-Z]{2,20}$"></asp:RegularExpressionValidator>
            </div>
            <div class="registerbox-textbox">
                 <asp:TextBox ID="Password3" placeholder="确认新密码" runat="server" TextMode="Password"
                MaxLength="30" class="form-control"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="密码必填" ControlToValidate="Password3"></asp:RequiredFieldValidator>
     <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
            ErrorMessage="不符合密码规则" ControlToValidate="Password3" 
            ValidationExpression="^[0-9a-zA-Z]{2,20}$"></asp:RegularExpressionValidator>
        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="两次输入密码不一致" ControlToCompare="Password2" ControlToValidate="Password3" Width="100%"></asp:CompareValidator>    
           </div>
          <div class="registerbox-submit">
               <asp:Label ID="ErrorLabel" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
          
               <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true"
                oncheckedchanged="CheckBox1_CheckedChanged" Text="显示密码" Visible="true" />  
            
               <asp:Button ID="Button1" runat="server" class="btn btn-info pull-right" Text=" 确定修改 " 
                onclick="Button1_Click" />
            </div>
        </div>
    </div>
     </ContentTemplate>
                   </asp:UpdatePanel>
</asp:Content>

