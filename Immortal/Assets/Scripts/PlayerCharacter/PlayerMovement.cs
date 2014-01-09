using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private Vector2 velocityHolder;
	private float movementValue;
	public float drag = 0.87f;
	public float accel = 2f;
	public float MaximumSpeed = 6f;
	private float inputValue;

	public Rigidbody2D pivotRotater;
	public float rotateSpeedScale = 0.01f;
	void Update () 
	{
		#region Get Input
		inputValue = 0;
		if(Input.GetKey(KeyCode.S))
		{
			inputValue -= accel;
		}
		if(Input.GetKey(KeyCode.W))
		{
			inputValue += accel;
		}
		#endregion

		#region Apply Movement
		movementValue += inputValue;
		movementValue *= drag;

		if(Mathf.Abs(movementValue) > MaximumSpeed)
		{
			movementValue = Mathf.Sign(movementValue) * MaximumSpeed;
		}
		rigidbody2D.velocity = (transform.position - pivotRotater.transform.position).normalized * movementValue;
		#endregion

		#region Rotate around centerpoint
		Vector3 hey = pivotRotater.transform.eulerAngles;
		hey.z -= 10/(transform.position - pivotRotater.transform.position).magnitude * rotateSpeedScale * Time.deltaTime;
		pivotRotater.transform.eulerAngles = hey;


		#endregion
	}
}
