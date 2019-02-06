using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteFadeInOut : MonoBehaviour
{
	private SpriteRenderer sprite;
	private Color initColor;
	public float fade = 0;
	private float lastFade = 0;
	public bool fadeIn=true;
	public float speed = 3;
	private bool turnedOn = true;
	public bool run = true;
	public bool blink = false;
	private bool running = false;
	public float delay = 0;
	public float delayReverse = -1;
	private float initTime;
	private bool reversed = false;
	public float alphaOverride = -1;

	void applyFade()
	{
		Color clr = new Color(initColor[0],initColor[1],initColor[2],initColor[3]*fade);
		sprite.color = clr;
	}
	
    void Start()
    {
		sprite = GetComponentInChildren<SpriteRenderer>();
		initColor = sprite.color;
		initTime = Time.time;
		applyFade();
    }

    void Update()
    {
		//if(gameObject.name == "Cover")
		//{
		//	Debug.Log(blink);
		//}
		if(run)
		{	
			if(Time.time >= initTime+delay)
			{
				running = true;
				float t=Time.deltaTime*speed;
				fade+=fadeIn?t:-t;
				fade=Mathf.Clamp01(fade);
				applyFade();
				
				
				if (blink)
				{
					if (fade >=1)
					{
						fadeIn=false;
					}
					else if(fade<=0)
					{
						fadeIn=true;
					}
				}
				else if(!blink)
				{
					if(fade<=0)
					{
						if(turnedOn)
						{
							sprite.enabled=false;
							turnedOn=false;
						}
					}
					else
					{
						if(!turnedOn)
						{
							sprite.enabled=true;
							turnedOn=true;
						}
					}
				}
				
				if(!reversed)
				{
					if(delayReverse >0)
					{
						if(Time.time > initTime+delay+delayReverse)
						{
							fadeIn=!fadeIn;
							reversed=true;
						}
					}
				}
			}
			
			if(alphaOverride>=0)
			{
				Color clr = new Color(initColor[0],initColor[1],initColor[2],alphaOverride);
				sprite.color = clr;			
			}				
		}
		
		//Clean up, if we stop to run, set fade to whatever fade comes in externally, then stop running
		else if(running)
		{
			applyFade();
			running = false;
		}
    }
}
