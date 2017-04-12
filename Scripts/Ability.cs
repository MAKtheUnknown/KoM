using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Ability
{
	public string description;
	public int cooldown;

	// Use this for initialization
	void Start ();
	
	// Update is called once per frame
	void Update ();

	string GetName();

	string GetDescription();

	void Use();
}
