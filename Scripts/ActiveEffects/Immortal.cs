using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Immortal : Passive {

	private int baseDefense;
	int health;

	public Immortal(CharacterCharacter c)
	{
		Init (c);
		health=c.currentHP;
		baseDefense=c.type.defense;
	}
	
	public Immortal(CharacterCharacter c, int r)
	{
		Init (c,r);
		baseDefense=c.type.defense;
		health=c.currentHP;
	}

	public override bool Condition()
	{
		return (health!=subject.currentHP);
	}
	
	public override void Activate()
	{
		subject.type.defense=(int)(75.0*(.75-(1.0*subject.currentHP/subject.type.maximumHealth)*.75))+baseDefense;
		health=subject.currentHP;
	}
	
	public override ActiveEffect Clone(CharacterCharacter c)
	{
		return new Immortal(c,turnsLeft);
	}
}
