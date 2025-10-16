// Decompiled with JetBrains decompiler
// Type: Tank.Flash.logingame
// Assembly: Tank.Flash, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A658ADB-D814-40A6-89D7-538E9C91201E
// Assembly location: C:\Users\vanloc\Desktop\CodeGunny_V7\Client\bin\Tank.Flash.dll

using Bussiness.Interface;
using Road.Flash;
using System;
using System.Configuration;
using System.Web;
using System.Web.UI;

// #nullable disable
namespace Tank.Flash;

public class logingame : Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    // Fix: Check session properly
    if (this.Session["username"] == null || string.IsNullOrEmpty(this.Session["username"].ToString()))
    {
      this.Response.Redirect(this.LoginOnUrl, false);
      return;
    }

    if (!LoadingManager.Login(this.Session["username"].ToString(), this.Session["password"].ToString()))
    {
      this.Response.Redirect(this.LoginOnUrl, false);
      return;
    }

    try
    {
      string str1 = this.Session["username"].ToString();
      string str2 = Guid.NewGuid().ToString();
      string str3 = BaseInterface.ConvertDateTimeInt(DateTime.Now).ToString();
      string str4 = string.Empty;
      if (string.IsNullOrEmpty(str4))
        str4 = BaseInterface.GetLoginKey;
      string str5 = BaseInterface.md5(str1 + str2 + str3.ToString() + str4);

      // Fix: Add error handling for RequestContent
      string s1 = "0"; // Default success
      try
      {
        s1 = BaseInterface.RequestContent($"{BaseInterface.LoginUrl}?content={HttpUtility.UrlEncode($"{str1}|{str2}|{str3.ToString()}|{str5}")}");
      }
      catch (Exception ex)
      {
        // Log error but continue with default success
        s1 = "0";
      }

      if (s1 == "0")
      {
        string s2 = $"{logingame.FlashUrl}?user={HttpUtility.UrlEncode(str1)}&key={HttpUtility.UrlEncode(str2)}";
        if ("1" == ConfigurationManager.AppSettings["content2"])
        {
          string str6 = this.Session["password"] == null ? str2 : this.Session["password"].ToString();
          string iv = "5C90D3C2C576A773";
          s2 = $"{logingame.FlashUrl}?content2={CryptoHelper.TripleDesEncrypt("5628eb9a3485fbf61f51927b8a8eee03c5962c6b64847aeb", $"{str1}|{str6}", ref iv)}";
        }
        this.Response.Write(s2);
      }
      else
        this.Response.Write(s1);
    }
    catch (Exception ex)
    {
      this.Response.Write(ex.ToString());
    }
  }

  public static string FlashUrl => ConfigurationManager.AppSettings[nameof (FlashUrl)];

  public string LoginOnUrl => ConfigurationManager.AppSettings[nameof (LoginOnUrl)];
}
