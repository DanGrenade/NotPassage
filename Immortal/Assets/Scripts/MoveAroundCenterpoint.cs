﻿using UnityEngine;
using System.Collections;

public class MoveAroundCenterpoint : MonoBehaviour 
{
	public Rigidbody2D PivotRotater;                                                                                                                                                                           
	private float RotateSpeedScale = 0.2f;
	private float xSpeed = 17.0f;
	private Vector3 rotatingPoint;

	private float maximumDistance = 22f;

	// Update is called once per frame
	void Update () 
	{
		if((transform.position - PivotRotater.transform.position).magnitude > maximumDistance)
		{
			transform.position = ((transform.position - PivotRotater.transform.position).normalized * maximumDistance) + PivotRotater.transform.position;
		}

		rotatingPoint = PivotRotater.transform.eulerAngles;
		rotatingPoint.z -= (10/(transform.position - PivotRotater.transform.position).magnitude * RotateSpeedScale) * xSpeed  * Time.deltaTime;
		PivotRotater.transform.eulerAngles = rotatingPoint;
	}
}
