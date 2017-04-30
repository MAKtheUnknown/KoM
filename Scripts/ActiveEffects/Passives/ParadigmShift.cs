using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ParadigmShift : Passive {

	private TileAttributes lastTile;
	
	private ClassSpecifications specs;
	private int defenseBonus=3;
	private int movementBonus=1;
	private int attackBonus;
	private int healthBonus;
	
	private double attackPercent=.15;
	private double healthPercent=.10;

	public ParadigmShift(CharacterCharacter c)
	{
		Init (c);
	}

	public void Init(CharacterCharacter c)
	{
		Init (c, 100);
	}

	public new void Init(CharacterCharacter c, int rounds)
	{
		attackBonus=Dround(c.type.attack*(attackPercent));
		healthBonus=Dround(c.type.maximumHealth*(healthPercent));
		base.Init (c, rounds);
		lastTile = c.tile;
		NewTile();
		specs=c.type;
	}
	
	public override void Activate()
	{
		OldTile();
		NewTile();
		lastTile = base.subject.tile;
}
	
	public override bool Condition()
	{
		return lastTile.type!=base.subject.tile.type;
	}
	
	void OldTile()
	{
		if(lastTile.type==TileAttributes.TileType.mountains)
			FromMountain();
		if(lastTile.type==TileAttributes.TileType.grass)
			FromGrass();
		if(lastTile.type==TileAttributes.TileType.hills)
			FromHill();
		if(lastTile.type==TileAttributes.TileType.trees)
			FromForest();			
	}
	
	void NewTile()
	{
		if(base.subject.tile.type==TileAttributes.TileType.mountains)
			OnMountain();
		if(base.subject.tile.type==TileAttributes.TileType.grass)
			OnGrass();
		if(base.subject.tile.type==TileAttributes.TileType.hills)
			OnHill();
		if(base.subject.tile.type==TileAttributes.TileType.trees)
			OnForest();		
	}
	
	//type: mountains
	void FromMountain()
	{
		specs.defense-=defenseBonus;		
	}
	
	//type: grass
	void FromGrass()
	{
		((LimitedSpaces)base.subject.type.movement).timeToMove-=movementBonus;
		((LimitedSpaces)base.subject.type.movement).timeSpent=Math.Max(((LimitedSpaces)base.subject.type.movement).timeSpent,0);
	}
	//type: hills
	void FromHill()
	{
		base.subject.type.attack-=attackBonus;
		
	}
	
	//type: trees
	void FromForest()
	{
		
		base.subject.type.maximumHealth-=healthBonus;
		base.subject.currentHP=Dround((base.subject.currentHP*(1/(1+healthPercent))));
		if(base.subject.currentHP<=0)
		{
			base.subject.currentHP=1;
		}
		if(base.subject.currentHP>base.subject.type.maximumHealth)
		{
			base.subject.currentHP=base.subject.type.maximumHealth;
		}
	}
	
	//type: mountains
	void OnMountain()
	{
		specs.defense+=defenseBonus;
	}
	
	//type: grass
	void OnGrass()
	{
		((LimitedSpaces)base.subject.type.movement).timeToMove+=movementBonus;
		
	}
	//type: hills
	void OnHill()
	{
		base.subject.type.attack+=attackBonus;
		
	}
	
	//type: trees
	void OnForest()
	{
		base.subject.type.maximumHealth+=healthBonus;
		base.subject.currentHP=Dround((base.subject.currentHP*(1+healthPercent)));
		
	}
	
	int Dround(double d)
	{
		return (int)Math.Round(d);
	}
	
	public override ActiveEffect Clone(CharacterCharacter c)
	{
		return new ParadigmShift(c);
	}
}
