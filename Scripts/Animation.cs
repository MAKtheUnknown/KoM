using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animation
{
	public GameObject subject;

	public List<Animation> endTriggers = new List<Animation>();

	public bool initialized = false;

	// Use this for initialization
	public abstract void Init ();

	public abstract void Action ();

	public abstract bool IsFinished ();

	public abstract void OnFinish();

	public void AddEndTrigger(Animation a)
	{
		endTriggers.Add (a);
	}
}
