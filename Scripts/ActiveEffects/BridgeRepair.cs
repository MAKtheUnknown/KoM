using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BridgeRepair : ActiveEffect
{
	
	
	public BridgeRepair (CharacterCharacter c)
	{
		Init(c, 100);
	}
	
	public BridgeRepair (CharacterCharacter c, int r)
	{
		Init(c, r);
	}

	public new void Init(CharacterCharacter c, int rounds)
	{
		base.Init (c, rounds);
		type=ActiveEffect.EffectType.buff;
		
	}

	public override void Finish()
	{
		
	}
	
	public override void Act()
	{
		turnsLeft++;
	}
	
	public override ActiveEffect Clone(CharacterCharacter c)
	{
		return new BridgeRepair(c,turnsLeft);
	}
}


