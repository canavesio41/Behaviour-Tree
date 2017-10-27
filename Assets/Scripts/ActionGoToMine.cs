using UnityEngine;
using System.Collections;

public class ActionGoToMine : Leaf
{
	
	public override int run()
	{		
		WorkerBehaviour workerScript = miner.GetComponent<WorkerBehaviour> ();

		workerScript.target = GetClosestMine ();


		if(workerScript.target == null)
		{
			return (int)States.FALSE;	
		}

		System.Array.Resize(ref workerScript.PathToFollow, 0);
		if (workerScript.target.GetComponent<Mine> ().WorkerIntoTheMine.Count == 1) 
		{
			return (int)States.FALSE;	
		}

		else
		{
			workerScript.target.GetComponent<Mine> ().AddWorker (workerScript);
		}

		if(workerScript.PathToFollow == null || workerScript.PathToFollow.Length == 0)
		{
			PathRequestManager.RequestPath(workerScript.gameObject, workerScript.target, workerScript.OnPathFound);
			return (int)States.TRUE;
		}

		else
		{
			return (int)States.RUNNING;
		}

		return (int)States.RUNNING;
	}


	GameObject GetClosestMine() 
	{
		GameObject[] mines = GameObject.FindGameObjectsWithTag("Mine");

		float minDist = 99999;
		GameObject closestMine = null;

		float x = miner.transform.position.x;
		float y = miner.transform.position.y;

		foreach (GameObject mine in mines)
		{
			float dist = Mathf.Abs(mine.transform.position.x - x) + Mathf.Abs(mine.transform.position.y - y);

			if (dist < minDist)
			{
				minDist = dist;
				closestMine = mine;
			}
		}

		return closestMine;
	}

}

