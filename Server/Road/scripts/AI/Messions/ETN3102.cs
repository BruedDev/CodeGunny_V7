using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic;
using Bussiness;
namespace GameServerScript.AI.Messions
{
    public class ETN3102 : AMissionControl
    {
        private List<SimpleNpc> someNpc = new List<SimpleNpc>();

        private SimpleBoss boss = null;

        private SimpleBoss m_king = null;

        protected int m_maxBlood;

        protected int m_blood;

        private int npcID = 3102;

        private int npcID1 = 3107;

        private int npcID2 = 3105;

        private int bossId = 3108;

        private int kill = 0;

        private SimpleBoss m_boss = null;

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
            int[] resources = { npcID, npcID1, npcID2, bossId };
            int[] gameOverResource = { npcID, npcID1, npcID2, bossId };
            Game.LoadResources(resources);
            Game.LoadNpcGameOverResources(gameOverResource);
            Game.AddLoadingFile(1, "bombs/58.swf", "tank.resource.bombs.Bomb58");
            Game.SetMap(1123);
        }

        public override void OnStartGame()
        {
            base.OnStartGame();
            Game.IsBossWar = "3102";
            m_king = Game.CreateBoss(bossId, 100, 444, 1, 1,"");
            m_king.FallFrom(m_king.X, m_king.Y, "", 0, 0, 2000);
            m_king.PlayMovie("castA", 500, 0);
            m_king.CallFuction(new LivingCallBack(CreateStarGame), 2500);

        }

        public void CreateStarGame()
        {
            LivingConfig config = Game.BaseLivingConfig();
            config.IsHelper = true;
            config.ReduceBloodStart = 2;
            boss = Game.CreateBoss(npcID1, 1100, 444, -1, 10,"", config);
            boss.FallFrom(boss.X, boss.Y, "", 0, 0, 1000, (LivingCallBack)null);

            m_boss = Game.CreateBoss(npcID2, 300, 444, 1, 0, "");
            m_boss.FallFrom(m_boss.X, m_boss.Y, "", 0, 1, 1000, (LivingCallBack)null);
            someNpc.Add(Game.CreateNpc(npcID, 450, 344, 1, 1));
            someNpc.Add(Game.CreateNpc(npcID, 400, 344, 1, 1));
            someNpc.Add(Game.CreateNpc(npcID, 350, 344, 1, 1));

            Game.SendGameFocus(boss, 500, 3000);
            boss.Say(LanguageMgr.GetTranslation("Hồi máu cho tôi, tôi sẻ dẫn các cậu ra khỏi đây !"), 0, 1500, 0);
        }


        public override void OnNewTurnStarted()
        {
            base.OnNewTurnStarted();
            if (m_boss == null || m_boss.IsLiving)
                return;
            m_boss = Game.CreateBoss(npcID2, 300, 444, 1, 1,"");
            m_boss.FallFrom(m_boss.X, m_boss.Y, "", 0, 0, 1000, null);
            someNpc.Add(Game.CreateNpc(npcID, 450, 344, 1, 1));
            someNpc.Add(Game.CreateNpc(npcID, 400, 344, 1, 1));
            someNpc.Add(Game.CreateNpc(npcID, 350, 344, 1, 1));
        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();
            if (Game.TurnIndex != 1)
                return;
            if (m_king != null && m_king.IsLiving)
            {
                m_king.PlayMovie("out", 0, 2000);
                m_king.CallFuction(new LivingCallBack(CreateOutGame), 1200);
            }
        }
        public void CreateOutGame()
        {
            Game.RemoveLiving(m_king.Id);
            m_king.Die();
        }
        public override bool CanGameOver()
        {
            base.CanGameOver();
            if (boss.Blood == boss.NpcInfo.Blood)
                return true;
            if (boss == null || boss.IsLiving)
                return false;
            kill++;
            return true;
        }

        public override int UpdateUIData()
        {
            base.UpdateUIData();
            return kill;
        }       

        public override void OnGameOver()
        {
            base.OnGameOver();
            if (boss.Blood == boss.NpcInfo.Blood)
            {
                boss.PlayMovie("grow", 0, 1000);
                Game.IsWin = true;
            }
            else
            {
                Game.IsWin = false;
            }

            List<LoadingFileInfo> loadingFileInfos = new List<LoadingFileInfo>();
            loadingFileInfos.Add(new LoadingFileInfo(2, "image/map/show3.jpg", ""));
            Game.SendLoadResource(loadingFileInfos);
        }
    }
}
