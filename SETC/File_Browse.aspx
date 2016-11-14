<%@ Page Language="C#" AutoEventWireup="true" CodeFile="File_Browse.aspx.cs" Inherits="File_Browse" %>

<%@ Register TagPrefix="CuteWebUI" Namespace="CuteWebUI" Assembly="CuteWebUI.AjaxUploader" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>资源管理</title>
    <style type="text/css">
        /* Tab样式 */.container
        {
            width: 800px;
            margin: 10px auto;
        }
        ul.tabs
        {
            margin: 0;
            padding: 0;
            float: left;
            list-style: none;
            height: 32px;
            border-bottom: 1px solid #999;
            border-left: 1px solid #999;
            width: 100%;
        }
        ul.tabs li
        {
            float: left;
            margin: 0;
            padding: 0;
            height: 31px;
            line-height: 31px;
            border: 1px solid #999;
            border-left: none;
            margin-bottom: -1px;
            background: #e0e0e0;
            overflow: hidden;
            position: relative;
        }
        ul.tabs li a
        {
            text-decoration: none;
            color: #000;
            display: block;
            padding: 0 20px;
            border: 1px solid #fff;
            outline: none;
        }
        ul.tabs li a:hover
        {
            background: #ccc;
        }
        html ul.tabs li.active, html ul.tabs li.active a:hover
        {
            background: #fff;
            border-bottom: 1px solid #fff;
        }
        .tab_container
        {
            border: 1px solid #999;
            border-top: none;
            clear: both;
            float: left;
            width: 100%;
            background: #fff;
            -moz-border-radius-bottomright: 5px;
            -khtml-border-radius-bottomright: 5px;
            -webkit-border-bottom-right-radius: 5px;
            -moz-border-radius-bottomleft: 5px;
            -khtml-border-radius-bottomleft: 5px;
            -webkit-border-bottom-left-radius: 5px;
        }
        .tab_content
        {
            padding: 20px;
        }
        .tab_content h2
        {
            font-weight: normal;
            padding-bottom: 10px;
            border-bottom: 1px dashed #ddd;
        }
        .tab_content h3 a
        {
            color: #254588;
        }
        .tab_content img
        {
            float: left;
            margin: 0 20px 20px 0;
            border: 1px solid #ddd;
            padding: 5px;
        }
        /* 文件夹样式 */#folders div
        {
            margin: 20px 20px;
            padding: 20px 20px;
            border: 1px solid #ccc;
            width: 60px;
            height: 60px;
            float: left;
        }
        .divHover
        {
            cursor: pointer;
            background-color: #73aef7;
            color: #fff;
        }
        #folders div p
        {
            text-align: center;
        }
        #folders a
        {
            margin: 20px 20px;
            padding: 20px 20px;
            border: 1px solid #ccc;
            width: 60px;
            height: 60px;
            float: left;
            text-align: center;
        }
        /*具体类型文件夹的样式表*/#AllFolders a
        {
            margin: 20px 20px;
            padding: 20px 20px;
            border: 1px solid #ccc;
            width: 60px;
            height: 60px;
            float: left;
            text-align: center;
        }
        a:hover
        {
            cursor: pointer;
            background-color: #73aef7;
            color: #fff;
        }
        a
        {
            text-decoration: none;
        }
        /*#AllFiles img
     {
         margin:20px 20px;
            padding:20px 20px;
            border:1px solid #ccc;
            width:60px;
            height:60px;
            float:left;} */</style>

    <script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>

    <%--<script type="text/javascript">
        $(document).ready(function() {
            // 文件夹
            $("#folders div").mouseover(function() {
                $(this).addClass("divHover");
            }).mouseout(function() {
                $(this).removeClass("divHover");
            }).click(function() {
                document.location.href = "File_Browse.aspx?type=" + $(this).children("p").html();
            });


            // Tab 功能
            $(".tab_content").hide(); //Hide all content
            $("ul.tabs li:first").addClass("active").show(); //Activate first tab
            $(".tab_content:first").show(); //Show first tab content

            //On Click Event
            $("ul.tabs li").click(function() {
                $("ul.tabs li").removeClass("active"); //Remove any "active" class
                $(this).addClass("active"); //Add "active" class to selected tab
                $(".tab_content").hide(); //Hide all tab content
                var activeTab = $(this).find("a").attr("href"); //Find the rel attribute value to identify the active tab + content
                $(activeTab).fadeIn(); //Fade in the active content

            });
        });
    </script>--%>

</head>
<body>
    <form id="form1" runat="server">
    <!-- Tab Begin -->
    <div class="container">
        <p>
            插入多媒体、附件等资源。当前UserID=<%= Session["UserID"] %></p>
        <ul class="tabs">
            <li><a href="#tab2">上传新资源</a></li>
        </ul>
        <div class="tab_container">            
            <!-- Tab 2 -->
            <div id="tab2" class="tab_content">
                <!-- <h2>Submit</h2>            -->
                <p>1、从本地上传新资源到服务器</p>
                <p><small>合法的文件包括：音频、视频、图片、文档、压缩、Flash等，音频、视频、Flash、文档和压缩文件最大200M，图片最大20M。</small></p>
                <div>
                    <asp:Label ID="FET" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="FS" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="FN" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="FP" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="ResourceTypeLabel" runat="server" Text="" Visible="false"></asp:Label>
                    <CuteWebUI:Uploader runat="server" ID="Uploader1" InsertText="上传资源到服务器" OnFileUploaded="Uploader_FileUploaded">
                    </CuteWebUI:Uploader>
                </div>
                                  
            <p>2、对资源重命名</p>
            <p><small>输入有意义的名字，方便日后对资源的查找和管理</small></p>
            <p id="IMG">
                <asp:Image ID="Image1" runat="server" />
            </p>          
            <div>
                <asp:TextBox ID="TextBox1" runat="server" Width="280px"></asp:TextBox>&nbsp; 
                </div>
            
            <%--<p>3、选择资源的类型</p>
            <p><small>对于图片和视频资源，请选择他们对应的版本</small></p>
            <div>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Selected="True">电脑</asp:ListItem>
                    <asp:ListItem>手机</asp:ListItem>
                </asp:RadioButtonList>
            </div>--%>
            
                
                <p>
                    &nbsp;</p>
                <div>
                    <asp:Button ID="Insert" runat="server" Text="插入" CssClass="Button" 
                    onclick="Insert_Click"  />
                    &nbsp;&nbsp;<asp:Button ID="Abolish" runat="server" Text="取消" Visible="false" />
                </div>
                <p>
                    <asp:Label ID="TestLabel" runat="server" Text="Test Label" Visible="false"></asp:Label></p>
                <p>
                    &nbsp;</p>
            </div>
            <!-- End of tab2 -->
        </div>
        <!-- End of tab_container -->
    </div>
    <!-- Tab End -->
    </form>
</body>

<script type="text/javascript">
    var fileUrl = "<%= insertFileHTML %>";
    var btn1 = document.getElementById("Insert");
    btn1.onclick = function() {
        //alert(fileUrl);
        window.opener.CKEDITOR.instances.ctl00_ContentPlaceHolder1_Editor1.insertHtml(fileUrl);
        window.close();

    };    

</script>

</html>
