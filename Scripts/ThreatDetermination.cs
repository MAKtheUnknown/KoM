using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ThreatDetermination : MonoBehaviour {
	//This threat is filled with DETERMINATIOn
   TileArrangement map;
   	int searchRadius;
	CharacterCharacter highestThreat;
	List<CharacterCharacter> enemysInRange;
	double [] ratio;
	
	public void Threat (CharacterCharacter c){
		map = GameObject.FindGameObjectWithTag ("Map").GetComponent<TileArrangement>();
		enemysInRange = new List<CharacterCharacter> ();
		searchRadius = 5;
		
		//check characters move + attackRange
		enemysInRange =inRange(c.x,c.y,(float)searchRadius,c); 
		//if list has elements
		if(enemysInRange.Count!=0){
		//calculate ratio between morale and distance away
		getRatio(enemysInRange,c);
		//Move towards character with the most 'threat'
		highestThreat = getHighestThreat();
	    moveTowardsHighThreat(c,highestThreat);
		if(!c.usedAbility)
			useStrongestAbility(c,highestThreat);
		}
		//else find the closest enemy and move towards
		else{
			moveTowardsHighThreat(c,findClosest(c));
			
		}
	}
	
	public CharacterCharacter findClosest(CharacterCharacter ai){
		CharacterCharacter nearestEnemy = null;
		double minDistance= 9001.0;
			foreach (Team t in map.teams.teams) 
		{
			foreach (CharacterCharacter c in t.pieces) 
			{
				if(c.team != ai.team){
					if (distance(c.x,c.y,ai.x,ai.y)<minDistance){
						minDistance = distance(c.x,c.y,ai.x,ai.y);
						nearestEnemy = c;
					}
				}
			}
		}
		return nearestEnemy;
	}
	
	public void getRatio(List<CharacterCharacter> enemy,CharacterCharacter c){
		ratio = new double[enemy.Count];
		for(int i = 0; i < enemy.Count; i++)
		{
			ratio[i] = ((double)enemy[i].type.morale / distance(enemy[i].x,enemy[i].y,c.x,c.y)) / (1f*enemy[i].currentHP/enemy[i].type.maximumHealth);
		}
	}
	public void moveTowardsHighThreat(CharacterCharacter ai, CharacterCharacter c){
		 List<TileAttributes> possibleMoves = new List<TileAttributes>();
		 double closest =9001.0;
		 int closestIndex = 0;
		 possibleMoves = ((LimitedSpaces)(ai.type.movement)).GetPossibleMoves();
		 
		 if(possibleMoves.Count!=0)
		 {
		 for(int i = 0; i<possibleMoves.Count;i++){
		  if(Math.Abs(distance(possibleMoves[i].x, possibleMoves[i].y, c.x,c.y)-(ai.type.range)) < Math.Abs(closest-(ai.type.range)))
		  {
			  closest = distance(possibleMoves[i].x, possibleMoves[i].y, c.x,c.y);
			  closestIndex =i;
		  }
		 }
		 if(Math.Abs(distance(possibleMoves[closestIndex].x,possibleMoves[closestIndex].y,c.x,c.y)-(ai.type.range))<=Math.Abs(distance(ai.x,ai.y,c.x,c.y)-(ai.type.range)))
		 {
			((LimitedSpaces)(ai.type.movement)).Move(possibleMoves[closestIndex]);			 
		 }
			 
		 }
	}
	public CharacterCharacter getHighestThreat()
	{
		int highIndex = 0;
		for(int i = 0; i < ratio.Length; i++){
			if( ratio[i] >ratio[highIndex])
				highIndex = i;
		}
		return enemysInRange[highIndex];
		
	}
	public double distance(int x1,int y1, int x2, int y2){
		int x = (x2-x1)*(x2-x1);
		int y = (y2-y1)*(y2-y1);
		
		return Math.Sqrt(x+y);
	}
	
	List<CharacterCharacter> inRange(int x, int y, float range, CharacterCharacter host)
	{
		List<CharacterCharacter> charTargets = new List<CharacterCharacter> ();

		foreach (Team t in map.teams.teams) 
		{
			foreach (CharacterCharacter c in t.pieces) 
			{
				if(c.team != host.team){
				int rsqrd = (int)(range * range);
				int dxsqrd = (x - c.x) * (x - c.x);
				int dysqrd = (y - c.y) * (y - c.y);
				bool b = !(x == c.x && y == c.y);
				if ((rsqrd >= dxsqrd + dysqrd) && b) 
				{
					charTargets.Add (c);
				}
				}
			}
		}
		return charTargets;
	}
	
	public void useStrongestAbility(CharacterCharacter user, CharacterCharacter target)
	{
		Ability abilityToUse=null;
		foreach(Ability a in user.specialAbilities)
		{
			if(a.ultimate==true&&a.cooldownTimer<=0)
				abilityToUse=a;
		}
		
		while(abilityToUse==null||abilityToUse.cooldownTimer>0)
		{
			abilityToUse=user.specialAbilities[(int)UnityEngine.Random.Range(0,user.specialAbilities.Length-1)];
		}
		
		abilityToUse.AIUse(target);
	}
	
}
