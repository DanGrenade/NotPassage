using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	private Vector2 MovementVector;
	public float drag = 0.87f;
	public float accel = 2f;
	public float MaximumSpeed = 6f;
	private Vector2 inputVector;

	void Start()
	{

	}

	void Update () 
	{
		#region Get Input
		inputVector = Vector2.zero;
		if(Input.GetKey(KeyCode.A))
		{
			inputVector.x -= 1;
		}
		if(Input.GetKey(KeyCode.D))
		{
			inputVector.x += 1;
		}

		if(Input.GetKey(KeyCode.W))
		{
			inputVector.y += 1;
		}

		if(Input.GetKey(KeyCode.S))
		{
			inputVector.y -= 1;
		}
		#endregion

		#region Apply Movement
		MovementVector += (inputVector * accel);
		MovementVector *= drag;

		if(MovementVector.magnitude > MaximumSpeed)
		{
			MovementVector = MovementVector.normalized * MaximumSpeed;
		}

		rigidbody2D.Velocity = MovementVector;
		#endregion


	}
}
