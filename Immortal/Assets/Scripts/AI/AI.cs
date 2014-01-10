using UnityEngine;
using System.Collections;

public abstract class AI : MonoBehaviour 
{
	protected enum AIState
	{
		Ignore, Run, MoveTowards, Following
	};
	protected AIState currentAIState;

	public GameObject PivotRotater;
	public GameObject Protagonist;

	private Vector3 rotate;

	private float AIReactSpeed = 10f;
	private float MaximumSpeed = 30f;

	public void Start()
	{
		rotate = transform.eulerAngles;
		rotate.z = Vector2.Angle (Vector2.up, PivotRotater.transform.position - transform.position);
		if (PivotRotater.transform.position.x < transform.position.x) rotate.z -= 180;
		else rotate.z = 180 - rotate.z;
		transform.eulerAngles = rotate;

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
				rigidbody2D.velocity = Vector2.ClampMagnitude(((rigidbody2D.velocity + ((Vector2)transform.position - (Vector2)Protagonist.transform.position)).normalized * Time.deltaTime * AIReactSpeed), MaximumSpeed);
			}
			break;
		case AIState.MoveTowards: 
			if((Protagonist.transform.position - transform.position).sqrMagnitude < 2)
			{
				rigidbody2D.velocity = Vector2.ClampMagnitude(((rigidbody2D.velocity + (Vector2)Protagonist.transform.position - (Vector2)transform.position).normalized * Time.deltaTime * AIReactSpeed), MaximumSpeed);

			}
			break;
		case AIState.Following:
			rigidbody2D.velocity = Vector2.ClampMagnitude(((rigidbody2D.velocity + (Vector2)Protagonist.transform.position - (Vector2)transform.position).normalized * Time.deltaTime * AIReactSpeed), MaximumSpeed);

			break;
		}


         ChildUpdate();

		rotate = transform.eulerAngles;
		rotate.z = Vector2.Angle (Vector2.up, PivotRotater.transform.position - transform.position);
		if (PivotRotater.transform.position.x < transform.position.x) rotate.z -= 180;
		else rotate.z = 180 - rotate.z;
		transform.eulerAngles = rotate;
	}

	public virtual void ChildUpdate()
	{

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			currentAIState = AIState.Following;
		}
	}
}
