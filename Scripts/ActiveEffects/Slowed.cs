using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slowed : ActiveEffect
{
	//We still don't know what the design team meant by -1 movement tile, so I'm just gonna use 3 as a plug
	int movementReduction=3;
	
	
	public Slowed (CharacterCharacter c)
	{
		Init(c, 1);
	}
	
	public Slowed (CharacterCharacter c, int r)
	{
		Init(c, r);
	}
	
	public Slowed (CharacterCharacter c, int rounds, int reduction)
	{
		Init(c, rounds, reduction);
	}


	public new void Init(CharacterCharacter c, int rounds)
	{
		base.Init (c, rounds);
		((LimitedSpaces)(c.type.movement)).timeToMove-=movementReduction;
		type=ActiveEffect.EffectType.debuff;
	}
	
	public void Init(CharacterCharacter c, int rounds, int reduction)
	{
		base.Init (c, rounds);
		movementReduction=reduction;
		((LimitedSpaces)(c.type.movement)).timeToMove-=movementReduction;
	}

	public override void Act()
	{

	}

	public override void Finish()
	{
		((LimitedSpaces)(base.subject.type.movement)).timeToMove+=movementReduction;
	}
	
	public override ActiveEffect Clone(CharacterCharacter c)
	{
		return new Slowed(c,turnsLeft,movementReduction);
	}
}


