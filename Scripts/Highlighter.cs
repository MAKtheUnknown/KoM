using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class Highlighter : MonoBehaviour {

	bool downStroke = true;
	long timeOfKeyDown = 0;
	long timeOfLastMove = 0;

	public int timeToMoveAfterDownStroke = 250;
	public int holdMoveWaitTime = 15;

	public int tilesToSelect = 1;

	public TileArrangement map;
	public TileAttributes selectedTile;

	public SelectionMode mode = SelectionMode.PIECE_TO_USE;

	public Stack<TileAttributes> chosenTiles; 
	public CharacterCharacter chosenCharacter;

	public Team currentTeam;

	public GameObject possibleMovesHighlighter;
	public List<GameObject> possibleMoveHighlighters;

	public GameObject savedAbilitySelectorObject;
	public GameObject abilitySelectorObject;
	public AbilitySelector abilitySelector;
	public Ability abilityInUse;

	public bool mouseIsOverSidebar = false;

	void Awake()
	{
		map = GetComponentInParent<TileArrangement> ();
		chosenTiles = new Stack<TileAttributes> ();
		possibleMoveHighlighters = new List<GameObject> ();
		abilitySelectorObject = savedAbilitySelectorObject;
		abilitySelector = abilitySelectorObject.GetComponent<AbilitySelector> ();
	}

	// Use this for initialization
	void Start () 
	{
		currentTeam = map.teams.teams [0];
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (currentTeam.type == Team.PlayerType.human
			&& selectedTile != null) 
		{

			if (Input.GetKey (KeyCode.LeftArrow)) 
			{
				move (Direction.west);
			}
			if (Input.GetKeyUp (KeyCode.LeftArrow)) 
			{
				downStroke = true;
			}
			if (Input.GetKey (KeyCode.RightArrow)) 
			{
				move (Direction.east);
			}
			if (Input.GetKeyUp (KeyCode.RightArrow)) 
			{
				downStroke = true;
			}

			if (Input.GetKey (KeyCode.UpArrow)) {
				move (Direction.north);
			}
			if (Input.GetKeyUp (KeyCode.UpArrow)) {
				downStroke = true;
			}

			if (Input.GetKey (KeyCode.DownArrow)) {
				move (Direction.south);
			}
			if (Input.GetKeyUp (KeyCode.DownArrow)) {
				downStroke = true;
			}

			if (selectedTile.containedCharacter != null) 
			{
                updateSidebar();
			}

			this.executeSelectionModeActions ();
		}
	}

    void updateSidebar()
    {
        GameObject.FindGameObjectWithTag("Character Display").GetComponent<SpriteRenderer>().sprite = selectedTile.containedCharacter.GetComponent<SpriteRenderer>().sprite;
        GameObject.FindGameObjectWithTag("Character Display").transform.localScale = new Vector3(75, 75, 1);
        GameObject.FindGameObjectWithTag("Health Indicator").transform.localScale = new Vector3(11.80754f, 71.82333f * selectedTile.containedCharacter.currentHP / selectedTile.containedCharacter.type.maximumHealth, 85.7285f);
        GameObject.FindGameObjectWithTag("Health Indicator").transform.localPosition = new Vector3(369.7f, 17f + 72f * selectedTile.containedCharacter.currentHP / selectedTile.containedCharacter.type.maximumHealth, -102);
        GameObject.FindGameObjectWithTag("Health Label").GetComponent<Text>().text = selectedTile.containedCharacter.currentHP + "/" + selectedTile.containedCharacter.type.maximumHealth;
		GameObject.FindGameObjectWithTag("Name Label").GetComponent<Text>().text = selectedTile.containedCharacter.name;
    }

	void move(Direction d)
	{

		if (downStroke == true)
		{
			moveInDirection (d);
			timeOfKeyDown = System.DateTime.UtcNow.Ticks / 10000;
			downStroke = false; 
		}
		else 
		{
			if (System.DateTime.UtcNow.Ticks / 10000 - timeOfKeyDown > timeToMoveAfterDownStroke
				&& System.DateTime.UtcNow.Ticks / 10000 - timeOfLastMove > holdMoveWaitTime) 
			{
				moveInDirection (d);
				timeOfLastMove = System.DateTime.UtcNow.Ticks / 10000;
			}
		}
	}

	void moveInDirection(Direction d)
	{
		switch (d) 
		{
		case Direction.west:
			if(selectedTile.west != null)
				selectedTile = selectedTile.west;
			break;
		case Direction.north:
			if(selectedTile.north != null)
				selectedTile = selectedTile.north;
			break;
		case Direction.south:
			if(selectedTile.south != null)
				selectedTile = selectedTile.south;
			break;
		case Direction.east:
			if(selectedTile.east != null)
				selectedTile = selectedTile.east;
			break;
		}

		this.gameObject.transform.position = selectedTile.transform.position;
	}

	void executeSelectionModeActions()
	{
		switch (mode) 
		{
		case SelectionMode.PIECE_TO_USE:
			//For selecting tiles with charachters on them.
			if (selectedTile.containedCharacter != null && currentTeam.Contains(selectedTile.containedCharacter)) 
			{
                    //expand over piece.
                    gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                    //gameObject.transform.localScale = new Vector3 (1.459983f,1.459983f,1f);

                    //Use Enter key to highlight possible moves
				if ((Input.GetKeyUp (KeyCode.Return) || Input.GetMouseButtonUp (0)) && selectedTile.containedCharacter.usedAbility == false) 
				{
					chosenCharacter = selectedTile.containedCharacter;
					abilitySelectorObject = GameObject.Instantiate (savedAbilitySelectorObject, (new Vector3(0f,0f,0f))/*GameObject.Find("Ability Instruction Panel").transform.position*/ /*new Vector3(0,0,0)*/ /*new Vector3(308, 0, -101)*/, new Quaternion(), map.ui.transform);
					abilitySelector = abilitySelectorObject.GetComponent<AbilitySelector> ();


					//Instantiate(Resources.Load("Panels/Ability Select Panel"));
					mode = SelectionMode.HIGHLIGHT_POSSIBLE_MOVES;
				}
			} 
            /*else 
			{
				
				gameObject.transform.localScale = new Vector3 (1f,1f,1f);
			}*/
			break;
		case SelectionMode.HIGHLIGHT_POSSIBLE_MOVES: //an init state for MOVE_TO_TILE
			foreach (GameObject g in possibleMoveHighlighters) 
			{
				GameObject.Destroy (g);
			}
			chosenCharacter.HighlightMoves();
			mode = SelectionMode.MOVE_TO_TILE;
			break;
		case SelectionMode.MOVE_TO_TILE: 
			if (Input.GetKeyUp (KeyCode.Return) || Input.GetMouseButtonUp(0)) 
			{
				if (selectedTile.GetComponentInChildren<RectTransform>() != null)
				{
					chosenCharacter.type.movement.Move(selectedTile);
				}
				foreach (GameObject g in possibleMoveHighlighters) 
				{
					GameObject.Destroy (g);
				}
				Destroy (abilitySelectorObject);
				mode = SelectionMode.PIECE_TO_USE;
			}
			break;
		case SelectionMode.USE_ABILITY:
			abilityInUse.Use();
				foreach (GameObject g in possibleMoveHighlighters) 
				{
					GameObject.Destroy (g);
				}
			break;
		case SelectionMode.SELECT_TARGETS:
			abilityInUse.Use ();
			if (Input.GetKeyUp (KeyCode.Return) || Input.GetMouseButtonUp (0)) 
			{
				((CharachterTargeter)abilityInUse).targets.Add (selectedTile);
			}
			break;
		case SelectionMode.SELECT_TILES:
			abilityInUse.Use();
			if (Input.GetKeyUp (KeyCode.Return) || Input.GetMouseButtonUp (0)) 
			{
				((TileTargeter)abilityInUse).targets.Add (selectedTile);
			}
			break;
		}

	}

	void resetPossibleTiles()
	{
		possibleMoveHighlighters.Clear ();
	}

	public Stack<TileAttributes> requestSelections(int number)
	{
		return null;
	}

	enum Direction
	{
		north,
		south,
		east,
		west
	}

	public enum SelectionMode
	{
		PIECE_TO_USE,
		HIGHLIGHT_POSSIBLE_MOVES,
		MOVE_TO_TILE,
		USE_ABILITY,
		SELECT_TARGETS,
		SELECT_TILES,
		INACTIVE
	}
}
