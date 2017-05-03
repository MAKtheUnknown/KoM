using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Poisoned : ActiveEffect
{
	//We still don't know what the design team meant by -1 movement tile, so I'm just gonna use 3 as a plug
	double DOT;
	
	CharacterCharacter c;

	public Poisoned (CharacterCharacter c, double d, int r)
	{
		Init(c, d, r);
	}


	public void Init(CharacterCharacter c, double d, int rounds)
	{
		this.c = c;
		base.Init (c, rounds);
		DOT=d;
		SpriteRenderer sr = c.gameObject.GetComponent<SpriteRenderer> ();
		sr.color = new Color (0, 1, 0, 1);
	}

	public override void Act()
	{
		subject.damage(DOT);
	}

	public override void Finish()
	{
		SpriteRenderer sr = c.gameObject.GetComponent<SpriteRenderer> ();
		sr.color = new Color (1, 1, 1, 1);
	}
	
	public override ActiveEffect Clone(CharacterCharacter c)
	{
		return new Poisoned(subject, DOT, turnsLeft);
	}
}


