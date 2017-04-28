using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guarded : ActiveEffect {
	
	int changeInDefense;

	public Guarded(CharacterCharacter c)
	{
		Init (c);
	}
	
	public Guarded(CharacterCharacter c, double r)
	{
		this.Init(c,1,r);
	}
	
	public void Init(CharacterCharacter c)
	{
		this.Init (c, 1);
	}

	public new void Init(CharacterCharacter c, int rounds)
	{
		base.Init (c, rounds);
		changeInDefense=9001;
		c.type.defense+=changeInDefense;
		type=ActiveEffect.EffectType.buff;
	}

	//r is a supposed to a percent (r=.7 := 70% damage reduction)
	public void Init(CharacterCharacter c, int rounds, double r)
	{
		base.Init (c, rounds);
		changeInDefense=(int)(r*50);
		c.type.defense+=changeInDefense;
		type=ActiveEffect.EffectType.buff;
	}
	
	//d is the flat increase of a unit's defense
	public void Init(CharacterCharacter c, int rounds, int d)
	{
		base.Init (c, rounds);
		changeInDefense=d;
		c.type.defense+=changeInDefense;
		type=ActiveEffect.EffectType.buff;
	}

	public override void Act()
	{
		
	}

	public override void Finish()
	{
		base.subject.type.defense-=changeInDefense;
	}
}
