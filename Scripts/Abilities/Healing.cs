using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : CharachterTargeter {


	public int addedHealth;

	public int range;

	public ClassSpecifications specs;

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



	public override void Use()
	{
		range=specs.range;
		addedHealth= specs.attack*2;
		if (targetsAquired == false) 
		{
			base.GetAllyTargets(specs.owner.x, specs.owner.y, range,specs.owner.team);
		}
		if (targetsAquired == true) 
		{
			foreach (CharacterCharacter t in charachterTargets) 
			{
				t.heal (addedHealth);
			}
			map.highlighter.mode = Highlighter.SelectionMode.PIECE_TO_USE;
			Start ();
			specs.owner.usedAbility = true;
			cooldownTimer=cooldown;
		}

	}
	
	public override void AIUse(CharacterCharacter target)
	{
		int count =0;
		addedHealth= specs.attack*2;
		range=specs.range;
			
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
		
		foreach (CharacterCharacter t in charachterTargets) 
		{
			t.heal (addedHealth);
		}
		Start ();
		specs.owner.usedAbility = true;
		cooldownTimer=cooldown;
		
	}
}
