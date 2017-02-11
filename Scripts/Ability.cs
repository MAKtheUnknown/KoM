using UnityEngine;
using System.Collections;

public interface Ability 
{

	// Use this for initialization
	void Start ();
	
	// Update is called once per frame
	void Update ();

	void GetName();

	void GetDescription();

	void Use();
}
