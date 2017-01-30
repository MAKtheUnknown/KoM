using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Contains all information about a charachter's class; their abilities
 */
public class ClassSpecifications : MonoBehaviour {

	public IDictionary<TileAttributes.TileType, double> tileTypeDifficulties;

	public ClassSpecifications nextLevel;

	public Ability basicMovement;

	public Ability[] classAbilities;

	public double grassSpeed = 1;
	public double hillSpeed = 1;
	public double mountainSpeed = 1;
	public double forestSpeed = 1;
	public double shallowWaterSpeed = 1;

	void Awake()
	{

		tileTypeDifficulties = new Dictionary<TileAttributes.TileType, double> ();
		tileTypeDifficulties.Add (TileAttributes.TileType.grass, grassSpeed);
		tileTypeDifficulties.Add (TileAttributes.TileType.hills, hillSpeed);
		tileTypeDifficulties.Add (TileAttributes.TileType.mountains, mountainSpeed);
		tileTypeDifficulties.Add (TileAttributes.TileType.trees, forestSpeed);
		tileTypeDifficulties.Add (TileAttributes.TileType.water, shallowWaterSpeed);
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
