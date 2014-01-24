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
	
	private GameObject Protagonist;

	#region Movement
	private Vector3 rotate;
	[System.NonSerialized]
	public Vector2  FollowGoTo;

	private float RunSpeed = 15f;
	private float RunMaxSpeed = 30f;

	private float TowardsSpeed = 20f;
	private float TowardsMaxSpeed = 30f;

	private float FollowSpeed = 40f;
	private float FollowMaxSpeed = 50f;
	#endregion

	#region Time
	public float MaxTime;
	private float currentTime;
	public float AgeSpeed;

	private bool aged = false;
	public float timeToOld;
	public Sprite spriteWhenOld;
	#endregion

	public bool hasGooglyEyes;
	private Vector2 VectorToPlayer;
	private Vector3 rotationHolder;
	private float rotation;
	public GameObject eyeOne;
	public GameObject eyeTwo;

	public void Awake()
	{
		rotate = transform.eulerAngles;
		rotate.z = Vector2.Angle (Vector2.up, PivotRotater - (Vector2)transform.position);
		if (PivotRotater.x < transform.position.x) rotate.z -= 180;
		else rotate.z = 180 - rotate.z;
		transform.eulerAngles = rotate;

		Protagonist = GameObject.FindGameObjectWithTag ("Player");

		if(hasGooglyEyes)	rotationHolder = eyeOne.transform.eulerAngles;

		currentTime = MaxTime;

		ChildStart ();
	}

	public virtual void ChildStart()
	{

	}

	public void Update()
	{
		if(!aged && timeToOld >= currentTime)
		{
			aged = true;
			gameObject.GetComponent<SpriteRenderer>().sprite = spriteWhenOld;
		}

		VectorToPlayer = (Vector2)transform.position - (Vector2)Protagonist.transform.position;

		switch(currentAIState)
		{
		case AIState.Run:
			if((Protagonist.transform.position - transform.position).sqrMagnitude < 4)
			{
				rigidbody2D.velocity = Vector2.ClampMagnitude((rigidbody2D.velocity + VectorToPlayer).normalized * Time.deltaTime * RunSpeed, RunMaxSpeed);
			}
			break;
		case AIState.MoveTowards:
			if((Protagonist.transform.position - transform.position).sqrMagnitude < 4)
			{
				rigidbody2D.velocity = Vector2.ClampMagnitude((rigidbody2D.velocity + -VectorToPlayer * Time.deltaTime * TowardsSpeed), TowardsMaxSpeed);

			}
			break;
		case AIState.Following:
			rigidbody2D.velocity = Vector2.ClampMagnitude(((rigidbody2D.velocity + (Vector2)((Vector2)Protagonist.transform.TransformPoint(FollowGoTo) - (Vector2)transform.position)) * Time.deltaTime * FollowSpeed), FollowMaxSpeed);

			break;
		}

		if(hasGooglyEyes)
		{
			rotation = Vector2.Angle (Vector2.up, VectorToPlayer);
			if(Protagonist.transform.position.x > transform.position.x)	rotation *= -1;
			rotation = 360 - rotation;
			rotation -= 90;

			rotationHolder.z = rotation;
			eyeOne.transform.eulerAngles = rotationHolder;
			eyeTwo.transform.eulerAngles = rotationHolder;
		}
		

		rotate = transform.eulerAngles;
		rotate.z = Vector2.Angle (Vector2.up, PivotRotater - (Vector2)transform.position);
		if (PivotRotater.x < transform.position.x) rotate.z -= 180;
		else rotate.z = 180 - rotate.z;
		transform.eulerAngles = rotate;

		currentTime -= (AgeSpeed * Time.deltaTime);

		if(currentTime < 0)
		{
			if(currentAIState == AIState.Following)
			{
				Protagonist.GetComponent<PlayerScore>().RemoveScore(1, this);
			}
			GameObject.Destroy (gameObject);
		}
	}

	public void ChildTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player" && currentAIState != AIState.Following)
		{
			currentAIState = AIState.Following;
			Protagonist.GetComponent<PlayerScore>().AddScore(1, this);
		}
	}
}
