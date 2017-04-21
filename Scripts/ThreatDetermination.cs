using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatDetermination : MonoBehaviour {
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
		if(enemysInRange != null){
		//calculate ratio between morale and distance away
		getRatio(enemysInRange,c);
		//Move towards character with the most 'threat'
		highestThreat = getHighestThreat();
	    moveTowardsHighThreat(c,highestThreat);
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
		for(int i = 0; i < enemy.Count -1; i++)
		{
			ratio[i] = ((double)enemy[i].type.morale / distance(enemy[i].x,enemy[i].y,c.x,c.y)) / (enemy[i].currentHP/enemy[i].type.maximumHealth);
		}
	}
	public void moveTowardsHighThreat(CharacterCharacter ai, CharacterCharacter c){
		 List<TileAttributes> possibleMoves = new List<TileAttributes>();
		 double closest =9001.0;
		 int closestIndex = 0;
		 possibleMoves = ((LimitedSpaces)(ai.type.movement)).GetPossibleMoves();
		 for(int i = 0; i<possibleMoves.Count-1;i++){
		  if(distance(possibleMoves[i].x, possibleMoves[i].y, c.x,c.y) > closest)
		  {
			  closest = distance(possibleMoves[i].x, possibleMoves[i].y, c.x,c.y);
			  closestIndex =i;
		  }
		 }
		 
		 ((LimitedSpaces)(ai.type.movement)).Move(possibleMoves[closestIndex]);
	}
	public CharacterCharacter getHighestThreat()
	{
		int highIndex = 0;
		for(int i = 0; i < ratio.Length -1; i++){
			if( ratio[i] >ratio[highIndex])
				highIndex = i;
		}
		return enemysInRange[highIndex];
		
	}
	public double distance(int x1,int y1, int x2, int y2){
		int x = (x2-x1)*(x2-x1);
		int y = (y2-y1)*(y2-y1);
		
		return Mathf.Sqrt(x+y);
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
	
}
