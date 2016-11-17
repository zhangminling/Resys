<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="Article_Add.aspx.cs" Inherits="Article_Add" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <link href="assets/css/style.css" rel="stylesheet" />
 <script type="text/javascript">
     $(document).ready(function (e) {
         $(".login2").click(function (e) {
             $('.blanks2').show();
             $('.blanks2').height($(document).height());
             $(".module-area2").slideDown(400);//fadeIn()


         });
         $(".module-close2").click(function (e) {
             $('.blanks2').hide();
             $(".module-area2").slideUp(400);//fadeOut()
         });

         $(".Sure2").click(function (e) {
             $('.blanks2').hide();
             $(".module-area2").slideUp(400);//fadeOut()
         });

     });

 </script>   
    <!-- Make sure the path to CKEditor is correct. -->
    <script type="text/javascript" src="ckeditor201507/ckeditor.js"></script>
    <%--插入资源用的js代码--%>
    <script type="text/javascript">
        var fileUrl = "<%= RandomIDCD %>";
        CKEDITOR.plugins.add('timestamp', {
            icons: 'timestamp',
            init: function (editor) {
                //Plugin logic goes here.
                editor.addCommand('insertTimestamp', {
                    exec: function showMyDialog(e) {
                        var str = 'width=980,height=650,left=' + ((screen.width - 900) / 2) + ',top=' + ((screen.height - 650) / 2) + ',scrollbars=no,scrolling=no,location=no,toolbar=no'
                        var w = window.open('File_Browse2.aspx?ID=' + fileUrl + '', 'MyWindow', str);
                    }
                });
                editor.ui.addButton('Timestamp', {
                    label: '插入资源',
                    command: 'insertTimestamp',
                    toolbar: 'insert'
                });
            }
        });
    </script>

    <!-- 旧版母版的css -->
    <link rel="stylesheet" type="text/css" href="User.master.css" />
    <link rel="stylesheet" type="text/css" href="webuploader/style2.css" />
    <link rel="stylesheet" type="text/css" href="webuploader/webuploader.css" />
    <script type="text/javascript" src="webuploader/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" src="webuploader/webuploader.js"></script>
    <script type="text/javascript" src="webuploader/ImagUpload.js"></script>
    <script type="text/javascript">
        $().ready(function () {
        });
    </script>

    <!--Basic Styles-->
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" />
    <%--<link href="#" rel="stylesheet" />--%>
    <link href="assets/css/font-awesome.min.css" rel="stylesheet" />
    <link href="assets/css/weather-icons.min.css" rel="stylesheet" />

    <!--Beyond styles-->
    <link href="assets/css/beyond.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/demo.min.css" rel="stylesheet" />
    <link href="assets/css/typicons.min.css" rel="stylesheet" />
    <link href="assets/css/animate.min.css" rel="stylesheet" />
     <!-- 上传附件模态框弹出部分  --->
    <div id="files_add" runat="server" class="row" style="position:fixed;z-index:1003;width:60%;left:25%;padding-top:1%;display:none;background-color:rgba(85, 85, 85, 0.60)">
                            <div class="col-lg-12 col-sm-12 col-xs-12">
                                <div class="well with-header  with-footer">
                                    <div class="header bordered-blue">上传资源<asp:Button ID="FilesClose" runat="server" class="module-close2" BorderStyle="None" OnClick="File_Close_Click" /></div>
                                    <div class="form-group">
                                        <div>
                                                        <div id="wrapper">
                                                            <div id="container">
                                                                <!--头部，相册选择和格式选择-->
                                                                <div id="uploader">
                                                                    <div class="queueList">
                                                                        <div id="dndArea" class="placeholder">
                                                                            <div id="filePicker"></div>
                                                                            <p>或将文件拖到这里，单次最多可选50个文件</p >
                                                                        </div>
                                                                    </div>
                                                                    <div class="statusBar" style="display: none;">
                                                                        <div class="progress">
                                                                            <span class="text">0%</span>
                                                                            <span class="percentage"></span>
                                                                        </div>
                                                                        <div class="info"></div>
                                                                        <div class="btns">
                                                                            <div id="filePicker2"></div>
                                                                            <div class="uploadBtn">开始上传</div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    <div>
                                                        &nbsp;&nbsp;&nbsp;
                                                        <asp:Label ID="FET" runat="server" Text="Label" Visible="false"></asp:Label>
                                                        <asp:Label ID="FS" runat="server" Text="Label" Visible="false"></asp:Label>
                                                        <asp:Label ID="FN" runat="server" Text="Label" Visible="false"></asp:Label>
                                                        <asp:Label ID="FP" runat="server" Text="Label" Visible="false"></asp:Label>
                                                        <asp:Label ID="ResourceTypeLabel" runat="server" Text="" Visible="false"></asp:Label>
                                                
                        <asp:TextBox ID="TextBox2" runat="server" Width="280px" Visible="false" ></asp:TextBox>
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            </asp:UpdatePanel>
                                                    <p>
                                                        &nbsp;
                                                    </p>
                                                    <div>
                                                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="file_sure" runat="server" Text="确定保存" class="btn btn-info" OnClick="file_sure_Click" />
                                                        &nbsp;&nbsp;<asp:Button ID="Abolish" runat="server" Text="取消" class="btn btn-default" Visible="false" />
                                                    </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
        </div>
       <!-- 封面图模态框弹出部分  --->
    
    <div id="Cover" runat="server" style="background-color: rgba(114, 114, 114, 0.5); position:fixed; padding:10px 10px; width:50%; height:65%; overflow-y:scroll; z-index: 1000; margin:0 10%;display:none;">
        <div class="col-lg-12" >
            <div class="widget">
                <div class="widget-header"><span class="widget-caption">选择封面图片</span>
                    <asp:Button ID="EscCoverShow" runat="server" class="module-close2" BorderStyle="None" OnClick="EscCoverShow_Click"/>
                </div>
                <div id="coverphotos" runat="server" class="widget-body">
                    
                </div>
    </div>
    </div>
    </div>
       <!-- //结束  --->
    <div class="row">
        <!-- 标签表模态框弹出部分  --->
        <div class="blanks2"></div>
        <div class="module-area2 modal-content2">
            <div class="module-head2">
                <span>标签查询</span>
                  <asp:Button runat="server" class="module-close2" BorderStyle="None" OnClick="Unnamed1_Click"/>
            </div>
            <div id="BDCenter">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div id="loginInput">
                            <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
                            <p style="float: left; width: 100%;">
                                <asp:TextBox ID="ArTagName" runat="server" Height="28px" Width="40%"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="AddArTag" runat="server" class="btn btn-info " Text="添加标签" OnClick="AddArTag_Click" />
                                （用逗号隔开）
                            </p>
                            <div  runat="server" style="overflow: scroll; width: 100%; height: 150px; overflow-x: hidden;">
                                <asp:CheckBoxList ID="TagsList" runat="server" Style="margin-left: 5px; margin-top: 0px; text-align: left; width: 100%" RepeatDirection="Horizontal"
                                    RepeatLayout="Table" RepeatColumns="5">
                                </asp:CheckBoxList>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:Button ID="Button8" runat="server" Text="确定"  class="Sure2 " BorderStyle="None" OnClick="Button8_Click" />
                <asp:Button ID="Button9" runat="server" Text="取消"  class="Sure2 " BorderStyle="None" OnClick="Button9_Click"/>
            </div>
        </div>

        <!-- 模态框弹出部分  --->
        <asp:Label ID="RandomID" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="IDLabel" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="TagiIDs" runat="server" Text="" Visible="false"></asp:Label>
        <!---存放模态框选中的TagID -->
        <asp:Label ID="Label3" runat="server" Text="" Visible="false"></asp:Label>
        <!---存放数据库中“Articles_ArticleTags”的TagID数-->
        <asp:Label ID="Count" runat="server" Text="" Visible="false"></asp:Label>
        <!--存标签-->
        <asp:Label ID="tag_return" runat="server" Text="," Visible="false"></asp:Label>


        <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="2000" Enabled="false" />
        <div id="CurrentPosition">当前位置：<a href="Article_Man.aspx">文章管理</a> >> <a href="Article_Add.aspx">发表新文章</a></div>
        <p>&nbsp;</p>
       
 
        <table width="100%" align="left" id="RegistTable">
            <tr>
                <td align="center" class="auto-style1">
                    <asp:Button ID="Button3" runat="server" Text=" 提交发表 " OnClick="Button3_Click" class="btn btn-default shiny" />
                </td>

                <td width="200" align="left" style="text-align: center">
                        <asp:Button ID="Button1" runat="server" Text=" 保存草稿 " class="btn btn-default shiny" OnClick="Button1_Click" />&nbsp;
                   
                </td>
                <td width="120" align="left">
                     <a>
                        <asp:Button ID="Button2" runat="server" Text=" 文章预览 " class="btn btn-default shiny" OnClick="Button2_Click" /></a>

                </td>
                <td width="200">
                     <asp:Button ID="Button11" runat="server" Text="撰写新文章 " class="btn btn-default shiny" OnClick="Button11_Click" />&nbsp;
                    </td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td align="left" colspan="3">
                    <asp:Label ID="ResultLabel" runat="server"
                        Font-Bold="True" ForeColor="#00CC00"></asp:Label></td>
            </tr>
            <tr>
                <td style="text-align: center" class="auto-style1">文章标题：</td>
                   
                <td width="300" align="left" colspan="3">
                    <asp:TextBox ID="TitleTB" runat="server" MaxLength="30" CssClass="TextBox" Width="560"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="auto-style2"></td>
                <td align="left" colspan="3" class="noUi-horizontal">
                    <div id="div4" style="font-size: 12px; color: #999;">文章标题，请勿超过20个字符<asp:Label ID="cktext" runat="server"></asp:Label></div>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center" class="auto-style1">文章分类：</td>
                <td width="200" align="left" colspan="3">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:DropDownList ID="Cats" runat="server" Height="30" AutoPostBack="True" OnSelectedIndexChanged="Cats_SelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                            <asp:DropDownList ID="Subs" runat="server" Height="30" AutoPostBack="True" OnSelectedIndexChanged="Subs_SelectedIndexChanged">
                            </asp:DropDownList>
                             &nbsp;&nbsp;
                            <asp:DropDownList ID="Class" runat="server" Height="30" Visible="false">
                            </asp:DropDownList>
                             <asp:Label ID="Warning" runat="server" Visible="false" Text="选择作品类或学院动态类的要求必需写简介和上传封面图"
                        Font-Bold="True" ForeColor="#f6c12a"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
            </tr>
          
             <tr>
                <td class="auto-style1">&nbsp;</td>
            </tr>
    <tr>
        
        <td style="text-align: center" class="auto-style1"> 文章附件：</td>
        <td id="Td1" runat="server">
           <asp:LinkButton ID="file_chose" runat="server" OnClick="file_chose_Click">+添加附件</asp:LinkButton>
        </td>
            </tr>
            <tr>
                <td style="text-align: center" class="auto-style1"></td> 
                <td  ></td>
            </tr>
        </table>
    </div>
    <div style="clear: both"></div>
    <div id="File_Box" runat="server" style="margin-left:17%;width:45%;border:0px solid #ccc;"></div>
    <div style="clear: both ;height:15px;"></div>
     <div class="row">
        <div>
            <div class="col-xs-12">
                <div class="dashboard-box">
                    <div class="box-tabbs">
                        <div class="tabbable">
                            <asp:Button ID="Button5" runat="server" Text="内容编辑" Style="padding: 7px 0; width: 33%; float: left;" BackColor="#F0F0F0" BorderStyle="None" OnClick="Button5_Click" />
                            <asp:Button ID="Button6" runat="server" Text="发布设置" Style="padding: 7px 0; width: 33%; float: left;" BackColor="#d3d3d3" BorderStyle="None" OnClick="Button6_Click" />
                            <asp:Button ID="Button7" runat="server" Text="封面图设置" Style="padding: 7px 0; width: 34%;" BackColor="#d3d3d3" BorderStyle="None" OnClick="Button7_Click" />
                            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                <asp:View ID="view1" runat="server">
                                    <div id="visits" class="tab-pane active animated fadeInUp">
                                        <div class="row">
                                            <div class="col-lg-12 chart-container" style="height: 710px;">
                                                <div id="dashboard-chart-visits" class="chart chart-lg no-margin">
                                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                                    <asp:TextBox ID="Editor1" runat="server" TextMode="MultiLine" />
                                                    <script type="text/javascript">
                                                        // Replace the <textarea id="editor1"> with a CKEditor
                                                        // instance, using default configuration.
                                                        var editor = CKEDITOR.replace('<%= Editor1.ClientID %>', { height: "600px" });
                                                    </script>
                                                    <p>&nbsp;</p>
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                </asp:View>
                                <asp:View ID="view2" runat="server">
                                    <div id="realtime" class="tab-pane   padding-left-5 padding-right-10 animated fadeInUp">
                                        <div class="row">
                                            <div class="col-lg-12" style="height: 650px">
                                                <div id="dashboard-chart-realtime" class="chart chart-lg no-margin" style="width: 100%; padding: 5% 0;">
                                                    <div style="margin: auto; width: 90%;">
                                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#00CC00"></asp:Label>
                                                        <p></p>
                                                        <div class="txt">
                                                            <p></p>
                                                            <p>发布位置：</p>

                                                            <p style="width: 420px;">
                                                                <asp:CheckBox ID="PC" runat="server" Text="电脑网站" Checked="true" /><asp:CheckBox ID="Phone" runat="server" Text="手机网站" />
                                                                <asp:CheckBox ID="Wechat" runat="server" Text="微信平台" /><asp:CheckBox ID="iPad" runat="server" Text="平板电脑" />
                                                                <asp:CheckBox ID="APP" runat="server" Text="手机App" /><asp:CheckBox ID="TV" runat="server" Text="大屏幕电视" />
                                                            </p>
                                                           
                                                        </div>

                                                        <div class="txt">
                                                            <p></p>
                                                            <p>文章摘要：</p>
                                                            <p style="width: 40%">
                                                                <asp:TextBox ID="Summary" class="form-control" TextMode="MultiLine"  MaxLength="20"   Columns="60" Rows="6" Width="100%" placeholder="简述文章内容 【学院动态类最好不要超过30字，作品类最好不要超过80字】" runat="server"></asp:TextBox>

                                                            </p>

                                                            <p></p>
                                                        </div>
                                                        <p></p>
                                                        <div class="txt">
                                                            <p></p>
                                                            <p>文章排序：</p>
                                                            <p style="font-size: 12px; color: #999; width:300px;">
                                                                <asp:TextBox ID="Orders" runat="server" CssClass="TextBox" Text="1"></asp:TextBox> &nbsp;&nbsp;如需置顶，排序值需><asp:Label ID="MaxOrders" runat="server" Text="0"></asp:Label>
                                                            </p>
                                                            <p ></p>
                                                        </div>
                                                         <p></p>
                                                        <div class="txt">
                                                            <p></p>
                                                            <p>是否在列表页显示：</p>
                                                            <p style="font-size: 12px; color: #999; width:300px;">
                                                               <asp:DropDownList ID="IsList" runat="server" >
                                                                   
                                                                   <asp:ListItem Value="1">是</asp:ListItem>
                                                                   <asp:ListItem Value="0">否</asp:ListItem>
                                                                   
                                                               </asp:DropDownList>
                                                            </p>
                                                            <p ></p>
                                                        </div>
                                                         <p></p>
                                                        <div class="txt">
                                                            <p></p>
                                                            <p>是否允许评论：</p>
                                                            <p style="font-size: 12px; color: #999; width:300px;">
                                                               <asp:DropDownList ID="IsComment" runat="server" >
                                                                   <asp:ListItem Value="1">是</asp:ListItem>
                                                                   <asp:ListItem Value="0">否</asp:ListItem>
                                                               </asp:DropDownList>
                                                            </p>
                                                            <p ></p>
                                                        </div>
                                                         <p></p>
                                                        <div class="txt">
                                                            <p></p>
                                                            <p>发表日期：</p>
                                                            <p>
                                                                <asp:TextBox ID="CDT_TextBox" runat="server" CssClass="TextBox"> </asp:TextBox>
                                                            </p>
                                                            <p></p>
                                                        </div>
                                                        <p></p> <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                                                                <ContentTemplate>
                                                        <div class="txt">
                                                            <p></p>
                                                            <p>用户标签：</p>
                                                            <p>  <asp:DropDownList ID="UserTag" runat="server" Height="30" AutoPostBack="True" OnSelectedIndexChanged="UserTag_SelectedIndexChanged"></asp:DropDownList>
                                                                      </p>
                                                            <p></p>
                                                        </div></ContentTemplate>
                                                                </asp:UpdatePanel>
                                                        <p></p>
                                                        <div class="txt">
                                                            <p></p>
                                                            <p>关键字(标签)：</p>
                                                            <div id="Tag_Show" runat="server" style="border:1px solid #ccc; height:120px;  width:320px; margin-left:32%">
                                                            </div>
                                                        </div>
                                                        <div class="txt">
                                                            <p></p><p></p>
                                                            <div style="float:left;">
                                                                <asp:Label ID="Label2" class="btn btn-info login2" runat="server" Text="添加标签"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="Clear1" runat="server" Text="清空"  class="btn btn-info login2" OnClick="Clearn_Click"/>  
                                                            </div>
                                                        </div>
                                                        <%-- <asp:Button ID="TagBtn" runat="server" Text="标签查询" class="btn btn-info "  onclick="TagBtn_Click" />&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                                        <p></p>
                                                        <!--封面图原处-->
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:View>
                                                  <asp:View runat="server" ID="view3">
                                                      <div id="visits1" class="tab-pane active animated fadeInUp">
                                                        <div class="row">
                                                            <div class="col-lg-12 chart-container" style="height:500px;padding:5% 5%;text-align:center;";>
                                                                
                                                                <div  style ="background-color:#f6efa4; width:335px;  padding:5px; float:left">
                                                                    <div style ="background-color:white;width:325px;">
                                                                     <asp:Image ID="CoverPhoto" runat="server" Width="325" Height="170" AlternateText="文章封面图" BorderColor="#000000" BorderWidth="1px" Visible="false"  />
                                                               <asp:Image ID="CoverPhoto1" runat="server"  Width="325" Height="170" AlternateText="文章封面图" BorderColor="#000000" BorderWidth="1px"  />
                                                                     </div>
                                                                         </div>

                                                                <div style="display:inline-block;text-align:left;">     
                                                                    <h1></h1>                                                                                                                             
                                                                    <h5> &nbsp;&nbsp;1.如果上传的文章属于<asp:Label ID="Label4" runat="server" Text="作品类或学院动态类" ForeColor="Red"></asp:Label>，则要求必须上传封面图，</h5>
                                                                    <h5> &nbsp;&nbsp;2.如果上传的文章属于<asp:Label ID="Label5" runat="server" Text="活动类或普通新闻类" ForeColor="Red"></asp:Label>，则建议上传封面图，</h5>
                                                                    <h5>&nbsp;&nbsp;3.如果选择不上传封面图，将以默认图为封面图。</h5>
                                                                    <h5 >&nbsp;&nbsp;（封面图片大小建议:作品类为：263*177;学院动态类为：660*356）</h5>

                                                                    <p></p>                                               
                                                                </div>

                                                                <div style="clear:both; border:1px solid  white">   
                                                                    <h1>&nbsp;</h1> 
                                                                      
                                                                      <div style="width:70px; overflow:hidden;float:left;margin-left:25%" >  <asp:FileUpload ID="FileUpload1" runat="server"  CssClass="Button"/> </div> 
                                                                      <div style="float:left"><asp:Button ID="UploadButton" runat="server" Text=" 上传 " class="Button" OnClick="UploadButton_Click" /></div>
                                                                       &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="Button10" runat="server"  CssClass="Button" Text="从服务器上选择" OnClick="Button10_Click" />
                                                                   </div>
                                                            </div>
                                                        </div>
                                                      </div>
                                                  </asp:View>
                            </asp:MultiView>
                            <div class="tab-content tabs-flat no-padding">
                                <!--  1  --->
                                <!---  2  -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="clear">
    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        #txt p {
            float: left;
            text-align: center;
            width: 200px;
        }

        .auto-style1 {
            width: 120px;
        }

        .auto-style2 {
            width: 120px;
            height: 18px;
        }

        .txt p {
            float: left;
            width: 16%;
        }

        .txt {
            clear: both;
            width: 100%;
        }
    </style>
</asp:Content>
