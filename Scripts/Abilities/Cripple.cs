using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cripple : CharachterTargeter
{

	ClassSpecifications specs;
	int damage;
	public int extraDamage;
	public int range;

	// Use this for initialization
	public override void Start () 
	{
		specs = GetComponentInParent<ClassSpecifications> ();
		name = "Cripple";
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
			base.GetEnemyTargets(specs.owner.x, specs.owner.y, range, specs.owner.team);
		}
		if (targetsAquired == true) 
		{
			foreach (CharacterCharacter c in charachterTargets) 
			{
				c.damage (damage+extraDamage);
				c.activeEffects.Add(new Slowed(c));
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
		target.damage (damage+extraDamage);
		target.activeEffects.Add(new Slowed(target));
		Start ();
		specs.owner.usedAbility = true;
		cooldownTimer=cooldown;		
	}

}
