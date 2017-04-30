using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonedDagger : CharachterTargeter
{

	ClassSpecifications specs;
	double damage;
	public int range;

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
		name = "PoisonedDagger";
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
		damage=specs.attack/3.0;
		range=specs.range;
		if (targetsAquired == false) 
		{
			base.GetEnemyTargets(specs.owner.x, specs.owner.y, range, specs.owner.team);
		}
		if (targetsAquired == true) 
		{
			foreach (CharacterCharacter c in charachterTargets) 
			{
				c.activeEffects.Add(new Poisoned(c,damage,3));
			}
			map.highlighter.mode = Highlighter.SelectionMode.PIECE_TO_USE;
			Start ();
			specs.owner.usedAbility = true;
			cooldownTimer=cooldown;
		}

	}
	
	public override void AIUse(CharacterCharacter target)
	{
		damage=specs.attack/3.0;
		range=specs.range;
		target.activeEffects.Add(new Poisoned(target,damage,3));
		Start ();
		specs.owner.usedAbility = true;
		cooldownTimer=cooldown;		
	}

}
