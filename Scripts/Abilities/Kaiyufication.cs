using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Kaiyufication : Ability {

	public string name = "Kaiyufication";
	public string description;
	// Use this for initialization
	public void Start () 
	{
	
	}
	
	// Update is called once per frame
	public void Update () {
	
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
		/*TileAttributes tileTarget = ts [0];
		if (tileTarget.containedCharacter != null) 
		{
			CharacterCharacter attackedCharachter = tileTarget.containedCharacter;
			attackedCharachter.name = "Kaiyu"; //Caillou
		}*/
	}
}
