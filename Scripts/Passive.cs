using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Passive : ContinuousEffect
{
	/**
	 * Initialize the effect with a charachter and a time.
	 * c - the charachter the Effect will act upon
	 * turns - the number of rounds the Effect will last
	 */

	/**
	 * A method that is executed with each round.
	 */
	public override void Init(CharacterCharacter c, int rounds)
	{
		subject = c;
		turnsLeft=rounds;
		type = ActiveEffect.EffectType.passive;
	}
	
	public override void Act()
	{
		turnsLeft++;
	}
	

	/**
	 * A method to reset everything once the Effect's time is up;.
	 */
	
	public override void Finish()
	{
		
	}
}
