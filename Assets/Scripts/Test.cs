using UnityEngine;
using System.Collections;

public  class Test : MonoBehaviour
{
	public Renderer r;
	public Color c;

	public void Awake ()
	{
		r = GetComponent<Renderer> ();
	}

	// Use this for initialization
	public void Start ()
	{
	
	}
	
	// Update is called once per frame
	public void Update ()
	{
		r.material.color = c;
	}
}

