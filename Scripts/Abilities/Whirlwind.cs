using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlwind : MonoBehaviour, Ability 
{

	string name = "Whirlwind";
	string description = "";

	ClassSpecifications specs;
	TileArrangement map;

	// Use this for initialization
	public void Start () 
	{
		specs = GetComponentInParent<ClassSpecifications> ();
	}
	
	// Update is called once per frame
	public void Update () 
	{
		
	}

	public string GetName()
	{
		return name;
	}

	public string GetDescription()
	{
		return description;
	}

	public void Use()
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
