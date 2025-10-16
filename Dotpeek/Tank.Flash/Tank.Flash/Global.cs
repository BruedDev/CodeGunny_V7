// Decompiled with JetBrains decompiler
// Type: Tank.Flash.Global
// Assembly: Tank.Flash, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A658ADB-D814-40A6-89D7-538E9C91201E
// Assembly location: C:\Users\vanloc\Desktop\CodeGunny_V7\Client\bin\Tank.Flash.dll

using System;
using System.Web;

#nullable disable
namespace Tank.Flash;

public class Global : HttpApplication
{
  protected void Application_AuthenticateRequest(object sender, EventArgs e)
  {
  }

  protected void Application_BeginRequest(object sender, EventArgs e)
  {
  }

  protected void Application_End(object sender, EventArgs e)
  {
  }

  protected void Application_Error(object sender, EventArgs e)
  {
  }

  protected void Application_Start(object sender, EventArgs e)
  {
  }

  protected void Session_End(object sender, EventArgs e)
  {
    if (this.Session["Loading"] == null)
      return;
    --LoadingManager.LoadingCount;
  }

  protected void Session_Start(object sender, EventArgs e)
  {
  }
}
