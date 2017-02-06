using UnityEngine;
using System.Collections;

public class Team : MonoBehaviour {

	public PlayerType type;

	public string name;

	public CharacterCharacter[] pieces;

	public TileArrangement map;

	public TeamManager manager;

	void Awake()
	{
		map = this.GetComponentInParent<TileArrangement> ();
		manager = this.GetComponentInParent<TeamManager> ();
		pieces = this.GetComponentsInChildren<CharacterCharacter> ();
	}

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public bool Contains(CharacterCharacter p)
	{
		foreach(CharacterCharacter c in pieces)
		{
			if(c == p)
			{
				return true;
			}
		}
		return false;
	}

	public enum PlayerType
	{
		human,
		computer
	}
}
