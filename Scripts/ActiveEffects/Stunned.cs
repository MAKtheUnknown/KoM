using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunned : ActiveEffect {
	private int changeInDefense;


	public Stunned(CharacterCharacter c)
	{
		Init (c);
	}

	public Stunned(CharacterCharacter c, int r)
	{
		Init (c,r);
	}

	public void Init(CharacterCharacter c)
	{
		this.Init (c, 1);
	}

	public new void Init(CharacterCharacter c, int rounds)
	{
		base.Init (c, rounds);
		type=ActiveEffect.EffectType.debuff;
	}

	public override void Act()
	{
		
		subject.usedAbility=true;
	}

	public override void Finish()
	{
		
	}
	
	public override ActiveEffect Clone(CharacterCharacter c)
	{
		return new Stunned(c,turnsLeft);
	}
}
