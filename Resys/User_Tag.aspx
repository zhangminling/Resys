<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="User_Tag.aspx.cs" Inherits="User_Tag" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
#margin{
 margin-top:20px;
}
#margin th {
  text-align:center;
           }
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <asp:Label ID="LabelUserID" runat="server" Visible="false" Text=""></asp:Label>
       <div id="CurrentPosition">当前位置：<a href="#">用户标签</a> >> <a href="#">添加标签</a></div>
    <p></p>

    <p><span class="row-title before-sky"> <i class="fa fa-square-o sky"></i>标签</span></p>
    <div class="row">
     <div class="col-lg-6 col-sm-6 col-xs-12">
                                    <div class="well with-header">
                                        <div class="header bordered-darkpink">添加标签</div>
                                        <h6>标签名</h6>
                                        <asp:TextBox ID="TagName"  class="form-control" TextMode="MultiLine" Columns="20" Rows="1" placeholder="此字段不能为空" runat="server"></asp:TextBox>
                 
                                        <hr class="wide" />
                                        <h6>标签描述</h6>
                                        <asp:TextBox ID="TagDescription"  class="form-control" TextMode="MultiLine" Columns="40" Rows="4" placeholder="关于你的标签" runat="server"></asp:TextBox>
                                        <p>
                                       <%-- <asp:Panel ID="ClassifyPanel" runat="server" style="margin-left:-15px;">
                                          &nbsp; &nbsp; &nbsp;
                                           &nbsp; &nbsp; &nbsp;<asp:RadioButton ID="PublishDepartment" runat="server" GroupName="Valid" Text="发表部门" />   
             
                                        </asp:Panel>--%>
      
         

     <asp:Button ID="Button1" runat="server" class="btn btn-magenta" Text="新建标签" style="margin-left:5px;margin-top:2px;" OnClick="Button1_Click"/>
                                       </p>
     
                                         
                                    </div>
     </div>
        </div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>

    <div class="col-lg-11 col-sm-11 col-xs-17">
                                    <div class="well with-header">
                                         <div class="header bordered-blueberry">选择标签添加用户</div>
                                        <div class="row">
                                   
                                            
                                            <asp:CheckBoxList ID="TagsList" runat="server" style="margin-left:20px" RepeatDirection="Horizontal" 
                                              RepeatLayout="Table" RepeatColumns="7" Width="950px">
                                                    
                                                   </asp:CheckBoxList>
                                            <p></p>
                                     
                                            <asp:Button ID="Button3"  class="btn btn-sky shiny" style="margin-left:800px;margin-top:15px;" runat="server" Text="确认" OnClick="Button2_Click" />
                                            
                                         
                                    </div>
                                        </div>
                                </div>
             </ContentTemplate>
        </asp:UpdatePanel>
   
</asp:Content>

