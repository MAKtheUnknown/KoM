﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WellPlannedAttack : CharachterTargeter {

	public ClassSpecifications specs;

	public double damage;
	public float range;

	// Use this for initialization
	public override void Start () 
	{
		name="A Well-Planned Attack";
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
		range=specs.range;
		if (targetsAquired == false) 
		{
			base.GetEnemyTargets(specs.owner.x, specs.owner.y, range,specs.owner.team);
		}
		if (targetsAquired == true) 
		{
			foreach (CharacterCharacter t in charachterTargets) 
			{
				t.type.defense/=2;
				t.damage (damage);
				t.damage(damage);
				t.type.defense*=2;
			}
			map.highlighter.mode = Highlighter.SelectionMode.PIECE_TO_USE;
			Start ();
			specs.owner.usedAbility = true;
			cooldownTimer=cooldown;
		}

	}
	
	public override void AIUse(CharacterCharacter target)
	{
		damage=specs.attack;
		range=specs.range;
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
		
		
		foreach (CharacterCharacter t in charachterTargets) 
		{
			t.type.defense/=2;
			t.damage (damage);
			t.damage(damage);
			t.type.defense*=2;
		}
		Start ();
		specs.owner.usedAbility = true;
		cooldownTimer=cooldown;
	}
}
