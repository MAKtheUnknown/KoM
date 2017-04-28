using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kaiyufied : ActiveEffect {

	private string originalName;

	public Kaiyufied(CharacterCharacter c)
	{
		Init (c);
	}

	public void Init(CharacterCharacter c)
	{
		this.Init (c, 9001);
	}

	public new void Init(CharacterCharacter c, int rounds)
	{
		base.Init (c, rounds);
		originalName = c.name;
		c.name = "Kaiyu";
		c.team.TeamDamage(3.0);
	}

	public override void Act()
	{
		
	}

	public override void Finish()
	{
		subject.name = originalName;
	}
}
