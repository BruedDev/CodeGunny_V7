using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic;
using Game.Logic.Phy.Object;

namespace GameServerScript.AI.NPC
{
    public class FiveNormalSecondBoss : ABrain
    {
        private int m_attackTurn = 0;

        private int m_turn = 0;
		
		private PhysicalObj m_moive;

        private PhysicalObj m_wallLeft = null;

        private PhysicalObj m_wallRight = null;

        private int IsEixt = 0;

        private PhysicalObj m_NPC;
        private PhysicalObj n_NPC;

        #region NPC 说话内容
        private static string[] AllAttackChat = new string[] { 
             "要地震喽！！<br/>各位请扶好哦",
       
             "把你武器震下来！",
       
             "看你们能还经得起几下！！"
        };

        private static string[] ShootChat = new string[]{
             "让你知道什么叫百发百中！",
                               
             "送你一个球~你可要接好啦",

             "你们这群无知的低等庶民"
        };

        private static string[] ShootedChat = new string[]{
           "哎呀~~你们为什么要攻击我？<br/>我在干什么？",
                   
            "噢~~好痛!我为什么要战斗？<br/>我必须战斗…"

        };

        private static string[] KillPlayerChat = new string[]{
             "马迪亚斯不要再控制我！",       

             "这就是挑战我的下场！",

             "不！！这不是我的意愿… " 
        };

        private static string[] AddBooldChat = new string[]{
            "扭啊扭~<br/>扭啊扭~~",
               
            "哈利路亚~<br/>路亚路亚~~",
                
            "呀呀呀，<br/>好舒服啊！"
         
        };

        private static string[] KillAttackChat = new string[]{
            "君临天下！！"
        };

        private static string[] FrostChat = new string[]{
            "来尝尝这个吧",
               
            "让你冷静一下",
               
            "你们激怒了我"
              
        };

        private static string[] WallChat = new string[]{
             "神啊，赐予我力量吧！",

             "绝望吧，看我的水晶防护墙！"
         };

        #endregion

        public override void OnBeginSelfTurn()
        {
            base.OnBeginSelfTurn();
        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();
            m_body.CurrentDamagePlus = 1;
            m_body.CurrentShootMinus = 1;

        }

        public override void OnCreated()
        {
            base.OnCreated();
        }

        public override void OnStartAttacking()
        {
            base.OnStartAttacking();
            bool result = false;
            int maxdis = 0;
            foreach (Player player in Game.GetAllFightPlayers())
            {
                if (player.IsLiving && player.X > 1200 && player.X < 1984)
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
                KillAttack(1200, 1984);

                return;
            }

            if (m_attackTurn == 0)
            {
                m_NPC = ((PVEGame)Game).Createlayer(1550, 650, "NPC", "game.living.Living154", "stand", 1, 0);
                n_NPC = ((PVEGame)Game).Createlayer(1367, 845, "NPC", "game.living.Living147", "stand", 1, 0);        
                Goblinhunghan();
                m_attackTurn++;
            }
            else if (m_attackTurn == 1)
            {
                BeatA();
                m_attackTurn++;
            }
			else if (m_attackTurn == 2)
            {
                Goblinxaotra();
                m_attackTurn++;
            }
			else if (m_attackTurn == 3)
            {
                BeatB();
                m_attackTurn++;
            }
			else if (m_attackTurn == 4)
            {
                Goblinhunghan();
                m_attackTurn++;
            }
            else
            {
                BeatD();
                m_attackTurn = 0;
            }
        }

        private void BeatD()
        {
            Body.PlayMovie("beatC", 1000, 1000);
            Body.CallFuction(new LivingCallBack(NpcDame2), 3000);
        }

        private void NpcDame2()
        {
            if (n_NPC != null)
            {
                Game.RemovePhysicalObj(n_NPC, true);
                n_NPC = null;
            }
            n_NPC = ((PVEGame)Game).Createlayer(1367, 845, "NPC", "game.living.Living147", "beatA", 1, 0);
            ((PVEGame)Game).SendGameFocus(n_NPC, 0, 4000);
        }

        private void KillAttack(int fx, int tx)
        {
            int index = Game.Random.Next(0, KillAttackChat.Length);
            if (m_turn == 0)
            {
                Body.CurrentDamagePlus = 10;
                Body.Say(KillAttackChat[index], 1, 13000);
                Body.PlayMovie("beat1", 15000, 0);
                Body.RangeAttacking(fx, tx, "cry", 17000, null);
                m_turn++;
            }
            else
            {
                Body.CurrentDamagePlus = 10;
                Body.Say(KillAttackChat[index], 1, 0);
                Body.PlayMovie("beat1", 2000, 0);
                Body.RangeAttacking(fx, tx, "cry", 4000, null);
            }
        }
		
		private void Goblinhunghan()
        {
            int index = Game.Random.Next(0, AllAttackChat.Length);
            Body.Say(AllAttackChat[index], 1, 0);
            Body.PlayMovie("beatD", 1000, 1000);
        }

        private void BeatB()
        {
            Body.PlayMovie("beatB", 1000, 1000);
            Body.CallFuction(new LivingCallBack(NpcDame), 3000);
        }

        private void NpcDame()
        {
            if (m_NPC != null)
            {
                Game.RemovePhysicalObj(m_NPC, true);
                m_NPC = (PhysicalObj)null;
            }
            m_NPC = (PhysicalObj)((PVEGame)Game).Createlayer(1550, 650, "NPC", "game.living.Living154", "beatA", 1, 0);
            Body.CallFuction(new LivingCallBack(DameBlood), 4000);
        }

        private void DameBlood()
        {
            Body.CallFuction(new LivingCallBack(GoAtck), 1000);
        }

        private void GoAtck()
        {
            foreach (Player player in Game.GetAllFightPlayers())
            {
                int num = Game.Random.Next(321, 515);
                player.AddBlood(-num, 1);
                player.AddBlood(-num, 1);
                player.AddBlood(-num, 1);
                player.AddBlood(-num, 1);
                player.AddBlood(-num, 1);
                player.AddBlood(-num, 1);
            }
        }

		
		private void Goblinxaotra()
        {
            int index = Game.Random.Next(0, AllAttackChat.Length);
            Body.Say(AllAttackChat[index], 1, 0);
            Body.PlayMovie("beatC", 1000, 1000);
        }

        private void BeatA()
        {
            Body.PlayMovie("beatA", 1000, 4000);
            Body.CallFuction(new LivingCallBack(GoAttack), 3000);
        }

        private void GoAttack()
        {
            Player randomPlayer = Game.FindRandomPlayer();
            ((PVEGame)Game).SendGameFocus(randomPlayer, 0, 1500);
            int num = Game.Random.Next(321, 515);
            randomPlayer.AddBlood(-num, 1);
            m_moive = ((PVEGame)Game).Createlayer(randomPlayer.X, randomPlayer.Y, "wallLeft", "asset.game.4.xiaopao", "1", 1, 0);
        }
		
        public override void OnStopAttacking()
        {
            base.OnStopAttacking();
			if (m_moive != null)
                {
                    Game.RemovePhysicalObj(m_moive, true);
                    m_moive = null;
                }
        }
    }
}
