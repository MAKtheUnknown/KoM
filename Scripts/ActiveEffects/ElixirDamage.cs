using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElixirDamage : ActiveEffect {

	private double percentDamageIncrease;
	private int changeInDamage;

	public ElixirDamage(CharacterCharacter c)
	{
		Init (c);
	}

	public ElixirDamage(CharacterCharacter c, int r)
	{
		Init (c, r);
	}

	public void Init(CharacterCharacter c)
	{
		this.Init (c, 4);
	}

	public new void Init(CharacterCharacter c, int rounds)
	{
		base.Init (c, rounds);
		percentDamageIncrease=1.3;
		changeInDamage=(int)(c.type.attack*(percentDamageIncrease-1));
		c.type.attack+=changeInDamage;
		type=ActiveEffect.EffectType.buff;
	}

	public override void Act()
	{
		
	}

	public override void Finish()
	{
		base.subject.type.attack-=changeInDamage;
	}
	
	public override ActiveEffect Clone(CharacterCharacter c)
	{
		return new ElixirDamage(c, turnsLeft);
	}
}
