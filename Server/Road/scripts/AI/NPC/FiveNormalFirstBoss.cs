using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic.Effects;
using Game.Logic;

namespace GameServerScript.AI.NPC
{
    public class FiveNormalFirstBoss : ABrain
    {
        private int m_attackTurn = 0;

        private PhysicalObj m_moive;

        private PhysicalObj m_wallRight = null;

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

        private static string[] AddBooldChat = new string[]{
            "扭啊扭~<br/>扭啊扭~~",
               
            "哈利路亚~<br/>路亚路亚~~",
                
            "呀呀呀，<br/>好舒服啊！"
         
        };

        private static string[] KillAttackChat = new string[]{
            "君临天下！！"
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
                KillAttack(1400, 1600);

                return;
            }

            if (m_attackTurn == 0)
            {
                BeatA();
                Body.SetXY(Body.X, 659);
                m_attackTurn++;
            }
            else if (m_attackTurn == 1)
            {
                Body.SetXY(Body.X, 559);
                BeatB();
                m_attackTurn++;
            }
            else if (m_attackTurn == 2)
            {
                Body.SetXY(Body.X, 459);
                BeatC();
                m_attackTurn++;
            }
            else if (m_attackTurn == 3)
            {
                Body.SetXY(Body.X, 359);
                BeatD();
                m_attackTurn++;
            }
            else if (m_attackTurn == 4)
            {
                if (Body.Y == 758)
                {
                    m_attackTurn = 0;
                }
                else
                {
                    BeatE();
                    Body.SetXY(Body.X, 259);
                    m_attackTurn++;
                }
               
            }
            else
            {
                if (m_attackTurn != 5)
                    return;
                if (Body.Y == 758)
                {
                    m_attackTurn = 0;
                }
                else
                {
                    BeatG();
                    Body.SetXY(Body.X, 259);
                }
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
            Body.PlayMovie("beat", 3000, 10000);
            Body.RangeAttacking(fx, tx, "cry", 5000, null);
        }

        private void BeatA()
        {
            Body.PlayMovie("beatA", 3000, 11000);
            Body.CallFuction(new LivingCallBack(CallBeatA), 11000);
        }

        private void CallBeatA()
        {
            Body.PlayMovie("standA", 2000, 0);
            List<Player> allLivingPlayers = Game.GetAllLivingPlayers();
            int num = Game.Random.Next(1000, 2510);
            foreach (Player player in allLivingPlayers)
                player.AddEffect((AbstractEffect)new ContinueReduceBloodEffect(2, num, (Living)player), 0);
        }

        private void BeatB()
        {
            Body.PlayMovie("beatB", 3000, 10000);
            Player randomPlayer = Game.FindRandomPlayer();
            ((SimpleBoss)Body).NpcInfo.FireY = 0;
            if (randomPlayer != null)
                Body.ShootPoint(randomPlayer.X, randomPlayer.Y, 56, 1000, 10000, 1, 2f, 10000);
            Body.CallFuction(new LivingCallBack(CallBeatB), 11000);
        }

        private void CallBeatB()
        {
            Body.PlayMovie("standB", 3000, 0);
        }

        private void BeatC()
        {
            Body.PlayMovie("beatC", 3000, 10000);
            Body.CallFuction(new LivingCallBack(CallBeatC), 11000);
        }

        private void CallBeatC()
        {
            Body.PlayMovie("standC", 3000, 0);
            List<Player> allFightPlayers = Game.GetAllFightPlayers();
            foreach (Player player in allFightPlayers)
            {
                int num = Game.Random.Next(200, 510);
                m_wallRight = ((PVEGame)Game).CreatePhysicalObj(player.X, player.Y, "wallLeft", "asset.game.4.zap", "1", 1, 1);
                player.AddEffect((AbstractEffect)new ReduceStrengthEffect(2, 5), 0);
                player.AddBlood(-num, 1);
            }
            List<Player> list = new List<Player>();
            foreach (Player player in allFightPlayers)
            {
                if (!player.IsFrost)
                    list.Add(player);
            }
        }

        private void BeatD()
        {
            Body.PlayMovie("beatD", 3000, 10000);
            Body.CallFuction(new LivingCallBack(CallBeatD), 11000);
        }

        private void CallBeatD()
        {
            Body.PlayMovie("standD", 3000, 0);
            foreach (Player player in Game.GetAllFightPlayers())
            {
                int num = Game.Random.Next(100, 515);
                m_moive = ((PVEGame)Game).Createlayer(player.X, player.Y, "moive", "asset.game.4.minigun", "", 1, 1);
                player.AddBlood(-num, 1);
                player.AddBlood(-num, 1);
                player.AddBlood(-num, 1);
                player.AddBlood(-num, 1);
                player.AddBlood(-num, 1);
                player.AddBlood(-num, 1);
            }
        }

        private void BeatE()
        {
            Body.PlayMovie("DtoE", 3000, 10000);
            Body.CallFuction(new LivingCallBack(BeatG), 10000);
        }

        private void CallBeatE()
        {
            Body.PlayMovie("standE", 3000, 0);
        }

        private void BeatG()
        {
            Player randomPlayer = Game.FindRandomPlayer();
            Body.PlayMovie("beatE", 3000, 10000);
            ((SimpleBoss)Body).NpcInfo.FireY = 20;
            Body.ShootPoint(randomPlayer.X, randomPlayer.Y, 72, 1000, 10000, 1, 1f, 5500);
            Body.CallFuction(new LivingCallBack(CallBeatE), 10000);
        }
        
    }
}
