using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Vent : CharachterTargeter  {

	public ClassSpecifications specs;
	// Percent of max health healed for each unit killed (1:= 100%)
	public double healPerKill;

	// Use this for initialization
	public override void Start () 
	{
		name = "Vent";
		specs = GetComponentInParent<ClassSpecifications> ();
		map = GameObject.FindGameObjectWithTag ("Map").GetComponent<TileArrangement>();
		targets = new List<TileAttributes> ();
		charachterTargets = new List<CharacterCharacter> ();
		targetsAquired = false;
		targetsToAquire = numberOfTargets;
		//GameObject.Destroy (GameObject.FindGameObjectWithTag("Ability Selector"));
		base.Start ();
		GameObject.Destroy (GameObject.FindGameObjectWithTag("Ability Selector"));
		ultimate=true;
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
		specs.owner.heal(((IncomprehensibleRage)specs.passive).Reset()*healPerKill*specs.maximumHealth);
		
		map.highlighter.mode = Highlighter.SelectionMode.PIECE_TO_USE;
		Start ();
		specs.owner.usedAbility = true;
		cooldownTimer=cooldown;
	}
	
	public override bool Available()
	{
		return (((IncomprehensibleRage)specs.passive).getAttackBonus()>0);
	}
	
	public override void AIUse(CharacterCharacter target)
	{
		this.Use();
	}
}
