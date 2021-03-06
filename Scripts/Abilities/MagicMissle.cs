﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MagicMissle : CharachterTargeter {

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
		float m= Random.Range(1.2f,2f);
		damage=specs.attack;
		range=specs.range;
		if (targetsAquired == false) 
		{
			base.GetEnemyTargets(specs.owner.x, specs.owner.y, range, specs.owner.team);
		}
		if (targetsAquired == true) 
		{
			foreach (CharacterCharacter t in charachterTargets) 
			{
				/** theif passive**/
				if(!t.type.passive.GetType().Equals(typeof(AcrobaticLeap))  ||  Random.Range(0.0f,1.0f)>.25)
				t.damage (m*damage);
			}
			map.highlighter.mode = Highlighter.SelectionMode.PIECE_TO_USE;
			Start ();
			specs.owner.usedAbility = true;
			cooldownTimer=cooldown;
		}

	}
	
	public override void AIUse(CharacterCharacter target)
	{
		float m= Random.Range(1.2f,2f);
		int count=0;
		charachterTargets= new List<CharacterCharacter>();
		foreach(CharacterCharacter c in base.GetTargetsInRange(specs.owner.x,specs.owner.y,specs.range))
		{
			if(c!=null&c.team!=specs.owner.team&&count<numberOfTargets)
			{
				charachterTargets.Add(c);
				count++;
			}
		}
		
		
		foreach (CharacterCharacter t in charachterTargets) 
		{
			/** theif passive**/
			if(!t.type.passive.GetType().Equals(typeof(AcrobaticLeap))  ||  Random.Range(0.0f,1.0f)>.25)
			t.damage (m*damage);
		}
		Start ();
		specs.owner.usedAbility = true;
		cooldownTimer=cooldown;
	}
}
