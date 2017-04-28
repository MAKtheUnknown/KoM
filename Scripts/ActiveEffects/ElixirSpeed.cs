using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ElixirSpeed : ActiveEffect {

	private int changeInMovement;

	public ElixirSpeed(CharacterCharacter c)
	{
		Init (c);
	}

	public void Init(CharacterCharacter c)
	{
		this.Init (c, 4);
	}

	public new void Init(CharacterCharacter c, int rounds)
	{
		base.Init (c, rounds);
		changeInMovement=3;
		((LimitedSpaces)base.subject.type.movement).timeToMove+=changeInMovement;
		type=ActiveEffect.EffectType.buff;
	}

	public override void Act()
	{
		
	}

	public override void Finish()
	{
		((LimitedSpaces)base.subject.type.movement).timeToMove-=changeInMovement;
		((LimitedSpaces)base.subject.type.movement).timeSpent=Math.Max(((LimitedSpaces)base.subject.type.movement).timeSpent,0);
	}
}
