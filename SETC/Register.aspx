<%@ Page Title="" Language="C#" MasterPageFile="~/MasterFrontPage.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="page2" class="animated fadeInDown ">
    <div><asp:Label ID="ErrorLabel" runat="server" ForeColor="Red" Text=""></asp:Label></div>
    <div id="div1" style="font-size:12px;color:#999;"> 3-20λ�ַ����������ġ�Ӣ�ġ����ּ�"_"���</div>    
      <div id="div3" style="font-size:12px;color:red;">   *�û���һ��ȷ�������ܸ���</div>   
    <div><asp:TextBox ID="UserName" runat="server" placeholder="�û���" 
            MaxLength="30" CssClass="TextBox"></asp:TextBox></div>
    <div>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="�û�������" ControlToValidate="UserName"></asp:RequiredFieldValidator>
      
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ErrorMessage="�������û�������" ControlToValidate="UserName" 
            ValidationExpression="^[a-zA-Z0-9\u4e00-\u9fa5]{1,20}$"></asp:RegularExpressionValidator>
        </div>  
     <div id="div2" style="font-size:12px;color:#999;"> 6-20λ�ַ�������Ӣ�ġ����ּ�"_,&,#,!��"���</div>   
    <div><asp:TextBox ID="Password" placeholder="����" runat="server" MaxLength="30" TextMode="Password" CssClass="TextBox"></asp:TextBox></div>
    <div style="margin-top:-35px;"><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="�������" ControlToValidate="Password" style="margin-left:100px;"></asp:RequiredFieldValidator>
       <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="<p>1.����Ӧ�����ֺ���ĸ�������ַ����&nbsp;&nbsp;&nbsp;&nbsp; 2.��ĸ��������С��2����6</p><p>3.���ָ�������С��4����14 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    4.���볤��Ӧ����6С��20</p>" ControlToValidate="Password" 
            ValidationExpression="^[A-Za-z]{2,6}[0-9]{4,14}$|^[0-9]{4,14}[A-Za-z]{2,6}$|^[0-9]{5,20}[%&'@,;=?$\x22]{1,5}$|^[a-zA-Z]{4,20}[%&'@,;=?$\x22]{0,5}$|^[%&'@,;=?$\x22]{0,5}[a-zA-Z]{4,20}$"   ></asp:RegularExpressionValidator>
    </div>    
       <div id="div5" style="font-size:12px;color:#999;"> ���ٴ���������</div>     
    <div><asp:TextBox ID="Password2" placeholder="�ٴ���������" runat="server" MaxLength="30" TextMode="Password" CssClass="TextBox"></asp:TextBox></div>    
    
    <div>
        <asp:CompareValidator ID="CompareValidator1" runat="server"  ControlToCompare="Password" ControlToValidate="Password2" Operator="Equal"
            ErrorMessage="������������벻һ��"></asp:CompareValidator>
        </div> 
    <div id="div4" style="font-size:12px;color:#999;"> �����볣�����䣬����ʹ��QQ����</div>  
    <div><asp:TextBox ID="Email" runat="server" placeholder="����" 
            MaxLength="30" CssClass="TextBox"></asp:TextBox>
         <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
            ErrorMessage="����������������" ControlToValidate="Email" 
            ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"></asp:RegularExpressionValidator>
    </div> 
 
   <p>
         <asp:CheckBox ID="CheckBox1" runat="server"   onclick="if(this.checked){ }else{alert('��ѡ��ͬ��Э���ע��!') ;}" Checked="true"  style="margin-top:-10px;" />
         <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="#" style="float:left;">��Resys��˾�������</asp:HyperLink> 
     </p>
    <div style="right:auto;text-align: center;"> 
        <asp:Button ID="Button1" runat="server" Text=" �û�ע�� " class="btn btn-info btn-rounded"
            onclick="Button1_Click"  OnClientClick="return confirm('һ��ע��,��¼���޷��ı�,�Ƿ�ȷ��ע�᣿');" /> 
</div>
<p></p>
<div style="right:auto;text-align: center;">
    �Ѿ����˺�  <a href="Login2.aspx">������¼</a> 
</div>
</div>
<div class="changeblank"></div>
</asp:Content>


