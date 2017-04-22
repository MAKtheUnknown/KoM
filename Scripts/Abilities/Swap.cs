using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Swap : CharachterTargeter {

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
		//Can only target 1 unit.
		targetsToAquire=1;
		numberOfTargets=1;
		damage=specs.attack;
		if (targetsAquired == false) 
		{
			base.GetOtherAllyTargets(specs.owner.x, specs.owner.y, range, specs.owner.team);
		}
		if (targetsAquired == true) 
		{
			SwapPositions(specs.owner,charachterTargets[0]);
			
			map.highlighter.mode = Highlighter.SelectionMode.PIECE_TO_USE;
			Start ();
			specs.owner.usedAbility = true;
			cooldownTimer=cooldown;
		}

	}
	
	public override void AIUse(CharacterCharacter target)
	{
		//Can only target 1 unit.
		targetsToAquire=1;
		numberOfTargets=1;
		damage=specs.attack;
		int count=0;
		charachterTargets= new List<CharacterCharacter>();
		foreach(CharacterCharacter c in base.GetTargetsInRange(specs.owner.x,specs.owner.y,specs.range))
		{
			if(c.team!=specs.owner.team&&count<numberOfTargets)
			{
				charachterTargets.Add(c);
				count++;
			}
		}
		
		
		SwapPositions(specs.owner,charachterTargets[0]);
		
		Start ();
		specs.owner.usedAbility = true;
		cooldownTimer=cooldown;
	}
	
	void SwapPositions(CharacterCharacter c1, CharacterCharacter c2)
	{/*
		t.containedCharacter = moved.owner;
		moved.owner.tile.containedCharacter = null;
		moved.owner.tile = t;
		moved.owner.transform.position = t.transform.position;
		moved.owner.x = t.x;
		moved.owner.y = t.y;
		*/
		
		TileAttributes[] tiles={c1.tile,c2.tile};
		int[] positionOfC1={c1.x,c1.y};
		c1.tile=tiles[1];
		c2.tile=tiles[0];
		c1.tile.containedCharacter=c1;
		c2.tile.containedCharacter=c2;
		c1.transform.position = tiles[1].transform.position;
		c2.transform.position = tiles[0].transform.position;
		c1.x=c2.x;
		c1.y=c2.y;
		c2.x=positionOfC1[0];
		c2.y=positionOfC1[1];
	}
}
