using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterCharacter : MonoBehaviour 
{
	public int currentHP;

	public int currentXP;
	public int maxMovesPerTurn;
	public int movesLeftThisTurn;
	public List<CharacterCharacter> bardLinked;
	public bool guarded;
	public bool shielded;

	public IDictionary<TileAttributes, int> tilesToMoves; 

	public TileAttributes tile;

	public Team team;

	public Ability[] specialAbilities;

	public ClassSpecifications type; 

	public bool usedAbility;

	public List<ActiveEffect> activeEffects;

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
		if(!guarded)
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
		if(shielded)
		{
			d*=.7;
		}
		if(!guarded)
		{
			this.currentHP -= (int)(d*(100.0-2*this.type.defense)/100);
			

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
		team.TeamDamage(this.type.morale); //inflicts damage to team's morale		
		//foreach(ActiveEffect a in activeEffects)
		//	a.Finish();
		GameObject.Destroy (this.gameObject);
	}
}