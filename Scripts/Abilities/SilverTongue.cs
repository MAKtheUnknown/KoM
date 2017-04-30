using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SilverTongue : CharachterTargeter {

	public ClassSpecifications specs;

	public double damage;
	public float range;
	public double moraleMutliplier;

	// Use this for initialization
	public override void Start () 
	{
		name="Silver Tongue";
		specs = GetComponentInParent<ClassSpecifications> ();
		map = GameObject.FindGameObjectWithTag ("Map").GetComponent<TileArrangement>();
		targets = new List<TileAttributes> ();
		charachterTargets = new List<CharacterCharacter> ();
		targetsAquired = false;
		targetsToAquire = numberOfTargets;
		GameObject.Destroy (GameObject.FindGameObjectWithTag("Ability Selector"));
		base.Start ();
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
		damage=specs.attack;
		range=specs.range;
		if (targetsAquired == false) 
		{
			GetEnemyTargetsUnderPercent(specs.owner.x, specs.owner.y, range,specs.owner.team);
		}
		if (targetsAquired == true) 
		{
			foreach (CharacterCharacter t in charachterTargets) 
			{
				t.type.morale+=(int)(t.type.morale*moraleMutliplier);
				t.kill();
				if(t!=null)
				{
					t.type.morale-=(int)(t.type.morale/6.0);
				}
			}
			map.highlighter.mode = Highlighter.SelectionMode.PIECE_TO_USE;
			Start ();
			specs.owner.usedAbility = true;
			cooldownTimer=cooldown;
		}

	}
	
	public override void AIUse(CharacterCharacter target)
	{
		damage=specs.attack;
		range=specs.range;
		int count=0;
		charachterTargets= new List<CharacterCharacter>();
		foreach(CharacterCharacter c in base.GetTargetsInRange(specs.owner.x,specs.owner.y,specs.range))
		{
			if(c.team!=specs.owner.team&&count<numberOfTargets&&(double)c.currentHP/c.type.maximumHealth<=.3)
			{
				charachterTargets.Add(c);
				count++;
			}
		}
		
		
		foreach (CharacterCharacter t in charachterTargets) 
		{
			t.type.morale+=(int)(t.type.morale*moraleMutliplier);
			t.kill();
		}
		Start ();
		specs.owner.usedAbility = true;
		cooldownTimer=cooldown;
	}
	
	
	public  void GetEnemyTargetsUnderPercent(int x, int y, float range, Team team)
	{
		double underHPpercent =.3;
		List<CharacterCharacter> inRange = GetTargetsInRange (x, y, range);
		
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Possible Move Highlighters")) 
		{
			GameObject.Destroy (g);
		}
		map.highlighter.mode = Highlighter.SelectionMode.SELECT_TARGETS;
		if (instructionLabel.text.Equals ("Select " + targetsToAquire + "pieces.") == false) 
		{
			GameObject.Destroy (GameObject.FindGameObjectWithTag("Ability Selector"));
		}
		instructionLabel.text = "Select " + targetsToAquire + " pieces.";

		if (targets.Count > 0) 
		{
			TileAttributes t = targets [0];
			if (t.containedCharacter != null) 
			{
				if (inRange.Contains (t.containedCharacter)&&t.containedCharacter.team!=team&&(double)t.containedCharacter.currentHP/t.containedCharacter.type.maximumHealth<=underHPpercent) 
				{
					charachterTargets.Add (t.containedCharacter);
					targetsToAquire--;
				}
				else
				{
					map.highlighter.mode = Highlighter.SelectionMode.PIECE_TO_USE;
					instructionLabel.text = "";
				}
			}
			else 
			{
				instructionLabel.text = "";
				map.highlighter.mode = Highlighter.SelectionMode.PIECE_TO_USE;
			}
			targets.Remove (t);
		}

		if (targetsToAquire <= 0) 
		{
			targetsAquired = true;
			instructionLabel.text = "";
		}
	}
}
