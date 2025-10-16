using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic;
using Game.Logic.Phy.Object;

namespace GameServerScript.AI.NPC
{
    public class FiveTerrorThirdBoss : ABrain
    {
        private int m_attackTurn = 0;
		
		private int npcID = 5322;
		
		private int npcID2 = 5323;
		
		private int npcID3 = 5324;
		
		private PhysicalObj m_moive;

        private PhysicalObj m_front;

        #region NPC 说话内容
        private static string[] AllAttackChat = new string[] { 
            "Trận động đất, bản thân mình! ! <br/> bạn vui lòng Ay giúp đỡ",
       
            "Hạ vũ khí xuống!",
       
            "Xem nếu bạn có thể đủ khả năng, một số ít!！"
        };

        private static string[] ShootChat = new string[]{
             "Cho bạn biết những gì một cú sút vết nứt!",
                               
             "Gửi cho bạn một quả bóng - bạn phải chọn Vâng",

             "Nhóm của bạn của những người dân thường ngu dốt và thấp"
        };

        private static string[] ShootedChat = new string[]{
           "Ah ~ ~ Tại sao bạn tấn công? <br/> tôi đang làm gì?",
                   
            "Oh ~ ~ nó thực sự đau khổ! Tại sao tôi phải chiến đấu? <br/> tôi phải chiến đấu ..."

        };

        private static string[] AddBooldChat = new string[]{
            "Xoắn ah xoay ~ <br/>xoắn ah xoay ~ ~ ~",
               
            "~ Hallelujah <br/>Luyaluya ~ ~ ~",
                
            "Yeah Yeah Yeah, <br/> để thoải mái!"
         
        };

        private static string[] KillAttackChat = new string[]{
            "Con rồng trong thế giới! !"
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

            if (m_attackTurn == 0)
            {
                BeatE();
                ++m_attackTurn;
            }
            else if (m_attackTurn == 1)
            {
                CallNpc();
                m_attackTurn++;
            }
            else if (m_attackTurn == 2)
            {
                Body.MoveTo(Game.Random.Next(400, 1300), 600, "fly", 0, "", 10, new LivingCallBack(AllAttack));
                m_attackTurn++;
            }
            else if (m_attackTurn == 3)
            {
                BeatE();
                m_attackTurn++;
            }
            else
            {
                CallNpc();
                m_attackTurn = 0;
            }
        }

        private void BeatE()
        {
            Player randomPlayer = Game.FindRandomPlayer();
            Body.MoveTo(Game.Random.Next(randomPlayer.X - 50, randomPlayer.X + 50), Game.Random.Next(randomPlayer.Y - 100, randomPlayer.Y - 100), "fly", 1000, "", 10, new LivingCallBack(BeatOneKill));
        }

        private void BeatOneKill()
        {
            //int index = Game.Random.Next(0, FiveNormalThirdBoss.BeatOneKillChat.Length);
            //Body.Say(FiveNormalThirdBoss.BeatOneKillChat[index], 1, 0);
            Body.Direction = Game.FindlivingbyDir(Body);
            Body.PlayMovie("beatE", 3000, 0);
            Body.RangeAttacking(Body.X - 100, Body.X + 100, "cry", 5000, (List<Player>)null);
        }

        private void CallNpc()
        {
            Body.MoveTo(Game.Random.Next(500, 1200), Game.Random.Next(400, 600), "fly", 1000, "", 10, new LivingCallBack(CallMohang));
        }

        private void CallMohang()
        {
            Body.Direction = Game.FindlivingbyDir(Body);
            Body.PlayMovie("beatB", 3300, 4000);
            Body.CallFuction(new LivingCallBack(GoCallMohang), 3500);
        }

        private void GoCallMohang()
        {
            Body.Direction = Game.FindlivingbyDir(Body);
            int x = Game.Random.Next(700, 1300);
            if (Game.GetLivedLivings().Count <= 1)
                ((SimpleBoss)Body).CreateChild(npcID, x, 680, -1, 1, 1);
            if (Game.GetLivedLivings().Count <= 1)
                return;
            ((SimpleBoss)Body).CreateChild(npcID, x, 680, -1, 100, 2);
        }

        private void AllAttack()
        {
            Body.Direction = Game.FindlivingbyDir(Body);
            Body.PlayMovie("beatA", 3200, 0);
            Body.CallFuction(new LivingCallBack(In), 3400);
        }

        private void In()
        {
            Body.CurrentDamagePlus = 0.5f;
            m_moive = ((PVEGame)Game).CreatePhysicalObj(1000, 400, "moive", "asset.game.4.heip", "out", 2, 0);
            Body.RangeAttacking(Body.X - 1000, Body.X + 1000, "cry", 3000, null);
            Body.CallFuction(new LivingCallBack(Out), 2500);
        }

        private void Out()
        {
            m_moive.CanPenetrate = true;
            Game.RemovePhysicalObj(m_moive, true);
        }
    }
}
