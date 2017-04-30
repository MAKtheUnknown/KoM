using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonedTile : TileEffect {
	
	private double DOT;

	public PoisonedTile(TileAttributes s, Team t, double d)
	{
		Init (s, t, d);
	}

	public void Init(TileAttributes s, Team t, double d)
	{
		this.Init (s, t, d, 3);
	}

	public void Init(TileAttributes s, Team t, double d, int rounds)
	{
		base.Init (s, t, rounds);
		DOT=d;
	}

	public override void Act()
	{
		if(base.subject.containedCharacter!=null&&base.subject.containedCharacter.team!=base.source)
		{
			base.subject.containedCharacter.damage(DOT);
		}
	}

	public override void Finish()
	{
		
	}
}
