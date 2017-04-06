using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveEffect {

	public CharacterCharacter subject;
	public int turnsLeft;

	/**
	 * Initialize the effect with a charachter and a time.
	 * c - the charachter the Effect will act upon
	 * turns - the number of rounds the Effect will last
	 */
	public void Init (CharacterCharacter c, int rounds)
	{
		subject = c;
		turnsLeft = rounds;
	}

	/**
	 * A method that is executed with each round.
	 */
	public abstract void Act();

	/**
	 * A method to reset everything once the Effect's time is up;.
	 */
	public abstract void Finish();

}
