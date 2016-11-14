<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using ThoughtWorks.QRCode.Codec;
//下载于51aspx.com
public class Handler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        if (PageOperate.GetNullToString(context.Request["data"]) == "")
            return;

        QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
        try
        {
            int scale = PageOperate.GetIntValue(context.Request["len"]);
            qrCodeEncoder.QRCodeScale = scale;
        }
        catch 
        { 
            
        }
        //生成二维码
        string data = PageOperate.GetNullToString(context.Request["data"]);
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        System.Drawing.Image myimg = qrCodeEncoder.Encode(data, System.Text.Encoding.UTF8);
        myimg.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
        context.Response.ClearContent();
        context.Response.ContentType = "image/Gif";
        context.Response.BinaryWrite(ms.ToArray());
        context.Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}