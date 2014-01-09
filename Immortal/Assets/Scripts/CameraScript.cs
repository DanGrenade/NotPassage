using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public GameObject Protagonist;

	// Update is called once per frame
	void Update () 
	{
		transform.position = Protagonist.transform.position;
		transform.rotation = Protagonist.transform.rotation;
	}
}
