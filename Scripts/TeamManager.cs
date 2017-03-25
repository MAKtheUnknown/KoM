﻿using UnityEngine;
using System.Collections;
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
		
		teams[0].moraleText.GetComponent<Text>().fontStyle=FontStyle.Bold;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		Team t = teams [turn % teams.Length];

	}

	public void rotate()
	{
		turn = (turn+1)%teams.Length;
		map.highlighter.currentTeam = teams [turn];

		//prepare all the team's pieces for the new turn.
		foreach (CharacterCharacter c in teams[turn].pieces) 
		{
			c.type.movement.Reset ();
			c.usedAbility = false;
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
	}
}
