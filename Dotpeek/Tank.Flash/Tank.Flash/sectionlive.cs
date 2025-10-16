// Decompiled with JetBrains decompiler
// Type: Tank.Flash.sectionlive
// Assembly: Tank.Flash, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A658ADB-D814-40A6-89D7-538E9C91201E
// Assembly location: C:\Users\vanloc\Desktop\CodeGunny_V7\Client\bin\Tank.Flash.dll

using System.Web;
using System.Web.SessionState;

#nullable disable
namespace Tank.Flash;

public class sectionlive : IHttpHandler, IRequiresSessionState
{
  public void ProcessRequest(HttpContext context)
  {
    string str = context.Request["skiplive"];
    try
    {
      if (str == "false")
      {
        LoadingManager.Remove(context.Session["username"].ToString());
        context.Response.Write("clear");
      }
      else if (context.Session["username"] != null && !string.IsNullOrEmpty(context.Session["username"].ToString()))
      {
        if (LoadingManager.Login(context.Session["username"].ToString(), context.Session["password"].ToString()))
          context.Response.Write("live");
        else
          context.Response.Write("die");
      }
      else
        context.Response.Write("die");
    }
    catch
    {
      context.Response.Write("sessionfail");
    }
  }

  public bool IsReusable => false;
}
