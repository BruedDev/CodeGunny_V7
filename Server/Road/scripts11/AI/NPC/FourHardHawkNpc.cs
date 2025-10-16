using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic;

namespace GameServerScript.AI.NPC
{
    public class FourHardHawkNpc : ABrain
    {
        private int m_attackTurn = 0;

        private int Dander = 0;		
		
		private PhysicalObj m_moive;

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

            if (m_attackTurn == 0)
            {
                WalkA();
                m_attackTurn++;
            }
            else if (m_attackTurn == 1)
            {
                WalkA();
                m_attackTurn++;

            }
            else if (m_attackTurn == 2)
            {
                CallBoss();
                m_attackTurn++;
            }
            else if (m_attackTurn == 3)
            {
                CallBoss();
                m_attackTurn++;
            }
            else
            {
                WalkA();
                m_attackTurn = 0;
            }
        }

        public override void OnStopAttacking()
        {
            base.OnStopAttacking();
        }

        private void WalkA()
        {
            int dis = Game.Random.Next(200, 1389);
            Body.MoveTo(dis, Body.Y, "fly", 3000, "", 12, new LivingCallBack(AllAttack));
        }

        private void AllAttack()
        {
            Body.PlayMovie("beatA", 2000, 0);
            Body.Direction = Game.FindlivingbyDir(Body);
            Body.CallFuction(new LivingCallBack(CreateFeather), 3300);
        }

        private void AllAttack2()
        {
            Body.PlayMovie("beatA", 2000, 0);
            Body.Direction = Game.FindlivingbyDir(Body);
            Body.CallFuction(new LivingCallBack(CreateFeather), 3300);
        }

        private void CallBoss()
        {
            Body.Direction = Game.FindlivingbyDir(Body);
            int index = Game.Random.Next(0, AllAttackChat.Length);
            Body.Say(AllAttackChat[index], 1, 1000);
            Body.PlayMovie("cry", 2000, 0);
        }

        public void CreateFeather()
        {
            Body.CurrentDamagePlus = 0.5f;
            Body.RangeAttacking(Body.X - 1000, Body.X + 1000, "cry", 1000, null);
            Player target = Game.FindRandomPlayer();
            m_moive = ((PVEGame)Game).Createlayer(target.X, target.Y, "moive", "asset.game.4.feather", "out", 1, 0);
        }
    }
}