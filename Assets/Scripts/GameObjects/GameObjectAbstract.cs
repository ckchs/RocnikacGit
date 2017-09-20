using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rocnikacClassy
{

	public abstract class GameObjectAbstract
	{
		public Position actualPosition;
		public GamePlan gamePlan;

		abstract public void getDamage (int ammount);
		abstract public bool IsEmpty ();
        
	}
}
