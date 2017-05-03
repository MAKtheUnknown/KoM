using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class TeamManager : MonoBehaviour {

	public TileArrangement map;
	public Team[] teams;
	ThreatDetermination threatDet;
	
	int turn;

	void Awake()
	{
		map = this.GetComponentInParent<TileArrangement> ();
		teams = this.GetComponentsInChildren<Team> ();
		threatDet = this.GetComponentInChildren<ThreatDetermination>();
	}

	// Use this for initialization
	void Start () 
	{
		turn = 0;
		map.highlighter.currentTeam = teams [turn];
		
		for(int i = 0;i<teams.Length; i++)//sets moraleBar and moraleText for each team
		{
			teams[i].moraleBar=GameObject.FindGameObjectWithTag("Morale Bar "+(i+1));
			teams[i].moraleText=GameObject.FindGameObjectWithTag("Morale Text "+(i+1));
			teams[i].moraleText.GetComponent<Text>().text=teams[i].name+": "+teams[i].teamMorale+"/"+teams[i].maxMorale;
		}
		teams[0].moraleText.GetComponent<Text>().fontStyle=FontStyle.Bold;
		
		//Sets passives as active effects for specific classes
		foreach(Team t in teams)
		{
			foreach(CharacterCharacter c in t.pieces)
			{
				c.putOnBoard ();
				c.originalTile=c.tile;
				addPassive(c);
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Team t = teams [turn % teams.Length];

	}

	public void rotate()
	{
		
		List<ActiveEffect> effectsToRemove= new List<ActiveEffect>();
		//Increments tile effects
		turn = (turn+1)%teams.Length;
		map.highlighter.currentTeam = teams [turn];

		//prepare all the team's pieces for the new turn.
		foreach (CharacterCharacter c in teams[turn].pieces) 
		{
			if(c!=null)
			{
				c.type.movement.Reset ();
				c.usedAbility = false;

				foreach (ActiveEffect e in c.activeEffects) 
				{
					e.turnsLeft--;
					e.Act ();
					if (e.turnsLeft <= 0) 
					{
						e.Finish ();
						effectsToRemove.Add(e);
					}
				}
				foreach(ActiveEffect e in effectsToRemove)
					c.activeEffects.Remove (e);
				effectsToRemove= new List<ActiveEffect>();

				foreach (Ability a in c.type.classAbilities) 
				{
					if(a.cooldownTimer > 0) 
					{
						a.cooldownTimer--;
					}
				}
			}
		}
		
		foreach(TileEffect t in teams[turn].tileEffects)
			UpdateTiles(t);
		
		//bolds the active team's text
		foreach(Team x in teams)
		{
			x.moraleText.GetComponent<Text>().fontStyle= FontStyle.Normal;
			if (x == teams [turn % teams.Length]) 
			{
				x.moraleText.GetComponent<Text> ().fontStyle = FontStyle.Bold;
				foreach (CharacterCharacter c in x.pieces) 
				{
					if (c.gameObject != null) 
					{
						SpriteRenderer sr = c.gameObject.GetComponent<SpriteRenderer> ();
						sr.color = new Color (1f, 1f, 1f, 1f);
					}
				}
			}
			else 
			{
				foreach (CharacterCharacter c in x.pieces) 
				{
					if (c.gameObject != null) 
					{
						SpriteRenderer sr = c.gameObject.GetComponent<SpriteRenderer> ();
						sr.color = new Color (1f, 1f, 1f, 1f);
					}
				}
			}
		}
		
		if(teams[turn].type==Team.PlayerType.computer)
		{
			foreach(CharacterCharacter c in teams[turn].pieces)
			{
				threatDet.Threat(c);
			}
			rotate();
		}
		
		
		
		
	}
	
	void UpdateTiles(TileEffect e)
	{
		List<TileEffect> tileEffectsToRemove = new List<TileEffect>();
		e.turnsLeft--;
		e.Act ();
		if (e.turnsLeft <= 0) 
		{
			e.Finish ();
			tileEffectsToRemove.Add(e);
		}
		
		foreach(TileEffect t in tileEffectsToRemove)
			teams[turn].tileEffects.Remove(t);
	}
	
	//adds passives for characters as effects
	void addPassive(CharacterCharacter c)
	{
		if(c.type.type==ClassSpecifications.CharacterType.Swordsman)
		{
			c.type.passive = new Stalwart(c);
			c.activeEffects.Add(c.type.passive);
		}
		
		if(c.type.type==ClassSpecifications.CharacterType.Alchemist)
		{
			c.type.passive = new ParadigmShift(c);
			c.activeEffects.Add(c.type.passive);
			
		}
		
		if(c.type.type==ClassSpecifications.CharacterType.Bard)
		{
			c.type.passive = new RisingTempo(c);
			c.activeEffects.Add(c.type.passive);
		}
		
		if(c.type.type==ClassSpecifications.CharacterType.Priest)
		{
			c.type.passive = new Martyr(c);
			c.activeEffects.Add(c.type.passive);
		}
		
		if(c.type.type==ClassSpecifications.CharacterType.Hero)
		{
			c.type.passive = new IncomprehensibleRage(c);
			c.activeEffects.Add(c.type.passive);
		}
		
		if(c.type.type==ClassSpecifications.CharacterType.Noble)
		{
			c.type.passive = new TacticalManeuver(c);
			c.activeEffects.Add(c.type.passive);
		}
		
		if(c.type.type==ClassSpecifications.CharacterType.Thief)
		{
			c.type.passive = new AcrobaticLeap(c);
			c.activeEffects.Add(c.type.passive);
		}
		
		if(c.type.type==ClassSpecifications.CharacterType.Magician)
		{
			c.type.passive = new FantasiasReturn(c);
			c.activeEffects.Add(c.type.passive);
		}
		
	}
	
	public void RemoveTeam(Team selectedTeam) //Sets selectedTeam's type to "dead"
	{
		foreach (Team t in teams)
		{
			if(t == selectedTeam)
			{
				t.type=Team.PlayerType.dead;	
				
				foreach(CharacterCharacter p in t.pieces)
				{
					if(p!=null)
						GameObject.Destroy (p.gameObject);
				}
			}
			
		}
		this.CheckVictory();
	}
	
	public void CheckVictory()
	{
		int c = 0;	//Counter of remaining (non-dead) teams
		foreach (Team t in teams)
		{
			if(t.type==Team.PlayerType.dead)
			{
				c++;
			}
		}
		
		if(c<=1)
		{
			foreach(Team t in teams)
			{
				if(t.type!=Team.PlayerType.dead)
				{
					this.DeclareVictory(t.name);
				}
			}
		}
	}
	
	public void DeclareVictory(string teamName)
	{
		Debug.Log(teamName+" wins"); //placeholder for victory screen
        GameObject.FindGameObjectWithTag("Music Cube").GetComponent<AudioSource>().enabled = false;
        GameObject.FindGameObjectWithTag("Victory Cube").GetComponent<AudioSource>().enabled = true;
    }
}
