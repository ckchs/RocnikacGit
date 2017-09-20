using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rocnikacClassy;

public class TurnPlaner
{
	int characterCount;

	public TurnPlaner (Character[] characters)
	{
		planer = new Dictionary<Character, characterTurn> (characters.GetLength (0));
		for (int i = 0; i < characters.Length; i++) {
			planer.Add (characters [i], new characterTurn (i));
		}
		characterCount = characters.Length;

	}

	Dictionary<Character,characterTurn> planer;

	public void setMovement (Character chr, Position pos)
	{
		planer [chr].movementPosition = pos;
	}

	public void setAttack (Character chr, Position pos)
	{
		planer [chr].attackPosition = pos;
		planer [chr].selected = true;
	}

	public void setOrder (Character chr, int settedOrder)
	{
		if (settedOrder >= planer.Count) {
			throw new UnityException ("bad order");
		}
		int previous = planer [chr].order;
		bool bigger = previous > settedOrder;
		foreach (var item in planer) {
			if (item.Key == chr) {
				item.Value.order = settedOrder;
			} else {
				if (bigger) {
					if ((item.Value.order >= settedOrder) && (item.Value.order <= previous)) {
						item.Value.order++;
					}
						
				} else {
					if ((item.Value.order <= settedOrder) && (item.Value.order >= previous)) {
						item.Value.order--;
					}
				}
			}
		}
	}

	public int getOrder (Character chr)
	{
		return planer [chr].order;
	}

	public string decodeTurn ()
	{
		var outputArray = new Character[characterCount];
		foreach (var item in planer) {
			outputArray [item.Value.order] = item.Key;
		}
		var outputBuilder = new System.Text.StringBuilder ();
		for (int i = 0; i < characterCount; i++) {
			outputBuilder.Append (outputArray [i].actualPosition.x);
			outputBuilder.Append (" ");
			outputBuilder.Append (outputArray [i].actualPosition.y);
			outputBuilder.Append (" ");
			outputBuilder.Append (planer [outputArray [i]].movementPosition.x);
			outputBuilder.Append (" ");
			outputBuilder.Append (planer [outputArray [i]].movementPosition.y);
			outputBuilder.Append (" ");
			outputBuilder.Append (planer [outputArray [i]].attackPosition.x);
			outputBuilder.Append (" ");
			outputBuilder.Append (planer [outputArray [i]].attackPosition.y);
			outputBuilder.Append (" ");
		}

		return outputBuilder.ToString();
	}

	class characterTurn
	{
		public characterTurn (int order)
		{
			this.order = order;

		}

		public bool selected = false;
		public int order;
		public Position movementPosition;
		public Position attackPosition;

	}
}
