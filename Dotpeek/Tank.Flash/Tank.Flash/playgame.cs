// Decompiled with JetBrains decompiler
// Type: Tank.Flash.playgame
// Assembly: Tank.Flash, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A658ADB-D814-40A6-89D7-538E9C91201E
// Assembly location: C:\Users\vanloc\Desktop\CodeGunny_V7\Client\bin\Tank.Flash.dll

using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

#nullable disable
namespace Tank.Flash;

public class playgame : Page
{
  private string _content = "";
  private string autoParam = "";
  protected HtmlHead Head1;

  protected void Page_Load(object sender, EventArgs e)
  {
    try
    {
      if (this.Session["username"] == null && string.IsNullOrEmpty(this.Session["username"].ToString()))
        this.Response.Redirect(this.LoginOnUrl, false);
      else if (!LoadingManager.Login(this.Request["user"], this.Session["password"].ToString()))
      {
        LoadingManager.Remove(this.Session["username"].ToString());
        this.Response.Redirect(this.LoginOnUrl, false);
      }
      else if ("1" == ConfigurationManager.AppSettings["content2"])
      {
        string str = this.Request["content2"];
        if (!string.IsNullOrEmpty(str))
          this._content = str;
        else
          this.Response.Redirect(this.LoginOnUrl, false);
      }
      else
      {
        string str1 = HttpUtility.UrlDecode(this.Request["user"]);
        string str2 = HttpUtility.UrlDecode(this.Request["key"]);
        HttpUtility.UrlDecode(this.Request["config"]);
        string str3 = this.Request["editby"] == null ? "" : HttpUtility.UrlDecode(this.Request["editby"]);
        if (!string.IsNullOrEmpty(str1) && !string.IsNullOrEmpty(str2))
        {
          this._content = $"user={HttpUtility.UrlEncode(str1)}&key={HttpUtility.UrlEncode(str2)}";
          this.autoParam = "editby=" + HttpUtility.UrlEncode(str3);
        }
        else
          this.Response.Redirect(this.LoginOnUrl, false);
      }
    }
    catch
    {
      this.Response.Redirect(this.LoginOnUrl, false);
    }
  }

  public string AutoParam => this.autoParam;

  public string Config => ConfigurationManager.AppSettings["FlashConfig"];

  public string Content => this._content;

  public string Flash => ConfigurationManager.AppSettings["FlashSite"];

  public string LoginOnUrl => ConfigurationManager.AppSettings[nameof (LoginOnUrl)];

  public string SiteTitle
  {
    get
    {
      return ConfigurationManager.AppSettings[nameof (SiteTitle)] != null ? ConfigurationManager.AppSettings[nameof (SiteTitle)] : "DDTank";
    }
  }
}
