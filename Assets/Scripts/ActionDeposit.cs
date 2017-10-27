using UnityEngine;
using System.Collections;

public class ActionDeposit : Leaf {
	public override int run() {

		GameObject target = GetClosestDeposit();
		WorkerBehaviour minerScript = miner.GetComponent<WorkerBehaviour>();

		if (target == null)
		{
			return (int)States.FALSE;
		}
		if(minerScript.targetIndex >= minerScript.PathToFollow.Length)
		{
			if (minerScript.HasGold()) 
			{
				target.GetComponent<House>().AddGold();

				if (minerScript.SubstractGold()) 
				{
					if(minerScript.GoldInBag == 0)
						return (int)States.TRUE;
				}
			}
		}

		return (int)States.RUNNING;
	}

	GameObject GetClosestDeposit() {
		GameObject[] deposits = GameObject.FindGameObjectsWithTag("Warehouse");

		float minDist = 99999;
		GameObject closestDeposit = null;

		float x = miner.transform.position.x;
		float y = miner.transform.position.y;

		foreach (GameObject deposit in deposits)
		{
			float dist = Mathf.Abs(deposit.transform.position.x - x) + Mathf.Abs(deposit.transform.position.y - y);

			if (dist < minDist) {
				minDist = dist;
				closestDeposit = deposit;
			}
		}

		return closestDeposit;
	}
}
