using UnityEngine;
using System.Collections;

public class AISpawner : MonoBehaviour 
{
	public enum AreaState
	{
		One, Two, Three, Four
	}
	public AreaState currentState = AreaState.Four;

	public Vector2 PivotRotater;

	#region Spawning AI
	public float Radius;
	public GameObject[] AIPrefabs;
	private GameObject temp;

	private float nextAISpawnTime;
	public float[] TimerStates;
	#endregion

	#region Decay Times
	public float MaxTime;
	private float currentTime;
	public float[] timeForStates;
	public Sprite[] sprites;
	private SpriteRenderer render;
	public float rateOfDecay;
	#endregion

	private bool dead = false;
	private GameObject birds;
	public Vector2 birdLocation;

	public void Start()
	{
		currentTime = MaxTime;
		render = gameObject.GetComponent<SpriteRenderer> ();
		birds = (GameObject)Resources.Load ("Birds") as GameObject;
		nextAISpawnTime = TimerStates[0] + Time.time;

		Vector3 rotate = transform.eulerAngles;
		rotate.z = Vector2.Angle (Vector2.up, PivotRotater - (Vector2)transform.position);
		if (PivotRotater.x < transform.position.x) rotate.z -= 180;
		else rotate.z = 180 - rotate.z;
		transform.eulerAngles = rotate;

	}

	public void Update()
	{
		if(!dead)
		{
			#region Spawning AI
			if (nextAISpawnTime < Time.time) 
			{
				GameObject.Instantiate (AIPrefabs[Random.Range(0, AIPrefabs.Length)], 
				                        ((new Vector2(Radius * Mathf.Cos (Random.Range(0, 360) * Mathf.Deg2Rad), Radius * Mathf.Sin (Random.Range(0, 360) * Mathf.Deg2Rad))) + (Vector2)transform.position),
				                        Quaternion.identity);

				switch(currentState)
				{
				case AreaState.One:
					nextAISpawnTime += TimerStates[0];
					break;
				case AreaState.Two:
					nextAISpawnTime += TimerStates[1];
					break;
				case AreaState.Three:
					nextAISpawnTime += TimerStates[2];
					break;
				case AreaState.Four:
					nextAISpawnTime += TimerStates[3];

					break;
				}
			}
			#endregion

			#region Decay
			currentTime -= (rateOfDecay * Time.deltaTime);

			switch(currentState)
			{
			case AreaState.One:
				if(currentTime < timeForStates[0])
				{
					currentState = AreaState.Two;
					render.sprite = sprites[1];
				}
				if(nextAISpawnTime - Time.time > TimerStates[0])
				{
					nextAISpawnTime = Time.time + TimerStates[0];
				}
				break;
			case AreaState.Two:
				if(currentTime < timeForStates[1])
				{
					currentState = AreaState.Three;
					render.sprite = sprites[2];
				}
				if(nextAISpawnTime - Time.time > TimerStates[1])
				{
					nextAISpawnTime = Time.time + TimerStates[1];
				}
				break;
			case AreaState.Three:
				if(currentTime < timeForStates[2])
				{
					currentState = AreaState.Four;
					render.sprite = sprites[3];
				}
				if(nextAISpawnTime - Time.time > TimerStates[2])
				{
					nextAISpawnTime = Time.time + TimerStates[2];
				}
				break;
			case AreaState.Four:
				if(currentTime < 0)
				{
					dead = true;
					birds = (GameObject)GameObject.Instantiate(birds, birdLocation + (Vector2)transform.position, Quaternion.identity) as GameObject;
					birds.transform.eulerAngles = transform.eulerAngles;
				}
				break;
			}
			#endregion
		}
	}
}
