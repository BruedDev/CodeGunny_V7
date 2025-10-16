using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic;
using Bussiness;

namespace GameServerScript.AI.Messions
{
    public class ETN3104 : AMissionControl
    {
        private PhysicalObj m_kingMoive;

        private PhysicalObj m_kingFront;

        private SimpleBoss m_king = null;

        private SimpleBoss king = null;

        private SimpleBoss m_secondKing = null;

        private PhysicalObj[] m_leftWall = null;

        private PhysicalObj[] m_rightWall = null;

        private int IsSay = 0;

        private int m_kill = 0;

        private int m_state = 3116;

        private int turn = 0;

        private int firstBossID = 3116;

        private int secondBossID = 3117;

        private int npcID = 3103;

        private int npcID3 = 3118;

        private int npcID2 = 3112;

        private int npcID1 = 3113;

        private int direction;

        private static string[] ShootedChat = new string[]{
            "Ta dận rồi nha!!",

            "Yếu, quá yếu...",

            "Ta né, ta né, hãy...",
			
			"Hỡi thần thánh, trợ giúp cho ta..."
        };

        private static string[] ShootedChatSecond = new string[]{
            "Thân thể yếu ớt này mà các nguwoi không làm gì được!",

            "Yếu, quá yếu...",

            "Chọc dận ta à?...",
			
			"Dựa vào các nguwoi mà muốn cản nghi lễ của ta..."
        };

        private static string[] KillChat = new string[]{
            "Ngươi muốn làm gì đây?<br/>A...."
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
            Game.AddLoadingFile(1, "bombs/55.swf", "tank.resource.bombs.Bomb55");
            Game.AddLoadingFile(1, "bombs/54.swf", "tank.resource.bombs.Bomb54");
            Game.AddLoadingFile(1, "bombs/53.swf", "tank.resource.bombs.Bomb53");
            Game.AddLoadingFile(2, "image/game/effect/3/flame.swf", "asset.game.4.flame");
            Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.BossBgAsset");
            Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.ClanLeaderAsset");

            int[] resources = { firstBossID, secondBossID, npcID, npcID2, npcID1, npcID3 };
            Game.LoadResources(resources);
            int[] gameOverResources = { firstBossID };
            Game.LoadNpcGameOverResources(gameOverResources);

            Game.SetMap(1126);
            //Game.IsBossWar = LanguageMgr.GetTranslation("GameServerScript.AI.Messions.CHM1376.msg1");
        }

        public override void OnStartGame()
        {
            base.OnStartGame();
            m_kingMoive = Game.Createlayer(0, 0, "kingmoive", "game.asset.living.BossBgAsset", "out", 1, 1);
            m_kingFront = Game.Createlayer(700, 355, "font", "game.asset.living.ClanLeaderAsset", "out", 1, 1);
            m_king = Game.CreateBoss(m_state, 800, 400, -1, 1, "");

            m_king.FallFrom(800, 0, "fall", 0, 2, 1000, null);
            m_king.SetRelateDemagemRect(m_king.NpcInfo.X, m_king.NpcInfo.Y, m_king.NpcInfo.Width, m_king.NpcInfo.Height);
            m_king.AddDelay(10);

            m_king.Say("Đến đây thôi, dám ngăn cản nghi lễ của ta, không muốn sống à!", 0, 4000);
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
                m_secondKing = Game.CreateBoss(m_state, m_king.X, m_king.Y, m_king.Direction, 2,"");
                king = Game.CreateBoss(npcID3, 478, 560, -1, 0, "");
                Game.RemoveLiving(m_king.Id);


                if (m_secondKing.Direction == 1)
                {
                    m_secondKing.SetRelateDemagemRect(m_king.NpcInfo.X, m_king.NpcInfo.Y, m_king.NpcInfo.Width, m_king.NpcInfo.Height);

                }
                m_secondKing.SetRelateDemagemRect(m_king.NpcInfo.X, m_king.NpcInfo.Y, m_king.NpcInfo.Width, m_king.NpcInfo.Height);

                m_secondKing.Say(LanguageMgr.GetTranslation("Thể xác ốm yếu này, đưa ta mượn tạm xem!"), 0, 3000);

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

            List<LoadingFileInfo> loadingFileInfos = new List<LoadingFileInfo>();
            loadingFileInfos.Add(new LoadingFileInfo(2, "image/map/show7.jpg", ""));
            Game.SendLoadResource(loadingFileInfos);

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
           
        }       
    }
}
