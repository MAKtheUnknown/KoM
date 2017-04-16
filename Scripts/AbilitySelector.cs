using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AbilitySelector : MonoBehaviour {

	public TileArrangement map;

	public GameObject baseLabel;
	
	GameObject UIcamera;
	//Vector3 originalUIpos; //retrieved from map (arbitrarily chosen)
	//Vector3 UIposition;
	float cameraSize;
	//float scale=85.775f;//Scale for text to properly adjust to UI camera movement

	public float initialOffset;
	public float offset;

	// Use this for initialization
	void Start () 
	{
		initialOffset = 0.7f;
		offset = 0.0f;

		map = GameObject.FindGameObjectWithTag ("Map").GetComponent<TileArrangement> ();
		
		UIcamera = GameObject.Find("UI Camera");
		//originalUIpos=map.uiCameraPosition;
		//UIposition = UIcamera.transform.position;
		cameraSize=UIcamera.GetComponent<Camera>().orthographicSize;
		initialOffset*=cameraSize/3.5f;
		/*
		gameObject.transform.Translate (new Vector3(308f/85.75285f, 2f, -2f));//TODO: Figure out why you need such weird values.
		//gameObject.transform.position = new Vector3(1/(3f*cameraSize),2f+1/(3f*cameraSize),-100/85.75285f);
		gameObject.transform.Translate (Vector3.Scale(new Vector3(1,1,0),(UIposition-originalUIpos)));*/
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
		if(a.Available() && a.cooldownTimer <= 0)
		{
			GameObject l = Instantiate (baseLabel, gameObject.transform.position, new Quaternion (), gameObject.transform);
			//l.transform.Translate (Vector3.Scale(new Vector3(1,1,0),(UIposition-originalUIpos)));
			SelectAbilityLabel s = l.GetComponent<SelectAbilityLabel> ();
			s.selector = this;
			s.SetAbility (a);
			l.transform.Translate (new Vector3 (0, -initialOffset-offset, 0));
			//offset += .3f;
			offset += .3f*cameraSize/3.5f;
		}
	}
}
