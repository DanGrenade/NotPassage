using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour 
{

	public GameObject Protagonist;

	public bool InMenu = true;
	public bool InGame = false;
	
	public float zoomInMenu;
	public Vector2 cameraPositionInMenu;
	public float zoomInGame;
	public GameObject protagonist;
	public float startDist;
	
	public float zoomSpeed;

	public GameObject RotationObject;
	private Vector3 rotationVector;
	public float rotationSpeed;

	// Use this for initialization
	void Start () 
	{
		Camera.main.orthographicSize = zoomInMenu;
		Camera.main.transform.position = cameraPositionInMenu;
		Time.timeScale = 0;
		rotationVector = RotationObject.transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!InGame)
		{
			if(InMenu)
			{
				if(Input.GetKeyDown(KeyCode.Return))
				{
					InMenu = false;
				}
				if(Input.GetKeyDown (KeyCode.Escape))
				{
					Application.Quit();
				}

				rotationVector.x += rotationSpeed;
				RotationObject.transform.eulerAngles = rotationVector;
			}
			
			if(!InMenu)
			{
				Camera.main.transform.position = Vector2.Lerp(Camera.main.transform.position, protagonist.transform.position, zoomSpeed);
				Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, zoomInGame, zoomSpeed);
				rotationVector.x = Mathf.LerpAngle(rotationVector.x, 0, 0.3f);
				RotationObject.transform.eulerAngles = rotationVector;

				if((transform.position - protagonist.transform.position).sqrMagnitude <= startDist)
				{
					Camera.main.transform.parent = protagonist.transform;
					Time.timeScale = 1;
				}
			}
		}
		else
		{
			transform.position = Protagonist.transform.position;
			transform.rotation = Protagonist.transform.rotation;
		}
	}

}
