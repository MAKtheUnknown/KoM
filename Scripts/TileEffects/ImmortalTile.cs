using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalTile : BuffedTile {

	public override void GiveBuff(CharacterCharacter c)
	{
		ContinuousEffect i = new Immortal(c);
		c.activeEffects.Add(i);
		c.type.passives.Add(i);
	}
}
