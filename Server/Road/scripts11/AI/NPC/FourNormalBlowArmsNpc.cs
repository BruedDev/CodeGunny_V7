using Game.Logic.Effects;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using Game.Logic;

namespace GameServerScript.AI.NPC
{
    public class FourNormalBlowArmsNpc : ABrain
    {
        private int m_attackTurn = 0;
        private PhysicalObj m_moive = null;

        public override void OnBeginSelfTurn()
        {
            base.OnBeginSelfTurn();
        }

        public override void OnBeginNewTurn()
        {
            base.OnBeginNewTurn();
            m_body.CurrentDamagePlus = 1f;
            m_body.CurrentShootMinus = 1f;
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
                MoveToGate();
                ++m_attackTurn;
            }
            else if (m_attackTurn == 1)
            {
                MoveToGate();
                ++m_attackTurn;
            }
            else if (m_attackTurn == 2)
            {
                MoveToGate();
                ++m_attackTurn;
            }
            else
            {
                MoveToExit();
                m_attackTurn = 0;
            }
        }        

        private void MoveToGate()
        {
            Body.MoveTo(Body.X + Game.Random.Next(250, 300), Body.Y, "walk", 2000, "", 4, new LivingCallBack(CanDie));
        }

        private void MoveToExit()
        {
            Body.MoveTo(1415, Body.Y, "walk", 2000, "", 4, new LivingCallBack(BeatA));
        }

        private void CanDie()
        {
            if (Body.Blood <= 50)
            {
                Body.PlayMovie("die", 100, 0);
                Body.Die(1000);
            }
            else
            {
                Body.AddEffect((AbstractEffect)new ContinueReduceBloodEffect(2, Game.Random.Next(789, 1021), Body), 0);
                Body.PlayMovie("standB", 100, 0);
            }
        }

        private void BeatA()
        {
            Body.PlayMovie("beatA", 100, 0);
            if (Body.FindCount == 0)
                Body.CallFuction(new LivingCallBack(CryA), 2900);
            else if (Body.FindCount == 1)
                Body.CallFuction(new LivingCallBack(CryB), 2900);
            else
                Body.CallFuction(new LivingCallBack(CryC), 2900);
            Body.Die(3000);
        }

        private void CryA()
        {
            m_moive = (PhysicalObj)((PVEGame)Game).Createlayer(1590, 750, "moive", "game.asset.Gate", "cryA", 1, 0);
            Body.FindCount = 3;
        }

        private void CryB()
        {
            m_moive = (PhysicalObj)((PVEGame)Game).Createlayer(1590, 750, "moive", "game.asset.Gate", "cryB", 1, 0);
            Body.FindCount = 2;
        }

        private void CryC()
        {
            m_moive = (PhysicalObj)((PVEGame)Game).Createlayer(1590, 750, "moive", "game.asset.Gate", "cryC", 1, 0);
            Body.FindCount = 3;
        }

        public override void OnStopAttacking()
        {
            base.OnStopAttacking();
        }
    }
}
