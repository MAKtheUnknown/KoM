using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileAttributes : MonoBehaviour {

	//the adjacent tiles
	public TileAttributes north;
	public TileAttributes south;
	public TileAttributes east;
	public TileAttributes west;

	/**The type of terrain*/
	public TileType type;

	/**The parent object that owns all other tiles in the scene*/
	public TileArrangement map;

	/**The charachter occupying the space*/
	public CharacterCharacter containedCharacter;

	void Awake()
	{
		//find and set the map parent.
		map = this.GetComponentInParent<TileArrangement> ();
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	//things done when you mouse over a tile.
	void OnMouseOver()
	{
		//set the highlighter's selected tile.
		map.highlighter.selectedTile = this;
		//move the highlighter over this tile.
		map.highlighter.transform.position = this.gameObject.transform.position;
	}

	void OnClick()
	{
		
	}

	/**Terrains of the tiles*/
	public enum TileType
	{
		grass,
		hills,
		mountains,
		water,
		trees
	};
}
