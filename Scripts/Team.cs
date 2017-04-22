using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Team : MonoBehaviour {

	public PlayerType type;

	public new string name;

	public CharacterCharacter[] pieces;

	public TileArrangement map;

	public TeamManager manager;
	
	public float teamMorale;
	
	public float maxMorale;
	
	public int activePieces;
	
	public GameObject moraleBar; // Morale bar corresponding to team
	
	public GameObject moraleText;// Morale text corresponding to team
	
	public MoraleTextAlign textAlign;//Alignment of text
	
	public double teamDefense=0; //defense against morale damage 

	void Awake()
	{
		map = this.GetComponentInParent<TileArrangement> ();
		manager = this.GetComponentInParent<TeamManager> ();
		pieces = this.GetComponentsInChildren<CharacterCharacter> ();
		activePieces=pieces.Length;
	}

	// Use this for initialization
	void Start () 
	{
		
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
		teamMorale-= (int)(d*(1-teamDefense));
		if(teamMorale<=0)
		{
			teamMorale=0;
			manager.RemoveTeam(this);
		}
		changeMorale();
	}
	
	public void changeMorale()
	{	//First half to change morale bar
		moraleBar.GetComponent<Transform>().transform.localScale = Vector3.Scale(moraleBar.GetComponent<Transform>().transform.localScale, new Vector3(1,0,1))+new Vector3(0,(float)(teamMorale)/maxMorale*100,0); //Currently using 100 as placeholder. Later use variable maxMorale
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
