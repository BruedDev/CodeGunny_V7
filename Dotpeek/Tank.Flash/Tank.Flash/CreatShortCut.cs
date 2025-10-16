// Decompiled with JetBrains decompiler
// Type: Tank.Flash.CreatShortCut
// Assembly: Tank.Flash, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A658ADB-D814-40A6-89D7-538E9C91201E
// Assembly location: C:\Users\vanloc\Desktop\CodeGunny_V7\Client\bin\Tank.Flash.dll

using Bussiness;
using System;
using System.Web;

#nullable disable
namespace Tank.Flash;

public class CreatShortCut : IHttpHandler
{
  public void ProcessRequest(HttpContext context)
  {
    try
    {
      string str1 = context.Request.QueryString["gameurl"];
      string upper = context.Request.UserAgent.ToUpper();
      string str2 = LanguageMgr.GetTranslation("Game.ProductionName") + ".url";
      string str3 = !upper.Contains("MS") || !upper.Contains("IE") ? (!upper.Contains("FIREFOX") ? HttpUtility.UrlEncode(str2) : $"\"{str2}\"") : HttpUtility.UrlEncode(str2);
      context.Response.ContentType = "application/octet-stream;";
      context.Response.AddHeader("Content-Disposition", "attachment;filename=" + str3);
      context.Response.Write("[InternetShortcut]\n");
      context.Response.Write($"URL={str1}\n");
      context.Response.Write("IDList=\n");
      context.Response.Write("IconFile=\n");
      context.Response.Write("IconIndex=1\n");
      context.Response.Write("[{000214A0-0000-0000-C000-000000000046}]\n");
      context.Response.Write("Prop3=19,2\n");
      context.ApplicationInstance.CompleteRequest();
    }
    catch (Exception ex)
    {
      context.Response.Write("Error:" + (object) ex);
    }
  }

  public bool IsReusable => false;
}
