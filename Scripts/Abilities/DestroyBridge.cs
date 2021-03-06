﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DestroyBridge : Ability  {

	public ClassSpecifications specs;

	TileArrangement map;
	Material brokenBridgeMat;

	// Use this for initialization
	public override void Start () 
	{
		name = "Destroy Bridge";
		specs = GetComponentInParent<ClassSpecifications> ();
		map = GameObject.FindGameObjectWithTag ("Map").GetComponent<TileArrangement>();
		brokenBridgeMat = GetComponent<MeshRenderer>().material;
	}
	
	// Update is called once per frame
	public override void Update () 
	{
	
	}
		
	public override string GetName()
	{
		return name;
	}

	public override string GetDescription()
	{
		return description;
	}

	public override void Use()
	{
		DestroyNearbyBridges(specs.owner.tile);
		
			map.highlighter.mode = Highlighter.SelectionMode.PIECE_TO_USE;
			Start ();
			specs.owner.usedAbility = true;
			cooldownTimer=cooldown;
		

	}
	
	public override bool Available()
	{
		bool neighboring=false;
		
		if(specs.owner.tile.north!=null&&specs.owner.tile.north.type==TileAttributes.TileType.breakableBridge)
		{
			neighboring=true;
		}
		if(specs.owner.tile.east!=null&&specs.owner.tile.east.type==TileAttributes.TileType.breakableBridge)
		{
			neighboring=true;
		}
		if(specs.owner.tile.south!=null&&specs.owner.tile.south.type==TileAttributes.TileType.breakableBridge)
		{
			neighboring=true;
		}
		if(specs.owner.tile.west!=null&&specs.owner.tile.west.type==TileAttributes.TileType.breakableBridge)
		{
			neighboring=true;
		}
		
		return neighboring&&specs.owner.tile.type!=TileAttributes.TileType.breakableBridge;
		
	}
	
	public override void AIUse(CharacterCharacter target)
	{
		this.Use();
	}
	
	void DestroyNearbyBridges(TileAttributes t)
	{
		if(specs.owner.tile.north!=null&&t.north.type==TileAttributes.TileType.breakableBridge)
		{
			BreakBridge(t.north);
			DestroyNearbyBridges(t.north);
		}
		if(specs.owner.tile.east!=null&&t.east.type==TileAttributes.TileType.breakableBridge)
		{
			BreakBridge(t.east);
			DestroyNearbyBridges(t.east);
		}
		if(specs.owner.tile.south!=null&&t.south.type==TileAttributes.TileType.breakableBridge)
		{
			BreakBridge(t.south);
			DestroyNearbyBridges(t.south);
		}
		if(specs.owner.tile.west!=null&&t.west.type==TileAttributes.TileType.breakableBridge)
		{
			BreakBridge(t.west);
			DestroyNearbyBridges(t.west);
		}
	}
	
	void BreakBridge(TileAttributes t)
	{
		t.type=TileAttributes.TileType.brokenBridge;
		t.GetComponent<Renderer>().material=brokenBridgeMat;
		if(t.containedCharacter!=null)
			t.containedCharacter.returnToStart();
		
	}
}
