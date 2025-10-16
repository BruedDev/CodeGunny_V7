using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic;
using Game.Logic.Actions;
using Bussiness;

namespace GameServerScript.AI.Messions
{
    public class DCT4301 : AMissionControl
    {
        private SimpleBoss m_boss = null;
        private int kill = 0;
        private PhysicalObj m_moive;
        private PhysicalObj m_front;
        private SimpleNpc npc;
        private int npcID2 = 4301;
		
		private int npcID = 4303;

        private int bossID = 4304;


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
            int[] resources = { bossID, npcID, npcID2 };
            int[] gameOverResource = {};
            Game.LoadResources(resources);
            Game.LoadNpcGameOverResources(gameOverResource);
			Game.AddLoadingFile(2, "image/game/effect/4/Gate.swf", "game.asset.Gate");
            Game.SetMap(1142);
        }

        public override void OnStartGame()
        {
            base.OnStartGame();
            m_boss = Game.CreateBoss(bossID, 1520, 350, -1, 1, "NoBlood");
            npc = Game.CreateNpc(npcID2, 340, 750, 1, 0);
            npc.FallFrom(npc.X, npc.Y, "", 0, 0, 2000);
            Game.CreatePhysicalObj(1500, 250, "door", "", "start", 1, 0);
            Game.SendGameObjectFocus(1, "door", 2000, 3000);
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
            if (npc == null || npc.IsLiving)
                return;
            npc = Game.CreateNpc(npcID2, 340, 750, 1, 0);
            npc.FallFrom(npc.X, npc.Y, "", 0, 0, 2000);
        }

        public override bool CanGameOver()
        {
            if (npc.FindCount != 3)
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
            if (npc.FindCount == 3)
                Game.IsWin = true;
            else
                Game.IsWin = false;
        }
    }
}
