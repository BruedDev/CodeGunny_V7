using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic.Effects;

namespace GameServerScript.AI.NPC
{
    public class FourNormalFrantCattleBoss : ABrain
    {
        public int attackingTurn = 1;

        public int orchinIndex = 1;

        public int currentCount = 0;

        public int Dander = 0;

        public List<SimpleNpc> orchins = new List<SimpleNpc>();

        #region NPC 说话内容
        private static string[] AllAttackChat = new string[]{
             "看我的绝技！",
          
             "这招酷吧，<br/>想学不？",
          
             "消失吧！！！<br/>卑微的灰尘！",

             "你们会为此付出代价的！ "
        };

        private static string[] ShootChat = new string[]{
             "你是在给我挠痒痒吗？",
               
             "我可不会像刚才那个废物一样被你打败！",
             
             "哎哟，你打的我好疼啊，<br/>哈哈哈哈！",
               
             "啧啧啧，就这样的攻击力！",
               
             "看到我是你们的荣幸！"          
        };

        private static string[] CallChat = new string[]{
            "来啊，<br/>让他们尝尝炸弹的厉害！"                          
        };

        private static string[] AngryChat = new string[]{
            "是你们逼我使出绝招的！"                          
        };

        private static string[] KillAttackChat = new string[]{
            "你来找死吗？"                          
        };

        private static string[] SealChat = new string[]{
            "异次元放逐！"                          
        };

        private static string[] KillPlayerChat = new string[]{
            "灭亡是你唯一的归宿！",                  
 
            "太不堪一击了！"
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
        }

        public override void OnCreated()
        {
            base.OnCreated();
        }

        public override void OnStartAttacking()
        {
            bool result = false;
            int maxdis = 0;
			Body.Direction = Game.FindlivingbyDir(Body);
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

            if (result == true)
            {
                return;
            }

            if (attackingTurn == 1)
            {
                Jump();
            }
            else if (attackingTurn == 2)
            {
                PersonalAttack();
            }
            else
            {
                attackingTurn = 0;
            }
            attackingTurn++;
        }

        public override void OnStopAttacking()
        {
            base.OnStopAttacking();
        }

        public void Jump()
        {
			Body.PlayMovie("jump", 1000, 6000);	
			Player target = Game.FindRandomPlayer();
			Body.JumpToSpeed(target.X, Body.Y - 1000, "", 2500, 1, 10, new LivingCallBack(Fall));
        }
		
		public void Fall()
        {
			Body.PlayMovie("fall", 0, 0);	
			Body.RangeAttacking(Body.X - 2000, Body.X + 2000, "cry", 0, null);
        }

        private void PersonalAttack()
        {
            if (Body.X > Game.Map.Info.ForegroundWidth / 2)
                Body.MoveTo(1, Body.Y, "walk", 1000, "", 24, new LivingCallBack(FallTo));
            if (Body.X < Game.Map.Info.ForegroundWidth / 2)
                Body.MoveTo(1600, Body.Y, "walk", 1000, "", 24, new LivingCallBack(FallTo));
            Body.RangeAttacking(Body.X - 2000, Body.X + 2000, "cry", 1200, null);
        }
		private void KillAttack(int fx, int tx)
        {
            Body.CurrentDamagePlus = 10;
			Body.Direction = Game.FindlivingbyDir(Body);
            int index = Game.Random.Next(0, KillAttackChat.Length);
            Body.Say(KillAttackChat[index], 1, 1000);
            Body.PlayMovie("beat", 3000, 0);
            Body.RangeAttacking(fx, tx, "cry", 4000, null);
        }
		public void AllAttack()
        {	
		    Body.RangeAttacking(Body.X - 2000, Body.X + 2000, "cry", 0, null);
        }
        public void FallTo()
        {
            if (Body.X > 700)
            {
                Body.ChangeDirection(-1, 0);
                Body.FallFrom(1599, 900, "fallB", 10, 10, 10);
                Body.PlayMovie("fallB", 20, 0);
            }
            else
            {
                Body.ChangeDirection(1, 0);
                Body.FallFrom(1, 900, "fallB", 10, 10, 10);
                Body.PlayMovie("fallB", 20, 0);
            }
        }
    }
}
