using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SmokeScreen : CharachterTargeter  {

	public ClassSpecifications specs;

	public float range;

	// Use this for initialization
	public override void Start () 
	{
		base.ultimate=true;
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
			base.GetAllyTargets(specs.owner.x, specs.owner.y, range,specs.owner.team);
		}
		if (targetsAquired == true) 
		{
			foreach (CharacterCharacter c in charachterTargets) 
			{
				c.activeEffects.Add(new Stealthed(c));
			}
			map.highlighter.mode = Highlighter.SelectionMode.PIECE_TO_USE;
			Start ();
			specs.owner.usedAbility = true;
			cooldownTimer=cooldown;
		}

	}
	
	public override void AIUse(CharacterCharacter target)
	{
		int count=0;
		range=specs.range;
		
		charachterTargets= new List<CharacterCharacter>();
		foreach(CharacterCharacter c in base.GetAllTargetsInRange(specs.owner.x,specs.owner.y,specs.range))
		{
			if(c.team==specs.owner.team&&count<numberOfTargets)
			{
				charachterTargets.Add(c);
				count++;
			}
		}
		
		foreach (CharacterCharacter c in charachterTargets) 
		{
				c.activeEffects.Add(new Stealthed(c));
		}
		
		Start ();
		specs.owner.usedAbility = true;
		cooldownTimer=cooldown;
	}
	
}
