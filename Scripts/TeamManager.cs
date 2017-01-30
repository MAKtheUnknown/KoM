using UnityEngine;
using System.Collections;

public class TeamManager : MonoBehaviour {

	TileArrangement map;
	Team[] teams;
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

	}
	
	// Update is called once per frame
	void Update () 
	{
		Team t = teams [turn % teams.Length];

	}

	void rotate()
	{
		turn = (turn+1)%teams.Length;
	}
}
