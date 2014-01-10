using UnityEngine;
using System.Collections;

public class AISpawner : MonoBehaviour 
{
	public enum AreaState
	{
		One, Two, Three, Four
	}
	public AreaState currentState = AreaState.Four;

	public float Radius;
	public GameObject[] AIPrefabs;
	private GameObject temp;

	private float nextAISpawnTime;
	public float[] TimerStates;
	
	public void Update()
	{
		if (nextAISpawnTime < Time.time) 
		{
			GameObject.Instantiate (AIPrefabs[Random.Range(0, AIPrefabs.Length)], 
			                        new Vector2(Radius * Mathf.Cos (Random.Range(0, 360) * Mathf.Deg2Rad), Radius * Mathf.Sin (Random.Range(0, 360) * Mathf.Deg2Rad)),
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
	}
}
