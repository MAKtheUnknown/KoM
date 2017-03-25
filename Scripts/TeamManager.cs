using UnityEngine;
using System.Collections;

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
	}
}
