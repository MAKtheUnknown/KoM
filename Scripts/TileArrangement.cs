using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileArrangement : MonoBehaviour 
{
	/**The 1d array of all tiles. Used temporarily.*/
	TileAttributes[] tiles;

	/**The 2d array of all tiles. Set up on Awake().*/
	public TileAttributes[,] tileMap;

	/**The Teams that occupy the map.*/
	public TeamManager teams;

	/**The thing that selects and highlights tiles.*/
	public Highlighter highlighter;

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
	}

	// Use this for initialization
	void Start () 
	{
		//start the highlighter somewhere. TODO: make the highlighter start constantly at the bottom leftmost corner.
		//we currently use tiles[0] for this only because tileMap[0,0] would risk a null entry.
		highlighter.selectedTile = tiles[0];
		//move the highlighter to its starting position.
		highlighter.transform.position = highlighter.selectedTile.transform.position;

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	/**A method to make and return a 2d array of tiles.
	 * Also links the tiles toghether by cardinal directions.*/
	TileAttributes[,] initMap()
	{
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
}
