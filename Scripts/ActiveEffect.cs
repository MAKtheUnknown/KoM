using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveEffect {

	public int turnsLeft;

	/**
	 * Initialize the effect with a charachter and a time.
	 * c - the charachter the Effect will act upon
	 * turns - the number of rounds the Effect will last
	 */
	public void Init (CharacterCharacter c, int rounds); 

	/**
	 * A method that is executed with each round.
	 */
	public void Act();

	/**
	 * A method to reset everything once the Effect's time is up;.
	 */
	public void Finish();

}
