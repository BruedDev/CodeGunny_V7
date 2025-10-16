// Decompiled with JetBrains decompiler
// Type: Tank.Flash.auth.forgotpass1
// Assembly: Tank.Flash, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A658ADB-D814-40A6-89D7-538E9C91201E
// Assembly location: C:\Users\vanloc\Desktop\CodeGunny_V7\Client\bin\Tank.Flash.dll

using System.Web;

#nullable disable
namespace Tank.Flash.auth;

public class forgotpass1 : IHttpHandler
{
  public void ProcessRequest(HttpContext context)
  {
    string s = "Request False!";
    context.Response.Write(s);
  }

  public bool IsReusable => false;
}
