<%@ WebHandler Language="C#" Class="UserCode" %>

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

public class UserCode : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) 
    {
        context.Response.ContentType = "image/Png";
        context.Response.Clear();

        var buffer = GenerateImage(context.Request["a"]);
        context.Response.OutputStream.Write(buffer, 0, buffer.Length);
        context.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    private static byte[] GenerateImage(string text)
    {
        var bitmap = new Bitmap(1, 1);

        bitmap.SetResolution(300, 300);

        var g = Graphics.FromImage(bitmap);

        var font = new Font("Times New Roman", 9, FontStyle.Bold);

        var size = g.MeasureString(text, font).ToSize();

        var point = g.MeasureString(text, font).ToPointF();

        bitmap = new Bitmap(bitmap, size);

        bitmap.SetResolution(300, 300);

        g = Graphics.FromImage(bitmap);

        g.Clear(Color.GhostWhite);

        var brush = new LinearGradientBrush(new PointF(0, 0), point, Color.Blue, Color.DodgerBlue);

        g.DrawString(text, font, brush, 0, 0);

        g.Dispose();

        var stream = new MemoryStream();
        bitmap.Save(stream, ImageFormat.Png);
        return stream.ToArray();
    }
}