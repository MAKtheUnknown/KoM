using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Kaiyufication : Ability {

	public string name = "Kaiyufication";
	public string description;
	public TileArrangement map;
	// Use this for initialization
	public void Start () 
	{
		map = GameObject.FindGameObjectWithTag ("Map").GetComponent<TileArrangement>();
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
		foreach (Team t in map.teams.teams) 
		{
			foreach (CharacterCharacter c in t.pieces) 
			{
				c.activeEffects.Add (new Kaiyufied (c));
			}
		}
	}
}
