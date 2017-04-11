using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cripple : MonoBehaviour, Ability
{

	string name = "Cripple";
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
		
	}

}
