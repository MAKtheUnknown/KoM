using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poisoned : TileEffect {
	
	private double DOT;

	public Poisoned(TileAttributes t, CharacterCharacter c, double d)
	{
		Init (t, c, d);
	}

	public void Init(TileAttributes t, CharacterCharacter c, double d)
	{
		this.Init (t, c, d, 3);
	}

	public void Init(TileAttributes t, CharacterCharacter c, double d, int rounds)
	{
		base.Init (t, c, rounds);
		DOT=d;
		source=c;
	}

	public override void Act()
	{
		if(base.subject.containedCharacter!=null&&base.subject.containedCharacter.team!=base.source.team)
		{
			base.subject.containedCharacter.damage(DOT);
		}
	}

	public override void Finish()
	{
		
	}
}
