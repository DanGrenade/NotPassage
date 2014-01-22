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

	private float AIReactSpeed = 50f;
	private float MaximumSpeed = 150f;

	private float RunSpeed = 30f;
	private float RunMaxSpeed = 60f;

	private float TowardsSpeed = 40f;
	private float TowardsMaxSpeed = 60f;

	private float FollowSpeed = 60f;
	private float FollowMaxSpeed = 80f;
	#endregion

	#region Time
	public float MaxTime;
	private float currentTime;
	public float AgeSpeed;

	private bool aged = false;
	public float timeToOld;
	public Sprite spriteWhenOld;
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
		if(!aged && timeToOld >= currentTime)
		{
			aged = true;
			gameObject.GetComponent<SpriteRenderer>().sprite = spriteWhenOld;
		}

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
			rigidbody2D.velocity = Vector2.ClampMagnitude(((rigidbody2D.velocity + (Vector2)((Vector2)Protagonist.transform.TransformPoint(FollowGoTo) - (Vector2)transform.position)).normalized * Time.deltaTime * FollowSpeed), FollowMaxSpeed);

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
			if(currentAIState == AIState.Following)
			{
				Protagonist.GetComponent<PlayerScore>().RemoveScore(1, this);
			}
			GameObject.Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player" && currentAIState != AIState.Following)
		{
			currentAIState = AIState.Following;
			Protagonist.GetComponent<PlayerScore>().AddScore(1, this);
		}
	}
}
