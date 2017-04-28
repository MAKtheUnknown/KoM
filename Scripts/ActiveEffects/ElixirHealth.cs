using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ElixirHealth : ActiveEffect {

	private double percentHealthIncrease;
	private int changeInMaxHP;

	public ElixirHealth(CharacterCharacter c)
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
		percentHealthIncrease=1.2;
		changeInMaxHP=Dround((c.type.maximumHealth*(percentHealthIncrease-1)));
		c.type.maximumHealth+=changeInMaxHP;
		c.currentHP=Dround((c.currentHP*percentHealthIncrease));
		type=ActiveEffect.EffectType.buff;
	}

	public override void Act()
	{
		
	}

	public override void Finish()
	{
		
		base.subject.type.maximumHealth-=changeInMaxHP;
		base.subject.currentHP=Dround((base.subject.currentHP*(1/percentHealthIncrease)));
		if(base.subject.currentHP<=0)
		{
			base.subject.currentHP=1;
		}
		if(base.subject.currentHP>base.subject.type.maximumHealth)
		{
			base.subject.currentHP=base.subject.type.maximumHealth;
		}
	}
	
	int Dround(double d)
	{
		return (int)Math.Round(d);
	}
}
