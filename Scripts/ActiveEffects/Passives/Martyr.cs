using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Martyr : Passive {

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
		base.turnsLeft++;
	}

	public override void Check()
	{
		if(!done&&base.subject.currentHP<=0)
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
