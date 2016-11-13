<%@ Page Title="" Language="C#" MasterPageFile="~/MasterFrontPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
         <script  type="text/javascript">
    function reloadcode()
    {
      document.getElementById('safecode').src='CreatePic.aspx?'+Math.random();
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:Label ID="U_Label" runat="server" Visible="false" Text="" />
    <asp:Label ID="Label1" runat="server" Visible="false" Text="" />

    <div id="page" class="animated fadeInDown ">
        <div><asp:Label ID="ErrorLabel" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label></div>
        
        <div id="div1" style="font-size:12px;color:#999;"> 3-20位字符，可由中文、英文、数字及"_"组成</div>
        <div style="left:10px;">
            <asp:TextBox ID="UserName" placeholder="请输入用户名" runat="server" MaxLength="30" CssClass="TextBox"></asp:TextBox>
        </div>
        <div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="用户名必填" ControlToValidate="UserName"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ErrorMessage="不符合用户名规则" ControlToValidate="UserName" 
            ValidationExpression="^[a-zA-Z0-9\u4e00-\u9fa5]{2,20}$"></asp:RegularExpressionValidator>
        </div>
        
        <div id="div2" style="font-size:12px;color:#999;"> 3-20位字符，可由英文、数字及"_"组成</div>
        <div><asp:TextBox ID="Password" placeholder="请输入密码" runat="server" MaxLength="30" TextMode="Password" CssClass="TextBox"></asp:TextBox></div>
        <div><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="密码必填" ControlToValidate="Password"></asp:RequiredFieldValidator>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
             ErrorMessage="不符合密码规则" ControlToValidate="Password" 
             ValidationExpression="^[a-zA-Z0-9]{2,20}$"></asp:RegularExpressionValidator>
        </div>
      
      
      <%--  <div style=" float:left;width:120px; "><asp:TextBox ID="ValidateCode" runat="server" placeholder="验证码" 
            MaxLength="30" CssClass="TextBox"></asp:TextBox></div>
        <div style="float:right;"><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="validateCode.aspx" />
        &nbsp; &nbsp; <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false">换一张</asp:LinkButton></div>--%>
        
    
      <div style=" float:left;width:120px; "><asp:TextBox ID="ValidateCode" runat="server" placeholder="验证码" 
            MaxLength="30" CssClass="TextBox"></asp:TextBox></div>
          <div style="float:right;">                 
         <a href="javascript:reloadcode();" title="更换一张">
                         <img src="CreatePic.aspx" id="safecode"  alt="更换一张验证"  width="80"/>
                    换一张？</a>
            </div>
                           
                    
     

        <div style="clear:both;text-align: center;margin-top:50px;"><asp:Button ID="Button1" runat="server" Text=" 用户登录 " class="btn btn-info btn-rounded" onclick="Button1_Click" />
        </div>
        <p>&nbsp;</p>  
        <div style="right:auto;text-align: center;">
           <a href="Register2.aspx">注册账号</a>&nbsp;丨 &nbsp;<a href="Find_Password.aspx">找回密码</a> 
        </div>
    </div>
  <div class="changeblank"></div>              
</asp:Content>

