using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileEffect {

	public Team source;
	public int turnsLeft;
	public TileAttributes subject;

	/**
	 * Initialize the effect with a team and a time.
	 * t - the team the Effect will act upon
	 * turns - the number of rounds the Effect will last
	 */
	public void Init (TileAttributes s, Team t, int rounds)
	{
		subject = s;
		source = t;
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
