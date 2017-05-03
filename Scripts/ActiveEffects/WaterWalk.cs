using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaterWalk : ActiveEffect
{
	//We still don't know what the design team meant by -1 movement tile, so I'm just gonna use 3 as a plug
	int movementReduction=3;
	
	
	public WaterWalk (CharacterCharacter c)
	{
		Init(c, 5);
	}
	
	public WaterWalk (CharacterCharacter c, int r)
	{
		Init(c, r);
	}

	public new void Init(CharacterCharacter c, int rounds)
	{
		base.Init (c, rounds);
		((LimitedSpaces)(c.type.movement)).tileTypeTimes[TileAttributes.TileType.water]=2;
		((LimitedSpaces)(c.type.movement)).tileTypeTimes[TileAttributes.TileType.brokenBridge]=2;
		type=ActiveEffect.EffectType.buff;
	}

	public override void Finish()
	{
		
		((LimitedSpaces)(subject.type.movement)).tileTypeTimes[TileAttributes.TileType.water]=9001;;
		((LimitedSpaces)(subject.type.movement)).tileTypeTimes[TileAttributes.TileType.brokenBridge]=9001;
	}
	public override void Act()
	{
		
	}
	public override ActiveEffect Clone(CharacterCharacter c)
	{
		return new Slowed(c,turnsLeft,movementReduction);
	}
}


