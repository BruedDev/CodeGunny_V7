
using Game.Logic;
using Game.Logic.AI;
using Game.Logic.Phy.Object;
using System.Collections.Generic;

namespace GameServerScript.AI.NPC
{
  public class FourNormalFireNpc : ABrain
  {
    private int m_turn = 0;
    private int m_attackTurn = 0;
    private PhysicalObj m_moive = (PhysicalObj) null;

    public override void OnCreated()
    {
      base.OnCreated();
    }

    public override void OnStartAttacking()
    {
      base.OnStartAttacking();      
      if (m_attackTurn == 0)
      {
        Move();
        m_attackTurn++;
      }
      else if (m_attackTurn == 1)
      {
        Move();
        m_attackTurn++;
      }
      else
      {
        Die();
        m_attackTurn = 0;
      }
    }

    private void Move()
    {
      Body.MoveTo(Game.Random.Next(300, 980), Game.Random.Next(300, 600), "fly", 500, "", 6, new LivingCallBack(CreateChild));
    }

    public void Die()
    {
      Body.PlayMovie("cry", 1000, 0);
      Body.PlayMovie("die", 2000, 0);
      Body.Die(3000);
    }

    private void CreateChild()
    {
      m_moive = ((PVEGame) Game).Createlayer(Body.X, Body.Y + 20, "moive", "game.living.Living141", "stand", 1, 0);
    }

    public override void OnBeginNewTurn()
    {
      base.OnBeginNewTurn();
      if (m_moive == null)
        return;
      Game.RemovePhysicalObj(m_moive, true);
      m_moive = null;
    }
  }
}
