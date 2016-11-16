<%@ Page Title="" Language="C#" MasterPageFile="~/MasterFrontPage.master" AutoEventWireup="true" CodeFile="Find_Password.aspx.cs" Inherits="Find_Password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <link rel="stylesheet" href="pager.css" type="text/css" />
     <link href="css2/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css2/fontello.css" rel="stylesheet" type="text/css" />
    <link href="css2/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="ActiveCode" runat="server" Text=""></asp:Label>
    <p></p>
      <div id="CurrentPosition">密码管理：<a href="#">找回密码</a> </div>
  <section class="section full-width-bg gray-bg animated fadeInDown">
  <div class="my-bg">
        <div class="row">
              
            <ul class="list finger-list">
		       <li style="margin-left:20%;">如果您的用户密码已经丢失，可通过密码找回页面找回。请您按照以下的步骤进行填写。
                   <p>首先输入您的用户名称，单击下一步按钮进行下一步操作！</p>

		       </li>
			</ul>
             <p></p>
             <asp:Panel ID="PanelInputName" runat="server"  style="margin-left:40%;" >		
                <p style="color:red;font-size:16px;font-weight:bold;">1/请输入需要找回密码的用户名</p>
              <br />
             <span> 会员名：<asp:TextBox ID="txtName" runat="server" Width="155px" Height="30px"></asp:TextBox></span>
            <p style="margin-left:5%;">
            <asp:Button ID="btnNext" runat="server"  Text="下一步" OnClick="btnNext_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Button ID="btnNreturn" runat="server" PostBackUrl="~/Index2.aspx" Text="返　回"  />
            </p>
         <div style="margin-left:55px;"><asp:Label ID="ErrorLabel" runat="server" ForeColor="Red" Text=""></asp:Label></div>
         </asp:Panel>

         <asp:Panel ID="WayToFind" runat="server" Visible="false"  style="margin-left:40%;margin-top:40px;" >
           <div class="col-lg-4 col-md-4 col-sm-12">
        <asp:DropDownList ID="WayToFindPsd" runat="server" Height="40px" Width="250px" OnSelectedIndexChanged="WayToFindPsd_SelectedIndexChanged" AutoPostBack="True" >
      <asp:ListItem >请选择找回密码的方式</asp:ListItem>
     <asp:ListItem >通过邮件找回密码</asp:ListItem>
    <asp:ListItem > 验证密保找回密码</asp:ListItem>    
      </asp:DropDownList>
          <p style="margin-left:10%;">
              <asp:Button ID="WayToFindReturn" runat="server" Text="返　回"  OnClick="WayToFindReturn_Click"  />
            </p>
        </div>

         </asp:Panel>


         <asp:Panel ID="EmailPanel" runat="server" Visible="false"  style="margin-left:40%;">
              <p style="color:red;font-size:16px;font-weight:bold;margin-left:30px;">2/请确认您的邮箱</p>
              <br />
             <span> 邮箱号：<asp:TextBox ID="Email" runat="server" Width="155px" Height="30px"></asp:TextBox></span>
             <asp:Label ID="Label5" runat="server" Width="155px" Text=""></asp:Label>
            <p style="margin-left:4.8%;">
            <asp:Button ID="EmailToFind" runat="server"  Text="下一步" OnClick="EmailToFind_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Button ID="Button7" runat="server" Text="返　回"  OnClick="EmailToReturn_Click"  />
            </p>
         <div style="margin-left:55px;"><asp:Label ID="EmailError" runat="server" ForeColor="Red" Text=""></asp:Label></div>
         </asp:Panel>


            <asp:Panel ID="QuestionPanel1" runat="server"  Visible="false" style="margin-left:40%;">
                 <p style="color:red;font-size:16px;font-weight:bold;">2/请输入需要密码提示问题&nbsp;1&nbsp;的答案</p>
                <p><span>问题：<asp:TextBox ID="txtQuestion1" runat="server" Width="157px" ReadOnly="True"></asp:TextBox></span></p>
                <p><span>答案:<asp:TextBox ID="txtAnswer1" runat="server" Width="157px"></asp:TextBox></span></p>
                  <div style="margin-left:80px;"><asp:Label ID="Label3" runat="server" ForeColor="Red" Text=""></asp:Label></div>
                    <p style="margin-left:5%;">
                    <asp:Button ID="Button2" runat="server" Text="返回" OnClick="Button2_Click"  />&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Button ID="Button3" runat="server" Text="下一题"  OnClick="Button3_Click" />
                           </p>
            </asp:Panel>


                <asp:Panel ID="QuestionPanel2" runat="server"  Visible="false" style="margin-left:40%;">
                 <p style="color:red;font-size:16px;font-weight:bold;">2/请输入需要密码提示问题&nbsp;2&nbsp;的答案</p>
                <p><span>问题：<asp:TextBox ID="txtQuestion2" runat="server" Width="157px" ReadOnly="True"></asp:TextBox></span></p>
                <p><span>答案:<asp:TextBox ID="txtAnswer2" runat="server" Width="157px"></asp:TextBox></span></p>
                  <div style="margin-left:80px;"><asp:Label ID="Label4" runat="server" ForeColor="Red" Text=""></asp:Label></div>
                    <p style="margin-left:5%;">
                    <asp:Button ID="Button4" runat="server" Text="上一题" OnClick="Button4_Click"   />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button5" runat="server" Text="下一题" OnClick="Button5_Click"  />
                  </p>
              </asp:Panel>

            
            <asp:Panel ID="PanelGetPass" runat="server"  Visible="false" style="margin-left:40%;">
                 <p style="color:red;font-size:16px;font-weight:bold;">2/请输入需要密码提示问题&nbsp;3&nbsp;的答案</p>
                <p><span>问题：<asp:TextBox ID="txtQuestion3" runat="server" Width="157px" ReadOnly="True"></asp:TextBox></span></p>
                <p><span>答案:<asp:TextBox ID="txtAnswer3" runat="server" Width="157px"></asp:TextBox></span></p>
                  <div style="margin-left:80px;"><asp:Label ID="Label1" runat="server" ForeColor="Red" Text=""></asp:Label></div>
                    <p style="margin-left:5%;">
                     <asp:Button ID="btnReturn" runat="server" Text="上一题" OnClick="btnReturn_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnGet" runat="server" Text="查　找" OnClick="btnGet_Click"  />
                  
                           </p>
            </asp:Panel>
            
            <asp:Panel ID="ResetPassword" runat="server" Visible="false" style="margin-left:40%;">
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

                 
            </div>
                       </div>
								
					
    </section>

 <div class="changeblank1"></div>  
							
</asp:Content>

