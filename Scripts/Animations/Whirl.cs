using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirl : Animation 
{
	bool rotateClockwise;
	double speedOfRotation;
	bool revolveClockwise; 
	double radiusOfRevolution; 
	double speedOfRevolution; 
	long time;

	long startTime;

	Quaternion origionalRotation;

	public Whirl(bool rotateClockwise, double speedOfRotation, bool revolveClockwise, double radiusOfRevolution, double speedOfRevolution, long time)
	{
		this.speedOfRotation = speedOfRotation;
		this.speedOfRevolution = speedOfRevolution;
		this.rotateClockwise = rotateClockwise;
		this.revolveClockwise = revolveClockwise;
		this.time = time;
	}

	public override void Init()
	{
		origionalRotation = subject.transform.rotation;
	}

	public override void Action()
	{
		//Vector2 rot = rotateClockwise ? new Vector2(Mathf.Cos(-speedOfRotation),Mathf.Sin(-speedOfRotation)): new Vector2(Mathf.Cos(speedOfRotation),Mathf.Sin(speedOfRotation));
		subject.transform.Rotate (rot);
	}

	public override bool IsFinished()
	{
		return true;
	}

	public override void OnFinish()
	{

	}

}
