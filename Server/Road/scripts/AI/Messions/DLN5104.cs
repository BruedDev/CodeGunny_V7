using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic;

namespace GameServerScript.AI.Messions
{
    public class DLN5104 : AMissionControl
    {
        private SimpleBoss m_king = null;

        private SimpleBoss king = null;

        SimpleBoss m_preKing = null;

        private PhysicalObj m_kingMoive;

        private PhysicalObj kingMoive;

        private PhysicalObj m_kingFront;

        private int turn = 0;

        private int m_kill = 0;

        private int IsSay = 0;

        private int bossID = 5131;

        private int bossID2 = 5133;

        private int npcID = 5132;

        private int npcID2 = 5134;

        private static string[] KillChat = new string[]{
           "Địa ngục là điểm đến duy nhất của bạn!",                  
 
            "Quá dễ bị tổn thương."
        };

        private static string[] ShootedChat = new string[]{
            "Oh ~ bạn chơi tốt một điều đau khổ!<br/>Ah ha ha ha ha!",
               
            "Bạn sẽ chỉ có khả năng này? !",
               
            "Có một chút có nghĩa là"
        };

        public override int CalculateScoreGrade(int score)
        {
            base.CalculateScoreGrade(score);
            if (score > 900)
            {
                return 3;
            }
            else if (score > 825)
            {
                return 2;
            }
            else if (score > 725)
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
            int[] resources = { npcID, npcID2, bossID, bossID2 };
            Game.LoadResources(resources);
            Game.LoadNpcGameOverResources(resources);
            Game.AddLoadingFile(1, "bombs/56.swf", "tank.resource.bombs.Bomb56");
            Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.BossBgAsset");
            Game.AddLoadingFile(2, "image/game/effect/5/guang.swf", "asset.game.4.guang");
            Game.AddLoadingFile(2, "image/game/effect/5/tang.swf", "asset.game.4.tang");
            Game.AddLoadingFile(2, "image/game/effect/5/ruodian.swf", "asset.game.4.ruodian");
            Game.AddLoadingFile(2, "image/game/effect/5/jinqudan.swf", "asset.game.4.jinqudan");
            Game.AddLoadingFile(2, "image/game/thing/BossBornBgAsset.swf", "game.asset.living.xieyanjulongAsset");
            Game.AddLoadingFile(2, "image/game/living/living156.swf", "game.living.Living156");
            Game.SetMap(1154);
        }

        public override void OnStartGame()
        {
            base.OnStartGame();
            Game.IsBossWar = "5131";
            m_kingMoive = Game.Createlayer(0, 0, "kingmoive", "game.asset.living.BossBgAsset", "out", 1, 0);
            m_kingFront = Game.Createlayer(1300, 280, "font", "game.asset.living.xieyanjulongAsset", "out", 1, 0);
            LivingConfig config = Game.BaseLivingConfig();
            config.IsFly = true;
            m_king = Game.CreateBoss(bossID, 1702, 470, -1, 0, "", config);
            king = Game.CreateBoss(bossID2, 200, 200, 1, 0, "");
            m_king.SetRelateDemagemRect(-100, -79, 172, 100);
            king.Say("Có ta ở đây đừng sợ!", 3000, 0);
            m_kingMoive.PlayMovie("in", 9000, 0);
            m_kingFront.PlayMovie("in", 9000, 0);
            m_kingMoive.PlayMovie("out", 10000, 0);
            m_kingFront.PlayMovie("out", 10400, 0);
            turn = Game.TurnIndex;
        }

        public override void OnNewTurnStarted()
        {
            base.OnNewTurnStarted();
        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();
            IsSay = 0;
            kingMoive = Game.Createlayer(1710, 480, "kingmoive", "asset.game.4.ruodian", "out", 1, 0);
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
                if (kingMoive != null)
                {
                    Game.RemovePhysicalObj(kingMoive, true);
                    kingMoive = null;
                }
            }
        }

        public override bool CanGameOver()
        {

            if (m_king.IsLiving == false)
            {
                m_kill++;
                return true;
            }

            if (Game.TurnIndex > Game.MissionInfo.TotalTurn - 1)
            {
                return true;
            }

            return false;

        }

        public override int UpdateUIData()
        {
            return m_kill;
        }

        public override void OnGameOver()
        {
            base.OnGameOver();
            bool IsAllPlayerDie = true;
            foreach (Player player in Game.GetAllFightPlayers())
            {
                if (player.IsLiving == true)
                {
                    IsAllPlayerDie = false;
                }
            }
            if (m_king.IsLiving == false && IsAllPlayerDie == false)
            {
                Game.IsWin = true;
            }
            else
            {
                Game.IsWin = false;
            }

            //List<LoadingFileInfo> loadingFileInfos = new List<LoadingFileInfo>();
            //loadingFileInfos.Add(new LoadingFileInfo(2, "image/map/8", ""));
            //Game.SendLoadResource(loadingFileInfos);
        }

        public override void DoOther()
        {
            base.DoOther();

            int index = Game.Random.Next(0, KillChat.Length);
            m_king.Say(KillChat[index], 0, 0);
        }

        public override void OnShooted()
        {
            if (m_king.IsLiving && IsSay == 0)
            {
                int index = Game.Random.Next(0, ShootedChat.Length);
                m_king.Say(ShootedChat[index], 0, 1500);
                IsSay = 1;
            }

        }
    }
}
