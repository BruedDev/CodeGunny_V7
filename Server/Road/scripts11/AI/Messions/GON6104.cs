using Game.Logic.AI;
using Game.Logic.Phy.Object;

namespace GameServerScript.AI.Messions
{
  public class GON6104 : AMissionControl
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
    private int bossID = 6141;
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
      Game.AddLoadingFile(2, "image/game/effect/6/chang.swf", "asset.game.six.chang");
      Game.AddLoadingFile(2, "image/game/effect/6/popcan.swf", "asset.game.six.popcan");
      int[] npcIds = new int[1] { bossID };
      Game.LoadResources(npcIds);
      Game.LoadNpcGameOverResources(npcIds);
      Game.SetMap(1168);
    }

    public override void OnStartGame()
    {
      base.OnStartGame();
      m_boss = Game.CreateBoss(bossID, 1250, 950, -1, 1,"");
      m_front = Game.Createlayer(1245, 520, "font", "game.living.Living189", "stand", 1, 0);
      m_boss.FallFrom(m_boss.X, m_boss.Y, "", 0, 0, 1000);
      m_boss.SetRelateDemagemRect(m_boss.NpcInfo.X, m_boss.NpcInfo.Y, m_boss.NpcInfo.Width, m_boss.NpcInfo.Height);
      m_boss.PlayMovie("shengqi", 1000, 2000);
      m_boss.PlayMovie("xialai", 2000, 3000);
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
