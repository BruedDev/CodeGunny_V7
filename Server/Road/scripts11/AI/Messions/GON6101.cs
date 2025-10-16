using Game.Logic.AI;
using Game.Logic.Phy.Object;
using System.Collections.Generic;
using System.Drawing;

namespace GameServerScript.AI.Messions
{
  public class GON6101 : AMissionControl
  {
    private int turn = 0;
    private SimpleBoss m_boss;
    private PhysicalObj m_kingMoive;
    private PhysicalObj m_kingFront;

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
      Game.AddLoadingFile(2, "image/game/thing/bossborn6.swf", "game.asset.living.GuizeAsset");
      Game.AddLoadingFile(2, "image/game/effect/6/ball.swf", "asset.game.six.ball");
      Game.AddLoadingFile(2, "image/game/effect/6/jifenpai.swf", "asset.game.six.fenshu");
      Game.SetMap(1165);
    }

    public override void OnStartGame()
    {
      base.OnStartGame();

        //345,800
    }

    private void CreatBall()
    {
        int[] arrX = { 1199, 973, 842, 705, 971, 1110, 1240, 799, 662, 556, 572, 731, 926, 1106, 587, 1305, 775, 941, 1127, 675, 889, 1147, 462, 493, 846, 537, 771, 1009 };
        int[] arrY = { 812, 776, 718, 765, 617, 648, 574, 596, 624, 702, 496, 472, 495, 476, 345, 374, 332, 338, 313, 245, 196, 198, 585, 411, 860, 228, 127, 111};
        string[] actReds = { "s1", "s2", "s3", "s4", "s5", "double", "s1", "s2", "s3", "s4", "s5", "s1", "s2", "s3", "s4", "s5"};
        string[] actBlues = { "s-1", "s-2", "s-3", "s-4", "s-5", "s-1", "s-2", "s-3", "s-4", "s-5","s-1", "s-2", "s-3", "s-4", "s-5" };
        Point[] arrPoint = { new Point(1199, 812), new Point(973, 776), new Point(842, 718), new Point(705, 765), new Point(971, 617) };

        Game.Shuffer(arrX);
        Game.Shuffer(arrY);
        Game.Shuffer(arrPoint);
        for (int i = 0; i < arrPoint.Length; i++)
        {
            int actInd = Game.Random.Next(actBlues.Length);
            if (i == 3 || i == 7 || i == 13)
            {
                Game.CreateBall(arrPoint[i].X, arrPoint[i].Y, actBlues[actInd]);
            }
            else
            {
                actInd = Game.Random.Next(actReds.Length);
                Game.CreateBall(arrPoint[i].X, arrPoint[i].Y, actReds[actInd]);
            }
        }
        /*
        Game.CreateBall(900, 500, "s2");
        Game.CreateBall(1000, 500, "s3");
        Game.CreateBall(1100, 500, "s4");
        Game.CreateBall(1200, 500, "s5");
        Game.CreateBall(1200, 500, "s6");
        Game.CreateBall(800, 600, "s-1");
        Game.CreateBall(900, 600, "s-2");
        Game.CreateBall(1000, 600, "s-3");
        Game.CreateBall(1100, 600, "s-4");
        Game.CreateBall(1200, 600, "s-5");
        Game.CreateBall(1200, 600, "s-6");
        Game.CreateBall(1100, 700, "double");*/
    }

    public override void OnNewTurnStarted()
    {
      base.OnNewTurnStarted();
      Game.ClearBall();
      CreatBall();
    }

    public override void OnBeginNewTurn()
    {
      base.OnBeginNewTurn();
      if (Game.TurnIndex <= turn + 1)
        return;
      if (m_kingMoive != null)
      {
        Game.RemovePhysicalObj(m_kingMoive, true);
        m_kingMoive = (PhysicalObj) null;
      }
      if (m_kingFront != null)
      {
        Game.RemovePhysicalObj(m_kingFront, true);
        m_kingFront = (PhysicalObj) null;
      }
    }

    public override bool CanGameOver()
    {
      base.CanGameOver();
      return m_boss != null && !m_boss.IsLiving;
    }

    public override int UpdateUIData()
    {
      return base.UpdateUIData();
    }

    public override void OnGameOver()
    {
      base.OnGameOver();
      if (m_boss == null || m_boss.IsLiving)
        return;
      Game.IsWin = true;
    }
  }
}
