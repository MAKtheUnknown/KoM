using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileAttributes : MonoBehaviour {

	public TileAttributes north;
	public TileAttributes south;
	public TileAttributes east;
	public TileAttributes west;

	public TileType type;

	public TileArrangement map;

	public CharacterCharacter containedCharacter;

	void Awake()
	{
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

	void OnMouseOver()
	{
		map.highlighter.selectedTile = this;
		map.highlighter.transform.position = this.gameObject.transform.position;
	}

	void OnClick()
	{
		
	}

	public enum TileType
	{
		grass,
		hills,
		mountains,
		water,
		trees
	};
}
