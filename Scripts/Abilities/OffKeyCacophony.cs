using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class OffKeyCacophony : CharachterTargeter {

	public ClassSpecifications specs;

	public double damage;
	public float range;

	// Use this for initialization
	public override void Start () 
	{
		specs = GetComponentInParent<ClassSpecifications> ();
		map = GameObject.FindGameObjectWithTag ("Map").GetComponent<TileArrangement>();
		targets = new List<TileAttributes> ();
		charachterTargets = new List<CharacterCharacter> ();
		targetsAquired = false;
		targetsToAquire = numberOfTargets;
		GameObject.Destroy (GameObject.FindGameObjectWithTag("Ability Selector"));
		base.Start ();
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
		damage=specs.attack;
		if (targetsAquired == false) 
		{
			base.GetTargets(specs.owner.x, specs.owner.y, range);
		}
		if (targetsAquired == true) 
		{
			foreach (CharacterCharacter c in charachterTargets) 
			{
				TileAttributes tile;
				int x=(c.x-specs.owner.x)/Math.Max(Math.Abs(c.x-specs.owner.x),1);
				int y=(c.y-specs.owner.x)/Math.Max(Math.Abs(c.x-specs.owner.x),1);
				c.x+=x;
				c.y+=y;
				tile = specs.owner.team.map.tileMap[c.x, c.y];
				specs.owner.tile.containedCharacter = null;
				specs.owner.tile = tile;
				tile.containedCharacter = c;
				specs.owner.transform.position = tile.transform.position;
				/*
				specs.owner.x = t.x;
				specs.owner.y = t.y;
				*/
				c.damage (damage);
			}
			map.highlighter.mode = Highlighter.SelectionMode.PIECE_TO_USE;
			Start ();
			specs.owner.usedAbility = true;
		}

	}
}
