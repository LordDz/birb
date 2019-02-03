using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startGameFromMenu : MonoBehaviour
{
	private float startDelay=0.25f;
	private float pressTime=0;
	private bool start = false;
	private bool started = false;
	private GameObject tutorial;
	
	public GameObject[] activateGameObjectsOnStart;

    void Start()
	{
		tutorial=GameObject.Find("Tutorial");
	}
    void Update()
    {
		if(Input.GetButtonUp("Action"))
		{
			start = true;
			pressTime=Time.time;
		}
		if(start)
		{
			if(Time.time > pressTime+startDelay)
			{
				foreach (GameObject go in activateGameObjectsOnStart)
				{
					go.SetActive(true);
				}
				tutorial.GetComponent<Tutorial>().enabled=true;
				gameObject.SetActive(false);
			}
		}
    }
}
