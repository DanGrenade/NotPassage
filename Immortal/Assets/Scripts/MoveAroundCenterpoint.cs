using UnityEngine;
using System.Collections;

public class MoveAroundCenterpoint : MonoBehaviour 
{
	public Rigidbody2D PivotRotater;
	private float RotateSpeedScale = 0.01f;
	private float xSpeed = 1.0f;

	// Update is called once per frame
	void Update () 
	{
		Vector3 hey = PivotRotater.transform.eulerAngles;
		hey.z -= (10/(transform.position - PivotRotater.transform.position).magnitude * RotateSpeedScale) * xSpeed  * Time.deltaTime;
		PivotRotater.transform.eulerAngles = hey;
	}
}
