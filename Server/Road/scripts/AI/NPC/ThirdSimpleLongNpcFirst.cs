using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic;
using System.Drawing;
using Game.Logic.Actions;
using Bussiness;


namespace GameServerScript.AI.NPC
{
    public class ThirdSimpleLongNpcFirst : ABrain
    {
        private int m_attackTurn = 0;

        private int isSay = 0;
		
		protected Player m_targer;

        #region NPC 说话内容
        private static string[] AllAttackChat = new string[] {
            LanguageMgr.GetTranslation("星辰彈彈堂是第一！"),
        };

        private static string[] ShootChat = new string[]{
            LanguageMgr.GetTranslation("繼續前進！"),
        };

        private static string[] KillPlayerChat = new string[]{
            LanguageMgr.GetTranslation("繼續前進！")
        };

        private static string[] CallChat = new string[]{
            LanguageMgr.GetTranslation("部落首領說把他們殺了就可以獲得獎勵的了！"),

        };

        private static string[] JumpChat = new string[]{
             LanguageMgr.GetTranslation("部落首領說把他們殺了就可以獲得獎勵的了！"),

        };

        private static string[] KillAttackChat = new string[]{
             LanguageMgr.GetTranslation("靠近我，就只有死！"),
			 
			 LanguageMgr.GetTranslation("你是想送死麼？")
        };

        private static string[] ShootedChat = new string[]{
            LanguageMgr.GetTranslation("GameServerScript.AI.NPC.SimpleQueenAntAi.msg15"),

            LanguageMgr.GetTranslation("GameServerScript.AI.NPC.SimpleQueenAntAi.msg16")

        };

        private static string[] DiedChat = new string[]{
            LanguageMgr.GetTranslation("GameServerScript.AI.NPC.SimpleQueenAntAi.msg17")
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

            isSay = 0;
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
                if (player.IsLiving && player.X > 1269 && player.X < Game.Map.Info.ForegroundWidth + 1)
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
                KillAttack(1269, Game.Map.Info.ForegroundWidth + 1);

                return;
            }

            if (m_attackTurn == 0)
            {
                if (((PVEGame)Game).GetLivedLivings().Count == 9)
                {
                    PersonalAttack();
                }
                else
                {
                    PersonalAttack();
                }
                m_attackTurn++;
            }
            else
            {
                PersonalAttack();
                m_attackTurn = 0;
            }
        }

        public override void OnStopAttacking()
        {
            base.OnStopAttacking();
        }

        private void KillAttack(int fx, int tx)
        {
            int index = Game.Random.Next(0, KillAttackChat.Length);
            Body.Say(KillAttackChat[index], 0, 1000);
            Body.CurrentDamagePlus = 1;
			Player target = Game.FindRandomPlayer();
			int mtX = Game.Random.Next(target.X - 70, target.X + 70);
            Body.MoveTo(mtX, target.Y, "walk", 1000, "", 3);
			Body.Direction = Game.FindlivingbyDir(Body);
            Body.PlayMovie("beatB", 3000, 0);
            Body.RangeAttacking(fx, tx, "cry", 5000, null);
        }	
		
		private void PersonalAttack()
        {
            int dis = Game.Random.Next(1200, 1550);
            Body.MoveTo(dis, Body.Y, "walk", 1000, "", 3, new LivingCallBack(NextAttack));
        }

        private void NextAttack()
        {
            Player target = Game.FindRandomPlayer();
			Body.Direction = Game.FindlivingbyDir(Body);


            if (target != null)
            {
                Body.CurrentDamagePlus = 0.8f;
               
                int mtX = Game.Random.Next(target.X - 0, target.X + 0);

                if (Body.ShootPoint(target.X, target.Y, 58, 1000, 10000, 1, 3.0f, 2550))
                {
                    Body.PlayMovie("beatA", 1700, 0);
                }
            }
        }
		

        public override void OnKillPlayerSay()
        {
            base.OnKillPlayerSay();
            int index = Game.Random.Next(0, KillPlayerChat.Length);
            Body.Say(KillPlayerChat[index], 1, 0, 2000);
        }

        public override void OnDiedSay()
        {

        }

        private void CreateChild()
        {

        }

        public override void OnShootedSay()
        {
            int index = Game.Random.Next(0, ShootedChat.Length);
            if (isSay == 0 && Body.IsLiving == true)
            {
                Body.Say(ShootedChat[index], 1, 900, 0);
                isSay = 1;
            }

            if (!Body.IsLiving)
            {
                index = Game.Random.Next(0, DiedChat.Length);
                Body.Say(DiedChat[index], 1, 900 - 800, 2000);
                //Game.AddAction(new FocusAction(Body.X, Body.Y - 90, 0, delay - 900, 4000));
            }
        }
		#region NPC 小怪说话

        private static Random random = new Random();
        private static string[] listChat = new string[] { 
            "為了部落首領，我們一定要贏！",
            "搶了強化石就可以強化武器了！",
            "星辰彈彈堂是最好的！",
            "我們要準備對付前面的敵人！",
            "最近首領的行為太不尋常了！",
            "團結就是力量，上啊！",
            "儘快消滅敵人！",
            "團結就是力量！",
            "速戰速決！",
            "我們要把敵人包圍，然後把他們消滅。",
            "增援！增援！我們需要增援！ ！",
            "我們不會讓你得逞的",
            "你們不要輕視部落勇士的實力，否則會因此而付出代價喔！"
        };

        public static string GetOneChat()
        {
            int index = random.Next(0, listChat.Length);
            return listChat[index];
        }


        /// <summary>
        /// 小怪说话
        /// </summary>
        public static void LivingSay(List<Living> livings)
        {
            if (livings == null || livings.Count == 0)
                return;
            int sayCount = 0;
            int livCount = livings.Count;
            foreach (Living living in livings)
            {
                living.IsSay = false;
            }
            if (livCount <= 5)
            {
                sayCount = random.Next(0, 2);
            }
            else if (livCount > 5 && livCount <= 10)
            {
                sayCount = random.Next(1, 3);
            }
            else
            {
                sayCount = random.Next(1, 4);
            }

            if (sayCount > 0)
            {
                int[] sayIndexs = new int[sayCount];
                for (int i = 0; i < sayCount;)
                {
                    int index = random.Next(0, livCount);
                    if (livings[index].IsSay == false)
                    {
                        livings[index].IsSay = true;
                        int delay = random.Next(0, 5000);
                        livings[index].Say(ThirdSimpleLongNpcFirst.GetOneChat(), 0, delay);
                        i++;
                    }
                }
            }
        }

        #endregion
    }
}
