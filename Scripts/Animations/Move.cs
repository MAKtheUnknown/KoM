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

	public Move (GameObject subject, TileAttributes source, TileAttributes target, double speed)
	{
		this.subject = subject;
		sourceX = source.x;
		sourceY = source.y;
		targetX = target.x;
		targetY = source.y;
		this.speed = speed;
		direction = (new Vector3 ((float)(targetX - sourceX), (float)(targetY - sourceY), 0.0f));
		direction = direction.normalized;
		direction.Scale (new Vector3 ((float)speed, (float)speed, (float)speed));
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
		if (Math.Abs (subject.transform.position.x - targetX) < 5 && Math.Abs (subject.transform.position.y - targetY) < 5) 
		{
			return true;
		}
		return false;
	}

	public override void OnFinish()
	{

	}
}
