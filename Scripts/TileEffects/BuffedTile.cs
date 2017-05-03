using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffedTile : MonoBehaviour {

	public Team source;
	public int turnsLeft;
	public TileAttributes subject;

	public void Start()
	{
		subject=GetComponentInParent<TileAttributes>();
		source=subject.map.teams.teams[0];
		turnsLeft=100;
	}
	
	public void Update()
	{
		if(subject.containedCharacter!=null)
		{
			GiveBuff(subject.containedCharacter);
			GameObject.Destroy(this);
		}
	}
	
	public abstract void GiveBuff(CharacterCharacter c);	
}
