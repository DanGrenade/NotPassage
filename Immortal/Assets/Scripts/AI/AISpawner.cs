using UnityEngine;
using System.Collections;

public class AISpawner : MonoBehaviour 
{
	public enum AreaState
	{
		One, Two, Three, Four
	}
	public AreaState currentState = AreaState.Four;

	#region Spawning AI
	public float Radius;
	public GameObject[] AIPrefabs;
	private GameObject temp;

	private float nextAISpawnTime;
	public float[] TimerStates;
	#endregion

	#region 
	public float MaxTime;
	private float currentTime;
	public float[] timeForStates;
	public float rateOfDecay;
	#endregion

	public void Start()
	{
		currentTime = MaxTime;
	}

	public void Update()
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
			}
			break;
		case AreaState.Two:
			if(currentTime < timeForStates[1])
			{
				currentState = AreaState.Three;
			}
			else if(currentTime > timeForStates[0])
			{
				currentState = AreaState.One;
			}
			break;
		case AreaState.Three:
			if(currentTime < timeForStates[2])
			{
				currentState = AreaState.Four;
			}
			else if(currentTime > timeForStates[1])
			{
				currentState = AreaState.One;
			}
			break;
		case AreaState.Four:
			if(currentTime < 0)
			{
				//Destroy!
			}
			else if(currentTime > timeForStates[2])
			{
				currentState = AreaState.Three;
			}
			break;
		}
		#endregion
	}
}
