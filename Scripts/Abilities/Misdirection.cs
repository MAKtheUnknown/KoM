using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misdirection : CharachterTargeter
{

	ClassSpecifications specs;
	public int extraDamage;
	public int range;
	public int turns;
	public int movementReduction;

	// Use this for initialization
	public override void Start () 
	{
		specs = GetComponentInParent<ClassSpecifications> ();
		name = "Misdirection";
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
		if (targetsAquired == false) 
		{
			base.GetEnemyTargets(specs.owner.x, specs.owner.y, range, specs.owner.team);
		}
		if (targetsAquired == true) 
		{
			foreach (CharacterCharacter c in charachterTargets) 
			{
				c.activeEffects.Add(new Slowed(c,turns,movementReduction));
			}
			map.highlighter.mode = Highlighter.SelectionMode.PIECE_TO_USE;
			Start ();
			specs.owner.usedAbility = true;
			cooldownTimer=cooldown;
		}

	}
	
	public override void AIUse(CharacterCharacter target)
	{
		range=specs.range;
		target.activeEffects.Add(new Slowed(target,turns,movementReduction));
		Start ();
		specs.owner.usedAbility = true;
		cooldownTimer=cooldown;		
	}

}
