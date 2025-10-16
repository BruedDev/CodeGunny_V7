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
    public class DCT4303 : AMissionControl
    {
        private PhysicalObj m_kingMoive;

        private PhysicalObj m_kingFront;

        private SimpleBoss m_king = null;

        private SimpleBoss m_secondKing = null;

        private PhysicalObj[] m_leftWall = null;

        private PhysicalObj[] m_rightWall = null;

        private int m_kill = 0;

        private int m_state = 4308;

        private int turn = 0;

        private int firstBossID = 4308;

        private int secondBossID = 4309;
		
		private int npcID2 = 4310;

        private int npcID = 4307;
		
		private int IsSay = 0;

        private int direction;
		
		private static string[] KillChat = new string[]{
            "Mathias không kiểm soát tôi!",

            "Đây là thách thức số phận của tôi!",

            "Không! !Đây không phải là ý chí của tôi ..."
        };

        private static string[] ShootedChat = new string[]{
            "Tên nhóc kia, xem ta trừng trị mi!",
                   
            "Ta đỡ!!!"
        };

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
            Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.BossBgAsset");
            Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.emozhanshiAsset");
            Game.AddLoadingFile(2, "image/game/effect/4/power.swf", "game.crazytank.assetmap.Buff_powup");
			Game.AddLoadingFile(2, "image/game/effect/4/blade.swf", "asset.game.4.blade");
			Game.AddLoadingFile(2, "image/game/living/living141.swf", "game.living.Living141");
            int[] resources = { firstBossID, secondBossID, npcID, npcID2 };
            Game.LoadResources(resources);
            int[] gameOverResources = { firstBossID };
            Game.LoadNpcGameOverResources(gameOverResources);

            Game.SetMap(1144);
        }

        public override void OnStartGame()
        {
            base.OnStartGame();
			m_kingMoive = Game.Createlayer(0, 0, "kingmoive", "game.asset.living.BossBgAsset", "out", 1, 0);
            m_kingFront = Game.Createlayer(1100, 600, "font", "game.asset.living.emozhanshiAsset", "out", 1, 0);
            m_king = Game.CreateBoss(m_state, 1200, 790, -1, 1,"");
            m_king.FallFrom(m_king.X, m_king.Y, null, 0, 0, 5000, null);
            m_king.SetRelateDemagemRect(-41, -187, 83, 140);
            m_king.AddDelay(10);
            m_king.Say("Ngọn lửa câm giận<br/>đang sôi sục ...!", 0, 3500);
            m_king.PlayMovie("in", 0, 2500);
			m_kingMoive.PlayMovie("in", 9000, 0);
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
            if (Game.TurnIndex > turn + 1)
            {
                if (m_kingMoive != null)
                {
                    Game.RemovePhysicalObj(m_kingMoive, true);
                    m_kingMoive = null;
                }
                if (m_kingFront != null)
                {
                    Game.RemovePhysicalObj(m_kingFront, true);
                    m_kingFront = null;
                }
            }
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
                //m_kingMoive = Game.Createlayer(0, 0, "moive", "game.asset.living.BossBgAsset", "out", 1, 1, 0);
                m_secondKing = Game.CreateBoss(m_state, m_king.X, m_king.Y, m_king.Direction, 2,"");
                Game.RemoveLiving(m_king.Id);


                if (m_secondKing.Direction == 1)
                {
                    m_secondKing.SetRelateDemagemRect(-41, -187, 83, 140);

                }
                m_secondKing.SetRelateDemagemRect(-41, -187, 83, 140);

                m_secondKing.Say(LanguageMgr.GetTranslation("Chống cự chỉ là vô nghĩa !"), 0, 3000);
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
            
            m_leftWall = Game.FindPhysicalObjByName("wallLeft");
            m_rightWall = Game.FindPhysicalObjByName("wallRight");

            for (int i = 0; i < m_leftWall.Length; i++)
                Game.RemovePhysicalObj(m_leftWall[i], true);

            for (int i = 0; i < m_rightWall.Length; i++)
                Game.RemovePhysicalObj(m_rightWall[i], true);
        }
		
		public override void DoOther()
        {
            base.DoOther();
            if (m_king == null)
                return;
            if (m_king.IsLiving)
            {
                int index = Game.Random.Next(0, KillChat.Length);
                m_king.Say(KillChat[index], 0, 0);
            }
            else
            {
                int index = Game.Random.Next(0, KillChat.Length);
                m_king.Say(KillChat[index], 0, 0);
            }
        }

        public override void OnShooted()
        {
            if (IsSay == 0)
            {
                if (m_king.IsLiving)
                {
                    int index = Game.Random.Next(0, ShootedChat.Length);
                    m_king.Say(ShootedChat[index], 0, 1500);
                }
                else
                {
                    int index = Game.Random.Next(0, ShootedChat.Length);
                    m_secondKing.Say(ShootedChat[index], 0, 1500);
                }

                IsSay = 1;
            }
        }
    }
}
