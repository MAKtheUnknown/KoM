using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CharacterCharacter : MonoBehaviour 
{
	public int currentHP;

	public int currentXP;
	public int maxMovesPerTurn;
	public int movesLeftThisTurn;
	public List<CharacterCharacter> bardLinked;

	public IDictionary<TileAttributes, int> tilesToMoves; 

	public TileAttributes tile;

	public Team team;

	public Ability[] specialAbilities;
	
	public ActiveEffect passive;

	public ClassSpecifications type; 

	public bool usedAbility;

	public List<ActiveEffect> activeEffects;
	
	//used to detect terrain changes
	TileAttributes lastTile;

	public int x;
	public int y;

	void Awake()
	{
		team = GetComponentInParent<Team> ();
		type = GetComponentInChildren<ClassSpecifications> ();
		specialAbilities = GetComponentsInChildren<Ability> ();
		bardLinked = new List<CharacterCharacter>();
		usedAbility = false;
		activeEffects = new List<ActiveEffect> ();
	}

	// Use this for initialization
	void Start () 
	{
		putOnBoard ();
		currentHP = type.maximumHealth;
	}
	
	// Update is called once per frame
	void Update () {
		//while (true) {
		//}
		if(lastTile!=tile&&type.type==ClassSpecifications.CharacterType.Alchemist)
		{
			passive.Act();
			lastTile=tile;
		}
		if(currentHP==0&&type.type==ClassSpecifications.CharacterType.Priest)
		{
			passive.Finish();
			currentHP=-1;
		}
		if(currentHP==0&&type.type==ClassSpecifications.CharacterType.Swordsman)
		{
			passive.Finish();
			currentHP=-1;
		}
			
	}

	public void putOnBoard()
	{
		x = (int)this.transform.position.x-team.map.LowX;
		y = (int)this.transform.position.y-team.map.LowY;
		
		tile = team.map.tileMap[x, y];
		lastTile=team.map.tileMap[x, y];
		tile.containedCharacter = this;
		
	}

	void findRoutes()
	{
		
	}

	void damage(int dmg)
	{
		currentHP-=dmg*(100-2*type.defense)/100;
		if(bardLinked!=null)
		{
			foreach(CharacterCharacter c in bardLinked)
			{
				if(c!=null)
				{
					c.currentHP-=dmg*(100-2*c.type.defense)/200;
				}
			}
		}	
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
		this.currentHP -= Math.Max(0,(int)(d*(100.0-2*this.type.defense)/100));
		

		if(currentHP <= 0)
		{
			this.kill();
		}
		
		if(bardLinked!=null)
		{	
			foreach(CharacterCharacter c in bardLinked)
			{
				if(c!=null)
				{
					c.currentHP-=(int)(d*(100.0-2*c.type.defense)/200);
					if(c.currentHP <= 0)
					{
						c.kill();
					}
				}
			}
		}
	
	}

	public void heal(int h)
	{
		this.currentHP += h;		

		if (currentHP > type.maxXP) 
		{
			currentHP = type.maxXP;
		}
		
		if(bardLinked!=null)
		{
			foreach(CharacterCharacter c in bardLinked)
			{
				if(c!=null)
				{
					c.currentHP+=h/2;
					if (c.currentHP > c.type.maxXP) 
					{
						c.currentHP = c.type.maxXP;
					}
				}
			}
		}
	}

	public void kill()
	{
		//Remove this if statement if we Finish/Remove active effects after a character dies
		team.TeamDamage(this.type.morale); 	
		
		//team.TeamDamage(this.type.morale); //inflicts damage to team's morale		
		//foreach(ActiveEffect a in activeEffects)
		//{
		//	a.Finish();
		//}
		GameObject.Destroy (this.gameObject);
		/*if(type.type==ClassSpecifications.CharacterType.Priest)
		{
			passive.Finish();
		}*/
	}
}