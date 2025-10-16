using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic;

namespace GameServerScript.AI.NPC
{
    public class FourNormalCattleBoss : ABrain
    {
        private int m_attackTurn = 0;

        private PhysicalObj m_moive;

        private int Dander = 0;

       

        private int npcID = 4107;

        #region NPC 说话内容
        private static string[] AllAttackChat = new string[] {
            "你们这是自寻死路！",

            "你惹毛我了!",

            "超级无敌大地震……<br/>震……震…… "
        };

        private static string[] ShootChat = new string[]{
            "砸你家玻璃。",

            "看哥打的可比你们准多了"
        };

        private static string[] KillPlayerChat = new string[]{
            "送你回老家！",

            "就凭你还妄想能够打败我？"
        };

        private static string[] CallChat = new string[]{
            "卫兵！ <br/>卫兵！！ ",
                  
            "啵咕们！！<br/>给我些帮助！"
        };

        private static string[] ShootedChat = new string[]{
            "哎呦！很痛…",

            "我还顶的住…"
        };

        private static string[] JumpChat = new string[]{
             "为了你们的胜利，<br/>向我开炮！",

             "你再往前半步我就把你给杀了！",

             "高！<br/>实在是高！"
        };

        private static string[] KillAttackChat = new string[]{
             "超级肉弹！！"
        };
        #endregion

        private List<SimpleNpc> m_child = new List<SimpleNpc>();
        public List<SimpleNpc> Child
        {
            get
            {
                return m_child;
            }
        }
        public int CurrentLivingNpcNum
        {
            get
            {
                int num = 0;
                foreach (Physics physics in Child)
                {
                    if (!physics.IsLiving)
                        ++num;
                }
                return Child.Count - num;
            }
        }

        public override void OnBeginSelfTurn()
        {
            base.OnBeginSelfTurn();
        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();

            Body.CurrentDamagePlus = 1;
            Body.CurrentShootMinus = 1;
            Body.SetRect(((SimpleBoss)Body).NpcInfo.X, ((SimpleBoss)Body).NpcInfo.Y, ((SimpleBoss)Body).NpcInfo.Width, ((SimpleBoss)Body).NpcInfo.Height);

            if (Body.Direction == -1)
            {
                Body.SetRect(((SimpleBoss)Body).NpcInfo.X, ((SimpleBoss)Body).NpcInfo.Y, ((SimpleBoss)Body).NpcInfo.Width, ((SimpleBoss)Body).NpcInfo.Height);
            }
            else
            {
                Body.SetRect(-((SimpleBoss)Body).NpcInfo.X - ((SimpleBoss)Body).NpcInfo.Width, ((SimpleBoss)Body).NpcInfo.Y, ((SimpleBoss)Body).NpcInfo.Width, ((SimpleBoss)Body).NpcInfo.Height);
            }

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
                if (player.IsLiving && player.X > 0 && player.X < 0)
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
                KillAttack(0, 0);

                return;
            }

           if (m_attackTurn == 1)
            {
                if (CurrentLivingNpcNum > 1)
                {
                    Body.AddBlood(15000);
                }
                AllAttack2();
                m_attackTurn++;
            }
            else if (m_attackTurn == 2)
            {
                PersonalAttack();
                m_attackTurn++;
            }
            else if (m_attackTurn == 3)
            {
                Jump();
                m_attackTurn++;
            }
			else
            {
                Physicallyinjured();
                m_attackTurn = 0;
            }
           
        }

        public override void OnStopAttacking()
        {
            base.OnStopAttacking();
        }
        private void Star()
        {
            int index = Game.Random.Next(0, FourNormalCattleBoss.CallChat.Length);
            Body.Say(FourNormalCattleBoss.CallChat[index], 1, 500);
            m_moive = ((PVEGame)Game).Createlayer(Body.X, Body.Y - 150, "moive", "game.crazytank.assetmap.Buff_powup", "", 1, 0);
            Body.CallFuction(new LivingCallBack(CreateChild), 2000);
        }
        private void Physicallboss()
        {
            m_moive = ((PVEGame)Game).Createlayer(Body.X, Body.Y - 150, "moive", "game.crazytank.assetmap.Buff_powup", "", 1, 0);
        }
        public void CreateChild()
        {
            //((SimpleBoss)Body).CreateBossFly(npcID, Game.Random.Next(100, 1000), 227, -1, 430, 3, "");
        }
		private void Physicallyinjured()
        {
            Body.CurrentDamagePlus = 0.5f;
            Body.PlayMovie("AtoB", 1000, 0);
        }
		
		private void AllAttack()
        {
            Body.CurrentDamagePlus = 0.5f;
			Body.Direction = Game.FindlivingbyDir(Body);
            Body.PlayMovie("beatA", 1000, 0);
            Body.RangeAttacking(Body.X - 1000, Body.X + 1000, "cry", 3000, null);
        }
		
		private void AllAttack2()
        {
            Body.CurrentDamagePlus = 0.5f;
            Body.PlayMovie("beatB", 2000, 0);
            Body.RangeAttacking(Body.X - 1000, Body.X + 1000, "cry", 3000, null);
        }
		
		private void Healing()
        {
            Body.PlayMovie("beatC", 500, 0);
            Body.AddBlood(5000);
        }

        public void Jump()
        {
			Body.PlayMovie("jump", 1000, 6000);	
			Player target = Game.FindRandomPlayer();
			Body.JumpToSpeed(target.X, Body.Y - 1000, "", 2500, 1, 10, new LivingCallBack(Jump2));
        }
		
		public void Jump2()
        {
			Body.PlayMovie("fall", 0, 0);	
			Body.RangeAttacking(Body.X - 2000, Body.X + 2000, "cry", 0, null);
        }
		
		private void PersonalAttack()
        {
            Body.MoveTo(Body.X - 100, Body.Y, "walk", 1000, "", 6, new LivingCallBack(AllAttack));
        }	
		
		public void KillAttack(int fx, int tx)
        {
            Body.CurrentDamagePlus = 10;
            int index = Game.Random.Next(0, KillAttackChat.Length);
            ((SimpleBoss)Body).Say(KillAttackChat[index], 1, 500);
            Body.PlayMovie("beatB", 2500, 0);
            Body.RangeAttacking(fx, tx, "cry", 3300, null);
        }

    }
}