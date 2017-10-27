using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionGoToDeposit : Leaf 
{

	public override int run() 
	{
		WorkerBehaviour workerScript = miner.GetComponent<WorkerBehaviour> ();

		workerScript.target = GetClosestDeposit ();

		if(workerScript.target  == null)
		{
			return (int)States.FALSE;	
		}


		//if(!workerScript.isMoving)
		//{

			if(workerScript.PathToFollow != null || workerScript.PathToFollow.Length > 0)
			{
				PathRequestManager.RequestPath(workerScript.gameObject, workerScript.target , workerScript.OnPathFound);
				return (int)States.TRUE;
			}
			else
			{
				return (int)States.RUNNING;
			}

		//}
	
		return (int)States.RUNNING;
	}

	GameObject GetClosestDeposit() 
	{
		GameObject[] deposits = GameObject.FindGameObjectsWithTag("Warehouse");

		float minDist = 99999;
		GameObject closestDeposit = null;

		float x = miner.transform.position.x;
		float y = miner.transform.position.y;

		foreach (GameObject deposit in deposits)
		{
			float dist = Mathf.Abs(deposit.transform.position.x - x) + Mathf.Abs(deposit.transform.position.y - y);

			if (dist < minDist)
			{
				minDist = dist;
				closestDeposit = deposit;
			}
		}

		return closestDeposit;
	}


}


public static class Utilities 
{
	public static void ClearArray<T>(T[] arr)
	{
		for ( int i = 0; i < arr.Length; i++)
		{
			arr[i] = default(T);
		}	
	}
		
}