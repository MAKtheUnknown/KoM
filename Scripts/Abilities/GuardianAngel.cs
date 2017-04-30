using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GuardianAngel : CharachterTargeter  {

	public ClassSpecifications specs;

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
		range=specs.range;
		if (targetsAquired == false) 
		{
			base.GetAllyTargets(specs.owner.x, specs.owner.y, range, specs.owner.team);
		}
		if (targetsAquired == true) 
		{
			foreach (CharacterCharacter c in charachterTargets) 
			{
				c.activeEffects.Add(new Guarded(c));
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
		int count =0;
			
		foreach(CharacterCharacter c in base.GetTargetsInRange(specs.owner.x,specs.owner.y,specs.range))
		{
			if(c.team==specs.owner.team&&count<numberOfTargets)
			{
				charachterTargets.Add(c);
				count++;
			}
			else
			{
				foreach(CharacterCharacter c2 in charachterTargets)
				{
					if(c.team==specs.owner.team&&c.currentHP<c2.currentHP)
					{
						charachterTargets.Remove(c2);
						charachterTargets.Add(c);
						break;
					}
				}
			}
		}
		
		foreach (CharacterCharacter c in charachterTargets) 
		{
			c.activeEffects.Add(new Guarded(c));
		}
		Start ();
		specs.owner.usedAbility = true;
		cooldownTimer=cooldown;
		
	}
	
}
