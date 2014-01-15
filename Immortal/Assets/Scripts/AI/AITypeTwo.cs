using UnityEngine;
using System.Collections;

public class AITypeTwo : AI
{

	public override void ChildStart()
	{
		currentAIState = AIState.MoveTowards;
	}
}
