using System;

using UnityEngine;

public class Shake : Animation
{
	GameObject subject;
	double magnitude;
	double frequency;
	Vector3 direction;
	int cycles;
	float startTime;

	public Shake (GameObject subject, double magnitude, double frequency, Vector3 direction)
	{
		this.subject = subject;
		this.magnitude = magnitude;
		this.frequency = frequency;
		this.direction = direction.normalized;
		cycles = 9001;
	}

	public Shake (GameObject subject, double magnitude, double frequency, Vector3 direction, int cycles)
	{
		this.subject = subject;
		this.magnitude = magnitude;
		this.frequency = frequency;
		this.direction = direction.normalized;
		this.cycles = cycles;
	}

	public override void Init()
	{
		startTime = Time.time;
	}

	public override void Action()
	{
		subject.transform.Translate (new Vector3 ((float) (magnitude * Math.Cos (2 * Math.PI * direction.x * Time.time)), (float) (magnitude * Math.Cos (2 * Math.PI * direction.y*Time.time)), 0f));
	}

	public override bool IsFinished()
	{
		if (frequency * (Time.time - startTime) >= cycles) 
		{
			return true;
		}
		return false;
	}

	public override void OnFinish()
	{

	}
}

