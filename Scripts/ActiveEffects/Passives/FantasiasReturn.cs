using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FantasiasReturn : Passive {

	private bool ultCD;
	Ability ultimate;

	public FantasiasReturn(CharacterCharacter c)
	{
		Init (c);
	}

	public void Init(CharacterCharacter c)
	{
		this.Init (c, 100);
	}

	public new void Init(CharacterCharacter c, int rounds)
	{
		base.Init (c, rounds);
		ultCD=false;
		foreach(Ability a in c.specialAbilities)
		{
			if(a.ultimate)
				ultimate=a;
		}
	}

	public override void Act()
	{
	}
	public override bool Condition()
	{
		return ultCD!=(ultimate.cooldownTimer<=0);
	}
	public override void Activate()
	{
		if(ultimate.cooldownTimer<=0)
		{
			foreach(Ability a in subject.specialAbilities)
			{
				a.cooldown++;
			}
		}
		if(ultimate.cooldownTimer>0)
		{
			foreach(Ability a in subject.specialAbilities)
			{
				a.cooldown--;
			}
		}
		
		ultCD=!(ultimate.cooldownTimer<=0);
	}
	
	public override ActiveEffect Clone(CharacterCharacter c)
	{
		return new FantasiasReturn(c);
	}
}
