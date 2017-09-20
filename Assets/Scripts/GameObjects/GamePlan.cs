using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rocnikacClassy
{
	public class GamePlan
	{
		GameObjectAbstract[,] gamePlan;
		int planSize = 10;
		public Character[] alliedCharacters;
		int alliedCharactersCount=0;
		Character[] enemyCharacters;
		int enemyCharactersCount=0;

		public GamePlan (int size = 10, int characterCount = 2)
		{
			planSize = size;
			gamePlan = new GameObjectAbstract[planSize, planSize];
			alliedCharacters = new Character[characterCount];
			enemyCharacters = new Character[characterCount];
		}
		//return number of characters
		public int AddAlliedCharacterAtPosition (Character chr, Position pos)
		{
			gamePlan [pos.x, pos.y] = chr;
			alliedCharacters [alliedCharactersCount] = chr;
			return ++alliedCharactersCount;
		}

		public int AddEnemyCharacterAtPosition (Character chr, Position pos)
		{
			gamePlan [pos.x, pos.y] = chr;
			return ++enemyCharactersCount;
		}



		public GameObjectAbstract getObjectAtPosition (Position pos)
		{
			if (pos.x >= planSize) {
				return null;
			}
			if (pos.y >= planSize) {
				return null;
			}
			if (pos.x < 0) {
				return null;
			}
			if (pos.y < 0) {
				return null;
			}

			return gamePlan [pos.x, pos.y];
		}
	}
}
