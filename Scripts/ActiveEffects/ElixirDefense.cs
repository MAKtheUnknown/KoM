using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElixirDefense : ActiveEffect {

	private int changeInDefense;

	public ElixirDefense(CharacterCharacter c)
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
		c.type.defense+=changeInDefense;
		changeInDefense=6;
	}

	public override void Act()
	{
		
	}

	public override void Finish()
	{
		subject.type.defense-=changeInDefense;
	}
}
