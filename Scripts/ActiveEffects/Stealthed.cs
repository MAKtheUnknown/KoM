using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealthed : ActiveEffect {
	
	double damageMultiplyer=1.3;
	int changeInAttack;

	public Stealthed(CharacterCharacter c)
	{
		this.Init(c,2);
	}
	
	public Stealthed(CharacterCharacter c, int r)
	{
		this.Init(c,r);
	}
	
	/** Stealthed Targets are not targetable, integrated into CharacterTargeter, in "GetTargetsInRange" method**/
	
	public override void Init(CharacterCharacter c, int rounds)
	{
		base.Init (c, rounds);
		changeInAttack=(int)(damageMultiplyer*c.type.attack);
		c.type.attack+=changeInAttack;
		type=ActiveEffect.EffectType.buff;
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
		base.subject.type.attack-=changeInAttack;
	}
	
	public override ActiveEffect Clone(CharacterCharacter c)
	{
		return new Stealthed(c,turnsLeft);
	}
}
