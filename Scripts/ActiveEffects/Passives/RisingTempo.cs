﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingTempo : Passive {

	private int turnCounter;

	public RisingTempo(CharacterCharacter c)
	{
		Init (c);
	}

	public override void Init(CharacterCharacter c)
	{
		this.Init (c, 100);
	}

	public new void Init(CharacterCharacter c, int rounds)
	{
		base.Init (c, rounds);
		turnCounter=1;
	}

	public override void Act()
	{
		base.turnsLeft++;
		turnCounter=(turnCounter+1)%4;
		if(turnCounter==0)
		{
			foreach(CharacterCharacter c in subject.team.pieces)
			if(c!=null)
			{
				foreach(Ability a in c.type.classAbilities)
				{
					if(a.ultimate!=true)
					{
						a.cooldownTimer--;						
					}
				}
			}
		}
	}
	public override bool Condition()
	{
		return false;
	}
	public override void Activate()
	{
		
	}
	
	public override ActiveEffect Clone(CharacterCharacter c)
	{
		return new RisingTempo(c);
	}
}
