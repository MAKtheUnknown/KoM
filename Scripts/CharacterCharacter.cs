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

	public IDictionary<TileAttributes, int> tilesToMoves; 

	public TileAttributes tile;

	public Team team;

	public Ability[] specialAbilities;

	public ClassSpecifications type; 

	public bool usedAbility;

	public List<ActiveEffect> activeEffects;
	
	//used to detect terrain changes
	public TileAttributes lastTile;

	public int x;
	public int y;

	void Awake()
	{
		team = GetComponentInParent<Team> ();
		type = GetComponentInChildren<ClassSpecifications> ();
		specialAbilities = GetComponentsInChildren<Ability> ();
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
	void Update () 
	{
		EffectCheck();
	}

	public void putOnBoard()
	{
		x = (int)Math.Round(this.transform.position.x-team.map.LowX);
		y = (int)Math.Round(this.transform.position.y-team.map.LowY);
		
		tile = team.map.tileMap[x, y];
		lastTile=team.map.tileMap[x, y];
		tile.containedCharacter = this;
		
	}
	
	int Dround(double d)
	{
		return (int)Math.Round(d);
	}

	void findRoutes()
	{
		
	}

	void damage(int dmg)
	{
		currentHP-=dmg*(100-2*type.defense)/100;
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
	
	}

	public void heal(int h)
	{
		this.currentHP += h;		

		if (currentHP > type.maximumHealth) 
		{
			currentHP = type.maximumHealth;
		}
	}
	
	public void heal(double d)
	{
		heal((int)d);
	}

	public void kill()
	{
		//Remove this if statement if we Finish/Remove active effects after a character dies
		
		team.TeamDamage(this.type.morale); 
		team.activePieces--;
		x=-500;
		y=-500;
		tile=null;
		
		//team.TeamDamage(this.type.morale); //inflicts damage to team's morale		
		//foreach(ActiveEffect a in activeEffects)
		//{
		//	a.Finish();
		//}
		EffectCheck();
		GameObject.Destroy (this.gameObject);
		/*if(type.type==ClassSpecifications.CharacterType.Priest)
		{
			passive.Finish();
		}*/
	}
	
	void EffectCheck()
	{
		List<ContinuousEffect> contEffects = new List<ContinuousEffect>();
		foreach(ActiveEffect e in activeEffects)
			if(e.GetType().Equals(typeof(ContinuousEffect)))
				contEffects.Add((ContinuousEffect)e);
		foreach(ContinuousEffect e in contEffects)
			e.Check();
	}
}