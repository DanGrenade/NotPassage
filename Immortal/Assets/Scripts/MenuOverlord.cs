using UnityEngine;
using System.Collections;

public class MenuOverlord : MonoBehaviour 
{
	public bool InMenu = true;
	public bool InGame = false;

	public float zoomInMenu;
	public Vector2 cameraPositionInMenu;
	public float zoomInGame;
	public GameObject protagonist;

	public float zoomSpeed;


	// Use this for initialization
	void Start () 
	{
		Camera.main.orthographicSize = zoomInMenu;
		Camera.main.transform.position = cameraPositionInMenu;
		Time.timeScale = 0;
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
			}

			if(!InMenu)
			{
				Camera.main.transform.position = Vector2.Lerp(Camera.main.transform.position, protagonist.transform.position, Time.deltaTime * zoomSpeed);
				if(transform.position == protagonist.transform.position)
				{
					Camera.main.transform.parent = protagonist.transform;
					Time.timeScale = 1;
				}
			}
		}
	}
}
