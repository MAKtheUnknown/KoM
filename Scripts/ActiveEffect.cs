using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveEffect {

	public CharacterCharacter subject;
	public int turnsLeft;
	public EffectType type;

	/**
	 * Initialize the effect with a charachter and a time.
	 * c - the charachter the Effect will act upon
	 * turns - the number of rounds the Effect will last
	 */
	public virtual void Init (CharacterCharacter c, int rounds)
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
	
	public abstract ActiveEffect Clone(CharacterCharacter c);
	
	/** Types of effects **/
	public enum EffectType
	{
		buff,
		debuff,
		passive
	};

}
