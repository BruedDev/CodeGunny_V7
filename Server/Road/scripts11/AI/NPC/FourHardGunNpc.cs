using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic.Effects;

namespace GameServerScript.AI.NPC
{
    public class FourHardGunNpc : ABrain
    {
        public int attackingTurn = 1;

        private int npcID = 4203;

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
                if (player.IsLiving && player.X > 400 && player.X < 1600)
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
                KillAttack(400, 1600);
                return;
            }

            if (result == true)
            {
                return;
            }

            ((PVEGame)Game).SendGameObjectFocus(1, "door", 1000, 0);
            if (attackingTurn == 1)
            {
                BestA();
            }
            else if (attackingTurn == 2)
            {
                BestC();
            }
            else
            {
                BestB();
                attackingTurn = 0;
            }
            attackingTurn++;
        }

        public override void OnStopAttacking()
        {
            base.OnStopAttacking();
        }

        public void BestB()
        {
            Body.PlayMovie("beatB", 0, 3000);
            List<SimpleNpc> m_livings = new List<SimpleNpc>();
            foreach (Living npc in m_livings)
            {
                if (npc is SimpleNpc)
                {
                    m_livings.Add(npc as SimpleNpc);
                    npc.AddEffect(new ContinueReduceBloodEffect(2, 500, npc), 0);

                }
            }

        }

        public void BestA()
        {
            Body.PlayMovie("beatA", 1000, 0);
        }

        public void BestC()
        {
            Body.PlayMovie("beatC", 1000, 0);
            int dis = Game.Random.Next(400, 1680);
            Body.CallFuction(new LivingCallBack(CreateChild), 2500);
        }

        public void CreateChild()
        {
            int dis = Game.Random.Next(470, 880);
            ((SimpleBoss)Body).CreateChild(npcID, dis, 700, 2, 400, -1);
        }

        private void KillAttack(int fx, int tx)
        {
            Body.CurrentDamagePlus = 10;
            Body.Direction = Game.FindlivingbyDir(Body);
            Body.PlayMovie("beatB", 3000, 0);
            Body.RangeAttacking(fx, tx, "cry", 4000, null);
        } 
    }
}
