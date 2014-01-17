using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour 
{

	public GameObject Protagonist;
	public GameObject Title;
	public GameObject play;
	public GameObject quit;

	public GameObject endOfGameUI;

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

	public float timeToDisplayExit = 1500f;

	// Use this for initialization
	void Start ()
	{
		Camera.main.orthographicSize = zoomInMenu;
		Camera.main.transform.position = cameraPositionInMenu;
		Time.timeScale = 0;
		rotationVector = RotationObject.transform.eulerAngles;

		endOfGameUI.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!InGame)
		{
			if(InMenu)
			{
				if(Input.GetKeyDown(KeyCode.Space))
				{
					InMenu = false;
					Title.SetActive(false);
					play.SetActive(false);
					quit.SetActive(false);
				}
				if(Input.GetKeyDown (KeyCode.Escape))
				{
					Application.Quit();
				}

				rotationVector.z += rotationSpeed;
				RotationObject.transform.eulerAngles = rotationVector;
			}
			
			if(!InMenu)
			{
				Camera.main.transform.position = Vector2.Lerp(Camera.main.transform.position, protagonist.transform.position, zoomSpeed);
				Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, zoomInGame, zoomSpeed);
				rotationVector.z = Mathf.LerpAngle(rotationVector.z, 0, 0.3f);
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

			if(timeToDisplayExit < Time.time)
			{
				if(!endOfGameUI.activeSelf)
				{
					endOfGameUI.SetActive(true);
				}

				if(Input.GetKeyDown (KeyCode.Escape))
				{
					Application.LoadLevel("GameScene");
				}
			}
		}
	}
}
