using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class TeamManager : MonoBehaviour {

	public TileArrangement map;
	public Team[] teams;
	
	int turn;

	void Awake()
	{
		map = this.GetComponentInParent<TileArrangement> ();
		teams = this.GetComponentsInChildren<Team> ();
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
				if(c.type.charClass==ClassSpecifications.CharacterType.Swordsman)
				{
					
				}
				
				if(c.type.charClass==ClassSpecifications.CharacterType.Alchemist)
				{
					
				}
				
				if(c.type.charClass==ClassSpecifications.CharacterType.Bard)
				{
					c.activeEffects.Add(new RisingTempo(c));
				}
				
				if(c.type.charClass==ClassSpecifications.CharacterType.Priest)
				{
					
				}
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
		
		//bolds the active team's text
		foreach(Team x in teams)
		{
			x.moraleText.GetComponent<Text>().fontStyle= FontStyle.Normal;
			if(x==teams[turn%teams.Length])
			{
				x.moraleText.GetComponent<Text>().fontStyle= FontStyle.Bold;
			}
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
