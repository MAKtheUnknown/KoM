using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterCharacter : MonoBehaviour 
{
	
	public int maxHP;
	public int currentHP;
	public int maxXP;
	public int currentXP;
	public int maxMovesPerTurn;
	public int movesLeftThisTurn;

	public IDictionary<TileAttributes, int> tilesToMoves; 

	public TileAttributes tile;

	public Team team;

	public Ability[] specialAbilities;

	public ClassSpecifications type; 

	void Awake()
	{
		team = GetComponentInParent<Team> ();
		type = GetComponentInChildren<ClassSpecifications> ();
		specialAbilities = GetComponentsInChildren<Ability> ();
	}

	// Use this for initialization
	void Start () 
	{
		putOnBoard ();
	}
	
	// Update is called once per frame
	void Update () {
		//while (true) {
		//}
	}

	void putOnBoard()
	{
		int x = (int)this.transform.position.x;
		int y = (int)this.transform.position.y;

		tile = team.map.tileMap[x-team.map.LowX, y-team.map.LowY];
		tile.containedCharacter = this;
	}

	void findRoutes()
	{
		
	}

	void damage(int dmg)
	{
		currentHP-=dmg;
	}

	public void HighlightMoves()
	{
		List<TileAttributes> possibleMoves = type.movement.GetPossibleMoves ();

		foreach (TileAttributes t in possibleMoves) 
		{
			Instantiate (t.map.highlighter.possibleMovesHighlighters, t.transform);
		}
	}
}
