// Decompiled with JetBrains decompiler
// Type: Tank.Flash.LoadingManager
// Assembly: Tank.Flash, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A658ADB-D814-40A6-89D7-538E9C91201E
// Assembly location: C:\Users\vanloc\Desktop\CodeGunny_V7\Client\bin\Tank.Flash.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;

#nullable disable
namespace Tank.Flash;

public class LoadingManager
{
  public static int LoadingCount = 0;
  private static Dictionary<string, LoadingManager.PlayerData> m_players = new Dictionary<string, LoadingManager.PlayerData>();
  private static int m_timeout = 30;
  private static Timer m_timer;
  private static object sys_obj = new object();

  public static void Add(string name, string pass) => LoadingManager.Add(name, pass, false);

  public static void Add(string name, string pass, bool isAdmin)
  {
    lock (LoadingManager.sys_obj)
    {
      if (LoadingManager.m_players.ContainsKey(name))
      {
        LoadingManager.m_players[name].Name = name;
        LoadingManager.m_players[name].Pass = pass;
        LoadingManager.m_players[name].Date = DateTime.Now;
        LoadingManager.m_players[name].Count = 0;
        LoadingManager.m_players[name].IsAdmin = isAdmin;
      }
      else
      {
        LoadingManager.PlayerData playerData = new LoadingManager.PlayerData()
        {
          Name = name,
          Pass = pass,
          Date = DateTime.Now,
          IsAdmin = isAdmin
        };
        LoadingManager.m_players.Add(name, playerData);
      }
    }
  }

  protected static bool CheckTimeOut(DateTime dt)
  {
    return (DateTime.Now - dt).TotalMinutes > (double) LoadingManager.m_timeout;
  }

  private static void CheckTimerCallback(object state)
  {
    lock (LoadingManager.sys_obj)
    {
      List<string> stringList = new List<string>();
      foreach (LoadingManager.PlayerData playerData in LoadingManager.m_players.Values)
      {
        if (LoadingManager.CheckTimeOut(playerData.Date))
          stringList.Add(playerData.Name);
      }
      foreach (string key in stringList)
        LoadingManager.m_players.Remove(key);
    }
  }

  public static bool CheckUser(string name)
  {
    lock (LoadingManager.sys_obj)
    {
      if (LoadingManager.m_players.ContainsKey(name))
        return true;
    }
    return false;
  }

  public static bool GetByUserIsFirst(string name)
  {
    lock (LoadingManager.sys_obj)
    {
      if (LoadingManager.m_players.ContainsKey(name))
        return LoadingManager.m_players[name].Count == 0;
    }
    return false;
  }

  public static bool Login(string name, string pass)
  {
    lock (LoadingManager.sys_obj)
    {
      if (!LoadingManager.m_players.ContainsKey(name) || !(LoadingManager.m_players[name].Pass == pass))
        return false;
      LoadingManager.PlayerData player = LoadingManager.m_players[name];
      return player.Pass == pass && !LoadingManager.CheckTimeOut(player.Date);
    }
  }

  public static bool Remove(string name)
  {
    lock (LoadingManager.sys_obj)
      return LoadingManager.m_players.Remove(name);
  }

  public static void Setup()
  {
    LoadingManager.m_timeout = int.Parse(ConfigurationManager.AppSettings["LoginSessionTimeOut"]);
    LoadingManager.m_timer = new Timer(new TimerCallback(LoadingManager.CheckTimerCallback), (object) null, 0, 60000);
  }

  public static bool Update(string name, string pass)
  {
    lock (LoadingManager.sys_obj)
    {
      if (LoadingManager.m_players.ContainsKey(name))
      {
        LoadingManager.m_players[name].Pass = pass;
        ++LoadingManager.m_players[name].Count;
        return true;
      }
    }
    return false;
  }

  public static bool UpdateKey(string name, string key)
  {
    lock (LoadingManager.sys_obj)
    {
      if (LoadingManager.m_players.ContainsKey(name))
      {
        LoadingManager.m_players[name].Key = key;
        return true;
      }
    }
    return false;
  }

  private class PlayerData
  {
    public int Count;
    public DateTime Date;
    public bool IsAdmin;
    public string Key;
    public string Name;
    public string Pass;
  }
}
