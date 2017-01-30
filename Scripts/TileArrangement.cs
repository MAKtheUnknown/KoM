using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileArrangement : MonoBehaviour 
{
	
	TileAttributes[] tiles;

	public TileAttributes[,] tileMap;

	public TeamManager teams;

	public Highlighter highlighter;

	int lowX = 0;
	int highX = 0;
	int lowY = 0;
	int highY = 0;

	void Awake()
	{
		tiles = this.gameObject.GetComponentsInChildren<TileAttributes>();
		tileMap = initMap ();
		highlighter = this.GetComponentInChildren<Highlighter> ();
		teams = this.GetComponentInChildren<TeamManager> ();
	}

	// Use this for initialization
	void Start () 
	{
		highlighter.selectedTile = tiles[0];
		highlighter.transform.position = highlighter.selectedTile.transform.position;


	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	TileAttributes[,] initMap()
	{

		foreach (TileAttributes t in tiles) 
		{
			if ((int)t.gameObject.transform.position.x < lowX) 
			{
				lowX = (int)t.gameObject.transform.position.x;
			}
			if ((int)t.gameObject.transform.position.y < lowY) 
			{
				lowY = (int)t.gameObject.transform.position.y;
			}
			if ((int)t.gameObject.transform.position.x > highX) 
			{
				highX = (int)t.gameObject.transform.position.x;
			}
			if ((int)t.gameObject.transform.position.y > highY) 
			{
				highY = (int)t.gameObject.transform.position.y;
			}
		}

		TileAttributes[,] tileMap = new TileAttributes[highX-lowX+1,highY-lowY+1];

		foreach(TileAttributes t in tiles)
		{
			int x = (int)t.gameObject.transform.position.x - lowX;
			int y = (int)t.gameObject.transform.position.y - lowY;
			tileMap [x, y] = t;
		}

		foreach(TileAttributes t in tiles)
		{
			int x = (int)t.gameObject.transform.position.x - lowX;
			int y = (int)t.gameObject.transform.position.y - lowY;

			if (y < highY - lowY)
			{
				t.north = tileMap [x, y + 1];
			}
			if (y > 0) 
			{
				t.south = tileMap [x, y - 1];
			}
			if (x < highX - lowX)
			{
				t.east = tileMap [x + 1, y];
			}
			if (x > 0) 
			{
				t.west = tileMap [x - 1, y];
			}
		}

		return tileMap;
	}

		
	public int LowX 
	{
		get {
			return lowX;
		}
	}

	public int LowY {
		get {
			return lowY;
		}
	}
}
