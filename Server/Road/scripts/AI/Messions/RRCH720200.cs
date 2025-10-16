using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic;
using Bussiness;
namespace GameServerScript.AI.Messions
{
    public class RRCH720200 : AMissionControl
    {
        private List<SimpleBoss> someBoss = new List<SimpleBoss>();

        private int bossID = 721100;

        private int bossID2 = 721100;

        private int bossID3 = 721100;

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
            int[] resources = { bossID, bossID2, bossID3 };
            int[] gameOverResource = { bossID };
            Game.LoadResources(resources);
            Game.LoadNpcGameOverResources(gameOverResource);
            Game.AddLoadingFile(1, "bombs/84.swf", "tank.resource.bombs.Bomb84");
            Game.AddLoadingFile(2, "image/game/effect/7/cao.swf", "asset.game.seven.cao");
            Game.SetMap(1162);
        }

        public override void OnStartGame()
        {
            base.OnStartGame();
            Game.IsBossWar = "7202";
            SimpleBoss boss = Game.CreateBoss(bossID, 1565, 787, -1, 1, "standA");
            someBoss.Add(boss);
            boss = Game.CreateBoss(bossID, 1583, 495, -1, 1, "standA");
            someBoss.Add(boss);
            boss = Game.CreateBoss(bossID, 1643, 236, -1, 1, "standA");
            someBoss.Add(boss);
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
            bool result = true;
            base.CanGameOver();
            kill = 0;
            foreach (SimpleBoss npc in someBoss)
            {
                if (npc.IsLiving)
                {
                    result = false;
                }
                else
                {
                    kill++;
                }
            }

            if (result && kill == Game.MissionInfo.TotalCount)
                return true;

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
            if (Game.GetLivedLivings().Count == 0)
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
