// Decompiled with JetBrains decompiler
// Type: Tank.Flash.checkuser
// Assembly: Tank.Flash, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A658ADB-D814-40A6-89D7-538E9C91201E
// Assembly location: C:\Users\vanloc\Desktop\CodeGunny_V7\Client\bin\Tank.Flash.dll

using Bussiness;
using System.Configuration;
using System.Web;
using System.Web.SessionState;

#nullable disable
namespace Tank.Flash;

public class checkuser : IHttpHandler, IRequiresSessionState
{
  public void ProcessRequest(HttpContext context)
  {
    string str1 = context.Request["username"];
    string str2 = context.Request["password"];
    using (MemberShipBussiness memberShipBussiness = new MemberShipBussiness())
    {
      if (memberShipBussiness.CheckUsername(this.SiteTitle, str1, str2))
      {
        context.Session["username"] = (object) str1;
        context.Session["password"] = (object) str2;
        LoadingManager.Add(str1, str2);
        context.Response.Write("ok");
      }
      else if (memberShipBussiness.CheckUsername("GameAdmin", str1, str2))
      {
        context.Session["username"] = (object) str1;
        context.Session["password"] = (object) str2;
        LoadingManager.Add(str1, str2, true);
        context.Response.Write("ok");
      }
      else
        context.Response.Write("帐号或密码错误!");
    }
  }

  public bool IsReusable => false;

  public string SiteTitle
  {
    get
    {
      return ConfigurationManager.AppSettings[nameof (SiteTitle)] != null ? ConfigurationManager.AppSettings[nameof (SiteTitle)] : "DanDanTang";
    }
  }
}
