using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Team : MonoBehaviour {

	public PlayerType type;

	public string name;

	public CharacterCharacter[] pieces;

	public TileArrangement map;

	public TeamManager manager;
	
	public float teamMorale;
	
	public float maxMorale;
	
	public GameObject moraleBar; // Morale bar corresponding to team
	
	public GameObject moraleText;// Morale text corresponding to team
	
	public MoraleTextAlign textAlign;//Alignment of text

	void Awake()
	{
		map = this.GetComponentInParent<TileArrangement> ();
		manager = this.GetComponentInParent<TeamManager> ();
		pieces = this.GetComponentsInChildren<CharacterCharacter> ();
		teamMorale=100; //for testing
		maxMorale=100; //for testing
	}

	// Use this for initialization
	void Start () 
	{
		for(int i = 0; i<manager.teams.Length; i++)
		{
			if(manager.teams[i]==this) //sets moraleBar to the morale moraleBar corresponding to the team
			{
				moraleBar = manager.moraleBars[i];
				moraleText= manager.moraleTexts[i];
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
	
	public void TeamDamage (double d)
	{
		teamMorale-= (int)d;
		if(teamMorale<=0)
		{
			teamMorale=0;
			manager.RemoveTeam(this);
		}
		changeMorale();
		Debug.Log("damage: "+d);
	}
	
	public void changeMorale()
	{	//First half to change morale bar
		moraleBar.GetComponent<Transform>().transform.localScale -= new Vector3(0,(float)(maxMorale-teamMorale)/maxMorale*100,0); //Currently using 100 as placeholder. Later use variable maxMorale
		if(textAlign==Team.MoraleTextAlign.left)
		{
			moraleBar.GetComponent<Transform>().transform.localPosition -=new Vector3((float)(maxMorale-teamMorale)/maxMorale*100,0,0);
		}
		else
		{
			moraleBar.GetComponent<Transform>().transform.localPosition +=new Vector3((float)(maxMorale-teamMorale)/maxMorale*100,0,0);
		}
		//Changes morale text
		moraleText.GetComponent<Text>().text=name+": "+teamMorale+"/"+maxMorale;
	}

	public enum PlayerType
	{
		human,
		computer,
		dead
	}
	
	public enum MoraleTextAlign
	{
		left,
		right
	}
}
