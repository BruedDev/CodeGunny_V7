using System;
using System.Collections.Generic;
using System.Text;
using Game.Logic.AI;
using Game.Logic.Phy.Object;

namespace GameServerScript.AI.NPC
{
    public class FourNormalShortNpc : ABrain
    {
        public int attackingTurn = 1;
        public int orchinIndex = 1;
        public int currentCount = 0;
        public int Dander = 0;
        protected List<Living> m_livings;

        public override void OnBeginSelfTurn()
        {
            base.OnBeginSelfTurn();
        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();
            Body.CurrentDamagePlus = 1f;
            Body.CurrentShootMinus = 1f;
        }

        public override void OnCreated()
        {
            base.OnCreated();
        }

        public override void OnStartAttacking()
        {
            bool result = false;
            int maxdis = 0;
            foreach (Player player in Game.GetAllFightPlayers())
            {
                if (player.IsLiving && player.X > 0 && player.X < 0)
                {
                    int dis = (int)((Physics)Body).Distance(player.X, player.Y);
                    if (dis > maxdis)
                        maxdis = dis;
                    result = true;
                }
            }
            if (result)
            {
                KillAttack(0, 0);
            }
            else
            {
                if (result)
                    return;
                if (attackingTurn == 1)
                    MoveToPlayer();
                else if (attackingTurn == 2)
                    MoveToPlayer();
                else if (attackingTurn == 3)
                {
                    MoveToPlayer();
                }
                else
                {
                    MoveToPlayer();
                    attackingTurn = 0;
                }
                ++attackingTurn;
            }
        }

        public override void OnStopAttacking()
        {
            base.OnStopAttacking();
        }

        public void MoveToPlayer()
        {
            Body.MoveTo(Game.Random.Next(370, 650), Body.Y, "walk", 2000, "", 4);
        }

        private void KillAttack(int fx, int tx)
        {
            Body.PlayMovie("beat", 3000, 0);
            Body.RangeAttacking(fx, tx, "cry", 4000, (List<Player>)null);
        }
    }
}
