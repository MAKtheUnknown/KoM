using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongOfReckoning : CharachterTargeter {


	public double damage;
	public int range;

	public ClassSpecifications specs;

	// Use this for initialization
	public override void Start () 
	{
		name="Song of Reckoning";
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
				t.damage (damage);
				t.activeEffects.Add(new Reckoned(t));
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
			t.damage (damage);
			t.activeEffects.Add(new Reckoned(t));
		}
		Start ();
		specs.owner.usedAbility = true;
		cooldownTimer=cooldown;
	}
}
