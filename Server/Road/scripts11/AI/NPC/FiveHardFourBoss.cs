using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic;
using System.Drawing;
using Bussiness;


namespace GameServerScript.AI.NPC
{
    public class FiveHardFourBoss : ABrain
    {
        private int m_attackTurn = 0;

        private int npcID = 5232;

        private int isSay = 0;
		
		private int m_maxBlood;
        
		private int m_blood;
		
		private PhysicalObj m_moive;

        private PhysicalObj m_front;

        #region NPC 说话内容
        private static string[] AllAttackChat = new string[] {
            LanguageMgr.GetTranslation("GameServerScript.AI.NPC.NormalQueenAntAi.msg1"),

            LanguageMgr.GetTranslation("GameServerScript.AI.NPC.NormalQueenAntAi.msg2"),

            LanguageMgr.GetTranslation("GameServerScript.AI.NPC.NormalQueenAntAi.msg3")
        };

        private static string[] ShootChat = new string[]{
            LanguageMgr.GetTranslation("GameServerScript.AI.NPC.NormalQueenAntAi.msg4"),

            LanguageMgr.GetTranslation("GameServerScript.AI.NPC.NormalQueenAntAi.msg5")  
        };

        private static string[] KillPlayerChat = new string[]{
            LanguageMgr.GetTranslation("GameServerScript.AI.NPC.NormalQueenAntAi.msg6"),

            LanguageMgr.GetTranslation("GameServerScript.AI.NPC.NormalQueenAntAi.msg7")
        };

        private static string[] CallChat = new string[]{
            LanguageMgr.GetTranslation("GameServerScript.AI.NPC.NormalQueenAntAi.msg8"),

            LanguageMgr.GetTranslation("GameServerScript.AI.NPC.NormalQueenAntAi.msg9")

        };

        private static string[] JumpChat = new string[]{
             LanguageMgr.GetTranslation("GameServerScript.AI.NPC.NormalQueenAntAi.msg10"),

             LanguageMgr.GetTranslation("GameServerScript.AI.NPC.NormalQueenAntAi.msg11"),

             LanguageMgr.GetTranslation("GameServerScript.AI.NPC.NormalQueenAntAi.msg12")
        };

        private static string[] KillAttackChat = new string[]{
             LanguageMgr.GetTranslation("GameServerScript.AI.NPC.NormalQueenAntAi.msg13"),

              LanguageMgr.GetTranslation("GameServerScript.AI.NPC.NormalQueenAntAi.msg14")
        };

        private static string[] ShootedChat = new string[]{
            LanguageMgr.GetTranslation("GameServerScript.AI.NPC.NormalQueenAntAi.msg15"),

            LanguageMgr.GetTranslation("GameServerScript.AI.NPC.NormalQueenAntAi.msg16")

        };

        private static string[] DiedChat = new string[]{
            LanguageMgr.GetTranslation("GameServerScript.AI.NPC.NormalQueenAntAi.msg17")
        };

        #endregion

        public override void OnBeginSelfTurn()
        {
            base.OnBeginSelfTurn();
        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();

            Body.CurrentDamagePlus = 1;
            Body.CurrentShootMinus = 1;

            isSay = 0;
        }

        public override void OnCreated()
        {
            base.OnCreated();
        }

        public override void OnStartAttacking()
        {
            Body.Direction = Game.FindlivingbyDir(Body);
            bool result = false;
            int maxdis = 0;
            foreach (Player player in Game.GetAllFightPlayers())
            {
                if (player.IsLiving && player.X > 1500 && player.X < Game.Map.Info.ForegroundWidth + 1)
                {
                    int dis = (int)Body.Distance(player.X, player.Y);
                    if (dis > maxdis)
                    {
                        maxdis = dis;
                    }
                    result = true;
                }
            }

            if (result)
            {
                KillAttack(1500, Game.Map.Info.ForegroundWidth + 1);

                return;
            }

            if (m_attackTurn == 0)
            {
                BeatE();
                m_attackTurn++;
            }
			else if (m_attackTurn == 1)
            {
                AllAttack();
                m_attackTurn++;
            }
			else if (m_attackTurn == 2)
            {
                m_attackTurn++;
            }
			else if (m_attackTurn == 3)
            {
                AllAttack2();
                m_attackTurn++;
            }
			else if (m_attackTurn == 4)
            {
                Dame();
                m_attackTurn++;
            }
			else if (m_attackTurn == 5)
            {
                Summon();
                m_attackTurn++;
            }
			else if (m_attackTurn == 6)
            {
                AtoB();
                m_attackTurn++;
            }
			else if (m_attackTurn == 7)
            {
                AtoB();
                m_attackTurn++;
            }
			else if (m_attackTurn == 8)
            {
                Born();
                m_attackTurn++;
            }
            else
            {
                PersonalAttack();
                m_attackTurn = 0;
            }
        }

        public override void OnStopAttacking()
        {
            base.OnStopAttacking();
        }

        private void KillAttack(int fx, int tx)
        {
            int index = Game.Random.Next(0, KillAttackChat.Length);
            Body.Say(KillAttackChat[index], 1, 1000);
            Body.CurrentDamagePlus = 10;
            Body.PlayMovie("beatB", 3000, 0);
            Body.RangeAttacking(fx, tx, "cry", 5000, null);
        }
		
		private void Born()
        {
            Body.CurrentDamagePlus = 0.5f;
            Body.PlayMovie("born", 1000, 0);
            Body.RangeAttacking(Body.X - 1000, Body.X + 1000, "cry", 3000, null);
        }
		
		private void BeatE()
        {
            Body.CurrentDamagePlus = 0.5f;
            Body.PlayMovie("beatE", 1000, 0);
            Body.RangeAttacking(Body.X - 1000, Body.X + 1000, "cry", 3000, null);
        }
		
		private void AllAttack()
        {
            Body.PlayMovie("beatA", 1000, 3000);
        }
		
		private void AllAttack2()
        {
            Body.CurrentDamagePlus = 0.5f;
            Body.PlayMovie("beatB", 1000, 0);
            Body.RangeAttacking(Body.X - 1000, Body.X + 1000, "cry", 3000, null);
        }
		
		private void Dame()
        {
		
			
                Body.PlayMovie("beatD", 1000, 3000);	
                Body.RangeAttacking(Body.X - 1000, Body.X + 1000, "cry", 3000, null);		
                List<Player> players = Game.GetAllLivingPlayers();

                foreach (Player player in players)
                {
                    player.MoveTo(player.X - 400, Body.Y, "run", 0, "", 3);
					m_moive = ((PVEGame)Game).Createlayer(player.X, player.Y, "moive", "asset.game.4.tang", "out", 1, 0);     
                }			
			
        }
		
		private void PersonalAttack()
        {
            Body.PlayMovie("beatC", 3000, 5000);
			Body.CallFuction(new LivingCallBack(OnPersonalAttack), 3000);
        }
		
        private void OnPersonalAttack()
        {
            Player target = Game.FindRandomPlayer();

            if (target != null)
            {
                int mtX = Game.Random.Next(target.Y + 10, target.Y + 10);
				
                if (Body.Shoot(0, target.X, target.Y, 66, 66, 1, 2550)) 
                { 
					m_moive = ((PVEGame)Game).Createlayer(target.X, target.Y, "moive", "asset.game.4.guang", "out", 1, 0);
                }
            }
        }
		
		private void Summon()
        {
            Body.CurrentDamagePlus = 0.5f;
            Body.PlayMovie("beatE", 1000, 0);
            Body.RangeAttacking(Body.X - 1000, Body.X + 1000, "cry", 3000, null);
            Body.CallFuction(new LivingCallBack(Call), 4000);

        }

        private void AtoB()
        {
            Body.PlayMovie("AtoB", 1700, 2000);
        }

        public void Call()
        {
            ((SimpleBoss)Body).CreateChild(npcID, 1000, 530, 430, 1, -1);
        }
    }
}
