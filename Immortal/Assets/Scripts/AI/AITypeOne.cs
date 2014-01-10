using UnityEngine;
using System.Collections;

public class AITypeOne : AI 
{
	public override void ChildStart()
	{
		currentAIState = AIState.Run;
	}
}
