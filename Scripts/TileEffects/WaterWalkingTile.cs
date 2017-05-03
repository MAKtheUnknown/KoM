using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWalkingTile : BuffedTile {

	public override void GiveBuff(CharacterCharacter c)
	{
		c.activeEffects.Add(new WaterWalk(c));
	}
}
