using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : CharachterTargeter {

	public string name;
	public string description;

	public int addedHealth;


	// Use this for initialization
	public override void Start () 
	{
		map = GameObject.FindGameObjectWithTag ("Map").GetComponent<TileArrangement>();
		targets = new List<TileAttributes> ();
		charachterTargets = new List<CharacterCharacter> ();
		targetsAquired = false;
		targetsToAquire = numberOfTargets;
		GameObject.Destroy (GameObject.FindGameObjectWithTag("Ability Selector"));
		base.Start ();
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

	public override void Use()
	{
		if (targetsAquired == false) 
		{
			base.GetTargets();
		}
		if (targetsAquired == true) 
		{
			foreach (CharacterCharacter t in charachterTargets) 
			{
				t.heal (addedHealth);
			}
			map.highlighter.mode = Highlighter.SelectionMode.PIECE_TO_USE;
			Start ();
		}

	}
}
