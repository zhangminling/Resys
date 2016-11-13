<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/User.master"  CodeFile="Sub_Edit.aspx.cs" Inherits="Sub_Edit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"><!--OnSelectedIndexChanged="Unnamed2_SelectedIndexChanged"--> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="LabelCatsID" runat="server" Visible="false" Text=""></asp:Label>
    <asp:Label ID="IDLabel" runat="server" Text="" Visible="false"></asp:Label>
   
    <div id="CurrentPosition" >当前位置：<a href="Cat_Man.aspx">分类管理</a>>><a href="Cat_Edi.aspx">分类编辑</a></div>
    <p>&nbsp;</p>
    
    <div class="row">

         <div class="col-lg-6 col-sm-6 col-xs-12">
                            <div class="well with-header  with-footer">
                                <div class="header bordered-blue">二级分类修改</div>
                                     <div class="form-group">
        <div class="auto-style1">二级分类名称：</div>
            <div><asp:TextBox ID="SubName" runat="server" Width="370px" Height="29px"></asp:TextBox></div>
            <div class="auto-style1">描述：</div>
            <div><asp:TextBox ID="Description" runat="server" TextMode="MultiLine" Columns="50" Rows="5" CssClass="TextBoxMultiLine" MaxLength="140" Height="65px" Width="370px" ></asp:TextBox></div>
            <div style="margin-top:10px;"></div>
                                         <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
           
            <div class="auto-style1">所属分类
            <span>
          <asp:DropDownList ID="CatName" runat="server" Height="30" AutoPostBack="True" >
                                     </asp:DropDownList>
    
       </span></div>
                                             </ContentTemplate></asp:UpdatePanel>


        <div style="margin-top:10px;"></div>
        <div class="auto-style1">有效性： 
                <asp:RadioButton ID="true1" runat="server" GroupName="Valid" Checked="true" Text="True" />
          <asp:RadioButton ID="false1" runat="server" GroupName="Valid" Text="False" /></div>
                                      
             
                                         <p>&nbsp;</p>
            <div><asp:Button ID="SubSave" runat="server" Text="保存" Height="35px" Width="97px" class="btn btn-info" OnClick="ButtonSave_Click" />
            <asp:Button ID="Button1" runat="server" Text="取消" Height="35px" Width="97px" class="btn btn-default" OnClick="Button4_Click"/>
                <asp:Label ID="ErrorLabel" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
            </div>
                                                           </div>
                </div>
            </div>
        </div>
</asp:Content>
   


