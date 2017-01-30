using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Highlighter : MonoBehaviour {

	bool downStroke = true;
	long timeOfKeyDown = 0;
	long timeOfLastMove = 0;

	public int timeToMoveAfterDownStroke = 250;
	public int holdMoveWaitTime = 15;

	public int tilesToSelect = 1;

	public TileArrangement map;
	public TileAttributes selectedTile;

	public SelectionMode mode = SelectionMode.PIECE_TO_USE;

	public Stack<TileAttributes> chosenTiles; 
	public CharacterCharacter chosenCharachter;

	void Awake()
	{
		map = GetComponentInParent<TileArrangement> ();
	}

	// Use this for initialization
	void Start () 
	{
		chosenTiles = new Stack<TileAttributes> ();
	}
	
	// Update is called once per frame
	void Update () 
	{


		if (Input.GetKey (KeyCode.LeftArrow)) 
		{
			move (Direction.west);
		}
		if (Input.GetKeyUp (KeyCode.LeftArrow)) 
		{
			downStroke = true;
		}

		if (Input.GetKey (KeyCode.RightArrow)) 
		{
			move (Direction.east);
		}
		if (Input.GetKeyUp (KeyCode.RightArrow)) 
		{
			downStroke = true;
		}

		if (Input.GetKey (KeyCode.UpArrow)) 
		{
			move (Direction.north);
		}
		if (Input.GetKeyUp (KeyCode.UpArrow)) 
		{
			downStroke = true;
		}

		if (Input.GetKey (KeyCode.DownArrow)) 
		{
			move (Direction.south);
		}
		if (Input.GetKeyUp (KeyCode.DownArrow)) 
		{
			downStroke = true;
		}

		this.executeSelectionModeActions ();
	}

	void move(Direction d)
	{

		if (downStroke == true)
		{
			moveInDirection (d);
			timeOfKeyDown = System.DateTime.UtcNow.Ticks / 10000;
			downStroke = false;
		}
		else 
		{
			if (System.DateTime.UtcNow.Ticks / 10000 - timeOfKeyDown > timeToMoveAfterDownStroke
				&& System.DateTime.UtcNow.Ticks / 10000 - timeOfLastMove > holdMoveWaitTime) 
			{
				moveInDirection (d);
				timeOfLastMove = System.DateTime.UtcNow.Ticks / 10000;
			}
		}
	}

	void moveInDirection(Direction d)
	{
		switch (d) 
		{
		case Direction.west:
			if(selectedTile.west != null)
				selectedTile = selectedTile.west;
			break;
		case Direction.north:
			if(selectedTile.north != null)
				selectedTile = selectedTile.north;
			break;
		case Direction.south:
			if(selectedTile.south != null)
				selectedTile = selectedTile.south;
			break;
		case Direction.east:
			if(selectedTile.east != null)
				selectedTile = selectedTile.east;
			break;
		}

		this.gameObject.transform.position = selectedTile.transform.position;
	}

	void executeSelectionModeActions()
	{
		switch (mode) 
		{
		case SelectionMode.PIECE_TO_USE:
			if (selectedTile.containedCharacter != null) 
			{
				gameObject.transform.localScale = new Vector3 (1.459983f,1.459983f,1f);
				if (Input.GetKeyUp (KeyCode.Return)) 
				{
					chosenCharachter = selectedTile.containedCharacter;
					mode = SelectionMode.MOVE_TILE;
				}
				else if (Input.GetKeyUp (KeyCode.RightShift)) 
				{
					chosenCharachter = selectedTile.containedCharacter;
					mode = SelectionMode.USE_ABILITY;
				}
			} else 
			{
				
				gameObject.transform.localScale = new Vector3 (1f,1f,1f);
			}
			break;
		case SelectionMode.MOVE_TILE:
			
			break;
		case SelectionMode.ABILITY_TARGETS:
			break;
		}

	}

	public Stack<TileAttributes> requestSelections(int number)
	{
		return null;
	}

	enum Direction
	{
		north,
		south,
		east,
		west
	}

	public enum SelectionMode
	{
		PIECE_TO_USE,
		MOVE_TILE,
		USE_ABILITY,
		ABILITY_TARGETS
	}
}
