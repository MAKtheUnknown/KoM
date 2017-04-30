using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElixirDefense : ActiveEffect {

	private int changeInDefense;

	public ElixirDefense(CharacterCharacter c)
	{
		Init (c);
	}
	
	public ElixirDefense(CharacterCharacter c, int r)
	{
		Init (c,r);
	}

	public void Init(CharacterCharacter c)
	{
		this.Init (c, 4);
	}

	public new void Init(CharacterCharacter c, int rounds)
	{
		base.Init (c, rounds);
		changeInDefense=6;
		c.type.defense+=changeInDefense;
		type=ActiveEffect.EffectType.buff;
	}

	public override void Act()
	{
		
	}

	public override void Finish()
	{
		subject.type.defense-=changeInDefense;
	}
	
	public override ActiveEffect Clone(CharacterCharacter c)
	{
		return new ElixirDefense(c,turnsLeft);
	}
}
