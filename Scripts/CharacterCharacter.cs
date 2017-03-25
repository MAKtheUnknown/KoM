using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterCharacter : MonoBehaviour 
{
	public int currentHP;

	public int currentXP;
	public int maxMovesPerTurn;
	public int movesLeftThisTurn;
	public int morale;

	public IDictionary<TileAttributes, int> tilesToMoves; 

	public TileAttributes tile;

	public Team team;

	public Ability[] specialAbilities;

	public ClassSpecifications type; 

	public bool usedAbility;

	public int x;
	public int y;

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
		currentHP = type.maximumHealth;
		usedAbility = false;
	}
	
	// Update is called once per frame
	void Update () {
		//while (true) {
		//}
	}

	public void putOnBoard()
	{
		x = (int)this.transform.position.x-team.map.LowX;
		y = (int)this.transform.position.y-team.map.LowY;

		tile = team.map.tileMap[x, y];
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
			GameObject h = Instantiate (t.map.highlighter.possibleMovesHighlighter);
            h.transform.SetParent(t.transform);
			RectTransform rt = h.GetComponent<RectTransform> ();
			rt.anchoredPosition = new Vector2 (0,0);
			this.team.manager.map.highlighter.possibleMoveHighlighters.Add (h);
		}
	}

	public void damage(double d)
	{
		this.currentHP -= (int)d;

		if(currentHP <= 0)
		{
			this.kill();
		}
	}

	public void heal(int h)
	{
		this.currentHP += h;

		if (currentHP > type.maxXP) 
		{
			currentHP = type.maxXP;
		}
	}

	public void kill()
	{
		team.TeamDamage(morale); //inflicts damage to team's morale
		GameObject.Destroy (this.gameObject);
	}
}
