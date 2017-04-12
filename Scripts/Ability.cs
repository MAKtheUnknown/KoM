﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Ability : MonoBehaviour
{
	public string description;
	public int cooldown;

	// Use this for initialization
	public abstract void Start ();
	
	// Update is called once per frame
	public abstract void Update ();

	public abstract string GetName();

	public abstract string GetDescription();

	public abstract void Use();
}
