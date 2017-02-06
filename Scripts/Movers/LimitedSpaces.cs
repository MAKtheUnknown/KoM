using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * This method of movement allows a limited time to move.
 * Movements can be made to the north, south, east and west.
 * 
 */
public class LimitedSpaces : MonoBehaviour, Mover 
{

	public ClassSpecifications moved;

	public double timeToMove = 10; //seconds in turn.
	public double timeSpent = 0;

	TileAttributes t;
	TileArrangement map;
	TileAttributes[,] m;

	public List<TileAttributes> possibilities;

	/**The set of tiles and the difficulties with which this class can cover them*/
	public IDictionary<TileAttributes.TileType, double> tileTypeTimes;

	public double grassSpeed = 1;
	public double hillSpeed = 1;
	public double mountainSpeed = 1;
	public double forestSpeed = 1;
	public double shallowWaterSpeed = 1;

	public double[,] times;

	void Awake()
	{
		moved = GetComponentInParent<ClassSpecifications> ();

		tileTypeTimes = new Dictionary<TileAttributes.TileType, double> ();
		tileTypeTimes.Add (TileAttributes.TileType.grass, grassSpeed);
		tileTypeTimes.Add (TileAttributes.TileType.hills, hillSpeed);
		tileTypeTimes.Add (TileAttributes.TileType.mountains, mountainSpeed);
		tileTypeTimes.Add (TileAttributes.TileType.trees, forestSpeed);
		tileTypeTimes.Add (TileAttributes.TileType.water, shallowWaterSpeed);
	}

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public List<TileAttributes> GetPossibleMoves()
	{
		t = moved.owner.tile;
		map = t.map; 
		m = map.tileMap;

		possibilities = new List<TileAttributes> ();

		times = new double[m.GetLength (0), m.GetLength (1)];

		propogateTimes (t, timeSpent);

		times [moved.owner.x, moved.owner.y] = 0;

		for (int n = 0; n < times.GetLength (0); n++) 
		{
			for(int nn = 0; nn < times.GetLength(1); nn++)
			{
				if (times [n, nn] != 0) 
				{
					possibilities.Add (m [n, nn]);
				}
			}
		}

		return possibilities;
	}

	public void propogateTimes(TileAttributes t, double time)
	{
		double newTime = time + tileTypeTimes[t.type];
		if (newTime <= timeToMove
			&& (newTime < times[t.x,t.y] || times[t.x,t.y] == 0))
		{
			times[t.x,t.y] = newTime;

			if (t.north != null) 
			{
				propogateTimes (t.north, newTime);
			} 
			if (t.south != null) 
			{
				propogateTimes (t.south, newTime);
			}
			if (t.east != null) 
			{
				propogateTimes (t.east, newTime);
			}
			if (t.west != null) 
			{
				propogateTimes (t.west, newTime);
			}
		}
	}

	public void Move(TileAttributes t)
	{
		t.containedCharacter = moved.owner;
		moved.owner.tile.containedCharacter = null;
		moved.owner.tile = t;
		moved.owner.transform.position = t.transform.position;

		timeSpent += times[t.x,t.y];
	}

	public void Reset()
	{
		timeSpent = 0;
	}
}
