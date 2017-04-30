using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspired : ActiveEffect {

	private int changeInDamage;

	public Inspired(CharacterCharacter c)
	{
		Init (c);
	}

	public Inspired(CharacterCharacter c,int r)
	{
		Init (c,r);
	}

	public void Init(CharacterCharacter c)
	{
		this.Init (c, 6);
	}

	public new void Init(CharacterCharacter c, int rounds)
	{
		base.Init (c, rounds);
		changeInDamage = (int)(c.type.attack*0.1);
		c.type.attack+=changeInDamage;
		c.type.defense+=2;
		type=ActiveEffect.EffectType.buff;
	}

	public override void Act()
	{
		
	}

	public override void Finish()
	{
		subject.type.attack -= changeInDamage;
		subject.type.defense-=2;
	}
	
	public override ActiveEffect Clone(CharacterCharacter c)
	{
		return new Inspired(c,turnsLeft);
	}
}
