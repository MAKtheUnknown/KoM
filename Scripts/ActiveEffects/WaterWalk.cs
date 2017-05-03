using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaterWalk : ActiveEffect{
	
	
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
		if(((LimitedSpaces)(subject.type.movement)).tileTypeTimes[subject.tile.type]>9000)
		{
			subject.Move(subject.FindSafeTile());
		}
	}
	public override void Act()
	{
		
	}
	public override ActiveEffect Clone(CharacterCharacter c)
	{
		return new WaterWalk(c,turnsLeft);
	}
	
}


