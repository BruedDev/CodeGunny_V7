using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic.Effects;
using Game.Logic;
using Game.Logic.Actions;

namespace GameServerScript.AI.NPC
{
    public class FourTerrorWolfNpc : ABrain
    {
        private int m_attackTurn = 0;
        protected Player m_targer;
        private int m_run = 0;
        private int Dander = 0;
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
                Angger();
                m_attackTurn++;
            }
            else if (m_attackTurn == 1)
            {
                WalkA();
                Angger();
                m_attackTurn++;
            }
            else if (m_attackTurn == 2)
            {
                Jump();
                m_attackTurn++;
            }
            else if (m_attackTurn == 3)
            {
                WalkB();
                m_attackTurn++;
            }
            else
            {
                WalkB();
                m_attackTurn = 0;
            }
        }

        public override void OnStopAttacking()
        {
            base.OnStopAttacking();
        }

        public void Jump()
        {
            Player target = Game.FindRandomPlayer();
            //int mtX = Game.Random.Next(target.X - 0, target.X + 0);
            //int dis = Game.Random.Next(500, 700);
            Body.JumpToSpeed(target.X, target.Y, "jump", 1000, 1, 1000, new LivingCallBack(Fall));
            ((SimpleBoss)Body).SetRelateDemagemRect(-41, -100, 83, 70);
        }
        public void Fall()
        {
            Body.PlayMovie("fall", 1000, 0);
            Body.RangeAttacking(Body.X - 100, Body.X + 100, "cry", 1000, null);
            Body.Direction = Game.FindlivingbyDir(Body);
            Body.SetRelateDemagemRect(-41, -100, 83, 70);
        }
        private void WalkA()
        {
            Body.Direction = Game.FindlivingbyDir(Body);
            int dis = Game.Random.Next(100, 170);
            foreach (Player player in Game.GetAllLivingPlayers())
                Body.Direction = Game.FindlivingbyDir(Body);
            if (Body.X < 400)
                Body.MoveTo(Body.X + dis, Body.Y, "walkA", 1000, "", 4);
            else
                Body.MoveTo(Body.X - dis, Body.Y, "walkA", 1000, "", 4);
            Body.CallFuction(new LivingCallBack(AllAttack), 2600);
        }

        private void AllAttack()
        {
            ChangeDirection(1);
            Body.CurrentDamagePlus = 0.5f;
            Body.PlayMovie("beatA", 0, 0);
            ChangeDirection(3);
        }

        private void AllAttack2()
        {
            Body.Direction = Game.FindlivingbyDir(Body);
            Body.CurrentDamagePlus = 0.5f;
            Body.PlayMovie("beatB", 0, 0);
            Body.RangeAttacking(Body.X - 1000, Body.X + 1000, "cry", 1000, null);
            ((SimpleBoss)Body).SetRelateDemagemRect(-41, -100, 83, 70);

        }

        private void WalkB()
        {
            m_targer = Game.FindNearestPlayer(Body.X, Body.Y);
            if (m_run == 0)
            {
                Beat(m_targer);
                m_run = 1;
            }
            else
            {
                Beat2(m_targer);
                m_run = 0;
            }
        }

        private void Beat(Player player)
        {
            m_targer = Game.FindNearestPlayer(Body.X, Body.Y);
            int num = (int)((Physics)player).Distance(Body.X, Body.Y);
            if (num > 200)
            {
                if (player.X > Body.X)
                    Body.MoveTo(Body.X + num - 150, Body.Y, "walkB", 0, "", 12);
                else
                    Body.MoveTo(Body.X - num + 150, Body.Y, "walkB", 0, "", 12);
            }
            if (num < 200)
            {
                Body.PlayMovie("beatB", 2000, 0);
                Body.Direction = Game.FindlivingbyDir(Body);
                Body.CallFuction(new LivingCallBack(RangeAttacking), 2100);
            }
            else
            {
                Body.PlayMovie("beatB", num * 3 + 200, 0);
                Body.Direction = Game.FindlivingbyDir(Body);
                Body.CallFuction(new LivingCallBack(RangeAttacking), num * 4);
            }
        }

        private void Beat2(Player player)
        {
            int num = (int)((Physics)player).Distance(Body.X, Body.Y);
            m_targer = Game.FindNearestPlayer(Body.X, Body.Y);
            if (num > 200)
            {
                if (player.X > Body.X)
                    Body.MoveTo(Body.X + num - 150, Body.Y, "walkB", 0, "", 12);
                else
                    Body.MoveTo(Body.X - num + 150, Body.Y, "walkB", 0, "", 12);
            }
            if (num < 200)
            {
                Body.PlayMovie("beatB", 2000, 0);
                Body.Direction = Game.FindlivingbyDir(Body);
                Body.CallFuction(new LivingCallBack(RangeAttacking), 2100);
            }
            else
            {
                Body.PlayMovie("beatB", num * 3 + 200, 0);
                Body.Direction = Game.FindlivingbyDir(Body);
                Body.CallFuction(new LivingCallBack(RangeAttacking), num * 4);
            }
        }

        private void RangeAttacking()
        {
            Body.RangeAttacking(Body.X - 200, Body.X + 200, "", 0, null);
        }

        public void Angger()
        {
            Body.State = 1;
            Dander = Dander + 100;
            ((SimpleBoss)Body).SetDander(Dander);

            if (Body.Direction == -1)
            {
                ((SimpleBoss)Body).SetRelateDemagemRect(8, -252, 74, 50);
            }
            else
            {
                ((SimpleBoss)Body).SetRelateDemagemRect(-8, -252, 74, 50);
            }
        }

        private void ChangeDirection(int count)
        {
            int direction = Body.Direction;
            for (int i = 0; i < count; i++)
            {
                Body.ChangeDirection(-direction, i * 200 + 100);
                Body.ChangeDirection(direction, (i + 1) * 100 + i * 200);
            }
        }

    }
}