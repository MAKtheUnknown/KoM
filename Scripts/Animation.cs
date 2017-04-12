using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Animation
{

	// Use this for initialization
	void Init (GameObject subject);

	void Action ();

	bool IsFinished ();

	void OnFinish();
}
