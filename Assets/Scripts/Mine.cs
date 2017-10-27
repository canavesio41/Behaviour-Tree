using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class Mine : MonoBehaviour
{
	public int GoldInMine = 1000;
	public bool check = false;

	public List<WorkerBehaviour> WorkerIntoTheMine = new List<WorkerBehaviour> ();


	void Update ()
	{
		if(GoldInMine <=  0)
		{
			Die ();	
		}

	}

	public void Die()
	{
		if(check == false)
		{
			MinesManager.sharedInstance.deactivateMines.Add (this.gameObject);
			MinesManager.sharedInstance.activateMines.Remove (this.gameObject);
			check = true;
			gameObject.SetActive (false);
		}
	}

	public void AddWorker( WorkerBehaviour go)
	{
		WorkerIntoTheMine.Add (go);
	}

	public void RemoveWorker(WorkerBehaviour go)
	{
		WorkerIntoTheMine.Remove (go);
	}

	public bool SubstractGold()
	{
		GoldInMine--;
		return (GoldInMine <= 0);
	}

}

