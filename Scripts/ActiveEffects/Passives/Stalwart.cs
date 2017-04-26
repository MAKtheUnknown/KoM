using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stalwart : Passive  {

	private double changeInDefense=.15;
	bool done =false;

	public Stalwart(CharacterCharacter c)
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
		c.team.teamDefense+=changeInDefense;
	}
	public override void Check()
	{
		if(base.subject.currentHP==0)
		{
			if(!done)
			{
				subject.team.teamDefense-=changeInDefense;
				done=true;
			}			
		}
	}
}
