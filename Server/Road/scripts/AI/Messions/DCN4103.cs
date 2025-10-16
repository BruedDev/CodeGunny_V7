using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic;
using Bussiness;

namespace GameServerScript.AI.Messions
{
    public class DCN4103 : AMissionControl
    {
        private PhysicalObj m_kingMoive;

        private PhysicalObj m_kingFront;

        private SimpleBoss m_king = null;

        private SimpleBoss m_secondKing = null;

        private int m_kill = 0;

        private int m_state = 4108;

        private int turn = 0;

        private int firstBossID = 4108;

        private int secondBossID = 4109;

        private int npcID = 4107;

        private int direction;

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
            int[] resources = { firstBossID, secondBossID, npcID };
            Game.LoadResources(resources);
            int[] gameOverResources = { firstBossID };
            Game.LoadNpcGameOverResources(gameOverResources);
            Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.BossBgAsset");
            Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.emozhanshiAsset");
            Game.AddLoadingFile(2, "image/game/effect/4/power.swf", "game.crazytank.assetmap.Buff_powup");
            Game.AddLoadingFile(2, "image/game/effect/4/blade.swf", "asset.game.4.blade");
            Game.AddLoadingFile(2, "image/game/living/living141.swf", "game.living.Living141");
            Game.SetMap(1144);
        }

        public override void OnStartGame()
        {
            base.OnStartGame();
            m_kingMoive = Game.Createlayer(0, 0, "kingmoive", "game.asset.living.BossBgAsset", "out", 1, 0);
            m_kingFront = Game.Createlayer(1100, 600, "font", "game.asset.living.emozhanshiAsset", "out", 1, 0);
      
            m_king = Game.CreateBoss(m_state, 1200, 790, -1, 1,"");
            m_king.FallFromTo(m_king.X, m_king.Y, null, 0, 0, 2000, null);
            m_king.SetRelateDemagemRect(-41, -187, 83, 140);
            m_king.AddDelay(10);

            m_king.Say(LanguageMgr.GetTranslation("我的憎恨，被勞役在此的憎恨，被你們點燃了！"), 0, 200, 0);
            m_king.PlayMovie("in", 0, 2300);
            m_kingFront.PlayMovie("in", 9000, 0);
            m_kingMoive.PlayMovie("out", 13000, 0);
            m_kingFront.PlayMovie("out", 13400, 0);
            turn = Game.TurnIndex;

            Game.BossCardCount = 1;

        }


        public override void OnNewTurnStarted()
        {
            base.OnNewTurnStarted();
        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();            
        }

        public override bool CanGameOver()
        {
            base.CanGameOver();
            if (m_king.IsLiving == false)
            {
                if (m_state == firstBossID)
                {
                    m_state++;
                }
            }

            if (m_state == secondBossID && m_secondKing == null)
            {
                m_secondKing = Game.CreateBoss(m_state, m_king.X, m_king.Y, m_king.Direction, 2,"");
                Game.RemoveLiving(m_king.Id);


                if (m_secondKing.Direction == 1)
                {
                    m_secondKing.SetRelateDemagemRect(-41, -187, 83, 140);

                }
                m_secondKing.SetRelateDemagemRect(-41, -187, 83, 140);

                m_secondKing.Say(LanguageMgr.GetTranslation("你是不能阻止我了！"), 0, 3000);
                //m_kingMoive.PlayMovie("in", 5000, 0);
                //m_kingMoive.PlayMovie("out", 9000, 0);

                List<Player> players = Game.GetAllFightPlayers();
                Player RandomPlayer = Game.FindRandomPlayer();
                int minDelay = 0;

                if (RandomPlayer != null)
                {
                    minDelay = RandomPlayer.Delay;
                }

                foreach (Player player in players)
                {
                    if (player.Delay < minDelay)
                    {
                        minDelay = player.Delay;
                    }
                }

                m_secondKing.AddDelay(minDelay - 2000);
                turn = Game.TurnIndex;
            }

            if (m_secondKing != null && m_secondKing.IsLiving == false)
            {
                direction = m_secondKing.Direction;
                m_kill++;
                return true;
            }

            return false;
        }

        public override int UpdateUIData()
        {
            base.UpdateUIData();
            return m_kill;
        }

        //public override void OnPrepareGameOver()
        //{
        //    base.OnPrepareGameOver();


        //    /** 死亡倒播动画
        //   if (m_secondKing != null && m_secondKing.IsLiving == false)
        //   {
        //       PhysicalObj objKing = Game.CreatePhysicalObj(m_secondKing.X, m_secondKing.Y, "king", "game.living.LivingRecover005", "0", -direction, 1, 0);
        //       Game.RemoveLiving(m_secondKing.Id);
        //       objKing.PlayMovie("1", 0, 2000);
        //   }  
        //     * */
        //}

        public override void OnGameOver()
        {
            base.OnGameOver();
            if (m_state == secondBossID && m_secondKing.IsLiving == false)
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
