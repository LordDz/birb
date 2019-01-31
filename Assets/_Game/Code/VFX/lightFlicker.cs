using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightFlicker : MonoBehaviour
{
	GameObject flyManager;
	GameObject windowSelector;
	
	Light li;
	float initialIntensity;
	float currentIntensity;
	float prevIntensity;
	Vector4 initialColor;
	Vector4 currentColor;
	Vector4 prevColor;
	float curLerp=0;
	float prevLerp=0;
	float cycleOffset=0;
	
	public enum lightTypes {lamp,firePlace,directional,ambient};
	
	public lightTypes lightType;

	
    void Start()
    {
		flyManager = GameObject.Find("FlyManager");
		windowSelector = GameObject.Find("WindowSelector");
		
		
		li=gameObject.GetComponent<Light>();
		initialIntensity = li.intensity;
		currentIntensity=initialIntensity;
		prevIntensity=currentIntensity;
		initialColor=li.color;
		currentColor=initialColor;
		prevColor=currentColor;
		cycleOffset=gameObject.GetInstanceID()*7.134f;
    }


    void Update()
    {
		FlyManager fly = flyManager.GetComponent<FlyManager>();
		WindowSelector win = windowSelector.GetComponent<WindowSelector>();
		
		bool birdIsActive = fly.birdIsActive;
		bool birdIsApproaching = fly.birdIsApproaching;
		bool peter = birdIsActive && !birdIsApproaching;
		bool cameraShake = win.isSelectingPeter;

		
		if(lightType == lightTypes.lamp)
		{
		if (!birdIsActive || birdIsApproaching || cameraShake)
			{
					float bai = birdIsApproaching? 10:1;
					float bas = birdIsApproaching? 2:1;
					//currentIntensity=Mathf.Abs(Mathf.Sin(Time.time*3.14f)+Mathf.Sin(Time.time*147.3f)+Mathf.Sin(Time.time*35.73F))*0.1f+initialIntensity;
					float bop1=Mathf.Clamp01(((Mathf.Sin(Time.time*Mathf.PI*bas)+1)/2)-0.9f)*3*bai;
					float bop2=Mathf.Clamp01(((Mathf.Sin(Time.time*Mathf.PI*Mathf.PI*bas)+1)/2)-0.95f)*2*bai;
					float bop3=Mathf.Clamp01(((Mathf.Sin(Time.time*7.35f*bas)+1)/2)-0.95f)*5*bai;
					currentIntensity=(bop1+bop2+initialIntensity-bop3);
					float camShake = (Mathf.Sin(Time.time*30)+Mathf.Sin(Time.time*14))*Mathf.Sin(Time.time*73);
					currentIntensity=cameraShake?currentIntensity*camShake:currentIntensity;
			}
			else
			{
				currentIntensity=Mathf.Clamp01(prevIntensity-1.0f*Time.deltaTime);
			}
		}
		
		
		else if(lightType == lightTypes.firePlace)
		{
			float bai = birdIsApproaching? 1:1;
			float bas = birdIsApproaching? 1:1;
			float peterIntensity = cameraShake? 0.25f:1;
			float peterSpeed = cameraShake? 2:1;
			float peterFlicker1 = cameraShake?Mathf.Sin((Time.time+cycleOffset)*4.11f)*3:1;
			float peterFlicker2 = cameraShake?Mathf.Sin((Time.time+cycleOffset)*16)*3:1;
			float peterFlicker3 = cameraShake?Mathf.Sin((Time.time+cycleOffset)*2.57f)*3:1;
			
			float bop1=Mathf.Clamp01(((Mathf.Sin(Time.time*Mathf.PI*bas*peterSpeed*0.2f)+1)/2)-0.9f*peterFlicker1)*3*bai*peterIntensity;
			float bop2=Mathf.Clamp01(((Mathf.Sin(Time.time*Mathf.PI*Mathf.PI*bas*peterSpeed*0.3f)+1)/2)-0.95f*peterFlicker2)*2*bai*peterIntensity;
			float bop3=Mathf.Clamp01(((Mathf.Sin(Time.time*0.35f*bas*peterSpeed)+1)/2)-0.95f*peterFlicker3)*5*bai*peterIntensity;
			currentIntensity=(bop1+bop2+initialIntensity-bop3);				
		}
		
		else if(lightType == lightTypes.directional)
		{
			float strength =peter?0.75f:1;
			float dir = peter?-1:1;
			float speed = 1.0f;
			float motion = prevIntensity+strength*speed*Time.deltaTime*dir;
			curLerp=Mathf.Clamp01(prevLerp+1.0f*speed*Time.deltaTime*dir);
			float ci = peter?Mathf.Max(initialIntensity*strength,motion):Mathf.Min(initialIntensity*strength,motion);
			Vector4 peterColor = new Vector4(1,0.2f,0.2f,1);
			currentColor=Vector4.Lerp(Vector4.Lerp(initialColor,peterColor,0.2f),initialColor,curLerp);
			
			currentIntensity = ci;
		}
		
		else if(lightType == lightTypes.ambient)
		{
			curLerp+= (peter?1:-1)*Time.deltaTime;
			curLerp=Mathf.Clamp01(curLerp);
			currentIntensity=Mathf.Lerp(initialIntensity,initialIntensity*0.75f,curLerp);
		}
	
	
	li.intensity = currentIntensity;
	li.color=currentColor;
		
	
	if(currentIntensity <= 0)
		{
			li.enabled = false;
		}
		else
		{
			li.enabled = true;
		}
		prevIntensity=currentIntensity;
		prevColor=currentColor;
		prevLerp=curLerp;
    }
}
