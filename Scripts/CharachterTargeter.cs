using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class CharachterTargeter : MonoBehaviour, Ability {

	public TileArrangement map;

	public string name;

	public string description;

	public List<TileAttributes> targets;
	public List<CharacterCharacter> charachterTargets;

	public int numberOfTargets;
	public int targetsToAquire;

	public bool targetsAquired;

	public Text instructionLabel;

	// Use this for initialization
	public virtual void Start () 
	{
		map = GameObject.FindGameObjectWithTag ("Map").GetComponent<TileArrangement>();
		targets = new List<TileAttributes> ();
		charachterTargets = new List<CharacterCharacter> ();
		targetsAquired = false;
		targetsToAquire = numberOfTargets;
		GameObject.Destroy (GameObject.FindGameObjectWithTag("Ability Selector"));
		instructionLabel = GameObject.FindGameObjectWithTag("Ability Instructions").GetComponentInChildren<Text>();
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



	public void GetTargets()
	{
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Possible Move Highlighters")) 
		{
			GameObject.Destroy (g);
		}
		map.highlighter.mode = Highlighter.SelectionMode.SELECT_TARGETS;
		if (instructionLabel.text.Equals ("Select " + targetsToAquire + "pieces.") == false) 
		{
			GameObject.Destroy (GameObject.FindGameObjectWithTag("Ability Selector"));
		}
		instructionLabel.text = "Select " + targetsToAquire + " pieces.";

		if (targets.Count > 0) 
		{
			TileAttributes t = targets [0];
			if (t.containedCharacter != null) 
			{
				charachterTargets.Add (t.containedCharacter);
				targetsToAquire--;
			}
			targets.Remove (t);
		}

		if (targetsToAquire <= 0) 
		{
			targetsAquired = true;
			instructionLabel.text = "";
		}
	}

	/* GetTargetsInRange(int x, int y, float range)
	{
		TileAttributes t = map.tileMap [x, y];
		for (int i = x - range; i < x + range; x++) 
		{
			for (int j = y - range; j < j + range; j++) 
			{
				if (i >= map.LowX && j >= map.LowY && i < map.HighX && j < map.HighY) 
				{
					//if (Mathf.Sqrt (i ^ 2 + j ^ 2) < ) 
					{

					}
				}
			}
		}
	}*/

	public virtual void Use()
	{
		
	}
}
