using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileEffect {

	public CharacterCharacter source;
	public int turnsLeft;
	public TileAttributes subject;

	/**
	 * Initialize the effect with a charachter and a time.
	 * c - the charachter the Effect will act upon
	 * turns - the number of rounds the Effect will last
	 */
	public void Init (TileAttributes s, CharacterCharacter c, int rounds)
	{
		subject = s;
		source = c;
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
