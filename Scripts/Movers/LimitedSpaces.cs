using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LimitedSpaces : MonoBehaviour, Mover {

	public ClassSpecifications moved;

	void Awake()
	{
		moved = GetComponentInParent<ClassSpecifications> ();
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public List<TileAttributes> GetPossibleMoves()
	{
		TileArrangement = moved.owner.
	}

	public void Reset()
	{

	}
}
