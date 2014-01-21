using UnityEngine;
using System.Collections;

public class DeterioratingWorld : MonoBehaviour 
{
	public float[] deteriorateTimes;
	public Sprite[] detriorateSprites;

	public CameraScript gameControl;

	private float time;
	private int currentSprite;

	private SpriteRenderer spriter;

	void Start()
	{
		spriter = gameObject.GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update () 
	{
		if(gameControl.InGame)
		{
			time = 0;
		}
		else
		{
			time += Time.deltaTime;
		}

		for(int i = 0; i < deteriorateTimes.Length; i++)
		{
			if(time <= deteriorateTimes[i])
			{
				currentSprite = i;
			}
		}

		if(spriter.sprite != detriorateSprites[currentSprite])
		{
			spriter.sprite = detriorateSprites[currentSprite];
		}
	}
}
