<%@ Page Title="" Language="C#" MasterPageFile="~/MasterFrontPage.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="page2" class="animated fadeInDown ">
    <div><asp:Label ID="ErrorLabel" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <div id="div1" style="font-size:12px;color:#999;"> 3-20位字符，可由中文、英文、数字及"_"组成</div>    
      <div id="div3" style="font-size:12px;color:red;">   *用户名一旦确定，不能更改</div>   
    <div><asp:TextBox ID="UserName" runat="server" placeholder="用户名" 
            MaxLength="30" CssClass="TextBox"></asp:TextBox></div>
    <div>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="用户名必填" ControlToValidate="UserName"></asp:RequiredFieldValidator>
      
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ErrorMessage="不符合用户名规则" ControlToValidate="UserName" 
            ValidationExpression="^[a-zA-Z0-9\u4e00-\u9fa5]{1,20}$"></asp:RegularExpressionValidator>
        </div>  
     <div id="div2" style="font-size:12px;color:#999;"> 6-20位字符，可由英文、数字及"_,&,#,!等"组成</div>   
    <div><asp:TextBox ID="Password" placeholder="密码" runat="server" MaxLength="30" TextMode="Password" CssClass="TextBox"></asp:TextBox></div>
    <div style="margin-top:-35px;"><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="密码必填" ControlToValidate="Password" style="margin-left:100px;"></asp:RequiredFieldValidator>
       <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="<p>1.密码应由数字和字母或特殊字符组成&nbsp;&nbsp;&nbsp;&nbsp; 2.字母个数不能小于2大于6</p><p>3.数字个数不能小于4大于14 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    4.密码长度应大于6小于20</p>" ControlToValidate="Password" 
            ValidationExpression="^[A-Za-z]{2,6}[0-9]{4,14}$|^[0-9]{4,14}[A-Za-z]{2,6}$|^[0-9]{5,20}[%&'@,;=?$\x22]{1,5}$|^[a-zA-Z]{4,20}[%&'@,;=?$\x22]{0,5}$|^[%&'@,;=?$\x22]{0,5}[a-zA-Z]{4,20}$"   ></asp:RegularExpressionValidator>
    </div>    
       <div id="div5" style="font-size:12px;color:#999;"> 请再次输入密码</div>     
    <div><asp:TextBox ID="Password2" placeholder="再次输入密码" runat="server" MaxLength="30" TextMode="Password" CssClass="TextBox"></asp:TextBox></div>    
    
    <div>
        <asp:CompareValidator ID="CompareValidator1" runat="server"  ControlToCompare="Password" ControlToValidate="Password2" Operator="Equal"
            ErrorMessage="两次输入的密码不一致"></asp:CompareValidator>
        </div> 
    <div id="div4" style="font-size:12px;color:#999;"> 请输入常用邮箱，建议使用QQ邮箱</div>  
    <div><asp:TextBox ID="Email" runat="server" placeholder="邮箱" 
            MaxLength="30" CssClass="TextBox"></asp:TextBox>
         <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
            ErrorMessage="不符合邮箱名规则" ControlToValidate="Email" 
            ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"></asp:RegularExpressionValidator>
    </div> 
 
   <p>
         <asp:CheckBox ID="CheckBox1" runat="server"   onclick="if(this.checked){ }else{alert('请选择同意协议后注册!') ;}" Checked="true"  style="margin-top:-10px;" />
         <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="#" style="float:left;">《Resys公司服务条款》</asp:HyperLink> 
     </p>
    <div style="right:auto;text-align: center;"> 
        <asp:Button ID="Button1" runat="server" Text=" 用户注册 " class="btn btn-info btn-rounded"
            onclick="Button1_Click"  OnClientClick="return confirm('一旦注册,登录名无法改变,是否确定注册？');" /> 
</div>
<p></p>
<div style="right:auto;text-align: center;">
    已经有账号  <a href="Login2.aspx">立即登录</a> 
</div>
</div>
<div class="changeblank"></div>
</asp:Content>


