using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShieldBearer : CharachterTargeter  {

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
		foreach (CharacterCharacter c in specs.owner.team.pieces) 
		{
			c.activeEffects.Add(new Guarded(c,.35));
		}
		map.highlighter.mode = Highlighter.SelectionMode.PIECE_TO_USE;
		Start ();
		specs.owner.usedAbility = true;
		cooldownTimer=cooldown;

	}
	
	public override void AIUse(CharacterCharacter target)
	{
		this.Use();		
	}
	
}
