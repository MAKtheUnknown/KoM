using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffedTile : TileEffect {

	public void Start()
	{
		subject=GetComponentInParent<TileAttributes>();
		source=subject.map.teams.teams[0];
		turnsLeft=100;
	}
	
	public override void Act()
	{
		turnsLeft++;
	}

	public override void Finish()
	{
		
	}
	
	public void Update()
	{
		if(subject.containedCharacter!=null)
		{
			GiveBuff(subject.containedCharacter);
			subject.tileEffects.Remove(this);
			GameObject.Destroy(this);
		}
	}
	
	public abstract void GiveBuff(CharacterCharacter c);	
}
