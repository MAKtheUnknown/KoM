using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InspiringPresence : CharachterTargeter  {

	public ClassSpecifications specs;

	public float range;

	// Use this for initialization
	public override void Start () 
	{
		name = "Inspiring Presence";
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
		range=1;
		foreach (CharacterCharacter c in specs.owner.team.pieces) 
		{
			int rsqrd = (int)(range * range);
			int dxsqrd = (specs.owner.x - c.x) * (specs.owner.x - c.x);
			int dysqrd = (specs.owner.y - c.y) * (specs.owner.y - c.y);
			
			if (rsqrd >= dxsqrd + dysqrd) 
			{
				c.activeEffects.Add (new Inspired (c));
			}
		}
			map.highlighter.mode = Highlighter.SelectionMode.PIECE_TO_USE;
			Start ();
			specs.owner.usedAbility = true;
			cooldownTimer=cooldown;
		

	}
	
	public override bool Available()
	{
		range=1;
		bool found=false;
		foreach (CharacterCharacter c in specs.owner.team.pieces) 
		{
			int rsqrd = (int)(range * range);
			int dxsqrd = (specs.owner.x - c.x) * (specs.owner.x - c.x);
			int dysqrd = (specs.owner.y - c.y) * (specs.owner.y - c.y);
			bool b = !(specs.owner.x == c.x && specs.owner.y == c.y);
			if ((rsqrd >= dxsqrd + dysqrd) && b) 
			{
				found=true;
			}
		}
		return found;
		
	}
	
	public override void AIUse(CharacterCharacter target)
	{
		this.Use();
	}
}
