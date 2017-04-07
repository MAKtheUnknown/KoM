using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface Ability
{

	// Use this for initialization
	void Start ();
	
	// Update is called once per frame
	void Update ();

	string GetName();

	string GetDescription();

	void Use();
}
