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
		direction = (new Vector3 (targetX - sourceX, targetY - sourceY, 0.0)).normalized.Scale (new Vector3 (speed, speed, speed));
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
		
		return false;
	}

	public override void OnFinish()
	{

	}
}
