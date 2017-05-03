using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RepairBridge : Ability  {

	public ClassSpecifications specs;

	TileArrangement map;
	Material breakableBridgeMat;

	// Use this for initialization
	public override void Start () 
	{
		name = "Repair Bridge";
		specs = GetComponentInParent<ClassSpecifications> ();
		map = GameObject.FindGameObjectWithTag ("Map").GetComponent<TileArrangement>();
		breakableBridgeMat = GetComponent<MeshRenderer>().material;
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
		RepairNearbyBridges(specs.owner.tile);
		
			map.highlighter.mode = Highlighter.SelectionMode.PIECE_TO_USE;
			Start ();
			specs.owner.usedAbility = true;
			cooldownTimer=cooldown;
		

	}
	
	public override bool Available()
	{
		bool neighboring=false;
		bool avail=false;
		
		if(specs.owner.tile.north!=null&&specs.owner.tile.north.type==TileAttributes.TileType.brokenBridge)
		{
			neighboring=true;
		}
		if(specs.owner.tile.east!=null&&specs.owner.tile.east.type==TileAttributes.TileType.brokenBridge)
		{
			neighboring=true;
		}
		if(specs.owner.tile.south!=null&&specs.owner.tile.south.type==TileAttributes.TileType.brokenBridge)
		{
			neighboring=true;
		}
		if(specs.owner.tile.west!=null&&specs.owner.tile.west.type==TileAttributes.TileType.brokenBridge)
		{
			neighboring=true;
		}
		
		foreach(ActiveEffect e in specs.owner.activeEffects)
		{
			if(e.GetType().Equals(typeof(BridgeRepair)))
				avail=true;
		}
		return avail&&neighboring&&specs.owner.tile.type!=TileAttributes.TileType.brokenBridge;
		
	}
	
	public override void AIUse(CharacterCharacter target)
	{
		this.Use();
	}
	
	void RepairNearbyBridges(TileAttributes t)
	{
		if(specs.owner.tile.north!=null&&t.north.type==TileAttributes.TileType.brokenBridge)
		{
			FixBridge(t.north);
			RepairNearbyBridges(t.north);
		}
		if(specs.owner.tile.east!=null&&t.east.type==TileAttributes.TileType.brokenBridge)
		{
			FixBridge(t.east);
			RepairNearbyBridges(t.east);
		}
		if(specs.owner.tile.south!=null&&t.south.type==TileAttributes.TileType.brokenBridge)
		{
			FixBridge(t.south);
			RepairNearbyBridges(t.south);
		}
		if(specs.owner.tile.west!=null&&t.west.type==TileAttributes.TileType.brokenBridge)
		{
			FixBridge(t.west);
			RepairNearbyBridges(t.west);
		}
	}
	
	void FixBridge(TileAttributes t)
	{
		t.type=TileAttributes.TileType.breakableBridge;
		t.GetComponent<Renderer>().material=breakableBridgeMat;
	}
}
