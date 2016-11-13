<%@ Page Language="C#" AutoEventWireup="true" CodeFile="File_Preview.aspx.cs" Inherits="File_Preview" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>资源预览页</title>
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" />
<link href="assets/css/beyond.min.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-color:#eee;">
    <form id="form1" runat="server">
        <asp:Label ID="LabelResourceID" runat="server" Visible="false" Text=""></asp:Label>
        <asp:Label ID="FileType" runat="server" Visible="false" Text=""></asp:Label>
    <div style="text-align:center;">
        <h1>资源预览</h1>
        <p>&nbsp;</p>
        <p style="text-align: center;">
            <asp:Image ID="Image1" Width="600" runat="server" AlternateText="" />
        </p>
        
            <asp:Button ID="Button1" runat="server" Text="返回" class="btn btn-info" OnClick="Button1_Click" />
        
    </div>
    </form>
</body>
</html>
