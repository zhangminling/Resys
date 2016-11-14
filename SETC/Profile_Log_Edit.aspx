<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="Profile_Log_Edit.aspx.cs" ValidateRequest="false" Inherits="Profile_Log_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!-- Make sure the path to CKEditor is correct. -->
    <script type="text/javascript" src="ckeditor4/ckeditor.js"></script>
    <%--插入资源用的js代码--%>
    <script type="text/javascript">
        //editor.addCommand('insertTimestamp', {
        //    exec: function showMyDialog(e) {
        //        //var str = 'width=980,height=200,left=' + ((screen.width - 900) / 2) + ',top=' + ((screen. height - 650) / 2) + ',scrollbars=yes,scrolling=no,location=no,toolbar=no，titlebar=no,status=no,menubar=no';
        //        var str = 'width=80%,height=460,' + ',top=' + ((screen.height - 50) / 2) + ',scrollbars=yes,scrolling=no,location=no,toolbar=no，titlebar=no,status=no,menubar=no';
        //    }
        //});
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="CurrentPosition">当前位置：<a href="Cat_Man.aspx">发表日志</a></div>
    <p>&nbsp;</p>
    <div class="col-md-12" style="background-color:#ffffff">
        <div class="widget">
            <div class="widget-header ">
                <span class="widget-caption">发表日志</span>
            </div>
        </div>
        <asp:TextBox ID="Editor1" runat="server" TextMode="MultiLine" />
        <script type="text/javascript">
            var editor = CKEDITOR.replace('<%= Editor1.ClientID %>', { height: "230px" });
        </script>
        <p><asp:Label ID="LabelUserName" runat="server" Text="" Visible="false"></asp:Label><asp:Label ID="LabelID" runat="server" Text="" Visible="false"></asp:Label><asp:Label ID="LabelPhotoSRC" runat="server" Text="" Visible="false"></asp:Label></p>
        <asp:Button ID="BtnOK" runat="server" Text="发表日志"  CssClass="btn btn-success" OnClick="Button1_Click"/>
        <p>&nbsp;</p>
        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
            <ItemTemplate>
                <div class="col-md-12" >
                    <p><hr style="border:1 ;dashed #987cb9"; width="100%"; color=#987cb9 ;SIZE=4"/></p>
                    <div style="margin-right:15px; float:left">
                        <asp:Image ID="ImageAvatar" runat="server" Height="60px" Width="60px" />
                        <p><span style=" font-size:18px; color:#1277c1"><%#Eval("UserName") %></span></p>
                    </div>                   
                    <div class="col-md-9">
                        <p style="float:right;z-index:100;">
                            <asp:LinkButton ID="LinkButton1" runat="server" Text="X" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID") %>' CommandName="Delete" OnClientClick="return confirm('确定要删除？')" Font-Size="Medium"></asp:LinkButton></p>
                        <div style="width:90%;">
                            <asp:Label ID="LabelContent" runat="server" Text='<%#Bind("LogContent") %>'></asp:Label>
                            <p><span style="color:#808080"><%# String.Format("{0:yyyy年MM月dd日 HH时}",Eval("CDT") ) %></span></p>
                        </div>                                     
                        <%--<p style="float:right;">style="float:left;"
                            <asp:Button ID="Delete" runat="server" Text="删除" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID") %>'  CssClass="btn btn-danger" CommandName="Delete" OnClientClick="return confirm('确定要删除？')"/></p>--%>                        
                    </div>
                </div>
                
            </ItemTemplate>
        </asp:Repeater>
    </div>

</asp:Content>

