using UnityEngine;
using System.Collections;

public class ActionMine : Leaf {
	public override int run() {

		GameObject target = GetClosestMine();
		WorkerBehaviour minerScript = miner.GetComponent<WorkerBehaviour>();

		if (target == null) 
		{
			return (int)States.FALSE;
		}

		if (minerScript.targetIndex == minerScript.PathToFollow.Length) 
		{	
			bool bagFull = minerScript.AddGold();
			bool mineEmpty = target.GetComponent<Mine>().SubstractGold();
			
			if (bagFull)
			{
				return (int)States.TRUE;
			}

			if (mineEmpty)
			{
				return (int)States.TRUE;
			}
		}
		return (int)States.RUNNING;
	}

	GameObject GetClosestMine() {
		GameObject[] mines = GameObject.FindGameObjectsWithTag("Mine");

		float minDist = Mathf.Infinity;
		GameObject closestMine = null;

		float x = miner.gameObject.transform.position.x; //miner.GetComponent<EntityScript>().x;
		float y = miner.gameObject.transform.position.y; //miner.GetComponent<EntityScript>().y;

		foreach (GameObject mine in mines) {
			float dist = Mathf.Abs(mine.transform.position.x - x) + Mathf.Abs(mine.transform.position.y - y);

			if (dist < minDist) {
				minDist = dist;
				closestMine = mine;
			}
		}

		return closestMine;
	}
}
