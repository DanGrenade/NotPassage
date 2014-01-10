using UnityEngine;
using System.Collections;

public class MoveAroundCenterpoint : MonoBehaviour 
{
	public Rigidbody2D PivotRotater;                                                                                                                                                                           
	private float RotateSpeedScale = 0.2f;
	private float xSpeed = 10.0f;
	private Vector3 rotatingPoint;

	// Update is called once per frame
	void Update () 
	{
		rotatingPoint = PivotRotater.transform.eulerAngles;
		rotatingPoint.z -= (10/(transform.position - PivotRotater.transform.position).magnitude * RotateSpeedScale) * xSpeed  * Time.deltaTime;
		PivotRotater.transform.eulerAngles = rotatingPoint;
	}
}
