using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private Vector2 velocityHolder;
	private float movementValue;
	public float Drag = 0.87f;
	public float Accel = 2f;
	public float MaximumSpeed = 6f;
	private float inputValue;
	public Rigidbody2D PivotRotater;


	void Update () 
	{
		#region Get Input
		inputValue = 0;
		if(Input.GetKey(KeyCode.S))
		{
			inputValue -= Accel;
		}
		if(Input.GetKey(KeyCode.W))
		{
			inputValue += Accel;
		}
		#endregion

		#region Apply Movement
		movementValue += inputValue;
		movementValue *= Drag;

		if(Mathf.Abs(movementValue) > MaximumSpeed)
		{
			movementValue = Mathf.Sign(movementValue) * MaximumSpeed;
		}
		rigidbody2D.velocity = (transform.position - PivotRotater.transform.position).normalized * movementValue;
		#endregion
	}
}
