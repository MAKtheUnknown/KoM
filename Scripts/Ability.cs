using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Ability : MonoBehaviour
{
	public string description;
	public int cooldown;
	public int cooldownTimer;
	public bool ultimate;

	// Use this for initialization
	public abstract void Start ();
	
	// Update is called once per frame
	public abstract void Update ();

	public abstract string GetName();

	public abstract string GetDescription();

	public abstract void Use();
	
	//virtual means that it is implemented, but can still be overridden
	public virtual bool Available()
	{
		return true;
	}
	
}
