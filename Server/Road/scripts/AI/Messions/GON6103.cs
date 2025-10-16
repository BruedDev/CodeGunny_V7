// Type: GameServerScript.AI.Messions.DTGT1167
// Assembly: GameServerScripts, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Assembly location: D:\DDTank\Trash\R\My Documents\GameServerScripts.dll

using Game.Logic.AI;
using Game.Logic.Phy.Object;

namespace GameServerScript.AI.Messions
{
  public class GON6103 : AMissionControl
  {
    private static string[] KillChat = new string[2]
    {
      "Gửi cho bạn trở về nhà!",
      "Một mình, bạn có ảo tưởng có thể đánh bại tôi?"
    };
    private static string[] ShootedChat = new string[2]
    {
      " Đau ah! Đau ...",
      "Quốc vương vạn tuế ..."
    };
    private int IsSay = 0;
    private int bossID = 6131;
    private SimpleBoss m_boss;
    private PhysicalObj m_front;

    public override int CalculateScoreGrade(int score)
    {
      base.CalculateScoreGrade(score);
      if (score > 900)
        return 3;
      if (score > 825)
        return 2;
      return score > 725 ? 1 : 0;
    }

    public override void OnPrepareNewSession()
    {
      base.OnPrepareNewSession();
      Game.AddLoadingFile(1, "bombs/61.swf", "tank.resource.bombs.Bomb61");
      Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.BossBgAsset");
      Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.boguoLeaderAsset");
      Game.AddLoadingFile(2, "image/game/living/Living189.swf", "game.living.Living189");
      int[] npcIds = { bossID };
      Game.LoadResources(npcIds);
      Game.LoadNpcGameOverResources(npcIds);
      Game.SetMap(1167);
    }

    public override void OnStartGame()
    {
      base.OnStartGame();
      m_boss = Game.CreateBoss(bossID, 1250, 700, -1, 1,"");
      m_front = Game.Createlayer(1245, 520, "font", "game.living.Living189", "stand", 1, 0);
      m_boss.FallFrom(m_boss.X, m_boss.Y, "", 0, 0, 1000);
      m_boss.SetRelateDemagemRect(-34, -35, 100, 70);
    }

    public override void OnNewTurnStarted()
    {
      base.OnNewTurnStarted();
    }

    public override void OnBeginNewTurn()
    {
      base.OnBeginNewTurn();
      IsSay = 0;
    }

    public override bool CanGameOver()
    {
      base.CanGameOver();
      return Game.TurnIndex > Game.MissionInfo.TotalTurn - 1 || !m_boss.IsLiving;
    }

    public override int UpdateUIData()
    {
      if (m_boss == null)
        return 0;
      if (!m_boss.IsLiving)
        return 1;
      else
        return base.UpdateUIData();
    }

    public override void OnGameOver()
    {
      base.OnGameOver();
      if (!m_boss.IsLiving)
        Game.IsWin = true;
      else
        Game.IsWin = false;
    }
  }
}
