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

	private List<AI> aiFollowers = new List<AI>();
	private List<Vector2> followerPosition = new List<Vector2>();
	public Vector2 currentPosition;
	public float distanceBetweenAI;
	private int numberAtInColumn;
	private int maxInColumn = 1;

	private Sprite[] numbers;

	public GameObject pointParent;
	private bool bounce = false;
	public float defaultBounce;
	public float scaledBounce;
	public float pointScale;


	public GameObject gravestonePrefab;

	public GameObject statuePrefab;
	public float statueScore;
	private bool statueExists = false;
	public GameObject secondStatuePrefab;
	public float secondStatueScore;
	private bool secondStatueExists = false;

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
			tempObject.transform.parent = pointParent.transform;
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
			scoreDisplayedFloat = Mathf.Lerp (scoreDisplayedFloat, Score, 1f);
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
				tempObject.transform.parent = pointParent.transform;
				tempObject.transform.eulerAngles = pointParent.transform.eulerAngles;
				tempObject.transform.localPosition = DisplayLocation;
				tempObject.transform.localScale = numberDisplays[numberDisplays.Count - 1].transform.localScale;
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

		if(numberDisplays.Count % 2 != 0)
		{
			int leftMost = (numberDisplays.Count - 1)/2;
			for(int i = 0; i < numberDisplays.Count; i++)
			{
				numberDisplays[i].transform.localPosition = -Vector2.right * distBetweenPoints * ((i - (float)leftMost) - (float)leftMost/2);
			}
		}
		else
		{
			int leftMost = numberDisplays.Count/2;
			for(int i = 0; i < numberDisplays.Count; i++)
			{
				numberDisplays[i].transform.localPosition = -Vector2.right * distBetweenPoints * ((i - (float)leftMost) + (float)leftMost/2);
			}
		}

		if (Score == statueScore && statueExists == false) 
		{
			Instantiate(statuePrefab, transform.position, transform.rotation);
			statueExists = true;
		}
		if (Score == secondStatueScore && secondStatueExists == false) 
		{
			Instantiate(secondStatuePrefab, transform.position, transform.rotation);
			secondStatueExists = true;
		}

		if(bounce)
		{
			pointParent.transform.localScale = Vector2.Lerp(pointParent.transform.localScale, Vector2.one * (pointScale + (pointScale * Score)), 0.2f);
			if(pointParent.transform.localScale.x < 0.01f)
			{
				pointParent.transform.localScale = Vector2.one;
				bounce = false;
			}
		}
	}

	public void AddScore(int _scoreToAdd, AI _ai)
	{
		Score += _scoreToAdd;
		aiFollowers.Add(_ai);
		while(aiFollowers.Count > followerPosition.Count)
		{
			if(numberAtInColumn <= maxInColumn)
			{
				numberAtInColumn++;
				currentPosition.y += distanceBetweenAI;
				followerPosition.Add(currentPosition);

			}
			else
			{
				numberAtInColumn = 2;
				maxInColumn++;
				currentPosition.x -= distanceBetweenAI;
				currentPosition.y = -(distanceBetweenAI * (maxInColumn/2));

				followerPosition.Add(currentPosition);
			}
		}

		pointParent.transform.localScale = Vector2.one * (defaultBounce + (pointScale * Score));
		bounce = true;

		aiFollowers[aiFollowers.Count - 1].FollowGoTo = followerPosition[aiFollowers.Count - 1];
	}

	public void RemoveScore(int _scoreToRemove, AI _ai)
	{
		GameObject.Instantiate(gravestonePrefab, _ai.transform.position, _ai.transform.rotation);

		Score -= _scoreToRemove;
		aiFollowers.Remove(_ai);

		for(int i = 0; i < aiFollowers.Count; i++)
		{
			aiFollowers[i].FollowGoTo = followerPosition[i];
		}
	}

}
