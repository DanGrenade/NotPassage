using UnityEngine;
using System.Collections;

public class AiChildHit : MonoBehaviour {

	public AI ai;

	void OnTriggerEnter2D(Collider2D other)
	{
		ai.ChildTriggerEnter2D(other);
	}
}
