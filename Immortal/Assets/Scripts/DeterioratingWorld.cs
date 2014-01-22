using UnityEngine;
using System.Collections;

public class DeterioratingWorld : MonoBehaviour 
{
	public float[] deteriorateTimes;
	public Sprite[] deteriorateSprites;

	public CameraScript gameControl;

	private float time;
	private int currentSprite;

	private SpriteRenderer spriter;
	private Color worldOne;
	private Color worldTwo;

	public float transitionSpeed;

	public SpriteRenderer secondaryWorld;

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

		if(spriter.sprite != deteriorateSprites[currentSprite])
		{

			worldOne = spriter.color;
			worldTwo = worldOne;


			worldOne.a = Mathf.Lerp(worldOne.a, 0, Time.deltaTime * transitionSpeed);
			worldTwo.a = 1 - worldOne.a;
			secondaryWorld.color = worldTwo;


			spriter.color = worldOne;

			if(worldOne.a < 0.0001f)
			{
				spriter.sprite = deteriorateSprites[currentSprite];
				worldOne.a = 1;
				spriter.color = worldOne;

				if(deteriorateSprites.Length < currentSprite + 1)
				{
					secondaryWorld.sprite = deteriorateSprites[currentSprite + 1];					                                
				}
				worldOne.a = 0;
				secondaryWorld.color = worldOne;
			}
		}
	}
}
