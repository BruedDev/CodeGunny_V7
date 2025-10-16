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
    public class FiveHardFourNpc2 : ABrain
    {
        private int m_attackTurn = 0;
		
		private int npcID2 = 5234;
		protected Living targer;
		
		//protected Player targer;

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

            if (m_attackTurn == 0)
            {
                StandB();
				m_attackTurn++;
            }
			else if (m_attackTurn == 1)
            {
				StandC();
				m_attackTurn++;
            }
			else if (m_attackTurn == 2)
            {
				Die();
				m_attackTurn++;
            }

        }

        private void KillAttack(int fx, int tx)
        {
            Body.CurrentDamagePlus = 10;
            int index = Game.Random.Next(0, KillAttackChat.Length);
            Body.Say(KillAttackChat[index], 1, 1000);
            Body.PlayMovie("beat", 3000, 0);
            Body.RangeAttacking(fx, tx, "cry", 4000, null);
        }

        private void Die()
        {
			Body.PlayMovie("dieB", 3000, 0);
			Body.Die(1000);
        }

		private void StandB()
        {

            Body.PlayMovie("standB", 1500, 0);

        }

        private void StandC()
        {

            Body.PlayMovie("standC", 1500, 0);

        }
		private void CreateChild()
        {
            ((SimpleBoss)Body).CreateChild(npcID2, 1350, 700,700, 1, -1);
		}	
    }
}
