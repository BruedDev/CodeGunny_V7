// Decompiled with JetBrains decompiler
// Type: Tank.Flash.auth.register
// Assembly: Tank.Flash, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A658ADB-D814-40A6-89D7-538E9C91201E
// Assembly location: C:\Users\vanloc\Desktop\CodeGunny_V7\Client\bin\Tank.Flash.dll

using Bussiness;
using log4net;
using System.Configuration;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

#nullable disable
namespace Tank.Flash.auth;

public class register : IHttpHandler, IRequiresSessionState
{
  private string code;
  private string email;
  protected static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
  private string message;
  private string password;
  private string repassword;
  private bool sex;
  private string username;

  protected bool CheckPara(HttpContext context, ref string message)
  {
    if (context.Session["CheckCode"] == null || this.code.ToLower() != context.Session["CheckCode"].ToString().ToLower())
    {
      message = "验证码错误!";
      return false;
    }
    using (MemberShipBussiness memberShipBussiness = new MemberShipBussiness())
    {
      if (memberShipBussiness.ExistsUsername(this.username))
      {
        message = "exit";
        return false;
      }
    }
    return true;
  }

  protected bool CreateUsername(HttpContext context, ref string message)
  {
    this.password = FormsAuthentication.HashPasswordForStoringInConfigFile(this.password, "md5");
    using (MemberShipBussiness memberShipBussiness = new MemberShipBussiness())
      return memberShipBussiness.CreateUsername(this.SiteTitle, this.username, this.password, this.email, "1", "MD5", this.sex);
  }

  public void ProcessRequest(HttpContext context)
  {
    this.username = context.Request["username"];
    this.password = context.Request["password"];
    this.repassword = context.Request["repassword"];
    this.email = context.Request["email"];
    this.code = context.Request["code"];
    this.message = "";
    if (this.CheckPara(context, ref this.message) && this.CreateUsername(context, ref this.message))
      this.message = "ok";
    context.Response.Write(this.message);
  }

  public bool IsReusable => false;

  protected string SiteTitle
  {
    get
    {
      return ConfigurationManager.AppSettings[nameof (SiteTitle)] != null ? ConfigurationManager.AppSettings[nameof (SiteTitle)] : "DanDanTang";
    }
  }
}
