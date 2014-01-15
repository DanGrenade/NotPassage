using UnityEngine;
using System.Collections;

public abstract class AI : MonoBehaviour 
{
	protected enum AIState
	{
		Ignore, Run, MoveTowards, Following
	};
	protected AIState currentAIState;

	public Vector2 PivotRotater;

	[System.NonSerialized]
	private GameObject Protagonist;

	#region Movement
	private Vector3 rotate;

	private float AIReactSpeed = 25f;
	private float MaximumSpeed = 50f;

	private float RunSpeed = 15f;
	private float RunMaxSpeed = 30f;

	private float TowardsSpeed = 20f;
	private float TowardsMaxSpeed = 30f;

	private float FollowSpeed = 30f;
	private float FollowMaxSpeed = 40f;
	#endregion

	#region Time
	public float MaxTime;
	private float currentTime;
	public float AgeSpeed;
	#endregion

	public void Awake()
	{
		rotate = transform.eulerAngles;
		rotate.z = Vector2.Angle (Vector2.up, PivotRotater - (Vector2)transform.position);
		if (PivotRotater.x < transform.position.x) rotate.z -= 180;
		else rotate.z = 180 - rotate.z;
		transform.eulerAngles = rotate;

		Protagonist = GameObject.FindGameObjectWithTag ("Player");

		currentTime = MaxTime;

		ChildStart ();
	}

	public virtual void ChildStart()
	{

	}

	public void Update()
	{
		switch(currentAIState)
		{
		case AIState.Run:
			if((Protagonist.transform.position - transform.position).sqrMagnitude < 2)
			{
				rigidbody2D.velocity = Vector2.ClampMagnitude(((rigidbody2D.velocity + ((Vector2)transform.position - (Vector2)Protagonist.transform.position)).normalized * Time.deltaTime * RunSpeed), RunMaxSpeed);
			}
			break;
		case AIState.MoveTowards:
			if((Protagonist.transform.position - transform.position).sqrMagnitude < 2)
			{
				rigidbody2D.velocity = Vector2.ClampMagnitude(((rigidbody2D.velocity + (Vector2)Protagonist.transform.position - (Vector2)transform.position).normalized * Time.deltaTime * TowardsSpeed), TowardsMaxSpeed);

			}
			break;
		case AIState.Following:
			rigidbody2D.velocity = Vector2.ClampMagnitude(((rigidbody2D.velocity + (Vector2)Protagonist.transform.position - (Vector2)transform.position).normalized * Time.deltaTime * FollowSpeed), FollowMaxSpeed);

			break;
		}

		rotate = transform.eulerAngles;
		rotate.z = Vector2.Angle (Vector2.up, PivotRotater - (Vector2)transform.position);
		if (PivotRotater.x < transform.position.x) rotate.z -= 180;
		else rotate.z = 180 - rotate.z;
		transform.eulerAngles = rotate;

		currentTime -= (AgeSpeed * Time.deltaTime);

		if(currentTime < 0)
		{
			Protagonist.GetComponent<PlayerScore>().RemoveScore(10);
			GameObject.Destroy (this);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player" && currentAIState != AIState.Following)
		{
			currentAIState = AIState.Following;
			Protagonist.GetComponent<PlayerScore>().AddScore(10);
		}
	}
}
