using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Martyr : ActiveEffect {

	private Team team;
	private int moraleDamage;
	bool done=false;

	public Martyr(CharacterCharacter c)
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
		moraleDamage=c.type.morale;
		team=c.team;
	}

	public override void Act()
	{
	}

	public override void Finish()
	{
		if(!done)
		{
			foreach(Team t in team.manager.teams)
			{
				if(t!=team)
				{
					t.TeamDamage(moraleDamage);
				}
			}
			done=true;
		}
	}
}
