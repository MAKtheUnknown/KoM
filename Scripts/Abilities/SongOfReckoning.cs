using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongOfReckoning : CharachterTargeter {


	public double damage;
	public int range;

	public ClassSpecifications specs;

	// Use this for initialization
	public override void Start () 
	{
		name="Song of Reckoning";
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
	public void Update () 
	{

	}



	public override void Use()
	{
		damage=specs.attack;
		if (targetsAquired == false) 
		{
			base.GetTargets(specs.owner.x, specs.owner.y, range);
		}
		if (targetsAquired == true) 
		{
			foreach (CharacterCharacter t in charachterTargets) 
			{
				t.damage (damage);
				t.activeEffects.Add(new Stunned(t));
			}
			map.highlighter.mode = Highlighter.SelectionMode.PIECE_TO_USE;
			Start ();
			specs.owner.usedAbility = true;
		}

	}
}
