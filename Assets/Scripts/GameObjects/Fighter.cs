using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rocnikacClassy
{
	class Fighter:Character
	{
		public Fighter (int posX, int posY, bool allied)
		{
			this.allied = allied;
			hp = 100;
			attackRange = 1;
			movementRange = 2;
			actualPosition.x = posX;
			actualPosition.y = posY;
		}
		Position attackPosition;
		public override void Attack ()
		{
			
			attackPosition.x = actualPosition.x + _relativePosXAttack;
			attackPosition.y = actualPosition.y + _relativePosYAttack;

			switch (_relativePosXAttack) {
			case 1://attacking up
				attackAtAttackPosition ();
				attackPosition.x++;
				attackAtAttackPosition ();
				attackPosition.x--;
				attackPosition.y++;
				attackAtAttackPosition ();
				attackPosition.y -= 2;
				attackAtAttackPosition ();

				break;
			case -1://attacking down
				attackAtAttackPosition ();
				attackPosition.x--;
				attackAtAttackPosition ();
				attackPosition.x++;
				attackPosition.y++;
				attackAtAttackPosition ();
				attackPosition.y -= 2;
				attackAtAttackPosition ();
				break;
			default:
				break;
			}
			switch (_relativePosYAttack) {
			case 1://attacking right
				attackAtAttackPosition ();
				attackPosition.y++;
				attackAtAttackPosition ();
				attackPosition.y--;
				attackPosition.x++;
				attackAtAttackPosition ();
				attackPosition.x -= 2;
				attackAtAttackPosition ();
				break;
			case -1://attacking left
				attackAtAttackPosition ();
				attackPosition.y--;
				attackAtAttackPosition ();
				attackPosition.y++;
				attackPosition.x++;
				attackAtAttackPosition ();
				attackPosition.x -= 2;
				attackAtAttackPosition ();
				break;
			default:
				break;
			}
		}
		private void attackAtAttackPosition()
		{
			if (gamePlan.getObjectAtPosition (attackPosition) != null) {
				gamePlan.getObjectAtPosition (attackPosition).getDamage (dmg);
			}
		}

	}
}
