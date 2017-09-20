using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rocnikacClassy
{
	public abstract class Character:GameObjectAbstract
	{
		public bool allied;
		public int hp;
		public int movementRange;
		public int attackRange;
		public int dmg;
		private Position _absoluteAttackPos;

		public Position AbsoluteAttackPos {
			get { return _absoluteAttackPos; }
			set { 
				_absoluteAttackPos = value;
				_relativePosXAttack = _absoluteAttackPos.x - planedMovement.x;
				_relativePosXAttack = _absoluteAttackPos.y - planedMovement.y;
			}

		}

		public Position planedMovement;
		protected int _relativePosXAttack;
		protected int _relativePosYAttack;

		public override sealed bool IsEmpty ()
		{
			return false;
		}

		public abstract void Attack();

		public override sealed void getDamage (int ammount)
		{
			hp = hp - ammount;
			if (hp < 0) {
				this.isDying ();
			}
		}

		protected void isDying ()
		{
		}

	}
}
