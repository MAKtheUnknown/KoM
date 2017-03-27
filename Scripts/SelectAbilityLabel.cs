using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectAbilityLabel : MonoBehaviour {

	public Ability associatedAbility;

	public AbilitySelector selector;

	public string abilityName;

	public string description;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonUp (0)) 
		{
			/*selector.map.highlighter.mode = Highlighter.SelectionMode.USE_ABILITY;
			selector.map.highlighter.abilityInUse = associatedAbility;
			GameObject.Destroy (GameObject.FindGameObjectWithTag("Ability Selector"));*/
		}
	}

	public void OnMouseUp()
	{
		selector.map.highlighter.mode = Highlighter.SelectionMode.USE_ABILITY;
		selector.map.highlighter.abilityInUse = associatedAbility;
		GameObject.Destroy (GameObject.FindGameObjectWithTag("Ability Selector"));
	}

	void OnMouseOver()
	{
		Text label = gameObject.GetComponent<Text> ();
		label.fontStyle = FontStyle.Bold;
	}

	void OnMouseExit()
	{
		Text label = gameObject.GetComponent<Text> ();
		label.fontStyle = FontStyle.Normal;
	}

	public void SetAbility(Ability a)
	{
		associatedAbility = a;
		abilityName = a.GetName();
		description = a.GetDescription();

		Text label = gameObject.GetComponent<Text> ();
		label.text = abilityName;
	}
		
}
