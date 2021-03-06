﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileArrangement : MonoBehaviour 
{
	/**The 1d array of all tiles. Used temporarily.*/
	public TileAttributes[] tiles;

	/**The 2d array of all tiles. Set up on Awake().*/
	public TileAttributes[,] tileMap;

	/**The Teams that occupy the map.*/
	public TeamManager teams;

	/**The thing that selects and highlights tiles.*/
	public Highlighter highlighter;

	public List<Animation> runningAnimations;

	public GameObject ui;
	
	/** Used for ability label: using the map to store its initial position**/
	//public Vector2 uiCameraPosition;
	//public float uiCameraSize;

	//The lowest and highest positions of the physical tiles.
	int lowX = 0;
	int highX = 0;
	int lowY = 0;
	int highY = 0;

	void Awake()
	{
		//get all of the child tiles.
		tiles = this.gameObject.GetComponentsInChildren<TileAttributes>();
		//make the map and connect all the tiles.
		tileMap = initMap ();
		//get the child highlighter.
		highlighter = this.GetComponentInChildren<Highlighter> ();
		//get the teams.
		teams = this.GetComponentInChildren<TeamManager> ();

		runningAnimations = new List<Animation> ();
	}

	// Use this for initialization
	void Start () 
	{
		/**for abilityselector retrieval
		//uiCameraPosition=GameObject.Find("UI Camera").transform.position;
		//uiCameraSize=GameObject.Find("UI Camera").GetComponent<Camera>().orthographicSize;
		**/
		//start the highlighter somewhere. TODO: make the highlighter start constantly at the bottom leftmost corner.
		//we currently use tiles[0] for this only because tileMap[0,0] would risk a null entry.
		highlighter.selectedTile = tiles[0];
		//move the highlighter to its starting position.
		highlighter.transform.position = highlighter.selectedTile.transform.position;
		//get the user interface from the scene.
		ui = GameObject.FindGameObjectWithTag("UI");
	}
	
	// Update is called once per frame
	void Update () 
	{
		RunAnimations ();
	}

	/**A method to make and return a 2d array of tiles.
	 * Also links the tiles toghether by cardinal directions.*/
	TileAttributes[,] initMap()
	{
		//loop through tiles and precisely align them.
		foreach (TileAttributes t in tiles) 
		{
			t.transform.position = new Vector2(Mathf.Round(t.transform.position.x), Mathf.Round(t.transform.position.y));
		}

		//loop through tiles to find the bottomleftmost and toprightmost tiles.
		foreach (TileAttributes t in tiles) 
		{
			// if a tile's x is lower than the lowest,
			if ((int)t.gameObject.transform.position.x < lowX) 
			{
				//make it the new low.
				lowX = (int)t.gameObject.transform.position.x;
			}
			//if a tile's y is lower than the lowest,
			if ((int)t.gameObject.transform.position.y < lowY) 
			{
				//make it the new low.
				lowY = (int)t.gameObject.transform.position.y;
			}
			//ifa tile's x is higher than the highest,
			if ((int)t.gameObject.transform.position.x > highX) 
			{
				//make it the new high.
				highX = (int)t.gameObject.transform.position.x;
			}
			//if a tile's y is higher than the highest,
			if ((int)t.gameObject.transform.position.y > highY) 
			{
				//make it the new high.
				highY = (int)t.gameObject.transform.position.y;
			}
		}
		//initialize the 2d array with the ranges between the lowest and highest x's and y's
		TileAttributes[,] tileMap = new TileAttributes[highX-lowX+1,highY-lowY+1];

		//add all tiles to this new 2d array.
		foreach(TileAttributes t in tiles)
		{
			int x = (int)t.gameObject.transform.position.x - lowX;
			int y = (int)t.gameObject.transform.position.y - lowY;
			tileMap [x, y] = t;
			t.x = x;
			t.y = y;
		}

		//go through to connect all the tiles via a linked net.
		foreach(TileAttributes t in tiles)
		{
			//get locations of each tile in the new array.
			int x = (int)t.gameObject.transform.position.x - lowX;
			int y = (int)t.gameObject.transform.position.y - lowY;

			//if not on the upper edge,
			if (y < highY - lowY)
			{
				//add a northern tile.
				t.north = tileMap [x, y + 1];
			}
			//if not on the bottom edge,
			if (y > 0) 
			{
				//add a southern tile.
				t.south = tileMap [x, y - 1];
			}
			//if not on the rightmost edge,
			if (x < highX - lowX)
			{
				//add an eastern tile.
				t.east = tileMap [x + 1, y];
			}
			//if not on the leftmost edge
			if (x > 0) 
			{
				//add a western tile.
				t.west = tileMap [x - 1, y];
			}
		}

		//now, put the charachters on the map.
		
		/*
		TeamManager ts = this.GetComponentInChildren<TeamManager>();
		foreach (Team t in ts.teams) 
		{
			foreach (CharacterCharacter c in t.pieces) 
			{
				c.putOnBoard ();
			}
		}
		*/
		
		//return the 2d map.
		return tileMap;
	}

	/**A getter for the x position of the leftmost tile.*/	
	public int LowX 
	{
		get {
			return lowX;
		}
	}
	/**A getter for the y position of the lowermost tile.*/
	public int LowY {
		get {
			return lowY;
		}
	}

	/**A getter for the x position of the leftmost tile.*/	
	public int HighX 
	{
		get {
			return highX;
		}
	}
	/**A getter for the y position of the lowermost tile.*/
	public int HighY {
		get {
			return highY;
		}
	}

	public void RunAnimations ()
	{
		List<Animation> toRemove = new List<Animation>();
		List<Animation> toAdd = new List<Animation>();

		foreach (Animation a in runningAnimations) 
		{
			if (a.IsFinished ()) 
			{
				a.OnFinish ();
				foreach (Animation n in a.endTriggers) 
				{
					toAdd.Add (n);
				}
				toRemove.Add (a);
			}
			else 
			{
				if (!a.initialized) 
				{
					a.Init ();
				}
				a.Action ();
			}
		}
		foreach (Animation r in toRemove) 
		{
			runningAnimations.Remove (r);
		}
		foreach (Animation d in toAdd) 
		{
			runningAnimations.Add (d);
		}
	}

	public void AddAnimation(Animation a)
	{
		runningAnimations.Add (a);
	}

	public void AddAnimationSequence (List<Animation> l)
	{
		if (l.Count>0) 
		{
			AddAnimation (l [0]);
		}
		for (int i = 1; i < l.Count; i++) 
		{
			l[i-1].AddEndTrigger(l[i]);
		}
	}
}
