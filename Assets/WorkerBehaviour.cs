using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerBehaviour : MonoBehaviour
{
	#region private variables
	private int MaxGold = 100;
	#endregion

	#region public variables
	public Sequencer mainSeq = null;
	public GameObject[] PathToFollow;
	public int GoldInBag = 0; 
	//public bool isMoving = false;
	public int speed = 0;
	public int targetIndex = 0;
	public GameObject target;
	#endregion

	private void Start ()
	{
		createBehaviourTree ();
	}
	
	private void Update () 
	{
		if(mainSeq.children.Count > 0)
			mainSeq.run();
		
		Debug.Log(mainSeq.childrenPos);

	}

	private void createBehaviourTree()
	{
		//Secuencia
		mainSeq = new Sequencer ();

		//crea secuencia de la mina
		Sequencer mineSeq = new Sequencer ();
		mainSeq.children.Add (mineSeq);

		//pregunta si existen minas
		QuestionMineAvailable availableQuestion = new QuestionMineAvailable();
		mineSeq.children.Add (availableQuestion);

		// se mueve a la mina
		ActionGoToMine gotoMineAction = new ActionGoToMine();
		mineSeq.children.Add(gotoMineAction);
		// se pone a minar
		ActionMine mineAction = new ActionMine();
		mineSeq.children.Add(mineAction);

		//crea secuencia para depositar
		Sequencer depositSeq = new Sequencer();
		mainSeq.children.Add(depositSeq);

		//se mueve al almacen
		ActionGoToDeposit gotoDepositAction = new ActionGoToDeposit();
		depositSeq.children.Add(gotoDepositAction);
		//se va a depositar
		ActionDeposit depositAction = new ActionDeposit(); 
		depositSeq.children.Add(depositAction);

		mainSeq.miner = gameObject;
		mineSeq.miner = gameObject;
		availableQuestion.miner = gameObject;
		gotoMineAction.miner = gameObject;
		mineAction.miner = gameObject;
		depositSeq.miner = gameObject;
		gotoDepositAction.miner = gameObject;
		depositAction.miner = gameObject;
	}

	public void OnPathFound(GameObject[] newPath, bool pathSuccessful)
	{
		if (pathSuccessful) 
		{
			this.PathToFollow = newPath;
			this.targetIndex = 0;
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}
		
	public IEnumerator FollowPath()
	{
		Vector3 currentWaypoint = this.PathToFollow[0].transform.position;
		while (true)
		{
			if (this.transform.position == currentWaypoint) 
			{
				this.targetIndex ++;
				if (this.targetIndex >= this.PathToFollow.Length) 
				{
					yield break;
				}
				currentWaypoint = this.PathToFollow [targetIndex].transform.position;
			}

			this.transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
			yield return null;
		}
	}
		
	public bool AddGold()
	{
		GoldInBag++;
		return (GoldInBag >= MaxGold);
	}

	public bool SubstractGold()
	{
		GoldInBag--;
		return (GoldInBag <= 0);
	}

	public bool HasGold()
	{
		return (GoldInBag > 0);
	}
}
