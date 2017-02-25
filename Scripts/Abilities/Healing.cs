using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour, Ability {

	public string name;
	public string description;

	public int addedHealth;

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

	public void Use(List<TileAttributes> ts)
	{
		CharacterCharacter c = ts [0].containedCharacter;
		c.heal (addedHealth);
	}
}
