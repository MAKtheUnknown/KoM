using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BardLinked : ActiveEffect {
	private List<CharacterCharacter> linked= new List<CharacterCharacter>();


	public BardLinked(CharacterCharacter c, List<CharacterCharacter> l)
	{
		Init (c, l);
	}

	public void Init(CharacterCharacter c, List<CharacterCharacter> l)
	{
		this.Init (c, l,3);
	}

	public void Init(CharacterCharacter c, List<CharacterCharacter> l, int rounds)
	{
		base.Init (c, rounds);
		linked=l;
			foreach(CharacterCharacter d in linked)
			{
				foreach(CharacterCharacter t in linked)
				{
					if(t!=d)
						d.bardLinked.Add(t);
				}
			}
	}

	public override void Act()
	{	
		bool active=true;
		base.turnsLeft++;
		foreach(Ability a in subject.type.classAbilities)
		{
			if(a.ultimate==true)
				a.cooldownTimer++;
		}
		foreach(CharacterCharacter c in linked)
		{
			if(c==null)
			{
				active=false;
			}
		}
		if(!active)
		{
			this.Finish();
		}
		
		
			
	}

	public override void Finish()
	{
		
		foreach(Ability a in subject.type.classAbilities)
		{
			if(a.ultimate==true)
				a.cooldownTimer--;
		}
		foreach(CharacterCharacter c in linked)
			{
				foreach(CharacterCharacter t in c.bardLinked)
				{
					foreach(CharacterCharacter d in linked)
					{
						bool occured=false;
						if(t.bardLinked.Contains(d)&&occured==false)
						{
							t.bardLinked.Remove(d);
							occured=true;
						}
						
					}
				}
					
			}
	}
}
