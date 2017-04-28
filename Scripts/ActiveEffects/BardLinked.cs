using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BardLinked : ContinuousEffect {
	private List<CharacterCharacter> linked;
	int[] currentHP;
	CharacterCharacter user;

	/**c is the one linked, l are the units to whom c is linked, u is caster of effect**/
	public BardLinked(CharacterCharacter u, List<CharacterCharacter> l)
	{
		Init (u, l);
	}

	public void Init(CharacterCharacter u, List<CharacterCharacter> l)
	{
		this.Init (u, l,3);
	}

	public void Init(CharacterCharacter u, List<CharacterCharacter> l, int rounds)
	{
		base.Init (l[0], rounds);
		linked=l;
		user = u;
		currentHP=new int[l.Count];
	}

	public override void Act()
	{	
		base.turnsLeft++;
		foreach(Ability a in user.specialAbilities)
		{
			if(a.ultimate==true)
				a.cooldownTimer++;
		}
		foreach(CharacterCharacter c in linked)
		{
			if(c==null)
			{
				Finish();
				break;
			}
		}
	}

	public override void Finish()
	{
		if(user!=null)
		{
			foreach(Ability a in user.specialAbilities)
			{
				if(a.ultimate==true)
					a.cooldownTimer=3;
			}
			
		}
	}
	
	public override bool Condition()
	{
		return currentHP==CurrentHealth();
	}
	
	public override void Activate()
	{
		int[] updatedHealth = CurrentHealth();
		int[] changeInHealth= new int[updatedHealth.Length];
		for(int i = 0; i<changeInHealth.Length;i++)
		{
			changeInHealth[i]=currentHP[i]-updatedHealth[i];
		}
		for(int i = 0; i<changeInHealth.Length;i++)
		{
			DamageOthers(linked[i],changeInHealth[i]);
		}
		currentHP=updatedHealth;
		
		for(int i = 0; i<currentHP.Length;i++)
		{
			if(currentHP[i]==0)
			{
				Finish();
				base.subject.activeEffects.Remove(this);
				break;
			}
		}
	}
	
	public int[] CurrentHealth()
	{
		int[] updatedHealth= new int[linked.Count];
		for(int i = 0; i<linked.Count; i++)
		{
			updatedHealth[i]=linked[i].currentHP;
		}
		return updatedHealth;
	}
	
	public void DamageOthers(CharacterCharacter t, int d)
	{
		if(d>0) //when damage has been done
		{
			foreach(CharacterCharacter c in linked)
			{
				if(c!=t)
					c.damage(d);				
			}
		}
			
		if(d<0)
		{
			foreach(CharacterCharacter c in linked)
			{
				if(c!=t)
					c.heal(-d);				
			}
		}
	}
}
