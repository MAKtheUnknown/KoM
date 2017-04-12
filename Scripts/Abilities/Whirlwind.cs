using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlwind : Ability 
{

	string name = "Whirlwind";
	string description = "";

	ClassSpecifications specs;
	TileArrangement map;

	// Use this for initialization
	public override void Start () 
	{
		specs = GetComponentInParent<ClassSpecifications> ();
	}
	
	// Update is called once per frame
	public override void Update () 
	{
		
	}

	public override string GetName()
	{
		return name;
	}

	public override string GetDescription()
	{
		return description;
	}

	public override void Use()
	{
		TileAttributes t = specs.owner.tile;
		if (t.north != null && t.north.containedCharacter != null)
		{
			t.north.containedCharacter.damage (1);
		}
		if (t.south != null && t.south.containedCharacter != null)
		{
			t.south.containedCharacter.damage (1);
		}
		if (t.east != null && t.east.containedCharacter != null)
		{
			t.east.containedCharacter.damage (1);
		}
		if (t.west != null && t.west.containedCharacter != null)
		{
			t.west.containedCharacter.damage (1);
		}

		map = specs.owner.tile.map;
		map.highlighter.mode = Highlighter.SelectionMode.PIECE_TO_USE;
		specs.owner.usedAbility = true;
	}
}
