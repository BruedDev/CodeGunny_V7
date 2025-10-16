using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic;
using Bussiness;

namespace GameServerScript.AI.Messions
{
    public class wc2 : AMissionControl
    {
        private SimpleBoss boss = null;

        private int npcID = 25001;

        private int bossID = 80005;

        private int kill = 0;

        private PhysicalObj m_moive;

        private PhysicalObj m_front;

        public override int CalculateScoreGrade(int score)
        {
            base.CalculateScoreGrade(score);
            if (score > 1750)
            {
                return 3;
            }
            else if (score > 1675)
            {
                return 2;
            }
            else if (score > 1600)
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
            int[] resources = { bossID, npcID };
            int[] gameOverResource = { bossID };
            Game.LoadResources(resources);
            Game.LoadNpcGameOverResources(gameOverResource);
            Game.AddLoadingFile(2, "image/bomb/blastOut/blastOut51.swf", "shootMovie51");
            Game.AddLoadingFile(2, "image/bomb/bullet/bullet51.swf", "bullet51");
            //Game.AddLoadingFile(1, "bombs/51.swf", "tank.resource.bombs.Bomb51");
            Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.BossBgAsset");
            Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.AntQueenAsset");
            Game.SetMap(1313);
        }
        //public override void OnPrepareStartGame()
        //{
        //    base.OnPrepareStartGame();
        //}

        //public override void OnStartGame()
        //{
        //    base.OnStartGame();

        //}

        public override void OnStartGame()
        {
            base.OnStartGame();
            Game.IsBossWar = "1000002";
            m_moive = Game.Createlayer(0, 0, "moive", "game.asset.living.BossBgAsset", "out", 1, 1);
            m_front = Game.Createlayer(1131, 150, "font", "game.asset.living.AntQueenAsset", "out", 1, 1);
            boss = Game.CreateBoss(bossID, 807, 335, -1, 1, "");
            boss.SetRelateDemagemRect(-42, -200, 84, 194);
            boss.Say(LanguageMgr.GetTranslation("Chào Mừng Bạn Đến Với Chúng Tôi - Chúc Bạn Chơi Game Vui Vẻ"), 0, 200, 0);           
            m_moive.PlayMovie("in", 6000, 0);
            m_front.PlayMovie("in", 6100, 0);
            m_moive.PlayMovie("out", 10000, 1000);
            m_front.PlayMovie("out", 9900, 0);
            
        }

        public override void OnNewTurnStarted()
        {
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
        }

        public override bool CanGameOver()
        {
            if (boss != null && boss.IsLiving == false)
            {
                kill++;
                return true;
            }
            return false;
        }

        public override int UpdateUIData()
        {
            base.UpdateUIData();
            return kill;
        }

        //public override void OnPrepareGameOver()
        //{
        //    base.OnPrepareGameOver();
        //}

        public override void OnGameOver()
        {
            base.OnGameOver();
            if (boss != null && boss.IsLiving == false)
            {
                Game.IsWin = true;
            }
            else
            {
                Game.IsWin = false;
            }
        }
    }
}
