using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic;
using Bussiness;

namespace GameServerScript.AI.Messions
{
    public class DCT4302 : AMissionControl
    {
        private SimpleBoss boss = null;
		
		private SimpleBoss m_king = null;

        private int bossID = 4305;

        private int bossID2 = 4306;

        private int kill = 0;
		
		protected int m_blood;

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
            int[] resources = { bossID, bossID2 };
            int[] gameOverResource = { bossID, bossID2 };
			Game.AddLoadingFile(2, "image/game/effect/4/feather.swf", "asset.game.4.feather");
            Game.LoadResources(resources);
            Game.LoadNpcGameOverResources(gameOverResource);
            Game.SetMap(1143);
        }

        public override void OnStartGame()
        {
            base.OnStartGame();
            boss = Game.CreateBoss(bossID2, 1380, 900, -1, 1, "");
            boss.FallFromTo(boss.X, boss.Y, null, 0, 0, 2000, null);
            boss.SetRelateDemagemRect(-41, -100, 83, 70);
            LivingConfig config = Game.BaseLivingConfig();
            config.IsFly = true;
            m_king = Game.CreateBoss(bossID, 189, 520, -1, 0, "", config);
            m_king.SetRelateDemagemRect(-41, -100, 50, 70);
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
            if (m_king != null && !m_king.IsLiving)
            {
                kill++;
                return true;
            }
            else
            {
                if (boss == null || boss.IsLiving)
                    return false;

                kill++;
                return true;
            }
        }

        public override int UpdateUIData()
        {
            base.UpdateUIData();
            return kill;
        }

        public override void OnGameOver()
        {
            base.OnGameOver();
            if (m_king != null && !m_king.IsLiving)
            {
                boss.PlayMovie("die", 1000, 1000);
                Game.IsWin = true;
            }
            if (boss != null && !boss.IsLiving)
            {
                m_king.PlayMovie("die", 1000, 1000);
                Game.IsWin = true;
            }
            else
                Game.IsWin = false;
        }

        public override void OnShooted()
        {
            base.OnShooted();
            //int num1 = boss.PlayerDame + boss.PetDame;
            //int num2 = m_king.PlayerDame + m_king.PetDame;
            //if (boss.UpdateBloodBoss > 0 || boss.UpdateBloodPet > 0)
            //    m_king.AddBlood(-num1, 1);
            //if (m_king.UpdateBloodBoss <= 0 && m_king.UpdateBloodPet <= 0)
            //    return;
            //boss.AddBlood(-num2, 1);
        }
    }
}
