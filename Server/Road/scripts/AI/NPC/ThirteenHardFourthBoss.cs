using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic.Effects;

namespace GameServerScript.AI.NPC
{
    public class ThirteenHardFourthBoss : ABrain
    {
        public int attackingTurn = 0;

        Player target = null;

        private PhysicalObj m_moive;

        private PhysicalObj m_front;

        private static string[] AllAttackChat = new string[] {

             "你會為此付出代價的！ "
        };

        private static string[] ShootChat = new string[]{
               
             "打死你根本就是浪費我的力量！",
             
             "呵呵，你打得我很疼啊！ <br/>哈哈哈哈!",
               
             "絕招吧！"          
        };

        private static string[] CallChat = new string[]{
            "這些垃圾 <br/>轟！"                          
        };

        private static string[] AngryChat = new string[]{
            "終極的力量！"                          
        };

        private static string[] KillAttackChat = new string[]{
            "你是來找死？"                          
        };

        private static string[] SealChat = new string[]{
            "擁抱帽子到了極點！"                          
        };

        private static string[] KillPlayerChat = new string[]{
            "到底是獨一無二的你！",                  
 
            "這是太容易殺死了。"
        };


        public override void OnBeginSelfTurn()
        {
            base.OnBeginSelfTurn();
        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();
            Body.CurrentDamagePlus = 1;
            Body.CurrentShootMinus = 1;
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
                if (player.IsLiving && player.X < 158)
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
                KillAttack(0, 300);
                return;
            }
            if (attackingTurn == 0)
            {
                CallDeadZone();
                attackingTurn++;
            }
            else if (attackingTurn == 1)
            {
                AttackInDeadZone();
                attackingTurn++;
            }
            else if (attackingTurn == 2)
            {
                PersonalActack();
                attackingTurn++;
            }
            else
            {
                AllAttack();
                attackingTurn = 0;
            }

        }

        public override void OnStopAttacking()
        {
            base.OnStopAttacking();
        }

        private void CallDeadZone()
        {
            Body.PlayMovie("beatA", 2000, 0);
            Body.CallFuction(CreateMovie, 4000);
        }
        public void CreateMovie()
        {
            m_moive = ((PVEGame)Game).Createlayer(800, Body.Y, "moive", "asset.game.ten.tedabiaoji", "out", 1, 0);
        }
        public void CreateEffect()
        {
            if (target != null)
                m_front = ((PVEGame)Game).Createlayer(target.X, target.Y, "effect", "asset.game.ten.qunbao", "out", 1, 0);
        }
        public void AllAttack()
        {
            Body.PlayMovie("beatA", 1000, 1000);
            Body.RangeAttacking(Body.X - 1500, Body.X + 1500, "cry", 4000, null);
            Body.CallFuction(MultiMovie, 4000);
            Body.CallFuction(Out, 5000);
        }
        private void MultiMovie()
        {
            List<Player> targets = Game.GetAllFightPlayers();
            foreach (Player p in targets)
            {
                m_moive = ((PVEGame)Game).Createlayer(p.X, p.Y, "boom", "asset.game.ten.qunbao", "out", 1, 0);
            }
        }
        public void Out()
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

        public void AttackInDeadZone()
        {
            int index = Game.Random.Next(0, AngryChat.Length);
            Body.Say(AngryChat[index], 1, 0);
            Body.PlayMovie("beatD", 3000, 0);
            Body.CurrentDamagePlus = 10;
            Body.RangeAttacking(Body.X, 1400, "cry", 4000, null);
            Body.CallFuction(Out, 5000);
        }

        public void PersonalActack()
        {
            Body.PlayMovie("beatA", 1000, 1000);
            target = Game.FindRandomPlayer();
            Body.CurrentDamagePlus = 2;
            Body.RangeAttacking(target.X - 20, target.X + 20, "cry", 2000, null);
            Body.CallFuction(CreateEffect, 2000);
            Body.CallFuction(Out, 3000);
        }

        public void KillAttack(int fx, int tx)
        {
            Body.CurrentDamagePlus = 100;
            int index = Game.Random.Next(0, KillAttackChat.Length);
            ((SimpleBoss)Body).Say(KillAttackChat[index], 1, 500);
            Body.PlayMovie("beatB", 2500, 0);
            Body.RangeAttacking(fx, tx, "cry", 3300, null);
        }

    }
}

