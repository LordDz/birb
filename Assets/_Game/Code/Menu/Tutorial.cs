using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
	private float startTime;
	private float elapsed;
	
	//Player
	private PlayerController pc;
	
	bool isOnEgg = false;
	bool hasCarriedTowel = false;
	bool isCarryingTowel = false;
	bool hasCarriedHotTowel = false;
	bool carriedTowelBeforeJumpinOnEgg = false;
	
	
	//Move buttons
	private string[] moveButtonNames=new string[] {"w_button","a_button","s_button","d_button"};
	//private GameObject[] moveButtonsObj=new GameObject[4];
	private spriteFadeInOut[] moveButtons = new spriteFadeInOut[4];
	private bool moveButtonsPressed=false;
		
	//Jump button
	private string jumpButtonName="space_button";
	private spriteFadeInOut jumpButton;
	private bool jumpedOnEgg=false;
	
	//Pickup button
	private string pickupButtonName="e_button";
	private spriteFadeInOut pickupButton;
	public bool itemPickedUp=false;

	//Stove
	private string stoveButtonName="e_button_stove";
	private spriteFadeInOut stoveButton;
	private bool placedOnStove=false;
	
	//Stove hot
	private string stoveHotButtonName="e_button_stoveHot";
	private spriteFadeInOut stoveHotButton;
	//private bool hotTovelOnStove=false;


	//Egg
	private string eggButtonName="e_button_egg";
	private spriteFadeInOut eggButton;
	private bool placedOnEgg=false;
	
	//Hiding spot
	private bool hidingSpotVisited = false;
	private bool hidingSpotDone = false;
	private string hidingSpotName = "Cover";
	private List<GameObject> hidingSpots = new List<GameObject>();
	
	void Awake()
	{
				hidingSpots.AddRange(GameObject.FindGameObjectsWithTag(hidingSpotName));
				this.enabled=false;
	}
	
    
    void Start()
    {
		
		startTime = Time.time;
		
		hidingSpots.AddRange(GameObject.FindGameObjectsWithTag(hidingSpotName));
		
		float ci=0;
		foreach (GameObject go in hidingSpots)
		{
			spriteFadeInOut cover = go.GetComponent<spriteFadeInOut>();
			cover.fade = 0;
			cover.run = true;
			cover.blink = true;
			cover.speed = 2;
			cover.fade = ci/hidingSpots.Count;
			ci++;
		}
				
		pc = GameObject.Find("Player").GetComponent<PlayerController>();
		
		for (int i = 0; i < moveButtonNames.Length; i++)
		{	
			moveButtons[i]=GameObject.Find(moveButtonNames[i]).GetComponent<spriteFadeInOut>();
		}
        pickupButton=GameObject.Find(pickupButtonName).GetComponent<spriteFadeInOut>();
        stoveButton=GameObject.Find(stoveButtonName).GetComponent<spriteFadeInOut>();
		stoveHotButton=GameObject.Find(stoveHotButtonName).GetComponent<spriteFadeInOut>();
		eggButton=GameObject.Find(eggButtonName).GetComponent<spriteFadeInOut>();
		
    }

    void Update()
    {	
		isOnEgg=pc.tutorialIsOnOrTransitioningToEgg;
		bool isCarryingHotTowel = false;
		if(pc.itemInHand)
		{
			hasCarriedTowel = true;
			isCarryingTowel = true;
			
			isCarryingHotTowel = pc.itemInHand.GetHeat() > 0; //Essentially we're always carrying a hot towel after it's been heated.. but whatevs.
			if(isCarryingHotTowel)
			{
				hasCarriedHotTowel=true;
			}
		}
		else
		{
			isCarryingTowel = false;
		}
		if(pc.tutorialIsCovered)
		{
			hidingSpotVisited=true;
		}

		
		//Fetch it here since it might not available during Start()
		if(jumpButton==null)
		{
			jumpButton=GameObject.Find(jumpButtonName).GetComponent<spriteFadeInOut>();
		}
		
		elapsed=Time.time-startTime;
		
		float moveButtonsDelay=0.0f;
		
		if (elapsed > moveButtonsDelay)
		{
			for(int i =0; i < moveButtons.Length; i++)
			{
				if(elapsed > moveButtonsDelay + i*0.1f)
				{
					moveButtons[i].fadeIn=!moveButtonsPressed;
				}
				
				float tresh=0.1f;
				if(Mathf.Abs(Input.GetAxis("Horizontal"))>tresh||Mathf.Abs(Input.GetAxis("Vertical"))>tresh)
				{
					moveButtonsPressed=true;
				}
			}
		}

		//Egg
		if(moveButtonsPressed)
		{
			jumpButton.fadeIn=!jumpedOnEgg&&!isCarryingTowel;
			
			if(pc.tutorialHasSatOnEgg == true)
			{
				if(!jumpedOnEgg)
				{
					carriedTowelBeforeJumpinOnEgg=hasCarriedTowel;
				}
				jumpedOnEgg=true;
			}
		}        

		//Towel on ground
		if(jumpedOnEgg&&!carriedTowelBeforeJumpinOnEgg)
		{
			pickupButton.fadeIn=!itemPickedUp&&!isOnEgg;
			if(pc.itemInHand)
			{
				itemPickedUp=true;
			}
		}
		
		
		
		//Stove
		if((isCarryingTowel||hasCarriedTowel))
		{
			stoveButton.fadeIn=!placedOnStove&&isCarryingTowel;
			if(pc.tutorialHasPlacedTovelOnStove)
			{
				placedOnStove=true;
			}
		}
		
		//Placed on stove
		if(placedOnStove)
		{
			stoveHotButton.fadeIn=!hasCarriedHotTowel&&!isOnEgg;
		}
		
		//Egg
		if((isCarryingHotTowel|hasCarriedHotTowel))
		{
			bool droppedTowelBlink=(Time.time*4)%2>1;
			eggButton.fadeIn=!placedOnEgg&&(isCarryingHotTowel?true:droppedTowelBlink)&&!isOnEgg;
			if(pc.tutorialHasPlacedHeatedTovelOnEgg)
			{
				placedOnEgg=true;
			}
		}
		
		
		/*
		Actually, never mind... the heat doesn't really drop to zero, so this is pretty futile, 
		beside, it seems buggy, let's just keep the way it is for now.
		
		// If we've carried a hot towel at one point, are carrying a towel, but it isn't hot any more, 
		// and we haven't placed it on the egg, then reset stuff back to the stove
		if(hasCarriedHotTowel&&!isCarryingHotTowel&&isCarryingTowel&&!placedOnEgg)
		{
			pc.tutorialHasPlacedTovelOnStove=false;
			placedOnStove=false;
			hasCarriedHotTowel=false;
		}
		*/
		
		if (hidingSpotVisited && !hidingSpotDone)
		{
			foreach (GameObject go in hidingSpots)
			{
				spriteFadeInOut cover = go.GetComponent<spriteFadeInOut>();
				
				cover.blink = false;
				cover.fade = 1;
				cover.run = false;
			}
			
			hidingSpotDone = true;
		}
		
		//Turn off self if we are done
		if(hidingSpotDone && placedOnEgg)
		{
			enabled=false;
		}
    }
}
