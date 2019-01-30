using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteFadeInOut : MonoBehaviour
{
	private SpriteRenderer sprite;
	private Color initColor;
	public float fade = 0;
	public bool fadeIn=true;
	public float speed = 3;
	private bool turnedOn = true;
	public bool run = true;
	public bool blink = false;
	private bool running = false;

		
    // Start is called before the first frame update
    void Start()
    {
		sprite = GetComponentInChildren<SpriteRenderer>();
		initColor = sprite.color;
    }

    // Update is called once per frame
    void Update()
    {
		if(run)
		{
			running = true;
			float t=Time.deltaTime*speed;
			fade+=fadeIn?t:-t;
			fade=Mathf.Clamp01(fade);
			Color clr = new Color(initColor[0],initColor[1],initColor[2],initColor[3]*fade);
			sprite.color = clr;
			
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
		}
		//Clean up, if we stop to run, set fade to whatever fade comes in externally, then stop running
		else if(running)
		{
			Color clr = new Color(initColor[0],initColor[1],initColor[2],initColor[3]*fade);
			sprite.color = clr;
			running = false;
		}
    }
}
