using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Kaiyufication : MonoBehaviour, Ability {

	public string name = "Kaiyufication";
	public string description;
	public TileArrangement map;
	public ClassSpecifications specs;
	// Use this for initialization
	public void Start () 
	{
		map = GameObject.FindGameObjectWithTag ("Map").GetComponent<TileArrangement>();
		specs = GetComponentInParent<ClassSpecifications> ();
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
			if (t != specs.owner.team) 
			{
				foreach (CharacterCharacter c in t.pieces) 
				{
					c.activeEffects.Add (new Kaiyufied (c));
				}
			}
		}
		map.highlighter.mode = Highlighter.SelectionMode.PIECE_TO_USE;
		specs.owner.usedAbility = true;
	}
}
