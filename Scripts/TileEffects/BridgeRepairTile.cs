using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeRepairTile : BuffedTile {

	public override void GiveBuff(CharacterCharacter c)
	{
		c.activeEffects.Add(new BridgeRepair(c));
	}
}
