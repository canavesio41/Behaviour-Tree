using UnityEngine;
using System.Collections;

public class QuestionMineAvailable : Leaf
{
	public override int run ()
	{
		if(GameObject.FindGameObjectsWithTag("Mine").Length > 0)
		{
			return(int)States.TRUE;
		}	
		return (int)States.FALSE;
	}
}

