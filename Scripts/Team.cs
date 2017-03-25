using UnityEngine;
using System.Collections;

public class Team : MonoBehaviour {

	public PlayerType type;

	public string name;

	public CharacterCharacter[] pieces;

	public TileArrangement map;

	public TeamManager manager;
	
	public float teamMorale;
	
	public GameObject bar; //

	void Awake()
	{
		map = this.GetComponentInParent<TileArrangement> ();
		manager = this.GetComponentInParent<TeamManager> ();
		pieces = this.GetComponentsInChildren<CharacterCharacter> ();
	}

	// Use this for initialization
	void Start () 
	{
		for(int i = 0; i<manager.teams.Length; i++)
		{
			if(manager.teams[i]==this) //sets bar to the morale bar corresponding to the team
			{
				bar = manager.moraleBars[i];
			}
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public bool Contains(CharacterCharacter p)
	{
		foreach(CharacterCharacter c in pieces)
		{
			if(c == p)
			{
				return true;
			}
		}
		return false;
	}

	public enum PlayerType
	{
		human,
		computer,
		dead
	}
	
	public void TeamDamage (double d)
	{
		teamMorale-= (int)d;
		if(teamMorale<=0)
		{
			teamMorale=0;
			manager.RemoveTeam(this);
		}
		changeMorale();
	}
	
	public void changeMorale()
	{
		bar.GetComponent<Transform>().transform.localScale -= new Vector3(0,(100-teamMorale),0); //Currently using 100 as placeholder. Later use variable maxMorale
		bar.GetComponent<Transform>().transform.localPosition -=new Vector3((100-teamMorale),0,0);
	}
}
