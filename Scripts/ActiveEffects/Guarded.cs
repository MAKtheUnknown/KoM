using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guarded : ActiveEffect {

	public Guarded(CharacterCharacter c)
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
		subject.guarded=true;
	}

	public override void Act()
	{
		
	}

	public override void Finish()
	{
		subject.guarded=false;
	}
}
