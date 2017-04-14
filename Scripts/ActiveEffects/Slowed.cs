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

	public new void Init(CharacterCharacter c, int rounds)
	{
		base.Init (c, rounds);
		((LimitedSpaces)(c.type.movement)).timeToMove-=movementReduction;
	}

	public override void Act()
	{

	}

	public override void Finish()
	{
		((LimitedSpaces)(base.subject.type.movement)).timeToMove+=movementReduction;
	}
}


