using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sky : MonoBehaviour
{
	GameObject flyManager;
	private MeshRenderer rend;
	float omniousValue = 0;
	
	
    void Start()
    {
		rend = GetComponent<MeshRenderer>();
		flyManager = GameObject.Find("FlyManager");
    }

    void Update()
    {
		
		FlyManager fly = flyManager.GetComponent<FlyManager>();
		bool birdIsActive = fly.birdIsActive;
		bool birdIsApproaching = fly.birdIsApproaching;
		bool peter = birdIsActive && !birdIsApproaching;

		omniousValue+= (birdIsActive?1:-1)*Time.deltaTime;
		omniousValue=Mathf.Clamp01(omniousValue);
		rend.material.SetFloat("_Peter", omniousValue);		
    }
}
