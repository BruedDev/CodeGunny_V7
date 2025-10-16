using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic;
using Bussiness;

namespace GameServerScript.AI.Messions
{
    public class DLH5203 : AMissionControl
    {
        private SimpleBoss boss = null;
        private SimpleBoss m_boss =  null;
        private SimpleBoss m_king =  null;
        private PhysicalObj m_door =  null;
        private int npcID = 5222;
        private int npcID2 = 5223;
        private int npcID3 = 5224;
        private int bossID = 5221;
        private int bossID2 = 5121;
        private int bossID3 = 5204;
        private int kill = 0;
        private int IsTrue = 0;
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
            int[] resources = { bossID, npcID, npcID2, npcID3 };
            int[] gameOverResource = { bossID };
            Game.LoadResources(resources);
            Game.LoadNpcGameOverResources(gameOverResource);
            Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.BossBgAsset");
            Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.hongpaoxiaoemoAsset");
            Game.AddLoadingFile(2, "image/game/effect/5/heip.swf", "asset.game.4.heip");
            Game.AddLoadingFile(2, "image/game/effect/5/lanhuo.swf", "asset.game.4.lanhuo");
            Game.AddLoadingFile(2, "image/game/living/living145.swf", "game.living.Living145");
            Game.AddLoadingFile(2, "image/game/living/living153.swf", "game.living.Living153");
            Game.SetMap(1153);
        }       

        public override void OnStartGame()
        {
            base.OnStartGame();
            m_moive = (PhysicalObj)Game.Createlayer(0, 0, "moive", "game.asset.living.BossBgAsset", "out", 1, 1);
            m_front = (PhysicalObj)Game.Createlayer(870, 450, "font", "game.asset.living.hongpaoxiaoemoAsset", "out", 1, 1);
            boss = Game.CreateBoss(bossID, 1000, 400, -1, 0, "");
            boss.SetRelateDemagemRect(-21, -79, 72, 51);
            boss.Say("Can đảm lấm bọn nhọc, địa ngục cũng dám đến ! Ha ha ha...", 0, 1000);
            m_moive.PlayMovie("in", 6000, 0);
            m_front.PlayMovie("in", 6100, 0);
            m_moive.PlayMovie("out", 10000, 1000);
            m_front.PlayMovie("out", 9900, 0);
            m_door = Game.CreatePhysicalObj(0, 0, "door", "", "start", 1, 0);
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
            if (boss != null && !boss.IsLiving && IsTrue == 0)
            {
                ++kill;
                Game.RemoveLiving(boss.Id);
                m_boss = Game.CreateBoss(bossID2, boss.X, boss.Y, boss.Direction, 0,"");
                m_boss.MoveTo(1000, 400, "fly", 3000, "", 10, new LivingCallBack(Testing2), 3000);
                IsTrue = 1;
            }
            if (m_boss != null && !m_boss.IsLiving && IsTrue == 1)
            {
                m_king = Game.CreateBoss(bossID3, 200, 550, 1, 0, "");
                m_king.PlayMovie("cool", 0, 0);
                IsTrue = 2;
                m_door.PlayMovie("end", 2000, 0);
            }
            return m_door.CurrentAction == "end";
        }
        private void Testing2()
        {
            if (m_boss.X != 1000 || m_boss.Y != 400)
                return;
            m_boss.ChangeDirection(1, 0);
            m_boss.PlayMovie("out", 2000, 0);
            m_boss.CallFuction(new LivingCallBack(Testing3), 5000);
        }
        private void Testing3()
        {
            m_boss.Die(0);
        }
        public override int UpdateUIData()
        {
            base.UpdateUIData();
            return kill;
        }

        public override void OnGameOver()
        {
            base.OnGameOver();
            if (m_door.CurrentAction == "end")
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
