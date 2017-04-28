using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TacticalManeuver : Passive {
	
	ClassSpecifications specs;
	bool justUsed;
	float[] allTeamMorale;

	public void TactiacalManeuver(CharacterCharacter c)
	{
		Init (c);
	}
	
	public override void Activate()
	{
		if(allTeamMorale!=UpdateTeamMorale())
		{
			this.OnKill(allTeamMorale,UpdateTeamMorale());
			allTeamMorale=UpdateTeamMorale();
		}
		justUsed=true;
	}
	
	public override bool Condition()
	{
		return specs.owner.usedAbility!=justUsed;
	}

	public void Init(CharacterCharacter c)
	{
		this.Init (c, 100);
	}

	public new void Init(CharacterCharacter c, int rounds)
	{
		base.Init (c, rounds);
		specs=c.type;
		justUsed=false;
		allTeamMorale=UpdateTeamMorale();
	}

	public override void Act()
	{
		base.turnsLeft++;
		justUsed=false;
		allTeamMorale=UpdateTeamMorale();
	}

	public override void Finish()
	{
		
	}
	
	void OnKill(float[] old, float[] current)
	{
		for(int i = 0; i<old.Length; i++)
		{
			specs.owner.team.manager.teams[i].TeamDamage(old[i]-current[i]);
		}
	}
	
	float[] UpdateTeamMorale()
	{
		float[] teamsMorale=new float[specs.owner.team.manager.teams.Length];
		for(int i = 0; i<teamsMorale.Length;i++)
		{
			teamsMorale[i]=specs.owner.team.manager.teams[i].teamMorale;
		}
		
		return teamsMorale;
		
	}
}
