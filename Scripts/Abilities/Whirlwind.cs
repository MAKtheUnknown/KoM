using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlwind : MonoBehaviour, Ability 
{

	string name = "Whirlwind";
	string description = "";

	ClassSpecifications specs;

	// Use this for initialization
	public void Start () 
	{
		specs = GetComponentInParent<ClassSpecifications> ();
	}
	
	// Update is called once per frame
	public void Update () 
	{
		
	}

	public string GetName()
	{
		return name;
	}

	public string GetDescription()
	{
		return description;
	}

	public void Use()
	{
		TileAttributes t = specs.owner.tile;
		t.north.containedCharacter.damage (1);
		t.south.containedCharacter.damage (1);
		t.east.containedCharacter.damage (1);
		t.west.containedCharacter.damage (1);
	}
}
