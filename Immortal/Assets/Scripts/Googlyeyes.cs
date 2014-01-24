using UnityEngine;
using System.Collections;

public class Googlyeyes : MonoBehaviour {

	private Vector2 VectorToPlayer;
	private float rotation;
	public GameObject Protagonist;

	private Vector3 rotationHolder;

	private GameObject eyeOne;
	private GameObject eyeTwo;

	void Start()
	{
		rotationHolder = eyeOne.transform.eulerAngles;
	}

	// Update is called once per frame
	void Update () {
		VectorToPlayer = transform.position - Protagonist.transform.position;

		rotation = Vector2.Angle (Vector2.up, VectorToPlayer);
		if(Protagonist.transform.position.x > transform.position.x)	rotation *= -1;
		rotation = 360 - rotation;
		rotation -= 90;
		
		rotationHolder.z = rotation;
		eyeOne.transform.eulerAngles = rotationHolder;
		eyeTwo.transform.eulerAngles = rotationHolder;
	}
}
