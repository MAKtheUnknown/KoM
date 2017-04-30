using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class CharachterTargeter : Ability {

	public TileArrangement map;

	public List<TileAttributes> targets;
	public List<CharacterCharacter> charachterTargets;

	public int numberOfTargets;
	public int targetsToAquire;

	public bool targetsAquired;

	public Text instructionLabel;

	// Use this for initialization
	public override void Start () 
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



	public void GetTargets(int x, int y, float range)
	{
		List<CharacterCharacter> inRange = GetTargetsInRange (x, y, range);
		
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
				if (inRange.Contains (t.containedCharacter)) 
				{
					charachterTargets.Add (t.containedCharacter);
					targetsToAquire--;
				}
				else
				{
					map.highlighter.mode = Highlighter.SelectionMode.PIECE_TO_USE;
					instructionLabel.text = "";
				}
			}
			else 
			{
				instructionLabel.text = "";
				map.highlighter.mode = Highlighter.SelectionMode.PIECE_TO_USE;
			}
			targets.Remove (t);
		}

		if (targetsToAquire <= 0) 
		{
			targetsAquired = true;
			instructionLabel.text = "";
		}
	}
	
	public void GetAllyTargets(int x, int y, float range, Team team)
	{
		List<CharacterCharacter> inRange = GetAllTargetsInRange (x, y, range);
		
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
				if (inRange.Contains (t.containedCharacter)&&t.containedCharacter.team==team) 
				{
					charachterTargets.Add (t.containedCharacter);
					targetsToAquire--;
				}
				else
				{
					map.highlighter.mode = Highlighter.SelectionMode.PIECE_TO_USE;
					instructionLabel.text = "";
				}
			}
			else 
			{
				instructionLabel.text = "";
				map.highlighter.mode = Highlighter.SelectionMode.PIECE_TO_USE;
			}
			targets.Remove (t);
		}

		if (targetsToAquire <= 0) 
		{
			targetsAquired = true;
			instructionLabel.text = "";
		}
	}
	//doesn't include self
	public void GetOtherAllyTargets(int x, int y, float range, Team team)
	{
		List<CharacterCharacter> inRange = GetTargetsInRange (x, y, range);
		
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
				if (inRange.Contains (t.containedCharacter)&&t.containedCharacter.team==team) 
				{
					charachterTargets.Add (t.containedCharacter);
					targetsToAquire--;
				}
				else
				{
					map.highlighter.mode = Highlighter.SelectionMode.PIECE_TO_USE;
					instructionLabel.text = "";
				}
			}
			else 
			{
				instructionLabel.text = "";
				map.highlighter.mode = Highlighter.SelectionMode.PIECE_TO_USE;
			}
			targets.Remove (t);
		}

		if (targetsToAquire <= 0) 
		{
			targetsAquired = true;
			instructionLabel.text = "";
		}
	}
		
	
	public void GetEnemyTargets(int x, int y, float range, Team team)
	{
		List<CharacterCharacter> inRange = GetTargetsInRange (x, y, range);
		
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
				if (inRange.Contains (t.containedCharacter)&&t.containedCharacter.team!=team) 
				{
					charachterTargets.Add (t.containedCharacter);
					targetsToAquire--;
				}
				else
				{
					map.highlighter.mode = Highlighter.SelectionMode.PIECE_TO_USE;
					instructionLabel.text = "";
				}
			}
			else 
			{
				instructionLabel.text = "";
				map.highlighter.mode = Highlighter.SelectionMode.PIECE_TO_USE;
			}
			targets.Remove (t);
		}

		if (targetsToAquire <= 0) 
		{
			targetsAquired = true;
			instructionLabel.text = "";
		}
	}
	
	//Following are use to find possible targets
	public List<CharacterCharacter> GetTargetsInRange(int x, int y, float range)
	{
		List<CharacterCharacter> charTargets = new List<CharacterCharacter> ();
		
		foreach (Team t in map.teams.teams) 
		{
			foreach (CharacterCharacter c in t.pieces) 
			{
				int rsqrd = (int)(range * range);
				int dxsqrd = (x - c.x) * (x - c.x);
				int dysqrd = (y - c.y) * (y - c.y);
				bool b = !(x == c.x && y == c.y);
				if ((rsqrd >= dxsqrd + dysqrd) && b) 
				{
					charTargets.Add (c);
				}
			}
		}
		
		RemoveStealthed(charTargets);		
		return charTargets;
	}
	
	//Included self
	public List<CharacterCharacter> GetAllTargetsInRange(int x, int y, float range)
	{
		List<CharacterCharacter> charTargets = new List<CharacterCharacter> ();

		foreach (Team t in map.teams.teams) 
		{
			foreach (CharacterCharacter c in t.pieces) 
			{
				int rsqrd = (int)(range * range);
				int dxsqrd = (x - c.x) * (x - c.x);
				int dysqrd = (y - c.y) * (y - c.y);
				if (rsqrd >= dxsqrd + dysqrd) 
				{
					charTargets.Add (c);
				}
			}
		}
		RemoveStealthed(charTargets);
		return charTargets;
	}

	public override void Use()
	{
		
	}
	
	public void RemoveStealthed(List<CharacterCharacter> l)
	{
		List<CharacterCharacter> Stealthed=new List<CharacterCharacter>();
		foreach(CharacterCharacter c in l)
		{
			foreach(ActiveEffect e in c.activeEffects)
			{
				if(e.GetType().Equals(typeof(Stealthed)))
				{
					Stealthed.Add(c);
				}
			}
		}
		
		foreach(CharacterCharacter c in Stealthed)
		{
			l.Remove(c);
		}
	}
}
