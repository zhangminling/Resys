<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Jcrop测试</title>
    <script type="text/javascript" src="js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="js/swfupload/swfupload.js"></script>
    <script type="text/javascript" src="js/swfupload/handlers.js"></script>
    <script type="text/javascript" src="js/Jcrop/js/jquery.Jcrop.min.js"></script>
    <script type="text/javascript" src="js/Jcrop/js/jquery.color.js"></script>
    <script type="text/javascript" src="js/mytest.js"></script>
    <link href="js/Jcrop/css/jquery.Jcrop.min.css" rel="Stylesheet" type="text/css" />
    <link href="css2/default.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
         var swfu;
         window.onload = function () {
             swfu = new SWFUpload({
                 upload_url: "imgUpload.ashx",
                 post_params: {
                     "ASP.NET_SESSIONID": "<%=Session.SessionID %>",
                     "ASPSESSID": "<%=Session.SessionID %>"
                 },

                 file_size_limit: "10240",//这里改上传的文件大小
                 file_types: "*",//图片的MIME类型
                 file_types_description: " Images",//上传文件的种类
                 file_upload_limit: "-1",

                 file_queue_error_handler: fileQueueError,
                 file_dialog_complete_handler: fileDialogComplete,
                 upload_progress_handler: uploadProgress,
                 upload_error_handler: uploadError,
                 upload_success_handler: uploadSuccess,
                 upload_complete_handler: uploadComplete,

                 button_image_url: "/Image/swfupload/XPButtonNoText_160x22.png",
                 button_width: "160",
                 button_height: "22",
                 button_placeholder_id: "spanButtonPlaceHolder1",
                 button_text: '<span class="theFont">选择文件</span>',
                 button_text_style: ".theFont { font-size: 13;}",
                 button_text_left_padding: 12,
                 button_text_top_padding: 3,
                 button_action: SWFUpload.BUTTON_ACTION.SELECT_FILE, //SWFUplaod.BUTTON_ACTION.SELECT_FILES 可以多选文件
                 flash_url: "/JS/swfupload/swfupload.swf",

                 custom_settings: {
                     upload_target: "divFileProgressContainer1"
                 },
                 debug: false
             });
         }
        
        function colseWin() {
            window.opener.location.href = window.opener.location.href;
            window.close();
           
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div ></div>
                
           
                <div id="swfu_container1" style="margin: 0px 10px;">
                    <div id="22">
                        <p>上传的图片小于10M</p>
                        <p>暂不支持火狐浏览器。</p>
                        <span id="spanButtonPlaceHolder1"></span>
                    </div>
                    <div id="divFileProgressContainer1" style="height: 75px;">
                    </div>
                    <div id="88" style="display: none"></div>
                    <div id="thumbnails1">
                        <input type="button" id="btn_imgcut" style="display: none" onclick="checkCoords($('#x').val(), $('#y').val(), $('#w').val(), $('#h').val(), $('#userphoto1').val())"
                        value="剪切头像" />
                        <div id="div_addPhoto">
                            <img alt="用户头像" id="img_UserPhoto1" name="img_UserPhoto1" />
                        </div>
                        <input type="hidden" runat="server" id="userphoto1" />
                    </div>
                    <div id="btn" style="display: none">
                        <br />
                            <input  type="button" value="确认插入" onclick = "colseWin();"/>&nbsp;&nbsp;&nbsp;
                            <input  type="button" value="重新上传" onclick="document.location.reload(); "/>
                       
                    </div>
                    <div id="div_photoadd" style="width: 230px; height: 230px; overflow: hidden; display: none">
                        <img alt="用户头像" id="viewUserPhoto" style="width: 230px; height: 230px;display: none"/>
                    </div>
                    
                    <input type="hidden" id="x" name="x" />
                    <input type="hidden" id="y" name="y" />
                    <input type="hidden" id="w" name="w" />
                    <input type="hidden" id="h" name="h" />
                </div>
    </form>
</body>
</html>
