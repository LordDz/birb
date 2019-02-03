using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeInOnStart : MonoBehaviour
{
	private bool fadeIn = false;
	private float fadeSpeed=5;
	private float alpha = 0;
	private Vector4 initColor;
	private SpriteRenderer sprite;
	
	void Start()
	{
		sprite = gameObject.GetComponent<SpriteRenderer>();
		initColor = sprite.color;
	}

    void Update()
    {
		if(Input.GetButtonUp("Action"))
		{
			fadeIn = true;
		}
		if(fadeIn)
		{
			alpha=Mathf.Clamp01(alpha+fadeSpeed*Time.deltaTime);
			Vector4 clr = new Vector4(initColor[0],initColor[1],initColor[2],alpha);
			sprite.color = clr;
		}
    }
}
