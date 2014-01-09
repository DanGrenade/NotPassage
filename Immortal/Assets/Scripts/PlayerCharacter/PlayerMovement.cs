using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private Vector2 MovementVector = Vector2.zero;
	public float drag = 0.87f;
	public float accel = 2f;
	public float MaximumSpeed = 6f;
	private Vector2 inputVector;

	public Rigidbody2D pivotRotater;
	public float rotateSpeedScale = 0.0000000000000000001f;

	void Update () 
	{
		#region Get Input
		inputVector = Vector2.zero;
		if(Input.GetKey(KeyCode.S))
		{
			inputVector.y -= 1;
		}
		if(Input.GetKey(KeyCode.W))
		{
			inputVector.y += 1;
		}
		#endregion

		#region Apply Movement
		MovementVector += (inputVector * accel);
		MovementVector *= drag;

		if(MovementVector.magnitude > MaximumSpeed)
		{
			MovementVector = MovementVector.normalized * MaximumSpeed;
		}

		MovementVector.x = rigidbody2D.velocity.x;
		rigidbody2D.velocity = MovementVector;
		#endregion

		#region Rotate around centerpoint
		Vector3 hey = pivotRotater.transform.eulerAngles;
		hey.z += (transform.position - pivotRotater.transform.position).magnitude * rotateSpeedScale;
		pivotRotater.transform.eulerAngles = hey;


		#endregion
	}
}
