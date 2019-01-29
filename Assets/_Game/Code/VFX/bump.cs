using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bump : MonoBehaviour
{
	private float animPercent=0;
	private bool bumped = false;
	private bool nudged = false;
	private Vector3 initialPos;
	private float bumpTime=0;
	private float bumpDuration=0.2f;
	private float nudgeTime=0;
	private float nudgeDuration=0.1f;
	//private float nudgeCoolDown=0.0f;
	//private float nudgeCoolDownDuration=0.5f;
	private Transform childTransform;
	
	private Quaternion initialRot;
	
	void Start()
	{
		childTransform = GetComponentsInChildren<Transform>()[1];

		initialPos = childTransform.position;
		initialRot = childTransform.rotation;
		bumpTime=bumpDuration;
		nudgeTime=nudgeDuration;
	}

    void OnCollisionEnter(Collision collision)
    {
		if(!bumped)
		{
			if(collision.collider.tag == "Player")
			{
				bumpTime=bumpDuration;
				bumped = true;
				
			}
		}
    }
	
    void OnCollisionStay(Collision collision)
    {
		if(!bumped)
		{
			if(!nudged)
			{
				if(collision.collider.tag == "Player")
				{
					nudgeTime=nudgeDuration;
					nudged = true;
				}
			}
		}
	}
	
	void Update()
	{
		if(bumped || nudged)
		{
			float scale=bumped?0.1f:0.025f;
			float rotScale=bumped?4.0f:0.8f;
			Vector3 randOffset = new Vector3(Random.Range(-1,1)*scale,Random.Range(0,2)*scale,Random.Range(-1,1)*scale);
			Vector3 randRot = new Vector3(Random.Range(-1,1)*rotScale,Random.Range(0,2)*rotScale,Random.Range(-1,1)*rotScale);

			childTransform.position = initialPos+randOffset;
			childTransform.rotation = Quaternion.Euler(randRot);
			
			if (bumped)
			{
				bumpTime-=Time.deltaTime;
				
				if(bumpTime<=0)
				{
					childTransform.position = initialPos;
					childTransform.rotation = initialRot;
					bumped=false;
				}
			}
			
			else if (nudged)
			{
				nudgeTime-=Time.deltaTime;
				
				if(nudgeTime<=0)
				{
					childTransform.position = initialPos;
					childTransform.rotation = initialRot;
					nudged=false;
				}
			}

		}
	}
}
