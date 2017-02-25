using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicAttack : MonoBehaviour, Ability {


	public string name;

	public string description;

	public double damage;

	// Use this for initialization
	public void Start () 
	{
	
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

	public void Use(List<TileAttributes> ts)
	{
		TileAttributes tileTarget = ts [0];
		if (tileTarget.containedCharacter != null) 
		{
			CharacterCharacter attackedCharachter = tileTarget.containedCharacter;
			attackedCharachter.damage (damage);
		}

	}
}
