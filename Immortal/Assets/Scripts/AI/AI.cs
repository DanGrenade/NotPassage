using UnityEngine;
using System.Collections;

public abstract class AI : MonoBehaviour 
{
	public GameObject PivotRotater;

	public void Start()
	{
		SetStartPosition (new Vector2(-1, 1));
	}

	public virtual void SetStartPosition(Vector2 start)
	{
		transform.localPosition = start;
		Vector3 rotate = transform.eulerAngles;
		rotate.z = Vector2.Angle (Vector2.up, PivotRotater.transform.position - transform.position);
		if (PivotRotater.transform.position.z > transform.position.z)	rotate.z = 360 - rotate.z;
		transform.eulerAngles = rotate;
	}
}
