using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Sheepified : ActiveEffect {
	int changeInAttack;
	int changeInHealth;
	int changeInDefense;
	double percentHealthDecrease;

	public Sheepified(CharacterCharacter c)
	{
		this.Init(c,8);
	}
	
	public Sheepified(CharacterCharacter c, int r)
	{
		this.Init(c,r);
	}
	
	/** Stealthed Targets are not targetable, integrated into CharacterTargeter, in "GetTargetsInRange" method**/
	
	public override void Init(CharacterCharacter c, int rounds)
	{
		base.Init (c, rounds);
		changeInAttack=c.type.attack-2;
		c.type.attack=2;
		
		
		percentHealthDecrease=.5;
		changeInHealth=Dround((c.type.maximumHealth*(percentHealthDecrease-1)));
		c.type.maximumHealth+=changeInHealth;
		c.currentHP=Dround((c.currentHP*percentHealthDecrease));
		
		changeInDefense=Dround((c.type.defense*(percentHealthDecrease-1)));
		c.type.defense+=changeInDefense;
		
		type=ActiveEffect.EffectType.debuff;
	}

	public override void Act()
	{
	}
	
	public void Update()
	{
		if(base.subject.usedAbility)
		{
			Finish();
			base.subject.activeEffects.Remove(this);
		}
	}

	public override void Finish()
	{
		base.subject.type.attack+=changeInAttack;
		base.subject.type.defense-=changeInDefense;
		base.subject.type.maximumHealth-=changeInHealth;
		base.subject.currentHP=Dround((base.subject.currentHP*(1/percentHealthDecrease)));
		if(base.subject.currentHP<=0)
		{
			base.subject.currentHP=1;
		}
		if(base.subject.currentHP>base.subject.type.maximumHealth)
		{
			base.subject.currentHP=base.subject.type.maximumHealth;
		}
	}
	
	public override ActiveEffect Clone(CharacterCharacter c)
	{
		return new Sheepified(c,turnsLeft);
	}
	
	int Dround(double d)
	{
		return (int)Math.Round(d);
	}
}
