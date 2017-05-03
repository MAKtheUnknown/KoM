using System;

using UnityEngine;

public class Move:Animation
{
	double sourceX;
	double sourceY;

	double targetX;
	double targetY;

	double speed;

	Vector3 direction;

	TileArrangement map;

	public Move (GameObject subject, TileAttributes source, TileAttributes target, double speed)
	{
		this.subject = subject;
		sourceX = source.x;
		sourceY = source.y;
		targetX = target.x;
		targetY = target.y;
		this.speed = speed;
		direction = (new Vector3 ((float)(targetX - sourceX), (float)(targetY - sourceY), 0.0f));
		direction = direction.normalized;
		direction.Scale (new Vector3 ((float)speed, (float)speed, (float)speed));

		map = target.map;
	}

	public override void Init()
	{

	}

	public override void Action()
	{
		subject.transform.Translate (direction);
	}

	public override bool IsFinished()
	{
		if ( Math.Pow(Math.Pow(subject.transform.position.x-map.LowX - targetX, 2) + Math.Pow(subject.transform.position.y-map.LowY - targetY, 2), .5) < .5) 
		{
			return true;
		}
		return false;
	}

	public override void OnFinish()
	{

	}
}
