using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScore : MonoBehaviour 
{
	private int Score = 0;
	private int scoreDisplayed = 0;

	private float scoreDisplayedFloat = 0;

	public Vector2 DisplayLocation;
	private List<GameObject> numberDisplays;
	private int tempInt;

	private Sprite[] numbers;

	public void Start()
	{
		//numbers = Resources.LoadAll("ScoreFont");
	}

	public void Update()
	{
		if(scoreDisplayed != Score)
		{
			scoreDisplayedFloat = Mathf.Lerp (scoreDisplayedFloat, Score, Time.deltaTime * 0.1f);
			scoreDisplayed = (int)scoreDisplayedFloat;

			/*do
			{


			} while(tempInt > 0);*/

		}
	}

	public void AddScore(int _scoreToAdd)
	{
		Score += _scoreToAdd;
	}

	public void RemoveScore(int _scoreToRemove)
	{
		Score -= _scoreToRemove;
	}

}
