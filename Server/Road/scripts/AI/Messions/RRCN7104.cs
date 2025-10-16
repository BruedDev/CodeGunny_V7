using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic;
using SqlDataProvider.Data;
using Bussiness;

namespace GameServerScript.AI.Messions
{
    public class RRCN7104 : AMissionControl
    {
        private PhysicalObj m_moive;

        private PhysicalObj m_front;

        private PhysicalObj m_eggs = null;

        private PhysicalObj m_out = null;

        private SimpleBoss cage = null;

        private SimpleBoss boss = null;

        private PhysicalObj[] m_leftWall = null;

        private PhysicalObj[] m_rightWall = null;

        private List<SimpleNpc> someNpc = new List<SimpleNpc>();

        private int m_kill = 0;

        private int turn = 0;

        private int bossID1 = 7131;

        private int bossID2 = 7132;

        private int npcID2 = 7133;

        public override int CalculateScoreGrade(int score)
        {
            base.CalculateScoreGrade(score);
            if (score > 1150)
            {
                return 3;
            }
            else if (score > 925)
            {
                return 2;
            }
            else if (score > 700)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public override void OnPrepareNewSession()
        {
            base.OnPrepareNewSession();
            Game.AddLoadingFile(1, "bombs/83.swf", "tank.resource.bombs.Bomb83");
            Game.AddLoadingFile(1, "bombs/84.swf", "tank.resource.bombs.Bomb84");
            Game.AddLoadingFile(2, "image/map/1076/objects/1076MapAsset.swf", "com.mapobject.asset.WaveAsset_01_left");
            Game.AddLoadingFile(2, "image/map/1076/objects/1076MapAsset.swf", "com.mapobject.asset.WaveAsset_01_right");
            Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.BossBgAsset");
            Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.choudanbenbenAsset");
            Game.AddLoadingFile(2, "image/game/living/living177.swf", "game.living.Living177");
            Game.AddLoadingFile(2, "image/game/effect/7/choud.swf", "asset.game.seven.choud");
            Game.AddLoadingFile(2, "image/game/effect/7/jinqucd.swf", "asset.game.seven.jinqucd");
            Game.AddLoadingFile(2, "image/game/effect/7/du.swf", "asset.game.seven.du");

            int[] resources = { bossID1, npcID2, bossID2 };
            Game.LoadResources(resources);
            int[] gameOverResources = { bossID1 };
            Game.LoadNpcGameOverResources(gameOverResources);

            Game.SetMap(1164);
            //Game.IsBossWar = "Gà mái xấu xí";
        }

        public override void OnStartGame()
        {
            base.OnStartGame();
            m_moive = Game.Createlayer(0, 0, "kingmoive", "game.asset.living.BossBgAsset", "out", 1, 1);
            m_front = Game.Createlayer(300, 595, "font", "game.asset.living.choudanbenbenAsset", "out", 1, 1);
            m_eggs = ((PVEGame)Game).CreatePhysicalObj(2070, 633, "eggs", "game.living.Living178", "in", 1, 0);
            cage = Game.CreateBoss(bossID2, 1920, 920, -1, 0, "stand");
            cage.SetRelateDemagemRect(cage.NpcInfo.X, cage.NpcInfo.Y, cage.NpcInfo.Width, cage.NpcInfo.Height);
            boss = Game.CreateBoss(bossID1, 181, 875, 1, 1, "");
            boss.SetRelateDemagemRect(boss.NpcInfo.X, boss.NpcInfo.Y, boss.NpcInfo.Width, boss.NpcInfo.Height);
            boss.Say(LanguageMgr.GetTranslation("既然你們進來，就別走了。把你們關進籠子給我感染吧！"), 0, 4000);
            m_moive.PlayMovie("in", 9000, 0);
            m_front.PlayMovie("in", 9000, 0);
            m_moive.PlayMovie("out", 13000, 0);
            m_front.PlayMovie("out", 13400, 0);
            //turn = Game.TurnIndex;
            //Game.BossCardCount = 1;

        }


        public override void OnNewTurnStarted()
        {
            base.OnNewTurnStarted();
            if (Game.TurnIndex > 1)
            {
                cage.AddDelay(-200);
            }
            int maxDelay = 0;
            List<Player> players = Game.GetAllFightPlayers();
            foreach (Player player in players)
            {
                if (player.Delay < maxDelay)
                {
                    maxDelay = player.Delay;
                }
            }
            cage.AddDelay(maxDelay + 200);
        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();
            if (Game.TurnIndex > 1)
            {
                if (m_moive != null)
                {
                    Game.RemovePhysicalObj(m_moive, true);
                    m_moive = null;
                }
                if (m_front != null)
                {
                    Game.RemovePhysicalObj(m_front, true);
                    m_front = null;
                }
            }
            if (Game.TurnIndex == 1)
            {
                cage.PlayMovie("standB", 1000, 0);
                cage.Say(LanguageMgr.GetTranslation("我們不想給他感染。救命啊！"), 0, 1000);
            }
        }

        public override bool CanGameOver()
        {
            base.CanGameOver();
            if (boss != null && !boss.IsLiving)
            {
                cage.PlayMovie("out", 1000, 0);
                cage.SetRelateDemagemRect(-144, cage.NpcInfo.Y, cage.NpcInfo.Width, cage.NpcInfo.Height);
                int minDelay = 0;
                List<Player> players = Game.GetAllFightPlayers();
                foreach (Player player in players)
                {
                    if (player.Delay < minDelay)
                    {
                        minDelay = player.Delay;
                    }
                }
                cage.AddDelay(minDelay - 200);
            }
            if (cage != null && !cage.IsLiving)
            {
                List<Player> players = Game.GetAllFightPlayers();
                Game.RemoveLiving(cage.Id);
                m_out = ((PVEGame)Game).CreatePhysicalObj(1920, 947, "movie", "game.living.Living177", "die", 1, 0);
                //m_out.PlayMovie("win", 700, 0);
                return true;
            }
            return false;
        }

        public override int UpdateUIData()
        {
            base.UpdateUIData();
            return m_kill;
        }

        public override void OnGameOver()
        {
            base.OnGameOver();
            if (cage != null && !cage.IsLiving)
            {
                Game.IsWin = true;
            }
            else
            {
                Game.IsWin = false;
            }
            m_leftWall = Game.FindPhysicalObjByName("wallLeft");
            m_rightWall = Game.FindPhysicalObjByName("wallRight");

            for (int i = 0; i < m_leftWall.Length; i++)
                Game.RemovePhysicalObj(m_leftWall[i], true);

            for (int i = 0; i < m_rightWall.Length; i++)
                Game.RemovePhysicalObj(m_rightWall[i], true);
        }
    }
}
