using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cripple : Ability
{

	ClassSpecifications specs;

	// Use this for initialization
	public override void Start () 
	{
		specs = GetComponentInParent<ClassSpecifications> ();
		name = "Cripple";
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
		
	}

}
