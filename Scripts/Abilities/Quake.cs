using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Quake : TileTargeter  {

	public ClassSpecifications specs;
	public float range;
	public int size;

	// Use this for initialization
	public override void Start () 
	{
		specs = GetComponentInParent<ClassSpecifications> ();
		map = GameObject.FindGameObjectWithTag ("Map").GetComponent<TileArrangement>();
		GameObject.Destroy (GameObject.FindGameObjectWithTag("Ability Selector"));
		targets = new List<TileAttributes> ();
		tileTargets = new List<TileAttributes> ();
		targetsAquired = false;
		targetsToAquire = numberOfTargets;
		base.Start ();
	}
	
	public void Awake()
	{
		
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
			base.GetTargets(specs.owner.x, specs.owner.y, range);
		}
		if (targetsAquired == true) 
		{
			base.GetFullTargets(size);
			foreach(TileAttributes t in fullTileTargets)
			{
				if(t.containedCharacter!=null&&t.containedCharacter.team!=specs.owner.team)
					t.containedCharacter.activeEffects.Add(new Stunned(t.containedCharacter));
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
		tileTargets= new List<TileAttributes>();
		tileTargets.Add(target.tile);
		base.GetFullTargets(size);
		foreach(TileAttributes t in fullTileTargets)
		{
			if(t.containedCharacter!=null&&t.containedCharacter.team!=specs.owner.team)
					t.containedCharacter.activeEffects.Add(new Stunned(t.containedCharacter));
		}
		
		
		Start ();
		specs.owner.usedAbility = true;
		cooldownTimer=cooldown;
	}
}
