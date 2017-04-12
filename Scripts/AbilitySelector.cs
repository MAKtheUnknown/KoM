using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySelector : MonoBehaviour {

	public TileArrangement map;

	public GameObject baseLabel;

	public float initialOffset;
	public float offset;

	// Use this for initialization
	void Start () 
	{
		initialOffset = 0.7f;
		offset = 0.0f;

		map = GameObject.FindGameObjectWithTag ("Map").GetComponent<TileArrangement> ();
		gameObject.transform.Translate (new Vector3(308f/85.75285f, 2f, -2f));//TODO: Figure out why you need such weird values.
		gameObject.transform.localScale.Set(1, 1, 1);

		foreach (Ability a in map.highlighter.chosenCharacter.specialAbilities) 
		{
			AddLabel (a);
		}
		/*foreach (Ability a in map.highlighter.chosenCharacter.type.classAbilities) 
		{
			AddLabel (a);
		}*/


	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void AddLabel(Ability a)
	{
		if(a.Available() && a.cooldown <= 0)
		{
			GameObject l = Instantiate (baseLabel, new Vector3(308f/85.75285f, 2f, -2f), new Quaternion (), gameObject.transform);
			SelectAbilityLabel s = l.GetComponent<SelectAbilityLabel> ();
			s.selector = this;
			s.SetAbility (a);
			l.transform.Translate (new Vector3 (0, -initialOffset-offset, 0));
			offset += .3f;
		}
	}
}
