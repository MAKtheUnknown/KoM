using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IncomprehensibleRage : Passive {
	
	int attackBonus=0;
	int attackPerKill=3;
	ClassSpecifications specs;
	bool justUsed;
	int remainingEnemies;

	public IncomprehensibleRage(CharacterCharacter c)
	{
		Init (c);
	}
	
	public override bool Condition()
	{
		return specs.owner.usedAbility!=justUsed;
	}
	
	public override void Activate()
	{
			if(remainingEnemies!=this.CurrentEnemyCount())
			{
				this.OnKill();
				remainingEnemies=this.CurrentEnemyCount();
			}
			justUsed=true;
		
	}

	public override void Init(CharacterCharacter c)
	{
		this.Init (c, 100);
	}

	public new void Init(CharacterCharacter c, int rounds)
	{
		base.Init (c, rounds);
		specs=c.type;
		justUsed=false;
		remainingEnemies=this.CurrentEnemyCount();
	}

	public override void Act()
	{
		base.turnsLeft++;
		justUsed=false;
	}

	public override void Finish()
	{
		
	}
	
	public int Reset()
	{
		int kills = attackBonus/attackPerKill;
		specs.attack-=attackBonus;
		attackBonus=0;
		return kills;
	}
	
	void OnKill()
	{
		attackBonus+=attackPerKill;
		specs.attack+=attackPerKill;
	}
	
	int CurrentEnemyCount()
	{
		int sum=0;
		foreach(Team t in specs.owner.team.manager.teams)
		{
			if(t!=specs.owner.team)
				sum+=t.activePieces;
		}
		return sum;
	}
	
	public int getAttackBonus()
	{
		return attackBonus;
	}
	
	public override ActiveEffect Clone(CharacterCharacter c)
	{
		return new IncomprehensibleRage(c);
	}
}
