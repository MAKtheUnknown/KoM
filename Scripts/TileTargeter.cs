using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class TileTargeter : Ability {

	public TileArrangement map;

	public List<TileAttributes> targets;
	public List<TileAttributes> tileTargets;
	public List<TileAttributes> fullTileTargets;

	public int numberOfTargets;
	public int targetsToAquire;

	public bool targetsAquired;

	public Text instructionLabel;

	// Use this for initialization
	public override void Start () 
	{
		map = GameObject.FindGameObjectWithTag ("Map").GetComponent<TileArrangement>();
		targets = new List<TileAttributes> ();
		tileTargets = new List<TileAttributes> ();
		fullTileTargets= new List<TileAttributes>();
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
		List<TileAttributes> inRange = GetTilesInRange (x, y, range);
		
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Possible Move Highlighters")) 
		{
			GameObject.Destroy (g);
		}
		map.highlighter.mode = Highlighter.SelectionMode.SELECT_TILES;
		if (instructionLabel.text.Equals ("Select " + targetsToAquire + "tiles.") == false) 
		{
			GameObject.Destroy (GameObject.FindGameObjectWithTag("Ability Selector"));
		}
		instructionLabel.text = "Select " + targetsToAquire + " tiles.";

		if (targets.Count > 0) 
		{
			TileAttributes t = targets [0];
			if (inRange.Contains (t))
			{
				tileTargets.Add (t);
				targetsToAquire--;
			}
			else
			{
				map.highlighter.mode = Highlighter.SelectionMode.PIECE_TO_USE;
				instructionLabel.text = "";
			}
			
			targets.Remove (t);
		}

		if (targetsToAquire <= 0) 
		{		
			targetsAquired = true;
			instructionLabel.text = "";
		}
	}
	
	public void GetFullTargets(int size)
	{
		foreach(TileAttributes t in tileTargets)
		{
			for(int i = size/2+t.x;i>t.x-size/2f;i--)
			{
				for(int j = t.y-size/2 ;j<t.y+size/2f;j++)
				{
					if((0<=i)&&(i<=t.map.HighX-t.map.LowX)&&(0<=j)&&(j<=t.map.HighY-t.map.LowY))
					{
						fullTileTargets.Add(t.map.tileMap[i,j]);
					}
				}
			}
		}
	}
	
	List<TileAttributes> GetTilesInRange(int x, int y, float range)
	{
		List<TileAttributes> tileTargets = new List<TileAttributes> ();

		foreach (TileAttributes t in map.tiles) 
		{
			int rsqrd = (int)(range * range);
			int dxsqrd = (x - t.x) * (x - t.x);
			int dysqrd = (y - t.y) * (y - t.y);
			if (rsqrd >= dxsqrd + dysqrd) 
			{
				tileTargets.Add (t);
			}
		}
		return tileTargets;
	}

	public override void Use()
	{
		
	}
}
