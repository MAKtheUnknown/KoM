using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ContinuousEffect : ActiveEffect
{
	/**
	 * Initialize the effect with a charachter and a time.
	 * c - the charachter the Effect will act upon
	 * turns - the number of rounds the Effect will last
	 */

	/**
	 * A method that is executed with each round.
	 */	

	/**
	 * A method to reset everything once the Effect's time is up;.
	 */
	 
	public void Check()
	{
			if(Condition())
				Activate();
	}
	
	public abstract bool Condition();
	
	public abstract void Activate();
}
