// Decompiled with JetBrains decompiler
// Type: Tank.Flash.LoadingCount
// Assembly: Tank.Flash, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A658ADB-D814-40A6-89D7-538E9C91201E
// Assembly location: C:\Users\vanloc\Desktop\CodeGunny_V7\Client\bin\Tank.Flash.dll

using System;
using System.Web.UI;

#nullable disable
namespace Tank.Flash;

public class LoadingCount : Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    this.Response.Write($"{LoadingManager.LoadingCount}");
  }
}
