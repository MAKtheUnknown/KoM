using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shielded : ActiveEffect {

	public Shielded(CharacterCharacter c)
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
		c.shielded=true;
	}

	public override void Act()
	{
		
	}

	public override void Finish()
	{
		subject.shielded=false;
	}
}
