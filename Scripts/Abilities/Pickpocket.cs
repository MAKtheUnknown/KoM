using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Pickpocket : CharachterTargeter {

	public ClassSpecifications specs;

	public float range;
	List<ActiveEffect> buffs;

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
		buffs= new List<ActiveEffect>();
		//Can only target 1 unit.
		range=specs.range;
		if (targetsAquired == false) 
		{
			base.GetTargets(specs.owner.x, specs.owner.y, range);
		}
		if (targetsAquired == true) 
		{
			foreach (CharacterCharacter c in charachterTargets)
			{
				if(c.team==specs.owner.team)
				{					
					TakeDebuffs(c);
					cooldown=4;
				}
				if(c.team!=specs.owner.team)
				{
					TakeBuffs(c);
					cooldown=6;
				}
				TransferEffects(c);
			}
			
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
		
		foreach (CharacterCharacter c in charachterTargets)
		{
			if(c.team==specs.owner.team)
				TakeDebuffs(c);
			if(c.team!=specs.owner.team)
				TakeBuffs(c);
			TransferEffects(c);
		}
		
		Start ();
		specs.owner.usedAbility = true;
		cooldownTimer=cooldown;
	}
	
	void TakeBuffs(CharacterCharacter c)
	{
		foreach(ActiveEffect e in c.activeEffects)
		{
			if(e.type==ActiveEffect.EffectType.buff)
			{
				buffs.Add(e);
			}
		}
		
		cooldown=4;
	}
	
	void TakeDebuffs(CharacterCharacter c)
	{
		foreach(ActiveEffect e in c.activeEffects)
		{
			if(e.type==ActiveEffect.EffectType.debuff)
			{
				buffs.Add(e);
			}
			
		}	

		cooldown=6;
	}
	
	void TransferEffects(CharacterCharacter c)
	{
		foreach(ActiveEffect e in buffs)
		{
			e.Finish();
			c.activeEffects.Remove(e);
			
			specs.owner.activeEffects.Add(e.Clone(specs.owner));
		}
		
	}
}
