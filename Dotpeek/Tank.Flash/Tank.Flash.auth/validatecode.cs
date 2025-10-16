// Decompiled with JetBrains decompiler
// Type: Tank.Flash.auth.validatecode
// Assembly: Tank.Flash, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A658ADB-D814-40A6-89D7-538E9C91201E
// Assembly location: C:\Users\vanloc\Desktop\CodeGunny_V7\Client\bin\Tank.Flash.dll

using Bussiness;
using System.Web;
using System.Web.SessionState;

#nullable disable
namespace Tank.Flash.auth;

public class validatecode : IHttpHandler, IRequiresSessionState
{
  public void ProcessRequest(HttpContext context)
  {
    string checkCode = CheckCode.GenerateCheckCode();
    byte[] image = CheckCode.CreateImage(checkCode);
    context.Session["CheckCode"] = (object) checkCode;
    context.Response.ClearContent();
    context.Response.ContentType = "image/Gif";
    context.Response.BinaryWrite(image);
  }

  public bool IsReusable => false;
}
