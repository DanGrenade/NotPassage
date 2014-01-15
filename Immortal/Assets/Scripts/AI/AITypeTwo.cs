using UnityEngine;
using System.Collections;

public class AITypetwo : AI
{

	public override void ChildStart()
	{
		currentAIState = AIState.MoveTowards;
	}
}
