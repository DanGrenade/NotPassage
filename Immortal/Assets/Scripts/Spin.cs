using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {

	public float spinSpeed;
	private Vector3 rotation;

	public void Start()
	{
		rotation = gameObject.transform.eulerAngles;
	}

	public void Update()
	{
		rotation.z += spinSpeed;
		gameObject.transform.eulerAngles = rotation;
	}
}
