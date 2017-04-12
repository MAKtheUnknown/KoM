using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Contains all information about a charachter's class; their abilities
 */
public class ClassSpecifications : MonoBehaviour 
{
	/**The charachter of this class*/
	public CharacterCharacter owner;


	/**The class to which this class advances.*/
	public ClassSpecifications nextLevel;

	/**The style of movement for this class.*/
	public Mover movement;

	/**The attacks, spells, or special abilities that this class has*/
	public Ability[] classAbilities;

	public int maximumHealth;
	
	public int attack;
	
	public int defense;

	public int maxXP;

	public int morale;

	public CharacterType charClass;

	void Awake()
	{
		owner = GetComponentInParent<CharacterCharacter> ();
		movement = GetComponentInChildren<Mover> ();
		classAbilities = GetComponentsInChildren<Ability> ();
		nextLevel = GetComponentInChildren<ClassSpecifications> ();

	}

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
	
	public enum CharacterType
	{
		Swordsman,
		Alchemist,
		Bard,
		Priest
	};
}