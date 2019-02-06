using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class reloadScene : MonoBehaviour
{
	private bool reloadEnabled = false;
	private float userInitiateReloadTime = -1;
	private float buttonPressDelay = 0.25f;
	private float fadeDelay = 0.5f;
	private bool startFading = false;
	private bool showResetButton = false;
	private float resetButtonDisplayDelay = 1.0f;
	private float reloadEnabledTime=-1;
	private GameObject foreground;
	private GameObject resetButton;
	
	void Start()
	{
		foreground = GameObject.Find("Foreground");
		resetButton = GameObject.Find("resetButton");
	}
	
	public void reload()
	{
		if(!reloadEnabled)
		{
			reloadEnabledTime = Time.time;
			reloadEnabled = true;
		}
	}
	
	void Update()
	{
		if(reloadEnabled)
		{
			if(!showResetButton && Time.time > reloadEnabledTime+resetButtonDisplayDelay)
			{
				resetButton.GetComponent<spriteFadeInOut>().fadeIn=true;
				showResetButton=true;
			}
			
			if(reloadEnabled && userInitiateReloadTime > 0)
			{
				if(!startFading && Time.time > userInitiateReloadTime+buttonPressDelay)
				{	
					foreground.GetComponent<SpriteRenderer>().enabled = true;
					spriteFadeInOut spr = foreground.GetComponent<spriteFadeInOut>();
					spr.fade=0;
					spr.fadeIn=true;
					spr.run=true;
					startFading	= true;
				}
				if(Time.time > userInitiateReloadTime+buttonPressDelay+fadeDelay)
				{
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				}
			}
			
			//Don't wait for button to render, allow user to reset regardless
			if(userInitiateReloadTime<0)
			{
				if(Input.GetButtonUp("Action"))
				{
					userInitiateReloadTime=Time.time;
				}
			}
		}
	}

}
