using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScore : MonoBehaviour 
{
	private int Score = 0;
	private int scoreDisplayed = 0;

	private float scoreDisplayedFloat = 0;

	public Vector2 DisplayLocation;
	public GameObject basePointObject;
	public float distBetweenPoints;
	private List<SpriteRenderer> numberDisplays = new List<SpriteRenderer>();
	private List<int> scoreList = new List<int>();
	private int tempInt;

	private Sprite[] numbers;

	public void Start()
	{
		numbers = Resources.LoadAll<Sprite>("ScoreFont");
		do
		{
			scoreList.Add (tempInt%10);
			tempInt /= 10;
			
		} while(tempInt > 0);
		
		while(scoreList.Count > numberDisplays.Count)
		{
			GameObject tempObject = (GameObject)GameObject.Instantiate(basePointObject) as GameObject;
			tempObject.transform.parent = gameObject.transform;
			tempObject.transform.localPosition = DisplayLocation;
			numberDisplays.Add (tempObject.GetComponent<SpriteRenderer>());
			DisplayLocation.x -= distBetweenPoints;
		}
		
		for(int i = 0; i < numberDisplays.Count; i++)
		{
			numberDisplays[i].sprite = numbers[scoreList[i]];
		}
		scoreList.Clear();
	}

	public void Update()
	{
		if(scoreDisplayedFloat != Score)
		{
			scoreDisplayedFloat = Mathf.Lerp (scoreDisplayedFloat, Score, Time.deltaTime * 5f);
			scoreDisplayed = Mathf.RoundToInt(scoreDisplayedFloat);
			tempInt = scoreDisplayed;

			scoreList.Clear();
			do
			{
				scoreList.Add (tempInt % 10);
				tempInt /= 10;

			} while(tempInt > 0);

			while(scoreList.Count > numberDisplays.Count)
			{
				GameObject tempObject = (GameObject)GameObject.Instantiate(basePointObject) as GameObject;
				tempObject.transform.parent = gameObject.transform;
				tempObject.transform.eulerAngles = transform.eulerAngles;
				tempObject.transform.localPosition = DisplayLocation;
				numberDisplays.Add (tempObject.GetComponent<SpriteRenderer>());
				DisplayLocation.x -= distBetweenPoints;
			}

			for(int i = 0; i < numberDisplays.Count; i++)
			{
				if(scoreList.Count > i)
				{
					numberDisplays[i].sprite = numbers[scoreList[i]];
					numberDisplays[i].enabled = true;
				}
				else
				{
					numberDisplays[i].enabled = false;
				}
			}


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
