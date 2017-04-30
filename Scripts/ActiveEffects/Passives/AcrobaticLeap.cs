using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcrobaticLeap : Passive {

	/**Passive (script) doesn't do anything. The actual passive dodges basic attacks. Since this seems to be unique to this passive, I've hard coded it into basic attack script**/
	public AcrobaticLeap(CharacterCharacter c)
	{
		Init (c);
	}

	public void Init(CharacterCharacter c)
	{
		this.Init (c, 100);
	}

	public new void Init(CharacterCharacter c, int rounds)
	{
		base.Init (c, rounds);
	}
	
	public override bool Condition()
	{
		return false;
	}
	public override void Activate()
	{
		
	}
	
	public override ActiveEffect Clone(CharacterCharacter c)
	{
		return new AcrobaticLeap(c);
	}
}
