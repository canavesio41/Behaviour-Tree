using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum States
{
	TRUE, 
	FALSE,
	RUNNING,
}

public class NodeWithChildren : NodoBehaviourTree 
{
	public List<NodoBehaviourTree> children = new List<NodoBehaviourTree>();
	public int childrenPos = 0;
}

public class Leaf : NodoBehaviourTree 
{
	
}

public class Sequencer : NodeWithChildren 
{
	public override int run() 
	{
		for (int i = childrenPos; i < children.Count; i++) 
		{
			int childState = children[i].run();

			if (childState == (int)States.FALSE) 
			{
				childrenPos = 0;
				return (int)States.FALSE;
			}

			if (childState == (int)States.RUNNING) 
			{
				return (int)States.RUNNING;
			}

			if (childState == (int)States.TRUE)
			{
				childrenPos++;
			}
		}

		childrenPos = 0;
		return (int)States.TRUE;
	}
}
