using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reckoned : ActiveEffect {
	private int changeInDefense;


	public Reckoned(CharacterCharacter c)
	{
		Init (c);
	}

	public void Init(CharacterCharacter c)
	{
		this.Init (c, 1);
	}

	public new void Init(CharacterCharacter c, int rounds)
	{
		base.Init (c, rounds);
		if(c.type.defense<10)
			changeInDefense=c.type.defense;
		else
			changeInDefense=10;
		c.type.defense-=changeInDefense;
	}

	public override void Act()
	{
		
		subject.usedAbility=true;
	}

	public override void Finish()
	{
		subject.type.defense+=changeInDefense;
	}
}
