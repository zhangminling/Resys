<%@ Page Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="Sub_Add.aspx.cs" Inherits="Sub_Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 167px;
        }
    </style> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Label1" runat="server" Text="" Visible="true"></asp:Label>
    <div id="CurrentPosition" >当前位置：<a href="Cat_Man.aspx">分类管理</a>>><a href="Sub_Add.aspx">新增二级分类</a></div>
    <p>&nbsp;</p>
    <div class="row">

         <div class="col-lg-6 col-sm-6 col-xs-12">
                            <div class="well with-header  with-footer">
                                <div class="header bordered-blue">新增二级分类</div>
                                     <div class="form-group">
    <div >
    <table   border="0" cellspacing="0" cellpadding="0">
        <tr><td ><p>&nbsp;</p></td></tr>
       
        <tr><td class="auto-style1">二级分类名称：</td>
            <td><asp:TextBox ID="SubName" runat="server" Width="300px" Height="29px" ></asp:TextBox></td>
        </tr>
        <tr><td>&nbsp;</td>
        <td align="left" colspan="2"></td>    
        </tr>
     
        <tr><td class="auto-style1">描述：</td>
            <td><asp:TextBox ID="Description" runat="server" TextMode="MultiLine" Columns="50" Rows="5" CssClass="TextBoxMultiLine" MaxLength="140" Height="65px" Width="300px" ></asp:TextBox></td>
        </tr>
        <tr><td>&nbsp;</td>
        <td align="left" colspan="2"></td>    
        </tr>
        <tr><td class="auto-style1">有效性：</td>
            <td >
               <asp:RadioButton ID="true1" runat="server" GroupName="Valid" Checked="true" Text="True" />
          <asp:RadioButton ID="false1" runat="server" GroupName="Valid" Text="False" />   
            </td>
         </tr>
        <tr><td>&nbsp;</td>
        <td align="left" colspan="2"></td>    
        </tr>
       
        <tr><td>&nbsp;</td>
        <td align="left" colspan="2"></td>    
        </tr>
     
        <tr>
            <td class="auto-style1">所属分类</td>
            <td><span><asp:DropDownList runat="server" ID="Cats" Height="39px" Width="300px">  
           </asp:DropDownList></span></td>
        </tr>
        <tr><td>&nbsp;</td>
        <td align="left" colspan="2"></td>    
        </tr>
      
        <tr align="right">
            <td class="auto-style1"><asp:Button ID="SubSave" runat="server" Text="保存" Height="35px" Width="97px" class="btn btn-info" OnClick="ButtonSave_Click"/></td>
            <td align="center"><asp:Button ID="Button1" runat="server" Text="取消" Height="35px" Width="97px" class="btn btn-default" OnClick="Button1_Click"/></td>
        </tr>
  
        <!--<td align="left">
            <asp:RadioButtonList  runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" CellPadding="10">
            <asp:ListItem Value="1" Selected="True">正常</asp:ListItem>
            <asp:ListItem Value="0">锁定</asp:ListItem>
            </asp:RadioButtonList>-->
        </table>
        </div>
                                                                                                                                  </div>
                </div>
            </div>
        </div>
</asp:Content>

