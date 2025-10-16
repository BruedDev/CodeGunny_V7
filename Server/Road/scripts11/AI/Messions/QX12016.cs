using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic;
using Game.Logic.Actions;
using Bussiness;
using Game.Server.Rooms;

namespace GameServerScript.AI.Messions
{
    public class QX12016 : AMissionControl
    {
        private SimpleBoss boss = null;
        private SimpleNpc npc = null;
        private int NpcID = 70099;
        private int bossID = 70001;
        private int bossID1 =70002;
        private int bossID2 =70003;
        private int bossID4 =70006;
        private int bossID5 =70007;
        private int bossID6 =70008;
        private int bossID7 =70009;
        private int bossID8 =70010;
        private int bossID9 = 70011;

        private int kill = 0;

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
            int[] resources = { bossID };
            int[] gameOverResource = { bossID };
            Game.LoadResources(resources);
            Game.LoadNpcGameOverResources(gameOverResource);
            Game.SetMap(1345);			
        }
        
        public override void OnStartGame()
        {
            base.OnStartGame();
            boss = Game.CreateBoss(bossID, 736, 793, -1, 1, "");
            boss.SetRelateDemagemRect(boss.NpcInfo.X, boss.NpcInfo.Y, boss.NpcInfo.Width, boss.NpcInfo.Height); 
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

        public override void OnGameOver()
        {
            base.OnGameOver();

            if (boss != null && boss.IsLiving == false)
            {
                Game.IsWin = true;
                Game.TakeSnow();
            }
            else
            {
                Game.IsWin = false;
            }
        }
    }
}