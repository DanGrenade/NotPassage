using UnityEngine;
using System.Collections;

public class AITypeThree : AI
{
	public override void ChildStart()
	{
		currentAIState = AIState.Ignore;
	}
}
